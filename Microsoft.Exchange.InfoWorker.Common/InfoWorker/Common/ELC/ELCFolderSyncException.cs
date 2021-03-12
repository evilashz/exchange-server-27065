using System;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x02000194 RID: 404
	internal class ELCFolderSyncException : IWTransientException
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x0002DF66 File Offset: 0x0002C166
		public ELCFolderSyncException(string mailbox, string folderName, Exception innerException) : base(Strings.descFailedToSyncFolder(folderName, mailbox), innerException)
		{
		}
	}
}
