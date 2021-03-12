using System;
using System.Text;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000011 RID: 17
	internal sealed class Ia5Coder : CoderBase
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003857 File Offset: 0x00001A57
		public override CodingScheme CodingScheme
		{
			get
			{
				return CodingScheme.Ia5;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000385C File Offset: 0x00001A5C
		public override int GetCodedRadixCount(char ch)
		{
			try
			{
				Ia5Coder.encoding.GetByteCount(new char[]
				{
					ch
				});
			}
			catch (EncoderFallbackException)
			{
				return 0;
			}
			int num = Convert.ToInt32(ch);
			if (128 <= num)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x04000026 RID: 38
		private static Encoding encoding = Encoding.GetEncoding("x-IA5", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
	}
}
