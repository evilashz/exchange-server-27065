using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000F6 RID: 246
	internal class WnsText
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x00018881 File Offset: 0x00016A81
		public WnsText(string text, string lang = null)
		{
			this.Text = text;
			this.Language = lang;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00018897 File Offset: 0x00016A97
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0001889F File Offset: 0x00016A9F
		public string Text { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x000188A8 File Offset: 0x00016AA8
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x000188B0 File Offset: 0x00016AB0
		public string Language { get; private set; }

		// Token: 0x060007F2 RID: 2034 RVA: 0x000188B9 File Offset: 0x00016AB9
		public override string ToString()
		{
			return string.Format("{{text:{0}; lang:{1}}}", this.Text.ToNullableString(), this.Language.ToNullableString());
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000188DC File Offset: 0x00016ADC
		internal void WriteWnsPayload(int id, WnsPayloadWriter wpw)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("id", id);
			ArgumentValidator.ThrowIfNull("wpw", wpw);
			wpw.WriteElementStart("text", true);
			wpw.WriteAttribute("id", id);
			wpw.WriteLanguageAttribute("lang", this.Language, true);
			wpw.WriteAttributesEnd();
			wpw.WriteContent(this.Text);
			wpw.WriteElementEnd();
		}
	}
}
