using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001A0 RID: 416
	[Serializable]
	public sealed class OwaOperationNotSupportedException : OwaPermanentException
	{
		// Token: 0x06000EDF RID: 3807 RVA: 0x0005E44F File Offset: 0x0005C64F
		public OwaOperationNotSupportedException(string message) : base(message)
		{
		}
	}
}
