using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001AA RID: 426
	[Serializable]
	public sealed class OwaPublicFolderViewStatesParseException : OwaPermanentException
	{
		// Token: 0x06000EFB RID: 3835 RVA: 0x0005E6C4 File Offset: 0x0005C8C4
		public OwaPublicFolderViewStatesParseException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}
	}
}
