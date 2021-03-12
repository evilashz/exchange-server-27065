using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000764 RID: 1892
	internal sealed class ObjectNull : IStreamable
	{
		// Token: 0x06005301 RID: 21249 RVA: 0x00123BF9 File Offset: 0x00121DF9
		internal ObjectNull()
		{
		}

		// Token: 0x06005302 RID: 21250 RVA: 0x00123C01 File Offset: 0x00121E01
		internal void SetNullCount(int nullCount)
		{
			this.nullCount = nullCount;
		}

		// Token: 0x06005303 RID: 21251 RVA: 0x00123C0C File Offset: 0x00121E0C
		public void Write(__BinaryWriter sout)
		{
			if (this.nullCount == 1)
			{
				sout.WriteByte(10);
				return;
			}
			if (this.nullCount < 256)
			{
				sout.WriteByte(13);
				sout.WriteByte((byte)this.nullCount);
				return;
			}
			sout.WriteByte(14);
			sout.WriteInt32(this.nullCount);
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x00123C62 File Offset: 0x00121E62
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.Read(input, BinaryHeaderEnum.ObjectNull);
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x00123C70 File Offset: 0x00121E70
		public void Read(__BinaryParser input, BinaryHeaderEnum binaryHeaderEnum)
		{
			switch (binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ObjectNull:
				this.nullCount = 1;
				return;
			case BinaryHeaderEnum.MessageEnd:
			case BinaryHeaderEnum.Assembly:
				break;
			case BinaryHeaderEnum.ObjectNullMultiple256:
				this.nullCount = (int)input.ReadByte();
				return;
			case BinaryHeaderEnum.ObjectNullMultiple:
				this.nullCount = input.ReadInt32();
				break;
			default:
				return;
			}
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x00123CBC File Offset: 0x00121EBC
		public void Dump()
		{
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x00123CBE File Offset: 0x00121EBE
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY") && this.nullCount != 1)
			{
				int num = this.nullCount;
			}
		}

		// Token: 0x04002547 RID: 9543
		internal int nullCount;
	}
}
