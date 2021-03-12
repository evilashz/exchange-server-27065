using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000093 RID: 147
	internal class AddressSpaceNotFoundException : AvailabilityException
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0000E916 File Offset: 0x0000CB16
		public AddressSpaceNotFoundException(LocalizedString message, uint locationIdentifier) : base(ErrorConstants.AddressSpaceNotFound, message, locationIdentifier)
		{
		}
	}
}
