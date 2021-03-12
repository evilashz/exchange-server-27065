using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000044 RID: 68
	[Cmdlet("Set", "SymphonyGroup", DefaultParameterSetName = "SingleGroupUpdate")]
	public class SetSymphonyGroup : SymphonyTaskBase
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		[Parameter(Mandatory = true, ParameterSetName = "SingleGroupUpdate")]
		public DataCenterRegion Region { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000ECB1 File Offset: 0x0000CEB1
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000ECB9 File Offset: 0x0000CEB9
		[Parameter(Mandatory = true, ParameterSetName = "SingleGroupUpdate")]
		public string Group { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000ECC2 File Offset: 0x0000CEC2
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000ECCA File Offset: 0x0000CECA
		[Parameter(Mandatory = true, ParameterSetName = "MultiGroupUpdate")]
		public PSObject[] Groups { get; set; }

		// Token: 0x06000349 RID: 841 RVA: 0x0000ED00 File Offset: 0x0000CF00
		protected override void InternalProcessRecord()
		{
			SetSymphonyGroup.<>c__DisplayClass1 CS$<>8__locals1 = new SetSymphonyGroup.<>c__DisplayClass1();
			CS$<>8__locals1.toUpdate = null;
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (!(parameterSetName == "SingleGroupUpdate"))
				{
					if (parameterSetName == "MultiGroupUpdate")
					{
						List<Group> list = new List<Group>();
						foreach (PSObject psobject in this.Groups)
						{
							string groupName = base.GetPropertyValue(psobject.Properties, "GroupName").ToString();
							int num;
							int.TryParse(base.GetPropertyValue(psobject.Properties, "Region").ToString(), out num);
							DataCenterRegion regionName = (DataCenterRegion)num;
							list.Add(new Group(groupName, regionName));
						}
						CS$<>8__locals1.toUpdate = list.ToArray();
					}
				}
				else
				{
					Group group = new Group(this.Group, this.Region);
					CS$<>8__locals1.toUpdate = new Group[]
					{
						group
					};
				}
			}
			using (ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints> workloadClient = new ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints>(base.WorkloadUri, base.Certificate))
			{
				workloadClient.CallSymphony(delegate
				{
					workloadClient.Proxy.UpdateGroup(CS$<>8__locals1.toUpdate);
				}, base.WorkloadUri.ToString());
			}
		}

		// Token: 0x04000133 RID: 307
		private const string SingleGroupUpdate = "SingleGroupUpdate";

		// Token: 0x04000134 RID: 308
		private const string MultiGroupUpdate = "MultiGroupUpdate";
	}
}
