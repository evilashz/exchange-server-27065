using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001BD RID: 445
	[Serializable]
	public sealed class OwaExistentNotificationPipeException : OwaNotificationPipeException
	{
		// Token: 0x06000F17 RID: 3863 RVA: 0x0005E7ED File Offset: 0x0005C9ED
		public OwaExistentNotificationPipeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0005E7F7 File Offset: 0x0005C9F7
		public OwaExistentNotificationPipeException(string message) : base(message)
		{
		}
	}
}
