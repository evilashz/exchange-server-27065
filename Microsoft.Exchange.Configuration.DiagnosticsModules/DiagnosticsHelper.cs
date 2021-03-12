using System;
using System.Collections.Specialized;
using System.Web;

namespace Microsoft.Exchange.Configuration.DiagnosticsModules
{
	// Token: 0x02000003 RID: 3
	internal class DiagnosticsHelper
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002323 File Offset: 0x00000523
		internal static NameValueCollection GetUrlProperties(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			return HttpUtility.ParseQueryString(uri.Query.Replace(';', '&'));
		}
	}
}
