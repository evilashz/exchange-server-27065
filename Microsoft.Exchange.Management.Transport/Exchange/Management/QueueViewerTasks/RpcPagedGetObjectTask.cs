using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.QueueViewer;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02000071 RID: 113
	public abstract class RpcPagedGetObjectTask<ObjectType> : PagedGetObjectTask<ObjectType> where ObjectType : PagedDataObject
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000FA53 File Offset: 0x0000DC53
		[Parameter(Mandatory = false, ParameterSetName = "Filter", ValueFromPipeline = true)]
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000FA66 File Offset: 0x0000DC66
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		protected SwitchParameter IncludeDetails
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeDetails"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeDetails"] = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0000FACA File Offset: 0x0000DCCA
		protected SwitchParameter IncludeLatencyInfo
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeLatencyInfo"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeLatencyInfo"] = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000FAE2 File Offset: 0x0000DCE2
		protected Server TargetServer
		{
			get
			{
				return this.targetServer;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000FAEA File Offset: 0x0000DCEA
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.Server == null)
			{
				this.Server = ServerIdParameter.Parse("localhost");
			}
			this.ResolveTargetServer();
			TaskLogger.LogExit();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000FB28 File Offset: 0x0000DD28
		protected override void InternalProcessRecord()
		{
			ObjectType[] array = null;
			int totalCount = 0;
			int num = 0;
			try
			{
				int num2 = 3;
				QueryFilter queryFilter = DateTimeConverter.ConvertQueryFilter(this.innerFilter);
				while (num2-- > 0)
				{
					try
					{
						if (base.BookmarkObject != null)
						{
							ObjectType bookmarkObject = base.BookmarkObject;
							bookmarkObject.ConvertDatesToUniversalTime();
						}
						QueueViewerInputObject queueViewerInputObject = new QueueViewerInputObject((base.BookmarkIndex <= 0) ? -1 : (base.BookmarkIndex - 1), base.BookmarkObject, base.IncludeBookmark, this.IncludeLatencyInfo.IsPresent, this.IncludeDetails.IsPresent, base.ResultSize.IsUnlimited ? int.MaxValue : base.ResultSize.Value, queryFilter, base.SearchForward, base.SortOrder);
						using (QueueViewerClient<ObjectType> queueViewerClient = new QueueViewerClient<ObjectType>((string)this.Server))
						{
							if (!VersionedQueueViewerClient.UsePropertyBagBasedAPI(this.targetServer))
							{
								PagedDataObject bookmarkObject2 = null;
								if (typeof(ObjectType) == typeof(ExtensibleQueueInfo))
								{
									if (base.Filter != null)
									{
										this.innerFilter = new MonadFilter(base.Filter, this, ObjectSchema.GetInstance<QueueInfoSchema>()).InnerFilter;
									}
									this.InitializeInnerFilter<QueueInfo>(null, QueueInfoSchema.Identity);
									if (base.BookmarkObject != null)
									{
										bookmarkObject2 = new QueueInfo(base.BookmarkObject as ExtensibleQueueInfo);
									}
								}
								else
								{
									if (base.Filter != null)
									{
										this.innerFilter = new MonadFilter(base.Filter, this, ObjectSchema.GetInstance<MessageInfoSchema>()).InnerFilter;
									}
									this.InitializeInnerFilter<MessageInfo>(MessageInfoSchema.Identity, MessageInfoSchema.Queue);
									if (base.BookmarkObject != null)
									{
										bookmarkObject2 = new MessageInfo(base.BookmarkObject as ExtensibleMessageInfo);
									}
								}
								queueViewerInputObject.QueryFilter = this.innerFilter;
								List<SortOrderEntry> list = null;
								if (queueViewerInputObject.SortOrder != null)
								{
									list = new List<SortOrderEntry>();
									foreach (QueueViewerSortOrderEntry queueViewerSortOrderEntry in queueViewerInputObject.SortOrder)
									{
										list.Add(SortOrderEntry.Parse(queueViewerSortOrderEntry.ToString()));
									}
								}
								array = queueViewerClient.GetQueueViewerObjectPage(queueViewerInputObject.QueryFilter, (queueViewerInputObject.SortOrder != null) ? list.ToArray() : null, queueViewerInputObject.SearchForward, queueViewerInputObject.PageSize, bookmarkObject2, queueViewerInputObject.BookmarkIndex, queueViewerInputObject.IncludeBookmark, queueViewerInputObject.IncludeDetails, queueViewerInputObject.IncludeComponentLatencyInfo, ref totalCount, ref num);
							}
							else if (VersionedQueueViewerClient.UseCustomSerialization(this.targetServer))
							{
								array = queueViewerClient.GetPropertyBagBasedQueueViewerObjectPageCustomSerialization(queueViewerInputObject, ref totalCount, ref num);
							}
							else
							{
								array = queueViewerClient.GetPropertyBagBasedQueueViewerObjectPage(queueViewerInputObject, ref totalCount, ref num);
							}
							break;
						}
					}
					catch (RpcException ex)
					{
						if ((ex.ErrorCode != 1753 && ex.ErrorCode != 1727) || num2 == 0)
						{
							throw;
						}
					}
				}
				if (base.ReturnPageInfo)
				{
					base.WriteObject(new PagedPositionInfo(num + 1, totalCount));
				}
				DateTimeConverter.ConvertResultSet<ObjectType>(array);
				foreach (ObjectType dataObject in array)
				{
					base.WriteResult(dataObject);
				}
			}
			catch (ParsingNonFilterablePropertyException ex2)
			{
				base.WriteError(ex2, ErrorCategory.InvalidArgument, ex2.PropertyName);
			}
			catch (QueueViewerException ex3)
			{
				base.WriteError(ErrorMapper.GetLocalizedException(ex3.ErrorCode, null, this.Server), ErrorCategory.InvalidOperation, null);
			}
			catch (RpcException ex4)
			{
				base.WriteError(ErrorMapper.GetLocalizedException(ex4.ErrorCode, null, this.Server), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06000403 RID: 1027
		internal abstract void InitializeInnerFilter<Object>(QueueViewerPropertyDefinition<Object> messageIdentity, QueueViewerPropertyDefinition<Object> queueIdentity) where Object : PagedDataObject;

		// Token: 0x06000404 RID: 1028 RVA: 0x0000FF0C File Offset: 0x0000E10C
		private void ResolveTargetServer()
		{
			try
			{
				this.targetServer = VersionedQueueViewerClient.GetServer((string)this.Server);
			}
			catch (QueueViewerException)
			{
				base.WriteError(new LocalizedException(Strings.ErrorServerNotFound((string)this.Server)), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0400017C RID: 380
		private Server targetServer;
	}
}
