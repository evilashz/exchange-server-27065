using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000467 RID: 1127
	public class PublishedStartPage : NavigationHost, IRegistryOnlyForm
	{
		// Token: 0x06002A50 RID: 10832 RVA: 0x000ECD0C File Offset: 0x000EAF0C
		protected override void OnInit(EventArgs e)
		{
			this.navigationModule = this.SelectNavagationModule();
			this.lastModuleApplicationElement = "PublishedFolder";
			this.lastModuleName = this.navigationModule.ToString();
			this.lastModuleContentClass = "IPF.Appointment";
			this.lastModuleContainerId = "PublishedFolder";
			base.InitializeView(this.viewPlaceHolder);
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x000ECD68 File Offset: 0x000EAF68
		protected override IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				return this.externalScriptFiles;
			}
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000ECD70 File Offset: 0x000EAF70
		protected void RenderSecondaryNavigation()
		{
			Infobar infobar = new Infobar("divErrDP", "infobar");
			ExDateTime date = DateTimeUtilities.GetLocalTime().Date;
			DatePicker.Features features = DatePicker.Features.MultiDaySelection | DatePicker.Features.WeekSelector;
			DatePicker datePicker = new DatePicker("dp", date, (int)features);
			MonthPicker monthPicker = new MonthPicker(base.SessionContext, "divMp");
			if (base.SessionContext.ShowWeekNumbers)
			{
				features |= DatePicker.Features.WeekNumbers;
			}
			base.SanitizingResponse.Write("<div id=\"divCalPicker\">");
			infobar.Render(base.SanitizingResponse);
			datePicker.Render(base.SanitizingResponse);
			monthPicker.Render(base.SanitizingResponse);
			base.SanitizingResponse.Write("</div>");
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000ECE14 File Offset: 0x000EB014
		protected void RenderBreadcrumbs()
		{
			Breadcrumbs breadcrumbs = new Breadcrumbs(base.SessionContext, base.NavigationModule);
			breadcrumbs.Render(base.Response.Output);
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000ECE44 File Offset: 0x000EB044
		protected void RenderPublishRange()
		{
			IPublishedView publishedView = this.GetPublishedView();
			if (publishedView != null)
			{
				base.SanitizingResponse.Write(publishedView.PublishTimeRange);
			}
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000ECE6C File Offset: 0x000EB06C
		protected string GetEscapedPath()
		{
			return ((AnonymousSessionContext)base.SessionContext).EscapedPath;
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000ECE7E File Offset: 0x000EB07E
		protected override NavigationModule SelectNavagationModule()
		{
			return NavigationModule.Calendar;
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000ECE84 File Offset: 0x000EB084
		private IPublishedView GetPublishedView()
		{
			IPublishedView publishedView = null;
			foreach (OwaSubPage owaSubPage in base.ChildSubPages)
			{
				publishedView = (owaSubPage as IPublishedView);
				if (publishedView != null)
				{
					break;
				}
			}
			return publishedView;
		}

		// Token: 0x04001C92 RID: 7314
		private const string PublishedFolder = "PublishedFolder";

		// Token: 0x04001C93 RID: 7315
		private string[] externalScriptFiles = new string[]
		{
			"publishedstartpage.js"
		};
	}
}
