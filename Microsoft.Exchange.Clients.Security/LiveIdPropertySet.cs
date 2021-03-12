using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200002A RID: 42
	internal class LiveIdPropertySet
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00009DA8 File Offset: 0x00007FA8
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00009DB0 File Offset: 0x00007FB0
		private string RpsToken { get; set; }

		// Token: 0x0600012E RID: 302 RVA: 0x00009DBC File Offset: 0x00007FBC
		private LiveIdPropertySet(HttpContext httpContext)
		{
			string text = httpContext.Request.Headers["msExchOrganizationContext"];
			if (!string.IsNullOrEmpty(text))
			{
				this.RequestType = RequestType.EcpByoidAdmin;
				this.TargetTenant = text;
				return;
			}
			string text2 = httpContext.Request.Headers["msExchTargetTenant"];
			if (string.IsNullOrEmpty(text2))
			{
				this.RequestType = RequestType.Regular;
				return;
			}
			this.TargetTenant = text2;
			if (httpContext.Items["msExchNoResolveId"] != null)
			{
				this.RequestType = RequestType.EcpDelegatedAdminTargetForest;
				return;
			}
			this.RequestType = RequestType.EcpDelegatedAdmin;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00009E4C File Offset: 0x0000804C
		internal static LiveIdPropertySet Current
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				LiveIdPropertySet liveIdPropertySet = (LiveIdPropertySet)httpContext.Items["LiveIdPropertySet"];
				if (liveIdPropertySet == null)
				{
					liveIdPropertySet = new LiveIdPropertySet(httpContext);
					httpContext.Items["LiveIdPropertySet"] = liveIdPropertySet;
				}
				return liveIdPropertySet;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009E94 File Offset: 0x00008094
		internal static LiveIdPropertySet GetLiveIdPropertySet(HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			LiveIdPropertySet liveIdPropertySet = (LiveIdPropertySet)httpContext.Items["LiveIdPropertySet"];
			if (liveIdPropertySet == null)
			{
				liveIdPropertySet = new LiveIdPropertySet(httpContext);
				httpContext.Items["LiveIdPropertySet"] = liveIdPropertySet;
			}
			return liveIdPropertySet;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00009EE1 File Offset: 0x000080E1
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00009EE9 File Offset: 0x000080E9
		internal uint RpsTicketType { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00009EF2 File Offset: 0x000080F2
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00009EFA File Offset: 0x000080FA
		internal RequestType RequestType { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00009F03 File Offset: 0x00008103
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00009F0B File Offset: 0x0000810B
		internal string TargetTenant { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00009F14 File Offset: 0x00008114
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00009F1C File Offset: 0x0000811C
		internal string PUID { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00009F25 File Offset: 0x00008125
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00009F2D File Offset: 0x0000812D
		internal string OrgIdPUID { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00009F36 File Offset: 0x00008136
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00009F3E File Offset: 0x0000813E
		internal string CID { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00009F47 File Offset: 0x00008147
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00009F4F File Offset: 0x0000814F
		internal string MemberName { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00009F58 File Offset: 0x00008158
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00009F60 File Offset: 0x00008160
		internal bool HasAcceptedAccruals { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00009F69 File Offset: 0x00008169
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00009F71 File Offset: 0x00008171
		internal string RpsRespHeaders { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00009F7A File Offset: 0x0000817A
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00009F82 File Offset: 0x00008182
		internal string SiteName { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00009F8B File Offset: 0x0000818B
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00009F93 File Offset: 0x00008193
		internal uint LoginAttributes { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00009F9C File Offset: 0x0000819C
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00009FA4 File Offset: 0x000081A4
		internal uint IssueInstant { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00009FAD File Offset: 0x000081AD
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00009FB5 File Offset: 0x000081B5
		internal IIdentity Identity { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00009FBE File Offset: 0x000081BE
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00009FC6 File Offset: 0x000081C6
		internal OrganizationProperties OrganizationProperties { get; set; }

		// Token: 0x0600014D RID: 333 RVA: 0x00009FD0 File Offset: 0x000081D0
		internal void SetLiveIdProperties(string puid, string orgIdPuid, string cid, string memberName, bool hasAcceptedAccruals, uint loginAttributes, uint rpsTicketType, string respHeaders, string siteName, uint issueInstant)
		{
			this.PUID = puid;
			this.OrgIdPUID = orgIdPuid;
			this.CID = cid;
			this.MemberName = memberName;
			this.HasAcceptedAccruals = hasAcceptedAccruals;
			this.LoginAttributes = loginAttributes;
			this.RpsTicketType = rpsTicketType;
			this.RpsRespHeaders = respHeaders;
			this.SiteName = siteName;
			this.IssueInstant = issueInstant;
		}

		// Token: 0x04000147 RID: 327
		private const string Key = "LiveIdPropertySet";
	}
}
