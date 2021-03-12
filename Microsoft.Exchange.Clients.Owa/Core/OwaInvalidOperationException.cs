using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200019F RID: 415
	[Serializable]
	public sealed class OwaInvalidOperationException : OwaPermanentException
	{
		// Token: 0x06000EDD RID: 3805 RVA: 0x0005E43B File Offset: 0x0005C63B
		public OwaInvalidOperationException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0005E446 File Offset: 0x0005C646
		public OwaInvalidOperationException(string message) : base(message)
		{
		}
	}
}
