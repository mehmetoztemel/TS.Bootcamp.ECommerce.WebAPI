using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MO.Result;

namespace TS.Bootcamp.ECommerce.WebAPI.Filters
{
    public class CustomLogAttribute : Attribute, IActionFilter
    {
        private string Folder_Name = "Logs";
        private string FileName { get; set; }
        public CustomLogAttribute()
        {
            FileName = $@"{DateTime.Now.ToString("yyyy-MM-dd")}-Log.txt";

        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // This method runs before the action method
            RequestLog(context);

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This method runs after the action method
            ResponseLog(context);
        }


        public void RequestLog(ActionExecutingContext context)
        {
            try
            {
                if (!Directory.Exists(Path.Combine(Folder_Name)))
                {
                    Directory.CreateDirectory(Path.Combine(Folder_Name));
                }
                //File.AppentText StreamWriter ile aynı işi yapıyor
                using (StreamWriter writer = File.AppendText(Path.Combine(Folder_Name, FileName)))
                {
                    string bodyString = "";
                    foreach (var item in context.ActionArguments)
                    {
                        bodyString = item.Value!.ToString() ?? "";
                    }
                    writer.Write("Request ");
                    writer.Write("\rDate Time: ");
                    writer.WriteLine($"{DateTime.Now.ToLongTimeString()}, {DateTime.Now.ToLongDateString()}");
                    writer.WriteLine($"IP Address: {context.HttpContext.Connection.RemoteIpAddress!.ToString()}");
                    writer.WriteLine($"TraceId: {context.HttpContext.TraceIdentifier}");
                    writer.WriteLine($"Path: {context.HttpContext.Request.Path}");
                    writer.WriteLine($"Method: {context.HttpContext.Request.Method}");
                    writer.WriteLine($"QueryString: {context.HttpContext.Request.QueryString}");
                    writer.WriteLine($"Body: {bodyString}");
                    writer.WriteLine("---------------------------------------------------------------------------");
                }
            }
            catch (Exception e)
            {
                throw new Exception(Result<string>.Fail(e.Message).ToString());
            }
        }

        public void ResponseLog(ActionExecutedContext context)
        {
            try
            {
                if (!Directory.Exists(Path.Combine(Folder_Name)))
                {
                    Directory.CreateDirectory(Path.Combine(Folder_Name));
                }

                string bodyString = "";
                if (context.Result is ObjectResult objectResult)
                {
                    bodyString = objectResult.Value!.ToString() ?? "";
                }

                //File.AppentText StreamWriter ile aynı işi yapıyor
                using (StreamWriter writer = File.AppendText(Path.Combine(Folder_Name, FileName)))
                {
                    writer.Write("Response ");
                    writer.Write("\rDate Time: ");
                    writer.WriteLine($"{DateTime.Now.ToLongTimeString()}, {DateTime.Now.ToLongDateString()}");
                    writer.WriteLine($"IP Address: {context.HttpContext.Connection.RemoteIpAddress!.ToString()}");
                    writer.WriteLine($"TraceId: {context.HttpContext.TraceIdentifier}");
                    writer.WriteLine($"Path: {context.HttpContext.Request.Path}");
                    writer.WriteLine($"Method: {context.HttpContext.Request.Method}");
                    writer.WriteLine($"StatusCode: {context.HttpContext.Response.StatusCode}");
                    //writer.WriteLine($"Body: {bodyString}");
                    writer.WriteLine("---------------------------------------------------------------------------");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
