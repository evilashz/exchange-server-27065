using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000190 RID: 400
	internal class NamePrompt : TextPrompt
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x00033044 File Offset: 0x00031244
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"name",
				base.Config.PromptName,
				string.Empty,
				base.Text
			});
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0003308F File Offset: 0x0003128F
		internal override string ToSsml()
		{
			return this.AddProsodyWithVolume("<say-as type=\"name\">" + base.Text.Replace('.', ' ') + "</say-as>");
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000330B5 File Offset: 0x000312B5
		protected override void SanitizeRawText()
		{
			base.SanitizeRawText();
			base.RawText = Util.SanitizeStringForSayAs(base.RawText);
		}
	}
}
