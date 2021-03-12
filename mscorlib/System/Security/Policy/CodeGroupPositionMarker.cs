using System;

namespace System.Security.Policy
{
	// Token: 0x0200031F RID: 799
	internal class CodeGroupPositionMarker
	{
		// Token: 0x060028D8 RID: 10456 RVA: 0x00096A94 File Offset: 0x00094C94
		internal CodeGroupPositionMarker(int elementIndex, int groupIndex, SecurityElement element)
		{
			this.elementIndex = elementIndex;
			this.groupIndex = groupIndex;
			this.element = element;
		}

		// Token: 0x04001073 RID: 4211
		internal int elementIndex;

		// Token: 0x04001074 RID: 4212
		internal int groupIndex;

		// Token: 0x04001075 RID: 4213
		internal SecurityElement element;
	}
}
