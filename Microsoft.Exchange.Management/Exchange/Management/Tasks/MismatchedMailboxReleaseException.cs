using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E8A RID: 3722
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MismatchedMailboxReleaseException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A786 RID: 42886 RVA: 0x002888B4 File Offset: 0x00286AB4
		public MismatchedMailboxReleaseException(string mailbox, string database, string serverMailboxRelease, string desiredMailboxRelease) : base(Strings.ErrorMismatchedMailboxRelease(mailbox, database, serverMailboxRelease, desiredMailboxRelease))
		{
			this.mailbox = mailbox;
			this.database = database;
			this.serverMailboxRelease = serverMailboxRelease;
			this.desiredMailboxRelease = desiredMailboxRelease;
		}

		// Token: 0x0600A787 RID: 42887 RVA: 0x002888E3 File Offset: 0x00286AE3
		public MismatchedMailboxReleaseException(string mailbox, string database, string serverMailboxRelease, string desiredMailboxRelease, Exception innerException) : base(Strings.ErrorMismatchedMailboxRelease(mailbox, database, serverMailboxRelease, desiredMailboxRelease), innerException)
		{
			this.mailbox = mailbox;
			this.database = database;
			this.serverMailboxRelease = serverMailboxRelease;
			this.desiredMailboxRelease = desiredMailboxRelease;
		}

		// Token: 0x0600A788 RID: 42888 RVA: 0x00288914 File Offset: 0x00286B14
		protected MismatchedMailboxReleaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
			this.database = (string)info.GetValue("database", typeof(string));
			this.serverMailboxRelease = (string)info.GetValue("serverMailboxRelease", typeof(string));
			this.desiredMailboxRelease = (string)info.GetValue("desiredMailboxRelease", typeof(string));
		}

		// Token: 0x0600A789 RID: 42889 RVA: 0x002889AC File Offset: 0x00286BAC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
			info.AddValue("database", this.database);
			info.AddValue("serverMailboxRelease", this.serverMailboxRelease);
			info.AddValue("desiredMailboxRelease", this.desiredMailboxRelease);
		}

		// Token: 0x1700367B RID: 13947
		// (get) Token: 0x0600A78A RID: 42890 RVA: 0x00288A05 File Offset: 0x00286C05
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x1700367C RID: 13948
		// (get) Token: 0x0600A78B RID: 42891 RVA: 0x00288A0D File Offset: 0x00286C0D
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x1700367D RID: 13949
		// (get) Token: 0x0600A78C RID: 42892 RVA: 0x00288A15 File Offset: 0x00286C15
		public string ServerMailboxRelease
		{
			get
			{
				return this.serverMailboxRelease;
			}
		}

		// Token: 0x1700367E RID: 13950
		// (get) Token: 0x0600A78D RID: 42893 RVA: 0x00288A1D File Offset: 0x00286C1D
		public string DesiredMailboxRelease
		{
			get
			{
				return this.desiredMailboxRelease;
			}
		}

		// Token: 0x04005FE1 RID: 24545
		private readonly string mailbox;

		// Token: 0x04005FE2 RID: 24546
		private readonly string database;

		// Token: 0x04005FE3 RID: 24547
		private readonly string serverMailboxRelease;

		// Token: 0x04005FE4 RID: 24548
		private readonly string desiredMailboxRelease;
	}
}
