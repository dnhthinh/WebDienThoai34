using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models.Entities;

namespace WebsiteBanDienThoai.Areas.Admin.Controllers
{
    public class ThongKe_AdminController : Controller
    {
        // GET: Admin/ThongKe_Admin
        public ActionResult Index()
        {
            
                // Tạo danh sách để lưu trữ kết quả từ Stored Procedure
                List<AspNetUser> users = new List<AspNetUser>();

                // Chuỗi kết nối đến cơ sở dữ liệu
                string connectionString = "Server=THINHPC\\WEBDIENTHOAI;Database=WebDienThoai;Trusted_Connection=True";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi stored procedure
                    using (var command = new SqlCommand("GetAllAspNetUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Thực hiện truy vấn
                        using (var reader = command.ExecuteReader())
                        {
                            // Đọc dữ liệu từ reader và thêm vào danh sách users
                            while (reader.Read())
                            {
                                var user = new AspNetUser
                                {
                                    Id = reader.GetString(0),
                                    UserName = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    // Thêm các thuộc tính khác tùy theo bảng AspNetUsers
                                };
                                users.Add(user);
                            }
                        }
                    }
                }

                // Trả về danh sách users trong View
                return View(users);
            }
           
        }

        
    }

