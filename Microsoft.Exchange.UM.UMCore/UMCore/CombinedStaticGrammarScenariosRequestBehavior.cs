using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200027B RID: 635
	internal class CombinedStaticGrammarScenariosRequestBehavior : CombinedGrammarScenariosRequestBehavior
	{
		// Token: 0x060012EC RID: 4844 RVA: 0x00054914 File Offset: 0x00052B14
		public CombinedStaticGrammarScenariosRequestBehavior(Guid id, CultureInfo culture, Guid userObjectGuid, Guid tenantGuid, ExTimeZone timeZone) : base(id, culture, userObjectGuid, tenantGuid, timeZone)
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering CombinedStaticGrammarScenariosRequestBehavior constructor", new object[0]);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0005493C File Offset: 0x00052B3C
		protected override void InitializeScenarioBehaviors(Guid id, CultureInfo culture, Guid userObjectGuid, Guid tenantGuid, ExTimeZone timezone)
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering CombinedStaticScenariosRequestBehavior.InitializeScenarioBehaviors", new object[0]);
			base.ScenarioRequestBehaviors = new List<MobileSpeechRecoRequestBehavior>();
			base.ScenarioRequestBehaviors.Add(new DaySearchBehavior(Guid.NewGuid(), culture, userObjectGuid, tenantGuid, timezone));
			base.ScenarioRequestBehaviors.Add(new DateTimeAndDurationRecognitionBehavior(Guid.NewGuid(), culture, userObjectGuid, tenantGuid, timezone));
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x000549A1 File Offset: 0x00052BA1
		protected override MobileSpeechRecoResultType[] SupportedResultTypes
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
