using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000FC RID: 252
	internal class VersionTable : DataTable
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x00024A2A File Offset: 0x00022C2A
		public VersionTable(DatabaseAutoRecovery databaseAutoRecovery, long requiredVersion, long desiredRevision = 0L)
		{
			this.databaseAutoRecovery = databaseAutoRecovery;
			this.requiredVersion = requiredVersion;
			this.desiredRevision = desiredRevision;
			this.isAttached = false;
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00024A4E File Offset: 0x00022C4E
		public long DesiredRevision
		{
			get
			{
				return this.desiredRevision;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00024A56 File Offset: 0x00022C56
		public long CurrentVersion
		{
			get
			{
				if (!this.isAttached)
				{
					throw new InvalidOperationException("Version table not attached yet.");
				}
				return this.currentVersion;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00024A71 File Offset: 0x00022C71
		public long CurrentRevision
		{
			get
			{
				if (!this.isAttached)
				{
					throw new InvalidOperationException("Version table not attached yet.");
				}
				return this.currentRevision;
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00024A8C File Offset: 0x00022C8C
		public void UpgradeRevision(Transaction transaction, long newRevision)
		{
			DataColumn<long> dataColumn = (DataColumn<long>)base.Schemas[1];
			lock (this)
			{
				using (DataTableCursor dataTableCursor = this.OpenCursor(transaction.Connection))
				{
					dataTableCursor.MoveFirst();
					dataTableCursor.PrepareUpdate(true);
					dataColumn.WriteToCursor(dataTableCursor, newRevision);
					dataTableCursor.Update();
					this.currentRevision = newRevision;
				}
			}
			ExTraceGlobals.StorageTracer.TraceDebug<long, long>((long)this.GetHashCode(), "Database upgraded to version: {0} revision:{1}", this.currentVersion, this.currentRevision);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00024B3C File Offset: 0x00022D3C
		protected override void AttachLoadInitValues(Transaction transaction, DataTableCursor cursor)
		{
			DataColumn<long> dataColumn = (DataColumn<long>)base.Schemas[0];
			DataColumn<long> dataColumn2 = (DataColumn<long>)base.Schemas[1];
			if (base.IsNewTable)
			{
				cursor.PrepareInsert(false, false);
				dataColumn.WriteToCursor(cursor, this.requiredVersion);
				dataColumn2.WriteToCursor(cursor, this.desiredRevision);
				cursor.Update();
				this.currentVersion = this.requiredVersion;
				this.currentRevision = this.desiredRevision;
				ExTraceGlobals.StorageTracer.TraceDebug<long>((long)this.GetHashCode(), "Database created with version: {0}", this.requiredVersion);
			}
			else
			{
				if (cursor.TryMoveFirst())
				{
					this.currentVersion = dataColumn.ReadFromCursor(cursor);
					this.currentRevision = dataColumn2.ReadFromCursor(cursor);
				}
				ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "Database opened with version: {0} required: {1} revision:{2} desired:{3}", new object[]
				{
					this.currentVersion,
					this.requiredVersion,
					this.currentRevision,
					this.desiredRevision
				});
				if (this.currentVersion != this.requiredVersion)
				{
					string text = string.Empty;
					string text2 = string.Empty;
					try
					{
						text = cursor.Connection.Source.DatabasePath;
						text2 = cursor.Connection.Source.LogFilePath;
						if (this.databaseAutoRecovery != null)
						{
							this.databaseAutoRecovery.SetDatabaseCorruptionFlag();
						}
					}
					catch (ObjectDisposedException)
					{
					}
					Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_DatabaseSchemaNotSupported, null, new object[]
					{
						Strings.MessagingDatabaseInstanceName,
						this.currentVersion,
						this.requiredVersion,
						text,
						text2
					});
					throw new TransportComponentLoadFailedException(Strings.DatabaseSchemaNotSupported(Strings.MessagingDatabaseInstanceName), new TransientException(Strings.DatabaseSchemaNotSupported(Strings.MessagingDatabaseInstanceName)));
				}
			}
			this.isAttached = true;
		}

		// Token: 0x04000466 RID: 1126
		[DataColumnDefinition(typeof(long), ColumnAccess.CachedProp, Required = true)]
		public const int Version = 0;

		// Token: 0x04000467 RID: 1127
		[DataColumnDefinition(typeof(long), ColumnAccess.CachedProp, Required = false)]
		public const int Revision = 1;

		// Token: 0x04000468 RID: 1128
		private readonly long requiredVersion;

		// Token: 0x04000469 RID: 1129
		private readonly long desiredRevision;

		// Token: 0x0400046A RID: 1130
		private readonly DatabaseAutoRecovery databaseAutoRecovery;

		// Token: 0x0400046B RID: 1131
		private long currentVersion;

		// Token: 0x0400046C RID: 1132
		private long currentRevision;

		// Token: 0x0400046D RID: 1133
		private bool isAttached;
	}
}
