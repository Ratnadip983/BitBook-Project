using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BitBookProj.BLL;
using BitBookProj.Models;
//using System.Web.UI.WebControls;

namespace BitBookProj.Controllers
{
    public class UsersController : Controller
    {
        private readonly FriendManager _friendManager = new FriendManager();

        private readonly UserManager _userManager = new UserManager();
        private readonly BitBookDbContext db = new BitBookDbContext();

        // GET: Users
        [Authorize]
        public ActionResult Index()
        {
            var users = _userManager.GetAll();
            return View(users);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            var user = _userManager.GetByEmail(User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(string Password, string ConfirmPassword)
        {
            if (Password.Trim() == ConfirmPassword.Trim())
            {
                var user = _userManager.GetByEmail(User.Identity.Name);
                user.Password = Password.Trim();
                user.ConfirmPassword = Password.Trim();
                if (_userManager.Update(user))
                    return RedirectToAction("NewsFeed");
                return View();
            }

            return View();
        }

        [Authorize]
        public ActionResult NewsFeed(int? friendId)
        {
            var user = new User();
            if (friendId == null)
                user = _userManager.GetByEmail(User.Identity.Name);
            else
                user = _userManager.GetById((int) friendId);
            ViewBag.LoginUser = _userManager.GetByEmail(User.Identity.Name);
            var statusManager = new StatusManager();
            ViewBag.StatusList = statusManager.GetStatusById(user.Id);

            return View(user);
        }

        [Authorize]
        public ActionResult Timeline()
        {
            var user = _userManager.GetByEmail(User.Identity.Name);

            var statusManager = new StatusManager();
            ViewBag.StatusList = statusManager.GetStatusById(user.Id);

            return View(user);
        }

        public JsonResult IsEmailExist(string email)
        {
            return Json(!db.Users.Any(c => c.Email == email), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string name)
        {
            var users = _userManager.GetBySearchString(name);

            return View(users);
        }

        [HttpPost]
        public ActionResult AddFriend(int friendId)
        {
            var user = _userManager.GetByEmail(User.Identity.Name);
            var friend = new Friend();
            friend.User1Id = user.Id;
            friend.User2Id = friendId;
            friend.IsAccepted = false;

            _friendManager.sendFriendRequest(friend);
            return RedirectToAction("Search");
        }

        public ActionResult MyFriends()
        {
            var user = _userManager.GetByEmail(User.Identity.Name);
            var friends = _friendManager.FriendList(user);
            return View(friends);
        }

        public ActionResult ReceivedRequests()
        {
            var user = _userManager.GetByEmail(User.Identity.Name);
            var friends = _friendManager.ReceivedRequest(user);
            return View(friends);
        }

        public ActionResult Accept(int friendId)
        {
            var user = _userManager.GetByEmail(User.Identity.Name);
            var friend = _friendManager.GetReceivedFriendById(friendId, user.Id);
            friend.IsAccepted = true;
            _friendManager.AcceptFriend(friend);
            return RedirectToAction("ReceivedRequests");
        }

        public ActionResult Unfriend(int friendId)
        {
            var user = _userManager.GetByEmail(User.Identity.Name);
            var friend = _friendManager.GetFriendById(friendId, user.Id);
            _friendManager.RemoveFriend(friend);
            return RedirectToAction("MyFriends");
        }

        // GET: Users/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = _userManager.GetById((int) id);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NewsFeed");
            }
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,FirstName,LastName,Email,Gender,DateOfBirth,Password,ConfirmPassword,City,Country")] User user)
        {
            if (ModelState.IsValid)
            {
                _userManager.Add(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }


        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NewsFeed");
            }
                
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Response.Cache.SetNoStore();
            return RedirectToAction("Login");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(string lEmail, string lPassword)
        {
            var u = _userManager.GetByEmail(lEmail);
            if (u.Password == lPassword)
            {
                ViewBag.msg = null;
                FormsAuthentication.SetAuthCookie(u.Email, false);
                return RedirectToAction("NewsFeed");
            }
            ViewBag.msg = "Wrong Email or Password";
            return View();
        }

        // GET: Users/Edit/5
        [Authorize]
        public ActionResult Edit()
        {
            var user = _userManager.GetByEmail(User.Identity.Name);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(
            [Bind(Include = "Id,FirstName,LastName,DateOfBirth,City,Country")] User user)
        {
            User myUser = _userManager.GetById(user.Id);
            myUser.FirstName = user.FirstName;
            myUser.LastName = user.LastName;
            myUser.DateOfBirth = user.DateOfBirth;
            myUser.City = user.City;
            myUser.Country = user.Country;
            myUser.ConfirmPassword = myUser.Password;
            if(_userManager.Update(myUser))
                    return RedirectToAction("NewsFeed");
            return View(myUser); ;
           
        }

        // GET: Users/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = _userManager.GetById((int) id);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _userManager.GetById(id);
            _userManager.Delete(user);
            return RedirectToAction("Index");
        }

        //image upload
        public ActionResult ChangeProfilePic(HttpPostedFileBase ProfilePic)
        {
            if (ProfilePic != null)
            {
                var myPic = Image.FromStream(ProfilePic.InputStream, true, true);
                var imageInBytes = imageToByteArray(myPic);
           
                var user = _userManager.GetByEmail(User.Identity.Name);
                user.ConfirmPassword = user.Password;
                user.ProfilePic = imageInBytes;
                _userManager.Update(user);
            }
            
            return RedirectToAction("NewsFeed");
        }

        public ActionResult ChangeCoverPic(HttpPostedFileBase CoverPic)
        {
            
            if (CoverPic != null)
            {
                var myPic = Image.FromStream(CoverPic.InputStream, true, true);
                var imageInBytes = imageToByteArray(myPic);
                var user = _userManager.GetByEmail(User.Identity.Name);
                user.ConfirmPassword = user.Password;
                user.CoverPic = imageInBytes;
                _userManager.Update(user);
            }
            
            return RedirectToAction("NewsFeed");
        }

        //image to byte converter
        private byte[] imageToByteArray(Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }

        //byte to image converter
        public ActionResult byteArrayToImage(byte[] byteArrayIn)
        {
            //Image returnImage = null;
            //using (MemoryStream ms = new MemoryStream(byteArrayIn))
            //{
            //    returnImage = Image.FromStream(ms);
            //}
            //return returnImage;

            var ms = new MemoryStream(byteArrayIn);
            return File(ms.ToArray(), "image/png");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}