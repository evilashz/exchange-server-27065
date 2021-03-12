using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x02000195 RID: 405
	internal class ELCOrgFolderExistsException : LocalizedException
	{
		// Token: 0x06000ADE RID: 2782 RVA: 0x0002DF76 File Offset: 0x0002C176
		public ELCOrgFolderExistsException(string folderName) : base(Strings.descElcFolderExists(folderName))
		{
		}
	}
}
