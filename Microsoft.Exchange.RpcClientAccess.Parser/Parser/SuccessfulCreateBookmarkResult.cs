using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000231 RID: 561
	internal sealed class SuccessfulCreateBookmarkResult : RopResult
	{
		// Token: 0x06000C41 RID: 3137 RVA: 0x00026EB8 File Offset: 0x000250B8
		internal SuccessfulCreateBookmarkResult(byte[] bookmark) : base(RopId.CreateBookmark, ErrorCode.None, null)
		{
			if (bookmark == null)
			{
				throw new ArgumentNullException("bookmark");
			}
			this.bookmark = bookmark;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00026ED9 File Offset: 0x000250D9
		internal SuccessfulCreateBookmarkResult(Reader reader) : base(reader)
		{
			this.bookmark = reader.ReadSizeAndByteArray();
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00026EEE File Offset: 0x000250EE
		internal byte[] Bookmark
		{
			get
			{
				return this.bookmark;
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00026EF6 File Offset: 0x000250F6
		internal static SuccessfulCreateBookmarkResult Parse(Reader reader)
		{
			return new SuccessfulCreateBookmarkResult(reader);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00026EFE File Offset: 0x000250FE
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteSizedBytes(this.bookmark);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00026F13 File Offset: 0x00025113
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Bookmark=[");
			Util.AppendToString(stringBuilder, this.bookmark);
			stringBuilder.Append("]");
		}

		// Token: 0x040006C5 RID: 1733
		private readonly byte[] bookmark;
	}
}
