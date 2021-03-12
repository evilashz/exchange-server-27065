using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000498 RID: 1176
	public class SlabManifestException : Exception
	{
		// Token: 0x06002808 RID: 10248 RVA: 0x00093341 File Offset: 0x00091541
		public SlabManifestException(string message) : base(message)
		{
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x0009334A File Offset: 0x0009154A
		public SlabManifestException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
