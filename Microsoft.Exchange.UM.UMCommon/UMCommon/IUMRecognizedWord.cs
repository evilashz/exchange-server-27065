using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000B8 RID: 184
	internal interface IUMRecognizedWord
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600066B RID: 1643
		float Confidence { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600066C RID: 1644
		// (set) Token: 0x0600066D RID: 1645
		string Text { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600066E RID: 1646
		// (set) Token: 0x0600066F RID: 1647
		UMDisplayAttributes DisplayAttributes { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000670 RID: 1648
		TimeSpan AudioPosition { get; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000671 RID: 1649
		TimeSpan AudioDuration { get; }
	}
}
