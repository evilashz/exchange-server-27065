using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000D2 RID: 210
	internal class PreFillADObjectIdFiller : FixedDataFiller
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x00018D88 File Offset: 0x00016F88
		public PreFillADObjectIdFiller(DataTable table)
		{
			base.DataTable = table;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00018D97 File Offset: 0x00016F97
		public override object Clone()
		{
			return new PreFillADObjectIdFiller(base.DataTable);
		}
	}
}
