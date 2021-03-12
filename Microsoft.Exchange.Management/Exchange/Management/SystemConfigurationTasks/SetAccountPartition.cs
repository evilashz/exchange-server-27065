using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B1A RID: 2842
	[Cmdlet("Set", "AccountPartition", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class SetAccountPartition : SetSystemConfigurationObjectTask<AccountPartitionIdParameter, AccountPartition>
	{
		// Token: 0x17001EAE RID: 7854
		// (get) Token: 0x060064E3 RID: 25827 RVA: 0x001A5040 File Offset: 0x001A3240
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				return configurationSession.GetOrgContainerId().GetChildId(AccountPartition.AccountForestContainerName);
			}
		}

		// Token: 0x17001EAF RID: 7855
		// (get) Token: 0x060064E4 RID: 25828 RVA: 0x001A5069 File Offset: 0x001A3269
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAccountPartition(this.Identity.ToString());
			}
		}

		// Token: 0x17001EB0 RID: 7856
		// (get) Token: 0x060064E5 RID: 25829 RVA: 0x001A507B File Offset: 0x001A327B
		// (set) Token: 0x060064E6 RID: 25830 RVA: 0x001A5092 File Offset: 0x001A3292
		[Parameter]
		public Fqdn Trust
		{
			get
			{
				return (Fqdn)base.Fields["Trust"];
			}
			set
			{
				base.Fields["Trust"] = value;
			}
		}

		// Token: 0x17001EB1 RID: 7857
		// (get) Token: 0x060064E7 RID: 25831 RVA: 0x001A50A5 File Offset: 0x001A32A5
		// (set) Token: 0x060064E8 RID: 25832 RVA: 0x001A50CB File Offset: 0x001A32CB
		[Parameter(Mandatory = false)]
		public SwitchParameter EnabledForProvisioning
		{
			get
			{
				return (SwitchParameter)(base.Fields["EnabledForProvisioning"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["EnabledForProvisioning"] = value;
			}
		}

		// Token: 0x17001EB2 RID: 7858
		// (get) Token: 0x060064E9 RID: 25833 RVA: 0x001A50E3 File Offset: 0x001A32E3
		// (set) Token: 0x060064EA RID: 25834 RVA: 0x001A5109 File Offset: 0x001A3309
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

		// Token: 0x060064EB RID: 25835 RVA: 0x001A5124 File Offset: 0x001A3324
		protected override void InternalValidate()
		{
			this.adTrust = null;
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.Fields.IsModified("Trust") && this.Trust != null)
			{
				this.adTrust = NewAccountPartition.ResolveAndValidateForestTrustForADDomain(this.Trust, new Task.ErrorLoggerDelegate(base.WriteError), (IConfigurationSession)base.DataSession);
			}
		}

		// Token: 0x060064EC RID: 25836 RVA: 0x001A518C File Offset: 0x001A338C
		protected override void InternalProcessRecord()
		{
			if (base.Fields.IsModified("Trust") && !this.Force)
			{
				if (this.adTrust != null)
				{
					if (this.DataObject.IsLocalForest)
					{
						if (!base.ShouldContinue(Strings.ConfirmationResettingLocalForestAccountPartition(this.DataObject.Name)))
						{
							return;
						}
					}
					else if (this.DataObject.TrustedDomain != null && !this.adTrust.DistinguishedName.Equals(this.DataObject.TrustedDomain.DistinguishedName, StringComparison.OrdinalIgnoreCase) && !base.ShouldContinue(Strings.ConfirmationChangePartitionTrust(this.DataObject.Name, this.DataObject.TrustedDomain.Name, this.adTrust.Name)))
					{
						return;
					}
				}
				else if (this.DataObject.TrustedDomain != null && !base.ShouldContinue(Strings.ConfirmationResettingPartitionTrust(this.DataObject.Name, this.DataObject.TrustedDomain.Name)))
				{
					return;
				}
			}
			if (base.Fields.IsModified("EnabledForProvisioning") && this.DataObject.EnabledForProvisioning && !this.EnabledForProvisioning && !this.Force && !base.ShouldContinue(Strings.ConfirmationDisablingPartitionFromProvisioning(this.DataObject.Name)))
			{
				return;
			}
			if (base.Fields.IsModified("Trust"))
			{
				this.DataObject.TrustedDomain = ((this.adTrust == null) ? null : this.adTrust.Id);
			}
			if (this.DataObject.IsLocalForest && this.adTrust != null)
			{
				this.DataObject.IsLocalForest = false;
			}
			if (base.Fields.IsModified("EnabledForProvisioning"))
			{
				this.DataObject.EnabledForProvisioning = this.EnabledForProvisioning;
			}
			base.InternalProcessRecord();
		}

		// Token: 0x0400363A RID: 13882
		private const string EnabledForProvisioningStr = "EnabledForProvisioning";

		// Token: 0x0400363B RID: 13883
		private ADDomainTrustInfo adTrust;
	}
}
