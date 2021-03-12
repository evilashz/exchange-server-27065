using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x0200008A RID: 138
	public abstract class ByteEncoder : IDisposable
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x0001F578 File Offset: 0x0001D778
		public static ByteEncoder GetEncoder(string name)
		{
			if (string.Equals(ByteEncoder.Base64, name, StringComparison.OrdinalIgnoreCase))
			{
				return new Base64Encoder();
			}
			if (string.Equals(ByteEncoder.QuotedPrintable, name, StringComparison.OrdinalIgnoreCase))
			{
				return new QPEncoder();
			}
			if (string.Equals(ByteEncoder.UUEncode, name, StringComparison.OrdinalIgnoreCase) || string.Equals(ByteEncoder.UUEncodeAlt1, name, StringComparison.OrdinalIgnoreCase) || string.Equals(ByteEncoder.UUEncodeAlt2, name, StringComparison.OrdinalIgnoreCase))
			{
				return new UUEncoder();
			}
			if (string.Equals(ByteEncoder.BinHex, name, StringComparison.OrdinalIgnoreCase))
			{
				return new BinHexEncoder();
			}
			throw new NotSupportedException(name);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		public static ByteEncoder GetDecoder(string name)
		{
			if (string.Equals(ByteEncoder.Base64, name, StringComparison.OrdinalIgnoreCase))
			{
				return new Base64Decoder();
			}
			if (string.Equals(ByteEncoder.QuotedPrintable, name, StringComparison.OrdinalIgnoreCase))
			{
				return new QPDecoder();
			}
			if (string.Equals(ByteEncoder.UUEncode, name, StringComparison.OrdinalIgnoreCase) || string.Equals(ByteEncoder.UUEncodeAlt1, name, StringComparison.OrdinalIgnoreCase) || string.Equals(ByteEncoder.UUEncodeAlt2, name, StringComparison.OrdinalIgnoreCase))
			{
				return new UUDecoder();
			}
			if (string.Equals(ByteEncoder.BinHex, name, StringComparison.OrdinalIgnoreCase))
			{
				return new BinHexDecoder();
			}
			throw new NotSupportedException(name);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001F678 File Offset: 0x0001D878
		public void Convert(Stream sourceStream, Stream destinationStream)
		{
			if (sourceStream == null)
			{
				throw new ArgumentNullException("sourceStream");
			}
			Stream stream = new EncoderStream(destinationStream, this, EncoderStreamAccess.Write);
			byte[] array = new byte[4096];
			for (;;)
			{
				int num = sourceStream.Read(array, 0, array.Length);
				if (num == 0)
				{
					break;
				}
				stream.Write(array, 0, num);
			}
			stream.Flush();
		}

		// Token: 0x060005B9 RID: 1465
		public abstract void Convert(byte[] input, int inputIndex, int inputSize, byte[] output, int outputIndex, int outputSize, bool done, out int inputUsed, out int outputUsed, out bool completed);

		// Token: 0x060005BA RID: 1466
		public abstract int GetMaxByteCount(int dataCount);

		// Token: 0x060005BB RID: 1467 RVA: 0x0001F6C7 File Offset: 0x0001D8C7
		public virtual ByteEncoder Clone()
		{
			throw new NotSupportedException(EncodersStrings.ThisEncoderDoesNotSupportCloning(base.GetType().ToString()));
		}

		// Token: 0x060005BC RID: 1468
		public abstract void Reset();

		// Token: 0x060005BD RID: 1469 RVA: 0x0001F6DE File Offset: 0x0001D8DE
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001F6ED File Offset: 0x0001D8ED
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001F6EF File Offset: 0x0001D8EF
		internal static bool IsWhiteSpace(byte bT)
		{
			return bT <= 32 && 0 != (byte)(ByteEncoder.Tables.CharacterTraits[(int)bT] & ByteEncoder.Tables.CharClasses.WSp);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001F708 File Offset: 0x0001D908
		internal static bool BeginsWithNI(byte[] bytes, int offset, byte[] prefix, int length)
		{
			uint num = 0U;
			while ((ulong)num < (ulong)((long)length) && (ulong)num < (ulong)((long)prefix.Length))
			{
				if (bytes[(int)(checked((IntPtr)(unchecked((long)offset + (long)((ulong)num)))))] != prefix[(int)((UIntPtr)num)] && ((prefix[(int)((UIntPtr)num)] | 32) < 97 || (prefix[(int)((UIntPtr)num)] | 32) > 122 || (bytes[(int)(checked((IntPtr)(unchecked((long)offset + (long)((ulong)num)))))] | 32) != (prefix[(int)((UIntPtr)num)] | 32)))
				{
					return false;
				}
				num += 1U;
			}
			return (ulong)num == (ulong)((long)prefix.Length);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001F770 File Offset: 0x0001D970
		internal unsafe static void BlockCopy(byte[] data, int offset, byte[] newData, int newOffset, int count)
		{
			if (newOffset < 0 || count < 0 || newOffset + count > newData.Length)
			{
				throw new ByteEncoderException("internal error");
			}
			fixed (byte* ptr = data, ptr2 = newData)
			{
				byte* ptr3 = ptr + offset;
				byte* ptr4 = ptr2 + newOffset;
				if (count >= 16)
				{
					int num = count;
					do
					{
						*(int*)ptr4 = *(int*)ptr3;
						*(int*)(ptr4 + 4) = *(int*)(ptr3 + 4);
						*(int*)(ptr4 + 8) = *(int*)(ptr3 + 8);
						*(int*)(ptr4 + 12) = *(int*)(ptr3 + 12);
						ptr4 += 16;
						ptr3 += 16;
					}
					while ((num -= 16) >= 16);
				}
				if (count > 0)
				{
					if ((count & 8) != 0)
					{
						*(int*)ptr4 = *(int*)ptr3;
						*(int*)(ptr4 + 4) = *(int*)(ptr3 + 4);
						ptr4 += 8;
						ptr3 += 8;
					}
					if ((count & 4) != 0)
					{
						*(int*)ptr4 = *(int*)ptr3;
						ptr4 += 4;
						ptr3 += 4;
					}
					if ((count & 2) != 0)
					{
						*(short*)ptr4 = *(short*)ptr3;
						ptr4 += 2;
						ptr3 += 2;
					}
					if ((count & 1) != 0)
					{
						*ptr4 = *ptr3;
					}
				}
			}
		}

		// Token: 0x04000412 RID: 1042
		internal const byte CarriageReturn = 13;

		// Token: 0x04000413 RID: 1043
		internal const byte LineFeed = 10;

		// Token: 0x04000414 RID: 1044
		internal static readonly byte[] LineWrap = new byte[]
		{
			13,
			10
		};

		// Token: 0x04000415 RID: 1045
		internal static readonly byte[] NibbleToHex = CTSGlobals.AsciiEncoding.GetBytes("0123456789ABCDEF");

		// Token: 0x04000416 RID: 1046
		private static readonly string Base64 = "base64";

		// Token: 0x04000417 RID: 1047
		private static readonly string QuotedPrintable = "quoted-printable";

		// Token: 0x04000418 RID: 1048
		private static readonly string UUEncode = "uuencode";

		// Token: 0x04000419 RID: 1049
		private static readonly string UUEncodeAlt1 = "x-uuencode";

		// Token: 0x0400041A RID: 1050
		private static readonly string UUEncodeAlt2 = "x-uue";

		// Token: 0x0400041B RID: 1051
		private static readonly string BinHex = "mac-binhex40";

		// Token: 0x0200008B RID: 139
		internal struct Tables
		{
			// Token: 0x0400041C RID: 1052
			public static readonly byte[] ByteToBase64 = CTSGlobals.AsciiEncoding.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/ ");

			// Token: 0x0400041D RID: 1053
			public static readonly byte[] Base64ToByte = new byte[]
			{
				64,
				64,
				64,
				64,
				64,
				64,
				64,
				64,
				64,
				64,
				64,
				62,
				64,
				64,
				64,
				63,
				52,
				53,
				54,
				55,
				56,
				57,
				58,
				59,
				60,
				61,
				64,
				64,
				64,
				64,
				64,
				64,
				64,
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				14,
				15,
				16,
				17,
				18,
				19,
				20,
				21,
				22,
				23,
				24,
				25,
				64,
				64,
				64,
				64,
				64,
				64,
				26,
				27,
				28,
				29,
				30,
				31,
				32,
				33,
				34,
				35,
				36,
				37,
				38,
				39,
				40,
				41,
				42,
				43,
				44,
				45,
				46,
				47,
				48,
				49,
				50,
				51,
				64,
				64,
				64,
				64,
				64
			};

			// Token: 0x0400041E RID: 1054
			public static readonly byte[] NumFromHex = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				10,
				11,
				12,
				13,
				14,
				15,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				10,
				11,
				12,
				13,
				14,
				15,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue
			};

			// Token: 0x0400041F RID: 1055
			public static readonly ByteEncoder.Tables.CharClasses[] CharacterTraits = new ByteEncoder.Tables.CharClasses[]
			{
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.WSp | ByteEncoder.Tables.CharClasses.QPWSp | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.WSp | ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.WSp | ByteEncoder.Tables.CharClasses.QPWSp | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.WSp | ByteEncoder.Tables.CharClasses.QPWSp,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.QCommentUnsafe,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.QCommentUnsafe,
				ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.QCommentUnsafe,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPhraseUnsafe,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPhraseUnsafe,
				~(ByteEncoder.Tables.CharClasses.WSp | ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPWSp | ByteEncoder.Tables.CharClasses.QEncode | ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.QCommentUnsafe | ByteEncoder.Tables.CharClasses.Token2047),
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPhraseUnsafe,
				ByteEncoder.Tables.CharClasses.QPhraseUnsafe,
				ByteEncoder.Tables.CharClasses.QPhraseUnsafe,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPhraseUnsafe,
				ByteEncoder.Tables.CharClasses.QEncode,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QEncode | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPhraseUnsafe | ByteEncoder.Tables.CharClasses.Token2047,
				ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QEncode
			};

			// Token: 0x0200008C RID: 140
			[Flags]
			internal enum CharClasses : byte
			{
				// Token: 0x04000421 RID: 1057
				WSp = 1,
				// Token: 0x04000422 RID: 1058
				QPEncode = 2,
				// Token: 0x04000423 RID: 1059
				QPUnsafe = 4,
				// Token: 0x04000424 RID: 1060
				QPWSp = 8,
				// Token: 0x04000425 RID: 1061
				QEncode = 16,
				// Token: 0x04000426 RID: 1062
				QPhraseUnsafe = 32,
				// Token: 0x04000427 RID: 1063
				QCommentUnsafe = 64,
				// Token: 0x04000428 RID: 1064
				Token2047 = 128
			}
		}
	}
}
