using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000765 RID: 1893
	internal sealed class MessageEnd : IStreamable
	{
		// Token: 0x06005308 RID: 21256 RVA: 0x00123CE2 File Offset: 0x00121EE2
		internal MessageEnd()
		{
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x00123CEA File Offset: 0x00121EEA
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(11);
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x00123CF4 File Offset: 0x00121EF4
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x00123CF6 File Offset: 0x00121EF6
		public void Dump()
		{
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x00123CF8 File Offset: 0x00121EF8
		public void Dump(Stream sout)
		{
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x00123CFC File Offset: 0x00121EFC
		[Conditional("_LOGGING")]
		private void DumpInternal(Stream sout)
		{
			if (BCLDebug.CheckEnabled("BINARY") && sout != null && sout.CanSeek)
			{
				long length = sout.Length;
			}
		}
	}
}
