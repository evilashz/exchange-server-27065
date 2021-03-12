using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200011E RID: 286
	internal class GlsRawResponse
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x000366F7 File Offset: 0x000348F7
		internal string ResourceForest
		{
			get
			{
				return this.resourceForest ?? string.Empty;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00036708 File Offset: 0x00034908
		internal string AccountForest
		{
			get
			{
				return this.accountForest ?? string.Empty;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00036719 File Offset: 0x00034919
		internal string TenantId
		{
			get
			{
				return this.tenantId ?? string.Empty;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0003672A File Offset: 0x0003492A
		internal string SmtpNextHopDomain
		{
			get
			{
				return this.smtpNextHopDomain ?? string.Empty;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0003673B File Offset: 0x0003493B
		internal string TenantFlags
		{
			get
			{
				return this.tenantFlags ?? string.Empty;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0003674C File Offset: 0x0003494C
		internal string TenantContainerCN
		{
			get
			{
				return this.tenantContainerCN ?? string.Empty;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0003675D File Offset: 0x0003495D
		internal string DomainName
		{
			get
			{
				return this.domainName ?? string.Empty;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0003676E File Offset: 0x0003496E
		internal string DomainInUse
		{
			get
			{
				return this.domainInUse ?? string.Empty;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0003677F File Offset: 0x0003497F
		internal string DomainFlags
		{
			get
			{
				return this.domainFlags ?? string.Empty;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00036790 File Offset: 0x00034990
		internal string MSAUserNetID
		{
			get
			{
				return this.msaUserNetID ?? string.Empty;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x000367A1 File Offset: 0x000349A1
		internal string MSAUserMemberName
		{
			get
			{
				return this.msaUserMemberName ?? string.Empty;
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000367B4 File Offset: 0x000349B4
		internal void Populate(DomainInfo domainInfo)
		{
			if (domainInfo != null && domainInfo.Properties != null)
			{
				this.domainName = domainInfo.DomainName;
				foreach (KeyValuePair<string, string> keyValuePair in domainInfo.Properties)
				{
					if (keyValuePair.Key.Equals(DomainProperty.ExoDomainInUse.Name))
					{
						this.domainInUse = keyValuePair.Value;
					}
					else if (keyValuePair.Key.Equals(DomainProperty.ExoFlags.Name))
					{
						this.domainFlags = keyValuePair.Value;
					}
				}
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0003684C File Offset: 0x00034A4C
		internal void Populate(TenantInfo tenantInfo)
		{
			if (tenantInfo != null && tenantInfo.Properties != null)
			{
				this.tenantId = tenantInfo.TenantId.ToString();
				foreach (KeyValuePair<string, string> keyValuePair in tenantInfo.Properties)
				{
					if (keyValuePair.Key.Equals(TenantProperty.EXOResourceForest.Name))
					{
						this.resourceForest = keyValuePair.Value;
					}
					else if (keyValuePair.Key.Equals(TenantProperty.EXOAccountForest.Name))
					{
						this.accountForest = keyValuePair.Value;
					}
					else if (keyValuePair.Key.Equals(TenantProperty.EXOTenantContainerCN.Name))
					{
						this.tenantContainerCN = keyValuePair.Value;
					}
					else if (keyValuePair.Key.Equals(TenantProperty.EXOSmtpNextHopDomain.Name))
					{
						this.smtpNextHopDomain = keyValuePair.Value;
					}
					else if (keyValuePair.Key.Equals(TenantProperty.EXOTenantFlags.Name))
					{
						this.tenantFlags = keyValuePair.Value;
					}
				}
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00036970 File Offset: 0x00034B70
		internal void Populate(UserInfo userInfo)
		{
			if (userInfo != null)
			{
				this.msaUserNetID = userInfo.UserKey;
				this.msaUserMemberName = userInfo.MSAUserName;
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0003698D File Offset: 0x00034B8D
		internal void Populate(FindDomainResponse response)
		{
		}

		// Token: 0x04000628 RID: 1576
		private string resourceForest;

		// Token: 0x04000629 RID: 1577
		private string accountForest;

		// Token: 0x0400062A RID: 1578
		private string tenantId;

		// Token: 0x0400062B RID: 1579
		private string smtpNextHopDomain;

		// Token: 0x0400062C RID: 1580
		private string tenantFlags;

		// Token: 0x0400062D RID: 1581
		private string tenantContainerCN;

		// Token: 0x0400062E RID: 1582
		private string domainName;

		// Token: 0x0400062F RID: 1583
		private string domainInUse;

		// Token: 0x04000630 RID: 1584
		private string domainFlags;

		// Token: 0x04000631 RID: 1585
		private string msaUserNetID;

		// Token: 0x04000632 RID: 1586
		private string msaUserMemberName;
	}
}
