using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000091 RID: 145
	internal class AutoDiscoverFailedException : AvailabilityException
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000E8D1 File Offset: 0x0000CAD1
		public AutoDiscoverFailedException(LocalizedString message) : base(ErrorConstants.AutoDiscoverFailed, message)
		{
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000E8DF File Offset: 0x0000CADF
		public AutoDiscoverFailedException(LocalizedString message, uint locationIdentifier) : base(ErrorConstants.AutoDiscoverFailed, message, locationIdentifier)
		{
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000E8EE File Offset: 0x0000CAEE
		public AutoDiscoverFailedException(LocalizedString message, Exception innerException) : base(ErrorConstants.AutoDiscoverFailed, message, innerException)
		{
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000E8FD File Offset: 0x0000CAFD
		public AutoDiscoverFailedException(LocalizedString message, Exception innerException, uint locationIdentifier) : base(ErrorConstants.AutoDiscoverFailed, message, innerException, locationIdentifier)
		{
		}
	}
}
