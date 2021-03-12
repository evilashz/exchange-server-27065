using System;
using System.Text;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000013 RID: 19
	internal sealed class Iso_8859_8Coder : CoderBase
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003933 File Offset: 0x00001B33
		public override CodingScheme CodingScheme
		{
			get
			{
				return CodingScheme.Iso_8859_8;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003938 File Offset: 0x00001B38
		public override int GetCodedRadixCount(char ch)
		{
			int result;
			try
			{
				result = Iso_8859_8Coder.encoding.GetByteCount(new char[]
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

		// Token: 0x04000028 RID: 40
		private static Encoding encoding = Encoding.GetEncoding("iso-8859-8", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
	}
}
