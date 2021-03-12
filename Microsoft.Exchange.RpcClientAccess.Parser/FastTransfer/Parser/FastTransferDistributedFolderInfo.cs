using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200015E RID: 350
	internal sealed class FastTransferDistributedFolderInfo : IEquatable<FastTransferDistributedFolderInfo>
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x00012EC3 File Offset: 0x000110C3
		public FastTransferDistributedFolderInfo(uint flags, uint depth, StoreLongTermId folderLongTermId, uint localSiteDatabaseCount, string[] databases)
		{
			Util.ThrowOnNullArgument(databases, "databases");
			this.flags = flags;
			this.depth = depth;
			this.folderLongTermId = folderLongTermId;
			this.localSiteDatabaseCount = localSiteDatabaseCount;
			this.databases = databases;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00012EFC File Offset: 0x000110FC
		public uint Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00012F04 File Offset: 0x00011104
		public uint Depth
		{
			get
			{
				return this.depth;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00012F0C File Offset: 0x0001110C
		public StoreLongTermId FolderLongTermId
		{
			get
			{
				return this.folderLongTermId;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00012F14 File Offset: 0x00011114
		public uint LocalSiteDatabaseCount
		{
			get
			{
				return this.localSiteDatabaseCount;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00012F1C File Offset: 0x0001111C
		public string[] Databases
		{
			get
			{
				return this.databases ?? Array<string>.Empty;
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00012F2D File Offset: 0x0001112D
		public override bool Equals(object obj)
		{
			return obj is FastTransferDistributedFolderInfo && this.Equals((FastTransferDistributedFolderInfo)obj);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00012F48 File Offset: 0x00011148
		public override int GetHashCode()
		{
			return this.folderLongTermId.GetHashCode() ^ this.localSiteDatabaseCount.GetHashCode() ^ ArrayComparer<string>.Comparer.GetHashCode(this.Databases);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00012F8C File Offset: 0x0001118C
		public override string ToString()
		{
			return string.Format("FolderLongTermId: {0}, DatabaseCount: {1}, LocalSiteDatabaseCount: {2}, ReplicaDatabases: {{{3}}}", new object[]
			{
				this.FolderLongTermId.ToString(),
				this.Databases.Length,
				this.localSiteDatabaseCount,
				string.Join(",", this.Databases)
			});
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00012FF4 File Offset: 0x000111F4
		public bool Equals(FastTransferDistributedFolderInfo other)
		{
			return this.folderLongTermId.Equals(other.FolderLongTermId) && this.LocalSiteDatabaseCount == other.LocalSiteDatabaseCount && ArrayComparer<string>.Comparer.Equals(this.Databases, other.Databases);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00013040 File Offset: 0x00011240
		internal static FastTransferDistributedFolderInfo Parse(Reader reader)
		{
			uint num = reader.ReadUInt32();
			uint num2 = reader.ReadUInt32();
			StoreLongTermId storeLongTermId = StoreLongTermId.Parse(reader);
			uint num3 = reader.ReadUInt32();
			uint num4 = reader.ReadUInt32();
			string[] array = new string[num3];
			uint num5 = 0U;
			while ((ulong)num5 < (ulong)((long)array.Length))
			{
				array[(int)((UIntPtr)num5)] = reader.ReadAsciiString(StringFlags.IncludeNull);
				num5 += 1U;
			}
			return new FastTransferDistributedFolderInfo(num, num2, storeLongTermId, num4, array);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000130A8 File Offset: 0x000112A8
		internal void Serialize(Writer writer)
		{
			writer.WriteUInt32(this.flags);
			writer.WriteUInt32(this.depth);
			this.folderLongTermId.Serialize(writer);
			writer.WriteUInt32((uint)this.Databases.Length);
			writer.WriteUInt32(this.localSiteDatabaseCount);
			foreach (string value in this.Databases)
			{
				writer.WriteAsciiString(value, StringFlags.IncludeNull);
			}
		}

		// Token: 0x0400034C RID: 844
		private readonly uint flags;

		// Token: 0x0400034D RID: 845
		private readonly uint depth;

		// Token: 0x0400034E RID: 846
		private readonly StoreLongTermId folderLongTermId;

		// Token: 0x0400034F RID: 847
		private readonly uint localSiteDatabaseCount;

		// Token: 0x04000350 RID: 848
		private readonly string[] databases;
	}
}
