using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000043 RID: 67
	[Cmdlet("Set", "GroupCapacity", DefaultParameterSetName = "SingleGroupCapacityUpdate")]
	public class SetGroupCapacity : SymphonyTaskBase
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000E970 File Offset: 0x0000CB70
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000E978 File Offset: 0x0000CB78
		[Parameter(Mandatory = false, ParameterSetName = "SingleGroupCapacityUpdate")]
		public DateTime StartDate { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000E981 File Offset: 0x0000CB81
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000E989 File Offset: 0x0000CB89
		[Parameter(Mandatory = false, ParameterSetName = "SingleGroupCapacityUpdate")]
		public int UpgradeUnits { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000E992 File Offset: 0x0000CB92
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000E99A File Offset: 0x0000CB9A
		[Parameter(Mandatory = false, ParameterSetName = "SingleGroupCapacityUpdate")]
		public CapacityBlock[] CapacityBlocks { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000E9A3 File Offset: 0x0000CBA3
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000E9AB File Offset: 0x0000CBAB
		[Parameter(Mandatory = true, ParameterSetName = "SingleGroupCapacityUpdate")]
		public string GroupName { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		[Parameter(Mandatory = true, ParameterSetName = "MultiGroupCapacityUpdate")]
		public PSObject[] GroupCapacities { get; set; }

		// Token: 0x06000341 RID: 833 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		protected override void InternalProcessRecord()
		{
			SetGroupCapacity.<>c__DisplayClass1 CS$<>8__locals1 = new SetGroupCapacity.<>c__DisplayClass1();
			CS$<>8__locals1.toUpdate = null;
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (!(parameterSetName == "SingleGroupCapacityUpdate"))
				{
					if (parameterSetName == "MultiGroupCapacityUpdate")
					{
						Dictionary<string, List<CapacityBlock>> dictionary = new Dictionary<string, List<CapacityBlock>>();
						foreach (PSObject psobject in this.GroupCapacities)
						{
							string text = base.GetPropertyValue(psobject.Properties, "GroupName").ToString();
							int upgradeUnits;
							int.TryParse(base.GetPropertyValue(psobject.Properties, "UpgradeUnits").ToString(), out upgradeUnits);
							DateTime startDate;
							DateTime.TryParse(base.GetPropertyValue(psobject.Properties, "StartDate").ToString(), out startDate);
							Console.WriteLine("Capacity Group name is {0}", text);
							if (dictionary.ContainsKey(text))
							{
								Console.WriteLine("CSV Input Contains this groupname already");
								if (dictionary[text].Count >= 20)
								{
									base.ThrowTerminatingError(new PSArgumentException("Cannot update more than 20 capacities per group"), ErrorCategory.InvalidArgument, this.GroupCapacities);
								}
								dictionary[text].Add(new CapacityBlock(startDate, upgradeUnits));
							}
							else
							{
								Console.WriteLine("CSV Input is creating a new group name");
								dictionary.Add(text, new List<CapacityBlock>
								{
									new CapacityBlock(startDate, upgradeUnits)
								});
							}
						}
						List<GroupCapacity> list = new List<GroupCapacity>();
						foreach (KeyValuePair<string, List<CapacityBlock>> keyValuePair in dictionary)
						{
							list.Add(new GroupCapacity(keyValuePair.Key, keyValuePair.Value.ToArray()));
						}
						CS$<>8__locals1.toUpdate = list.ToArray();
					}
				}
				else
				{
					CapacityBlock[] capacities;
					if (this.CapacityBlocks == null)
					{
						CapacityBlock capacityBlock = new CapacityBlock(this.StartDate, this.UpgradeUnits);
						capacities = new CapacityBlock[]
						{
							capacityBlock
						};
					}
					else
					{
						capacities = this.CapacityBlocks;
					}
					GroupCapacity groupCapacity = new GroupCapacity(this.GroupName, capacities);
					CS$<>8__locals1.toUpdate = new GroupCapacity[]
					{
						groupCapacity
					};
				}
			}
			using (ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints> workloadClient = new ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints>(base.WorkloadUri, base.Certificate))
			{
				workloadClient.CallSymphony(delegate
				{
					workloadClient.Proxy.UpdateCapacity(CS$<>8__locals1.toUpdate);
				}, base.WorkloadUri.ToString());
			}
		}

		// Token: 0x0400012C RID: 300
		private const string SingleGroupCapacityUpdate = "SingleGroupCapacityUpdate";

		// Token: 0x0400012D RID: 301
		private const string MultiGroupCapacityUpdate = "MultiGroupCapacityUpdate";
	}
}
