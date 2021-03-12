using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200075E RID: 1886
	internal sealed class MemberPrimitiveTyped : IStreamable
	{
		// Token: 0x060052DD RID: 21213 RVA: 0x001232F6 File Offset: 0x001214F6
		internal MemberPrimitiveTyped()
		{
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x001232FE File Offset: 0x001214FE
		internal void Set(InternalPrimitiveTypeE primitiveTypeEnum, object value)
		{
			this.primitiveTypeEnum = primitiveTypeEnum;
			this.value = value;
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x0012330E File Offset: 0x0012150E
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(8);
			sout.WriteByte((byte)this.primitiveTypeEnum);
			sout.WriteValue(this.primitiveTypeEnum, this.value);
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x00123336 File Offset: 0x00121536
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.primitiveTypeEnum = (InternalPrimitiveTypeE)input.ReadByte();
			this.value = input.ReadValue(this.primitiveTypeEnum);
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x00123356 File Offset: 0x00121556
		public void Dump()
		{
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x00123358 File Offset: 0x00121558
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x0400252A RID: 9514
		internal InternalPrimitiveTypeE primitiveTypeEnum;

		// Token: 0x0400252B RID: 9515
		internal object value;
	}
}
