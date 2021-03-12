using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200017B RID: 379
	internal class AdObjectIdAsNameCoverter : TextConverter
	{
		// Token: 0x06000F2C RID: 3884 RVA: 0x0003AE24 File Offset: 0x00039024
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			return ((ADObjectId)arg).Name;
		}
	}
}
