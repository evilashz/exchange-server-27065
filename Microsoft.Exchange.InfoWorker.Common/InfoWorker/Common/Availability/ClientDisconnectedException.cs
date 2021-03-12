using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200009D RID: 157
	internal class ClientDisconnectedException : AvailabilityException
	{
		// Token: 0x06000367 RID: 871 RVA: 0x0000E9E1 File Offset: 0x0000CBE1
		public ClientDisconnectedException() : base(ErrorConstants.ClientDisconnected, Strings.descClientDisconnected)
		{
		}
	}
}
