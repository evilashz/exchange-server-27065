using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001B1 RID: 433
	[Serializable]
	public sealed class OwaFolderMruParserException : OwaPermanentException
	{
		// Token: 0x06000F03 RID: 3843 RVA: 0x0005E715 File Offset: 0x0005C915
		public OwaFolderMruParserException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}
	}
}
