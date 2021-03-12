using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon.FaultInjection
{
	// Token: 0x0200007B RID: 123
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UMLicensingFaultInjection
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x0000F25C File Offset: 0x0000D45C
		internal static bool TryCreateException(string exceptionType, ref Exception exception)
		{
			if (exceptionType != null && UMLicensingFaultInjection.RecipientTaskException.Equals(exceptionType))
			{
				exception = new StorageTransientException(new LocalizedString("This is a test purpose exception for testing"));
				return true;
			}
			return false;
		}

		// Token: 0x040002ED RID: 749
		internal const uint UMLicensingRetryOnUMDisable = 3341167933U;

		// Token: 0x040002EE RID: 750
		private static readonly string RecipientTaskException = "RecipientTaskException";
	}
}
