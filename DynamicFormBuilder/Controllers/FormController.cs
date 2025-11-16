using DynamicFormBuilder.Data;
using DynamicFormBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DynamicFormBuilder.Controllers
{
    public class FormController:Controller
    {
        private readonly ApplicationDBContext _db;


        public FormController(ApplicationDBContext db)
        {
            _db = db;
        }
        // GET: /Form
        public IActionResult Index()
        {
            return View();
        }
        // GET: /Form/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: /Form/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] string title)
        {
            var labels = Request.Form["fieldLabel[]"].ToArray();
            var requireds = Request.Form["fieldRequired[]"].ToArray();
            var options = Request.Form["fieldOption[]"].ToArray();


            if (string.IsNullOrWhiteSpace(title))
            {
                ModelState.AddModelError("Title", "Title is required");
                return View();
            }


            var form = new Form { Title = title };


            for (int i = 0; i < labels.Length; i++)
            {
                var label = labels[i];
                var req = (i < requireds.Length && requireds[i] == "true");
                var opt = i < options.Length ? options[i] : null;


                if (string.IsNullOrWhiteSpace(label)) continue;


                form.Fields.Add(new FormField
                {
                    Label = label,
                    IsRequired = req,
                    SelectedOption = opt,
                    Position = i
                });
            }


            _db.Forms.Add(form);
            await _db.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult LoadData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 10;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var query = _db.Forms.AsQueryable();

                // SEARCH
                if (!string.IsNullOrWhiteSpace(searchValue))
                {
                    query = query.Where(f => f.Title.Contains(searchValue));
                }

                int recordsTotal = query.Count();

                // Pagination
                var result = query
                    .OrderBy(f => f.Id)
                    .Skip(skip)
                    .Take(pageSize)
                    .Select(f => new
                    {
                        id = f.Id,
                        title = f.Title
                    })
                    .ToList();

                return Json(new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }




        // GET: /Form/Preview/5
        public IActionResult Preview(long id)
        {
            var form = _db.Forms
                               .Include(f => f.Fields)
                               .FirstOrDefault(f => f.Id == id);

            if (form == null)
                return NotFound();

            return View(form);
        }

    }
}
