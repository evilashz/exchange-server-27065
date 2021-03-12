using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000757 RID: 1879
	internal sealed class BinaryCrossAppDomainAssembly : IStreamable
	{
		// Token: 0x060052B3 RID: 21171 RVA: 0x001225AC File Offset: 0x001207AC
		internal BinaryCrossAppDomainAssembly()
		{
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x001225B4 File Offset: 0x001207B4
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(20);
			sout.WriteInt32(this.assemId);
			sout.WriteInt32(this.assemblyIndex);
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x001225D6 File Offset: 0x001207D6
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyIndex = input.ReadInt32();
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x001225F0 File Offset: 0x001207F0
		public void Dump()
		{
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x001225F2 File Offset: 0x001207F2
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002508 RID: 9480
		internal int assemId;

		// Token: 0x04002509 RID: 9481
		internal int assemblyIndex;
	}
}
