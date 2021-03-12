using System;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x02000193 RID: 403
	internal class ELCOrgFolderCreationException : IWTransientException
	{
		// Token: 0x06000ADC RID: 2780 RVA: 0x0002DF56 File Offset: 0x0002C156
		public ELCOrgFolderCreationException(string mailbox, string folderName, Exception innerException) : base(Strings.descFailedToCreateOrganizationalFolder(folderName, mailbox), innerException)
		{
		}
	}
}
