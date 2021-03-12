using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006B7 RID: 1719
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ExistsQuery : SinglePropertyQuery
	{
		// Token: 0x0600455B RID: 17755 RVA: 0x0012733F File Offset: 0x0012553F
		internal ExistsQuery(int index) : base(index)
		{
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x00127348 File Offset: 0x00125548
		public override bool IsMatch(object[] row)
		{
			PropertyError propertyError = row[this.Index] as PropertyError;
			return propertyError == null || (propertyError.PropertyErrorCode != PropertyErrorCode.NotFound && propertyError.PropertyErrorCode != PropertyErrorCode.NotSupported);
		}
	}
}
