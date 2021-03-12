using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003D6 RID: 982
	[ComVisible(true)]
	public interface ISymbolVariable
	{
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060032A3 RID: 12963
		string Name { get; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060032A4 RID: 12964
		object Attributes { get; }

		// Token: 0x060032A5 RID: 12965
		byte[] GetSignature();

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060032A6 RID: 12966
		SymAddressKind AddressKind { get; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060032A7 RID: 12967
		int AddressField1 { get; }

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060032A8 RID: 12968
		int AddressField2 { get; }

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060032A9 RID: 12969
		int AddressField3 { get; }

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060032AA RID: 12970
		int StartOffset { get; }

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060032AB RID: 12971
		int EndOffset { get; }
	}
}
