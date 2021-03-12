using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200037A RID: 890
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DayTimeDurationRecoResult
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x000722EE File Offset: 0x000704EE
		// (set) Token: 0x06001C99 RID: 7321 RVA: 0x000722F6 File Offset: 0x000704F6
		[DataMember]
		public bool AllDayEvent { get; set; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x000722FF File Offset: 0x000704FF
		// (set) Token: 0x06001C9B RID: 7323 RVA: 0x00072307 File Offset: 0x00070507
		[DataMember]
		public string Date { get; set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x00072310 File Offset: 0x00070510
		// (set) Token: 0x06001C9D RID: 7325 RVA: 0x00072318 File Offset: 0x00070518
		[DataMember]
		public int Duration { get; set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x00072321 File Offset: 0x00070521
		// (set) Token: 0x06001C9F RID: 7327 RVA: 0x00072329 File Offset: 0x00070529
		[IgnoreDataMember]
		public CalendarSpeechRecoResultType ResultType { get; set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x00072332 File Offset: 0x00070532
		// (set) Token: 0x06001CA1 RID: 7329 RVA: 0x00072344 File Offset: 0x00070544
		[DataMember(Name = "ResultType")]
		public string ResultTypeString
		{
			get
			{
				return this.ResultType.ToString();
			}
			set
			{
				this.ResultType = (CalendarSpeechRecoResultType)Enum.Parse(typeof(CalendarSpeechRecoResultType), value);
			}
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00072361 File Offset: 0x00070561
		public static DayTimeDurationRecoResult JsonDeserialize(string result, Type targetType)
		{
			return (DayTimeDurationRecoResult)SpeechRecognitionResultHandler.JsonDeserialize(result, targetType);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0007236F File Offset: 0x0007056F
		public static string JsonSerialize(object obj)
		{
			return SpeechRecognitionResultHandler.JsonSerialize(obj);
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00072377 File Offset: 0x00070577
		public string ToJsonString()
		{
			return DayTimeDurationRecoResult.JsonSerialize(this);
		}
	}
}
