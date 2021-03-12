using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.UM.Prompts.Config;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000168 RID: 360
	internal class LocConfig
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x0002CF18 File Offset: 0x0002B118
		private LocConfig()
		{
			this.languages = new Dictionary<CultureInfo, LocConfig.LanguageConfig>(32);
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0002CF30 File Offset: 0x0002B130
		public static LocConfig Instance
		{
			get
			{
				if (LocConfig.instance == null)
				{
					lock (LocConfig.lockObj)
					{
						if (LocConfig.instance == null)
						{
							LocConfig.instance = new LocConfig();
						}
					}
				}
				return LocConfig.instance;
			}
		}

		// Token: 0x170002A7 RID: 679
		public LocConfig.LanguageConfig this[CultureInfo c]
		{
			get
			{
				c = (c ?? CultureInfo.InvariantCulture);
				if (!this.languages.ContainsKey(c))
				{
					lock (LocConfig.lockObj)
					{
						if (!this.languages.ContainsKey(c))
						{
							LocConfig.instance.languages.Add(c, LocConfig.LanguageConfig.Load(c));
						}
					}
				}
				return this.languages[c];
			}
		}

		// Token: 0x0400096E RID: 2414
		private static object lockObj = new object();

		// Token: 0x0400096F RID: 2415
		private static LocConfig instance;

		// Token: 0x04000970 RID: 2416
		private Dictionary<CultureInfo, LocConfig.LanguageConfig> languages;

		// Token: 0x02000169 RID: 361
		internal class LanguageConfig
		{
			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002D018 File Offset: 0x0002B218
			// (set) Token: 0x06000A95 RID: 2709 RVA: 0x0002D020 File Offset: 0x0002B220
			public LocConfig.TranscriptionConfig Transcription { get; private set; }

			// Token: 0x170002A9 RID: 681
			// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0002D029 File Offset: 0x0002B229
			// (set) Token: 0x06000A97 RID: 2711 RVA: 0x0002D031 File Offset: 0x0002B231
			public LocConfig.GeneralConfig General { get; private set; }

			// Token: 0x170002AA RID: 682
			// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0002D03A File Offset: 0x0002B23A
			// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0002D042 File Offset: 0x0002B242
			public LocConfig.MowaSpeechConfig MowaSpeech { get; private set; }

			// Token: 0x06000A9A RID: 2714 RVA: 0x0002D04C File Offset: 0x0002B24C
			public static LocConfig.LanguageConfig Load(CultureInfo c)
			{
				return new LocConfig.LanguageConfig
				{
					General = LocConfig.GeneralConfig.Load(c),
					Transcription = LocConfig.TranscriptionConfig.Load(c),
					MowaSpeech = LocConfig.MowaSpeechConfig.Load(c)
				};
			}
		}

		// Token: 0x0200016A RID: 362
		internal class GeneralConfig
		{
			// Token: 0x06000A9C RID: 2716 RVA: 0x0002D08C File Offset: 0x0002B28C
			private GeneralConfig()
			{
			}

			// Token: 0x170002AB RID: 683
			// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0002D094 File Offset: 0x0002B294
			// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0002D09C File Offset: 0x0002B29C
			public int TTSVolume { get; private set; }

			// Token: 0x170002AC RID: 684
			// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0002D0A5 File Offset: 0x0002B2A5
			// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0002D0AD File Offset: 0x0002B2AD
			public bool SmartReadingHours { get; private set; }

			// Token: 0x06000AA1 RID: 2721 RVA: 0x0002D0B8 File Offset: 0x0002B2B8
			public static LocConfig.GeneralConfig Load(CultureInfo c)
			{
				LocConfig.GeneralConfig generalConfig = new LocConfig.GeneralConfig();
				generalConfig.TTSVolume = SafeConvert.ToInt32(Strings.TtsVolume.ToString(c), 0, 200, 100);
				int num = SafeConvert.ToInt32(Strings.SmartReadingHours.ToString(c), -1, 1, 0);
				generalConfig.SmartReadingHours = (1 == num || (num != 0 && c.Name == "en-US"));
				return generalConfig;
			}
		}

		// Token: 0x0200016B RID: 363
		internal class TranscriptionConfig
		{
			// Token: 0x06000AA2 RID: 2722 RVA: 0x0002D121 File Offset: 0x0002B321
			private TranscriptionConfig()
			{
			}

			// Token: 0x170002AD RID: 685
			// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0002D129 File Offset: 0x0002B329
			// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x0002D131 File Offset: 0x0002B331
			public double HighConfidence { get; private set; }

			// Token: 0x170002AE RID: 686
			// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0002D13A File Offset: 0x0002B33A
			// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0002D142 File Offset: 0x0002B342
			public double LowConfidence { get; private set; }

			// Token: 0x170002AF RID: 687
			// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0002D14B File Offset: 0x0002B34B
			// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x0002D153 File Offset: 0x0002B353
			public bool CapStartOfNewSentence { get; private set; }

			// Token: 0x170002B0 RID: 688
			// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0002D15C File Offset: 0x0002B35C
			// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0002D164 File Offset: 0x0002B364
			public TimeSpan SilenceThreshold { get; private set; }

			// Token: 0x170002B1 RID: 689
			// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0002D16D File Offset: 0x0002B36D
			// (set) Token: 0x06000AAC RID: 2732 RVA: 0x0002D175 File Offset: 0x0002B375
			public int NumSentencesPerParagraph { get; private set; }

			// Token: 0x170002B2 RID: 690
			// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0002D17E File Offset: 0x0002B37E
			// (set) Token: 0x06000AAE RID: 2734 RVA: 0x0002D186 File Offset: 0x0002B386
			public bool LastSentenceInNewLine { get; private set; }

			// Token: 0x170002B3 RID: 691
			// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0002D18F File Offset: 0x0002B38F
			// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x0002D197 File Offset: 0x0002B397
			public bool FirstSentenceInNewLine { get; private set; }

			// Token: 0x06000AB1 RID: 2737 RVA: 0x0002D1A0 File Offset: 0x0002B3A0
			public static LocConfig.TranscriptionConfig Load(CultureInfo c)
			{
				return new LocConfig.TranscriptionConfig
				{
					HighConfidence = SafeConvert.ToDouble(Strings.TranscriptionHighConfidence.ToString(c), 0.0, 1.0, 0.75),
					LowConfidence = SafeConvert.ToDouble(Strings.TranscriptionLowConfidence.ToString(c), 0.0, 1.0, 0.3),
					CapStartOfNewSentence = SafeConvert.ToBoolean(Strings.CapStartOfNewSentence.ToString(c), true),
					SilenceThreshold = TimeSpan.FromMilliseconds(SafeConvert.ToDouble(Strings.SilenceThreshold.ToString(c), 100.0, 5000.0, 400.0)),
					LastSentenceInNewLine = SafeConvert.ToBoolean(Strings.LastSentenceInNewLine.ToString(c), true),
					FirstSentenceInNewLine = SafeConvert.ToBoolean(Strings.FirstSentenceInNewLine.ToString(c), true),
					NumSentencesPerParagraph = SafeConvert.ToInt32(Strings.NumSentencesPerParagraph.ToString(c), 1, int.MaxValue, 3)
				};
			}
		}

		// Token: 0x0200016C RID: 364
		internal class MowaSpeechConfig
		{
			// Token: 0x06000AB2 RID: 2738 RVA: 0x0002D2AE File Offset: 0x0002B4AE
			private MowaSpeechConfig()
			{
			}

			// Token: 0x170002B4 RID: 692
			// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0002D2B6 File Offset: 0x0002B4B6
			// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0002D2BE File Offset: 0x0002B4BE
			public double MowaVoiceImmediateThreshold { get; private set; }

			// Token: 0x170002B5 RID: 693
			// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0002D2C7 File Offset: 0x0002B4C7
			// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x0002D2CF File Offset: 0x0002B4CF
			public bool EnableMowaVoice { get; private set; }

			// Token: 0x06000AB7 RID: 2743 RVA: 0x0002D2D8 File Offset: 0x0002B4D8
			public static LocConfig.MowaSpeechConfig Load(CultureInfo c)
			{
				return new LocConfig.MowaSpeechConfig
				{
					MowaVoiceImmediateThreshold = SafeConvert.ToDouble(Strings.MowaVoiceImmediateThreshold.ToString(c), 0.0, 1.0, 0.4),
					EnableMowaVoice = SafeConvert.ToBoolean(Strings.EnableMowaVoice.ToString(c), false)
				};
			}
		}
	}
}
