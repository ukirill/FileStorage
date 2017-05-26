using DBModel.Helpers;
using DBModel.Models;
using DBModel.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class StorageController : Controller
    {
        #region private 
        private NHDocumentRepository DocumentRepository { get; set; }
        private NHUserRepository UserRepository { get; set; }
        private IEnumerable<Document> SearchResult { get; set; }
        private IEnumerable<DocumentModel> GetDocsViewList(string q)
        {
            var user = UserRepository.GetUserWithDocs(User.Identity.Name);
            IEnumerable<Document> docsList;
            if (!string.IsNullOrWhiteSpace(q))
            {
                docsList = DocumentRepository.FindDocuments(user, q);
            }
            else docsList = user.Documents;

            var documents = docsList.Select(d => new DocumentModel()
            {
                Name = d.Name != null && d.Name.Length > 30
                            ? $"{d.Name.Substring(0, 30)}..."
                            : d.Name,
                OriginalFileName = d.OriginalFileName,
                Date = d.Date,
                Path = d.Id.ToString()
            });
            return documents;
        }
        #endregion

        public StorageController()
        {
            DocumentRepository = new NHDocumentRepository();
            UserRepository = new NHUserRepository();
        }

        // GET: Storage
        public ActionResult Index()
        {
            return RedirectToAction("Documents");
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, UploadModel model)
        {

            var user = UserRepository.GetUserByEmail(User.Identity.Name);
            if (file == null)
            {
                ModelState.AddModelError("", "Выберите файл");
                return View();
            }
            var document = new Document()
            {
                OriginalFileName = Path.GetFileName(file.FileName),
                Name = model.Name,
                Date = DateTime.Now,
                Author = user
            };
            if (DocumentRepository.CreateWithFile(document, file, СonfigHelper.ConnectionString, СonfigHelper.StoragePath))
            {
                ViewBag.Message = "Документ загружен";
                return View();
            }
            else
            {
                ViewBag.Message = "При загрузке произошла ошибка";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Documents(string SearchString)
        {
            var model = new StorageModel();
            model.DocumentsList = GetDocsViewList(SearchString);
            if (!string.IsNullOrWhiteSpace(SearchString)) model.SearchString = SearchString;
            return View(model);
        }

        public FileResult Download(string path, string originalFileName)
        {
            var user_id = UserRepository.GetUserByEmail(User.Identity.Name).Id.ToString();
            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(СonfigHelper.StoragePath, user_id, path));
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, originalFileName);
            }
            catch { return null; }
        }

    }
}