using System;
using System.IO;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000020 RID: 32
	internal class StreamAttachmentWrapper : BufferPoolAttachmentWrapper
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public StreamAttachmentWrapper(IRetrieverStreamAttachment streamAttachment)
		{
			Util.ThrowOnNullArgument(streamAttachment, "streamAttachment");
			using (Stream stream = streamAttachment.TryGetContentStream())
			{
				base.InitAttachmentStream(stream);
			}
		}
	}
}
