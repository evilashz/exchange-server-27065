using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000090 RID: 144
	internal sealed class String8
	{
		// Token: 0x0600039B RID: 923 RVA: 0x0000D356 File Offset: 0x0000B556
		public String8(ArraySegment<byte> encodedBytes)
		{
			this.encodedBytes = encodedBytes;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D365 File Offset: 0x0000B565
		public String8(string resolvedString)
		{
			if (resolvedString == null)
			{
				throw new ArgumentNullException("resolvedString");
			}
			this.resolvedString = resolvedString;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000D382 File Offset: 0x0000B582
		public string StringValue
		{
			get
			{
				if (this.resolvedString == null)
				{
					throw new InvalidOperationException("String8 is unresolved");
				}
				return this.resolvedString;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000D39D File Offset: 0x0000B59D
		public static String8 Parse(Reader reader, bool useUnicode, StringFlags flags)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (useUnicode)
			{
				return new String8(reader.ReadUnicodeString(flags));
			}
			return reader.ReadString8(flags);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
		public static String8 Create(string resolvedString)
		{
			if (resolvedString == null)
			{
				return null;
			}
			return new String8(resolvedString);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
		public override string ToString()
		{
			if (this.resolvedString != null)
			{
				return this.resolvedString;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnresolvedString8: ");
			Util.AppendToString(stringBuilder, this.encodedBytes.Array, this.encodedBytes.Offset, this.encodedBytes.Count);
			return stringBuilder.ToString();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000D430 File Offset: 0x0000B630
		internal void ResolveString8Values(Encoding string8Encoding)
		{
			if (this.resolvedString == null)
			{
				if (string8Encoding == null)
				{
					throw new ArgumentNullException("string8Encoding");
				}
				String8Encodings.ThrowIfInvalidString8Encoding(string8Encoding);
				int num = this.encodedBytes.Count;
				if (num != 0 && this.encodedBytes.Array[this.encodedBytes.Offset + num - 1] == 0)
				{
					num--;
				}
				this.resolvedString = string8Encoding.GetString(this.encodedBytes.Array, this.encodedBytes.Offset, num);
				this.encodedBytes = default(ArraySegment<byte>);
			}
		}

		// Token: 0x04000208 RID: 520
		private ArraySegment<byte> encodedBytes;

		// Token: 0x04000209 RID: 521
		private string resolvedString;
	}
}
