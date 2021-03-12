using System;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000003 RID: 3
	internal class LPVersioningValueException : ApplicationException
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020DF File Offset: 0x000002DF
		public LPVersioningValueException(string message) : base(message)
		{
			Logger.LogError(this);
		}
	}
}
