using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000762 RID: 1890
	internal sealed class MemberPrimitiveUnTyped : IStreamable
	{
		// Token: 0x060052F4 RID: 21236 RVA: 0x00123B43 File Offset: 0x00121D43
		internal MemberPrimitiveUnTyped()
		{
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x00123B4B File Offset: 0x00121D4B
		internal void Set(InternalPrimitiveTypeE typeInformation, object value)
		{
			this.typeInformation = typeInformation;
			this.value = value;
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00123B5B File Offset: 0x00121D5B
		internal void Set(InternalPrimitiveTypeE typeInformation)
		{
			this.typeInformation = typeInformation;
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x00123B64 File Offset: 0x00121D64
		public void Write(__BinaryWriter sout)
		{
			sout.WriteValue(this.typeInformation, this.value);
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x00123B78 File Offset: 0x00121D78
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.value = input.ReadValue(this.typeInformation);
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x00123B8C File Offset: 0x00121D8C
		public void Dump()
		{
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x00123B90 File Offset: 0x00121D90
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				string text = Converter.ToComType(this.typeInformation);
			}
		}

		// Token: 0x04002544 RID: 9540
		internal InternalPrimitiveTypeE typeInformation;

		// Token: 0x04002545 RID: 9541
		internal object value;
	}
}
