using System;
using System.Collections.Generic;
using Microsoft.Exchange.TextMessaging.MobileDriver.Resources;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200000D RID: 13
	internal abstract class CoderBase : ICoder
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004E RID: 78
		public abstract CodingScheme CodingScheme { get; }

		// Token: 0x0600004F RID: 79 RVA: 0x0000378C File Offset: 0x0000198C
		public CodedText Code(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentNullException("str");
			}
			if (this.CodingScheme == CodingScheme.Neutral)
			{
				throw new MobileDriverFatalErrorException(Strings.ErrorNeutralCodingScheme);
			}
			List<int> list = new List<int>(str.Length);
			int num = 0;
			while (str.Length > num)
			{
				list.Insert(num, this.GetCodedRadixCount(str[num]));
				num++;
			}
			return new CodedText(this.CodingScheme, str, list.AsReadOnly());
		}

		// Token: 0x06000050 RID: 80
		public abstract int GetCodedRadixCount(char ch);
	}
}
