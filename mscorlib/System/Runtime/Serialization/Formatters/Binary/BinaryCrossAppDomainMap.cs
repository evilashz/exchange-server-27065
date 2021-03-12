using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200075D RID: 1885
	internal sealed class BinaryCrossAppDomainMap : IStreamable
	{
		// Token: 0x060052D8 RID: 21208 RVA: 0x001232BB File Offset: 0x001214BB
		internal BinaryCrossAppDomainMap()
		{
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x001232C3 File Offset: 0x001214C3
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(18);
			sout.WriteInt32(this.crossAppDomainArrayIndex);
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x001232D9 File Offset: 0x001214D9
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.crossAppDomainArrayIndex = input.ReadInt32();
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x001232E7 File Offset: 0x001214E7
		public void Dump()
		{
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x001232E9 File Offset: 0x001214E9
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002529 RID: 9513
		internal int crossAppDomainArrayIndex;
	}
}
