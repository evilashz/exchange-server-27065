using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000045 RID: 69
	[Cmdlet("Set", "TenantReadiness", DefaultParameterSetName = "SingleTenantUpdate")]
	public class SetTenantReadiness : SymphonyTaskBase
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000EE7C File Offset: 0x0000D07C
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000EE84 File Offset: 0x0000D084
		[Parameter(Mandatory = false, ParameterSetName = "SingleTenantUpdate")]
		public string[] Constraints { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000EE8D File Offset: 0x0000D08D
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000EE95 File Offset: 0x0000D095
		[Parameter(Mandatory = false, ParameterSetName = "SingleTenantUpdate")]
		public string Group { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000EE9E File Offset: 0x0000D09E
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000EEA6 File Offset: 0x0000D0A6
		[Parameter(Mandatory = false, ParameterSetName = "SingleTenantUpdate")]
		public bool IsReady { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000EEAF File Offset: 0x0000D0AF
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000EEB7 File Offset: 0x0000D0B7
		[Parameter(Mandatory = true, ParameterSetName = "SingleTenantUpdate")]
		public Guid TenantId { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000EEC8 File Offset: 0x0000D0C8
		[Parameter(Mandatory = false, ParameterSetName = "SingleTenantUpdate")]
		public int UpgradeUnits { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000EED1 File Offset: 0x0000D0D1
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000EED9 File Offset: 0x0000D0D9
		[Parameter(Mandatory = false, ParameterSetName = "SingleTenantUpdate")]
		public SwitchParameter UseDefaultCapacity { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000EEE2 File Offset: 0x0000D0E2
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000EEEA File Offset: 0x0000D0EA
		[Parameter(Mandatory = true, ParameterSetName = "MultiTenantUpdate")]
		public PSObject[] TenantReadinesses { get; set; }

		// Token: 0x06000359 RID: 857 RVA: 0x0000EF20 File Offset: 0x0000D120
		protected override void InternalProcessRecord()
		{
			SetTenantReadiness.<>c__DisplayClass1 CS$<>8__locals1 = new SetTenantReadiness.<>c__DisplayClass1();
			CS$<>8__locals1.toUpdate = null;
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (!(parameterSetName == "SingleTenantUpdate"))
				{
					if (parameterSetName == "MultiTenantUpdate")
					{
						if (this.TenantReadinesses.Length > 500)
						{
							base.ThrowTerminatingError(new PSArgumentException("Cannot update more than 500 tenants at a time"), ErrorCategory.InvalidArgument, this.TenantReadinesses);
						}
						List<TenantReadiness> list = new List<TenantReadiness>();
						foreach (PSObject psobject in this.TenantReadinesses)
						{
							string[] constraints = base.GetPropertyValue(psobject.Properties, "Constraints").ToString().Split(new char[]
							{
								';'
							}, StringSplitOptions.RemoveEmptyEntries);
							string groupName = base.GetPropertyValue(psobject.Properties, "GroupName").ToString();
							bool isReady;
							bool.TryParse(base.GetPropertyValue(psobject.Properties, "IsReady").ToString(), out isReady);
							Guid tenantId = new Guid(base.GetPropertyValue(psobject.Properties, "TenantID").ToString());
							int upgradeUnits;
							int.TryParse(base.GetPropertyValue(psobject.Properties, "UpgradeUnits").ToString(), out upgradeUnits);
							bool useDefaultCapacity;
							bool.TryParse(base.GetPropertyValue(psobject.Properties, "UseDefaultCapacity").ToString(), out useDefaultCapacity);
							list.Add(new TenantReadiness(constraints, groupName, isReady, tenantId, upgradeUnits, useDefaultCapacity));
						}
						CS$<>8__locals1.toUpdate = list.ToArray();
					}
				}
				else
				{
					TenantReadiness tenantReadiness = new TenantReadiness(this.Constraints, this.Group, this.IsReady, this.TenantId, this.UpgradeUnits, this.UseDefaultCapacity);
					CS$<>8__locals1.toUpdate = new TenantReadiness[]
					{
						tenantReadiness
					};
				}
			}
			using (ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints> workloadClient = new ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints>(base.WorkloadUri, base.Certificate))
			{
				workloadClient.CallSymphony(delegate
				{
					workloadClient.Proxy.UpdateTenantReadiness(CS$<>8__locals1.toUpdate);
				}, base.WorkloadUri.ToString());
			}
		}

		// Token: 0x04000138 RID: 312
		private const string SingleTenantUpdate = "SingleTenantUpdate";

		// Token: 0x04000139 RID: 313
		private const string MultiTenantUpdate = "MultiTenantUpdate";
	}
}
