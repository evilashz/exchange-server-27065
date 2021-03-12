using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000D6 RID: 214
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DownloadLimitExceededException : LocalizedException
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x00014094 File Offset: 0x00012294
		public DownloadLimitExceededException(string size) : base(HttpStrings.DownloadLimitExceededException(size))
		{
			this.size = size;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000140A9 File Offset: 0x000122A9
		public DownloadLimitExceededException(string size, Exception innerException) : base(HttpStrings.DownloadLimitExceededException(size), innerException)
		{
			this.size = size;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000140BF File Offset: 0x000122BF
		protected DownloadLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.size = (string)info.GetValue("size", typeof(string));
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000140E9 File Offset: 0x000122E9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("size", this.size);
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00014104 File Offset: 0x00012304
		public string Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x0400046F RID: 1135
		private readonly string size;
	}
}
