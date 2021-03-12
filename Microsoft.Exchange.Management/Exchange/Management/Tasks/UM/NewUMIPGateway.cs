using System;
using System.Management.Automation;
using System.Net.Sockets;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D34 RID: 3380
	[Cmdlet("New", "UMIPGateway", SupportsShouldProcess = true)]
	public sealed class NewUMIPGateway : NewMultitenancySystemConfigurationObjectTask<UMIPGateway>
	{
		// Token: 0x1700283E RID: 10302
		// (get) Token: 0x06008192 RID: 33170 RVA: 0x00211B71 File Offset: 0x0020FD71
		// (set) Token: 0x06008193 RID: 33171 RVA: 0x00211B7E File Offset: 0x0020FD7E
		[Parameter(Mandatory = true)]
		public UMSmartHost Address
		{
			get
			{
				return this.DataObject.Address;
			}
			set
			{
				this.DataObject.Address = value;
			}
		}

		// Token: 0x1700283F RID: 10303
		// (get) Token: 0x06008194 RID: 33172 RVA: 0x00211B8C File Offset: 0x0020FD8C
		// (set) Token: 0x06008195 RID: 33173 RVA: 0x00211B99 File Offset: 0x0020FD99
		[Parameter(Mandatory = false)]
		public IPAddressFamily IPAddressFamily
		{
			get
			{
				return this.DataObject.IPAddressFamily;
			}
			set
			{
				this.DataObject.IPAddressFamily = value;
			}
		}

		// Token: 0x17002840 RID: 10304
		// (get) Token: 0x06008196 RID: 33174 RVA: 0x00211BA7 File Offset: 0x0020FDA7
		// (set) Token: 0x06008197 RID: 33175 RVA: 0x00211BBE File Offset: 0x0020FDBE
		[Parameter(Mandatory = false)]
		public UMDialPlanIdParameter UMDialPlan
		{
			get
			{
				return (UMDialPlanIdParameter)base.Fields["UMDialPlan"];
			}
			set
			{
				base.Fields["UMDialPlan"] = value;
			}
		}

		// Token: 0x17002841 RID: 10305
		// (get) Token: 0x06008198 RID: 33176 RVA: 0x00211BD1 File Offset: 0x0020FDD1
		// (set) Token: 0x06008199 RID: 33177 RVA: 0x00211BDE File Offset: 0x0020FDDE
		[Parameter(Mandatory = false)]
		public UMGlobalCallRoutingScheme GlobalCallRoutingScheme
		{
			get
			{
				return this.DataObject.GlobalCallRoutingScheme;
			}
			set
			{
				this.DataObject.GlobalCallRoutingScheme = value;
			}
		}

		// Token: 0x17002842 RID: 10306
		// (get) Token: 0x0600819A RID: 33178 RVA: 0x00211BEC File Offset: 0x0020FDEC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewUMIPGateway(base.Name.ToString(), this.Address.ToString());
			}
		}

		// Token: 0x0600819B RID: 33179 RVA: 0x00211C0C File Offset: 0x0020FE0C
		internal static LocalizedException ValidateFQDNInTenantAcceptedDomain(UMIPGateway gateway, IConfigurationSession session)
		{
			if (!CommonConstants.UseDataCenterCallRouting)
			{
				return null;
			}
			if (gateway == null || gateway.Address == null)
			{
				throw new ArgumentNullException("gateway");
			}
			if (gateway.GlobalCallRoutingScheme == UMGlobalCallRoutingScheme.E164)
			{
				return null;
			}
			string text = gateway.Address.ToString();
			QueryFilter filter = new NotFilter(new BitMaskAndFilter(AcceptedDomainSchema.AcceptedDomainFlags, 1UL));
			ADPagedReader<AcceptedDomain> adpagedReader = session.FindPaged<AcceptedDomain>(session.GetOrgContainerId(), QueryScope.SubTree, filter, null, 0);
			AcceptedDomain[] array = adpagedReader.ReadAllPages();
			bool flag = false;
			foreach (AcceptedDomain acceptedDomain in array)
			{
				string domain = acceptedDomain.DomainName.Domain;
				if (text.EndsWith(domain, StringComparison.OrdinalIgnoreCase))
				{
					string text2 = text.Substring(0, text.Length - domain.Length);
					if (text2.Length == 0)
					{
						flag = true;
						break;
					}
					int num = text2.IndexOf('.');
					if (num == text2.Length - 1)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return new GatewayFqdnNotInAcceptedDomainException();
			}
			return null;
		}

		// Token: 0x0600819C RID: 33180 RVA: 0x00211D00 File Offset: 0x0020FF00
		internal static LocalizedException ValidateIPAddressFamily(UMIPGateway gateway)
		{
			if (gateway.Address.IsIPAddress)
			{
				if (gateway.IsModified(UMIPGatewaySchema.IPAddressFamily) || gateway.IsModified(UMIPGatewaySchema.IPAddressFamily))
				{
					if ((gateway.Address.Address.AddressFamily == AddressFamily.InterNetwork && gateway.IPAddressFamily != IPAddressFamily.IPv4Only) || (gateway.Address.Address.AddressFamily == AddressFamily.InterNetworkV6 && gateway.IPAddressFamily != IPAddressFamily.IPv6Only))
					{
						return new GatewayIPAddressFamilyInconsistentException();
					}
				}
				else
				{
					IPAddressFamily ipaddressFamily = (gateway.Address.Address.AddressFamily == AddressFamily.InterNetworkV6) ? IPAddressFamily.IPv6Only : IPAddressFamily.IPv4Only;
					gateway.IPAddressFamily = ipaddressFamily;
				}
			}
			return null;
		}

		// Token: 0x0600819D RID: 33181 RVA: 0x00211D94 File Offset: 0x0020FF94
		protected override IConfigurable PrepareDataObject()
		{
			UMIPGateway umipgateway = (UMIPGateway)base.PrepareDataObject();
			umipgateway.SetId((IConfigurationSession)base.DataSession, base.Name);
			return umipgateway;
		}

		// Token: 0x0600819E RID: 33182 RVA: 0x00211DC8 File Offset: 0x0020FFC8
		protected override void InternalValidate()
		{
			base.InternalValidate();
			TaskLogger.LogEnter();
			if (!base.HasErrors)
			{
				if (CommonConstants.UseDataCenterCallRouting && this.DataObject.Address.IsIPAddress && this.DataObject.GlobalCallRoutingScheme != UMGlobalCallRoutingScheme.E164)
				{
					base.WriteError(new GatewayAddressRequiresFqdnException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
				LocalizedException ex = NewUMIPGateway.ValidateFQDNInTenantAcceptedDomain(this.DataObject, this.ConfigurationSession);
				if (ex != null)
				{
					base.WriteError(ex, ErrorCategory.InvalidOperation, this.DataObject);
				}
				string text = this.DataObject.Address.ToString();
				this.CheckAndWriteError(new IPGatewayAlreadyExistsException(text), text);
				if (this.UMDialPlan != null)
				{
					IConfigurationSession session = (IConfigurationSession)base.DataSession;
					this.dialPlan = (UMDialPlan)base.GetDataObject<UMDialPlan>(this.UMDialPlan, session, null, new LocalizedString?(Strings.NonExistantDialPlan(this.UMDialPlan.ToString())), new LocalizedString?(Strings.MultipleDialplansWithSameId(this.UMDialPlan.ToString())));
					if (this.dialPlan.URIType == UMUriType.SipName && !VariantConfiguration.InvariantNoFlightingSnapshot.UM.HuntGroupCreationForSipDialplans.Enabled)
					{
						base.WriteError(new CannotCreateGatewayForHostedSipDialPlanException(), ErrorCategory.InvalidOperation, this.DataObject);
					}
				}
				if (!this.DataObject.IsModified(UMIPGatewaySchema.GlobalCallRoutingScheme))
				{
					if (CommonConstants.UseDataCenterCallRouting)
					{
						this.GlobalCallRoutingScheme = UMGlobalCallRoutingScheme.GatewayGuid;
					}
					else
					{
						this.GlobalCallRoutingScheme = UMGlobalCallRoutingScheme.None;
					}
				}
				LocalizedException ex2 = NewUMIPGateway.ValidateIPAddressFamily(this.DataObject);
				if (ex2 != null)
				{
					base.WriteError(ex2, ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600819F RID: 33183 RVA: 0x00211F44 File Offset: 0x00210144
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.CreateParentContainerIfNeeded(this.DataObject);
			base.WriteVerbose(Strings.AttemptingToCreateIPGateway);
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				if (this.dialPlan != null)
				{
					this.CreateHuntgroup(this.dialPlan);
				}
				base.WriteResult();
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_NewIPGatewayCreated, null, new object[]
				{
					base.Name,
					this.DataObject.Address.ToString()
				});
				if (this.GlobalCallRoutingScheme == UMGlobalCallRoutingScheme.GatewayGuid)
				{
					this.WriteWarning(Strings.ConfigureGatewayToForwardCallsMsg);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060081A0 RID: 33184 RVA: 0x00211FE3 File Offset: 0x002101E3
		protected override void WriteResult()
		{
		}

		// Token: 0x060081A1 RID: 33185 RVA: 0x00211FE8 File Offset: 0x002101E8
		private void CreateHuntgroup(UMDialPlan dp)
		{
			UMHuntGroup umhuntGroup = new UMHuntGroup();
			umhuntGroup.UMDialPlan = dp.Id;
			AdName adName = new AdName("CN", Strings.DefaultUMHuntGroupName.ToString());
			ADObjectId descendantId = this.DataObject.Id.GetDescendantId(new ADObjectId(adName.ToString(), Guid.Empty));
			base.WriteVerbose(Strings.AttemptingToCreateHuntgroup);
			umhuntGroup.SetId(descendantId);
			if (base.CurrentOrganizationId != null)
			{
				umhuntGroup.OrganizationId = base.CurrentOrganizationId;
			}
			else
			{
				umhuntGroup.OrganizationId = base.ExecutingUserOrganizationId;
			}
			base.DataSession.Save(umhuntGroup);
		}

		// Token: 0x060081A2 RID: 33186 RVA: 0x0021208C File Offset: 0x0021028C
		private void CheckAndWriteError(LocalizedException ex, string addr)
		{
			UMIPGateway[] array = Utility.FindGatewayByIPAddress(addr, this.ConfigurationSession);
			if (array != null && array.Length > 0)
			{
				base.WriteError(ex, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x04003F29 RID: 16169
		private UMDialPlan dialPlan;
	}
}
