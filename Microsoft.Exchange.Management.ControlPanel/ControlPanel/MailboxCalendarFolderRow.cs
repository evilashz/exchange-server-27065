using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000BE RID: 190
	[DataContract]
	public class MailboxCalendarFolderRow : BaseRow
	{
		// Token: 0x06001CB4 RID: 7348 RVA: 0x00059049 File Offset: 0x00057249
		public MailboxCalendarFolderRow(MailboxCalendarFolder calendarFolder) : base(new Identity(calendarFolder.Identity.ToString(), calendarFolder.Identity.ToString()), calendarFolder)
		{
			this.MailboxCalendarFolder = calendarFolder;
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x00059074 File Offset: 0x00057274
		// (set) Token: 0x06001CB6 RID: 7350 RVA: 0x0005907C File Offset: 0x0005727C
		public MailboxCalendarFolder MailboxCalendarFolder { get; private set; }

		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x00059085 File Offset: 0x00057285
		// (set) Token: 0x06001CB8 RID: 7352 RVA: 0x00059092 File Offset: 0x00057292
		[DataMember]
		public bool PublishEnabled
		{
			get
			{
				return this.MailboxCalendarFolder.PublishEnabled;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x00059099 File Offset: 0x00057299
		// (set) Token: 0x06001CBA RID: 7354 RVA: 0x000590B0 File Offset: 0x000572B0
		[DataMember]
		public string DetailLevel
		{
			get
			{
				return this.MailboxCalendarFolder.DetailLevel.ToString();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x000590B7 File Offset: 0x000572B7
		// (set) Token: 0x06001CBC RID: 7356 RVA: 0x000590CE File Offset: 0x000572CE
		[DataMember]
		public string PublishDateRangeFrom
		{
			get
			{
				return this.MailboxCalendarFolder.PublishDateRangeFrom.ToString();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x000590D5 File Offset: 0x000572D5
		// (set) Token: 0x06001CBE RID: 7358 RVA: 0x000590EC File Offset: 0x000572EC
		[DataMember]
		public string PublishDateRangeTo
		{
			get
			{
				return this.MailboxCalendarFolder.PublishDateRangeTo.ToString();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x000590F4 File Offset: 0x000572F4
		// (set) Token: 0x06001CC0 RID: 7360 RVA: 0x00059119 File Offset: 0x00057319
		[DataMember]
		public string SearchableUrlEnabled
		{
			get
			{
				return this.MailboxCalendarFolder.SearchableUrlEnabled.ToString().ToLowerInvariant();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x00059120 File Offset: 0x00057320
		// (set) Token: 0x06001CC2 RID: 7362 RVA: 0x00059136 File Offset: 0x00057336
		[DataMember]
		public string PublishedICalUrl
		{
			get
			{
				return this.MailboxCalendarFolder.PublishedICalUrl ?? string.Empty;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x0005913D File Offset: 0x0005733D
		// (set) Token: 0x06001CC4 RID: 7364 RVA: 0x00059153 File Offset: 0x00057353
		[DataMember]
		public string PublishedCalendarUrl
		{
			get
			{
				return this.MailboxCalendarFolder.PublishedCalendarUrl ?? string.Empty;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}
	}
}
