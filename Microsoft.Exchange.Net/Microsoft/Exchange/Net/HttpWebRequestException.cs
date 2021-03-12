using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000738 RID: 1848
	internal class HttpWebRequestException : Exception
	{
		// Token: 0x060023DC RID: 9180 RVA: 0x0004A1F3 File Offset: 0x000483F3
		public HttpWebRequestException(Exception exception) : base("HttpWebRequest exception", exception)
		{
		}
	}
}
