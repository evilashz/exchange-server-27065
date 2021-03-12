using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000758 RID: 1880
	internal sealed class BinaryObject : IStreamable
	{
		// Token: 0x060052B8 RID: 21176 RVA: 0x001225FF File Offset: 0x001207FF
		internal BinaryObject()
		{
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x00122607 File Offset: 0x00120807
		internal void Set(int objectId, int mapId)
		{
			this.objectId = objectId;
			this.mapId = mapId;
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x00122617 File Offset: 0x00120817
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(1);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.mapId);
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x00122638 File Offset: 0x00120838
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.mapId = input.ReadInt32();
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x00122652 File Offset: 0x00120852
		public void Dump()
		{
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x00122654 File Offset: 0x00120854
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x0400250A RID: 9482
		internal int objectId;

		// Token: 0x0400250B RID: 9483
		internal int mapId;
	}
}
