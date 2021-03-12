using System;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000004 RID: 4
	internal class WebDownloaderException : ApplicationException
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020EE File Offset: 0x000002EE
		public WebDownloaderException(string message) : base(message)
		{
			Logger.LogError(this);
		}
	}
}
