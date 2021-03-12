using System;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000011 RID: 17
	internal class TextPrompt : TTSPrompt<string>
	{
		// Token: 0x0600010D RID: 269 RVA: 0x0000563A File Offset: 0x0000383A
		public TextPrompt()
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005642 File Offset: 0x00003842
		internal TextPrompt(string promptName, CultureInfo culture, string value) : base(promptName, culture, value)
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005650 File Offset: 0x00003850
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"text",
				base.Config.PromptName,
				string.Empty,
				base.RawText.Substring(0, Math.Min(128, base.RawText.Length))
			});
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000056B6 File Offset: 0x000038B6
		internal override string ToSsml()
		{
			return this.AddProsodyWithVolume(base.Text);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000056C4 File Offset: 0x000038C4
		protected override void InternalInitialize()
		{
			base.InternalInitialize();
			this.SanitizeRawText();
			base.Text = Util.TextNormalize(SpeechUtils.XmlEncode(base.RawText));
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000056E8 File Offset: 0x000038E8
		protected virtual void SanitizeRawText()
		{
			base.RawText = ((base.InitVal == null) ? string.Empty : base.InitVal.TrimEnd(null));
		}
	}
}
