using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Web_MZ.Models;



namespace Web_MZ.Pdf
{
    public class CreatorCvPdf : IDocument
    {
        private readonly ProfilViewModel _model;

        public CreatorCvPdf(ProfilViewModel model)
        {
            _model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(11).FontColor(Colors.Black));

                page.Content().Column(col =>
                {
                    col.Spacing(10);

                    // ================= HEADER =================
                    col.Item().Column(c =>
                    {
                        c.Item().AlignCenter().Text($"{_model.Creator.User.FirstName} {_model.Creator.User.LastName}")
                            .FontSize(22)
                            .Bold();

                        if (!string.IsNullOrEmpty(_model.Creator.Bio))
                        {
                            c.Item().Text(_model.Creator.Bio)
                                .FontSize(11)
                                .Italic()
                                .FontColor(Colors.Grey.Darken2)
                                .AlignCenter();
                        }

                        c.Item().Text($"📍 {_model.Creator.User.Country} | 📞 {_model.Creator.User.Phone} | ✉️ {_model.Creator.User.Email}")
                            .FontSize(10)
                            .AlignCenter();
                    });

                    col.Item().Padding(2, 0).LineHorizontal(1).LineColor(Colors.Black);

                    // ================= EXPERIENCE =================
                    SectionTitle(col, "Experience");
                    foreach (var exp in _model.Experiences)
                    {
                        col.Item().Column(c =>
                        {
                            c.Item().Text($"{exp.Title} - {exp.Company} ({exp.Location})").Bold();
                            c.Item().Text($"{exp.StartDate:MMM yyyy} - {(exp.EndDate.HasValue ? exp.EndDate.Value.ToString("MMM yyyy") : "Present")}")
                                .FontSize(10)
                                .FontColor(Colors.Grey.Darken1);
                            if (!string.IsNullOrEmpty(exp.Description))
                                c.Item().Text(exp.Description).FontSize(10);
                        });
                    }
                    col.Item().Padding(2, 0).LineHorizontal(1).LineColor(Colors.Black);

                    // ================= PROJECTS =================
                    SectionTitle(col, "Projects");
                    foreach (var p in _model.Projects)
                    {
                        col.Item().Column(c =>
                        {
                            c.Item().Text($"{p.Title}").Bold();
                            if (!string.IsNullOrEmpty(p.Description))
                                c.Item().Text(p.Description).FontSize(10);
                            if (!string.IsNullOrEmpty(p.Technologies))
                                c.Item().Text($"Technologies: {p.Technologies}").FontSize(10);
                            c.Item().Text($"{p.StartDate:MMM yyyy} - {(p.EndDate.HasValue ? p.EndDate.Value.ToString("MMM yyyy") : "Present")}")
                                .FontSize(10)
                                .FontColor(Colors.Grey.Darken1);
                        });
                    }
                    col.Item().Padding(2, 0).LineHorizontal(1).LineColor(Colors.Black);

                    // ================= CERTIFICATIONS =================
                    if (_model.Certifications != null && _model.Certifications.Any())
                    {
                        SectionTitle(col, "Certifications");
                        foreach (var cert in _model.Certifications)
                        {
                            col.Item().Text(
                                $"• {cert.Title}" +
                                $"{(string.IsNullOrWhiteSpace(cert.Issuer) ? "" : $" | {cert.Issuer}")}" +
                                $"{(!string.IsNullOrWhiteSpace(cert.CredentialId) ? $" · Credential ID: {cert.CredentialId}" : "")}" +
                                $" ({cert.DateIssued:yyyy})"
                            ).FontSize(10).FontColor(Colors.Black);
                        }
                    }
                    col.Item().Padding(2, 0).LineHorizontal(1).LineColor(Colors.Black);

                    // ================= SKILLS =================
                    SectionTitle(col, "Skills");
                    col.Item().Row(row =>
                    {
                        foreach (var s in _model.Competences)
                        {
                            row.RelativeItem().Padding(2).Border(1).BorderColor(Colors.Grey.Lighten2)
                                .AlignCenter()
                                .Text($"{s.Name} ({s.Level}/5)")
                                .FontSize(10);
                        }
                    });
                    col.Item().Padding(2, 0).LineHorizontal(1).LineColor(Colors.Black);

                    // ================= LANGUAGES =================
                    SectionTitle(col, "Languages");
                    foreach (var l in _model.Langues)
                    {
                        col.Item().Text($"• {l.Nom} - {l.Niveau}");
                    }
                });
            });
        }

        void SectionTitle(ColumnDescriptor col, string title)
        {
            col.Item().PaddingTop(5).PaddingBottom(2).Text(title)
                .FontSize(15)
                .Bold()
                .FontColor(Colors.Black);
        }
    }
}
