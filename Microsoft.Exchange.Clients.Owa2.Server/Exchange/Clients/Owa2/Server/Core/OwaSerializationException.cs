using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000122 RID: 290
	[Serializable]
	public sealed class OwaSerializationException : OwaPermanentException
	{
		// Token: 0x060009CC RID: 2508 RVA: 0x00022CBB File Offset: 0x00020EBB
		public OwaSerializationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
