using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000136 RID: 310
	internal class EmailNormalizedText
	{
		// Token: 0x060008A6 RID: 2214 RVA: 0x0002571F File Offset: 0x0002391F
		internal EmailNormalizedText(string inText)
		{
			inText = (inText ?? string.Empty);
			this.text = SpeechUtils.XmlEncode(Util.EmailNormalize(inText));
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00025744 File Offset: 0x00023944
		public override string ToString()
		{
			return this.text;
		}

		// Token: 0x040008B2 RID: 2226
		private string text;
	}
}
