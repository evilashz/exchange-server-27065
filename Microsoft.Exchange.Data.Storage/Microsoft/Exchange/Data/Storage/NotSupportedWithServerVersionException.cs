using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000BC RID: 188
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NotSupportedWithServerVersionException : StoragePermanentException
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x00067429 File Offset: 0x00065629
		public NotSupportedWithServerVersionException(string mailboxId, int mailboxVersion, int serverVersion) : base(ServerStrings.idNotSupportedWithServerVersionException(mailboxId, mailboxVersion, serverVersion))
		{
			this.mailboxId = mailboxId;
			this.mailboxVersion = mailboxVersion;
			this.serverVersion = serverVersion;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0006744E File Offset: 0x0006564E
		public NotSupportedWithServerVersionException(string mailboxId, int mailboxVersion, int serverVersion, Exception innerException) : base(ServerStrings.idNotSupportedWithServerVersionException(mailboxId, mailboxVersion, serverVersion), innerException)
		{
			this.mailboxId = mailboxId;
			this.mailboxVersion = mailboxVersion;
			this.serverVersion = serverVersion;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00067478 File Offset: 0x00065678
		protected NotSupportedWithServerVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxId = (string)info.GetValue("mailboxId", typeof(string));
			this.mailboxVersion = (int)info.GetValue("mailboxVersion", typeof(int));
			this.serverVersion = (int)info.GetValue("serverVersion", typeof(int));
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000674ED File Offset: 0x000656ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxId", this.mailboxId);
			info.AddValue("mailboxVersion", this.mailboxVersion);
			info.AddValue("serverVersion", this.serverVersion);
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0006752A File Offset: 0x0006572A
		public string MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00067532 File Offset: 0x00065732
		public int MailboxVersion
		{
			get
			{
				return this.mailboxVersion;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0006753A File Offset: 0x0006573A
		public int ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x0400094D RID: 2381
		private readonly string mailboxId;

		// Token: 0x0400094E RID: 2382
		private readonly int mailboxVersion;

		// Token: 0x0400094F RID: 2383
		private readonly int serverVersion;
	}
}
