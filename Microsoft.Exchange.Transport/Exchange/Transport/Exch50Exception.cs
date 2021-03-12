using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000445 RID: 1093
	internal class Exch50Exception : ApplicationException
	{
		// Token: 0x0600327D RID: 12925 RVA: 0x000C5B0D File Offset: 0x000C3D0D
		public Exch50Exception(string message) : base(message)
		{
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000C5B16 File Offset: 0x000C3D16
		public Exch50Exception(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
