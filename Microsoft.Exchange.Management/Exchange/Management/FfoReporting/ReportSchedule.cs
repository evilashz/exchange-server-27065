using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003FC RID: 1020
	[Serializable]
	public class ReportSchedule
	{
		// Token: 0x060023E2 RID: 9186 RVA: 0x0008FF1C File Offset: 0x0008E11C
		internal ReportSchedule()
		{
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x0008FF24 File Offset: 0x0008E124
		// (set) Token: 0x060023E4 RID: 9188 RVA: 0x0008FF2C File Offset: 0x0008E12C
		public string Action { get; internal set; }

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x0008FF35 File Offset: 0x0008E135
		// (set) Token: 0x060023E6 RID: 9190 RVA: 0x0008FF3D File Offset: 0x0008E13D
		public ReportDeliveryStatus DeliveryStatus { get; internal set; }

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x0008FF46 File Offset: 0x0008E146
		// (set) Token: 0x060023E8 RID: 9192 RVA: 0x0008FF4E File Offset: 0x0008E14E
		public ReportDirection Direction { get; internal set; }

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x0008FF57 File Offset: 0x0008E157
		// (set) Token: 0x060023EA RID: 9194 RVA: 0x0008FF5F File Offset: 0x0008E15F
		public MultiValuedProperty<Guid> DLPPolicy { get; internal set; }

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x0008FF68 File Offset: 0x0008E168
		// (set) Token: 0x060023EC RID: 9196 RVA: 0x0008FF70 File Offset: 0x0008E170
		public string Domain { get; internal set; }

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x0008FF79 File Offset: 0x0008E179
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x0008FF81 File Offset: 0x0008E181
		public DateTime EndDate { get; internal set; }

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x0008FF8A File Offset: 0x0008E18A
		// (set) Token: 0x060023F0 RID: 9200 RVA: 0x0008FF92 File Offset: 0x0008E192
		public MultiValuedProperty<string> EventType { get; set; }

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x0008FF9B File Offset: 0x0008E19B
		// (set) Token: 0x060023F2 RID: 9202 RVA: 0x0008FFA3 File Offset: 0x0008E1A3
		public CultureInfo Locale { get; internal set; }

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x0008FFAC File Offset: 0x0008E1AC
		// (set) Token: 0x060023F4 RID: 9204 RVA: 0x0008FFB4 File Offset: 0x0008E1B4
		public MultiValuedProperty<Guid> MalwareName { get; internal set; }

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x0008FFBD File Offset: 0x0008E1BD
		// (set) Token: 0x060023F6 RID: 9206 RVA: 0x0008FFC5 File Offset: 0x0008E1C5
		public MultiValuedProperty<string> MessageID { get; internal set; }

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x0008FFCE File Offset: 0x0008E1CE
		// (set) Token: 0x060023F8 RID: 9208 RVA: 0x0008FFD6 File Offset: 0x0008E1D6
		public MultiValuedProperty<string> NotifyAddress { get; internal set; }

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x0008FFDF File Offset: 0x0008E1DF
		// (set) Token: 0x060023FA RID: 9210 RVA: 0x0008FFE7 File Offset: 0x0008E1E7
		public string OriginalClientIP { get; internal set; }

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x0008FFF0 File Offset: 0x0008E1F0
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x0008FFF8 File Offset: 0x0008E1F8
		public MultiValuedProperty<string> RecipientAddress { get; internal set; }

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x00090001 File Offset: 0x0008E201
		// (set) Token: 0x060023FE RID: 9214 RVA: 0x00090009 File Offset: 0x0008E209
		public ReportRecurrence Recurrence { get; set; }

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x00090012 File Offset: 0x0008E212
		// (set) Token: 0x06002400 RID: 9216 RVA: 0x0009001A File Offset: 0x0008E21A
		public string ReportTitle { get; set; }

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x00090023 File Offset: 0x0008E223
		// (set) Token: 0x06002402 RID: 9218 RVA: 0x0009002B File Offset: 0x0008E22B
		public ScheduleReportType ReportType { get; set; }

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x00090034 File Offset: 0x0008E234
		// (set) Token: 0x06002404 RID: 9220 RVA: 0x0009003C File Offset: 0x0008E23C
		public MultiValuedProperty<string> SenderAddress { get; internal set; }

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x00090045 File Offset: 0x0008E245
		// (set) Token: 0x06002406 RID: 9222 RVA: 0x0009004D File Offset: 0x0008E24D
		public ReportSeverity Severity { get; internal set; }

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x00090056 File Offset: 0x0008E256
		// (set) Token: 0x06002408 RID: 9224 RVA: 0x0009005E File Offset: 0x0008E25E
		public DateTime StartDate { get; internal set; }

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x00090067 File Offset: 0x0008E267
		// (set) Token: 0x0600240A RID: 9226 RVA: 0x0009006F File Offset: 0x0008E26F
		public MultiValuedProperty<Guid> TransportRule { get; internal set; }
	}
}
