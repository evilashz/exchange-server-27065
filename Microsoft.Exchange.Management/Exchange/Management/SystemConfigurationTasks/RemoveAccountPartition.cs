using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B1B RID: 2843
	[Cmdlet("Remove", "AccountPartition", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveAccountPartition : RemoveSystemConfigurationObjectTask<AccountPartitionIdParameter, AccountPartition>
	{
		// Token: 0x17001EB3 RID: 7859
		// (get) Token: 0x060064EE RID: 25838 RVA: 0x001A536C File Offset: 0x001A356C
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				return configurationSession.GetOrgContainerId().GetChildId(AccountPartition.AccountForestContainerName);
			}
		}

		// Token: 0x17001EB4 RID: 7860
		// (get) Token: 0x060064EF RID: 25839 RVA: 0x001A5395 File Offset: 0x001A3595
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAccountPartition(this.Identity.ToString());
			}
		}

		// Token: 0x17001EB5 RID: 7861
		// (get) Token: 0x060064F0 RID: 25840 RVA: 0x001A53A7 File Offset: 0x001A35A7
		// (set) Token: 0x060064F1 RID: 25841 RVA: 0x001A53CD File Offset: 0x001A35CD
		[Parameter]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x060064F2 RID: 25842 RVA: 0x001A53E8 File Offset: 0x001A35E8
		protected override void InternalProcessRecord()
		{
			if (!this.Force)
			{
				PartitionId partitionId = null;
				if (base.DataObject.TryGetPartitionId(out partitionId))
				{
					ADSessionSettings sessionSettings = ADSessionSettings.SessionSettingsFactory.Default.FromAllTenantsPartitionId(partitionId);
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 65, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\SAFM\\RemoveAccountPartition.cs");
					ExchangeConfigurationUnit[] array = tenantConfigurationSession.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, new NotFilter(new ExistsFilter(OrganizationSchema.SharedConfigurationInfo)), null, 1);
					if (array != null && array.Length != 0 && !base.ShouldContinue(Strings.ConfirmationRemoveAccountPartitionWithTenants(this.Identity.ToString())))
					{
						return;
					}
				}
			}
			base.InternalProcessRecord();
		}
	}
}
