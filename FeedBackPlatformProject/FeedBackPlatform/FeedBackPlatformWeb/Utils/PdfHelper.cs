using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedBackPlatformDatabase.Models;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using FeedBackPlatformDatabase.DAL;

namespace FeedBackPlatformWeb.Utils
{
    public class PdfHelper
    {
        public static byte[] GeneratePdf(int surveyId, int userContextId, HttpServerUtilityBase server)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document(PageSize.A4))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    document.Open();
                    PdfContentByte canvas = writer.DirectContent;

                    using (var uow = new UnitOfWork())
                    {
                        var survey = uow.SurveyRepository.GetByIdAndUser(surveyId, userContextId);
                        var questions = uow.QuestionRepository.GetAllBySurvey(survey.Id);

                        //set pdf title
                        Paragraph title = new Paragraph();
                        title.Alignment = Element.ALIGN_CENTER;
                        title.Font = new Font(Font.FontFamily.HELVETICA, 18);
                        title.Add(survey.Name);
                        document.Add(title);
                        document.Add(Chunk.NEWLINE);
                        document.Add(Chunk.NEWLINE);


                        for (int i = 0; i < questions.Count; i++)
                        {
                            var q = questions[i];

                            Paragraph question = new Paragraph();
                            question.Alignment = Element.ALIGN_LEFT;
                            question.Font = new Font(Font.FontFamily.HELVETICA, 14);
                            question.Add(string.Format("{0}. {1}", (i + 1).ToString(), q.Description));
                            document.Add(question);


                            var questionAnswers = uow.QuestionAnswerRepository.GetAllByQuestionId(q.Id);

                            if (q.QuestionType == "Single Choice")
                            {
                                if (questionAnswers.Any())
                                {
                                    PdfPTable qaTbl = new PdfPTable(2);
                                    qaTbl.SetTotalWidth(new float[] { 30, PageSize.A4.Width - 50f });
                                    var imagePath = "";

                                    for (int j = 0; j < questionAnswers.Count; j++)
                                    {
                                        var qa = questionAnswers[j];

                                        //set dummy check to simulate responses
                                        if (j == 2)
                                        {
                                            imagePath = server.MapPath(@"~/Content/img-pdf/unchecked-radio-50.png");
                                        }
                                        else
                                        {
                                            imagePath = server.MapPath(@"~/Content/img-pdf/unchecked-radio-50.png");
                                        }
                                        //end dummy check

                                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                                        image.ScalePercent(25);
                                        PdfPCell imageCell = new PdfPCell(image);
                                        imageCell.Border = 0;
                                        imageCell.Padding = 5;
                                        imageCell.HorizontalAlignment = Element.ALIGN_LEFT;

                                        PdfPCell textCell = new PdfPCell(new Paragraph(qa.Content, new Font(Font.FontFamily.HELVETICA, 12)));
                                        textCell.Border = 0;
                                        textCell.Padding = 5;

                                        qaTbl.AddCell(imageCell);
                                        qaTbl.AddCell(textCell);
                                    }

                                    document.Add(qaTbl);
                                    document.Add(Chunk.NEWLINE);
                                    document.Add(Chunk.NEWLINE);
                                }
                            }
                            else if (q.QuestionType == "Checkboxes")
                            {
                                if (questionAnswers.Any())
                                {
                                    PdfPTable qaTbl = new PdfPTable(2);
                                    qaTbl.SetTotalWidth(new float[] { 30, PageSize.A4.Width - 50f });
                                    var imagePath = "";

                                    for (int j = 0; j < questionAnswers.Count; j++)
                                    {
                                        var qa = questionAnswers[j];

                                        //set dummy check to simulate responses
                                        if (j == 2)
                                        {
                                            imagePath = server.MapPath(@"~/Content/img-pdf/unchecked-checkbox-50.png");
                                        }
                                        else
                                        {
                                            imagePath = server.MapPath(@"~/Content/img-pdf/unchecked-checkbox-50.png");
                                        }
                                        //end dummy check

                                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                                        image.ScalePercent(25);
                                        PdfPCell imageCell = new PdfPCell(image);
                                        imageCell.Border = 0;
                                        imageCell.Padding = 5;
                                        imageCell.HorizontalAlignment = Element.ALIGN_LEFT;

                                        PdfPCell textCell = new PdfPCell(new Paragraph(qa.Content, new Font(Font.FontFamily.HELVETICA, 12)));
                                        textCell.Border = 0;
                                        textCell.Padding = 5;

                                        qaTbl.AddCell(imageCell);
                                        qaTbl.AddCell(textCell);
                                    }

                                    document.Add(qaTbl);
                                    document.Add(Chunk.NEWLINE);
                                    document.Add(Chunk.NEWLINE);
                                }
                            }
                            else if (q.QuestionType == "Comment box")
                            {
                                //set dummy check to simulate responses
                                PdfPTable qaTbl = new PdfPTable(1);
                                qaTbl.SetTotalWidth(new float[] { PageSize.A4.Width - 20f });

                                PdfPCell textCell = new PdfPCell(new Paragraph("Lorem ipsum dolor sit amet, ea eum saepe oportere consectetuer, vix cu veritus moderatius intellegam.", new Font(Font.FontFamily.HELVETICA, 12)));
                                textCell.Border = 0;
                                textCell.Padding = 5;
                                qaTbl.AddCell(textCell);

                                document.Add(qaTbl);
                                document.Add(Chunk.NEWLINE);
                                document.Add(Chunk.NEWLINE);
                                //end dummy check
                            }
                        }
                    }
                }

                return ms.ToArray();
            }
        }
    }
}