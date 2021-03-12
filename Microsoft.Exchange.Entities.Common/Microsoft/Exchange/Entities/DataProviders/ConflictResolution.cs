using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.DataProviders
{
	// Token: 0x0200000F RID: 15
	public static class ConflictResolution
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002DF5 File Offset: 0x00000FF5
		internal static void ThrowOnIrresolvableConflict(this ConflictResolutionResult result)
		{
			if (result.SaveStatus == SaveResult.IrresolvableConflict)
			{
				throw new IrresolvableConflictException(result);
			}
		}
	}
}
