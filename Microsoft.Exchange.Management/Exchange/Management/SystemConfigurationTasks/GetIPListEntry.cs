using System;
using System.ComponentModel;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A5F RID: 2655
	public abstract class GetIPListEntry<T> : GetObjectWithIdentityTaskBase<IPListEntryIdentity, T> where T : IPListEntry, new()
	{
		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x06005F0B RID: 24331 RVA: 0x0018E9BF File Offset: 0x0018CBBF
		// (set) Token: 0x06005F0C RID: 24332 RVA: 0x0018E9D6 File Offset: 0x0018CBD6
		[Parameter(Mandatory = true, ParameterSetName = "IPAddress")]
		public IPAddress IPAddress
		{
			get
			{
				return (IPAddress)base.Fields["IPAddress"];
			}
			set
			{
				base.Fields["IPAddress"] = value;
			}
		}

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x06005F0D RID: 24333 RVA: 0x0018E9E9 File Offset: 0x0018CBE9
		// (set) Token: 0x06005F0E RID: 24334 RVA: 0x0018E9F1 File Offset: 0x0018CBF1
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

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x06005F0F RID: 24335 RVA: 0x0018E9FA File Offset: 0x0018CBFA
		// (set) Token: 0x06005F10 RID: 24336 RVA: 0x0018EA02 File Offset: 0x0018CC02
		[Parameter(Mandatory = false)]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x06005F11 RID: 24337 RVA: 0x0018EA0B File Offset: 0x0018CC0B
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x06005F12 RID: 24338 RVA: 0x0018EA12 File Offset: 0x0018CC12
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (this.IPAddress == null)
				{
					return null;
				}
				return new IPListQueryFilter(this.IPAddress);
			}
		}

		// Token: 0x06005F13 RID: 24339 RVA: 0x0018EA29 File Offset: 0x0018CC29
		protected override IConfigDataProvider CreateSession()
		{
			if (this.server == null)
			{
				throw new InvalidOperationException("target RPC server (this.server) must be initialized prior to calling CreateSession().");
			}
			return new IPListDataProvider(this.server.Name);
		}

		// Token: 0x06005F14 RID: 24340 RVA: 0x0018EA50 File Offset: 0x0018CC50
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(RpcException).IsInstanceOfType(exception) || typeof(RpcClientException).IsInstanceOfType(exception) || typeof(Win32Exception).IsInstanceOfType(exception);
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x0018EAA0 File Offset: 0x0018CCA0
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

		// Token: 0x06005F16 RID: 24342 RVA: 0x0018EB2C File Offset: 0x0018CD2C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.serverId == null)
			{
				this.serverId = new ServerIdParameter();
			}
			IConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 160, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageHygiene\\IPAllowBlockListEntries\\GetIPListEntry.cs");
			this.server = (Server)base.GetDataObject<Server>(this.serverId, session, null, new LocalizedString?(Strings.ErrorServerNotFound(this.serverId.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.serverId.ToString())));
			if (this.server == null || !IPListEntryUtils.IsSupportedRole(this.server))
			{
				base.WriteError(new LocalizedException(Strings.ErrorInvalidServerRole((this.server != null) ? this.server.Name : Environment.MachineName)), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x04003507 RID: 13575
		private ServerIdParameter serverId;

		// Token: 0x04003508 RID: 13576
		private Server server;
	}
}
