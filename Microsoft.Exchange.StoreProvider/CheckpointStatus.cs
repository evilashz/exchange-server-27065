using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CheckpointStatus
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002B19 File Offset: 0x00000D19
		internal CheckpointStatus(ref CHECKPOINTSTATUSRAW pCheckpointStatus)
		{
			this.guidMdb = pCheckpointStatus.guidMdb;
			this.ulBeginCheckpointDepth = pCheckpointStatus.ulBeginCheckpointDepth;
			this.ulEndCheckpointDepth = pCheckpointStatus.ulEndCheckpointDepth;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B45 File Offset: 0x00000D45
		internal CheckpointStatus(Guid _guidMdb, uint _ulBeginCheckpointDepth, uint _ulEndCheckpointDepth)
		{
			this.guidMdb = _guidMdb;
			this.ulBeginCheckpointDepth = _ulBeginCheckpointDepth;
			this.ulEndCheckpointDepth = _ulEndCheckpointDepth;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002B62 File Offset: 0x00000D62
		public Guid MdbGuid
		{
			get
			{
				return this.guidMdb;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002B6A File Offset: 0x00000D6A
		public uint BeginCheckpointDepth
		{
			get
			{
				return this.ulBeginCheckpointDepth;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002B72 File Offset: 0x00000D72
		public uint EndCheckpointDepth
		{
			get
			{
				return this.ulEndCheckpointDepth;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B7C File Offset: 0x00000D7C
		public override string ToString()
		{
			return string.Format("guidMdb {0} : ", this.guidMdb) + string.Format("ulBeginCheckpointDepth {0}, ", this.ulBeginCheckpointDepth) + string.Format("ulEndCheckpointDepth {0} ", this.ulEndCheckpointDepth);
		}

		// Token: 0x04000024 RID: 36
		private Guid guidMdb;

		// Token: 0x04000025 RID: 37
		private uint ulBeginCheckpointDepth;

		// Token: 0x04000026 RID: 38
		private uint ulEndCheckpointDepth;
	}
}
