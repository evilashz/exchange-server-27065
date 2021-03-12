using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D3D RID: 3389
	[Cmdlet("Set", "UMCallRouterSettings", SupportsShouldProcess = true)]
	public sealed class SetUMCallRouterSettings : SetSingletonSystemConfigurationObjectTask<SIPFEServerConfiguration>
	{
		// Token: 0x17002858 RID: 10328
		// (get) Token: 0x060081E4 RID: 33252 RVA: 0x002130D7 File Offset: 0x002112D7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetUmCallRouterSettings;
			}
		}

		// Token: 0x17002859 RID: 10329
		// (get) Token: 0x060081E5 RID: 33253 RVA: 0x002130DE File Offset: 0x002112DE
		// (set) Token: 0x060081E6 RID: 33254 RVA: 0x002130F5 File Offset: 0x002112F5
		[Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x1700285A RID: 10330
		// (get) Token: 0x060081E7 RID: 33255 RVA: 0x00213108 File Offset: 0x00211308
		// (set) Token: 0x060081E8 RID: 33256 RVA: 0x0021311F File Offset: 0x0021131F
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

		// Token: 0x1700285B RID: 10331
		// (get) Token: 0x060081E9 RID: 33257 RVA: 0x00213134 File Offset: 0x00211334
		protected override ObjectId RootId
		{
			get
			{
				ServerIdParameter serverIdParameter = this.Server ?? ServerIdParameter.Parse(Environment.MachineName);
				Server server = (Server)base.GetDataObject<Server>(serverIdParameter, base.DataSession as IConfigurationSession, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
				this.serverName = server.Name;
				return SIPFEServerConfiguration.GetRootId(server);
			}
		}

		// Token: 0x060081EA RID: 33258 RVA: 0x002131A4 File Offset: 0x002113A4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (this.DialPlans != null)
				{
					this.DataObject.DialPlans = base.ResolveIdParameterCollection<UMDialPlanIdParameter, UMDialPlan, ADObjectId>(this.DialPlans, base.DataSession, null, null, (ExchangeErrorCategory)0, new Func<IIdentityParameter, LocalizedString>(Strings.NonExistantDialPlan), new Func<IIdentityParameter, LocalizedString>(Strings.MultipleDialplansWithSameId), null, new Func<IConfigurable, IConfigurable>(this.ValidateDialPlan));
				}
				else if (base.Fields.IsModified("DialPlans") && (this.DataObject.DialPlans != null || this.DataObject.DialPlans.Count > 0))
				{
					this.DataObject.DialPlans.Clear();
				}
				if (this.DataObject.IsChanged(SIPFEServerConfigurationSchema.ExternalHostFqdn))
				{
					if (this.DataObject.ExternalHostFqdn != null && this.DataObject.ExternalHostFqdn.IsIPAddress)
					{
						base.WriteError(new LocalizedException(Strings.InvalidExternalHostFqdn), ErrorCategory.InvalidArgument, this.DataObject);
					}
					else
					{
						this.WriteWarning(Strings.ChangesTakeEffectAfterRestartingUmCallRouterService(Strings.ExternalHostFqdnChanges, this.serverName, string.Empty));
					}
				}
				if (this.DataObject.IsChanged(SIPFEServerConfigurationSchema.UMStartupMode))
				{
					switch (this.DataObject.UMStartupMode)
					{
					case UMStartupMode.TCP:
						this.WriteWarning(Strings.ChangesTakeEffectAfterRestartingUmCallRouterService(Strings.UMStartupModeChanges, this.serverName, string.Empty));
						if (!string.IsNullOrEmpty(this.DataObject.UMCertificateThumbprint))
						{
							this.DataObject.UMCertificateThumbprint = null;
							this.WriteWarning(Strings.CallRouterTransferFromTLStoTCPModeWarning);
						}
						break;
					case UMStartupMode.TLS:
					case UMStartupMode.Dual:
						this.WriteWarning(Strings.ChangesTakeEffectAfterRestartingUmCallRouterService(Strings.UMStartupModeChanges, this.serverName, Strings.ValidCertRequiredForUMCallRouter));
						if (((SIPFEServerConfiguration)this.DataObject.GetOriginalObject()).UMStartupMode == UMStartupMode.TCP)
						{
							this.WriteWarning(Strings.TransferFromTCPtoTLSModeWarning);
						}
						break;
					default:
						throw new InvalidOperationException("Unknown value of UMStartupMode");
					}
				}
				if (this.DataObject.IsChanged(SIPFEServerConfigurationSchema.SipTcpListeningPort) || this.DataObject.IsChanged(SIPFEServerConfigurationSchema.SipTlsListeningPort))
				{
					if (this.DataObject.SipTcpListeningPort == this.DataObject.SipTlsListeningPort)
					{
						base.WriteError(new TcpAndTlsPortsCannotBeSameException(), ErrorCategory.InvalidArgument, this.DataObject);
					}
					this.WriteWarning(Strings.ChangesTakeEffectAfterRestartingUmCallRouterService(Strings.PortChanges, this.serverName, Strings.FirewallCorrectlyConfigured(this.serverName)));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060081EB RID: 33259 RVA: 0x00213418 File Offset: 0x00211618
		private IConfigurable ValidateDialPlan(IConfigurable configObject)
		{
			UMDialPlan umdialPlan = (UMDialPlan)configObject;
			if (umdialPlan.URIType != UMUriType.SipName)
			{
				this.WriteError(new CannotAddNonSipNameDialplanToCallRouterException(umdialPlan.ToString()), ErrorCategory.InvalidData, umdialPlan, false);
			}
			return umdialPlan;
		}

		// Token: 0x04003F2F RID: 16175
		private string serverName = string.Empty;
	}
}
