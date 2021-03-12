using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200075F RID: 1887
	internal sealed class BinaryObjectWithMap : IStreamable
	{
		// Token: 0x060052E3 RID: 21219 RVA: 0x00123365 File Offset: 0x00121565
		internal BinaryObjectWithMap()
		{
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x0012336D File Offset: 0x0012156D
		internal BinaryObjectWithMap(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0012337C File Offset: 0x0012157C
		internal void Set(int objectId, string name, int numMembers, string[] memberNames, int assemId)
		{
			this.objectId = objectId;
			this.name = name;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.assemId = assemId;
			if (assemId > 0)
			{
				this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapAssemId;
				return;
			}
			this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMap;
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x001233B8 File Offset: 0x001215B8
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.name);
			sout.WriteInt32(this.numMembers);
			for (int i = 0; i < this.numMembers; i++)
			{
				sout.WriteString(this.memberNames[i]);
			}
			if (this.assemId > 0)
			{
				sout.WriteInt32(this.assemId);
			}
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x0012342C File Offset: 0x0012162C
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.name = input.ReadString();
			this.numMembers = input.ReadInt32();
			this.memberNames = new string[this.numMembers];
			for (int i = 0; i < this.numMembers; i++)
			{
				this.memberNames[i] = input.ReadString();
			}
			if (this.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapAssemId)
			{
				this.assemId = input.ReadInt32();
			}
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x001234A2 File Offset: 0x001216A2
		public void Dump()
		{
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x001234A4 File Offset: 0x001216A4
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				for (int i = 0; i < this.numMembers; i++)
				{
				}
				BinaryHeaderEnum binaryHeaderEnum = this.binaryHeaderEnum;
			}
		}

		// Token: 0x0400252C RID: 9516
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x0400252D RID: 9517
		internal int objectId;

		// Token: 0x0400252E RID: 9518
		internal string name;

		// Token: 0x0400252F RID: 9519
		internal int numMembers;

		// Token: 0x04002530 RID: 9520
		internal string[] memberNames;

		// Token: 0x04002531 RID: 9521
		internal int assemId;
	}
}
