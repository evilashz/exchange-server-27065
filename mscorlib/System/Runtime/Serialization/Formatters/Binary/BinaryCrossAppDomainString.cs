using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200075C RID: 1884
	internal sealed class BinaryCrossAppDomainString : IStreamable
	{
		// Token: 0x060052D3 RID: 21203 RVA: 0x00123268 File Offset: 0x00121468
		internal BinaryCrossAppDomainString()
		{
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x00123270 File Offset: 0x00121470
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(19);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.value);
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x00123292 File Offset: 0x00121492
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadInt32();
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x001232AC File Offset: 0x001214AC
		public void Dump()
		{
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x001232AE File Offset: 0x001214AE
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002527 RID: 9511
		internal int objectId;

		// Token: 0x04002528 RID: 9512
		internal int value;
	}
}
