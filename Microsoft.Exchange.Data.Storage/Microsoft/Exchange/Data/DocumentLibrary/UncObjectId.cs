using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006D8 RID: 1752
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class UncObjectId : DocumentLibraryObjectId
	{
		// Token: 0x060045CF RID: 17871 RVA: 0x00128EDD File Offset: 0x001270DD
		internal UncObjectId(Uri path, UriFlags uriFlags) : base(uriFlags)
		{
			if (!path.IsUnc)
			{
				throw new ArgumentException("path");
			}
			this.path = path;
		}

		// Token: 0x060045D0 RID: 17872 RVA: 0x00128F00 File Offset: 0x00127100
		public override bool Equals(object obj)
		{
			UncObjectId uncObjectId = obj as UncObjectId;
			return uncObjectId != null && uncObjectId.path == this.path;
		}

		// Token: 0x060045D1 RID: 17873 RVA: 0x00128F2A File Offset: 0x0012712A
		public override int GetHashCode()
		{
			return this.path.GetHashCode();
		}

		// Token: 0x060045D2 RID: 17874 RVA: 0x00128F38 File Offset: 0x00127138
		public override string ToString()
		{
			return new UriBuilder(this.path)
			{
				Query = UncObjectId.QueryPart + base.UriFlags
			}.ToString();
		}

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x060045D3 RID: 17875 RVA: 0x00128F72 File Offset: 0x00127172
		public Uri Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x04002632 RID: 9778
		private readonly Uri path;

		// Token: 0x04002633 RID: 9779
		internal static readonly string QueryPart = "UriFlags=";
	}
}
