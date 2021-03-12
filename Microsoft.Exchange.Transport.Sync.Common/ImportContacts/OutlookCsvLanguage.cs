using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.ImportContacts
{
	// Token: 0x02000076 RID: 118
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OutlookCsvLanguage
	{
		// Token: 0x060002DE RID: 734 RVA: 0x000096B1 File Offset: 0x000078B1
		public OutlookCsvLanguage(int codePage, Dictionary<string, ImportContactProperties> columnsMapping, CultureInfo culture)
		{
			SyncUtilities.ThrowIfArgumentNull("columnsMapping", columnsMapping);
			SyncUtilities.ThrowIfArgumentNull("culture", culture);
			this.codePage = codePage;
			this.columnsMapping = columnsMapping;
			this.culture = culture;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000096E4 File Offset: 0x000078E4
		public int CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x000096EC File Offset: 0x000078EC
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x000096F4 File Offset: 0x000078F4
		public Dictionary<string, ImportContactProperties> ColumnsMapping
		{
			get
			{
				return this.columnsMapping;
			}
		}

		// Token: 0x04000196 RID: 406
		private int codePage;

		// Token: 0x04000197 RID: 407
		private CultureInfo culture;

		// Token: 0x04000198 RID: 408
		private Dictionary<string, ImportContactProperties> columnsMapping;
	}
}
