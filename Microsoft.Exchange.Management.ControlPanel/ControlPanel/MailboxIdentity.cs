using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006A7 RID: 1703
	[DataContract]
	public class MailboxIdentity : Identity
	{
		// Token: 0x060048E6 RID: 18662 RVA: 0x000DF087 File Offset: 0x000DD287
		public MailboxIdentity(MailboxId rawIdentity) : base(rawIdentity.ToString(), rawIdentity.ToString())
		{
		}
	}
}
