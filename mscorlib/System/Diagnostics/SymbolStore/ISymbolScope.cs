using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003D5 RID: 981
	[ComVisible(true)]
	public interface ISymbolScope
	{
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600329C RID: 12956
		ISymbolMethod Method { get; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600329D RID: 12957
		ISymbolScope Parent { get; }

		// Token: 0x0600329E RID: 12958
		ISymbolScope[] GetChildren();

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600329F RID: 12959
		int StartOffset { get; }

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060032A0 RID: 12960
		int EndOffset { get; }

		// Token: 0x060032A1 RID: 12961
		ISymbolVariable[] GetLocals();

		// Token: 0x060032A2 RID: 12962
		ISymbolNamespace[] GetNamespaces();
	}
}
