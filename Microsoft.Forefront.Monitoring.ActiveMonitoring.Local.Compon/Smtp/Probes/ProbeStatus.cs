using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200023A RID: 570
	public class ProbeStatus
	{
		// Token: 0x06001307 RID: 4871 RVA: 0x000380AC File Offset: 0x000362AC
		public ProbeStatus(StatusEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentException("Collection cannot be null");
			}
			this.ExecutionContext = entry["executionContext"];
			this.ResultType = (ResultType)Enum.Parse(typeof(ResultType), entry["resultType"]);
			this.InternalProbeId = entry["internalProbeId"];
			this.ProbeMailInfo = entry["probeMailInfo"];
			this.SendMailExecutionId = entry["sendMailExecutionId"];
			this.DeliveryExpected = bool.Parse(entry["deliveryExpected"]);
			this.SentTime = DateTime.Parse(entry["sentTime"]);
			this.RecordType = (RecordType)Enum.Parse(typeof(RecordType), entry["recordType"]);
			this.ProbeErrorType = (MailErrorType)Enum.Parse(typeof(MailErrorType), entry["probeErrorType"]);
			this.EhloIssued = entry["ehloIssued"];
			this.ExchangeMessageId = entry["exchangeMessageId"];
			this.FDServerEncountered = entry["fdServerEncountered"];
			this.SmtpResponseReceived = entry["smtpResponseReceived"];
			this.TargetVIP = entry["targetVip"];
			this.HubServer = entry["hubServer"];
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00038214 File Offset: 0x00036414
		public ProbeStatus(ProbeResult result)
		{
			if (result == null)
			{
				throw new ArgumentException("Collection cannot be null");
			}
			this.ExecutionContext = result.ExecutionContext;
			this.ResultType = result.ResultType;
			this.InternalProbeId = SmtpProbe.GetInternalProbeId(result);
			this.ProbeMailInfo = SmtpProbe.GetProbeMailInfo(result);
			this.SendMailExecutionId = SmtpProbe.GetSendMailExecutionId(result);
			this.DeliveryExpected = SmtpProbe.GetDeliveryExpected(result);
			this.SentTime = new DateTime((long)SmtpProbe.GetSentTime(result));
			this.RecordType = (RecordType)Enum.Parse(typeof(RecordType), SmtpProbe.GetProbeRecordType(result));
			this.ProbeErrorType = (MailErrorType)Enum.Parse(typeof(MailErrorType), SmtpProbe.GetProbeErrorType(result));
			this.EhloIssued = BucketedSmtpProbe.GetEhloIssued(result);
			this.ExchangeMessageId = BucketedSmtpProbe.GetExchangeMessageID(result);
			this.FDServerEncountered = BucketedSmtpProbe.GetFDServerEncountered(result);
			this.SmtpResponseReceived = BucketedSmtpProbe.GetSmtpResponseReceived(result);
			this.TargetVIP = BucketedSmtpProbe.GetTargetVIP(result);
			this.HubServer = BucketedSmtpProbe.GetHubServer(result);
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00038317 File Offset: 0x00036517
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x0003831F File Offset: 0x0003651F
		public string InternalProbeId { get; private set; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x00038328 File Offset: 0x00036528
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x00038330 File Offset: 0x00036530
		public string ProbeMailInfo { get; private set; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x00038339 File Offset: 0x00036539
		// (set) Token: 0x0600130E RID: 4878 RVA: 0x00038341 File Offset: 0x00036541
		public string SendMailExecutionId { get; private set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x0003834A File Offset: 0x0003654A
		// (set) Token: 0x06001310 RID: 4880 RVA: 0x00038352 File Offset: 0x00036552
		public string EhloIssued { get; private set; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x0003835B File Offset: 0x0003655B
		// (set) Token: 0x06001312 RID: 4882 RVA: 0x00038363 File Offset: 0x00036563
		public string ExchangeMessageId { get; private set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x0003836C File Offset: 0x0003656C
		// (set) Token: 0x06001314 RID: 4884 RVA: 0x00038374 File Offset: 0x00036574
		public string FDServerEncountered { get; private set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x0003837D File Offset: 0x0003657D
		// (set) Token: 0x06001316 RID: 4886 RVA: 0x00038385 File Offset: 0x00036585
		public string SmtpResponseReceived { get; private set; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0003838E File Offset: 0x0003658E
		// (set) Token: 0x06001318 RID: 4888 RVA: 0x00038396 File Offset: 0x00036596
		public string TargetVIP { get; private set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0003839F File Offset: 0x0003659F
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x000383A7 File Offset: 0x000365A7
		public string HubServer { get; private set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x000383B0 File Offset: 0x000365B0
		// (set) Token: 0x0600131C RID: 4892 RVA: 0x000383B8 File Offset: 0x000365B8
		public string ExecutionContext { get; private set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x000383C1 File Offset: 0x000365C1
		// (set) Token: 0x0600131E RID: 4894 RVA: 0x000383C9 File Offset: 0x000365C9
		public bool DeliveryExpected { get; private set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x000383D2 File Offset: 0x000365D2
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x000383DA File Offset: 0x000365DA
		public DateTime SentTime { get; private set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x000383E3 File Offset: 0x000365E3
		// (set) Token: 0x06001322 RID: 4898 RVA: 0x000383EB File Offset: 0x000365EB
		public RecordType RecordType { get; private set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x000383F4 File Offset: 0x000365F4
		// (set) Token: 0x06001324 RID: 4900 RVA: 0x000383FC File Offset: 0x000365FC
		public MailErrorType ProbeErrorType { get; private set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x00038405 File Offset: 0x00036605
		// (set) Token: 0x06001326 RID: 4902 RVA: 0x0003840D File Offset: 0x0003660D
		public ResultType ResultType { get; private set; }

		// Token: 0x06001327 RID: 4903 RVA: 0x00038418 File Offset: 0x00036618
		public void CreateStatusEntry(StatusEntryCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentException("Collection cannot be null");
			}
			StatusEntry statusEntry = collection.CreateStatusEntry();
			statusEntry["executionContext"] = this.ExecutionContext;
			statusEntry["resultType"] = this.ResultType.ToString();
			statusEntry["internalProbeId"] = this.InternalProbeId;
			statusEntry["probeMailInfo"] = this.ProbeMailInfo;
			statusEntry["sendMailExecutionId"] = this.SendMailExecutionId;
			statusEntry["deliveryExpected"] = this.DeliveryExpected.ToString();
			statusEntry["sentTime"] = this.SentTime.ToString();
			statusEntry["recordType"] = this.RecordType.ToString();
			statusEntry["probeErrorType"] = this.ProbeErrorType.ToString();
			statusEntry["ehloIssued"] = this.EhloIssued;
			statusEntry["exchangeMessageId"] = this.ExchangeMessageId;
			statusEntry["fdServerEncountered"] = this.FDServerEncountered;
			statusEntry["smtpResponseReceived"] = this.SmtpResponseReceived;
			statusEntry["targetVip"] = this.TargetVIP;
			statusEntry["hubServer"] = this.HubServer;
		}

		// Token: 0x040008D7 RID: 2263
		private const string InternalProbeIdKey = "internalProbeId";

		// Token: 0x040008D8 RID: 2264
		private const string ProbeMailInfoKey = "probeMailInfo";

		// Token: 0x040008D9 RID: 2265
		private const string SendMailExecutionIdKey = "sendMailExecutionId";

		// Token: 0x040008DA RID: 2266
		private const string ExecutionContextKey = "executionContext";

		// Token: 0x040008DB RID: 2267
		private const string RecordTypeKey = "recordType";

		// Token: 0x040008DC RID: 2268
		private const string ProbeErrorTypeKey = "probeErrorType";

		// Token: 0x040008DD RID: 2269
		private const string EhloIssuedKey = "ehloIssued";

		// Token: 0x040008DE RID: 2270
		private const string ExchangeMessageIdKey = "exchangeMessageId";

		// Token: 0x040008DF RID: 2271
		private const string FdServerEncounteredKey = "fdServerEncountered";

		// Token: 0x040008E0 RID: 2272
		private const string SmtpResponseReceivedKey = "smtpResponseReceived";

		// Token: 0x040008E1 RID: 2273
		private const string TargetVipKey = "targetVip";

		// Token: 0x040008E2 RID: 2274
		private const string HubServerKey = "hubServer";

		// Token: 0x040008E3 RID: 2275
		private const string DeliveryExpectedKey = "deliveryExpected";

		// Token: 0x040008E4 RID: 2276
		private const string SentTimeKey = "sentTime";

		// Token: 0x040008E5 RID: 2277
		private const string ResultTypeKey = "resultType";
	}
}
