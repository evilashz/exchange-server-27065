using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000763 RID: 1891
	internal sealed class MemberReference : IStreamable
	{
		// Token: 0x060052FB RID: 21243 RVA: 0x00123BB5 File Offset: 0x00121DB5
		internal MemberReference()
		{
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x00123BBD File Offset: 0x00121DBD
		internal void Set(int idRef)
		{
			this.idRef = idRef;
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x00123BC6 File Offset: 0x00121DC6
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(9);
			sout.WriteInt32(this.idRef);
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x00123BDC File Offset: 0x00121DDC
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.idRef = input.ReadInt32();
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x00123BEA File Offset: 0x00121DEA
		public void Dump()
		{
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x00123BEC File Offset: 0x00121DEC
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002546 RID: 9542
		internal int idRef;
	}
}
