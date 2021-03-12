using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x02000698 RID: 1688
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ClassifyResult
	{
		// Token: 0x06004506 RID: 17670 RVA: 0x00125C2C File Offset: 0x00123E2C
		internal ClassifyResult(Uri originalUri, ClassificationError error)
		{
			this.originalUri = originalUri;
			this.error = error;
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x00125C4A File Offset: 0x00123E4A
		internal ClassifyResult(DocumentLibraryObjectId objectId, Uri originalUri, UriFlags uriFlags)
		{
			this.objectId = objectId;
			this.originalUri = originalUri;
			this.uriFlags = uriFlags;
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x00125C70 File Offset: 0x00123E70
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			if (this.error != ClassificationError.None)
			{
				stringBuilder.AppendFormat("UriFlags:{0},\tOriginalUri:{1},\tObjectId:{2}", this.uriFlags, this.originalUri, this.objectId);
			}
			else
			{
				stringBuilder.AppendFormat("OriginalUri:{0}, Error:{1}", this.originalUri, this.error);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x00125CD5 File Offset: 0x00123ED5
		public DocumentLibraryObjectId ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x0600450A RID: 17674 RVA: 0x00125CDD File Offset: 0x00123EDD
		public Uri OriginalUri
		{
			get
			{
				return this.originalUri;
			}
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x0600450B RID: 17675 RVA: 0x00125CE5 File Offset: 0x00123EE5
		public ClassificationError Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x0600450C RID: 17676 RVA: 0x00125CED File Offset: 0x00123EED
		public UriFlags UriFlags
		{
			get
			{
				return this.uriFlags;
			}
		}

		// Token: 0x04002587 RID: 9607
		private Uri originalUri;

		// Token: 0x04002588 RID: 9608
		private ClassificationError error;

		// Token: 0x04002589 RID: 9609
		private UriFlags uriFlags = UriFlags.Other;

		// Token: 0x0400258A RID: 9610
		private DocumentLibraryObjectId objectId;
	}
}
