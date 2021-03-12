using System;
using System.Net;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000446 RID: 1094
	internal class Exch50Reader
	{
		// Token: 0x0600327F RID: 12927 RVA: 0x000C5B20 File Offset: 0x000C3D20
		public Exch50Reader(byte[] data, int start, int length)
		{
			this.data = data;
			this.current = start;
			this.end = start + length;
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x000C5B40 File Offset: 0x000C3D40
		public bool ReadNextBlob()
		{
			if (this.current >= this.end)
			{
				return false;
			}
			this.blobLength = this.ReadNetworkInt32();
			if (this.blobLength < 0 || this.blobLength > this.end - this.current)
			{
				throw new Exch50Exception("invalid blob length");
			}
			this.blobOffset = this.current;
			this.current += this.blobLength + 3 >> 2 << 2;
			return true;
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x000C5BB8 File Offset: 0x000C3DB8
		public MdbefPropertyCollection GetMdbefProperties()
		{
			if (this.blobLength == 0)
			{
				return null;
			}
			return MdbefPropertyCollection.Create(this.data, this.blobOffset, this.blobLength);
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000C5BDC File Offset: 0x000C3DDC
		private int ReadNetworkInt32()
		{
			if (4 > this.end - this.current)
			{
				throw new Exch50Exception("unexpected end of data");
			}
			int result = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(this.data, this.current));
			this.current += 4;
			return result;
		}

		// Token: 0x0400199A RID: 6554
		private byte[] data;

		// Token: 0x0400199B RID: 6555
		private int current;

		// Token: 0x0400199C RID: 6556
		private int end;

		// Token: 0x0400199D RID: 6557
		private int blobOffset;

		// Token: 0x0400199E RID: 6558
		private int blobLength;
	}
}
