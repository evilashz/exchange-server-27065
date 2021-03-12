using System;
using System.Text;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000012 RID: 18
	internal sealed class Iso_8859_1Coder : CoderBase
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000038CF File Offset: 0x00001ACF
		public override CodingScheme CodingScheme
		{
			get
			{
				return CodingScheme.Iso_8859_1;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000038D4 File Offset: 0x00001AD4
		public override int GetCodedRadixCount(char ch)
		{
			int result;
			try
			{
				result = Iso_8859_1Coder.encoding.GetByteCount(new char[]
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

		// Token: 0x04000027 RID: 39
		private static Encoding encoding = Encoding.GetEncoding("iso-8859-1", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
	}
}
