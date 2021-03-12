using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	public sealed class OwaInvalidOperationException : OwaPermanentException
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x00022BB5 File Offset: 0x00020DB5
		public OwaInvalidOperationException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00022BC0 File Offset: 0x00020DC0
		public OwaInvalidOperationException(string message) : base(message)
		{
		}
	}
}
