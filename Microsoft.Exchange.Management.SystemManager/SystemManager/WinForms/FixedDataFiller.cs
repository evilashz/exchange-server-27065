using System;
using System.Data;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000D1 RID: 209
	public class FixedDataFiller : AbstractDataTableFiller
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x00018D34 File Offset: 0x00016F34
		protected override void OnFill(DataTable table)
		{
			table.Merge(this.DataTable);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00018D44 File Offset: 0x00016F44
		public override object Clone()
		{
			return new FixedDataFiller
			{
				DataTable = this.DataTable
			};
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00018D64 File Offset: 0x00016F64
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x00018D6C File Offset: 0x00016F6C
		public DataTable DataTable { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00018D75 File Offset: 0x00016F75
		public override ICommandBuilder CommandBuilder
		{
			get
			{
				return NullableCommandBuilder.Value;
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00018D7C File Offset: 0x00016F7C
		public override void BuildCommand(string searchText, object[] pipeline, DataRow row)
		{
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00018D7E File Offset: 0x00016F7E
		public override void BuildCommandWithScope(string searchText, object[] pipeline, DataRow row, object scope)
		{
		}
	}
}
