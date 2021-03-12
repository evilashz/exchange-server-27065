using System;

namespace System
{
	// Token: 0x0200008B RID: 139
	internal struct SwitchStructure
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x0001923D File Offset: 0x0001743D
		internal SwitchStructure(string n, int v)
		{
			this.name = n;
			this.value = v;
		}

		// Token: 0x0400036A RID: 874
		internal string name;

		// Token: 0x0400036B RID: 875
		internal int value;
	}
}
