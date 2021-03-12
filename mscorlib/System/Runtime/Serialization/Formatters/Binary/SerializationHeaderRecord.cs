using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000755 RID: 1877
	internal sealed class SerializationHeaderRecord : IStreamable
	{
		// Token: 0x060052A6 RID: 21158 RVA: 0x001223E3 File Offset: 0x001205E3
		internal SerializationHeaderRecord()
		{
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x001223F2 File Offset: 0x001205F2
		internal SerializationHeaderRecord(BinaryHeaderEnum binaryHeaderEnum, int topId, int headerId, int majorVersion, int minorVersion)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
			this.topId = topId;
			this.headerId = headerId;
			this.majorVersion = majorVersion;
			this.minorVersion = minorVersion;
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x00122428 File Offset: 0x00120628
		public void Write(__BinaryWriter sout)
		{
			this.majorVersion = this.binaryFormatterMajorVersion;
			this.minorVersion = this.binaryFormatterMinorVersion;
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.topId);
			sout.WriteInt32(this.headerId);
			sout.WriteInt32(this.binaryFormatterMajorVersion);
			sout.WriteInt32(this.binaryFormatterMinorVersion);
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x0012248A File Offset: 0x0012068A
		private static int GetInt32(byte[] buffer, int index)
		{
			return (int)buffer[index] | (int)buffer[index + 1] << 8 | (int)buffer[index + 2] << 16 | (int)buffer[index + 3] << 24;
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x001224AC File Offset: 0x001206AC
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			byte[] array = input.ReadBytes(17);
			if (array.Length < 17)
			{
				__Error.EndOfFile();
			}
			this.majorVersion = SerializationHeaderRecord.GetInt32(array, 9);
			if (this.majorVersion > this.binaryFormatterMajorVersion)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFormat", new object[]
				{
					BitConverter.ToString(array)
				}));
			}
			this.binaryHeaderEnum = (BinaryHeaderEnum)array[0];
			this.topId = SerializationHeaderRecord.GetInt32(array, 1);
			this.headerId = SerializationHeaderRecord.GetInt32(array, 5);
			this.minorVersion = SerializationHeaderRecord.GetInt32(array, 13);
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x0012253A File Offset: 0x0012073A
		public void Dump()
		{
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x0012253C File Offset: 0x0012073C
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040024FF RID: 9471
		internal int binaryFormatterMajorVersion = 1;

		// Token: 0x04002500 RID: 9472
		internal int binaryFormatterMinorVersion;

		// Token: 0x04002501 RID: 9473
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x04002502 RID: 9474
		internal int topId;

		// Token: 0x04002503 RID: 9475
		internal int headerId;

		// Token: 0x04002504 RID: 9476
		internal int majorVersion;

		// Token: 0x04002505 RID: 9477
		internal int minorVersion;
	}
}
