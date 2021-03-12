using System;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003BF RID: 959
	[Serializable]
	public class MessageTraceDetail : MtrtObject
	{
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x0008CDEC File Offset: 0x0008AFEC
		// (set) Token: 0x06002271 RID: 8817 RVA: 0x0008CDF4 File Offset: 0x0008AFF4
		public string Organization { get; internal set; }

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x0008CDFD File Offset: 0x0008AFFD
		// (set) Token: 0x06002273 RID: 8819 RVA: 0x0008CE05 File Offset: 0x0008B005
		[ODataInput("MessageId")]
		public string MessageId { get; internal set; }

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x0008CE0E File Offset: 0x0008B00E
		// (set) Token: 0x06002275 RID: 8821 RVA: 0x0008CE16 File Offset: 0x0008B016
		[ODataInput("MessageTraceId")]
		public Guid MessageTraceId { get; internal set; }

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x0008CE1F File Offset: 0x0008B01F
		// (set) Token: 0x06002277 RID: 8823 RVA: 0x0008CE27 File Offset: 0x0008B027
		public DateTime Date { get; internal set; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x0008CE30 File Offset: 0x0008B030
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x0008CE38 File Offset: 0x0008B038
		public string Event { get; internal set; }

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x0008CE41 File Offset: 0x0008B041
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x0008CE49 File Offset: 0x0008B049
		[Redact]
		public string Action { get; internal set; }

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x0008CE52 File Offset: 0x0008B052
		// (set) Token: 0x0600227D RID: 8829 RVA: 0x0008CE5A File Offset: 0x0008B05A
		[Redact]
		public string Detail { get; internal set; }

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x0008CE63 File Offset: 0x0008B063
		// (set) Token: 0x0600227F RID: 8831 RVA: 0x0008CE6B File Offset: 0x0008B06B
		[Redact]
		public string Data { get; internal set; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x0008CE74 File Offset: 0x0008B074
		// (set) Token: 0x06002281 RID: 8833 RVA: 0x0008CE7C File Offset: 0x0008B07C
		[ODataInput("SenderAddress")]
		public string SenderAddress { get; internal set; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x0008CE85 File Offset: 0x0008B085
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x0008CE8D File Offset: 0x0008B08D
		[ODataInput("RecipientAddress")]
		public string RecipientAddress { get; internal set; }
	}
}
