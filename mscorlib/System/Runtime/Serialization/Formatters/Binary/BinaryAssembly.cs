using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000756 RID: 1878
	internal sealed class BinaryAssembly : IStreamable
	{
		// Token: 0x060052AD RID: 21165 RVA: 0x00122549 File Offset: 0x00120749
		internal BinaryAssembly()
		{
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x00122551 File Offset: 0x00120751
		internal void Set(int assemId, string assemblyString)
		{
			this.assemId = assemId;
			this.assemblyString = assemblyString;
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x00122561 File Offset: 0x00120761
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(12);
			sout.WriteInt32(this.assemId);
			sout.WriteString(this.assemblyString);
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x00122583 File Offset: 0x00120783
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyString = input.ReadString();
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x0012259D File Offset: 0x0012079D
		public void Dump()
		{
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x0012259F File Offset: 0x0012079F
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002506 RID: 9478
		internal int assemId;

		// Token: 0x04002507 RID: 9479
		internal string assemblyString;
	}
}
