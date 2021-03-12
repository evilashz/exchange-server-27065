using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003D1 RID: 977
	[ComVisible(true)]
	public interface ISymbolDocumentWriter
	{
		// Token: 0x06003283 RID: 12931
		void SetSource(byte[] source);

		// Token: 0x06003284 RID: 12932
		void SetCheckSum(Guid algorithmId, byte[] checkSum);
	}
}
