using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001C8 RID: 456
	internal class TasksFolderShape : Shape
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x00040BC4 File Offset: 0x0003EDC4
		static TasksFolderShape()
		{
			TasksFolderShape.defaultProperties.Add(BaseFolderSchema.FolderId);
			TasksFolderShape.defaultProperties.Add(BaseFolderSchema.DisplayName);
			TasksFolderShape.defaultProperties.Add(FolderSchema.UnreadCount);
			TasksFolderShape.defaultProperties.Add(BaseFolderSchema.TotalCount);
			TasksFolderShape.defaultProperties.Add(BaseFolderSchema.ChildFolderCount);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00040C26 File Offset: 0x0003EE26
		private TasksFolderShape() : base(Schema.TasksFolder, FolderSchema.GetSchema(), new BaseFolderShape(), TasksFolderShape.defaultProperties)
		{
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00040C42 File Offset: 0x0003EE42
		internal static TasksFolderShape CreateShape()
		{
			return new TasksFolderShape();
		}

		// Token: 0x04000A39 RID: 2617
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
