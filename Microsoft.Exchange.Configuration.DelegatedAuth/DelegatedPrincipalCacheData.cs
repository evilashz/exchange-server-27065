using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x02000006 RID: 6
	internal class DelegatedPrincipalCacheData
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00003F1D File Offset: 0x0000211D
		internal DelegatedPrincipalCacheData(DelegatedPrincipal principal, DateTime expirationUTC)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			this.principal = principal;
			this.expiration = expirationUTC;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003F41 File Offset: 0x00002141
		internal DelegatedPrincipal Principal
		{
			get
			{
				return this.principal;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003F49 File Offset: 0x00002149
		internal DateTime UTCExpirationTime
		{
			get
			{
				return this.expiration;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003F51 File Offset: 0x00002151
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00003F59 File Offset: 0x00002159
		internal DateTime LastReadTime
		{
			get
			{
				return this.lastReadTime;
			}
			set
			{
				this.lastReadTime = value;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003F62 File Offset: 0x00002162
		internal bool IsExpired()
		{
			return DateTime.UtcNow > this.expiration;
		}

		// Token: 0x04000021 RID: 33
		private DelegatedPrincipal principal;

		// Token: 0x04000022 RID: 34
		private DateTime expiration;

		// Token: 0x04000023 RID: 35
		private DateTime lastReadTime;
	}
}
