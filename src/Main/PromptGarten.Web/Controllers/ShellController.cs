using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PromptGarten.Web.Models;

namespace PromptGarten.Web.Controllers
{
    public class ShellController : Controller
    {
        //
        // GET: /Shell/

        public ActionResult Index()
        {
            return View(new ShellModel{Command = "", Output=""});
        }

        [HttpPost]    
        public ActionResult Exec(ShellModel m)
        {
            var proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.Arguments = "/c " + m.Command;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.EnableRaisingEvents = false;

            proc.Start();
            m.Output = proc.StandardOutput.ReadToEnd();
            proc.Close();
            return View(m);
        }

    }
}
