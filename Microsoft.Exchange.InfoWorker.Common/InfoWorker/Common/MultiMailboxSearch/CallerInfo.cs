using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001BD RID: 445
	internal class CallerInfo
	{
		// Token: 0x06000C0C RID: 3084 RVA: 0x00034AB0 File Offset: 0x00032CB0
		public CallerInfo(bool isOpenAsAdmin, CommonAccessToken commonAccessToken, ClientSecurityContext securityContext, string primarySmtpAddress, OrganizationId orgId, string[] userRoles, string[] applicationRoles) : this(isOpenAsAdmin, commonAccessToken, securityContext, primarySmtpAddress, orgId, string.Empty, Guid.NewGuid(), userRoles, applicationRoles)
		{
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00034AD8 File Offset: 0x00032CD8
		public CallerInfo(bool isOpenAsAdmin, CommonAccessToken commonAccessToken, ClientSecurityContext securityContext, string primarySmtpAddress, OrganizationId orgId, string userAgent, Guid queryCorrelationId, string[] userRoles, string[] applicationRoles)
		{
			this.isOpenAsAdmin = isOpenAsAdmin;
			this.commonAccessToken = commonAccessToken;
			this.clientSecurityContext = securityContext;
			this.orgId = orgId;
			this.primarySmtpAddress = primarySmtpAddress;
			this.userAgent = userAgent;
			this.queryCorrelationId = queryCorrelationId;
			this.userRoles = userRoles;
			this.applicationRoles = applicationRoles;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00034B30 File Offset: 0x00032D30
		public ClientSecurityContext ClientSecurityContext
		{
			get
			{
				return this.clientSecurityContext;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00034B38 File Offset: 0x00032D38
		public OrganizationId OrganizationId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00034B40 File Offset: 0x00032D40
		public string PrimarySmtpAddress
		{
			get
			{
				return this.primarySmtpAddress;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00034B48 File Offset: 0x00032D48
		public string UserAgent
		{
			get
			{
				return this.userAgent;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00034B50 File Offset: 0x00032D50
		public CommonAccessToken CommonAccessToken
		{
			get
			{
				return this.commonAccessToken;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00034B58 File Offset: 0x00032D58
		public bool IsOpenAsAdmin
		{
			get
			{
				return this.isOpenAsAdmin;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00034B60 File Offset: 0x00032D60
		public Guid QueryCorrelationId
		{
			get
			{
				return this.queryCorrelationId;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00034B68 File Offset: 0x00032D68
		public string[] UserRoles
		{
			get
			{
				return this.userRoles;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00034B70 File Offset: 0x00032D70
		public string[] ApplicationRoles
		{
			get
			{
				return this.applicationRoles;
			}
		}

		// Token: 0x040008EA RID: 2282
		private readonly ClientSecurityContext clientSecurityContext;

		// Token: 0x040008EB RID: 2283
		private readonly OrganizationId orgId;

		// Token: 0x040008EC RID: 2284
		private readonly string primarySmtpAddress;

		// Token: 0x040008ED RID: 2285
		private readonly string userAgent;

		// Token: 0x040008EE RID: 2286
		private readonly CommonAccessToken commonAccessToken;

		// Token: 0x040008EF RID: 2287
		private readonly bool isOpenAsAdmin;

		// Token: 0x040008F0 RID: 2288
		private readonly Guid queryCorrelationId;

		// Token: 0x040008F1 RID: 2289
		private readonly string[] userRoles;

		// Token: 0x040008F2 RID: 2290
		private readonly string[] applicationRoles;
	}
}
