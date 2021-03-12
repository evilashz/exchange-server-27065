using System;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;

namespace Microsoft.Exchange.EdgeSync.Common
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	public class Status
	{
		// Token: 0x0600010F RID: 271 RVA: 0x0000615F File Offset: 0x0000435F
		public Status(string name, SyncTreeType type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000618B File Offset: 0x0000438B
		public Status()
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000061AC File Offset: 0x000043AC
		public static Status Deserialize(byte[] bytes)
		{
			Status result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				result = (Status)Status.serializer.Deserialize(memoryStream);
			}
			return result;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000061F0 File Offset: 0x000043F0
		public void Starting()
		{
			this.Result = StatusResult.InProgress;
			this.Scanned = 0;
			this.Added = 0;
			this.Deleted = 0;
			this.Updated = 0;
			this.TargetScanned = 0;
			this.FailureDetails = string.Empty;
			this.StartUTC = DateTime.UtcNow;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000623D File Offset: 0x0000443D
		public void ObjectAdded()
		{
			this.Added++;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000624D File Offset: 0x0000444D
		public void ObjectDeleted()
		{
			this.Deleted++;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000625D File Offset: 0x0000445D
		public void ObjectUpdated()
		{
			this.Updated++;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000626D File Offset: 0x0000446D
		public void ObjectScanned()
		{
			this.Scanned++;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000627D File Offset: 0x0000447D
		public void TargetObjectScanned()
		{
			this.TargetScanned++;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000628D File Offset: 0x0000448D
		public void Finished(StatusResult result)
		{
			if (this.Result == StatusResult.InProgress)
			{
				this.Result = result;
				this.EndUTC = DateTime.UtcNow;
				this.Dump();
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000062B0 File Offset: 0x000044B0
		public byte[] Serialize()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Status.serializer.Serialize(memoryStream, this);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000062F4 File Offset: 0x000044F4
		public void Dump()
		{
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug((long)this.GetHashCode(), "Sync Status name: {0} type: {1} result: {2} start: {3} end: {4} scanned: {5} added: {6} deleted: {7} updated: {8} targetScanned: {9} failureDetails: {10}", new object[]
			{
				this.Name,
				this.Type,
				this.Result,
				this.StartUTC,
				this.EndUTC,
				this.Scanned,
				this.Added,
				this.Deleted,
				this.Updated,
				this.TargetScanned,
				this.FailureDetails
			});
		}

		// Token: 0x040000D5 RID: 213
		private static StatusSerializer serializer = new StatusSerializer();

		// Token: 0x040000D6 RID: 214
		public StatusResult Result;

		// Token: 0x040000D7 RID: 215
		public SyncTreeType Type;

		// Token: 0x040000D8 RID: 216
		public string Name = string.Empty;

		// Token: 0x040000D9 RID: 217
		public string FailureDetails = string.Empty;

		// Token: 0x040000DA RID: 218
		public DateTime StartUTC;

		// Token: 0x040000DB RID: 219
		public DateTime EndUTC;

		// Token: 0x040000DC RID: 220
		public int Added;

		// Token: 0x040000DD RID: 221
		public int Deleted;

		// Token: 0x040000DE RID: 222
		public int Updated;

		// Token: 0x040000DF RID: 223
		public int Scanned;

		// Token: 0x040000E0 RID: 224
		public int TargetScanned;
	}
}
