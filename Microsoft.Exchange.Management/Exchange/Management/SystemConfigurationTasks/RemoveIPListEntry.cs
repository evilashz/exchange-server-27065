using System;
using System.ComponentModel;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A6D RID: 2669
	public abstract class RemoveIPListEntry<T> : RemoveTaskBase<IPListEntryIdentity, T> where T : IPListEntry, new()
	{
		// Token: 0x17001CB4 RID: 7348
		// (get) Token: 0x06005F43 RID: 24387 RVA: 0x0018F66A File Offset: 0x0018D86A
		// (set) Token: 0x06005F44 RID: 24388 RVA: 0x0018F672 File Offset: 0x0018D872
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

		// Token: 0x06005F45 RID: 24389 RVA: 0x0018F67B File Offset: 0x0018D87B
		protected override IConfigDataProvider CreateSession()
		{
			if (this.server == null)
			{
				throw new InvalidOperationException("target RPC server (this.server) must be initialized prior to calling CreateSession().");
			}
			return new IPListDataProvider(this.server.Name);
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x0018F6A0 File Offset: 0x0018D8A0
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(RpcException).IsInstanceOfType(exception) || typeof(RpcClientException).IsInstanceOfType(exception) || typeof(Win32Exception).IsInstanceOfType(exception);
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x0018F6F0 File Offset: 0x0018D8F0
		protected override void TranslateException(ref Exception e, out ErrorCategory category)
		{
			category = (ErrorCategory)1001;
			e = RpcClientException.TranslateRpcException(e);
			if (typeof(RpcClientException).IsInstanceOfType(e))
			{
				e = new LocalizedException(Strings.ConnectionToIPListRPCEndpointFailed((this.server != null) ? this.server.Name : Environment.MachineName), e);
				return;
			}
			if (typeof(Win32Exception).IsInstanceOfType(e))
			{
				e = new LocalizedException(Strings.GenericError(e.Message), e);
				return;
			}
			base.TranslateException(ref e, out category);
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x0018F77C File Offset: 0x0018D97C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.serverId == null)
			{
				this.serverId = new ServerIdParameter();
			}
			IConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 121, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageHygiene\\IPAllowBlockListEntries\\RemoveIPListEntry.cs");
			this.server = (Server)base.GetDataObject<Server>(this.serverId, session, null, new LocalizedString?(Strings.ErrorServerNotFound(this.serverId.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.serverId.ToString())));
			if (this.server == null || !IPListEntryUtils.IsSupportedRole(this.server))
			{
				base.WriteError(new LocalizedException(Strings.ErrorInvalidServerRole((this.server != null) ? this.server.Name : Environment.MachineName)), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x04003520 RID: 13600
		private ServerIdParameter serverId;

		// Token: 0x04003521 RID: 13601
		private Server server;
	}
}
