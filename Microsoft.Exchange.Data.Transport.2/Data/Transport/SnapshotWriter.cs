using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000074 RID: 116
	internal abstract class SnapshotWriter
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00006932 File Offset: 0x00004B32
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00006939 File Offset: 0x00004B39
		internal static string SnapshotBaseFolder
		{
			get
			{
				return SnapshotWriter.snapshotBaseFolder;
			}
			set
			{
				SnapshotWriter.snapshotBaseFolder = value;
			}
		}

		// Token: 0x0600027F RID: 639
		public abstract void WriteOriginalData(int agentId, string id, string topic, string address, MailItem mailItem);

		// Token: 0x06000280 RID: 640
		public abstract void WritePreProcessedData(int agentId, string prefix, string id, string topic, MailItem mailItem);

		// Token: 0x06000281 RID: 641
		public abstract void WriteProcessedData(string prefix, string id, string topic, string agent, MailItem mailItem);

		// Token: 0x040001CC RID: 460
		private static string snapshotBaseFolder;
	}
}
