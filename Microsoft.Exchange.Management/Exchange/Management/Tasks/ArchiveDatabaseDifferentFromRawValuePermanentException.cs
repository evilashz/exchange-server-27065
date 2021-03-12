using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E98 RID: 3736
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ArchiveDatabaseDifferentFromRawValuePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A7D2 RID: 42962 RVA: 0x0028915B File Offset: 0x0028735B
		public ArchiveDatabaseDifferentFromRawValuePermanentException(string archiveDb, string archiveDbRaw) : base(Strings.ArchiveDatabaseDifferentFromRawValueError(archiveDb, archiveDbRaw))
		{
			this.archiveDb = archiveDb;
			this.archiveDbRaw = archiveDbRaw;
		}

		// Token: 0x0600A7D3 RID: 42963 RVA: 0x00289178 File Offset: 0x00287378
		public ArchiveDatabaseDifferentFromRawValuePermanentException(string archiveDb, string archiveDbRaw, Exception innerException) : base(Strings.ArchiveDatabaseDifferentFromRawValueError(archiveDb, archiveDbRaw), innerException)
		{
			this.archiveDb = archiveDb;
			this.archiveDbRaw = archiveDbRaw;
		}

		// Token: 0x0600A7D4 RID: 42964 RVA: 0x00289198 File Offset: 0x00287398
		protected ArchiveDatabaseDifferentFromRawValuePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.archiveDb = (string)info.GetValue("archiveDb", typeof(string));
			this.archiveDbRaw = (string)info.GetValue("archiveDbRaw", typeof(string));
		}

		// Token: 0x0600A7D5 RID: 42965 RVA: 0x002891ED File Offset: 0x002873ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("archiveDb", this.archiveDb);
			info.AddValue("archiveDbRaw", this.archiveDbRaw);
		}

		// Token: 0x1700368F RID: 13967
		// (get) Token: 0x0600A7D6 RID: 42966 RVA: 0x00289219 File Offset: 0x00287419
		public string ArchiveDb
		{
			get
			{
				return this.archiveDb;
			}
		}

		// Token: 0x17003690 RID: 13968
		// (get) Token: 0x0600A7D7 RID: 42967 RVA: 0x00289221 File Offset: 0x00287421
		public string ArchiveDbRaw
		{
			get
			{
				return this.archiveDbRaw;
			}
		}

		// Token: 0x04005FF5 RID: 24565
		private readonly string archiveDb;

		// Token: 0x04005FF6 RID: 24566
		private readonly string archiveDbRaw;
	}
}
