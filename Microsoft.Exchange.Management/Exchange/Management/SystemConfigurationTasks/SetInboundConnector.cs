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
	// Token: 0x02000B4D RID: 2893
	[Cmdlet("Set", "InboundConnector", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class SetInboundConnector : SetSystemConfigurationObjectTask<InboundConnectorIdParameter, TenantInboundConnector>
	{
		// Token: 0x17002057 RID: 8279
		// (get) Token: 0x060068DB RID: 26843 RVA: 0x001AFE76 File Offset: 0x001AE076
		// (set) Token: 0x060068DC RID: 26844 RVA: 0x001AFEA1 File Offset: 0x001AE0A1
		[Parameter(Mandatory = false)]
		public bool BypassValidation
		{
			get
			{
				return base.Fields.Contains("BypassValidation") && (bool)base.Fields["BypassValidation"];
			}
			set
			{
				base.Fields["BypassValidation"] = value;
			}
		}

		// Token: 0x17002058 RID: 8280
		// (get) Token: 0x060068DD RID: 26845 RVA: 0x001AFEB9 File Offset: 0x001AE0B9
		// (set) Token: 0x060068DE RID: 26846 RVA: 0x001AFED0 File Offset: 0x001AE0D0
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<AcceptedDomainIdParameter> AssociatedAcceptedDomains
		{
			get
			{
				return (MultiValuedProperty<AcceptedDomainIdParameter>)base.Fields["AssociatedAcceptedDomains"];
			}
			set
			{
				base.Fields["AssociatedAcceptedDomains"] = value;
			}
		}

		// Token: 0x17002059 RID: 8281
		// (get) Token: 0x060068DF RID: 26847 RVA: 0x001AFEE3 File Offset: 0x001AE0E3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetInboundConnector(this.Identity.ToString());
			}
		}

		// Token: 0x060068E0 RID: 26848 RVA: 0x001AFEF8 File Offset: 0x001AE0F8
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.Fields.IsModified("AssociatedAcceptedDomains"))
			{
				NewInboundConnector.ValidateAssociatedAcceptedDomains(this.AssociatedAcceptedDomains, base.DataSession, this.DataObject, this.RootId, this, new Func<IIdentityParameter, IConfigDataProvider, ObjectId, LocalizedString?, LocalizedString?, IConfigurable>(base.GetDataObject<AcceptedDomain>));
			}
			if (this.DataObject.SenderDomains == null)
			{
				base.WriteError(new LocalizedException(new LocalizedString("Sender Domain cannot be null.")), ErrorCategory.InvalidArgument, null);
			}
			bool flag = false;
			if (this.DataObject.SenderIPAddresses != null && this.DataObject.Enabled)
			{
				flag = true;
				NewInboundConnector.ValidateSenderIPAddresses(this.DataObject.SenderIPAddresses, this, this.BypassValidation);
				NewInboundConnector.CheckSenderIpAddressesOverlap(base.DataSession, this.DataObject, this);
			}
			if (this.DataObject.ConnectorType == TenantConnectorType.OnPremises)
			{
				bool flag2 = flag || this.DataObject.IsChanged(TenantInboundConnectorSchema.ConnectorType);
				bool flag3 = this.DataObject.IsChanged(TenantInboundConnectorSchema.ConnectorType) || this.DataObject.IsChanged(TenantInboundConnectorSchema.TlsSenderCertificateName);
				if ((flag2 || flag3) && !this.BypassValidation)
				{
					MultiValuedProperty<IPRange> ffoDCIPs;
					MultiValuedProperty<SmtpX509IdentifierEx> ffoFDSmtpCerts;
					MultiValuedProperty<ServiceProviderSettings> serviceProviders;
					if (!HygieneDCSettings.GetSettings(out ffoDCIPs, out ffoFDSmtpCerts, out serviceProviders))
					{
						base.WriteError(new ConnectorValidationFailedException(), ErrorCategory.ConnectionError, null);
					}
					if (flag2)
					{
						NewInboundConnector.ValidateSenderIPAddressRestrictions(this.DataObject.SenderIPAddresses, ffoDCIPs, serviceProviders, this);
					}
					if (flag3)
					{
						NewInboundConnector.ValidateTlsSenderCertificateRestrictions(this.DataObject.TlsSenderCertificateName, ffoFDSmtpCerts, serviceProviders, this);
					}
				}
			}
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x001B0064 File Offset: 0x001AE264
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			ADObject adobject = dataObject as ADObject;
			if (adobject != null)
			{
				this.dualWriter = new FfoDualWriter(adobject.Name);
			}
			base.StampChangesOn(dataObject);
		}

		// Token: 0x060068E2 RID: 26850 RVA: 0x001B0093 File Offset: 0x001AE293
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			this.dualWriter.Save<TenantInboundConnector>(this, this.DataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x04003698 RID: 13976
		private const string AssociatedAcceptedDomainsField = "AssociatedAcceptedDomains";

		// Token: 0x04003699 RID: 13977
		private FfoDualWriter dualWriter;
	}
}
