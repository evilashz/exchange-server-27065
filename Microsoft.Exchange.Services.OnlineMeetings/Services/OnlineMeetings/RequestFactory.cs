using System;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200003C RID: 60
	internal abstract class RequestFactory
	{
		// Token: 0x06000222 RID: 546
		internal abstract UcwaWebRequest CreateRequest(string absoluteUri, string method);
	}
}
