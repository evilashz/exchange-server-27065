using System;
using System.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000739 RID: 1849
	internal class RestrictedCredentials : ICredentials
	{
		// Token: 0x060023DD RID: 9181 RVA: 0x0004A201 File Offset: 0x00048401
		public RestrictedCredentials(ICredentials credentials, Func<Uri, string, bool> criteria)
		{
			if (credentials == null)
			{
				throw new ArgumentNullException("credentials");
			}
			if (criteria == null)
			{
				throw new ArgumentNullException("criteria");
			}
			this.credentials = credentials;
			this.criteria = criteria;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x0004A24C File Offset: 0x0004844C
		public RestrictedCredentials(ICredentials credentials, Func<string, bool> criteria) : this(credentials, (Uri uri, string authtype) => criteria(authtype))
		{
			if (criteria == null)
			{
				throw new ArgumentNullException("criteria");
			}
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x0004A293 File Offset: 0x00048493
		public NetworkCredential GetCredential(Uri uri, string authType)
		{
			if (this.criteria(uri, authType))
			{
				return this.credentials.GetCredential(uri, authType);
			}
			return null;
		}

		// Token: 0x040021B8 RID: 8632
		private readonly ICredentials credentials;

		// Token: 0x040021B9 RID: 8633
		private readonly Func<Uri, string, bool> criteria;
	}
}
