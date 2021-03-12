using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x0200001A RID: 26
	internal class RequestData
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00006158 File Offset: 0x00004358
		public RequestData(IPrincipal user, bool useClientCertificateAuthentication, CallerRequestedCapabilities optinCapabilities)
		{
			this.User = user;
			this.UseClientCertificateAuthentication = useClientCertificateAuthentication;
			this.Timestamp = ExDateTime.Now.TimeOfDay.ToString();
			this.RequestSchemas = new List<string>();
			this.ResponseSchemas = new List<string>();
			this.CallerCapabilities = optinCapabilities;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000061B7 File Offset: 0x000043B7
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000061BF File Offset: 0x000043BF
		public IPrincipal User { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000061C8 File Offset: 0x000043C8
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000061D0 File Offset: 0x000043D0
		public string EMailAddress { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000061D9 File Offset: 0x000043D9
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000061E1 File Offset: 0x000043E1
		public string RedirectPod { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000061EA File Offset: 0x000043EA
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000061F2 File Offset: 0x000043F2
		public string LegacyDN { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000061FB File Offset: 0x000043FB
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00006203 File Offset: 0x00004403
		public List<string> RequestSchemas { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000620C File Offset: 0x0000440C
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00006214 File Offset: 0x00004414
		public List<string> ResponseSchemas { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000621D File Offset: 0x0000441D
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00006225 File Offset: 0x00004425
		public string Timestamp { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000622E File Offset: 0x0000442E
		public string ComputerNameHash
		{
			get
			{
				return RequestData.computerNameHash;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00006235 File Offset: 0x00004435
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000623D File Offset: 0x0000443D
		public CallerRequestedCapabilities CallerCapabilities { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006246 File Offset: 0x00004446
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000624E File Offset: 0x0000444E
		public string UserAuthType { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006257 File Offset: 0x00004457
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000625F File Offset: 0x0000445F
		public IBudget Budget { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00006268 File Offset: 0x00004468
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00006270 File Offset: 0x00004470
		public ProxyRequestData ProxyRequestData { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00006279 File Offset: 0x00004479
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00006281 File Offset: 0x00004481
		public bool UseClientCertificateAuthentication { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000628A File Offset: 0x0000448A
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00006292 File Offset: 0x00004492
		public int MapiHttpVersion { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000629B File Offset: 0x0000449B
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x000062A3 File Offset: 0x000044A3
		public string UserAgent { get; set; }

		// Token: 0x060000F3 RID: 243 RVA: 0x000062AC File Offset: 0x000044AC
		public void Clear()
		{
			this.User = null;
			this.RequestSchemas.Clear();
			this.ResponseSchemas.Clear();
			this.EMailAddress = string.Empty;
			this.RedirectPod = string.Empty;
			this.LegacyDN = string.Empty;
			this.Timestamp = string.Empty;
			this.Budget = null;
			this.UserAuthType = string.Empty;
			this.ProxyRequestData = null;
			this.MapiHttpVersion = 0;
			this.CallerCapabilities = null;
			this.UseClientCertificateAuthentication = false;
			this.UserAgent = string.Empty;
		}

		// Token: 0x04000113 RID: 275
		private static readonly string computerNameHash = ((uint)Environment.MachineName.GetHashCode()).ToString();
	}
}
