using System;
using System.Text;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x0200001D RID: 29
	internal struct DataReference
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00005010 File Offset: 0x00003210
		internal DataReference(byte[] buffer, int offset, int length)
		{
			this.buffer = buffer;
			this.offset = offset;
			this.length = length;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005027 File Offset: 0x00003227
		internal DataReference(string data)
		{
			this.buffer = Encoding.ASCII.GetBytes(data);
			this.offset = 0;
			this.length = this.buffer.Length;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000504F File Offset: 0x0000324F
		public override string ToString()
		{
			return Encoding.ASCII.GetString(this.buffer, this.offset, this.length);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005070 File Offset: 0x00003270
		public int CompareTo(DataReference other)
		{
			int num = Math.Min(this.length, other.length);
			int num2 = this.offset;
			int num3 = other.offset;
			for (int i = 0; i < num; i++)
			{
				char c = char.ToLower((char)this.buffer[num2 + i]);
				char c2 = char.ToLower((char)other.buffer[num3 + i]);
				int num4 = (int)(c - c2);
				if (num4 != 0)
				{
					return num4;
				}
			}
			return this.length - other.length;
		}

		// Token: 0x04000069 RID: 105
		private byte[] buffer;

		// Token: 0x0400006A RID: 106
		private int offset;

		// Token: 0x0400006B RID: 107
		private int length;
	}
}
