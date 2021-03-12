using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000021 RID: 33
	[DataContract]
	internal sealed class DataExportBatch
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00004454 File Offset: 0x00002654
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000445C File Offset: 0x0000265C
		[DataMember(EmitDefaultValue = false)]
		public int Opcode
		{
			get
			{
				return this.opcode;
			}
			set
			{
				this.opcode = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00004465 File Offset: 0x00002665
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000446D File Offset: 0x0000266D
		[DataMember(EmitDefaultValue = false)]
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00004476 File Offset: 0x00002676
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000447E File Offset: 0x0000267E
		[DataMember(EmitDefaultValue = false)]
		public bool FlushAfterImport
		{
			get
			{
				return this.flushAfterImport;
			}
			set
			{
				this.flushAfterImport = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00004487 File Offset: 0x00002687
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000448F File Offset: 0x0000268F
		[DataMember(EmitDefaultValue = false)]
		public bool IsLastBatch
		{
			get
			{
				return this.isLastBatch;
			}
			set
			{
				this.isLastBatch = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00004498 File Offset: 0x00002698
		// (set) Token: 0x0600025E RID: 606 RVA: 0x000044A0 File Offset: 0x000026A0
		[DataMember(EmitDefaultValue = false)]
		public long DataExportHandle
		{
			get
			{
				return this.dataExportHandle;
			}
			set
			{
				this.dataExportHandle = value;
			}
		}

		// Token: 0x04000108 RID: 264
		private int opcode;

		// Token: 0x04000109 RID: 265
		private byte[] data;

		// Token: 0x0400010A RID: 266
		private bool flushAfterImport;

		// Token: 0x0400010B RID: 267
		private bool isLastBatch;

		// Token: 0x0400010C RID: 268
		private long dataExportHandle;
	}
}
