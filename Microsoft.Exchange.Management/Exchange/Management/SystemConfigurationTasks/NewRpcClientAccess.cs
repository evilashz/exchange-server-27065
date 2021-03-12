using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009A0 RID: 2464
	[Cmdlet("New", "RpcClientAccess", SupportsShouldProcess = true)]
	public sealed class NewRpcClientAccess : NewFixedNameSystemConfigurationObjectTask<ExchangeRpcClientAccess>
	{
		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x06005820 RID: 22560 RVA: 0x0016FEB8 File Offset: 0x0016E0B8
		// (set) Token: 0x06005821 RID: 22561 RVA: 0x0016FECF File Offset: 0x0016E0CF
		[Parameter(ValueFromPipeline = true, Mandatory = true)]
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

		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x06005822 RID: 22562 RVA: 0x0016FEE2 File Offset: 0x0016E0E2
		// (set) Token: 0x06005823 RID: 22563 RVA: 0x0016FEEF File Offset: 0x0016E0EF
		[Parameter(Mandatory = false)]
		public bool EncryptionRequired
		{
			get
			{
				return this.DataObject.EncryptionRequired;
			}
			set
			{
				this.DataObject.EncryptionRequired = value;
			}
		}

		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x06005824 RID: 22564 RVA: 0x0016FEFD File Offset: 0x0016E0FD
		// (set) Token: 0x06005825 RID: 22565 RVA: 0x0016FF0A File Offset: 0x0016E10A
		[Parameter(Mandatory = false)]
		public int MaximumConnections
		{
			get
			{
				return this.DataObject.MaximumConnections;
			}
			set
			{
				this.DataObject.MaximumConnections = value;
			}
		}

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x06005826 RID: 22566 RVA: 0x0016FF18 File Offset: 0x0016E118
		// (set) Token: 0x06005827 RID: 22567 RVA: 0x0016FF25 File Offset: 0x0016E125
		[Parameter(Mandatory = false)]
		public string BlockedClientVersions
		{
			get
			{
				return this.DataObject.BlockedClientVersions;
			}
			set
			{
				this.DataObject.BlockedClientVersions = value;
			}
		}

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x06005828 RID: 22568 RVA: 0x0016FF33 File Offset: 0x0016E133
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewRpcClientAccess(this.DataObject.Server.Name);
			}
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x0016FF4C File Offset: 0x0016E14C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ExchangeRpcClientAccess exchangeRpcClientAccess = (ExchangeRpcClientAccess)base.PrepareDataObject();
			this.server = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
			if (ExchangeRpcClientAccess.CanCreateUnder(this.server))
			{
				exchangeRpcClientAccess.SetId(ExchangeRpcClientAccess.FromServerId(this.server.Id));
			}
			else
			{
				base.WriteError(new ServerRoleOperationException(Strings.ClientAccessRoleAbsent(this.server.Name)), ErrorCategory.OpenError, this.server);
			}
			TaskLogger.LogExit();
			return exchangeRpcClientAccess;
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x00170004 File Offset: 0x0016E204
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			ExchangeRpcClientAccess exchangeRpcClientAccess = (ExchangeRpcClientAccess)dataObject;
			exchangeRpcClientAccess.CompleteAllCalculatedProperties(this.server);
			base.WriteResult(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x0017004C File Offset: 0x0016E24C
		internal static string SelectServerWithEqualProbability(List<string> servers)
		{
			Random random = new Random(Environment.TickCount);
			return servers[random.Next(0, servers.Count)];
		}

		// Token: 0x040032B5 RID: 12981
		private Server server;
	}
}
