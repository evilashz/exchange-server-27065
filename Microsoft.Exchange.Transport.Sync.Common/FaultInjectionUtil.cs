using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200006E RID: 110
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class FaultInjectionUtil
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public static Exception Callback(string exceptionType)
		{
			Exception result = null;
			if (exceptionType != null)
			{
				if (typeof(NullReferenceException).FullName.Contains(exceptionType))
				{
					result = new NullReferenceException("Fault Injection");
				}
				if (typeof(UnauthorizedAccessException).FullName.Contains(exceptionType))
				{
					result = new UnauthorizedAccessException("Fault Injection");
				}
				if (typeof(EndpointContainerNotFoundException).FullName.Contains(exceptionType))
				{
					result = new EndpointContainerNotFoundException("Fault Injection");
				}
				if (typeof(StorageTransientException).FullName.Contains(exceptionType))
				{
					result = new StorageTransientException(LocalizedString.Empty);
				}
				if (typeof(StoragePermanentException).FullName.Contains(exceptionType))
				{
					result = new StoragePermanentException(LocalizedString.Empty);
				}
			}
			return result;
		}

		// Token: 0x04000126 RID: 294
		private const string ExceptionMessage = "Fault Injection";
	}
}
