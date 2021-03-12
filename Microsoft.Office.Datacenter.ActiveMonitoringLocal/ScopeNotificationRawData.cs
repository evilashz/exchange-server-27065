using System;
using System.Runtime.Serialization;
using Microsoft.Office365.DataInsights.Uploader;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000045 RID: 69
	[DataContract(Name = "ScopeNotification", Namespace = "http://microsoft.com/exoanalytics")]
	public sealed class ScopeNotificationRawData : InsightRawData
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x000120A9 File Offset: 0x000102A9
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x000120B1 File Offset: 0x000102B1
		[DataMember(Name = "NotificationName")]
		public string NotificationName { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x000120BA File Offset: 0x000102BA
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x000120C2 File Offset: 0x000102C2
		[DataMember(Name = "ScopeName")]
		public string ScopeName { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000120CB File Offset: 0x000102CB
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x000120D3 File Offset: 0x000102D3
		[DataMember(Name = "ScopeType")]
		public string ScopeType { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x000120DC File Offset: 0x000102DC
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x000120E4 File Offset: 0x000102E4
		[DataMember(Name = "HealthSetName")]
		public string HealthSetName { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000120ED File Offset: 0x000102ED
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x000120F5 File Offset: 0x000102F5
		[DataMember(Name = "HealthState")]
		public int HealthState { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x000120FE File Offset: 0x000102FE
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00012106 File Offset: 0x00010306
		[DataMember(Name = "MachineName")]
		public string MachineName { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001210F File Offset: 0x0001030F
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x00012117 File Offset: 0x00010317
		[DataMember(Name = "SourceInstanceName")]
		public string SourceInstanceName { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00012120 File Offset: 0x00010320
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00012128 File Offset: 0x00010328
		[DataMember(Name = "SourceInstanceType")]
		public string SourceInstanceType { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00012131 File Offset: 0x00010331
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00012139 File Offset: 0x00010339
		[DataMember(Name = "IsMultiSourceInstance")]
		public bool IsMultiSourceInstance { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00012142 File Offset: 0x00010342
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0001214A File Offset: 0x0001034A
		[DataMember(Name = "ExecutionStartTime")]
		public DateTime ExecutionStartTime { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00012153 File Offset: 0x00010353
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0001215B File Offset: 0x0001035B
		[DataMember(Name = "ExecutionEndTime")]
		public DateTime ExecutionEndTime { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00012164 File Offset: 0x00010364
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0001216C File Offset: 0x0001036C
		[DataMember(Name = "Error")]
		public string Error { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00012175 File Offset: 0x00010375
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0001217D File Offset: 0x0001037D
		[DataMember(Name = "Exception")]
		public string Exception { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00012186 File Offset: 0x00010386
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0001218E File Offset: 0x0001038E
		[DataMember(Name = "ExecutionContext")]
		public string ExecutionContext { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00012197 File Offset: 0x00010397
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0001219F File Offset: 0x0001039F
		[DataMember(Name = "FailureContext")]
		public string FailureContext { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x000121A8 File Offset: 0x000103A8
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x000121B0 File Offset: 0x000103B0
		[DataMember(Name = "Data")]
		public string Data { get; set; }
	}
}
