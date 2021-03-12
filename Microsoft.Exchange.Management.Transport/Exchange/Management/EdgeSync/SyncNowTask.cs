using System;
using System.ComponentModel;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.EdgeSync;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x0200003E RID: 62
	[Cmdlet("Start", "EdgeSynchronization", SupportsShouldProcess = true)]
	public sealed class SyncNowTask : DataAccessTask<Server>
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007BA0 File Offset: 0x00005DA0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageStartEdgeSynchronization;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00007BA7 File Offset: 0x00005DA7
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00007BAF File Offset: 0x00005DAF
		[Parameter(Mandatory = false)]
		public ServerIdParameter Server
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00007BB8 File Offset: 0x00005DB8
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00007BC0 File Offset: 0x00005DC0
		[Parameter(Mandatory = false)]
		public string TargetServer
		{
			get
			{
				return this.targetServer;
			}
			set
			{
				this.targetServer = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007BC9 File Offset: 0x00005DC9
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00007BD1 File Offset: 0x00005DD1
		[Parameter(Mandatory = false)]
		public SwitchParameter ForceFullSync
		{
			get
			{
				return this.forceFullSync;
			}
			set
			{
				this.forceFullSync = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007BDA File Offset: 0x00005DDA
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00007BE2 File Offset: 0x00005DE2
		[Parameter(Mandatory = false)]
		public SwitchParameter ForceUpdateCookie
		{
			get
			{
				return this.forceUpdateCookie;
			}
			set
			{
				this.forceUpdateCookie = value;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007BEB File Offset: 0x00005DEB
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 127, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\SyncNowTask.cs");
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007C0C File Offset: 0x00005E0C
		protected override void InternalValidate()
		{
			if (this.serverId == null)
			{
				this.serverId = new ServerIdParameter();
			}
			this.server = (Server)base.GetDataObject<Server>(this.serverId, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound((string)this.serverId)), new LocalizedString?(Strings.ErrorServerNotUnique((string)this.serverId)));
			if (!this.server.IsHubTransportServer)
			{
				base.WriteError(new InvalidOperationException(Strings.SynNowCanOnlyRunAgainstHub), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007C98 File Offset: 0x00005E98
		protected override void InternalProcessRecord()
		{
			try
			{
				this.client = new EdgeSyncRpcClient(this.server.Name);
				StartResults startResults = this.TryStartSyncNow();
				if (startResults == StartResults.Started)
				{
					for (;;)
					{
						GetResultResults getResultResults = GetResultResults.NoMoreData;
						byte[] syncNowResult = this.client.GetSyncNowResult(ref getResultResults);
						if (getResultResults == GetResultResults.NoMoreData)
						{
							break;
						}
						if (getResultResults == GetResultResults.Error)
						{
							base.WriteError(new SyncNowFailedToRunException(), ErrorCategory.InvalidOperation, null);
						}
						else if (syncNowResult == null)
						{
							Thread.Sleep(1000);
						}
						else
						{
							Status sendToPipeline = Status.Deserialize(syncNowResult);
							base.WriteObject(sendToPipeline);
						}
					}
				}
				else if (startResults == StartResults.AlreadyStarted)
				{
					base.WriteError(new SyncNowAlreadyStartedException(), ErrorCategory.InvalidOperation, null);
				}
				else
				{
					base.WriteError(new SyncNowFailedToRunException(), ErrorCategory.InvalidOperation, null);
				}
			}
			catch (RpcException ex)
			{
				this.WriteTranslatedError(ex);
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007D4C File Offset: 0x00005F4C
		private StartResults TryStartSyncNow()
		{
			int num = 3;
			while (num-- > 0)
			{
				try
				{
					return this.client.StartSyncNow(this.targetServer, this.forceFullSync, this.forceUpdateCookie);
				}
				catch (RpcException ex)
				{
					RpcError errorCode = (RpcError)ex.ErrorCode;
					if ((errorCode != RpcError.EndpointNotRegistered && errorCode != RpcError.RemoteDidNotExecute) || num == 0)
					{
						throw;
					}
				}
			}
			return StartResults.ErrorOnStart;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007DC4 File Offset: 0x00005FC4
		private void WriteTranslatedError(RpcException ex)
		{
			RpcError errorCode = (RpcError)ex.ErrorCode;
			if (errorCode == RpcError.ServerUnavailable)
			{
				base.WriteError(new ServerUnavailableException(), ErrorCategory.ReadError, null);
				return;
			}
			if (errorCode != RpcError.EndpointNotRegistered)
			{
				base.WriteError(new Win32Exception(ex.ErrorCode), ErrorCategory.NotSpecified, null);
				return;
			}
			base.WriteError(new EndPointNotRegisteredException(), ErrorCategory.ReadError, null);
		}

		// Token: 0x04000097 RID: 151
		private EdgeSyncRpcClient client;

		// Token: 0x04000098 RID: 152
		private ServerIdParameter serverId;

		// Token: 0x04000099 RID: 153
		private Server server;

		// Token: 0x0400009A RID: 154
		private string targetServer = string.Empty;

		// Token: 0x0400009B RID: 155
		private SwitchParameter forceFullSync;

		// Token: 0x0400009C RID: 156
		private SwitchParameter forceUpdateCookie;
	}
}
