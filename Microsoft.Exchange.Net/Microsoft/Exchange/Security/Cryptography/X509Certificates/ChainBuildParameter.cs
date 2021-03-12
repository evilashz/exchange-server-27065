using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A8B RID: 2699
	internal class ChainBuildParameter
	{
		// Token: 0x06003A44 RID: 14916 RVA: 0x00094DE7 File Offset: 0x00092FE7
		public ChainBuildParameter(ChainMatchIssuer match, TimeSpan timeout, bool overrideCRLTime, TimeSpan freshnessTime)
		{
			this.match = match;
			this.urlRetrievalTimeout = timeout;
			this.overrideRevocationTime = overrideCRLTime;
			this.freshnessDelta = freshnessTime;
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003A45 RID: 14917 RVA: 0x00094E0C File Offset: 0x0009300C
		// (set) Token: 0x06003A46 RID: 14918 RVA: 0x00094E14 File Offset: 0x00093014
		internal ChainMatchIssuer Match
		{
			get
			{
				return this.match;
			}
			set
			{
				this.match = value;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x00094E1D File Offset: 0x0009301D
		// (set) Token: 0x06003A48 RID: 14920 RVA: 0x00094E25 File Offset: 0x00093025
		public TimeSpan UrlRetrievalTimeout
		{
			get
			{
				return this.urlRetrievalTimeout;
			}
			set
			{
				this.urlRetrievalTimeout = value;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003A49 RID: 14921 RVA: 0x00094E2E File Offset: 0x0009302E
		// (set) Token: 0x06003A4A RID: 14922 RVA: 0x00094E36 File Offset: 0x00093036
		public bool OverrideRevocationTime
		{
			get
			{
				return this.overrideRevocationTime;
			}
			set
			{
				this.overrideRevocationTime = value;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003A4B RID: 14923 RVA: 0x00094E3F File Offset: 0x0009303F
		// (set) Token: 0x06003A4C RID: 14924 RVA: 0x00094E47 File Offset: 0x00093047
		public TimeSpan RevocationFreshnessDelta
		{
			get
			{
				return this.freshnessDelta;
			}
			set
			{
				this.freshnessDelta = value;
			}
		}

		// Token: 0x04003278 RID: 12920
		private ChainMatchIssuer match;

		// Token: 0x04003279 RID: 12921
		private TimeSpan urlRetrievalTimeout;

		// Token: 0x0400327A RID: 12922
		private bool overrideRevocationTime;

		// Token: 0x0400327B RID: 12923
		private TimeSpan freshnessDelta;
	}
}
