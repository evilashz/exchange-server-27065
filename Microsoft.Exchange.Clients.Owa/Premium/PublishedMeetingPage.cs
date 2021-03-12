using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000466 RID: 1126
	public class PublishedMeetingPage : OwaSubPage, IRegistryOnlyForm
	{
		// Token: 0x06002A46 RID: 10822 RVA: 0x000ECB3C File Offset: 0x000EAD3C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string parameter = base.GetParameter("id", true);
			try
			{
				StoreObjectId itemId = Utilities.CreateStoreObjectId(parameter);
				PublishingUrl publishingUrl = ((AnonymousSessionContext)base.SessionContext).PublishingUrl;
				using (PublishedCalendar publishedCalendar = (PublishedCalendar)PublishedFolder.Create(publishingUrl))
				{
					this.detailLevel = publishedCalendar.DetailLevel;
					if (this.detailLevel == DetailLevelEnumType.AvailabilityOnly)
					{
						Utilities.EndResponse(OwaContext.Current.HttpContext, HttpStatusCode.Forbidden);
					}
					this.item = publishedCalendar.GetItemData(itemId);
				}
			}
			catch (FolderNotPublishedException)
			{
				Utilities.EndResponse(OwaContext.Current.HttpContext, HttpStatusCode.NotFound);
			}
			catch (OwaInvalidIdFormatException innerException)
			{
				throw new OwaInvalidRequestException("Invalid id param", innerException);
			}
			catch (PublishedFolderAccessDeniedException innerException2)
			{
				throw new OwaInvalidRequestException("Cannot access this published folder", innerException2);
			}
			catch (ObjectNotFoundException innerException3)
			{
				throw new OwaInvalidRequestException("Cannot open this item", innerException3);
			}
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000ECC4C File Offset: 0x000EAE4C
		protected void RenderSubject()
		{
			base.SanitizingResponse.Write(this.item.Subject);
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000ECC64 File Offset: 0x000EAE64
		protected void RenderLocation()
		{
			base.SanitizingResponse.Write(this.item.Location);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000ECC7C File Offset: 0x000EAE7C
		protected void RenderWhen()
		{
			base.SanitizingResponse.Write(this.item.When);
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000ECC94 File Offset: 0x000EAE94
		protected void RenderBody()
		{
			if (this.DetailLevel == DetailLevelEnumType.FullDetails)
			{
				base.SanitizingResponse.Write(this.item.BodyText);
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x000ECCB5 File Offset: 0x000EAEB5
		protected DetailLevelEnumType DetailLevel
		{
			get
			{
				return this.detailLevel;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x000ECCBD File Offset: 0x000EAEBD
		public override IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				return this.externalScriptFiles;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x000ECCC5 File Offset: 0x000EAEC5
		public override SanitizedHtmlString Title
		{
			get
			{
				return new SanitizedHtmlString(this.item.Subject);
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x000ECCD7 File Offset: 0x000EAED7
		public override string PageType
		{
			get
			{
				return "ReadPublishedMeetingPage";
			}
		}

		// Token: 0x04001C8F RID: 7311
		private PublishedCalendarItemData item;

		// Token: 0x04001C90 RID: 7312
		private DetailLevelEnumType detailLevel;

		// Token: 0x04001C91 RID: 7313
		private string[] externalScriptFiles = new string[]
		{
			"freadpublishedmeeting.js"
		};
	}
}
