using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	public class OwaADUserNotFoundException : OwaADObjectNotFoundException
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0002298E File Offset: 0x00020B8E
		public OwaADUserNotFoundException(string userName) : this(userName, null)
		{
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00022998 File Offset: 0x00020B98
		public OwaADUserNotFoundException(string userName, string message, Exception innerException) : base(message, innerException)
		{
			this.UserName = userName;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000229A9 File Offset: 0x00020BA9
		public OwaADUserNotFoundException(string userName, string message) : this(userName, message, null)
		{
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x000229B4 File Offset: 0x00020BB4
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x000229BC File Offset: 0x00020BBC
		public string UserName { get; private set; }
	}
}
