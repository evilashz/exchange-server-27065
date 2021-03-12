using System;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000E4 RID: 228
	internal class VersionedWebServiceUri
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x00019D94 File Offset: 0x00017F94
		public static VersionedWebServiceUri Create(WebServiceUri webServiceUri, int versionBucket)
		{
			VersionedWebServiceUri versionedWebServiceUri = new VersionedWebServiceUri();
			versionedWebServiceUri.Set(webServiceUri, versionBucket);
			return versionedWebServiceUri;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00019DB0 File Offset: 0x00017FB0
		public void Set(WebServiceUri webServiceUri, int versionBucket)
		{
			if (webServiceUri == null)
			{
				throw new ArgumentNullException("webServiceUri");
			}
			if (!LocalizedString.Empty.Equals(webServiceUri.AutodiscoverFailedExceptionString))
			{
				this.uriByVersionBuckets[versionBucket] = webServiceUri;
				return;
			}
			for (int i = versionBucket; i > -1; i--)
			{
				if (i == versionBucket)
				{
					this.uriByVersionBuckets[i] = webServiceUri;
				}
				else
				{
					WebServiceUri webServiceUri2 = this.uriByVersionBuckets[i];
					if (webServiceUri2 == null)
					{
						this.uriByVersionBuckets[i] = webServiceUri;
					}
					else if (!LocalizedString.Empty.Equals(webServiceUri2.AutodiscoverFailedExceptionString))
					{
						this.uriByVersionBuckets[i] = webServiceUri;
					}
				}
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00019E3A File Offset: 0x0001803A
		public WebServiceUri Get(int versionBucket)
		{
			return this.uriByVersionBuckets[versionBucket];
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00019E44 File Offset: 0x00018044
		public void Clear(int versionBucket)
		{
			this.uriByVersionBuckets[versionBucket] = null;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00019E50 File Offset: 0x00018050
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			for (int i = 0; i < this.uriByVersionBuckets.Length; i++)
			{
				WebServiceUri webServiceUri = this.uriByVersionBuckets[i];
				if (webServiceUri != null)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						i,
						":",
						webServiceUri.ToString(),
						";"
					}));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000366 RID: 870
		private WebServiceUri[] uriByVersionBuckets = new WebServiceUri[4];
	}
}
