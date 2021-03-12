using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200027C RID: 636
	internal abstract class MowaStaticGrammarRecognitionBehaviorBase : MobileSpeechRecoRequestBehavior
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x000549A8 File Offset: 0x00052BA8
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x000549B0 File Offset: 0x00052BB0
		private protected ExTimeZone TimeZone { protected get; private set; }

		// Token: 0x060012F1 RID: 4849 RVA: 0x000549B9 File Offset: 0x00052BB9
		public MowaStaticGrammarRecognitionBehaviorBase(Guid id, CultureInfo culture, Guid userObjectGuid, Guid tenantGuid, ExTimeZone timeZone) : base(id, culture, userObjectGuid, tenantGuid)
		{
			this.TimeZone = timeZone;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x000549CE File Offset: 0x00052BCE
		public override SpeechRecognitionEngineType EngineType
		{
			get
			{
				return SpeechRecognitionEngineType.CmdAndControl;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x000549D1 File Offset: 0x00052BD1
		public override int MaxAlternates
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x000549D4 File Offset: 0x00052BD4
		public override int MaxProcessingTime
		{
			get
			{
				return 20000;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060012F5 RID: 4853
		public abstract string MowaGrammarRuleName { get; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060012F6 RID: 4854
		public abstract List<string> TagsToProcess { get; }

		// Token: 0x060012F7 RID: 4855 RVA: 0x000549DC File Offset: 0x00052BDC
		public override List<UMGrammar> PrepareGrammars()
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering MowaStaticGrammarRecognitionBehaviorBase.PrepareGrammars.", new object[0]);
			List<UMGrammar> list = new List<UMGrammar>();
			UMGrammarConfig umgrammarConfig = new StaticUmGrammarConfig("Mowascenarios.grxml", this.MowaGrammarRuleName, string.Empty, null);
			UMGrammar grammar = umgrammarConfig.GetGrammar(null, base.Culture);
			ExDateTime exDateTime = new ExDateTime(this.TimeZone, DateTime.UtcNow);
			int num = (int)(-(int)exDateTime.Bias.TotalMinutes);
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Client time zone offset (minutes) is {0}.", new object[]
			{
				num
			});
			grammar.Script = string.Format(CultureInfo.InvariantCulture, MowaStaticGrammarRecognitionBehaviorBase.InitializationScriptTemplate, new object[]
			{
				num
			});
			list.Add(grammar);
			return list;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00054AA9 File Offset: 0x00052CA9
		public override string ProcessRecoResults(List<IMobileRecognitionResult> results)
		{
			ValidateArgument.NotNull(results, "results");
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering MowaStaticGrammarRecognitionBehaviorBase.ProcessRecoResults", new object[0]);
			return base.ConvertResultsToXml(results, this.TagsToProcess);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00054ADC File Offset: 0x00052CDC
		protected override bool ShouldAcceptBasedOnSmartConfidenceThreshold(IUMRecognitionPhrase phrase, MobileSpeechRecoResultType resultType)
		{
			switch (resultType)
			{
			case MobileSpeechRecoResultType.DaySearch:
				return phrase.ShouldAcceptBasedOnSmartConfidence(MobileSpeechRecoRequestBehavior.GetKeywordsFromGrammar("grCalendarDaySearch", base.Culture));
			case MobileSpeechRecoResultType.AppointmentCreation:
				return phrase.ShouldAcceptBasedOnSmartConfidence(MobileSpeechRecoRequestBehavior.GetKeywordsFromGrammar("grCalendarDayNewAppointment", base.Culture));
			default:
				return base.ShouldAcceptBasedOnSmartConfidenceThreshold(phrase, resultType);
			}
		}

		// Token: 0x04000C3F RID: 3135
		private const int MaxAlternatesValue = 1;

		// Token: 0x04000C40 RID: 3136
		private const int MaxProcessingTimeValue = 20000;

		// Token: 0x04000C41 RID: 3137
		private static readonly string InitializationScriptTemplate = "var st = new Date(); $.ClientToday = new Date(st.getTime() - 60000 * ({0} - st.getTimezoneOffset()));";
	}
}
