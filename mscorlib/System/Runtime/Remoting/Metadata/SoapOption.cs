using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007A8 RID: 1960
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum SoapOption
	{
		// Token: 0x04002710 RID: 10000
		None = 0,
		// Token: 0x04002711 RID: 10001
		AlwaysIncludeTypes = 1,
		// Token: 0x04002712 RID: 10002
		XsdString = 2,
		// Token: 0x04002713 RID: 10003
		EmbedAll = 4,
		// Token: 0x04002714 RID: 10004
		Option1 = 8,
		// Token: 0x04002715 RID: 10005
		Option2 = 16
	}
}
