using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004B2 RID: 1202
	internal class ClientData
	{
		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x000DE7C1 File Offset: 0x000DC9C1
		// (set) Token: 0x0600364C RID: 13900 RVA: 0x000DE7C9 File Offset: 0x000DC9C9
		public int Count { get; internal set; }

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x0600364D RID: 13901 RVA: 0x000DE7D2 File Offset: 0x000DC9D2
		public bool Discredited
		{
			get
			{
				return this.discredited;
			}
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000DE7DA File Offset: 0x000DC9DA
		public void MarkBad()
		{
			this.discredited = true;
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000DE7E3 File Offset: 0x000DC9E3
		public void MarkGood()
		{
			this.discredited = false;
		}

		// Token: 0x04001BC7 RID: 7111
		private bool discredited;
	}
}
