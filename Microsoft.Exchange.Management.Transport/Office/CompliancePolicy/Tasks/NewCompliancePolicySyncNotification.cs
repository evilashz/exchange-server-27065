using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200011D RID: 285
	[Cmdlet("New", "CompliancePolicySyncNotification", SupportsShouldProcess = true)]
	public sealed class NewCompliancePolicySyncNotification : DataAccessTask<UnifiedPolicySyncNotification>
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0002EA46 File Offset: 0x0002CC46
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x0002EA5D File Offset: 0x0002CC5D
		[Parameter(Mandatory = true)]
		public string Identity
		{
			get
			{
				return (string)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0002EA70 File Offset: 0x0002CC70
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0002EA87 File Offset: 0x0002CC87
		[Parameter(Mandatory = true)]
		public string SyncSvcUrl
		{
			get
			{
				return (string)base.Fields["SyncSvcUrl"];
			}
			set
			{
				base.Fields["SyncSvcUrl"] = value;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0002EA9A File Offset: 0x0002CC9A
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x0002EAC0 File Offset: 0x0002CCC0
		[Parameter(Mandatory = false)]
		public SwitchParameter FullSync
		{
			get
			{
				return (SwitchParameter)(base.Fields["FullSync"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["FullSync"] = value;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0002EAD8 File Offset: 0x0002CCD8
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x0002EAFE File Offset: 0x0002CCFE
		[Parameter(Mandatory = false)]
		public SwitchParameter SyncNow
		{
			get
			{
				return (SwitchParameter)(base.Fields["SyncNow"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SyncNow"] = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0002EB16 File Offset: 0x0002CD16
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x0002EB2D File Offset: 0x0002CD2D
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> SyncChangeInfos
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["SyncChangeInfos"];
			}
			set
			{
				base.Fields["SyncChangeInfos"] = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0002EB40 File Offset: 0x0002CD40
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x0002EB57 File Offset: 0x0002CD57
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002EB6C File Offset: 0x0002CD6C
		protected override void InternalValidate()
		{
			if (this.SyncChangeInfos != null)
			{
				try
				{
					foreach (string input in this.SyncChangeInfos)
					{
						SyncChangeInfo.Parse(input);
					}
				}
				catch (FormatException ex)
				{
					base.WriteError(new InvalidOperationException("Invalid format for SyncChangeInfo paramemter. The detail of the error is " + ex.Message), (ErrorCategory)1000, this.SyncChangeInfos);
				}
			}
			try
			{
				if (!string.IsNullOrEmpty(this.SyncSvcUrl))
				{
					new Uri(this.SyncSvcUrl, UriKind.Absolute);
				}
			}
			catch (FormatException ex2)
			{
				base.WriteError(new InvalidOperationException("Invalid format for SyncSvcUrl parameter. The detail of the error is " + ex2.Message), (ErrorCategory)1000, this.SyncChangeInfos);
			}
			base.InternalValidate();
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002EC58 File Offset: 0x0002CE58
		protected override IConfigDataProvider CreateSession()
		{
			this.ResolveCurrentOrganization();
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 141, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\UnifiedPolicy\\Tasks\\NewCompliancePolicySyncNotification.cs");
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002EC94 File Offset: 0x0002CE94
		protected override void InternalProcessRecord()
		{
			ADUser discoveryMailbox = MailboxDataProvider.GetDiscoveryMailbox((IRecipientSession)base.DataSession);
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(discoveryMailbox, null);
			List<SyncChangeInfo> list = null;
			if (this.SyncChangeInfos != null && this.SyncChangeInfos.Count > 0)
			{
				list = new List<SyncChangeInfo>();
				foreach (string input in this.SyncChangeInfos)
				{
					list.Add(new SyncChangeInfo(input));
				}
			}
			SyncNotificationResult syncNotificationResult = RpcClientWrapper.NotifySyncChanges(this.Identity, exchangePrincipal.MailboxInfo.Location.ServerFqdn, new Guid(this.organizationId.ToExternalDirectoryOrganizationId()), this.SyncSvcUrl, this.FullSync, this.SyncNow, (list != null) ? list.ToArray() : null);
			if (!syncNotificationResult.Success)
			{
				base.WriteError(syncNotificationResult.Error, ErrorCategory.WriteError, syncNotificationResult);
			}
			base.WriteObject((UnifiedPolicySyncNotification)syncNotificationResult.ResultObject);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002EDA8 File Offset: 0x0002CFA8
		private void ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 200, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\UnifiedPolicy\\Tasks\\NewCompliancePolicySyncNotification.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
				this.organizationId = adorganizationalUnit.OrganizationId;
				return;
			}
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
			this.organizationId = base.CurrentOrganizationId;
		}

		// Token: 0x0400043F RID: 1087
		private OrganizationId organizationId;
	}
}
