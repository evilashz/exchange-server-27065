using System;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x02000197 RID: 407
	internal class ELCDefaultFolderNotFoundException : IWTransientException
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002DF92 File Offset: 0x0002C192
		public ELCDefaultFolderNotFoundException(string folderName, string mailboxName, Exception innerException) : base(Strings.descElcCannotFindDefaultFolder(folderName, mailboxName), innerException)
		{
		}
	}
}
