using System;

namespace Microsoft.Exchange.Security.Compliance
{
	// Token: 0x02000005 RID: 5
	public class HMACMD5ForNonCryptographicPurposes : HMACForNonCryptographicPurposes
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000039F9 File Offset: 0x00001BF9
		public HMACMD5ForNonCryptographicPurposes(byte[] key) : base(key, new MessageDigestForNonCryptographicPurposes())
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00003A07 File Offset: 0x00001C07
		public override bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00003A0A File Offset: 0x00001C0A
		public override bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}
	}
}
