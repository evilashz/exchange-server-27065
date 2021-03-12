using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200075B RID: 1883
	internal sealed class BinaryObjectString : IStreamable
	{
		// Token: 0x060052CD RID: 21197 RVA: 0x00123206 File Offset: 0x00121406
		internal BinaryObjectString()
		{
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x0012320E File Offset: 0x0012140E
		internal void Set(int objectId, string value)
		{
			this.objectId = objectId;
			this.value = value;
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x0012321E File Offset: 0x0012141E
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(6);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.value);
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x0012323F File Offset: 0x0012143F
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadString();
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x00123259 File Offset: 0x00121459
		public void Dump()
		{
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x0012325B File Offset: 0x0012145B
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002525 RID: 9509
		internal int objectId;

		// Token: 0x04002526 RID: 9510
		internal string value;
	}
}
