using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x02000013 RID: 19
	public abstract class AgentBaseTask : DataAccessTask<Server>
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003BFC File Offset: 0x00001DFC
		protected Server GetServerObject()
		{
			Server result = null;
			try
			{
				result = ((ITopologyConfigurationSession)base.DataSession).FindLocalServer();
			}
			catch (LocalServerNotFoundException)
			{
				this.ThrowRoleRestrictionError();
			}
			return result;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003C38 File Offset: 0x00001E38
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003C40 File Offset: 0x00001E40
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003C49 File Offset: 0x00001E49
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003C6A File Offset: 0x00001E6A
		[Parameter(Mandatory = false)]
		public TransportService TransportService
		{
			get
			{
				return (TransportService)(base.Fields["TransportService"] ?? TransportService.Hub);
			}
			set
			{
				base.Fields["TransportService"] = value;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003C82 File Offset: 0x00001E82
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 101, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\Agents\\AgentBaseTask.cs");
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003CAC File Offset: 0x00001EAC
		protected override void InternalProcessRecord()
		{
			this.EnsureTransportServiceIsSupported();
			this.EnsureMExConfigLoaded();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003CBA File Offset: 0x00001EBA
		private void EnsureTransportServiceIsSupported()
		{
			if (this.TransportService != TransportService.Hub && this.TransportService != TransportService.Edge && this.TransportService != TransportService.FrontEnd)
			{
				base.ThrowTerminatingError(new LocalizedException(AgentStrings.TransportServiceNotSupported(this.TransportService.ToString())), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003CF8 File Offset: 0x00001EF8
		private void EnsureMExConfigLoaded()
		{
			if (this.mexConfig != null)
			{
				return;
			}
			Server serverObject = this.GetServerObject();
			if (serverObject == null || (!serverObject.IsEdgeServer && !serverObject.IsHubTransportServer && !serverObject.IsFrontendTransportServer))
			{
				this.ThrowRoleRestrictionError();
			}
			this.SetMExConfigPath();
			ProcessTransportRole transportProcessRole = ProcessTransportRole.Hub;
			if (serverObject.IsEdgeServer)
			{
				transportProcessRole = ProcessTransportRole.Edge;
			}
			else if (!serverObject.IsHubTransportServer && serverObject.IsFrontendTransportServer)
			{
				transportProcessRole = ProcessTransportRole.FrontEnd;
			}
			this.mexConfig = new MExConfiguration(transportProcessRole, ConfigurationContext.Setup.InstallPath);
			try
			{
				this.mexConfig.Load(this.mexConfigPath);
			}
			catch (ExchangeConfigurationException ex)
			{
				if (ex.InnerException is FileNotFoundException)
				{
					if (!(this is InstallTransportAgent))
					{
						base.ThrowTerminatingError(ex, ErrorCategory.ReadError, null);
					}
					this.missingConfigFile = true;
				}
				else
				{
					base.ThrowTerminatingError(ex, ErrorCategory.ReadError, null);
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003DC8 File Offset: 0x00001FC8
		private void ThrowRoleRestrictionError()
		{
			base.ThrowTerminatingError(new LocalizedException(AgentStrings.TransportAgentTasksOnlyOnFewRoles), ErrorCategory.InvalidOperation, null);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003DDC File Offset: 0x00001FDC
		internal string ValidateAndNormalizeAgentIdentity(string identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				base.WriteError(new ArgumentNullException("Identity", AgentStrings.NoIdentityArgument), ErrorCategory.InvalidArgument, null);
			}
			string text = identity.TrimStart(new char[]
			{
				'"',
				' '
			});
			text = text.TrimEnd(new char[]
			{
				'"',
				' '
			});
			if (string.IsNullOrEmpty(text))
			{
				base.WriteError(new ArgumentNullException("Identity", AgentStrings.NoIdentityArgument), ErrorCategory.InvalidArgument, null);
			}
			string text2 = "\",*";
			char[] anyOf = text2.ToCharArray();
			int num = text.IndexOfAny(anyOf, 0);
			if (num > -1)
			{
				base.WriteError(new ArgumentException(AgentStrings.InvalidIdentity, "Identity"), ErrorCategory.InvalidArgument, null);
			}
			return text;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003EA4 File Offset: 0x000020A4
		internal bool AgentExists(string name)
		{
			if (this.mexConfig != null)
			{
				foreach (AgentInfo agentInfo in this.MExConfiguration.AgentList)
				{
					if (string.Compare(agentInfo.AgentName, name, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003F10 File Offset: 0x00002110
		internal void Save()
		{
			if (this.mexConfig != null)
			{
				try
				{
					this.mexConfig.Save(this.mexConfigPath);
				}
				catch (ExchangeConfigurationException exception)
				{
					base.WriteError(exception, ErrorCategory.WriteError, null);
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003F58 File Offset: 0x00002158
		internal void SetAgentEnabled(string identity, bool enabled)
		{
			string strB = this.ValidateAndNormalizeAgentIdentity(identity);
			if (this.mexConfig != null)
			{
				foreach (AgentInfo agentInfo in this.mexConfig.GetPublicAgentList())
				{
					if (string.Compare(agentInfo.AgentName, strB, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						agentInfo.Enabled = enabled;
						return;
					}
				}
			}
			base.WriteError(new ArgumentException(AgentStrings.AgentNotFound(identity), "Identity"), ErrorCategory.InvalidArgument, null);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003FF0 File Offset: 0x000021F0
		private void SetMExConfigPath()
		{
			switch (this.TransportService)
			{
			case TransportService.Hub:
			case TransportService.Edge:
				this.mexConfigPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\Shared\\agents.config");
				return;
			case TransportService.FrontEnd:
				this.mexConfigPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\Shared\\fetagents.config");
				return;
			default:
				throw new InvalidOperationException(string.Format("TransportService is set to a value that is not supported: {0}", this.TransportService));
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004060 File Offset: 0x00002260
		internal string GetTransportServiceName()
		{
			switch (this.TransportService)
			{
			case TransportService.Hub:
			case TransportService.Edge:
				return "MSExchangeTransport";
			case TransportService.FrontEnd:
				return "MSExchangeFrontEndTransport";
			default:
				throw new InvalidOperationException(string.Format("TransportService is set to a value that is not supported: {0}", this.TransportService));
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000040AE File Offset: 0x000022AE
		internal MExConfiguration MExConfiguration
		{
			get
			{
				return this.mexConfig;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000040B6 File Offset: 0x000022B6
		internal string MExConfigPath
		{
			get
			{
				return this.mexConfigPath;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000040BE File Offset: 0x000022BE
		internal bool MissingConfigFile
		{
			get
			{
				return this.missingConfigFile;
			}
		}

		// Token: 0x04000027 RID: 39
		private const string MSExchangeTransportServiceName = "MSExchangeTransport";

		// Token: 0x04000028 RID: 40
		private const string MSExchangeFrontEndTransportServiceName = "MSExchangeFrontEndTransport";

		// Token: 0x04000029 RID: 41
		private MExConfiguration mexConfig;

		// Token: 0x0400002A RID: 42
		private bool missingConfigFile;

		// Token: 0x0400002B RID: 43
		private string mexConfigPath;
	}
}
