using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001C7 RID: 455
	internal sealed class TaskShape : Shape
	{
		// Token: 0x06000C63 RID: 3171 RVA: 0x00040B10 File Offset: 0x0003ED10
		static TaskShape()
		{
			TaskShape.defaultProperties.Add(ItemSchema.ItemId);
			TaskShape.defaultProperties.Add(ItemSchema.Subject);
			TaskShape.defaultProperties.Add(ItemSchema.Attachments);
			TaskShape.defaultProperties.Add(ItemSchema.HasAttachments);
			TaskShape.defaultProperties.Add(TaskSchema.DueDate);
			TaskShape.defaultProperties.Add(TaskSchema.PercentComplete);
			TaskShape.defaultProperties.Add(TaskSchema.StartDate);
			TaskShape.defaultProperties.Add(TaskSchema.Status);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00040B9F File Offset: 0x0003ED9F
		private TaskShape() : base(Schema.Task, TaskSchema.GetSchema(), ItemShape.CreateShape(), TaskShape.defaultProperties)
		{
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00040BBB File Offset: 0x0003EDBB
		internal static TaskShape CreateShape()
		{
			return new TaskShape();
		}

		// Token: 0x04000A38 RID: 2616
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
