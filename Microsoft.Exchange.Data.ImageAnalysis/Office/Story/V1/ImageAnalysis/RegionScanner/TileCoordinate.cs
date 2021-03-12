using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x02000056 RID: 86
	[DataContract]
	[Serializable]
	internal struct TileCoordinate
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000079AE File Offset: 0x00005BAE
		// (set) Token: 0x06000294 RID: 660 RVA: 0x000079B6 File Offset: 0x00005BB6
		[DataMember]
		public ushort Index { get; private set; }

		// Token: 0x06000295 RID: 661 RVA: 0x000079BF File Offset: 0x00005BBF
		public TileCoordinate(int index)
		{
			this = default(TileCoordinate);
			this.Index = checked((ushort)index);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000079D0 File Offset: 0x00005BD0
		public override bool Equals(object obj)
		{
			return obj is TileCoordinate && this == (TileCoordinate)obj;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000079F0 File Offset: 0x00005BF0
		public override int GetHashCode()
		{
			return this.Index.GetHashCode();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00007A0B File Offset: 0x00005C0B
		public static bool operator ==(TileCoordinate one, TileCoordinate another)
		{
			return one.Index == another.Index;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00007A1D File Offset: 0x00005C1D
		public static bool operator !=(TileCoordinate one, TileCoordinate another)
		{
			return one.Index != another.Index;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007A34 File Offset: 0x00005C34
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "@{0}", new object[]
			{
				this.Index
			});
		}
	}
}
