using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200044E RID: 1102
	internal class TransportPropertyWriter
	{
		// Token: 0x060032BF RID: 12991 RVA: 0x000C79B1 File Offset: 0x000C5BB1
		public TransportPropertyWriter()
		{
			this.buffer = new List<byte>();
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000C79C4 File Offset: 0x000C5BC4
		public int Length
		{
			get
			{
				return this.buffer.Count;
			}
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x000C79D4 File Offset: 0x000C5BD4
		public void Add(Guid range, uint id, byte[] value)
		{
			this.buffer.AddRange(range.ToByteArray());
			this.buffer.AddRange(BitConverter.GetBytes(id));
			this.buffer.AddRange(BitConverter.GetBytes(value.Length));
			this.buffer.AddRange(value);
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000C7A23 File Offset: 0x000C5C23
		public byte[] GetBytes()
		{
			return this.buffer.ToArray();
		}

		// Token: 0x040019B7 RID: 6583
		private List<byte> buffer;
	}
}
