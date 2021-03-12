using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000012 RID: 18
	internal class AddressPrompt : TextPrompt
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000570B File Offset: 0x0000390B
		public AddressPrompt()
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005713 File Offset: 0x00003913
		public AddressPrompt(string promptName, CultureInfo culture, string value) : base(promptName, culture, value)
		{
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005720 File Offset: 0x00003920
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"address",
				base.Config.PromptName,
				string.Empty,
				base.Text.ToString()
			});
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005770 File Offset: 0x00003970
		internal override string ToSsml()
		{
			return this.AddProsodyWithVolume("<say-as type=\"address\">" + Regex.Replace(base.Text ?? string.Empty, "(?<building>\\d\\d+)/(?<room>\\d\\d\\d\\d+)", "${building} / ${room}") + "</say-as>");
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000057A5 File Offset: 0x000039A5
		protected override void SanitizeRawText()
		{
			base.SanitizeRawText();
			base.RawText = Util.SanitizeStringForSayAs(base.RawText);
		}
	}
}
