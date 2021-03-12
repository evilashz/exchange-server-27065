using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000091 RID: 145
	internal class XHeaderStream : Stream
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x0000C91C File Offset: 0x0000AB1C
		public XHeaderStream(Action<string, string> setHeader)
		{
			Util.ThrowOnNullArgument(setHeader, "setHeader");
			this.setHeader = setHeader;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000C944 File Offset: 0x0000AB44
		public XHeaderStream(Func<string, string> getHeader)
		{
			Util.ThrowOnNullArgument(getHeader, "getHeader");
			this.getHeader = getHeader;
			string text = this.getHeader("X-MS-Exchange-Forest-IndexAgent");
			if (!XHeaderStream.TryParseVersionHeader(text, out this.length))
			{
				throw new InvalidDataException("InvalidVersion: " + text);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		public override bool CanRead
		{
			get
			{
				return this.getHeader != null;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000C9B2 File Offset: 0x0000ABB2
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000C9B5 File Offset: 0x0000ABB5
		public override bool CanWrite
		{
			get
			{
				return this.setHeader != null;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000C9C3 File Offset: 0x0000ABC3
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000C9CB File Offset: 0x0000ABCB
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000C9D2 File Offset: 0x0000ABD2
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000C9DC File Offset: 0x0000ABDC
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Read");
			}
			int num = 0;
			while (count > 0 && this.position != this.length)
			{
				if (this.bufferPosition == this.buffer.Length)
				{
					string text = XHeaderStream.FormatHeaderName(this.currentHeader++);
					string text2 = this.getHeader(text);
					if (string.IsNullOrEmpty(text2))
					{
						throw new IOException("Read:" + text);
					}
					try
					{
						text2 = text2.Replace(" ", string.Empty);
						this.buffer = Convert.FromBase64String(text2);
					}
					catch (FormatException innerException)
					{
						throw new IOException("Base64:" + text, innerException);
					}
					this.bufferPosition = 0;
				}
				int num2 = Math.Min(count, this.buffer.Length - this.bufferPosition);
				Buffer.BlockCopy(this.buffer, this.bufferPosition, buffer, offset, num2);
				this.bufferPosition += num2;
				this.position += (long)num2;
				offset += num2;
				count -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000CB10 File Offset: 0x0000AD10
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000CB17 File Offset: 0x0000AD17
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000CB20 File Offset: 0x0000AD20
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Write");
			}
			if (count > 20000)
			{
				throw new ArgumentException("count");
			}
			string arg = XHeaderStream.FormatHeaderName(this.currentHeader++);
			string text = Convert.ToBase64String(buffer, offset, count);
			int capacity = text.Length + (text.Length + 54 - 1) / 54 - 1;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			int i = text.Length;
			while (i > 0)
			{
				int num = Math.Min(i, 54);
				stringBuilder.Append(text, offset, num);
				offset += num;
				i -= num;
				if (i != 0)
				{
					stringBuilder.Append(' ');
				}
			}
			this.setHeader(arg, stringBuilder.ToString());
			this.length += (long)count;
			this.position += (long)count;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000CC04 File Offset: 0x0000AE04
		public override void Flush()
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Flush");
			}
			string arg = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				1,
				this.length
			});
			this.setHeader("X-MS-Exchange-Forest-IndexAgent", arg);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000CC64 File Offset: 0x0000AE64
		internal static bool IsVersionSupported(string versionString)
		{
			long num;
			return XHeaderStream.TryParseVersionHeader(versionString, out num);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		internal static bool TryParseVersionHeader(string versionString, out long length)
		{
			length = 0L;
			if (string.IsNullOrEmpty(versionString))
			{
				return false;
			}
			string[] array = versionString.Split(null, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 2)
			{
				return false;
			}
			long[] array2 = new long[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!long.TryParse(array[i], NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out array2[i]))
				{
					return false;
				}
			}
			if (array2[0] != 1L)
			{
				return false;
			}
			length = array2[1];
			return true;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		internal static string FormatHeaderName(int headerNumber)
		{
			return string.Format(CultureInfo.InvariantCulture, "X-MS-Exchange-Forest-IndexAgent-{0}", new object[]
			{
				headerNumber
			});
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000CD18 File Offset: 0x0000AF18
		internal void RemoveHeaders()
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("RemoveHeaders");
			}
			this.setHeader("X-MS-Exchange-Forest-IndexAgent", null);
			for (int i = 0; i < this.currentHeader; i++)
			{
				this.setHeader(XHeaderStream.FormatHeaderName(i), null);
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		protected override void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.CanWrite && this.length > 0L)
				{
					this.Flush();
				}
				this.getHeader = null;
				this.setHeader = null;
			}
			base.Dispose(calledFromDispose);
		}

		// Token: 0x040001AB RID: 427
		internal const int MaxBufferSize = 20000;

		// Token: 0x040001AC RID: 428
		internal const int Base64CharsPerLine = 72;

		// Token: 0x040001AD RID: 429
		internal const int BytesPerLine = 54;

		// Token: 0x040001AE RID: 430
		internal const string VersionHeader = "X-MS-Exchange-Forest-IndexAgent";

		// Token: 0x040001AF RID: 431
		private const string Prefix = "X-MS-Exchange-Forest-IndexAgent";

		// Token: 0x040001B0 RID: 432
		private const int VersionNumber = 1;

		// Token: 0x040001B1 RID: 433
		private const string VersionFormat = "{0} {1}";

		// Token: 0x040001B2 RID: 434
		private const string HeaderNameFormat = "X-MS-Exchange-Forest-IndexAgent-{0}";

		// Token: 0x040001B3 RID: 435
		private static readonly byte[] EmptyBuffer = new byte[0];

		// Token: 0x040001B4 RID: 436
		private int currentHeader;

		// Token: 0x040001B5 RID: 437
		private long length;

		// Token: 0x040001B6 RID: 438
		private long position;

		// Token: 0x040001B7 RID: 439
		private int bufferPosition;

		// Token: 0x040001B8 RID: 440
		private byte[] buffer = XHeaderStream.EmptyBuffer;

		// Token: 0x040001B9 RID: 441
		private Func<string, string> getHeader;

		// Token: 0x040001BA RID: 442
		private Action<string, string> setHeader;
	}
}
