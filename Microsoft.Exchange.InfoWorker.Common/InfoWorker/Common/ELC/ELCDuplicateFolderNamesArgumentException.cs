using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x02000199 RID: 409
	internal class ELCDuplicateFolderNamesArgumentException : LocalizedException
	{
		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002DFAB File Offset: 0x0002C1AB
		public ELCDuplicateFolderNamesArgumentException(string folderName) : base(Strings.descInputFolderNamesContainDuplicates(folderName))
		{
		}
	}
}
