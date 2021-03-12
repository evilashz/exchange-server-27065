using System;
using System.Collections.ObjectModel;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Speech.Recognition;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000056 RID: 86
	internal class UcmaRecognizedWordUnit : IUMRecognizedWord
	{
		// Token: 0x060003CF RID: 975 RVA: 0x00011740 File Offset: 0x0000F940
		internal UcmaRecognizedWordUnit(RecognizedWordUnit recognizedWordUnit, TimeSpan transcriptionResultAudioPosition)
		{
			this.audioPosition = transcriptionResultAudioPosition + recognizedWordUnit.AudioPosition;
			this.audioDuration = recognizedWordUnit.AudioDuration;
			this.text = recognizedWordUnit.Text;
			this.displayAttributes = recognizedWordUnit.DisplayAttributes;
			this.confidence = recognizedWordUnit.Confidence;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00011798 File Offset: 0x0000F998
		internal UcmaRecognizedWordUnit(UcmaReplacementText replacementText, ReadOnlyCollection<RecognizedWordUnit> recognizedWords, TimeSpan transcriptionResultAudioPosition)
		{
			float num = 0f;
			int firstWordIndex = replacementText.FirstWordIndex;
			RecognizedWordUnit recognizedWordUnit = recognizedWords[firstWordIndex];
			RecognizedWordUnit recognizedWordUnit2 = recognizedWords[firstWordIndex + replacementText.CountOfWords - 1];
			this.audioPosition = recognizedWordUnit.AudioPosition + transcriptionResultAudioPosition;
			this.audioDuration = recognizedWordUnit2.AudioPosition + recognizedWordUnit2.AudioDuration - recognizedWordUnit.AudioPosition;
			for (int i = 0; i < replacementText.CountOfWords; i++)
			{
				RecognizedWordUnit recognizedWordUnit3 = recognizedWords[firstWordIndex++];
				num += recognizedWordUnit3.Confidence;
			}
			this.confidence = ((replacementText.CountOfWords > 0) ? (num / (float)replacementText.CountOfWords) : 0f);
			this.text = replacementText.Text;
			this.displayAttributes = replacementText.DisplayAttributes;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00011868 File Offset: 0x0000FA68
		public float Confidence
		{
			get
			{
				return this.confidence;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00011870 File Offset: 0x0000FA70
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00011878 File Offset: 0x0000FA78
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00011881 File Offset: 0x0000FA81
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00011889 File Offset: 0x0000FA89
		public UMDisplayAttributes DisplayAttributes
		{
			get
			{
				return this.displayAttributes;
			}
			set
			{
				this.displayAttributes = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00011892 File Offset: 0x0000FA92
		public TimeSpan AudioPosition
		{
			get
			{
				return this.audioPosition;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001189A File Offset: 0x0000FA9A
		public TimeSpan AudioDuration
		{
			get
			{
				return this.audioDuration;
			}
		}

		// Token: 0x0400012A RID: 298
		private readonly TimeSpan audioPosition;

		// Token: 0x0400012B RID: 299
		private readonly TimeSpan audioDuration;

		// Token: 0x0400012C RID: 300
		private readonly float confidence;

		// Token: 0x0400012D RID: 301
		private string text;

		// Token: 0x0400012E RID: 302
		private DisplayAttributes displayAttributes;
	}
}
