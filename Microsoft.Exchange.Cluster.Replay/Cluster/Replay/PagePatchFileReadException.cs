using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200049C RID: 1180
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PagePatchFileReadException : PagePatchApiFailedException
	{
		// Token: 0x06002CC1 RID: 11457 RVA: 0x000BFFA1 File Offset: 0x000BE1A1
		public PagePatchFileReadException(string fileName, long actualBytesRead, long expectedBytesRead) : base(ReplayStrings.PagePatchFileReadException(fileName, actualBytesRead, expectedBytesRead))
		{
			this.fileName = fileName;
			this.actualBytesRead = actualBytesRead;
			this.expectedBytesRead = expectedBytesRead;
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000BFFCB File Offset: 0x000BE1CB
		public PagePatchFileReadException(string fileName, long actualBytesRead, long expectedBytesRead, Exception innerException) : base(ReplayStrings.PagePatchFileReadException(fileName, actualBytesRead, expectedBytesRead), innerException)
		{
			this.fileName = fileName;
			this.actualBytesRead = actualBytesRead;
			this.expectedBytesRead = expectedBytesRead;
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000BFFF8 File Offset: 0x000BE1F8
		protected PagePatchFileReadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = (string)info.GetValue("fileName", typeof(string));
			this.actualBytesRead = (long)info.GetValue("actualBytesRead", typeof(long));
			this.expectedBytesRead = (long)info.GetValue("expectedBytesRead", typeof(long));
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000C006D File Offset: 0x000BE26D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileName", this.fileName);
			info.AddValue("actualBytesRead", this.actualBytesRead);
			info.AddValue("expectedBytesRead", this.expectedBytesRead);
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x000C00AA File Offset: 0x000BE2AA
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000C00B2 File Offset: 0x000BE2B2
		public long ActualBytesRead
		{
			get
			{
				return this.actualBytesRead;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x000C00BA File Offset: 0x000BE2BA
		public long ExpectedBytesRead
		{
			get
			{
				return this.expectedBytesRead;
			}
		}

		// Token: 0x040014F8 RID: 5368
		private readonly string fileName;

		// Token: 0x040014F9 RID: 5369
		private readonly long actualBytesRead;

		// Token: 0x040014FA RID: 5370
		private readonly long expectedBytesRead;
	}
}
