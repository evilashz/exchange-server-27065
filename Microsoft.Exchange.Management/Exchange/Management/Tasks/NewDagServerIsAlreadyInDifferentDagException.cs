using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001068 RID: 4200
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NewDagServerIsAlreadyInDifferentDagException : LocalizedException
	{
		// Token: 0x0600B0DF RID: 45279 RVA: 0x00296F55 File Offset: 0x00295155
		public NewDagServerIsAlreadyInDifferentDagException(string mailboxServer, string currentDag, string desiredDag) : base(Strings.NewDagServerIsAlreadyInDifferentDagException(mailboxServer, currentDag, desiredDag))
		{
			this.mailboxServer = mailboxServer;
			this.currentDag = currentDag;
			this.desiredDag = desiredDag;
		}

		// Token: 0x0600B0E0 RID: 45280 RVA: 0x00296F7A File Offset: 0x0029517A
		public NewDagServerIsAlreadyInDifferentDagException(string mailboxServer, string currentDag, string desiredDag, Exception innerException) : base(Strings.NewDagServerIsAlreadyInDifferentDagException(mailboxServer, currentDag, desiredDag), innerException)
		{
			this.mailboxServer = mailboxServer;
			this.currentDag = currentDag;
			this.desiredDag = desiredDag;
		}

		// Token: 0x0600B0E1 RID: 45281 RVA: 0x00296FA4 File Offset: 0x002951A4
		protected NewDagServerIsAlreadyInDifferentDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxServer = (string)info.GetValue("mailboxServer", typeof(string));
			this.currentDag = (string)info.GetValue("currentDag", typeof(string));
			this.desiredDag = (string)info.GetValue("desiredDag", typeof(string));
		}

		// Token: 0x0600B0E2 RID: 45282 RVA: 0x00297019 File Offset: 0x00295219
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxServer", this.mailboxServer);
			info.AddValue("currentDag", this.currentDag);
			info.AddValue("desiredDag", this.desiredDag);
		}

		// Token: 0x1700385C RID: 14428
		// (get) Token: 0x0600B0E3 RID: 45283 RVA: 0x00297056 File Offset: 0x00295256
		public string MailboxServer
		{
			get
			{
				return this.mailboxServer;
			}
		}

		// Token: 0x1700385D RID: 14429
		// (get) Token: 0x0600B0E4 RID: 45284 RVA: 0x0029705E File Offset: 0x0029525E
		public string CurrentDag
		{
			get
			{
				return this.currentDag;
			}
		}

		// Token: 0x1700385E RID: 14430
		// (get) Token: 0x0600B0E5 RID: 45285 RVA: 0x00297066 File Offset: 0x00295266
		public string DesiredDag
		{
			get
			{
				return this.desiredDag;
			}
		}

		// Token: 0x040061C2 RID: 25026
		private readonly string mailboxServer;

		// Token: 0x040061C3 RID: 25027
		private readonly string currentDag;

		// Token: 0x040061C4 RID: 25028
		private readonly string desiredDag;
	}
}
