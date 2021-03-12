using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200028C RID: 652
	internal class TenantRelocationState
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x000898DE File Offset: 0x00087ADE
		// (set) Token: 0x06001EBC RID: 7868 RVA: 0x000898E6 File Offset: 0x00087AE6
		public string SourceForestFQDN { get; private set; }

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x000898EF File Offset: 0x00087AEF
		// (set) Token: 0x06001EBE RID: 7870 RVA: 0x000898F7 File Offset: 0x00087AF7
		public string TargetForestFQDN { get; private set; }

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x00089900 File Offset: 0x00087B00
		// (set) Token: 0x06001EC0 RID: 7872 RVA: 0x00089908 File Offset: 0x00087B08
		public TenantRelocationStatus SourceForestState { get; private set; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x00089911 File Offset: 0x00087B11
		// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x00089919 File Offset: 0x00087B19
		public RelocationStatusDetailsDestination TargetForestState { get; private set; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x00089922 File Offset: 0x00087B22
		// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x0008992A File Offset: 0x00087B2A
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x00089933 File Offset: 0x00087B33
		// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x0008993B File Offset: 0x00087B3B
		public OrganizationId TargetOrganizationId { get; private set; }

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00089944 File Offset: 0x00087B44
		public TenantRelocationState(string sourceForestFQDN, TenantRelocationStatus sourceForestState)
		{
			if (string.IsNullOrEmpty(sourceForestFQDN))
			{
				throw new ArgumentNullException("sourceForestFQDN");
			}
			this.SourceForestFQDN = sourceForestFQDN;
			this.SourceForestState = sourceForestState;
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x0008996D File Offset: 0x00087B6D
		public TenantRelocationState(string sourceForestFQDN, TenantRelocationStatus sourceForestState, string targetForestFQDN, RelocationStatusDetailsDestination targetForestState, OrganizationId organizationId, OrganizationId targetOrganizationId) : this(sourceForestFQDN, sourceForestState)
		{
			if (string.IsNullOrEmpty(targetForestFQDN))
			{
				throw new ArgumentNullException("targetForestFQDN");
			}
			this.TargetForestFQDN = targetForestFQDN;
			this.TargetForestState = targetForestState;
			this.OrganizationId = organizationId;
			this.TargetOrganizationId = targetOrganizationId;
		}
	}
}
