using System;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000109 RID: 265
	internal class XExch50Blob : LazyBytes
	{
		// Token: 0x06000B48 RID: 2888 RVA: 0x00027B53 File Offset: 0x00025D53
		public XExch50Blob(DataRow row, DataColumn column) : base(row, column)
		{
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00027B5D File Offset: 0x00025D5D
		public XExch50Blob(DataRow row, BlobCollection blobCollection, byte blobCollectionKey) : base(row, blobCollection, blobCollectionKey)
		{
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00027B68 File Offset: 0x00025D68
		public XExch50Blob()
		{
		}
	}
}
