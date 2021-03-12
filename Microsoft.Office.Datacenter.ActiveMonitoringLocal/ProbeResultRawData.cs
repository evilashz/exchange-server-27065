using System;
using System.Runtime.Serialization;
using Microsoft.Office365.DataInsights.Uploader;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000020 RID: 32
	[DataContract(Name = "ProbeResult", Namespace = "http://microsoft.com/exoanalytics")]
	public sealed class ProbeResultRawData : InsightRawData
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000B1C7 File Offset: 0x000093C7
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000B1CF File Offset: 0x000093CF
		[DataMember(Name = "AMInstanceName")]
		public string AMInstanceName { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000B1D8 File Offset: 0x000093D8
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000B1E0 File Offset: 0x000093E0
		[DataMember(Name = "Latency")]
		public double Latency { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000B1E9 File Offset: 0x000093E9
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000B1F1 File Offset: 0x000093F1
		[DataMember(Name = "Error")]
		public string Error { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000B1FA File Offset: 0x000093FA
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000B202 File Offset: 0x00009402
		[DataMember(Name = "Exception")]
		public string Exception { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000B20B File Offset: 0x0000940B
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000B213 File Offset: 0x00009413
		[DataMember(Name = "FailureCategory")]
		public int FailureCategory { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000B21C File Offset: 0x0000941C
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000B224 File Offset: 0x00009424
		[DataMember(Name = "StateAttribute1")]
		public string StateAttribute1 { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000B22D File Offset: 0x0000942D
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000B235 File Offset: 0x00009435
		[DataMember(Name = "StateAttribute2")]
		public string StateAttribute2 { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000B23E File Offset: 0x0000943E
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000B246 File Offset: 0x00009446
		[DataMember(Name = "StateAttribute3")]
		public string StateAttribute3 { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000B24F File Offset: 0x0000944F
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000B257 File Offset: 0x00009457
		[DataMember(Name = "StateAttribute4")]
		public string StateAttribute4 { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000B260 File Offset: 0x00009460
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000B268 File Offset: 0x00009468
		[DataMember(Name = "StateAttribute5")]
		public string StateAttribute5 { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000B271 File Offset: 0x00009471
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000B279 File Offset: 0x00009479
		[DataMember(Name = "StateAttribute6")]
		public double StateAttribute6 { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000B282 File Offset: 0x00009482
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000B28A File Offset: 0x0000948A
		[DataMember(Name = "StateAttribute7")]
		public double StateAttribute7 { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000B293 File Offset: 0x00009493
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000B29B File Offset: 0x0000949B
		[DataMember(Name = "StateAttribute8")]
		public double StateAttribute8 { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000B2A4 File Offset: 0x000094A4
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000B2AC File Offset: 0x000094AC
		[DataMember(Name = "StateAttribute9")]
		public double StateAttribute9 { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000B2B5 File Offset: 0x000094B5
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000B2BD File Offset: 0x000094BD
		[DataMember(Name = "StateAttribute10")]
		public double StateAttribute10 { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000B2C6 File Offset: 0x000094C6
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000B2CE File Offset: 0x000094CE
		[DataMember(Name = "StateAttribute11")]
		public string StateAttribute11 { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000B2D7 File Offset: 0x000094D7
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000B2DF File Offset: 0x000094DF
		[DataMember(Name = "StateAttribute12")]
		public string StateAttribute12 { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000B2E8 File Offset: 0x000094E8
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000B2F0 File Offset: 0x000094F0
		[DataMember(Name = "StateAttribute13")]
		public string StateAttribute13 { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000B2F9 File Offset: 0x000094F9
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000B301 File Offset: 0x00009501
		[DataMember(Name = "StateAttribute14")]
		public string StateAttribute14 { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000B30A File Offset: 0x0000950A
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000B312 File Offset: 0x00009512
		[DataMember(Name = "StateAttribute15")]
		public string StateAttribute15 { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000B31B File Offset: 0x0000951B
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000B323 File Offset: 0x00009523
		[DataMember(Name = "StateAttribute18")]
		public double StateAttribute18 { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000B32C File Offset: 0x0000952C
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000B334 File Offset: 0x00009534
		[DataMember(Name = "StateAttribute21")]
		public string StateAttribute21 { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000B33D File Offset: 0x0000953D
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000B345 File Offset: 0x00009545
		[DataMember(Name = "StateAttribute22")]
		public string StateAttribute22 { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000B34E File Offset: 0x0000954E
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000B356 File Offset: 0x00009556
		[DataMember(Name = "StateAttribute23")]
		public string StateAttribute23 { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000B35F File Offset: 0x0000955F
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000B367 File Offset: 0x00009567
		[DataMember(Name = "ExecutionContext")]
		public string ExecutionContext { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000B370 File Offset: 0x00009570
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000B378 File Offset: 0x00009578
		[DataMember(Name = "FailureContext")]
		public string FailureContext { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000B381 File Offset: 0x00009581
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000B389 File Offset: 0x00009589
		[DataMember(Name = "ResultId")]
		public int ResultId { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000B392 File Offset: 0x00009592
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000B39A File Offset: 0x0000959A
		[DataMember(Name = "IsCortex")]
		public bool IsCortex { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000B3A3 File Offset: 0x000095A3
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000B3AB File Offset: 0x000095AB
		[DataMember(Name = "DataPartition")]
		public string DataPartition { get; set; }
	}
}
