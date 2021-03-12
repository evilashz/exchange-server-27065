using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D43 RID: 3395
	[Cmdlet("Set", "UMService", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class SetUMServer : SetTopologySystemConfigurationObjectTask<UMServerIdParameter, UMServer, Server>
	{
		// Token: 0x1700286F RID: 10351
		// (get) Token: 0x06008227 RID: 33319 RVA: 0x0021421B File Offset: 0x0021241B
		// (set) Token: 0x06008228 RID: 33320 RVA: 0x00214232 File Offset: 0x00212432
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<UMDialPlanIdParameter> DialPlans
		{
			get
			{
				return (MultiValuedProperty<UMDialPlanIdParameter>)base.Fields["DialPlans"];
			}
			set
			{
				base.Fields["DialPlans"] = value;
			}
		}

		// Token: 0x17002870 RID: 10352
		// (get) Token: 0x06008229 RID: 33321 RVA: 0x00214245 File Offset: 0x00212445
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetUmServer(this.Identity.ToString());
			}
		}

		// Token: 0x0600822A RID: 33322 RVA: 0x00214258 File Offset: 0x00212458
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				UMServer umserver = new UMServer(this.DataObject);
				UMServer umserver2 = new UMServer((Server)this.DataObject.GetOriginalObject());
				if (this.DataObject.IsE15OrLater && base.Fields.IsModified(ServerSchema.Status))
				{
					base.WriteError(new StatusChangeException(this.DataObject.Name), ErrorCategory.InvalidOperation, null);
				}
				if (!this.DataObject.IsE15OrLater && base.Fields.IsModified("DialPlans"))
				{
					base.WriteError(new DialPlanChangeException(this.DataObject.Name), ErrorCategory.InvalidOperation, null);
				}
				if (this.DialPlans != null)
				{
					this.DataObject.DialPlans = base.ResolveIdParameterCollection<UMDialPlanIdParameter, UMDialPlan, ADObjectId>(this.DialPlans, base.DataSession, null, null, (ExchangeErrorCategory)0, new Func<IIdentityParameter, LocalizedString>(Strings.NonExistantDialPlan), new Func<IIdentityParameter, LocalizedString>(Strings.MultipleDialplansWithSameId), null, new Func<IConfigurable, IConfigurable>(this.ValidateDialPlan));
				}
				else if (base.Fields.IsModified("DialPlans") && (this.DataObject.DialPlans != null || this.DataObject.DialPlans.Count > 0))
				{
					this.DataObject.DialPlans.Clear();
				}
				if (this.DataObject.IsChanged(UMServerSchema.ExternalHostFqdn))
				{
					if (this.DataObject.ExternalHostFqdn != null && this.DataObject.ExternalHostFqdn.IsIPAddress)
					{
						base.WriteError(new LocalizedException(Strings.InvalidExternalHostFqdn), ErrorCategory.InvalidArgument, this.DataObject);
					}
					else
					{
						this.WriteWarning(Strings.ChangesTakeEffectAfterRestartingUmServer(Strings.ExternalHostFqdnChanges, this.DataObject.Name, string.Empty));
					}
				}
				if (this.DataObject.IsChanged(UMServerSchema.UMStartupMode))
				{
					switch (umserver.UMStartupMode)
					{
					case UMStartupMode.TCP:
						this.WriteWarning(Strings.ChangesTakeEffectAfterRestartingUmServer(Strings.UMStartupModeChanges, this.DataObject.Name, string.Empty));
						if (!string.IsNullOrEmpty(umserver.UMCertificateThumbprint))
						{
							umserver.UMCertificateThumbprint = null;
							this.WriteWarning(Strings.TransferFromTLStoTCPModeWarning);
						}
						break;
					case UMStartupMode.TLS:
					case UMStartupMode.Dual:
						this.WriteWarning(Strings.ChangesTakeEffectAfterRestartingUmServer(Strings.UMStartupModeChanges, this.DataObject.Name, Strings.ValidCertRequiredForUM));
						if (umserver2.UMStartupMode == UMStartupMode.TCP)
						{
							this.WriteWarning(Strings.TransferFromTCPtoTLSModeWarning);
						}
						break;
					default:
						throw new InvalidOperationException("Unknown value of UMStartupMode");
					}
				}
				if (this.DataObject.IsChanged(UMServerSchema.SipTcpListeningPort) || this.DataObject.IsChanged(UMServerSchema.SipTlsListeningPort))
				{
					if (umserver.SipTcpListeningPort == umserver.SipTlsListeningPort)
					{
						base.WriteError(new TcpAndTlsPortsCannotBeSameException(), ErrorCategory.InvalidArgument, this.DataObject);
					}
					this.WriteWarning(Strings.ChangesTakeEffectAfterRestartingUmServer(Strings.PortChanges, this.DataObject.Name, Strings.FirewallCorrectlyConfigured(this.DataObject.Name)));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600822B RID: 33323 RVA: 0x0021454C File Offset: 0x0021274C
		private IConfigurable ValidateDialPlan(IConfigurable configObject)
		{
			UMDialPlan umdialPlan = (UMDialPlan)configObject;
			if (this.DataObject.VersionNumber >= Server.E15MinVersion && umdialPlan.URIType != UMUriType.SipName)
			{
				base.WriteError(new CannotAddNonSipNameDialplanException(umdialPlan.ToString()), ErrorCategory.InvalidData, umdialPlan);
			}
			return umdialPlan;
		}
	}
}
