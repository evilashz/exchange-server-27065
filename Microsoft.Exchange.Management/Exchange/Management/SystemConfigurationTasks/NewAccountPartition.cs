using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B19 RID: 2841
	[Cmdlet("New", "AccountPartition", SupportsShouldProcess = true, DefaultParameterSetName = "Trust")]
	public sealed class NewAccountPartition : NewSystemConfigurationObjectTask<AccountPartition>
	{
		// Token: 0x17001EA9 RID: 7849
		// (get) Token: 0x060064D5 RID: 25813 RVA: 0x001A4CB3 File Offset: 0x001A2EB3
		// (set) Token: 0x060064D6 RID: 25814 RVA: 0x001A4CCA File Offset: 0x001A2ECA
		[Parameter(Mandatory = true, ParameterSetName = "Secondary")]
		[Parameter(Mandatory = true, ParameterSetName = "Trust")]
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

		// Token: 0x17001EAA RID: 7850
		// (get) Token: 0x060064D7 RID: 25815 RVA: 0x001A4CDD File Offset: 0x001A2EDD
		// (set) Token: 0x060064D8 RID: 25816 RVA: 0x001A4D03 File Offset: 0x001A2F03
		[Parameter(Mandatory = true, ParameterSetName = "LocalForest")]
		public SwitchParameter LocalForest
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

		// Token: 0x17001EAB RID: 7851
		// (get) Token: 0x060064D9 RID: 25817 RVA: 0x001A4D1B File Offset: 0x001A2F1B
		// (set) Token: 0x060064DA RID: 25818 RVA: 0x001A4D41 File Offset: 0x001A2F41
		[Parameter(Mandatory = false, ParameterSetName = "Trust")]
		[Parameter(Mandatory = false, ParameterSetName = "LocalForest")]
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

		// Token: 0x17001EAC RID: 7852
		// (get) Token: 0x060064DB RID: 25819 RVA: 0x001A4D59 File Offset: 0x001A2F59
		// (set) Token: 0x060064DC RID: 25820 RVA: 0x001A4D7F File Offset: 0x001A2F7F
		[Parameter(Mandatory = true, ParameterSetName = "Secondary")]
		public SwitchParameter Secondary
		{
			get
			{
				return (SwitchParameter)(base.Fields["Secondary"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Secondary"] = value;
			}
		}

		// Token: 0x17001EAD RID: 7853
		// (get) Token: 0x060064DD RID: 25821 RVA: 0x001A4D97 File Offset: 0x001A2F97
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewAccountPartition(base.Name);
			}
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x001A4DA4 File Offset: 0x001A2FA4
		protected override void InternalValidate()
		{
			this.adTrust = null;
			if (this.Trust != null)
			{
				this.adTrust = NewAccountPartition.ResolveAndValidateForestTrustForADDomain(this.Trust, new Task.ErrorLoggerDelegate(base.WriteError), (IConfigurationSession)base.DataSession);
			}
			if (this.LocalForest)
			{
				NewAccountPartition.VerifyNoOtherPartitionIsLocalForest(base.GlobalConfigSession, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			base.InternalValidate();
			bool hasErrors = base.HasErrors;
		}

		// Token: 0x060064DF RID: 25823 RVA: 0x001A4E1C File Offset: 0x001A301C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (AccountPartition)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			this.DataObject.SetId(configurationSession.GetOrgContainerId().GetChildId(AccountPartition.AccountForestContainerName).GetChildId(this.DataObject.Name));
			this.DataObject.TrustedDomain = ((this.Trust == null) ? null : this.adTrust.Id);
			this.DataObject.IsLocalForest = this.LocalForest;
			this.DataObject.EnabledForProvisioning = this.EnabledForProvisioning;
			this.DataObject.IsSecondary = this.Secondary;
			return this.DataObject;
		}

		// Token: 0x060064E0 RID: 25824 RVA: 0x001A4EEC File Offset: 0x001A30EC
		internal static ADDomainTrustInfo ResolveAndValidateForestTrustForADDomain(Fqdn domainFqdn, Task.ErrorLoggerDelegate errorDelegate, IConfigurationSession configSession)
		{
			if (domainFqdn == null)
			{
				throw new ArgumentNullException("domainFqdn");
			}
			if (errorDelegate == null)
			{
				throw new ArgumentNullException("errorDelegate");
			}
			if (configSession == null)
			{
				throw new ArgumentNullException("configSession");
			}
			ADForest localForest = ADForest.GetLocalForest();
			ADDomainTrustInfo[] array = localForest.FindTrustRelationshipsForDomain(domainFqdn);
			if (array == null || array.Length == 0)
			{
				errorDelegate(new ManagementObjectNotFoundException(Strings.ErrorManagementObjectNotFound(domainFqdn.ToString())), ExchangeErrorCategory.Client, null);
			}
			else if (array.Length != 1)
			{
				errorDelegate(new ManagementObjectAmbiguousException(Strings.ErrorManagementObjectAmbiguous(domainFqdn.ToString())), ExchangeErrorCategory.Client, null);
			}
			ADDomainTrustInfo addomainTrustInfo = array[0];
			AccountPartition[] array2 = configSession.Find<AccountPartition>(configSession.GetOrgContainerId().GetChildId(AccountPartition.AccountForestContainerName), QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, AccountPartitionSchema.TrustedDomainLink, addomainTrustInfo.DistinguishedName), null, 1);
			if (array2 != null && array2.Length != 0)
			{
				errorDelegate(new LocalizedException(Strings.ErrorTrustAlreadyInUse(addomainTrustInfo.Name, array2[0].Name)), ExchangeErrorCategory.Client, null);
			}
			return addomainTrustInfo;
		}

		// Token: 0x060064E1 RID: 25825 RVA: 0x001A4FDC File Offset: 0x001A31DC
		internal static void VerifyNoOtherPartitionIsLocalForest(ITopologyConfigurationSession topologySession, Task.ErrorLoggerDelegate errorDelegate)
		{
			AccountPartition[] array = topologySession.FindAllAccountPartitions();
			AccountPartition accountPartition = null;
			foreach (AccountPartition accountPartition2 in array)
			{
				if (accountPartition2.IsLocalForest)
				{
					accountPartition = accountPartition2;
					break;
				}
			}
			if (accountPartition != null)
			{
				errorDelegate(new LocalizedException(Strings.ErrorOnlyOnePartitionCanBeLocalForest(accountPartition.Name)), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x04003639 RID: 13881
		private ADDomainTrustInfo adTrust;
	}
}
