using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000574 RID: 1396
	[Serializable]
	public class MapiTransactionOutcome : ConfigurableObject
	{
		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003131 RID: 12593 RVA: 0x000C8774 File Offset: 0x000C6974
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MapiTransactionOutcome.schema;
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x000C877B File Offset: 0x000C697B
		// (set) Token: 0x06003133 RID: 12595 RVA: 0x000C878D File Offset: 0x000C698D
		public string Server
		{
			get
			{
				return (string)this[MapiTransactionOutcomeSchema.Server];
			}
			internal set
			{
				this[MapiTransactionOutcomeSchema.Server] = value;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x000C879B File Offset: 0x000C699B
		// (set) Token: 0x06003135 RID: 12597 RVA: 0x000C87AD File Offset: 0x000C69AD
		public string Database
		{
			get
			{
				return (string)this[MapiTransactionOutcomeSchema.Database];
			}
			internal set
			{
				this[MapiTransactionOutcomeSchema.Database] = value;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x000C87BB File Offset: 0x000C69BB
		// (set) Token: 0x06003137 RID: 12599 RVA: 0x000C87CD File Offset: 0x000C69CD
		public string Mailbox
		{
			get
			{
				return (string)this[MapiTransactionOutcomeSchema.Mailbox];
			}
			internal set
			{
				this[MapiTransactionOutcomeSchema.Mailbox] = value;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x000C87DB File Offset: 0x000C69DB
		// (set) Token: 0x06003139 RID: 12601 RVA: 0x000C87ED File Offset: 0x000C69ED
		public Guid? MailboxGuid
		{
			get
			{
				return (Guid?)this[MapiTransactionOutcomeSchema.MailboxGuid];
			}
			internal set
			{
				this[MapiTransactionOutcomeSchema.MailboxGuid] = value;
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x000C8800 File Offset: 0x000C6A00
		// (set) Token: 0x0600313B RID: 12603 RVA: 0x000C8812 File Offset: 0x000C6A12
		public bool? IsArchive
		{
			get
			{
				return (bool?)this[MapiTransactionOutcomeSchema.IsArchive];
			}
			internal set
			{
				this[MapiTransactionOutcomeSchema.IsArchive] = value;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x000C8825 File Offset: 0x000C6A25
		// (set) Token: 0x0600313D RID: 12605 RVA: 0x000C8837 File Offset: 0x000C6A37
		public bool IsDatabaseCopyActive
		{
			get
			{
				return (bool)this[MapiTransactionOutcomeSchema.IsDatabaseCopyActive];
			}
			internal set
			{
				this[MapiTransactionOutcomeSchema.IsDatabaseCopyActive] = value;
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x0600313E RID: 12606 RVA: 0x000C884A File Offset: 0x000C6A4A
		// (set) Token: 0x0600313F RID: 12607 RVA: 0x000C885C File Offset: 0x000C6A5C
		public MapiTransactionResult Result
		{
			get
			{
				return (MapiTransactionResult)this[MapiTransactionOutcomeSchema.Result];
			}
			internal set
			{
				this[MapiTransactionOutcomeSchema.Result] = value;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x000C886A File Offset: 0x000C6A6A
		// (set) Token: 0x06003141 RID: 12609 RVA: 0x000C888A File Offset: 0x000C6A8A
		public TimeSpan Latency
		{
			get
			{
				return (TimeSpan)(this[MapiTransactionOutcomeSchema.Latency] ?? TimeSpan.Zero);
			}
			internal set
			{
				this[MapiTransactionOutcomeSchema.Latency] = value;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000C889D File Offset: 0x000C6A9D
		// (set) Token: 0x06003143 RID: 12611 RVA: 0x000C88B4 File Offset: 0x000C6AB4
		public string Error
		{
			get
			{
				return (string)this.propertyBag[MapiTransactionOutcomeSchema.Error];
			}
			internal set
			{
				this.propertyBag[MapiTransactionOutcomeSchema.Error] = value;
			}
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000C88C7 File Offset: 0x000C6AC7
		public MapiTransactionOutcome() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000C88E0 File Offset: 0x000C6AE0
		public MapiTransactionOutcome(Server server, Database database, ADRecipient adRecipient) : base(new SimpleProviderPropertyBag())
		{
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			this.Server = (server.Name ?? string.Empty);
			this.Database = (database.Name ?? string.Empty);
			this.Mailbox = ((adRecipient == null) ? null : (adRecipient.Name ?? null));
			this.Result = new MapiTransactionResult(MapiTransactionResultEnum.Undefined);
			this.transactionTarget = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", new object[]
			{
				this.Server,
				this.Database
			});
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000C899C File Offset: 0x000C6B9C
		internal void Update(MapiTransactionResultEnum resultEnum, TimeSpan latency, string error, Guid? mailboxGuid, MailboxMiscFlags? mailboxMiscFlags, bool isDatabaseCopyActive)
		{
			lock (this.thisLock)
			{
				this.Result = new MapiTransactionResult(resultEnum);
				this.Latency = latency;
				this.MailboxGuid = mailboxGuid;
				this.IsArchive = ((mailboxMiscFlags != null) ? new bool?((mailboxMiscFlags & MailboxMiscFlags.ArchiveMailbox) == MailboxMiscFlags.ArchiveMailbox) : null);
				this.IsDatabaseCopyActive = isDatabaseCopyActive;
				this.Error = (error ?? string.Empty);
			}
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000C8A68 File Offset: 0x000C6C68
		internal string ShortTargetString()
		{
			return this.transactionTarget;
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000C8A70 File Offset: 0x000C6C70
		internal string LongTargetString()
		{
			return this.transactionTarget + "\\" + (this.Mailbox ?? string.Empty);
		}

		// Token: 0x040022DB RID: 8923
		private const string targetItemSeparator = "\\";

		// Token: 0x040022DC RID: 8924
		private const string transactionTargetFormatStr = "{0}\\{1}";

		// Token: 0x040022DD RID: 8925
		private readonly string transactionTarget;

		// Token: 0x040022DE RID: 8926
		[NonSerialized]
		private object thisLock = new object();

		// Token: 0x040022DF RID: 8927
		private static MapiTransactionOutcomeSchema schema = ObjectSchema.GetInstance<MapiTransactionOutcomeSchema>();
	}
}
