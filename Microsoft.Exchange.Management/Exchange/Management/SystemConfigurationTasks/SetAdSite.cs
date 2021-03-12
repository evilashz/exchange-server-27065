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
	// Token: 0x02000B4A RID: 2890
	[Cmdlet("Set", "AdSite", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetAdSite : SetTopologySystemConfigurationObjectTask<AdSiteIdParameter, ADSite>
	{
		// Token: 0x1700204E RID: 8270
		// (get) Token: 0x060068BF RID: 26815 RVA: 0x001AF906 File Offset: 0x001ADB06
		// (set) Token: 0x060068C0 RID: 26816 RVA: 0x001AF91D File Offset: 0x001ADB1D
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<AdSiteIdParameter> ResponsibleForSites
		{
			get
			{
				return (MultiValuedProperty<AdSiteIdParameter>)base.Fields["ResponsibleForSites"];
			}
			set
			{
				base.Fields["ResponsibleForSites"] = value;
			}
		}

		// Token: 0x1700204F RID: 8271
		// (get) Token: 0x060068C1 RID: 26817 RVA: 0x001AF930 File Offset: 0x001ADB30
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAdSite(this.Identity.ToString());
			}
		}

		// Token: 0x17002050 RID: 8272
		// (get) Token: 0x060068C2 RID: 26818 RVA: 0x001AF942 File Offset: 0x001ADB42
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x001AF945 File Offset: 0x001ADB45
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return true;
		}

		// Token: 0x060068C4 RID: 26820 RVA: 0x001AF948 File Offset: 0x001ADB48
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 83, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Transport\\SetAdSite.cs");
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x001AF974 File Offset: 0x001ADB74
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADSite dataObject = this.DataObject;
			if (dataObject != null)
			{
				if (dataObject.MinorPartnerId != -1 && dataObject.IsModified(ADSiteSchema.MinorPartnerId))
				{
					ADSite[] array = this.ConfigurationSession.Find<ADSite>(null, QueryScope.SubTree, null, null, 0);
					if (array != null && array.Length > 0)
					{
						foreach (ADSite adsite in array)
						{
							if (adsite.MinorPartnerId == dataObject.MinorPartnerId && !adsite.Id.Equals(dataObject.Id))
							{
								base.WriteError(new TaskException(Strings.ErrorMinorPartnerIdIsNotUnique(dataObject.MinorPartnerId.ToString())), (ErrorCategory)1000, null);
							}
						}
					}
				}
				if (base.Fields.IsModified("ResponsibleForSites"))
				{
					dataObject.ResponsibleForSites = base.ResolveIdParameterCollection<AdSiteIdParameter, ADSite, ADObjectId>(this.ResponsibleForSites, base.DataSession, this.RootId, null, (ExchangeErrorCategory)0, new Func<IIdentityParameter, LocalizedString>(Strings.ErrorSiteNotFound), new Func<IIdentityParameter, LocalizedString>(Strings.ErrorSiteNotUnique), null, null);
				}
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x04003693 RID: 13971
		private const string responsibleForSitesPropertyName = "ResponsibleForSites";
	}
}
