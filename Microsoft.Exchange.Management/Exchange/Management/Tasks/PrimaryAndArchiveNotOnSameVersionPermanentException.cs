using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E95 RID: 3733
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PrimaryAndArchiveNotOnSameVersionPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A7C4 RID: 42948 RVA: 0x00289031 File Offset: 0x00287231
		public PrimaryAndArchiveNotOnSameVersionPermanentException(string primaryVersion, string archiveVersion) : base(Strings.PrimaryAndArchiveNotOnSameVersionPostMove(primaryVersion, archiveVersion))
		{
			this.primaryVersion = primaryVersion;
			this.archiveVersion = archiveVersion;
		}

		// Token: 0x0600A7C5 RID: 42949 RVA: 0x0028904E File Offset: 0x0028724E
		public PrimaryAndArchiveNotOnSameVersionPermanentException(string primaryVersion, string archiveVersion, Exception innerException) : base(Strings.PrimaryAndArchiveNotOnSameVersionPostMove(primaryVersion, archiveVersion), innerException)
		{
			this.primaryVersion = primaryVersion;
			this.archiveVersion = archiveVersion;
		}

		// Token: 0x0600A7C6 RID: 42950 RVA: 0x0028906C File Offset: 0x0028726C
		protected PrimaryAndArchiveNotOnSameVersionPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.primaryVersion = (string)info.GetValue("primaryVersion", typeof(string));
			this.archiveVersion = (string)info.GetValue("archiveVersion", typeof(string));
		}

		// Token: 0x0600A7C7 RID: 42951 RVA: 0x002890C1 File Offset: 0x002872C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("primaryVersion", this.primaryVersion);
			info.AddValue("archiveVersion", this.archiveVersion);
		}

		// Token: 0x1700368D RID: 13965
		// (get) Token: 0x0600A7C8 RID: 42952 RVA: 0x002890ED File Offset: 0x002872ED
		public string PrimaryVersion
		{
			get
			{
				return this.primaryVersion;
			}
		}

		// Token: 0x1700368E RID: 13966
		// (get) Token: 0x0600A7C9 RID: 42953 RVA: 0x002890F5 File Offset: 0x002872F5
		public string ArchiveVersion
		{
			get
			{
				return this.archiveVersion;
			}
		}

		// Token: 0x04005FF3 RID: 24563
		private readonly string primaryVersion;

		// Token: 0x04005FF4 RID: 24564
		private readonly string archiveVersion;
	}
}
