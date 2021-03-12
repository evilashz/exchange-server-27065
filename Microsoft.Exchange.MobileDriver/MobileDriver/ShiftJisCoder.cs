using System;
using System.Text;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000014 RID: 20
	internal sealed class ShiftJisCoder : CoderBase
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003997 File Offset: 0x00001B97
		public override CodingScheme CodingScheme
		{
			get
			{
				return CodingScheme.ShiftJis;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000399C File Offset: 0x00001B9C
		public override int GetCodedRadixCount(char ch)
		{
			int result;
			try
			{
				result = ShiftJisCoder.encoding.GetByteCount(new char[]
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

		// Token: 0x04000029 RID: 41
		private static Encoding encoding = Encoding.GetEncoding("shift-jis", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
	}
}
