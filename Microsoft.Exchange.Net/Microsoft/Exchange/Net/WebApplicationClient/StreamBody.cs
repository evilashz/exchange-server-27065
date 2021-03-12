using System;
using System.IO;
using System.Net.Mime;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B29 RID: 2857
	internal class StreamBody : RequestBody
	{
		// Token: 0x06003DA6 RID: 15782 RVA: 0x000A0923 File Offset: 0x0009EB23
		public StreamBody(Stream stream, string contentType)
		{
			this.Stream = stream;
			base.ContentType = new ContentType(contentType);
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x000A093E File Offset: 0x0009EB3E
		// (set) Token: 0x06003DA8 RID: 15784 RVA: 0x000A0946 File Offset: 0x0009EB46
		public Stream Stream { get; private set; }

		// Token: 0x06003DA9 RID: 15785 RVA: 0x000A094F File Offset: 0x0009EB4F
		public override void Write(Stream writeStream)
		{
			if (this.Stream != null)
			{
				this.Stream.CopyTo(writeStream);
			}
		}
	}
}
