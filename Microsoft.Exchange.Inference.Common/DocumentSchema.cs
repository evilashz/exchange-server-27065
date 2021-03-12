using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200000E RID: 14
	internal class DocumentSchema : Schema
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002CC5 File Offset: 0x00000EC5
		public new static DocumentSchema Instance
		{
			get
			{
				if (DocumentSchema.instance == null)
				{
					DocumentSchema.instance = new DocumentSchema();
				}
				return DocumentSchema.instance;
			}
		}

		// Token: 0x04000029 RID: 41
		public static readonly PropertyDefinition Identity = new SimplePropertyDefinition("Identity", typeof(IIdentity), PropertyFlag.None);

		// Token: 0x0400002A RID: 42
		public static readonly PropertyDefinition Operation = new SimplePropertyDefinition("Operation", typeof(DocumentOperation), PropertyFlag.None);

		// Token: 0x0400002B RID: 43
		public static readonly PropertyDefinition PipelineInstanceName = new SimplePropertyDefinition("PipelineInstanceName", typeof(string), PropertyFlag.None);

		// Token: 0x0400002C RID: 44
		public static readonly PropertyDefinition PipelineVersion = new SimplePropertyDefinition("PipelineVersion", typeof(Version), PropertyFlag.None);

		// Token: 0x0400002D RID: 45
		public static readonly PropertyDefinition MailboxId = new SimplePropertyDefinition("MailboxId", typeof(string), PropertyFlag.None);

		// Token: 0x0400002E RID: 46
		public static readonly PropertyDefinition ParentIdentity = new SimplePropertyDefinition("ParentIdentity", typeof(IIdentity), PropertyFlag.None);

		// Token: 0x0400002F RID: 47
		private static DocumentSchema instance = null;
	}
}
