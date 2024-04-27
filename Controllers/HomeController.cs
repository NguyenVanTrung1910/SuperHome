using DataHome.Data;
using Microsoft.AspNetCore.Mvc;
using TestVersion.Models;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestVersion.Controllers
{
    public class HomeController : Controller
    {
        
        public readonly DataHomeContext _context;
        public HomeController(DataHomeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.home = "active";
            HttpContext.Session.SetString("loc", "a");

            return View();
        }

        [HttpGet]
       public IActionResult Search(int id,string input, string type, string address)//được dùng để tìm kiếm
        {
            ViewBag.searchName = input;
            ViewBag.searchType = type;
            ViewBag.searchAddress = address;
            if (input == null && type != "null" && address!="null")
            {                   
                var a = _context.Property.Include(p=>p.Type).Where(k=>k.Type.Name.Equals(type) && k.Address.Equals(address)).ToList() ??null ;
                return View("Search",a);
            }           
            else if(input != null && type == "null" && address != "null")
            {
                var a = _context.Property?.Include(p => p.Type).Where(k => k.Name.Equals(input) && k.Address.Equals(address)).ToList() ?? null;
                return View("Search", a);
            }
            else if (input != null && type != "null" && address == "null")
            {
                var a = _context.Property?.Include(p => p.Type).Where(k => k.Name.Equals(input)).ToList() ?? null;           
                return View("Search", a);
            }
            else if (input == null && type == "null" && address != "null")
            {
                var a = _context.Property?.Include(p => p.Type).Where(k => k.Address.Equals(address)).ToList() ?? null;            
                return View("Search", a);
            }
            else if (input == null && type != "null" && address == "null")
            {
                var a = _context.Property?.Include(p => p.Type).Where(k => k.Type.Name.Equals(type)).ToList() ?? null;            
                return View("Search", a);
            }
            else if (input != null && type == "null" && address == "null")
            {
                var a = _context.Property?.Include(p => p.Type).Where(k => k.Name.Equals(input)).ToList() ?? null;
                return View("Search", a);
            }else
            {
                var a = _context.Property?.Include(p => p.Type).Where(k => k.Name.Equals(input) && k.Address.Equals(address) && k.Type.Name.Equals(type)).ToList() ?? null ;             
                return View("Search", a);
            }
        }
        public IActionResult About() {//dùng để tạo ra các active cho bootrap
            ViewBag.about = "active";
            return View(); 
        }
        public IActionResult PropertyList() {//dùng để tạo ra các active cho bootrap
            ViewBag.property = "active";

            return View(); 
        }

        public IActionResult ViewList(int id)//được dùng để tìm kiếm
        {
            ViewBag.page = id;
            var a = _context.Property.Include(p=>p.Type).Where(p=>p.TypeId ==id).ToList();
            return View("Search", a);
        }
        public IActionResult PropertyType()
        {
            ViewBag.property = "active";
            return View(); 
        }
        public IActionResult PropertyAgent() {//dùng để tạo ra các active cho bootrap
            ViewBag.property = "active";
            return View(); 
        }
        public IActionResult Loc(string form)
        {
            var a = form ?? "a";          
            HttpContext.Session.SetString("loc", a);
            return View("Index");

        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            var user = _context.User?.SingleOrDefault(x => x.Name.Equals(name) && x.Password.Equals(password));
            if (user != null)
            {
                HttpContext.Session.SetString("name", name);//Gán các giá trị vào các session
                HttpContext.Session.SetString("pass", password);
                HttpContext.Session.SetString("role", user.Role );
                HttpContext.Session.SetInt32("id", user.Id);
                var claims = new List<Claim>//tạo ra claim để chứa các thuộc tính của client
                {
                    new Claim(ClaimTypes.Name, user.Name ),
                    new Claim(ClaimTypes.Role, user.Role ),
                };
                var claimsIdentity = new ClaimsIdentity( claims, CookieAuthenticationDefaults.AuthenticationScheme);// tạo claimIdentity để có các phương thức và thuộc tính cần dùng

                HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)); //phương thức SignInasync để tạo ra phiên đăng nhập
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }

        
        public IActionResult Logout()
        {
            //Xóa các Session để gán các giá trị mới
            HttpContext.Session.Remove("name");
            HttpContext.Session.Remove("pass");
            HttpContext.Session.Remove("role");
            HttpContext.Session.Remove("id");
            HttpContext.SignOutAsync();         
            return View("Login");
        }

        //Chuyển đến trang Create
        public IActionResult Create()
        {
            return View();
        }

        //Tạo account trong trang đăng nhập
        [HttpPost]
        public IActionResult Create([Bind("Id,Name,Password,Email,Role")] User user)
        {
            if(ModelState.IsValid)
            {
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        //Action đến View Account
        public IActionResult Account()
        {
            var a  = _context.User.Where(p=>p.Id == HttpContext.Session.GetInt32("id")).ToList();
            ViewBag.UserId = a[0].Id;
            return View(a);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}