﻿// Copyright (c) Source Tree Solutions, LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Author:					Joe Audette
// Created:					2016-08-07
// Last Modified:			2016-08-07
// 

using cloudscribe.Core.SimpleContent.Integration.ViewModels;
using cloudscribe.SimpleContent.Models;
using cloudscribe.Web.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace cloudscribe.Core.SimpleContent.Integration.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class ContentSettingsController : Controller
    {
        public ContentSettingsController(
            IProjectService projectService,
            IStringLocalizer<SimpleContentIntegration> localizer
            )
        {
            this.projectService = projectService;
            sr = localizer;
        }

        private IProjectService projectService;
        private IStringLocalizer sr;

        // GET: /ContentSettings
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = sr["Content Settings"];

            var projectSettings = await projectService.GetCurrentProjectSettings();

            var model = new ContentSettingsViewModel();
            model.ChannelCategoriesCsv = projectSettings.ChannelCategoriesCsv;
            //model.ChannelRating = projectSettings.ChannelRating;
            //model.ChannelTimeToLive = projectSettings.ChannelTimeToLive;
            model.CommentNotificationEmail = projectSettings.CommentNotificationEmail;
            model.DaysToComment = projectSettings.DaysToComment;
            model.Description = projectSettings.Description;
            model.IncludePubDateInPostUrls = projectSettings.IncludePubDateInPostUrls;
            model.LanguageCode = projectSettings.LanguageCode;
            model.ManagingEditorEmail = projectSettings.ManagingEditorEmail;
            model.ModerateComments = projectSettings.ModerateComments;
            model.PostsPerPage = projectSettings.PostsPerPage;
            model.PubDateFormat = projectSettings.PubDateFormat;
            //model.RemoteFeedProcessorUseAgentFragment = projectSettings.RemoteFeedProcessorUseAgentFragment;
            model.RemoteFeedUrl = projectSettings.RemoteFeedUrl;
            model.ShowTitle = projectSettings.ShowTitle;
            model.Title = projectSettings.Title;
            model.UseMetaDescriptionInFeed = projectSettings.UseMetaDescriptionInFeed;
            model.WebmasterEmail = projectSettings.WebmasterEmail;



            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContentSettingsViewModel model)
        {
            ViewData["Title"] = sr["Content Settings"];

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var projectSettings = await projectService.GetCurrentProjectSettings();

            projectSettings.ChannelCategoriesCsv = model.ChannelCategoriesCsv;
            //projectSettings.ChannelRating = model.ChannelRating;
            //projectSettings.ChannelTimeToLive = model.ChannelTimeToLive;
            projectSettings.CommentNotificationEmail = model.CommentNotificationEmail;
            projectSettings.DaysToComment = model.DaysToComment;
            projectSettings.Description = model.Description;
            projectSettings.IncludePubDateInPostUrls = model.IncludePubDateInPostUrls;
            projectSettings.LanguageCode = model.LanguageCode;
            projectSettings.ManagingEditorEmail = model.ManagingEditorEmail;
            projectSettings.ModerateComments = model.ModerateComments;
            projectSettings.PostsPerPage = model.PostsPerPage;
            projectSettings.PubDateFormat = model.PubDateFormat;
            //projectSettings.RemoteFeedProcessorUseAgentFragment = model.RemoteFeedProcessorUseAgentFragment;
            projectSettings.RemoteFeedUrl = model.RemoteFeedUrl;
            projectSettings.ShowTitle = model.ShowTitle;
            projectSettings.Title = model.Title;
            projectSettings.UseMetaDescriptionInFeed = model.UseMetaDescriptionInFeed;
            projectSettings.WebmasterEmail = model.WebmasterEmail;

            await projectService.Update(projectSettings);

            this.AlertSuccess(sr["Content Settings were successfully updated."], true);

            return RedirectToAction("Index");

        }

    }
}