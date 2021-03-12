using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.CertificateAuthentication
{
	// Token: 0x02000002 RID: 2
	internal class CertificateADUserCache
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal CertificateADUserCache() : this(4, 300)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DE File Offset: 0x000002DE
		internal CertificateADUserCache(int expirationTimeInHours, int maximumSize)
		{
			this.expirationTimeInHours = expirationTimeInHours;
			this.maximumSize = maximumSize;
			this.Cleanup();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002108 File Offset: 0x00000308
		internal ADUser GetUser(X509Identifier certificateId)
		{
			ADUser result;
			lock (this.syncObj)
			{
				ADUser aduser;
				if (!this.Cleanup() && this.userMapping.TryGetValue(certificateId, out aduser))
				{
					result = aduser;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002164 File Offset: 0x00000364
		internal void AddUser(X509Identifier certificateId, ADUser user)
		{
			lock (this.syncObj)
			{
				this.Cleanup();
				if (this.userMapping.Count <= this.maximumSize)
				{
					this.userMapping[certificateId] = user;
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021C8 File Offset: 0x000003C8
		private bool Cleanup()
		{
			if (this.userMapping == null || DateTime.UtcNow.CompareTo(this.expirationTimeUTC) > 0)
			{
				this.userMapping = new Dictionary<X509Identifier, ADUser>();
				this.expirationTimeUTC = DateTime.UtcNow.AddHours((double)this.expirationTimeInHours);
				return true;
			}
			return false;
		}

		// Token: 0x04000001 RID: 1
		internal const int CacheDefaultMaximumSize = 300;

		// Token: 0x04000002 RID: 2
		internal const int CacheDefaultExpirationTimeInHours = 4;

		// Token: 0x04000003 RID: 3
		private int expirationTimeInHours;

		// Token: 0x04000004 RID: 4
		private DateTime expirationTimeUTC;

		// Token: 0x04000005 RID: 5
		private int maximumSize;

		// Token: 0x04000006 RID: 6
		private Dictionary<X509Identifier, ADUser> userMapping;

		// Token: 0x04000007 RID: 7
		private object syncObj = new object();
	}
}
