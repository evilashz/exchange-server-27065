using System;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200027F RID: 639
	internal abstract class PeopleSearchRequestBehavior : MobileSpeechRecoRequestBehavior
	{
		// Token: 0x06001308 RID: 4872 RVA: 0x00055122 File Offset: 0x00053322
		public PeopleSearchRequestBehavior(Guid id, CultureInfo culture, Guid userObjectGuid, Guid tenantGuid) : base(id, culture, userObjectGuid, tenantGuid)
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering PeopleSearchRequestBehavior constructor", new object[0]);
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00055146 File Offset: 0x00053346
		protected override MobileSpeechRecoResultType[] SupportedResultTypes
		{
			get
			{
				return PeopleSearchRequestBehavior.supportedResultTypes;
			}
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00055150 File Offset: 0x00053350
		protected override bool ShouldAcceptBasedOnSmartConfidenceThreshold(IUMRecognitionPhrase phrase, MobileSpeechRecoResultType resultType)
		{
			switch (resultType)
			{
			case MobileSpeechRecoResultType.FindPeople:
				return phrase.ShouldAcceptBasedOnSmartConfidence(MobileSpeechRecoRequestBehavior.GetKeywordsFromGrammar("grFindPersonByNameMobile", base.Culture));
			case MobileSpeechRecoResultType.EmailPeople:
				return phrase.ShouldAcceptBasedOnSmartConfidence(MobileSpeechRecoRequestBehavior.GetKeywordsFromGrammar("grEmailPersonByNameMobile", base.Culture));
			default:
				return base.ShouldAcceptBasedOnSmartConfidenceThreshold(phrase, resultType);
			}
		}

		// Token: 0x04000C48 RID: 3144
		private static readonly MobileSpeechRecoResultType[] supportedResultTypes = new MobileSpeechRecoResultType[]
		{
			MobileSpeechRecoResultType.FindPeople,
			MobileSpeechRecoResultType.EmailPeople
		};
	}
}
