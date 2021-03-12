using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200027D RID: 637
	internal class DaySearchBehavior : MowaStaticGrammarRecognitionBehaviorBase
	{
		// Token: 0x060012FB RID: 4859 RVA: 0x00054B3D File Offset: 0x00052D3D
		public DaySearchBehavior(Guid id, CultureInfo culture, Guid userObjectGuid, Guid tenantGuid, ExTimeZone timeZone) : base(id, culture, userObjectGuid, tenantGuid, timeZone)
		{
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x00054B4C File Offset: 0x00052D4C
		public override string MowaGrammarRuleName
		{
			get
			{
				return "DaySearch";
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00054B53 File Offset: 0x00052D53
		public override List<string> TagsToProcess
		{
			get
			{
				return DaySearchBehavior.tagsToProcess;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00054B5A File Offset: 0x00052D5A
		protected override MobileSpeechRecoResultType[] SupportedResultTypes
		{
			get
			{
				return DaySearchBehavior.supportedResultTypes;
			}
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00054B64 File Offset: 0x00052D64
		// Note: this type is marked as 'beforefieldinit'.
		static DaySearchBehavior()
		{
			MobileSpeechRecoResultType[] array = new MobileSpeechRecoResultType[1];
			DaySearchBehavior.supportedResultTypes = array;
		}

		// Token: 0x04000C43 RID: 3139
		private static readonly List<string> tagsToProcess = new List<string>
		{
			"Day",
			"Month",
			"Year",
			"RecoEvent"
		};

		// Token: 0x04000C44 RID: 3140
		private static readonly MobileSpeechRecoResultType[] supportedResultTypes;
	}
}
