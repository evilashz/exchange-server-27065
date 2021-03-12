using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000845 RID: 2117
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TaskRequest : MessageItem
	{
		// Token: 0x06004EFA RID: 20218 RVA: 0x0014B28D File Offset: 0x0014948D
		internal TaskRequest(ICoreItem coreItem) : base(coreItem, false)
		{
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x0014B297 File Offset: 0x00149497
		public new static TaskRequest Bind(StoreSession session, StoreId storeId)
		{
			return TaskRequest.Bind(session, storeId, null);
		}

		// Token: 0x06004EFC RID: 20220 RVA: 0x0014B2A1 File Offset: 0x001494A1
		public new static TaskRequest Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<TaskRequest>(session, storeId, TaskRequestSchema.Instance, propsToReturn);
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x0014B2B0 File Offset: 0x001494B0
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return TaskRequestSchema.Instance;
			}
		}
	}
}
