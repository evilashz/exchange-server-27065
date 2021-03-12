using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CC0 RID: 3264
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RightsManagedMessageItemSchema : MessageItemSchema
	{
		// Token: 0x06007175 RID: 29045 RVA: 0x001F746B File Offset: 0x001F566B
		private RightsManagedMessageItemSchema()
		{
		}

		// Token: 0x17001E5C RID: 7772
		// (get) Token: 0x06007176 RID: 29046 RVA: 0x001F7473 File Offset: 0x001F5673
		public new static RightsManagedMessageItemSchema Instance
		{
			get
			{
				if (RightsManagedMessageItemSchema.instance == null)
				{
					RightsManagedMessageItemSchema.instance = new RightsManagedMessageItemSchema();
				}
				return RightsManagedMessageItemSchema.instance;
			}
		}

		// Token: 0x04004EB8 RID: 20152
		private static RightsManagedMessageItemSchema instance;
	}
}
