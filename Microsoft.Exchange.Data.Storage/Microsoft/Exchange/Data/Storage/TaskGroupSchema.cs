using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CDD RID: 3293
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskGroupSchema : FolderTreeDataSchema
	{
		// Token: 0x17001E6C RID: 7788
		// (get) Token: 0x060071FE RID: 29182 RVA: 0x001F8D31 File Offset: 0x001F6F31
		public new static TaskGroupSchema Instance
		{
			get
			{
				if (TaskGroupSchema.instance == null)
				{
					TaskGroupSchema.instance = new TaskGroupSchema();
				}
				return TaskGroupSchema.instance;
			}
		}

		// Token: 0x04004F16 RID: 20246
		[Autoload]
		public static readonly StorePropertyDefinition GroupClassId = InternalSchema.NavigationNodeGroupClassId;

		// Token: 0x04004F17 RID: 20247
		private static TaskGroupSchema instance;
	}
}
