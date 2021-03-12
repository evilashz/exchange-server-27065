using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200026B RID: 619
	internal class OtherUPNSuffixFiller : AbstractDataTableFiller
	{
		// Token: 0x06001A92 RID: 6802 RVA: 0x0007558C File Offset: 0x0007378C
		private static string GetColumnValue(DataRow row, string column)
		{
			string result = null;
			if (row != null && row.Table.Columns.Contains(column) && !DBNull.Value.Equals(row[column]))
			{
				result = (string)row[column];
			}
			return result;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000755D2 File Offset: 0x000737D2
		public OtherUPNSuffixFiller(string inputSuffixColumn, string fillColumn)
		{
			this.inputSuffixColumn = inputSuffixColumn;
			this.fillColumn = fillColumn;
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x000755E8 File Offset: 0x000737E8
		public override ICommandBuilder CommandBuilder
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000755EB File Offset: 0x000737EB
		public override void BuildCommand(string searchText, object[] pipeline, DataRow row)
		{
			this.otherUPNSuffix = OtherUPNSuffixFiller.GetColumnValue(row, this.inputSuffixColumn);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x000755FF File Offset: 0x000737FF
		public override void BuildCommandWithScope(string searchText, object[] pipeline, DataRow row, object scope)
		{
			this.otherUPNSuffix = OtherUPNSuffixFiller.GetColumnValue(row, this.inputSuffixColumn);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00075614 File Offset: 0x00073814
		protected override void OnFill(DataTable table)
		{
			if (!string.IsNullOrEmpty(this.otherUPNSuffix) && !table.Rows.Contains(this.otherUPNSuffix))
			{
				DataRow dataRow = table.NewRow();
				Type dataType = table.Columns[this.fillColumn].DataType;
				if (dataType.IsAssignableFrom(typeof(SmtpDomainWithSubdomains)))
				{
					dataRow[this.fillColumn] = new SmtpDomainWithSubdomains(this.otherUPNSuffix);
				}
				else
				{
					dataRow[this.fillColumn] = this.otherUPNSuffix;
				}
				table.BeginLoadData();
				table.Rows.Add(dataRow);
				table.EndLoadData();
			}
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000756B7 File Offset: 0x000738B7
		public override object Clone()
		{
			return new OtherUPNSuffixFiller(this.inputSuffixColumn, this.fillColumn);
		}

		// Token: 0x040009DC RID: 2524
		private string inputSuffixColumn;

		// Token: 0x040009DD RID: 2525
		private string fillColumn;

		// Token: 0x040009DE RID: 2526
		private string otherUPNSuffix;
	}
}
