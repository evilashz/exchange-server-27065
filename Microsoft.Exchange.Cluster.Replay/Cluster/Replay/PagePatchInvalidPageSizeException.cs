using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200049B RID: 1179
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PagePatchInvalidPageSizeException : PagePatchApiFailedException
	{
		// Token: 0x06002CBB RID: 11451 RVA: 0x000BFEC9 File Offset: 0x000BE0C9
		public PagePatchInvalidPageSizeException(long dataSize, long expectedPageSize) : base(ReplayStrings.PagePatchInvalidPageSizeException(dataSize, expectedPageSize))
		{
			this.dataSize = dataSize;
			this.expectedPageSize = expectedPageSize;
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000BFEEB File Offset: 0x000BE0EB
		public PagePatchInvalidPageSizeException(long dataSize, long expectedPageSize, Exception innerException) : base(ReplayStrings.PagePatchInvalidPageSizeException(dataSize, expectedPageSize), innerException)
		{
			this.dataSize = dataSize;
			this.expectedPageSize = expectedPageSize;
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000BFF10 File Offset: 0x000BE110
		protected PagePatchInvalidPageSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dataSize = (long)info.GetValue("dataSize", typeof(long));
			this.expectedPageSize = (long)info.GetValue("expectedPageSize", typeof(long));
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x000BFF65 File Offset: 0x000BE165
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dataSize", this.dataSize);
			info.AddValue("expectedPageSize", this.expectedPageSize);
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000BFF91 File Offset: 0x000BE191
		public long DataSize
		{
			get
			{
				return this.dataSize;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000BFF99 File Offset: 0x000BE199
		public long ExpectedPageSize
		{
			get
			{
				return this.expectedPageSize;
			}
		}

		// Token: 0x040014F6 RID: 5366
		private readonly long dataSize;

		// Token: 0x040014F7 RID: 5367
		private readonly long expectedPageSize;
	}
}
