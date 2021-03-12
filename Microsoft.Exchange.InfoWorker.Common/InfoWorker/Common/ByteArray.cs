using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000006 RID: 6
	internal sealed class ByteArray
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002474 File Offset: 0x00000674
		public ByteArray(byte[] bytes)
		{
			this.bytes = bytes;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002484 File Offset: 0x00000684
		public override string ToString()
		{
			if (this.bytes == null)
			{
				return "<null>";
			}
			if (this.bytes.Length > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(this.bytes.Length * 2);
				for (int i = 0; i < this.bytes.Length; i++)
				{
					stringBuilder.Append(this.bytes[i].ToString("X2"));
				}
				return stringBuilder.ToString();
			}
			return "<empty>";
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024F6 File Offset: 0x000006F6
		internal byte[] Bytes
		{
			get
			{
				return this.bytes;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002500 File Offset: 0x00000700
		internal static ByteArray Parse(string hexString)
		{
			if (hexString == null)
			{
				throw new ArgumentNullException("hexString");
			}
			if (hexString.Length == 0 || hexString.Length % 2 != 0)
			{
				throw new ArgumentException("Invalid hex encoded string", "hexString");
			}
			byte[] array = new byte[hexString.Length / 2];
			for (int i = 0; i < hexString.Length; i += 2)
			{
				string s = hexString.Substring(i, 2);
				byte b;
				if (!byte.TryParse(s, NumberStyles.HexNumber, null, out b))
				{
					throw new ArgumentException("Invalid hex encoded string", "hexString");
				}
				array[i / 2] = b;
			}
			return new ByteArray(array);
		}

		// Token: 0x04000019 RID: 25
		private byte[] bytes;
	}
}
