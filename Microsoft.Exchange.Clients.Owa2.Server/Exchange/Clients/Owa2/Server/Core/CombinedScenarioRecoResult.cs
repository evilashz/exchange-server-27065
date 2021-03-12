using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000378 RID: 888
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CombinedScenarioRecoResult
	{
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0007220A File Offset: 0x0007040A
		// (set) Token: 0x06001C8A RID: 7306 RVA: 0x00072212 File Offset: 0x00070412
		[DataMember]
		public string RequestId { get; set; }

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0007221B File Offset: 0x0007041B
		// (set) Token: 0x06001C8C RID: 7308 RVA: 0x00072223 File Offset: 0x00070423
		[DataMember]
		public string Text { get; set; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x0007222C File Offset: 0x0007042C
		// (set) Token: 0x06001C8E RID: 7310 RVA: 0x00072234 File Offset: 0x00070434
		[DataMember]
		public string JsonResponse { get; set; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x0007223D File Offset: 0x0007043D
		// (set) Token: 0x06001C90 RID: 7312 RVA: 0x00072245 File Offset: 0x00070445
		[IgnoreDataMember]
		public CombinedScenarioResultType ResultType { get; set; }

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x0007224E File Offset: 0x0007044E
		// (set) Token: 0x06001C92 RID: 7314 RVA: 0x00072260 File Offset: 0x00070460
		[DataMember(Name = "ResultType")]
		public string ResultTypeString
		{
			get
			{
				return this.ResultType.ToString();
			}
			set
			{
				this.ResultType = (CombinedScenarioResultType)Enum.Parse(typeof(CombinedScenarioResultType), value);
			}
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0007227D File Offset: 0x0007047D
		public static CombinedScenarioRecoResult JsonDeserialize(string result, Type targetType)
		{
			return (CombinedScenarioRecoResult)SpeechRecognitionResultHandler.JsonDeserialize(result, targetType);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0007228B File Offset: 0x0007048B
		public static string JsonSerialize(object obj)
		{
			return SpeechRecognitionResultHandler.JsonSerialize(obj);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00072293 File Offset: 0x00070493
		public string ToJsonString()
		{
			return CombinedScenarioRecoResult.JsonSerialize(this);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0007229C File Offset: 0x0007049C
		internal static CombinedScenarioResultType MapSpeechRecoResultTypeToCombinedRecoResultType(MobileSpeechRecoResultType resultType)
		{
			switch (resultType)
			{
			case MobileSpeechRecoResultType.DaySearch:
				return CombinedScenarioResultType.DaySearch;
			case MobileSpeechRecoResultType.AppointmentCreation:
				return CombinedScenarioResultType.AppointmentCreation;
			case MobileSpeechRecoResultType.FindPeople:
				return CombinedScenarioResultType.FindPeople;
			case MobileSpeechRecoResultType.EmailPeople:
				return CombinedScenarioResultType.EmailPeople;
			case MobileSpeechRecoResultType.None:
				return CombinedScenarioResultType.None;
			default:
				throw new ArgumentOutOfRangeException("ResultType", resultType, "ResultType does not have a mapping");
			}
		}
	}
}
