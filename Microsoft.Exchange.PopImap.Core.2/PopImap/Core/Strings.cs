using System;
using System.Text;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200003D RID: 61
	internal static class Strings
	{
		// Token: 0x04000207 RID: 519
		internal static readonly string CRLF = "\r\n";

		// Token: 0x04000208 RID: 520
		internal static readonly byte[] CRLFByteArray = Encoding.ASCII.GetBytes(Strings.CRLF);

		// Token: 0x04000209 RID: 521
		internal static readonly string CAS = "cas";

		// Token: 0x0400020A RID: 522
		internal static readonly string MBX = "mbx";
	}
}
