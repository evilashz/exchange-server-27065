using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000041 RID: 65
	[Cmdlet("Set", "Constraint", DefaultParameterSetName = "SingleConstraintUpdate")]
	public class SetConstraint : SymphonyTaskBase
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000E373 File Offset: 0x0000C573
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000E37B File Offset: 0x0000C57B
		[Parameter(Mandatory = true, ParameterSetName = "SingleConstraintUpdate")]
		public DateTime FixByDate { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000E384 File Offset: 0x0000C584
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000E38C File Offset: 0x0000C58C
		[Parameter(Mandatory = true, ParameterSetName = "SingleConstraintUpdate")]
		public string Owner { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000E395 File Offset: 0x0000C595
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000E39D File Offset: 0x0000C59D
		[Parameter(Mandatory = true, ParameterSetName = "SingleConstraintUpdate")]
		public ConstraintStatus Status { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000E3AE File Offset: 0x0000C5AE
		[Parameter(Mandatory = true, ParameterSetName = "SingleConstraintUpdate")]
		public bool IsBlocking { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000E3B7 File Offset: 0x0000C5B7
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000E3BF File Offset: 0x0000C5BF
		[Parameter(Mandatory = false, ParameterSetName = "SingleConstraintUpdate")]
		public string Comment { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		[Parameter(Mandatory = true, ParameterSetName = "SingleConstraintUpdate")]
		public string Name { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000E3D9 File Offset: 0x0000C5D9
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000E3E1 File Offset: 0x0000C5E1
		[Parameter(Mandatory = true, ParameterSetName = "MultiConstraintUpdate")]
		public PSObject[] Constraints { get; set; }

		// Token: 0x06000327 RID: 807 RVA: 0x0000E418 File Offset: 0x0000C618
		protected override void InternalProcessRecord()
		{
			SetConstraint.<>c__DisplayClass1 CS$<>8__locals1 = new SetConstraint.<>c__DisplayClass1();
			CS$<>8__locals1.toUpdate = null;
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (!(parameterSetName == "SingleConstraintUpdate"))
				{
					if (parameterSetName == "MultiConstraintUpdate")
					{
						List<Constraint> list = new List<Constraint>();
						foreach (PSObject psobject in this.Constraints)
						{
							string constraintName = base.GetPropertyValue(psobject.Properties, "ConstraintName").ToString();
							string owner = base.GetPropertyValue(psobject.Properties, "Owner").ToString();
							string comment = base.GetPropertyValue(psobject.Properties, "Comment").ToString();
							bool isBlocking;
							bool.TryParse(base.GetPropertyValue(psobject.Properties, "IsBlocking").ToString(), out isBlocking);
							int num;
							int.TryParse(base.GetPropertyValue(psobject.Properties, "Status").ToString(), out num);
							ConstraintStatus status = (ConstraintStatus)num;
							DateTime fixByDate;
							DateTime.TryParse(base.GetPropertyValue(psobject.Properties, "FixByDate").ToString(), out fixByDate);
							list.Add(new Constraint(constraintName, owner, fixByDate, status, isBlocking, comment));
						}
						CS$<>8__locals1.toUpdate = list.ToArray();
					}
				}
				else
				{
					Constraint constraint = new Constraint(this.Name, this.Owner, this.FixByDate, this.Status, this.IsBlocking, this.Comment);
					CS$<>8__locals1.toUpdate = new Constraint[]
					{
						constraint
					};
				}
			}
			using (ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints> workloadClient = new ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints>(base.WorkloadUri, base.Certificate))
			{
				workloadClient.CallSymphony(delegate
				{
					workloadClient.Proxy.UpdateConstraint(CS$<>8__locals1.toUpdate);
				}, base.WorkloadUri.ToString());
			}
		}

		// Token: 0x0400011B RID: 283
		private const string SingleConstraintUpdate = "SingleConstraintUpdate";

		// Token: 0x0400011C RID: 284
		private const string MultiConstraintUpdate = "MultiConstraintUpdate";
	}
}
