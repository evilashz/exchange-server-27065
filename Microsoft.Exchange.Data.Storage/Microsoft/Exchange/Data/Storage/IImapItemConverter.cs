using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005DC RID: 1500
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class IImapItemConverter : DisposableObject
	{
		// Token: 0x06003DB6 RID: 15798
		public abstract MimePartInfo GetMimeStructure();

		// Token: 0x06003DB7 RID: 15799
		public abstract void GetBody(Stream outStream);

		// Token: 0x06003DB8 RID: 15800
		public abstract bool GetBody(Stream outStream, uint[] indices);

		// Token: 0x06003DB9 RID: 15801
		public abstract void GetText(Stream outStream);

		// Token: 0x06003DBA RID: 15802
		public abstract bool GetText(Stream outStream, uint[] indices);

		// Token: 0x06003DBB RID: 15803
		public abstract MimePartHeaders GetHeaders();

		// Token: 0x06003DBC RID: 15804
		public abstract MimePartHeaders GetHeaders(uint[] indices);

		// Token: 0x06003DBD RID: 15805
		public abstract MimePartHeaders GetMime(uint[] indices);

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06003DBE RID: 15806
		public abstract bool ItemNeedsSave { get; }
	}
}
