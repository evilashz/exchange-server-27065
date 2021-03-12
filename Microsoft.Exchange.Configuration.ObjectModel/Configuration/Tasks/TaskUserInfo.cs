using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000AF RID: 175
	internal class TaskUserInfo
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001A895 File Offset: 0x00018A95
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001A89D File Offset: 0x00018A9D
		public OrganizationId ExecutingUserOrganizationId { get; private set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001A8A6 File Offset: 0x00018AA6
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0001A8AE File Offset: 0x00018AAE
		public OrganizationId CurrentOrganizationId { get; private set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001A8B7 File Offset: 0x00018AB7
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0001A8BF File Offset: 0x00018ABF
		public ADObjectId ExecutingUserId { get; private set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001A8C8 File Offset: 0x00018AC8
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001A8D0 File Offset: 0x00018AD0
		public string ExecutingUserIdentityName { get; private set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001A8D9 File Offset: 0x00018AD9
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001A8E1 File Offset: 0x00018AE1
		public SmtpAddress ExecutingWindowsLiveId { get; private set; }

		// Token: 0x06000722 RID: 1826 RVA: 0x0001A8EA File Offset: 0x00018AEA
		public TaskUserInfo(OrganizationId executingUserOrganizationId, OrganizationId currentOrganizationId, ADObjectId executingUserId, string executingUserIdentityName, SmtpAddress executingWindowsLiveId)
		{
			this.ExecutingUserOrganizationId = executingUserOrganizationId;
			this.CurrentOrganizationId = currentOrganizationId;
			this.ExecutingUserId = executingUserId;
			this.ExecutingUserIdentityName = executingUserIdentityName;
			this.ExecutingWindowsLiveId = executingWindowsLiveId;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001A917 File Offset: 0x00018B17
		public void UpdateCurrentOrganizationId(OrganizationId currentOrgId)
		{
			this.CurrentOrganizationId = currentOrgId;
		}
	}
}
