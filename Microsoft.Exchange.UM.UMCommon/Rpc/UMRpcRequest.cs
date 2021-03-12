using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public abstract class UMRpcRequest : UMVersionedRpcRequest
	{
		// Token: 0x060003DB RID: 987 RVA: 0x0000DF11 File Offset: 0x0000C111
		public UMRpcRequest()
		{
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000DF1C File Offset: 0x0000C11C
		internal UMRpcRequest(ADUser user) : this()
		{
			this.user = (ADUser)user.Clone();
			this.userId = user.Id;
			this.policyId = user.UMMailboxPolicy;
			this.dialPlanId = user.UMRecipientDialPlanId;
			this.addressList = user.EmailAddresses.ToStringArray();
			this.domainController = user.OriginatingServer;
			using (UMRecipient umrecipient = UMRecipient.Factory.FromADRecipient<UMRecipient>(this.user))
			{
				this.mailboxSiteId = umrecipient.MailboxServerSite;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		public ADObjectId UserId
		{
			get
			{
				return this.userId;
			}
			set
			{
				this.userId = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000DFC9 File Offset: 0x0000C1C9
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000DFD1 File Offset: 0x0000C1D1
		public ADObjectId PolicyId
		{
			get
			{
				return this.policyId;
			}
			set
			{
				this.policyId = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000DFDA File Offset: 0x0000C1DA
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000DFE2 File Offset: 0x0000C1E2
		public ADObjectId DialPlanId
		{
			get
			{
				return this.dialPlanId;
			}
			set
			{
				this.dialPlanId = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000DFEB File Offset: 0x0000C1EB
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000DFF3 File Offset: 0x0000C1F3
		public ADObjectId MailboxSiteId
		{
			get
			{
				return this.mailboxSiteId;
			}
			set
			{
				this.mailboxSiteId = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000DFFC File Offset: 0x0000C1FC
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000E004 File Offset: 0x0000C204
		public string[] AddressList
		{
			get
			{
				return this.addressList;
			}
			set
			{
				this.addressList = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000E00D File Offset: 0x0000C20D
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000E015 File Offset: 0x0000C215
		public string DomainController
		{
			get
			{
				return this.domainController;
			}
			set
			{
				this.domainController = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000E01E File Offset: 0x0000C21E
		public ADUser User
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000E028 File Offset: 0x0000C228
		protected override void LogErrorEvent(Exception ex)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_RPCRequestError, null, new object[]
			{
				this.GetFriendlyName(),
				(this.user != null) ? this.user.Name : string.Empty,
				CommonUtil.ToEventLogString(ex)
			});
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000E07D File Offset: 0x0000C27D
		protected virtual void PopulateUserFields(ADUser adUser)
		{
		}

		// Token: 0x040002B4 RID: 692
		private ADObjectId userId;

		// Token: 0x040002B5 RID: 693
		private ADObjectId policyId;

		// Token: 0x040002B6 RID: 694
		private ADObjectId dialPlanId;

		// Token: 0x040002B7 RID: 695
		private string[] addressList;

		// Token: 0x040002B8 RID: 696
		private string domainController;

		// Token: 0x040002B9 RID: 697
		[NonSerialized]
		private ADUser user;

		// Token: 0x040002BA RID: 698
		[NonSerialized]
		private ADObjectId mailboxSiteId;
	}
}
