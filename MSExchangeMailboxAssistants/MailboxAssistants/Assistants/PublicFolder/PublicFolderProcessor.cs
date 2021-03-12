using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000174 RID: 372
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PublicFolderProcessor
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x0005885C File Offset: 0x00056A5C
		public PublicFolderProcessor(IPublicFolderSession publicFolderSession, ITracer tracer)
		{
			if (publicFolderSession == null)
			{
				throw new ArgumentNullException("publicFolderSession");
			}
			if (tracer == null)
			{
				throw new ArgumentNullException("tracer");
			}
			this.publicFolderSession = publicFolderSession;
			this.now = ExDateTime.UtcNow;
			this.tracer = tracer;
		}

		// Token: 0x06000EEC RID: 3820
		public abstract void Invoke();

		// Token: 0x0400095E RID: 2398
		protected readonly ITracer tracer;

		// Token: 0x0400095F RID: 2399
		protected readonly IPublicFolderSession publicFolderSession;

		// Token: 0x04000960 RID: 2400
		protected ExDateTime now;
	}
}
