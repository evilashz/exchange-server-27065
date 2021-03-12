using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001F6 RID: 502
	internal class AutoResponseSuppressFormatter
	{
		// Token: 0x06001655 RID: 5717 RVA: 0x0005B368 File Offset: 0x00059568
		public AutoResponseSuppressFormatter()
		{
			this.enumValueToStringMap = new Dictionary<int, string>(64);
			for (int i = 0; i < 64; i++)
			{
				this.enumValueToStringMap.Add(i, ((AutoResponseSuppress)i).ToString());
			}
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0005B3AC File Offset: 0x000595AC
		public string Format(AutoResponseSuppress autoResponseSuppress)
		{
			string result;
			if (this.enumValueToStringMap.TryGetValue((int)autoResponseSuppress, out result))
			{
				return result;
			}
			int num = (int)autoResponseSuppress;
			return num.ToString();
		}

		// Token: 0x04000B28 RID: 2856
		private Dictionary<int, string> enumValueToStringMap;
	}
}
