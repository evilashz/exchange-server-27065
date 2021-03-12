using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000B3 RID: 179
	internal interface ITranscriptionData
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600065A RID: 1626
		RecoResultType RecognitionResult { get; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600065B RID: 1627
		RecoErrorType RecognitionError { get; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600065C RID: 1628
		string ErrorInformation { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600065D RID: 1629
		float Confidence { get; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600065E RID: 1630
		CultureInfo Language { get; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600065F RID: 1631
		ConfidenceBandType ConfidenceBand { get; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000660 RID: 1632
		XmlDocument TranscriptionXml { get; }
	}
}
