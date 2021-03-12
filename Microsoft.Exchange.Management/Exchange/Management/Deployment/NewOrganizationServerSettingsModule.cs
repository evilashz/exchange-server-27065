using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000210 RID: 528
	internal class NewOrganizationServerSettingsModule : RunspaceServerSettingsInitModule
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0004ECE4 File Offset: 0x0004CEE4
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x0004ECFB File Offset: 0x0004CEFB
		private SmtpDomain DomainName
		{
			get
			{
				return (SmtpDomain)this.fields["TenantDomainName"];
			}
			set
			{
				this.fields["TenantDomainName"] = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0004ED0E File Offset: 0x0004CF0E
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x0004ED28 File Offset: 0x0004CF28
		private string Name
		{
			get
			{
				return (string)this.fields["TenantName"];
			}
			set
			{
				LocalizedString localizedString;
				this.fields["TenantName"] = MailboxTaskHelper.GetNameOfAcceptableLengthForMultiTenantMode(value, out localizedString);
				if (localizedString != LocalizedString.Empty)
				{
					base.CurrentTaskContext.CommandShell.WriteWarning(localizedString);
				}
			}
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0004ED6B File Offset: 0x0004CF6B
		public NewOrganizationServerSettingsModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0004ED74 File Offset: 0x0004CF74
		protected override ADServerSettings GetCmdletADServerSettings()
		{
			this.fields = base.CurrentTaskContext.InvocationInfo.Fields;
			SwitchParameter switchParameter = this.fields.Contains("IsDatacenter") ? ((SwitchParameter)this.fields["IsDatacenter"]) : new SwitchParameter(false);
			if (!this.fields.Contains(ManageOrganizationTaskBase.ParameterCreateSharedConfig))
			{
				new SwitchParameter(false);
			}
			else
			{
				SwitchParameter switchParameter2 = (SwitchParameter)this.fields[ManageOrganizationTaskBase.ParameterCreateSharedConfig];
			}
			string text = (string)this.fields["TenantProgramId"];
			string text2 = (string)this.fields["TenantOfferId"];
			AccountPartitionIdParameter accountPartitionIdParameter = (AccountPartitionIdParameter)this.fields["AccountPartition"];
			string value = null;
			if (TopologyProvider.CurrentTopologyMode == TopologyMode.ADTopologyService && switchParameter)
			{
				ADServerSettings serverSettings = ExchangePropertyContainer.GetServerSettings(base.CurrentTaskContext.SessionState);
				if (serverSettings != null && accountPartitionIdParameter != null)
				{
					PartitionId partitionId = RecipientTaskHelper.ResolvePartitionId(accountPartitionIdParameter, null);
					if (partitionId != null)
					{
						value = serverSettings.PreferredGlobalCatalog(partitionId.ForestFQDN);
					}
				}
				if (string.IsNullOrEmpty(value) && this.Name != null)
				{
					if (this.domainBasedADServerSettings == null)
					{
						PartitionId partitionId2 = (accountPartitionIdParameter == null) ? PartitionId.LocalForest : RecipientTaskHelper.ResolvePartitionId(accountPartitionIdParameter, null);
						this.domainBasedADServerSettings = RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(this.Name.ToLowerInvariant(), partitionId2.ForestFQDN, false);
					}
					return this.domainBasedADServerSettings;
				}
			}
			return base.GetCmdletADServerSettings();
		}

		// Token: 0x040007BF RID: 1983
		private ADServerSettings domainBasedADServerSettings;

		// Token: 0x040007C0 RID: 1984
		private PropertyBag fields;
	}
}
