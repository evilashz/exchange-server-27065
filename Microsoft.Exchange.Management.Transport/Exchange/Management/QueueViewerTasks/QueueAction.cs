using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.QueueViewer;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x0200007D RID: 125
	public abstract class QueueAction : Task
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00010BF7 File Offset: 0x0000EDF7
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ParameterSetName = "Identity", Position = 0)]
		public QueueIdentity Identity
		{
			get
			{
				return (QueueIdentity)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00010C0A File Offset: 0x0000EE0A
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00010C21 File Offset: 0x0000EE21
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "Filter")]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				this.InitializeInnerFilter<ExtensibleQueueInfoSchema>(value, ObjectSchema.GetInstance<ExtensibleQueueInfoSchema>());
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00010C40 File Offset: 0x0000EE40
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00010C57 File Offset: 0x0000EE57
		[Parameter(ParameterSetName = "Filter", ValueFromPipeline = true)]
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

		// Token: 0x06000457 RID: 1111 RVA: 0x00010C6C File Offset: 0x0000EE6C
		internal void InitializeInnerFilter<Schema>(string filterString, Schema queueInfoSchema) where Schema : PagedObjectSchema
		{
			QueryFilter filter = new MonadFilter(filterString, this, queueInfoSchema).InnerFilter;
			this.innerFilter = DateTimeConverter.ConvertQueryFilter(filter);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00010C98 File Offset: 0x0000EE98
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.Identity != null)
			{
				this.Server = ServerIdParameter.Parse(this.Identity.Server);
			}
			if (this.Server == null || string.Compare(this.Server.ToString(), "localhost", true) == 0)
			{
				this.Server = new ServerIdParameter();
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(base.CurrentOrganizationId), 263, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\Queueviewer\\QueueTasks.cs");
			ServerIdParameter server = this.Server;
			Server entry = (Server)this.GetDataObject<Server>(server, tenantOrTopologyConfigurationSession, null, null, Strings.ErrorServerNotFound(server.ToString()), Strings.ErrorServerNotUnique(server.ToString()));
			ADScopeException ex;
			if (!tenantOrTopologyConfigurationSession.TryVerifyIsWithinScopes(entry, true, out ex))
			{
				base.WriteError(new TaskInvalidOperationException(Strings.ErrorCannotChangeObjectOutOfWriteScope((this.Identity != null) ? this.Identity.ToString() : this.Server.ToString(), (ex == null) ? string.Empty : ex.Message), ex), ExchangeErrorCategory.Context, this.Identity);
			}
			if (this.Filter != null && !VersionedQueueViewerClient.UsePropertyBagBasedAPI((string)this.Server))
			{
				this.InitializeInnerFilter<QueueInfoSchema>(this.Filter, ObjectSchema.GetInstance<QueueInfoSchema>());
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00010DE0 File Offset: 0x0000EFE0
		protected IConfigurable GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, OptionalIdentityData optionalData, LocalizedString notFoundError, LocalizedString multipleFoundError) where TObject : IConfigurable, new()
		{
			IConfigurable result = null;
			LocalizedString? localizedString = null;
			IEnumerable<TObject> objects = id.GetObjects<TObject>(rootID, session, optionalData, out localizedString);
			using (IEnumerator<TObject> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					result = enumerator.Current;
					if (enumerator.MoveNext())
					{
						Exception innerException = new ManagementObjectAmbiguousException(multipleFoundError);
						base.WriteError(new TaskInvalidOperationException(multipleFoundError, innerException), ExchangeErrorCategory.Client, this.Identity);
					}
				}
				else
				{
					LocalizedString message;
					if (localizedString != null)
					{
						string notFound = notFoundError;
						LocalizedString? localizedString2 = localizedString;
						message = Strings.ErrorNotFoundWithReason(notFound, (localizedString2 != null) ? localizedString2.GetValueOrDefault() : null);
					}
					else
					{
						message = notFoundError;
					}
					Exception innerException = new ManagementObjectNotFoundException(message);
					base.WriteError(new TaskInvalidOperationException(notFoundError, innerException), ExchangeErrorCategory.Client, this.Identity);
				}
			}
			return result;
		}

		// Token: 0x0600045A RID: 1114
		protected abstract void RunAction();

		// Token: 0x0600045B RID: 1115 RVA: 0x00010EC4 File Offset: 0x0000F0C4
		protected override void InternalProcessRecord()
		{
			try
			{
				int num = 10;
				while (num-- > 0)
				{
					try
					{
						this.RunAction();
						break;
					}
					catch (RpcException ex)
					{
						if ((ex.ErrorCode != 1753 && ex.ErrorCode != 1727) || num == 0)
						{
							throw;
						}
					}
				}
			}
			catch (QueueViewerException ex2)
			{
				base.WriteError(ErrorMapper.GetLocalizedException(ex2.ErrorCode, this.Identity, this.Server), ErrorCategory.InvalidOperation, null);
			}
			catch (RpcException ex3)
			{
				base.WriteError(ErrorMapper.GetLocalizedException(ex3.ErrorCode, null, this.Server), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x04000183 RID: 387
		protected QueryFilter innerFilter;
	}
}
