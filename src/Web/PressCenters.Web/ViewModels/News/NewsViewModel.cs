﻿namespace PressCenters.Web.ViewModels.News
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Text.RegularExpressions;

    using PressCenters.Common;
    using PressCenters.Data.Models;
    using PressCenters.Services;
    using PressCenters.Services.Mapping;

    public class NewsViewModel : IMapFrom<News>
    {
        private readonly ISlugGenerator slugGenerator;

        public NewsViewModel()
        {
            this.slugGenerator = new SlugGenerator();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent
        {
            get
            {
                // TODO: Extract as a service
                var strippedContent = WebUtility.HtmlDecode(this.Content?.StripHtml() ?? string.Empty);
                strippedContent = strippedContent.Replace("\n", " ");
                strippedContent = strippedContent.Replace("\t", " ");
                strippedContent = Regex.Replace(strippedContent, @"\s+", " ").Trim();
                var shortContent = strippedContent.Substring(0, Math.Min(235, strippedContent.Length)) + "...";
                return shortContent;
            }
        }

        public string ImageUrl { get; set; }

        public string OriginalUrl { get; set; }

        public string SourceName { get; set; }

        public string SourceShortName { get; set; }

        public string SourceUrl { get; set; }

        public string ShorterOriginalUrl
        {
            get
            {
                if (this.OriginalUrl.Length <= 65)
                {
                    return this.OriginalUrl;
                }

                return $"{this.OriginalUrl.Substring(0, 30)}..{this.OriginalUrl.Substring(this.OriginalUrl.Length - 30, 30)}";
            }
        }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString =>
            this.CreatedOn.Hour == 0 && this.CreatedOn.Minute == 0
                ? this.CreatedOn.ToString("ddd, dd MMM yyyy", new CultureInfo("bg-BG"))
                : this.CreatedOn.ToString("ddd, dd MMM yyyy HH:mm", new CultureInfo("bg-BG"));

        public string Url => $"/News/{this.Id}/{this.slugGenerator.GenerateSlug(this.Title)}";
    }
}