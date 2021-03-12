using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x020009A5 RID: 2469
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TeamMailboxSyncRpcInParameters : RpcParameters
	{
		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x06005B29 RID: 23337 RVA: 0x0017D234 File Offset: 0x0017B434
		// (set) Token: 0x06005B2A RID: 23338 RVA: 0x0017D23C File Offset: 0x0017B43C
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x06005B2B RID: 23339 RVA: 0x0017D245 File Offset: 0x0017B445
		// (set) Token: 0x06005B2C RID: 23340 RVA: 0x0017D24D File Offset: 0x0017B44D
		public OrganizationId OrgId { get; private set; }

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x06005B2D RID: 23341 RVA: 0x0017D256 File Offset: 0x0017B456
		// (set) Token: 0x06005B2E RID: 23342 RVA: 0x0017D25E File Offset: 0x0017B45E
		public string ClientString { get; private set; }

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x06005B2F RID: 23343 RVA: 0x0017D267 File Offset: 0x0017B467
		// (set) Token: 0x06005B30 RID: 23344 RVA: 0x0017D26F File Offset: 0x0017B46F
		public SyncOption SyncOption { get; private set; }

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x06005B31 RID: 23345 RVA: 0x0017D278 File Offset: 0x0017B478
		// (set) Token: 0x06005B32 RID: 23346 RVA: 0x0017D280 File Offset: 0x0017B480
		public string DomainController { get; private set; }

		// Token: 0x06005B33 RID: 23347 RVA: 0x0017D28C File Offset: 0x0017B48C
		public TeamMailboxSyncRpcInParameters(byte[] data) : base(data)
		{
			this.MailboxGuid = (Guid)base.GetParameterValue("MailboxGuid");
			this.OrgId = (OrganizationId)base.GetParameterValue("OrgId");
			this.ClientString = (string)base.GetParameterValue("ClientString");
			this.SyncOption = (SyncOption)base.GetParameterValue("SyncOption");
			this.DomainController = (string)base.GetParameterValue("DomainController");
		}

		// Token: 0x06005B34 RID: 23348 RVA: 0x0017D310 File Offset: 0x0017B510
		public TeamMailboxSyncRpcInParameters(Guid mailboxGuid, OrganizationId orgId, string clientString, SyncOption syncOption, string domainController)
		{
			this.MailboxGuid = mailboxGuid;
			this.OrgId = orgId;
			this.ClientString = clientString;
			this.SyncOption = syncOption;
			this.DomainController = domainController;
			base.SetParameterValue("MailboxGuid", this.MailboxGuid);
			base.SetParameterValue("OrgId", this.OrgId);
			base.SetParameterValue("ClientString", this.ClientString);
			base.SetParameterValue("SyncOption", this.SyncOption);
			base.SetParameterValue("DomainController", this.DomainController);
		}

		// Token: 0x0400324D RID: 12877
		private const string MailboxGuidParameterName = "MailboxGuid";

		// Token: 0x0400324E RID: 12878
		private const string OrgIdParameterName = "OrgId";

		// Token: 0x0400324F RID: 12879
		private const string ClientStringParameterName = "ClientString";

		// Token: 0x04003250 RID: 12880
		private const string SyncOptionParameterName = "SyncOption";

		// Token: 0x04003251 RID: 12881
		private const string DomainControllerParameterName = "DomainController";
	}
}
