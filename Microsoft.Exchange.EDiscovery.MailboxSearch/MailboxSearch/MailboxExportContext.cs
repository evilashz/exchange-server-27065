using System;
using System.Collections.Generic;
using Microsoft.Exchange.EDiscovery.Export;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000005 RID: 5
	internal class MailboxExportContext : IExportContext
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002428 File Offset: 0x00000628
		public MailboxExportContext(IExportMetadata exportMetadata, IList<ISource> sourceMailboxes, ITargetLocation targetLocation, bool isResume, Action<IEnumerable<ErrorRecord>> onWriteErrorRecord, Action<IEnumerable<ExportRecord>> onWriteResultManifest)
		{
			this.ExportMetadata = exportMetadata;
			this.Sources = sourceMailboxes;
			this.TargetLocation = targetLocation;
			this.IsResume = isResume;
			this.OnWriteErrorRecord = onWriteErrorRecord;
			this.OnWriteResultManifest = onWriteResultManifest;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000245D File Offset: 0x0000065D
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002465 File Offset: 0x00000665
		public Action<IEnumerable<ErrorRecord>> OnWriteErrorRecord { get; internal set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000246E File Offset: 0x0000066E
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002476 File Offset: 0x00000676
		public Action<IEnumerable<ExportRecord>> OnWriteResultManifest { get; internal set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000247F File Offset: 0x0000067F
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002487 File Offset: 0x00000687
		public bool IsResume { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002490 File Offset: 0x00000690
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002498 File Offset: 0x00000698
		public IExportMetadata ExportMetadata { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024A1 File Offset: 0x000006A1
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000024A9 File Offset: 0x000006A9
		public IList<ISource> Sources { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024B2 File Offset: 0x000006B2
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000024BA File Offset: 0x000006BA
		public ITargetLocation TargetLocation { get; private set; }

		// Token: 0x0600001B RID: 27 RVA: 0x000024C3 File Offset: 0x000006C3
		public void WriteErrorLog(IEnumerable<ErrorRecord> errorRecords)
		{
			if (this.OnWriteErrorRecord != null)
			{
				this.OnWriteErrorRecord(errorRecords);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024D9 File Offset: 0x000006D9
		public void WriteResultManifest(IEnumerable<ExportRecord> records)
		{
			if (this.OnWriteResultManifest != null)
			{
				this.OnWriteResultManifest(records);
			}
		}
	}
}
