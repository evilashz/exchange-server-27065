using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200019C RID: 412
	[Serializable]
	public sealed class OwaThemeManagerInitializationException : OwaPermanentException
	{
		// Token: 0x06000ED8 RID: 3800 RVA: 0x0005E40A File Offset: 0x0005C60A
		public OwaThemeManagerInitializationException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0005E415 File Offset: 0x0005C615
		public OwaThemeManagerInitializationException(string message) : base(message)
		{
		}
	}
}
