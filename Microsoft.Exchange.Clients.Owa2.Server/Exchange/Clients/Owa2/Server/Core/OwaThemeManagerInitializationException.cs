using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000124 RID: 292
	[Serializable]
	public sealed class OwaThemeManagerInitializationException : OwaPermanentException
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x00022D08 File Offset: 0x00020F08
		public OwaThemeManagerInitializationException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00022D13 File Offset: 0x00020F13
		public OwaThemeManagerInitializationException(string message) : base(message)
		{
		}
	}
}
