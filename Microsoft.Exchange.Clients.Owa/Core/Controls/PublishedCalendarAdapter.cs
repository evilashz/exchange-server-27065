using System;
using System.Net;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020003F9 RID: 1017
	internal class PublishedCalendarAdapter : CalendarAdapterBase
	{
		// Token: 0x06002531 RID: 9521 RVA: 0x000D757C File Offset: 0x000D577C
		public PublishedCalendarAdapter(AnonymousSessionContext sessionContext)
		{
			if (sessionContext == null)
			{
				throw new ArgumentNullException("sessionContext");
			}
			this.SessionContext = sessionContext;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000D7599 File Offset: 0x000D5799
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.publishedFolder != null)
			{
				this.publishedFolder.Dispose();
				this.publishedFolder = null;
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000D75B8 File Offset: 0x000D57B8
		public void LoadData(PropertyDefinition[] queryProperties, ExDateTime[] days, CalendarViewType viewType)
		{
			this.LoadData(queryProperties, days, viewType, null);
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000D75C4 File Offset: 0x000D57C4
		public void LoadData(PropertyDefinition[] queryProperties, ExDateTime[] days, CalendarViewType viewType, ExTimeZone preferredTimeZone)
		{
			this.LoadData(queryProperties, days, 0, 24, viewType, preferredTimeZone);
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000D75D4 File Offset: 0x000D57D4
		public void LoadData(PropertyDefinition[] queryProperties, ExDateTime[] days, int startHour, int endHour, CalendarViewType viewType)
		{
			this.LoadData(queryProperties, days, startHour, endHour, viewType, null);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000D75E4 File Offset: 0x000D57E4
		public void LoadData(PropertyDefinition[] queryProperties, ExDateTime[] days, int startHour, int endHour, CalendarViewType viewType, ExTimeZone preferredTimeZone)
		{
			if (queryProperties == null || queryProperties.Length == 0)
			{
				throw new ArgumentNullException("queryProperties");
			}
			days = CalendarUtilities.GetViewDaysForPublishedView(this.SessionContext, days, viewType);
			try
			{
				this.publishedFolder = (PublishedCalendar)PublishedFolder.Create(this.SessionContext.PublishingUrl);
			}
			catch (PublishedFolderAccessDeniedException innerException)
			{
				throw new OwaInvalidRequestException("Cannot open published folder", innerException);
			}
			catch (NotSupportedException innerException2)
			{
				throw new OwaInvalidRequestException("Cannot open published folder", innerException2);
			}
			if (preferredTimeZone != null)
			{
				this.SessionContext.TimeZone = preferredTimeZone;
				this.publishedFolder.TimeZone = preferredTimeZone;
				CalendarUtilities.AdjustTimesWithTimeZone(days, preferredTimeZone);
			}
			else if (this.SessionContext.IsTimeZoneFromCookie)
			{
				this.publishedFolder.TimeZone = this.SessionContext.TimeZone;
			}
			else
			{
				this.SessionContext.TimeZone = this.publishedFolder.TimeZone;
				CalendarUtilities.AdjustTimesWithTimeZone(days, this.SessionContext.TimeZone);
			}
			base.DateRanges = CalendarAdapterBase.ConvertDateTimeArrayToDateRangeArray(days, startHour, endHour);
			try
			{
				base.DataSource = new PublishedCalendarDataSource(this.SessionContext, this.publishedFolder, base.DateRanges, queryProperties);
			}
			catch (FolderNotPublishedException)
			{
				Utilities.EndResponse(OwaContext.Current.HttpContext, HttpStatusCode.NotFound);
			}
			base.CalendarTitle = string.Format("{0} ({1})", this.publishedFolder.DisplayName, this.publishedFolder.OwnerDisplayName);
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x000D7754 File Offset: 0x000D5954
		// (set) Token: 0x06002538 RID: 9528 RVA: 0x000D775C File Offset: 0x000D595C
		public AnonymousSessionContext SessionContext { get; private set; }

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x000D7765 File Offset: 0x000D5965
		public override string CalendarOwnerDisplayName
		{
			get
			{
				if (this.publishedFolder == null)
				{
					throw new InvalidOperationException("Need to call PublishedCalendarAdapter.LoadData first.");
				}
				return this.publishedFolder.OwnerDisplayName;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x000D7785 File Offset: 0x000D5985
		public override string IdentityString
		{
			get
			{
				return "PublishedCalendar";
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000D778C File Offset: 0x000D598C
		public string ICalUrl
		{
			get
			{
				if (this.publishedFolder == null)
				{
					throw new InvalidOperationException("Need to call PublishedCalendarAdapter.LoadData first.");
				}
				return this.publishedFolder.ICalUrl.ToString();
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x000D77B1 File Offset: 0x000D59B1
		public ExDateTime PublishedFromDateTime
		{
			get
			{
				return this.publishedFolder.PublishedFromDateTime;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x000D77BE File Offset: 0x000D59BE
		public ExDateTime PublishedToDateTime
		{
			get
			{
				return this.publishedFolder.PublishedToDateTime;
			}
		}

		// Token: 0x040019AF RID: 6575
		private PublishedCalendar publishedFolder;
	}
}
