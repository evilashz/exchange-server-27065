using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000725 RID: 1829
	[Serializable]
	public class LinkedUser : User
	{
		// Token: 0x17001D20 RID: 7456
		// (get) Token: 0x060056BD RID: 22205 RVA: 0x0013805E File Offset: 0x0013625E
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return LinkedUser.schema;
			}
		}

		// Token: 0x060056BE RID: 22206 RVA: 0x00138065 File Offset: 0x00136265
		public LinkedUser()
		{
			base.SetObjectClass("user");
		}

		// Token: 0x060056BF RID: 22207 RVA: 0x00138078 File Offset: 0x00136278
		public LinkedUser(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001D21 RID: 7457
		// (get) Token: 0x060056C0 RID: 22208 RVA: 0x00138081 File Offset: 0x00136281
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001D22 RID: 7458
		// (get) Token: 0x060056C1 RID: 22209 RVA: 0x00138088 File Offset: 0x00136288
		// (set) Token: 0x060056C2 RID: 22210 RVA: 0x00138090 File Offset: 0x00136290
		private new bool RemotePowerShellEnabled
		{
			get
			{
				return base.RemotePowerShellEnabled;
			}
			set
			{
				base.RemotePowerShellEnabled = value;
			}
		}

		// Token: 0x17001D23 RID: 7459
		// (get) Token: 0x060056C3 RID: 22211 RVA: 0x00138099 File Offset: 0x00136299
		// (set) Token: 0x060056C4 RID: 22212 RVA: 0x001380A1 File Offset: 0x001362A1
		private new UpgradeRequestTypes UpgradeRequest
		{
			get
			{
				return base.UpgradeRequest;
			}
			set
			{
				base.UpgradeRequest = value;
			}
		}

		// Token: 0x17001D24 RID: 7460
		// (get) Token: 0x060056C5 RID: 22213 RVA: 0x001380AA File Offset: 0x001362AA
		// (set) Token: 0x060056C6 RID: 22214 RVA: 0x001380B2 File Offset: 0x001362B2
		private new UpgradeStatusTypes UpgradeStatus
		{
			get
			{
				return base.UpgradeStatus;
			}
			set
			{
				base.UpgradeStatus = value;
			}
		}

		// Token: 0x17001D25 RID: 7461
		// (get) Token: 0x060056C7 RID: 22215 RVA: 0x001380BB File Offset: 0x001362BB
		private new string UpgradeDetails
		{
			get
			{
				return base.UpgradeDetails;
			}
		}

		// Token: 0x17001D26 RID: 7462
		// (get) Token: 0x060056C8 RID: 22216 RVA: 0x001380C3 File Offset: 0x001362C3
		private new string UpgradeMessage
		{
			get
			{
				return base.UpgradeMessage;
			}
		}

		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x060056C9 RID: 22217 RVA: 0x001380CB File Offset: 0x001362CB
		private new UpgradeStage? UpgradeStage
		{
			get
			{
				return base.UpgradeStage;
			}
		}

		// Token: 0x17001D28 RID: 7464
		// (get) Token: 0x060056CA RID: 22218 RVA: 0x001380D3 File Offset: 0x001362D3
		private new DateTime? UpgradeStageTimeStamp
		{
			get
			{
				return base.UpgradeStageTimeStamp;
			}
		}

		// Token: 0x17001D29 RID: 7465
		// (get) Token: 0x060056CB RID: 22219 RVA: 0x001380DB File Offset: 0x001362DB
		private new MailboxRelease MailboxRelease
		{
			get
			{
				return base.MailboxRelease;
			}
		}

		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x060056CC RID: 22220 RVA: 0x001380E3 File Offset: 0x001362E3
		private new MailboxRelease ArchiveRelease
		{
			get
			{
				return base.ArchiveRelease;
			}
		}

		// Token: 0x04003AA4 RID: 15012
		private static LinkedUserSchema schema = ObjectSchema.GetInstance<LinkedUserSchema>();
	}
}
