using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000175 RID: 373
	internal class AirSyncAttachmentInfo : IEquatable<AirSyncAttachmentInfo>
	{
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0005C7AE File Offset: 0x0005A9AE
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x0005C7B6 File Offset: 0x0005A9B6
		public AttachmentId AttachmentId { get; set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0005C7BF File Offset: 0x0005A9BF
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x0005C7C7 File Offset: 0x0005A9C7
		public bool IsInline { get; set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0005C7D0 File Offset: 0x0005A9D0
		// (set) Token: 0x06001061 RID: 4193 RVA: 0x0005C7D8 File Offset: 0x0005A9D8
		public string ContentId { get; set; }

		// Token: 0x06001062 RID: 4194 RVA: 0x0005C7E4 File Offset: 0x0005A9E4
		public override bool Equals(object obj)
		{
			AirSyncAttachmentInfo other = obj as AirSyncAttachmentInfo;
			return this.Equals(other);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0005C800 File Offset: 0x0005AA00
		public bool Equals(AirSyncAttachmentInfo other)
		{
			return this.AttachmentId != null && (this.AttachmentId.Equals(other.AttachmentId) && this.IsInline == other.IsInline) && string.Compare(this.ContentId, other.ContentId, StringComparison.Ordinal) == 0;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0005C850 File Offset: 0x0005AA50
		public override int GetHashCode()
		{
			return ((this.AttachmentId == null) ? 0 : this.AttachmentId.GetHashCode()) ^ this.IsInline.GetHashCode() ^ this.ContentId.GetHashCode();
		}
	}
}
