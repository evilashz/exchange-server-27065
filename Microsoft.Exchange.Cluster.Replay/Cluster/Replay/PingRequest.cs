using System;
using System.Net;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000266 RID: 614
	internal class PingRequest
	{
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x0006334F File Offset: 0x0006154F
		// (set) Token: 0x06001801 RID: 6145 RVA: 0x00063357 File Offset: 0x00061557
		public IPAddress IPAddress { get; set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00063360 File Offset: 0x00061560
		// (set) Token: 0x06001803 RID: 6147 RVA: 0x00063368 File Offset: 0x00061568
		public long StartTimeStamp { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00063371 File Offset: 0x00061571
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x00063379 File Offset: 0x00061579
		public long StopTimeStamp { get; set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00063382 File Offset: 0x00061582
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x0006338A File Offset: 0x0006158A
		public long LatencyInUSec { get; set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00063393 File Offset: 0x00061593
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x0006339B File Offset: 0x0006159B
		public bool TimedOut { get; set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x000633A4 File Offset: 0x000615A4
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x000633AC File Offset: 0x000615AC
		public bool Success { get; set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000633B5 File Offset: 0x000615B5
		// (set) Token: 0x0600180D RID: 6157 RVA: 0x000633BD File Offset: 0x000615BD
		public object UserContext { get; set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x000633C6 File Offset: 0x000615C6
		public byte[] ReplyBuffer
		{
			get
			{
				return this.m_receiveBuf;
			}
		}

		// Token: 0x04000986 RID: 2438
		private const int MaxResponseSize = 256;

		// Token: 0x04000987 RID: 2439
		private byte[] m_receiveBuf = new byte[256];
	}
}
