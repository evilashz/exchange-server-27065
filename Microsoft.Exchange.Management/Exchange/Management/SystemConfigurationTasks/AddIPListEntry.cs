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
	// Token: 0x02000A5B RID: 2651
	public abstract class AddIPListEntry<T> : NewTaskBase<T> where T : IPListEntry, new()
	{
		// Token: 0x06005EF7 RID: 24311 RVA: 0x0018E5D5 File Offset: 0x0018C7D5
		protected override IConfigDataProvider CreateSession()
		{
			if (this.server == null)
			{
				throw new InvalidOperationException("target RPC server (this.server) must be initialized prior to calling CreateSession().");
			}
			return new IPListDataProvider(this.server.Name);
		}

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x06005EF8 RID: 24312 RVA: 0x0018E5FC File Offset: 0x0018C7FC
		// (set) Token: 0x06005EF9 RID: 24313 RVA: 0x0018E620 File Offset: 0x0018C820
		[Parameter(Mandatory = true, ParameterSetName = "IPRange")]
		public IPRange IPRange
		{
			get
			{
				T dataObject = this.DataObject;
				return dataObject.IPRange;
			}
			set
			{
				T dataObject = this.DataObject;
				dataObject.IPRange = value;
			}
		}

		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x06005EFA RID: 24314 RVA: 0x0018E644 File Offset: 0x0018C844
		// (set) Token: 0x06005EFB RID: 24315 RVA: 0x0018E670 File Offset: 0x0018C870
		[Parameter(Mandatory = true, ParameterSetName = "IPAddress")]
		public IPAddress IPAddress
		{
			get
			{
				T dataObject = this.DataObject;
				return dataObject.IPRange.UpperBound;
			}
			set
			{
				T dataObject = this.DataObject;
				dataObject.IPRange = IPRange.CreateSingleAddress(value);
			}
		}

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x06005EFC RID: 24316 RVA: 0x0018E698 File Offset: 0x0018C898
		// (set) Token: 0x06005EFD RID: 24317 RVA: 0x0018E6BC File Offset: 0x0018C8BC
		[Parameter(Mandatory = false)]
		public DateTime ExpirationTime
		{
			get
			{
				T dataObject = this.DataObject;
				return dataObject.ExpirationTime;
			}
			set
			{
				T dataObject = this.DataObject;
				dataObject.ExpirationTime = value;
			}
		}

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x06005EFE RID: 24318 RVA: 0x0018E6E0 File Offset: 0x0018C8E0
		// (set) Token: 0x06005EFF RID: 24319 RVA: 0x0018E704 File Offset: 0x0018C904
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				T dataObject = this.DataObject;
				return dataObject.Comment;
			}
			set
			{
				T dataObject = this.DataObject;
				dataObject.Comment = value;
			}
		}

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x06005F00 RID: 24320 RVA: 0x0018E726 File Offset: 0x0018C926
		// (set) Token: 0x06005F01 RID: 24321 RVA: 0x0018E72E File Offset: 0x0018C92E
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

		// Token: 0x06005F02 RID: 24322 RVA: 0x0018E738 File Offset: 0x0018C938
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(RpcException).IsInstanceOfType(exception) || typeof(RpcClientException).IsInstanceOfType(exception) || typeof(Win32Exception).IsInstanceOfType(exception);
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x0018E788 File Offset: 0x0018C988
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

		// Token: 0x06005F04 RID: 24324 RVA: 0x0018E814 File Offset: 0x0018CA14
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.serverId == null)
			{
				this.serverId = new ServerIdParameter();
			}
			IConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 168, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageHygiene\\IPAllowBlockListEntries\\AddIPListEntry.cs");
			this.server = (Server)base.GetDataObject<Server>(this.serverId, session, null, new LocalizedString?(Strings.ErrorServerNotFound(this.serverId.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.serverId.ToString())));
			if (this.server == null || !IPListEntryUtils.IsSupportedRole(this.server))
			{
				base.WriteError(new LocalizedException(Strings.ErrorInvalidServerRole((this.server != null) ? this.server.Name : Environment.MachineName)), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x0018E8E0 File Offset: 0x0018CAE0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				T dataObject = this.DataObject;
				if (dataObject.HasExpired)
				{
					base.WriteError(new LocalizedException(Strings.ExpirationTimeTooSoon(this.ExpirationTime)), ErrorCategory.InvalidArgument, this.DataObject);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003503 RID: 13571
		private ServerIdParameter serverId;

		// Token: 0x04003504 RID: 13572
		private Server server;
	}
}
