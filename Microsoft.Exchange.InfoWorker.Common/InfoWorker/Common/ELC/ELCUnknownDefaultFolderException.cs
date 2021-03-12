using System;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x02000191 RID: 401
	internal class ELCUnknownDefaultFolderException : IWPermanentException
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002DF34 File Offset: 0x0002C134
		public ELCUnknownDefaultFolderException(string folderName, string mailbox) : base(Strings.descUnknownDefFolder(folderName, mailbox))
		{
		}
	}
}
