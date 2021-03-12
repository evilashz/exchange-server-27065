using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	public sealed class OwaSegmentationException : OwaPermanentException
	{
		// Token: 0x06000F02 RID: 3842 RVA: 0x0005E70C File Offset: 0x0005C90C
		public OwaSegmentationException(string message) : base(message)
		{
		}
	}
}
