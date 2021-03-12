using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02000075 RID: 117
	public abstract class MessageActionWithFilter : MessageActionWithIdentity
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000102C7 File Offset: 0x0000E4C7
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x000102DE File Offset: 0x0000E4DE
		[Parameter(Mandatory = true, ParameterSetName = "Filter")]
		[ValidateNotNullOrEmpty]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				this.InitializeInnerFilter<ExtensibleMessageInfoSchema>(value, ObjectSchema.GetInstance<ExtensibleMessageInfoSchema>());
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x000102FD File Offset: 0x0000E4FD
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x00010305 File Offset: 0x0000E505
		[Parameter(ParameterSetName = "Filter", ValueFromPipeline = true)]
		public new ServerIdParameter Server
		{
			get
			{
				return base.Server;
			}
			set
			{
				base.Server = value;
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00010310 File Offset: 0x0000E510
		internal void InitializeInnerFilter<Schema>(string filterString, Schema messageInfoSchema) where Schema : PagedObjectSchema
		{
			QueryFilter filter = new MonadFilter(filterString, this, messageInfoSchema).InnerFilter;
			this.innerFilter = DateTimeConverter.ConvertQueryFilter(filter);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001033C File Offset: 0x0000E53C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.Identity == null && this.Server == null)
			{
				this.Server = ServerIdParameter.Parse("localhost");
			}
			if (this.Filter != null && !VersionedQueueViewerClient.UsePropertyBagBasedAPI((string)this.Server))
			{
				this.InitializeInnerFilter<MessageInfoSchema>(this.Filter, ObjectSchema.GetInstance<MessageInfoSchema>());
			}
		}

		// Token: 0x0400017E RID: 382
		protected QueryFilter innerFilter;
	}
}
