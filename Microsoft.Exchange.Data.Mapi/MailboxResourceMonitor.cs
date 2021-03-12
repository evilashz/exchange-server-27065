using System;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public sealed class MailboxResourceMonitor : MailboxResourceMonitorEntry
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000073EC File Offset: 0x000055EC
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxResourceMonitor.schema;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000073F3 File Offset: 0x000055F3
		public string DigestCategory
		{
			get
			{
				return (string)this[MailboxResourceMonitorSchema.DigestCategory];
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00007405 File Offset: 0x00005605
		public uint? SampleId
		{
			get
			{
				return (uint?)this[MailboxResourceMonitorSchema.SampleId];
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00007417 File Offset: 0x00005617
		public DateTime? SampleTime
		{
			get
			{
				return (DateTime?)this[MailboxResourceMonitorSchema.SampleTime];
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00007429 File Offset: 0x00005629
		public string DisplayName
		{
			get
			{
				return (string)this[MailboxResourceMonitorSchema.DisplayName];
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000743B File Offset: 0x0000563B
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)this[MailboxResourceMonitorSchema.MailboxGuid];
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000744D File Offset: 0x0000564D
		public uint? TimeInServer
		{
			get
			{
				return (uint?)this[MailboxResourceMonitorSchema.TimeInServer];
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000745F File Offset: 0x0000565F
		public uint? TimeInCPU
		{
			get
			{
				return (uint?)this[MailboxResourceMonitorSchema.TimeInCPU];
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00007471 File Offset: 0x00005671
		public uint? ROPCount
		{
			get
			{
				return (uint?)this[MailboxResourceMonitorSchema.ROPCount];
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00007483 File Offset: 0x00005683
		public uint? PageRead
		{
			get
			{
				return (uint?)this[MailboxResourceMonitorSchema.PageRead];
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00007495 File Offset: 0x00005695
		public uint? PagePreread
		{
			get
			{
				return (uint?)this[MailboxResourceMonitorSchema.PagePreread];
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000074A7 File Offset: 0x000056A7
		public uint? LogRecordCount
		{
			get
			{
				return (uint?)this[MailboxResourceMonitorSchema.LogRecordCount];
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000074B9 File Offset: 0x000056B9
		public uint? LogRecordBytes
		{
			get
			{
				return (uint?)this[MailboxResourceMonitorSchema.LogRecordBytes];
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000074CB File Offset: 0x000056CB
		public uint? LdapReads
		{
			get
			{
				return (uint?)this[MailboxResourceMonitorSchema.LdapReads];
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000074DD File Offset: 0x000056DD
		public uint? LdapSearches
		{
			get
			{
				return (uint?)this[MailboxResourceMonitorSchema.LdapSearches];
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000074EF File Offset: 0x000056EF
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000074F7 File Offset: 0x000056F7
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
			internal set
			{
				this.serverName = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00007500 File Offset: 0x00005700
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00007508 File Offset: 0x00005708
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
			internal set
			{
				this.databaseName = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00007511 File Offset: 0x00005711
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00007519 File Offset: 0x00005719
		public bool IsDatabaseCopyActive
		{
			get
			{
				return this.isDatabaseCopyActive;
			}
			internal set
			{
				this.isDatabaseCopyActive = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00007522 File Offset: 0x00005722
		public bool? IsQuarantined
		{
			get
			{
				return (bool?)this[MailboxResourceMonitorSchema.IsQuarantined];
			}
		}

		// Token: 0x040000EC RID: 236
		private string serverName;

		// Token: 0x040000ED RID: 237
		private string databaseName;

		// Token: 0x040000EE RID: 238
		private bool isDatabaseCopyActive;

		// Token: 0x040000EF RID: 239
		private static MapiObjectSchema schema = ObjectSchema.GetInstance<MailboxResourceMonitorSchema>();
	}
}
