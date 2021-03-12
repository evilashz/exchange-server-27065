using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.UnifiedContent
{
	// Token: 0x0200000A RID: 10
	internal class SharedContentReader : BinaryReader
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002ECC File Offset: 0x000010CC
		internal SharedContentReader(Stream stream) : base(stream, Encoding.Unicode)
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EDC File Offset: 0x000010DC
		public override string ReadString()
		{
			long num = this.ReadInt64();
			if (num % 2L != 0L)
			{
				throw new InvalidDataException();
			}
			int num2 = (int)(num / 2L);
			char[] array = new char[num2];
			int num3 = this.Read(array, 0, num2);
			if (num3 != num2)
			{
				throw new InvalidDataException();
			}
			if (array[num2 - 1] != '\0')
			{
				throw new InvalidDataException();
			}
			return new string(array, 0, num2 - 1);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F38 File Offset: 0x00001138
		internal void ValidateEntryId(uint entryIdToValidate)
		{
			uint num = this.ReadUInt32();
			if (num != entryIdToValidate)
			{
				throw new InvalidDataException();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F58 File Offset: 0x00001158
		internal Stream ReadStream()
		{
			long num = this.ReadInt64();
			long position = this.BaseStream.Position;
			long num2 = position + num;
			if (num2 > this.BaseStream.Length)
			{
				throw new InvalidDataException();
			}
			this.BaseStream.Position = num2;
			return new StreamOnStream(this.BaseStream, position, num);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002FAC File Offset: 0x000011AC
		internal byte[] ReadBuffer()
		{
			int num = this.ReadInt32();
			byte[] array = new byte[num];
			this.Read(array, 0, num);
			return array;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002FD2 File Offset: 0x000011D2
		internal void ValidateAtEndOfEntry()
		{
			if (this.BaseStream.Position != this.BaseStream.Length)
			{
				throw new FormatException("Shared Content Entry invalid");
			}
		}
	}
}
