using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200018F RID: 399
	internal class VoiceMailSearchFolder : DefaultUMSearchFolder
	{
		// Token: 0x06000D3B RID: 3387 RVA: 0x000315AE File Offset: 0x0002F7AE
		internal VoiceMailSearchFolder(MailboxSession itemStore) : base(itemStore)
		{
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x000315B7 File Offset: 0x0002F7B7
		protected override DefaultFolderType DefaultFolderType
		{
			get
			{
				return DefaultFolderType.UMVoicemail;
			}
		}
	}
}
