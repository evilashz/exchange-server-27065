using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000101 RID: 257
	internal class TemporaryFile : DisposeTrackableBase
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x0002F6E0 File Offset: 0x0002D8E0
		public TemporaryFile(string fileFullPath)
		{
			this.m_fileFullPath = fileFullPath;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002F6EF File Offset: 0x0002D8EF
		public static implicit operator string(TemporaryFile tempFile)
		{
			return tempFile.m_fileFullPath;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002F6F7 File Offset: 0x0002D8F7
		public static implicit operator TemporaryFile(string fileFullPath)
		{
			return new TemporaryFile(fileFullPath);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002F6FF File Offset: 0x0002D8FF
		public override string ToString()
		{
			return this.m_fileFullPath;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002F707 File Offset: 0x0002D907
		public override int GetHashCode()
		{
			return this.m_fileFullPath.GetHashCode();
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002F714 File Offset: 0x0002D914
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				FileCleanup.TryDelete(this.m_fileFullPath);
			}
			this.m_fileFullPath = null;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0002F72B File Offset: 0x0002D92B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TemporaryFile>(this);
		}

		// Token: 0x04000451 RID: 1105
		private string m_fileFullPath;
	}
}
