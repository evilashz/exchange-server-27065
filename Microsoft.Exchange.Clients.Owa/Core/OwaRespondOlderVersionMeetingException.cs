using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001CE RID: 462
	[Serializable]
	public class OwaRespondOlderVersionMeetingException : OwaPermanentException
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x0005E985 File Offset: 0x0005CB85
		// (set) Token: 0x06000F3C RID: 3900 RVA: 0x0005E98D File Offset: 0x0005CB8D
		public string SharerDisplayName { get; private set; }

		// Token: 0x06000F3D RID: 3901 RVA: 0x0005E996 File Offset: 0x0005CB96
		public OwaRespondOlderVersionMeetingException(string message, string sharerDisplayName) : base(message)
		{
			this.SharerDisplayName = sharerDisplayName;
		}
	}
}
