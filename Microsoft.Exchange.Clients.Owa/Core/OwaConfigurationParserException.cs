using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001B2 RID: 434
	[Serializable]
	public sealed class OwaConfigurationParserException : OwaPermanentException
	{
		// Token: 0x06000F04 RID: 3844 RVA: 0x0005E720 File Offset: 0x0005C920
		public OwaConfigurationParserException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}
	}
}
