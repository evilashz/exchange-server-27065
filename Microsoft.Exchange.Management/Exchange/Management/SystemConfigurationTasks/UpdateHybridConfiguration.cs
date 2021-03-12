using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Hybrid;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008DF RID: 2271
	[Cmdlet("Update", "HybridConfiguration", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
	public sealed class UpdateHybridConfiguration : SingletonSystemConfigurationObjectActionTask<HybridConfiguration>, IUserInterface
	{
		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x06005087 RID: 20615 RVA: 0x00150AFE File Offset: 0x0014ECFE
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return HybridStrings.ConfirmationMessageUpdateHybridConfiguration;
			}
		}

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x06005088 RID: 20616 RVA: 0x00150B05 File Offset: 0x0014ED05
		// (set) Token: 0x06005089 RID: 20617 RVA: 0x00150B1C File Offset: 0x0014ED1C
		[Parameter(Mandatory = true)]
		public PSCredential OnPremisesCredentials
		{
			get
			{
				return (PSCredential)base.Fields["OnPremCredentials"];
			}
			set
			{
				base.Fields["OnPremCredentials"] = value;
			}
		}

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x0600508A RID: 20618 RVA: 0x00150B2F File Offset: 0x0014ED2F
		// (set) Token: 0x0600508B RID: 20619 RVA: 0x00150B46 File Offset: 0x0014ED46
		[Parameter(Mandatory = true)]
		public PSCredential TenantCredentials
		{
			get
			{
				return (PSCredential)base.Fields["TenantCredentials"];
			}
			set
			{
				base.Fields["TenantCredentials"] = value;
			}
		}

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x0600508C RID: 20620 RVA: 0x00150B59 File Offset: 0x0014ED59
		// (set) Token: 0x0600508D RID: 20621 RVA: 0x00150B7F File Offset: 0x0014ED7F
		[Parameter]
		public SwitchParameter ForceUpgrade
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceUpgrade"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForceUpgrade"] = value;
			}
		}

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x0600508E RID: 20622 RVA: 0x00150B97 File Offset: 0x0014ED97
		// (set) Token: 0x0600508F RID: 20623 RVA: 0x00150BBD File Offset: 0x0014EDBD
		[Parameter]
		public SwitchParameter SuppressOAuthWarning
		{
			get
			{
				return (SwitchParameter)(base.Fields["SuppressOAuthWarning"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SuppressOAuthWarning"] = value;
			}
		}

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06005090 RID: 20624 RVA: 0x00150BD5 File Offset: 0x0014EDD5
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x00150C70 File Offset: 0x0014EE70
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.WriteWarning(HybridStrings.WarningRedirectCU10HybridStandaloneConfiguration);
			base.InternalProcessRecord();
			Action<LocalizedString> writeVerbose = base.IsVerboseOn ? new Action<LocalizedString>(this.WriteVerbose) : null;
			using (ILogger logger = Logger.Create(writeVerbose))
			{
				IList<Tuple<string, string>> hybridConfigurationObjectValues = this.GetHybridConfigurationObjectValues();
				int maxNameLength = hybridConfigurationObjectValues.Max((Tuple<string, string> v) => v.Item1.Length);
				logger.LogInformation(string.Format("{0}\r\n{1}", "Hybrid Configuration Object", string.Join("\r\n", from v in hybridConfigurationObjectValues
				select string.Format("{0}{1} : {2}", v.Item1, new string(' ', maxNameLength - v.Item1.Length), v.Item2))));
				Func<IOnPremisesSession> createOnPremisesSessionFunc = () => new PowerShellOnPremisesSession(logger, Dns.GetHostName(), this.OnPremisesCredentials);
				Func<ITenantSession> createTenantSessionFunc = () => new PowerShellTenantSession(logger, Configuration.PowerShellEndpoint(this.DataObject.ServiceInstance), this.TenantCredentials);
				UpdateHybridConfigurationLogic.ProcessRecord(logger, this, this.DataObject, createOnPremisesSessionFunc, createTenantSessionFunc, new Action<Exception, ErrorCategory, object>(base.WriteError), this.ForceUpgrade, this.SuppressOAuthWarning);
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x00150DD8 File Offset: 0x0014EFD8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			SetHybridConfigurationLogic.Validate(this.DataObject, base.HasErrors, new Task.TaskErrorLoggingDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x00150E07 File Offset: 0x0014F007
		public new void WriteVerbose(LocalizedString text)
		{
			base.WriteVerbose(text);
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x00150E10 File Offset: 0x0014F010
		public new void WriteWarning(LocalizedString text)
		{
			base.WriteWarning(text);
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x00150E19 File Offset: 0x0014F019
		public void WriteProgessIndicator(LocalizedString activity, LocalizedString statusDescription, int percentCompleted)
		{
			base.WriteProgress(activity, statusDescription, percentCompleted);
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x00150E24 File Offset: 0x0014F024
		public new bool ShouldContinue(LocalizedString message)
		{
			return base.ShouldContinue(message);
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x00150E30 File Offset: 0x0014F030
		private IList<Tuple<string, string>> GetHybridConfigurationObjectValues()
		{
			HybridConfiguration dataObject = this.DataObject;
			return new List<Tuple<string, string>>
			{
				new Tuple<string, string>("Features", UpdateHybridConfiguration.ToString<HybridFeature>(dataObject.Features)),
				new Tuple<string, string>("Domains", UpdateHybridConfiguration.ToString<AutoDiscoverSmtpDomain>(dataObject.Domains)),
				new Tuple<string, string>("OnPremisesSmartHost", TaskCommon.ToStringOrNull(dataObject.OnPremisesSmartHost)),
				new Tuple<string, string>("ClientAccessServers", UpdateHybridConfiguration.ToString<ADObjectId>(dataObject.ClientAccessServers)),
				new Tuple<string, string>("EdgeTransportServers", UpdateHybridConfiguration.ToString<ADObjectId>(dataObject.EdgeTransportServers)),
				new Tuple<string, string>("ReceivingTransportServers", UpdateHybridConfiguration.ToString<ADObjectId>(dataObject.ReceivingTransportServers)),
				new Tuple<string, string>("SendingTransportServers", UpdateHybridConfiguration.ToString<ADObjectId>(dataObject.SendingTransportServers)),
				new Tuple<string, string>("TlsCertificateName", TaskCommon.ToStringOrNull(dataObject.TlsCertificateName)),
				new Tuple<string, string>("ServiceInstance", TaskCommon.ToStringOrNull(dataObject.ServiceInstance))
			};
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x00150F50 File Offset: 0x0014F150
		private static string ToString<T>(MultiValuedProperty<T> value)
		{
			Func<T, string> func = null;
			if (value == null)
			{
				return string.Empty;
			}
			string separator = ", ";
			if (func == null)
			{
				func = ((T v) => TaskCommon.ToStringOrNull(v));
			}
			return string.Join(separator, value.Select(func));
		}

		// Token: 0x04002F6B RID: 12139
		private const string HybridConfigurationObject = "Hybrid Configuration Object";

		// Token: 0x04002F6C RID: 12140
		private const string PropertyFeatures = "Features";

		// Token: 0x04002F6D RID: 12141
		private const string PropertyDomains = "Domains";

		// Token: 0x04002F6E RID: 12142
		private const string PropertyOnPremisesSmartHost = "OnPremisesSmartHost";

		// Token: 0x04002F6F RID: 12143
		private const string PropertyClientAccessServers = "ClientAccessServers";

		// Token: 0x04002F70 RID: 12144
		private const string PropertyEdgeTransportServers = "EdgeTransportServers";

		// Token: 0x04002F71 RID: 12145
		private const string PropertyReceivingTransportServers = "ReceivingTransportServers";

		// Token: 0x04002F72 RID: 12146
		private const string PropertySendingTransportServers = "SendingTransportServers";

		// Token: 0x04002F73 RID: 12147
		private const string PropertyTlsCertificateName = "TlsCertificateName";

		// Token: 0x04002F74 RID: 12148
		private const string PropertyServiceInstance = "ServiceInstance";
	}
}
