using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000137 RID: 311
	internal class EmailPrompt : TTSPrompt<EmailNormalizedText>
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x0002574C File Offset: 0x0002394C
		public EmailPrompt()
		{
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00025754 File Offset: 0x00023954
		internal EmailPrompt(string promptName, CultureInfo culture, EmailNormalizedText value) : base(promptName, culture, value)
		{
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00025760 File Offset: 0x00023960
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"email",
				base.Config.PromptName,
				string.Empty,
				base.RawText.Substring(0, Math.Min(128, base.RawText.Length))
			});
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000257C6 File Offset: 0x000239C6
		protected override void InternalInitialize()
		{
			base.InternalInitialize();
			base.RawText = ((base.InitVal == null) ? string.Empty : base.InitVal.ToString());
			base.Text = this.AddProsodyWithVolume(base.RawText);
		}
	}
}
