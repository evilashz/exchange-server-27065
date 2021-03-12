using System;
using System.Text;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000015 RID: 21
	internal sealed class EucKrCoder : CoderBase
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000039FB File Offset: 0x00001BFB
		public override CodingScheme CodingScheme
		{
			get
			{
				return CodingScheme.EucKr;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A00 File Offset: 0x00001C00
		public override int GetCodedRadixCount(char ch)
		{
			int result;
			try
			{
				result = EucKrCoder.encoding.GetByteCount(new char[]
				{
					ch
				});
			}
			catch (EncoderFallbackException)
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0400002A RID: 42
		private static Encoding encoding = Encoding.GetEncoding("euc-kr", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
	}
}
