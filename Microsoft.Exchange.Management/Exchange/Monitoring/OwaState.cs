using System;
using System.Net;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005AD RID: 1453
	internal class OwaState
	{
		// Token: 0x0600330B RID: 13067 RVA: 0x000D0474 File Offset: 0x000CE674
		internal OwaState(HttpWebRequest request, TestOwaConnectivityOutcome outcome)
		{
			this.request = request;
			this.outcome = outcome;
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x000D048A File Offset: 0x000CE68A
		internal HttpWebRequest Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x000D0492 File Offset: 0x000CE692
		// (set) Token: 0x0600330E RID: 13070 RVA: 0x000D049A File Offset: 0x000CE69A
		internal HttpWebResponse Response
		{
			get
			{
				return this.response;
			}
			set
			{
				this.response = value;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000D04A3 File Offset: 0x000CE6A3
		internal TestOwaConnectivityOutcome Outcome
		{
			get
			{
				return this.outcome;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000D04AB File Offset: 0x000CE6AB
		// (set) Token: 0x06003311 RID: 13073 RVA: 0x000D04B3 File Offset: 0x000CE6B3
		internal int RedirectCount { get; set; }

		// Token: 0x040023A5 RID: 9125
		private HttpWebRequest request;

		// Token: 0x040023A6 RID: 9126
		private HttpWebResponse response;

		// Token: 0x040023A7 RID: 9127
		private TestOwaConnectivityOutcome outcome;
	}
}
