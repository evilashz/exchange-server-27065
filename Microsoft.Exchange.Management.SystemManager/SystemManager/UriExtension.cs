using System;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000031 RID: 49
	public static class UriExtension
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000872C File Offset: 0x0000692C
		public static bool IsHttp(this Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			return uri.Scheme.Equals(Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008753 File Offset: 0x00006953
		public static bool IsHttps(this Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			return uri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);
		}
	}
}
