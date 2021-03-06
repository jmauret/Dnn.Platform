﻿#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2014
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using DotNetNuke.Common.Lists;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Definitions;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Services.Installer.Installers;
using DotNetNuke.Services.Upgrade;

namespace Dnn.Modules.Dashboard.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessController : IUpgradeable
    {
        private const string DashboardInstallerName = "DashboardControl";
        private const string DashboardInstallerType = "Dnn.Modules.Dashboard.Components.Installers.DashboardInstaller, Dnn.Modules.Dashboard";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public string UpgradeModule(string version)
        {
            try
            {
                switch (version)
                {
                    case "08.00.00":
                        AddDashboardControlInstaller();

                        ModuleDefinitionInfo moduleDefinition = ModuleDefinitionController.GetModuleDefinitionByFriendlyName("Dashboard");
                        if (moduleDefinition != null)
                        {
                            //Create New Host Page (or get existing one)
                            TabInfo dashboardPage = Upgrade.AddHostPage("Dashboard",
                                                        "Summary view of application and site settings.",
                                                        "~/images/icon_dashboard_16px.gif",
                                                        "~/images/icon_dashboard_32px.gif",
                                                        true);

                            //Add Module To Page
                            Upgrade.AddModuleToPage(dashboardPage,
                                                        moduleDefinition.ModuleDefID,
                                                        "Dashboard",
                                                        "~/images/icon_dashboard_32px.gif",
                                                        true);
                        }

                        break;
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        private void AddDashboardControlInstaller()
        {
            var listController = new ListController();
            ListEntryInfo entry = listController.GetListEntryInfo("Installer", DashboardInstallerName);

            if (entry == null)
            {
                listController.AddListEntry(new ListEntryInfo()
                {
                    ListName = "Installer",
                    Value = DashboardInstallerName,
                    Text = DashboardInstallerType
                });
            }
        }
    }
}
