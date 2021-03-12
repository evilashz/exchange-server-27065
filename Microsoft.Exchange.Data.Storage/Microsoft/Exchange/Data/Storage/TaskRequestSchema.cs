using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CDF RID: 3295
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TaskRequestSchema : MessageItemSchema
	{
		// Token: 0x06007204 RID: 29188 RVA: 0x001F8DA8 File Offset: 0x001F6FA8
		private TaskRequestSchema()
		{
			base.AddDependencies(new Schema[]
			{
				TaskSchema.Instance
			});
		}

		// Token: 0x17001E6E RID: 7790
		// (get) Token: 0x06007205 RID: 29189 RVA: 0x001F8DD1 File Offset: 0x001F6FD1
		public new static TaskRequestSchema Instance
		{
			get
			{
				if (TaskRequestSchema.instance == null)
				{
					TaskRequestSchema.instance = new TaskRequestSchema();
				}
				return TaskRequestSchema.instance;
			}
		}

		// Token: 0x04004F1D RID: 20253
		private static TaskRequestSchema instance;
	}
}
