using System;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Monitoring;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200093C RID: 2364
	[Cmdlet("Set", "ServerComponentState", SupportsShouldProcess = true)]
	public sealed class SetServerComponentState : Task
	{
		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x06005410 RID: 21520 RVA: 0x0015B33A File Offset: 0x0015953A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetComponentState(this.Component, this.State.ToString());
			}
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x06005411 RID: 21521 RVA: 0x0015B357 File Offset: 0x00159557
		// (set) Token: 0x06005412 RID: 21522 RVA: 0x0015B35F File Offset: 0x0015955F
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public ServerIdParameter Identity
		{
			get
			{
				return this.serverId;
			}
			set
			{
				this.serverId = value;
			}
		}

		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x06005413 RID: 21523 RVA: 0x0015B368 File Offset: 0x00159568
		// (set) Token: 0x06005414 RID: 21524 RVA: 0x0015B37F File Offset: 0x0015957F
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public string Component
		{
			get
			{
				return (string)base.Fields["Component"];
			}
			set
			{
				base.Fields["Component"] = value;
			}
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x06005415 RID: 21525 RVA: 0x0015B392 File Offset: 0x00159592
		// (set) Token: 0x06005416 RID: 21526 RVA: 0x0015B3A9 File Offset: 0x001595A9
		[Parameter(Mandatory = true)]
		public ServiceState State
		{
			get
			{
				return (ServiceState)base.Fields["State"];
			}
			set
			{
				base.Fields["State"] = value;
			}
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x06005417 RID: 21527 RVA: 0x0015B3C1 File Offset: 0x001595C1
		// (set) Token: 0x06005418 RID: 21528 RVA: 0x0015B3D8 File Offset: 0x001595D8
		[Parameter(Mandatory = true)]
		public string Requester
		{
			get
			{
				return (string)base.Fields["Requester"];
			}
			set
			{
				base.Fields["Requester"] = value;
			}
		}

		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x06005419 RID: 21529 RVA: 0x0015B3EB File Offset: 0x001595EB
		// (set) Token: 0x0600541A RID: 21530 RVA: 0x0015B3F3 File Offset: 0x001595F3
		[Parameter(Mandatory = false)]
		public Fqdn DomainController
		{
			get
			{
				return this.domainController;
			}
			set
			{
				this.domainController = value;
			}
		}

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x0600541B RID: 21531 RVA: 0x0015B3FC File Offset: 0x001595FC
		// (set) Token: 0x0600541C RID: 21532 RVA: 0x0015B422 File Offset: 0x00159622
		[Parameter(Mandatory = false)]
		public SwitchParameter RemoteOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoteOnly"] ?? false);
			}
			set
			{
				base.Fields["RemoteOnly"] = value;
			}
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x0600541D RID: 21533 RVA: 0x0015B43A File Offset: 0x0015963A
		// (set) Token: 0x0600541E RID: 21534 RVA: 0x0015B460 File Offset: 0x00159660
		[Parameter(Mandatory = false)]
		public SwitchParameter LocalOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["LocalOnly"] ?? false);
			}
			set
			{
				base.Fields["LocalOnly"] = value;
			}
		}

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x0600541F RID: 21535 RVA: 0x0015B478 File Offset: 0x00159678
		// (set) Token: 0x06005420 RID: 21536 RVA: 0x0015B49A File Offset: 0x0015969A
		[Parameter(Mandatory = false)]
		public int TimeoutInSeconds
		{
			get
			{
				return (int)(base.Fields["TimeoutInSeconds"] ?? 120);
			}
			set
			{
				base.Fields["TimeoutInSeconds"] = value;
			}
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x0015B4B4 File Offset: 0x001596B4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.ProvisioningHandlers != null)
			{
				foreach (ProvisioningHandler provisioningHandler in base.ProvisioningHandlers)
				{
					provisioningHandler.Validate(this.serverObject);
				}
			}
			if (!ServerComponentStateManager.IsValidComponent(this.Component))
			{
				base.WriteError(new ArgumentException(Strings.ServerComponentStateInvalidComponentName(this.Component)), ErrorCategory.InvalidArgument, null);
			}
			if (this.LocalOnly)
			{
				ServerComponentEnum serverComponentEnum;
				Enum.TryParse<ServerComponentEnum>(this.Component, true, out serverComponentEnum);
				if (serverComponentEnum == ServerComponentEnum.Monitoring || serverComponentEnum == ServerComponentEnum.RecoveryActionsEnabled)
				{
					base.WriteError(new ArgumentException(Strings.ServerComponentStateNoLocalOnly(this.Component)), ErrorCategory.InvalidArgument, null);
				}
			}
			ServerComponentRequest serverComponentRequest;
			if (!Enum.TryParse<ServerComponentRequest>(this.Requester, true, out serverComponentRequest))
			{
				string allowedRequesters = string.Join(",", Enum.GetNames(typeof(ServerComponentRequest)));
				base.WriteError(new ArgumentException(Strings.ServerComponentStateInvalidRequester(this.Requester, allowedRequesters)), ErrorCategory.InvalidArgument, null);
			}
			if (this.LocalOnly && this.RemoteOnly)
			{
				base.WriteError(new ArgumentException(Strings.SetServerComponentStateInvalidLocalRemoteSwitch), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x0015B618 File Offset: 0x00159818
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			ADComputer adcomputer = null;
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 213, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ComponentStates\\SetServerComponentState.cs");
			string fqdn = this.serverId.Fqdn;
			string text = fqdn;
			int num = fqdn.IndexOf('.');
			if (num > 0)
			{
				text = fqdn.Substring(0, num);
			}
			Server server = topologyConfigurationSession.FindServerByName(text);
			if (server == null)
			{
				topologyConfigurationSession.UseConfigNC = false;
				topologyConfigurationSession.UseGlobalCatalog = true;
				adcomputer = topologyConfigurationSession.FindComputerByHostName(text);
				if (adcomputer == null)
				{
					base.WriteError(new ADServerNotFoundException(fqdn), ErrorCategory.InvalidArgument, null);
				}
			}
			if (!this.LocalOnly)
			{
				if (server != null)
				{
					server.ComponentStates = ServerComponentStates.UpdateRemoteState(server.ComponentStates, this.Requester, this.Component, this.State);
					topologyConfigurationSession.Save(server);
				}
				else
				{
					adcomputer.ComponentStates = ServerComponentStates.UpdateRemoteState(adcomputer.ComponentStates, this.Requester, this.Component, this.State);
					topologyConfigurationSession.Save(adcomputer);
				}
			}
			if (!this.RemoteOnly)
			{
				string serverFqdn = (server != null) ? server.Fqdn : adcomputer.DnsHostName;
				TimeSpan invokeTimeout = TimeSpan.FromSeconds((double)this.TimeoutInSeconds);
				Exception ex = null;
				try
				{
					InvokeWithTimeout.Invoke(delegate()
					{
						ServerComponentStates.UpdateLocalState(serverFqdn, this.Requester, this.Component, this.State);
					}, null, invokeTimeout, true, null);
				}
				catch (IOException ex2)
				{
					ex = ex2;
				}
				catch (UnauthorizedAccessException ex3)
				{
					ex = ex3;
				}
				catch (SecurityException ex4)
				{
					ex = ex4;
				}
				if (ex != null && this.LocalOnly)
				{
					base.WriteError(new ArgumentException(Strings.SetServerComponentStateServerUnreachable(serverFqdn, ex.Message)), ErrorCategory.ResourceUnavailable, null);
				}
			}
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x0015B800 File Offset: 0x00159A00
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is IOException;
		}

		// Token: 0x04003111 RID: 12561
		private ServerIdParameter serverId;

		// Token: 0x04003112 RID: 12562
		private Fqdn domainController;

		// Token: 0x04003113 RID: 12563
		private ServerComponentStatePresentationObject serverObject = new ServerComponentStatePresentationObject();
	}
}
