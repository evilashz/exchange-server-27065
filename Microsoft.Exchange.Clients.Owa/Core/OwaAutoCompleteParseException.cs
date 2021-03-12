using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001AB RID: 427
	[Serializable]
	public sealed class OwaAutoCompleteParseException : OwaPermanentException
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x0005E6CF File Offset: 0x0005C8CF
		public OwaAutoCompleteParseException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}
	}
}
