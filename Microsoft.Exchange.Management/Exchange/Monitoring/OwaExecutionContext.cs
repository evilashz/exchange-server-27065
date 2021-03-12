using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005AE RID: 1454
	internal class OwaExecutionContext
	{
		// Token: 0x06003312 RID: 13074 RVA: 0x000D04BC File Offset: 0x000CE6BC
		public OwaExecutionContext(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			this.instance = instance;
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x000D04CB File Offset: 0x000CE6CB
		// (set) Token: 0x06003314 RID: 13076 RVA: 0x000D04D3 File Offset: 0x000CE6D3
		public AuthenticationMethod AuthMethod
		{
			get
			{
				return this.authMethod;
			}
			set
			{
				this.authMethod = value;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000D04DC File Offset: 0x000CE6DC
		// (set) Token: 0x06003316 RID: 13078 RVA: 0x000D04E4 File Offset: 0x000CE6E4
		public HttpWebResponse FbaResponse
		{
			get
			{
				return this.fbaResponse;
			}
			set
			{
				this.fbaResponse = value;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000D04ED File Offset: 0x000CE6ED
		// (set) Token: 0x06003318 RID: 13080 RVA: 0x000D04F5 File Offset: 0x000CE6F5
		public bool IsIsaFbaLogon
		{
			get
			{
				return this.isIsaFbaLogon;
			}
			set
			{
				this.isIsaFbaLogon = value;
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x000D04FE File Offset: 0x000CE6FE
		// (set) Token: 0x0600331A RID: 13082 RVA: 0x000D0506 File Offset: 0x000CE706
		public int IsaFbaFormdir
		{
			get
			{
				return this.isaFbaFormdir;
			}
			set
			{
				this.isaFbaFormdir = value;
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000D050F File Offset: 0x000CE70F
		public TestCasConnectivity.TestCasConnectivityRunInstance Instance
		{
			get
			{
				return this.instance;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000D0517 File Offset: 0x000CE717
		// (set) Token: 0x0600331D RID: 13085 RVA: 0x000D051F File Offset: 0x000CE71F
		public OwaState CurrentState
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x000D0528 File Offset: 0x000CE728
		// (set) Token: 0x0600331F RID: 13087 RVA: 0x000D0530 File Offset: 0x000CE730
		public bool GotAuthChallenge
		{
			get
			{
				return this.gotAuthChallenge;
			}
			set
			{
				this.gotAuthChallenge = value;
			}
		}

		// Token: 0x040023A9 RID: 9129
		private TestCasConnectivity.TestCasConnectivityRunInstance instance;

		// Token: 0x040023AA RID: 9130
		private OwaState state;

		// Token: 0x040023AB RID: 9131
		private bool gotAuthChallenge;

		// Token: 0x040023AC RID: 9132
		private AuthenticationMethod authMethod;

		// Token: 0x040023AD RID: 9133
		private bool isIsaFbaLogon;

		// Token: 0x040023AE RID: 9134
		private int isaFbaFormdir;

		// Token: 0x040023AF RID: 9135
		private HttpWebResponse fbaResponse;
	}
}
