using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000033 RID: 51
	internal class InvalidUserOofSettingsXmlDocument : LocalizedException
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00006B12 File Offset: 0x00004D12
		public InvalidUserOofSettingsXmlDocument() : base(Strings.descCorruptUserOofSettingsXmlDocument)
		{
		}
	}
}
