using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D14 RID: 3348
	public abstract class UMReportsTaskBase<TIdentity> : ObjectActionTenantADTask<TIdentity, ADUser> where TIdentity : IIdentityParameter, new()
	{
		// Token: 0x170027D2 RID: 10194
		// (get) Token: 0x06008075 RID: 32885 RVA: 0x0020D565 File Offset: 0x0020B765
		// (set) Token: 0x06008076 RID: 32886 RVA: 0x0020D56D File Offset: 0x0020B76D
		public override TIdentity Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170027D3 RID: 10195
		// (get) Token: 0x06008077 RID: 32887 RVA: 0x0020D576 File Offset: 0x0020B776
		// (set) Token: 0x06008078 RID: 32888 RVA: 0x0020D58D File Offset: 0x0020B78D
		[Parameter]
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

		// Token: 0x06008079 RID: 32889 RVA: 0x0020D5A0 File Offset: 0x0020B7A0
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.Organization != null)
			{
				IConfigurationSession configurationSession = this.CreateSessionToResolveConfigObjects(true);
				configurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, configurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				base.CurrentOrganizationId = adorganizationalUnit.OrganizationId;
			}
		}

		// Token: 0x0600807A RID: 32890
		protected abstract void ProcessMailbox();

		// Token: 0x0600807B RID: 32891 RVA: 0x0020D618 File Offset: 0x0020B818
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 107, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\UMReportsTaskBase.cs");
		}

		// Token: 0x0600807C RID: 32892 RVA: 0x0020D654 File Offset: 0x0020B854
		protected override IConfigurable ResolveDataObject()
		{
			ADUser result = null;
			try
			{
				result = CommonUtil.ValidateAndReturnUMDataStorageOrgMbx(OrganizationMailbox.GetOrganizationMailboxesByCapability((IRecipientSession)base.DataSession, OrganizationCapability.UMDataStorage));
			}
			catch (ObjectNotFoundException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
			}
			catch (NonUniqueRecipientException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ReadError, null);
			}
			return result;
		}

		// Token: 0x0600807D RID: 32893 RVA: 0x0020D6B4 File Offset: 0x0020B8B4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.ProcessMailbox();
			TaskLogger.LogExit();
		}

		// Token: 0x0600807E RID: 32894 RVA: 0x0020D6C8 File Offset: 0x0020B8C8
		protected void ValidateCommonParamsAndSetOrg(UMDialPlanIdParameter dpParam, UMIPGatewayIdParameter gwParam, out Guid dpGuid, out Guid gwGuid, out string dpName, out string gwName)
		{
			dpGuid = Guid.Empty;
			gwGuid = Guid.Empty;
			dpName = string.Empty;
			gwName = string.Empty;
			if (dpParam == null && gwParam == null)
			{
				return;
			}
			IConfigurationSession session = this.CreateSessionToResolveConfigObjects(false);
			OrganizationId organizationId = null;
			if (dpParam != null)
			{
				UMDialPlan umdialPlan = (UMDialPlan)base.GetDataObject<UMDialPlan>(dpParam, session, null, new LocalizedString?(Strings.NonExistantDialPlan(dpParam.ToString())), new LocalizedString?(Strings.MultipleDialplansWithSameId(dpParam.ToString())));
				dpGuid = umdialPlan.Guid;
				dpName = umdialPlan.Name;
				organizationId = umdialPlan.OrganizationId;
			}
			if (gwParam != null)
			{
				UMIPGateway umipgateway = (UMIPGateway)base.GetDataObject<UMIPGateway>(gwParam, session, null, new LocalizedString?(Strings.NonExistantIPGateway(gwParam.ToString())), new LocalizedString?(Strings.MultipleIPGatewaysWithSameId(gwParam.ToString())));
				gwGuid = umipgateway.Guid;
				gwName = umipgateway.Name;
				if (organizationId != null && !organizationId.Equals(umipgateway.OrganizationId))
				{
					base.WriteError(new InvalidParameterException(Strings.MismatchedOrgInDPAndGW(dpParam.ToString(), gwParam.ToString())), ErrorCategory.InvalidArgument, null);
				}
				else
				{
					organizationId = umipgateway.OrganizationId;
				}
			}
			if (this.Organization != null)
			{
				organizationId != null;
			}
			if (organizationId != null)
			{
				base.CurrentOrganizationId = organizationId;
			}
		}

		// Token: 0x0600807F RID: 32895 RVA: 0x0020D804 File Offset: 0x0020BA04
		internal MailboxSession ConnectToEDiscoveryMailbox(string clientString)
		{
			ADUser dataObject = this.DataObject;
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(dataObject, RemotingOptions.AllowCrossSite);
			return MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, clientString);
		}

		// Token: 0x06008080 RID: 32896 RVA: 0x0020D830 File Offset: 0x0020BA30
		private IConfigurationSession CreateSessionToResolveConfigObjects(bool scopeToExcecutingUser)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, scopeToExcecutingUser);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 278, "CreateSessionToResolveConfigObjects", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\UMReportsTaskBase.cs");
		}
	}
}
