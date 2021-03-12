using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D24 RID: 3364
	[Cmdlet("Get", "UMMailboxPin", DefaultParameterSetName = "Identity")]
	public class GetUMMailboxPIN : GetRecipientObjectTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17002805 RID: 10245
		// (get) Token: 0x0600810E RID: 33038 RVA: 0x002102CC File Offset: 0x0020E4CC
		// (set) Token: 0x0600810F RID: 33039 RVA: 0x002102D4 File Offset: 0x0020E4D4
		[Parameter]
		public SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return base.InternalIgnoreDefaultScope;
			}
			set
			{
				base.InternalIgnoreDefaultScope = value;
			}
		}

		// Token: 0x17002806 RID: 10246
		// (get) Token: 0x06008110 RID: 33040 RVA: 0x002102DD File Offset: 0x0020E4DD
		// (set) Token: 0x06008111 RID: 33041 RVA: 0x00210303 File Offset: 0x0020E503
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreErrors
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreErrors"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IgnoreErrors"] = value;
			}
		}

		// Token: 0x17002807 RID: 10247
		// (get) Token: 0x06008112 RID: 33042 RVA: 0x0021031B File Offset: 0x0020E51B
		// (set) Token: 0x06008113 RID: 33043 RVA: 0x00210332 File Offset: 0x0020E532
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

		// Token: 0x17002808 RID: 10248
		// (get) Token: 0x06008114 RID: 33044 RVA: 0x00210348 File Offset: 0x0020E548
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter internalFilter = base.InternalFilter;
				QueryFilter queryFilter = new BitMaskAndFilter(ADUserSchema.UMEnabledFlags, 1UL);
				if (internalFilter != null)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						internalFilter,
						queryFilter
					});
				}
				return queryFilter;
			}
		}

		// Token: 0x17002809 RID: 10249
		// (get) Token: 0x06008115 RID: 33045 RVA: 0x00210383 File Offset: 0x0020E583
		protected override ObjectId RootId
		{
			get
			{
				if (!(base.CurrentOrganizationId == null))
				{
					return base.CurrentOrganizationId.OrganizationalUnit;
				}
				return base.RootId;
			}
		}

		// Token: 0x06008116 RID: 33046 RVA: 0x002103A8 File Offset: 0x0020E5A8
		protected override void InternalProcessRecord()
		{
			this.matchFound = false;
			try
			{
				base.InternalProcessRecord();
			}
			catch (InvalidOperationForGetUMMailboxPinException ex)
			{
				this.WriteWarningOrError(ex.LocalizedString);
				return;
			}
			if (this.Identity != null && !this.matchFound)
			{
				LocalizedString errorMessageObjectNotFound = base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), null);
				base.WriteError(new ManagementObjectNotFoundException(errorMessageObjectNotFound), ErrorCategory.InvalidData, this.Identity);
			}
		}

		// Token: 0x06008117 RID: 33047 RVA: 0x00210428 File Offset: 0x0020E628
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			PINInfo pininfo = null;
			ADUser aduser = dataObject as ADUser;
			this.userObject = aduser;
			if (UMSubscriber.IsValidSubscriber(aduser))
			{
				this.matchFound = true;
				try
				{
					using (IUMUserMailboxStorage umuserMailboxAccessor = InterServerMailboxAccessor.GetUMUserMailboxAccessor(aduser, false))
					{
						pininfo = umuserMailboxAccessor.GetUMPin();
					}
					goto IL_84;
				}
				catch (LocalizedException ex)
				{
					throw new InvalidOperationForGetUMMailboxPinException(Strings.GetPINInfoError(aduser.PrimarySmtpAddress.ToString(), ex.LocalizedString), ex);
				}
				goto IL_64;
				IL_84:
				return new UMMailboxPin(aduser, pininfo.PinExpired, pininfo.LockedOut, pininfo.FirstTimeUser, base.NeedSuppressingPiiData);
			}
			IL_64:
			throw new InvalidOperationForGetUMMailboxPinException(Strings.InvalidUMUserName(aduser.PrimarySmtpAddress.ToString()));
		}

		// Token: 0x06008118 RID: 33048 RVA: 0x002104F4 File Offset: 0x0020E6F4
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.NetCredential, sessionSettings, ConfigScopes.TenantSubTree, 236, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\GetUMMailboxpin.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x06008119 RID: 33049 RVA: 0x002105A8 File Offset: 0x0020E7A8
		private void WriteWarningOrError(LocalizedString message)
		{
			if (this.IgnoreErrors.IsPresent)
			{
				this.WriteWarning(message);
				return;
			}
			base.WriteError(new LocalizedException(message), ErrorCategory.InvalidArgument, this.userObject);
		}

		// Token: 0x04003F1B RID: 16155
		private bool matchFound;

		// Token: 0x04003F1C RID: 16156
		private ADUser userObject;
	}
}
