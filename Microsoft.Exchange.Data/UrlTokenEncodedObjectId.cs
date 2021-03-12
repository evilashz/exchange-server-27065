using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002B9 RID: 697
	[Serializable]
	public class UrlTokenEncodedObjectId : ObjectId
	{
		// Token: 0x06001917 RID: 6423 RVA: 0x0004F078 File Offset: 0x0004D278
		public UrlTokenEncodedObjectId(string rawValue)
		{
			this.urlTokenEncodedValue = UrlTokenConverter.UrlTokenEncode(rawValue);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0004F08C File Offset: 0x0004D28C
		public override byte[] GetBytes()
		{
			if (this.urlTokenEncodedValue == null)
			{
				return new byte[0];
			}
			return Encoding.UTF8.GetBytes(this.urlTokenEncodedValue);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0004F0AD File Offset: 0x0004D2AD
		public override string ToString()
		{
			return this.urlTokenEncodedValue;
		}

		// Token: 0x04000ECC RID: 3788
		private readonly string urlTokenEncodedValue;
	}
}
