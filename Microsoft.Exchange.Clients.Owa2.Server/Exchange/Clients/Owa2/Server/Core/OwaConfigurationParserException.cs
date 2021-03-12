using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200010A RID: 266
	[Serializable]
	public sealed class OwaConfigurationParserException : OwaPermanentException
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x00022A1B File Offset: 0x00020C1B
		public OwaConfigurationParserException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}
	}
}
