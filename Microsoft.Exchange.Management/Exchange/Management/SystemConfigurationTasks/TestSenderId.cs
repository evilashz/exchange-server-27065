using System;
using System.Management.Automation;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.SenderId;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A3A RID: 2618
	[Cmdlet("Test", "SenderId", SupportsShouldProcess = true)]
	public sealed class TestSenderId : Task
	{
		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x06005D6F RID: 23919 RVA: 0x001899C4 File Offset: 0x00187BC4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestSenderId(this.IPAddress.ToString(), this.PurportedResponsibleDomain.ToString());
			}
		}

		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x06005D71 RID: 23921 RVA: 0x001899E9 File Offset: 0x00187BE9
		// (set) Token: 0x06005D72 RID: 23922 RVA: 0x00189A00 File Offset: 0x00187C00
		[Parameter]
		public Fqdn DomainController
		{
			get
			{
				return (Fqdn)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x06005D73 RID: 23923 RVA: 0x00189A13 File Offset: 0x00187C13
		// (set) Token: 0x06005D74 RID: 23924 RVA: 0x00189A1B File Offset: 0x00187C1B
		[Parameter(Mandatory = true)]
		public IPAddress IPAddress
		{
			get
			{
				return this.ipAddress;
			}
			set
			{
				this.ipAddress = value;
			}
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x06005D75 RID: 23925 RVA: 0x00189A24 File Offset: 0x00187C24
		// (set) Token: 0x06005D76 RID: 23926 RVA: 0x00189A2C File Offset: 0x00187C2C
		[Parameter(Mandatory = true)]
		public SmtpDomain PurportedResponsibleDomain
		{
			get
			{
				return this.purportedResponsibleDomain;
			}
			set
			{
				this.purportedResponsibleDomain = value;
			}
		}

		// Token: 0x17001C08 RID: 7176
		// (get) Token: 0x06005D77 RID: 23927 RVA: 0x00189A35 File Offset: 0x00187C35
		// (set) Token: 0x06005D78 RID: 23928 RVA: 0x00189A3D File Offset: 0x00187C3D
		[Parameter(Mandatory = false)]
		public string HelloDomain
		{
			get
			{
				return this.helloDomain;
			}
			set
			{
				this.helloDomain = value;
			}
		}

		// Token: 0x17001C09 RID: 7177
		// (get) Token: 0x06005D79 RID: 23929 RVA: 0x00189A46 File Offset: 0x00187C46
		// (set) Token: 0x06005D7A RID: 23930 RVA: 0x00189A5D File Offset: 0x00187C5D
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
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

		// Token: 0x06005D7B RID: 23931 RVA: 0x00189A70 File Offset: 0x00187C70
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				base.GetType().FullName
			});
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 118, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageHygiene\\Diagnostics\\TestSenderId.cs");
			if (this.Server == null)
			{
				try
				{
					this.serverObject = topologyConfigurationSession.ReadLocalServer();
					goto IL_A6;
				}
				catch (TransientException exception)
				{
					this.WriteError(exception, ErrorCategory.ResourceUnavailable, null, false);
					return;
				}
			}
			this.serverObject = topologyConfigurationSession.FindServerByName(this.Server.ToString());
			if (this.serverObject != null)
			{
				goto IL_A6;
			}
			this.WriteError(new LocalizedException(Strings.ErrorServerNotFound(this.Server.ToString())), ErrorCategory.InvalidOperation, null, false);
			return;
			IL_A6:
			if (this.serverObject == null || (!this.serverObject.IsHubTransportServer && !this.serverObject.IsEdgeServer))
			{
				this.WriteError(new LocalizedException(Strings.ErrorInvalidServerRole((this.serverObject != null) ? this.serverObject.Name : Environment.MachineName)), ErrorCategory.InvalidOperation, this.serverObject, false);
				return;
			}
			Dns andInitializeDns = Provider.GetAndInitializeDns(this.serverObject);
			Util.AsyncDns.SetDns(andInitializeDns);
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x00189BA8 File Offset: 0x00187DA8
		protected override void InternalProcessRecord()
		{
			RoutingAddress purportedResponsibleAddress = new RoutingAddress("postmaster", this.purportedResponsibleDomain.Domain);
			this.validator = new SenderIdValidator(new TestSenderId.TestServer());
			this.resetEvent = new AutoResetEvent(false);
			this.validator.BeginCheckHost(this.ipAddress, purportedResponsibleAddress, (this.helloDomain != null) ? this.helloDomain : string.Empty, true, new AsyncCallback(this.CheckHostCallback), null);
			this.resetEvent.WaitOne();
			base.WriteObject(this.result);
		}

		// Token: 0x06005D7D RID: 23933 RVA: 0x00189C36 File Offset: 0x00187E36
		private void CheckHostCallback(IAsyncResult ar)
		{
			this.result = this.validator.EndCheckHost(ar);
			this.resetEvent.Set();
		}

		// Token: 0x040034B7 RID: 13495
		private IPAddress ipAddress;

		// Token: 0x040034B8 RID: 13496
		private SmtpDomain purportedResponsibleDomain;

		// Token: 0x040034B9 RID: 13497
		private string helloDomain;

		// Token: 0x040034BA RID: 13498
		private SenderIdValidator validator;

		// Token: 0x040034BB RID: 13499
		private AutoResetEvent resetEvent;

		// Token: 0x040034BC RID: 13500
		private SenderIdResult result;

		// Token: 0x040034BD RID: 13501
		private Server serverObject;

		// Token: 0x02000A3B RID: 2619
		private class TestServer : SmtpServer
		{
			// Token: 0x17001C0A RID: 7178
			// (get) Token: 0x06005D7E RID: 23934 RVA: 0x00189C56 File Offset: 0x00187E56
			public override string Name
			{
				get
				{
					return "TestServer";
				}
			}

			// Token: 0x17001C0B RID: 7179
			// (get) Token: 0x06005D7F RID: 23935 RVA: 0x00189C5D File Offset: 0x00187E5D
			public override Version Version
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17001C0C RID: 7180
			// (get) Token: 0x06005D80 RID: 23936 RVA: 0x00189C64 File Offset: 0x00187E64
			public override IPPermission IPPermission
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17001C0D RID: 7181
			// (get) Token: 0x06005D81 RID: 23937 RVA: 0x00189C6B File Offset: 0x00187E6B
			public override AcceptedDomainCollection AcceptedDomains
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17001C0E RID: 7182
			// (get) Token: 0x06005D82 RID: 23938 RVA: 0x00189C72 File Offset: 0x00187E72
			public override RemoteDomainCollection RemoteDomains
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17001C0F RID: 7183
			// (get) Token: 0x06005D83 RID: 23939 RVA: 0x00189C79 File Offset: 0x00187E79
			public override AddressBook AddressBook
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06005D84 RID: 23940 RVA: 0x00189C80 File Offset: 0x00187E80
			public override void SubmitMessage(EmailMessage message)
			{
				throw new NotImplementedException();
			}
		}
	}
}
