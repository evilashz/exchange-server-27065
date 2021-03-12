using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200001F RID: 31
	internal class OnlineMeetingSettings
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x000032CA File Offset: 0x000014CA
		public OnlineMeetingSettings()
		{
			this.leaders = new Collection<string>();
			this.attendees = new Collection<string>();
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000032E8 File Offset: 0x000014E8
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000032F0 File Offset: 0x000014F0
		public string Subject { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000032F9 File Offset: 0x000014F9
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003301 File Offset: 0x00001501
		public string Description { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000330A File Offset: 0x0000150A
		public Collection<string> Leaders
		{
			get
			{
				return this.leaders;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003312 File Offset: 0x00001512
		public Collection<string> Attendees
		{
			get
			{
				return this.attendees;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000331A File Offset: 0x0000151A
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003322 File Offset: 0x00001522
		public DateTime? ExpiryTime { get; set; }

		// Token: 0x040000E8 RID: 232
		private readonly Collection<string> leaders;

		// Token: 0x040000E9 RID: 233
		private readonly Collection<string> attendees;
	}
}
