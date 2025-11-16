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
        public async Task<IActionResult> Index()
        {
            var forms = await _db.Forms.AsNoTracking().ToListAsync();
            return View(forms);
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
            // The fields will come as arrays: fieldLabel[], fieldRequired[], fieldOption[]


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


                if (string.IsNullOrWhiteSpace(label)) continue; // skip invalid


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
        // GET: /Form/Preview/5
        public async Task<IActionResult> Preview(long id)
        {
            var form = await _db.Forms.Include(f => f.Fields.OrderBy(ff => ff.Position)).FirstOrDefaultAsync(f => f.Id == id);
            if (form == null) return NotFound();
            return View(form);
        }
    }
}
