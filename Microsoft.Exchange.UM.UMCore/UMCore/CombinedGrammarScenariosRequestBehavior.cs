using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200027A RID: 634
	internal abstract class CombinedGrammarScenariosRequestBehavior : MobileSpeechRecoRequestBehavior
	{
		// Token: 0x060012E1 RID: 4833 RVA: 0x000546AD File Offset: 0x000528AD
		public CombinedGrammarScenariosRequestBehavior(Guid id, CultureInfo culture, Guid userObjectGuid, Guid tenantGuid, ExTimeZone timezone) : base(id, culture, userObjectGuid, tenantGuid)
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering CombinedGrammarScenariosRequestBehavior constructor", new object[0]);
			this.InitializeScenarioBehaviors(id, culture, userObjectGuid, tenantGuid, timezone);
			this.InitializeMaxAlternates();
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x000546E4 File Offset: 0x000528E4
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x000546EC File Offset: 0x000528EC
		protected List<MobileSpeechRecoRequestBehavior> ScenarioRequestBehaviors { get; set; }

		// Token: 0x060012E4 RID: 4836
		protected abstract void InitializeScenarioBehaviors(Guid id, CultureInfo culture, Guid userObjectGuid, Guid tenantGuid, ExTimeZone timezone);

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x000546F5 File Offset: 0x000528F5
		public override SpeechRecognitionEngineType EngineType
		{
			get
			{
				return SpeechRecognitionEngineType.CmdAndControl;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x000546F8 File Offset: 0x000528F8
		public override int MaxAlternates
		{
			get
			{
				return this.maxAlternates;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00054700 File Offset: 0x00052900
		public override int MaxProcessingTime
		{
			get
			{
				return 60000;
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00054708 File Offset: 0x00052908
		public override List<UMGrammar> PrepareGrammars()
		{
			ValidateArgument.NotNull(this.ScenarioRequestBehaviors, "RequestBehaviorGrammarsToUse");
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering CombinedGrammarScenariosRequestBehavior.PrepareGrammars", new object[0]);
			List<UMGrammar> list = new List<UMGrammar>();
			foreach (MobileSpeechRecoRequestBehavior mobileSpeechRecoRequestBehavior in this.ScenarioRequestBehaviors)
			{
				List<UMGrammar> collection = mobileSpeechRecoRequestBehavior.PrepareGrammars();
				list.AddRange(collection);
			}
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "CombinedGrammarScenariosRequestBehavior.PrepareGrammars: Done with Preparing Grammars", new object[0]);
			return list;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000547A8 File Offset: 0x000529A8
		public override string ProcessRecoResults(List<IMobileRecognitionResult> results)
		{
			ValidateArgument.NotNull(results, "results");
			ValidateArgument.NotNull(this.ScenarioRequestBehaviors, "ScenarioRequestBehaviors");
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering CombinedGrammarScenariosRequestBehavior.ProcessRecoResults", new object[0]);
			if (results.Count > 0)
			{
				foreach (MobileSpeechRecoRequestBehavior mobileSpeechRecoRequestBehavior in this.ScenarioRequestBehaviors)
				{
					if (mobileSpeechRecoRequestBehavior.CanProcessResultType(results[0].MobileScenarioResultType))
					{
						MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Found a request behavior to processRecoResults", new object[0]);
						return mobileSpeechRecoRequestBehavior.ProcessRecoResults(results);
					}
				}
				return this.GenerateEmptyXmlResult(results);
			}
			return this.GenerateEmptyXmlResult(results);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00054878 File Offset: 0x00052A78
		private string GenerateEmptyXmlResult(List<IMobileRecognitionResult> results)
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Cannot find a request behavior to process result. Generating empty xml result", new object[0]);
			List<string> requiredTags = new List<string>();
			return base.ConvertResultsToXml(results, requiredTags);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000548AC File Offset: 0x00052AAC
		private void InitializeMaxAlternates()
		{
			this.maxAlternates = 0;
			foreach (MobileSpeechRecoRequestBehavior mobileSpeechRecoRequestBehavior in this.ScenarioRequestBehaviors)
			{
				this.maxAlternates += mobileSpeechRecoRequestBehavior.MaxAlternates;
			}
		}

		// Token: 0x04000C3C RID: 3132
		private const int MaxProcessingTimeValue = 60000;

		// Token: 0x04000C3D RID: 3133
		private int maxAlternates;
	}
}
