using System;
using System.Web.Security;

namespace Final11373.Account
{
    public partial class AddRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // إنشاء الأدوار إذا لم تكن موجودة
                if (!Roles.RoleExists("Admin"))
                {
                    Roles.CreateRole("Admin");
                }
                if (!Roles.RoleExists("Patient"))
                {
                    Roles.CreateRole("Patient");
                }

                // إضافة المستخدم إلى دور "Admin" (نفذها لمرة واحدة فقط)
                string adminEmail = "admin@aftercare.com"; // غيره إذا لزم
                if (!Roles.IsUserInRole(adminEmail, "Admin"))
                {
                    Roles.AddUserToRole(adminEmail, "Admin");
                }
            }
        }
    }
}
