using System;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x02000196 RID: 406
	internal class ELCNoMatchingOrgFoldersException : IWPermanentException
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x0002DF84 File Offset: 0x0002C184
		public ELCNoMatchingOrgFoldersException(string folderName) : base(Strings.descElcNoMatchingOrgFolder(folderName))
		{
		}
	}
}
