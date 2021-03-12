using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000BF RID: 191
	[DataContract]
	public class SetMailboxCalendarFolder : SetObjectProperties
	{
		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0005915A File Offset: 0x0005735A
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-MailboxCalendarFolder";
			}
		}

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00059161 File Offset: 0x00057361
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x00059168 File Offset: 0x00057368
		// (set) Token: 0x06001CC8 RID: 7368 RVA: 0x00059184 File Offset: 0x00057384
		[DataMember]
		public bool PublishEnabled
		{
			get
			{
				return (bool)(base["PublishEnabled"] ?? false);
			}
			set
			{
				base["PublishEnabled"] = value;
			}
		}

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x00059197 File Offset: 0x00057397
		// (set) Token: 0x06001CCA RID: 7370 RVA: 0x000591A9 File Offset: 0x000573A9
		[DataMember]
		public string DetailLevel
		{
			get
			{
				return (string)base["DetailLevel"];
			}
			set
			{
				base["DetailLevel"] = value;
			}
		}

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x000591B7 File Offset: 0x000573B7
		// (set) Token: 0x06001CCC RID: 7372 RVA: 0x000591C9 File Offset: 0x000573C9
		[DataMember]
		public string PublishDateRangeFrom
		{
			get
			{
				return (string)base["PublishDateRangeFrom"];
			}
			set
			{
				base["PublishDateRangeFrom"] = value;
			}
		}

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x000591D7 File Offset: 0x000573D7
		// (set) Token: 0x06001CCE RID: 7374 RVA: 0x000591E9 File Offset: 0x000573E9
		[DataMember]
		public string PublishDateRangeTo
		{
			get
			{
				return (string)base["PublishDateRangeTo"];
			}
			set
			{
				base["PublishDateRangeTo"] = value;
			}
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x000591F7 File Offset: 0x000573F7
		// (set) Token: 0x06001CD0 RID: 7376 RVA: 0x00059213 File Offset: 0x00057413
		[DataMember]
		public bool SearchableUrlEnabled
		{
			get
			{
				return (bool)(base["SearchableUrlEnabled"] ?? false);
			}
			set
			{
				base["SearchableUrlEnabled"] = value;
			}
		}
	}
}
