using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003CF RID: 975
	[ComVisible(true)]
	public interface ISymbolBinder1
	{
		// Token: 0x06003278 RID: 12920
		ISymbolReader GetReader(IntPtr importer, string filename, string searchPath);
	}
}
