using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public class OwaDefaultFolderIdUnavailableException : OwaPermanentException
	{
		// Token: 0x06000F24 RID: 3876 RVA: 0x0005E8A2 File Offset: 0x0005CAA2
		public OwaDefaultFolderIdUnavailableException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0005E8AC File Offset: 0x0005CAAC
		public OwaDefaultFolderIdUnavailableException(string message) : base(message)
		{
		}
	}
}
