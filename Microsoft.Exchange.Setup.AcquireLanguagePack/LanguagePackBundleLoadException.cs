using System;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000002 RID: 2
	internal class LanguagePackBundleLoadException : ApplicationException
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public LanguagePackBundleLoadException(string message) : base(message)
		{
			Logger.LogError(this);
		}
	}
}
