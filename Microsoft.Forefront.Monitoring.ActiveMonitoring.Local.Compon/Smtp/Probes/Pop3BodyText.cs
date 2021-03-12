using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000236 RID: 566
	public class Pop3BodyText
	{
		// Token: 0x060012C0 RID: 4800 RVA: 0x000370B0 File Offset: 0x000352B0
		public Pop3BodyText(string contentType, string text)
		{
			this.ContentType = contentType;
			this.Text = text;
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x000370C6 File Offset: 0x000352C6
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x000370CE File Offset: 0x000352CE
		public string ContentType { get; private set; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x000370D7 File Offset: 0x000352D7
		// (set) Token: 0x060012C4 RID: 4804 RVA: 0x000370DF File Offset: 0x000352DF
		public string Text { get; private set; }
	}
}
