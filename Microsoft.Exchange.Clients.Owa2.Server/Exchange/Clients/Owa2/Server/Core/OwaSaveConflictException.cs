using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000120 RID: 288
	[Serializable]
	public sealed class OwaSaveConflictException : OwaPermanentException
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x00022C89 File Offset: 0x00020E89
		public OwaSaveConflictException(string message, object thisObject) : base(message, null, thisObject)
		{
		}
	}
}
