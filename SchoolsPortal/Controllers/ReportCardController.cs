
using iTextSharp.text;
using iTextSharp.text.pdf;
using SchoolsPortal.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolsPortal.Controllers
{
    public class ReportCardController : Controller
    {
        // GET: ReportCard
        public ActionResult Index()
        {
            db db = new db();
            ViewBag.reportcard=db.getschoolyears(((user)Session["user"]).getusercred().getuserid());
            return View();
        }
        [HttpPost]
        public ActionResult Report()
        {
            int schoolyearid = Convert.ToInt32(Request["schoolyearid"]);
            db db = new db();
           List<reportcarddisplay> courseid = db.getcourseid(schoolyearid, ((user)Session["user"]).getusercred().getuserid());
            course a = new course();
            for (int x = 0; x < courseid.Count; x++)
            {
               courseid[x].setlettergrade(db.getlettergrade(((user)Session["user"]).getusercred().getuserid(),100*a.finalcalcgrade(Convert.ToInt32(courseid[x].getcourseid()), Convert.ToInt32(Request["schoolyearid"]))));
            }
            createpdf pdf = new createpdf();
            //pdf.createpass();
            ShowPdf(CreatePDF2(courseid));

            //download pdf
            //get course id for grade
            //db db = new db();
            //ViewBag.reportcard = db.getschoolyears(((user)Session["user"]).getusercred().getuserid());
            return View();
        }

        private byte[] CreatePDF2(List<reportcarddisplay> courseid)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 50, 50);

            using (MemoryStream output = new MemoryStream())
            {
                PdfWriter wri = PdfWriter.GetInstance(doc, output);
                doc.Open();

                Paragraph header = new Paragraph("Report Card") { Alignment = Element.ALIGN_CENTER };

                PdfPTable table = new PdfPTable(5);

                table.AddCell("Course Name");
                table.AddCell("Department");
                table.AddCell("Course Code");
                table.AddCell("Teacher");
                table.AddCell("Grade");
                for(int x = 0; x < courseid.Count; x++)
                {
                    table.AddCell(courseid[x].getcoursename());
                    table.AddCell(courseid[x].getdepartment());
                    table.AddCell(courseid[x] .getcoursenumber());
                    table.AddCell(courseid[x].getname().getfirstname() +" "+ courseid[x].getname().getlastname());
                    table.AddCell(courseid[x].getlettergrade());
                }
                doc.Add(header);
                doc.Add(table);

                doc.Close();
                return output.ToArray();
            }

        }

        private void ShowPdf(byte[] strS)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now+".pdf");

            Response.BinaryWrite(strS);
            Response.End();
            Response.Flush();
            Response.Clear();
        }
    }
}