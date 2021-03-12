using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003D3 RID: 979
	[ComVisible(true)]
	public interface ISymbolNamespace
	{
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600328F RID: 12943
		string Name { get; }

		// Token: 0x06003290 RID: 12944
		ISymbolNamespace[] GetNamespaces();

		// Token: 0x06003291 RID: 12945
		ISymbolVariable[] GetVariables();
	}
}
