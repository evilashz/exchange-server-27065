using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000129 RID: 297
	internal class EmailAddressPrompt : TextPrompt
	{
		// Token: 0x06000848 RID: 2120 RVA: 0x00022DD0 File Offset: 0x00020FD0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"emailAddress",
				base.Config.PromptName,
				string.Empty,
				base.Text.ToString()
			});
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00022E20 File Offset: 0x00021020
		internal override string ToSsml()
		{
			int num = base.Text.IndexOf('@');
			if (-1 != num)
			{
				return this.AddProsodyWithVolume(string.Format(CultureInfo.InvariantCulture, "<say-as interpret-as=\"net\" format=\"email\">{0}</say-as>", new object[]
				{
					base.Text
				}));
			}
			return this.AddProsodyWithVolume(string.Format(CultureInfo.InvariantCulture, "<say-as interpret-as=\"letters\">{0}</say-as>", new object[]
			{
				base.Text
			}));
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00022E8C File Offset: 0x0002108C
		protected override void SanitizeRawText()
		{
			base.SanitizeRawText();
			base.RawText = Util.SanitizeStringForSayAs(base.RawText);
		}

		// Token: 0x04000890 RID: 2192
		private const string SpellFormat = "<say-as interpret-as=\"letters\">{0}</say-as>";

		// Token: 0x04000891 RID: 2193
		private const string SmtpFormat = "<say-as interpret-as=\"net\" format=\"email\">{0}</say-as>";
	}
}
