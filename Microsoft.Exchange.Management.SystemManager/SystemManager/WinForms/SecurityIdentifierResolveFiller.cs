using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Principal;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200026C RID: 620
	internal class SecurityIdentifierResolveFiller : AbstractDataTableFiller
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x000756CA File Offset: 0x000738CA
		public SecurityIdentifierResolveFiller(string sidColumn, string userColumn)
		{
			if (string.IsNullOrEmpty(sidColumn))
			{
				throw new ArgumentNullException("sidColumn");
			}
			if (string.IsNullOrEmpty(userColumn))
			{
				throw new ArgumentNullException("userColumn");
			}
			this.sidColumn = sidColumn;
			this.userColumn = userColumn;
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x00075706 File Offset: 0x00073906
		public override ICommandBuilder CommandBuilder
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00075709 File Offset: 0x00073909
		public override void BuildCommand(string searchText, object[] pipeline, DataRow row)
		{
			this.BuildCommandWithScope(searchText, pipeline, row, null);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00075718 File Offset: 0x00073918
		public override void BuildCommandWithScope(string searchText, object[] pipeline, DataRow row, object scope)
		{
			if (!DBNull.Value.Equals(row["SidToUserFriendlyNameMap"]))
			{
				this.sidToUserFriendlyNameMap = (IDictionary<SecurityIdentifier, string>)row["SidToUserFriendlyNameMap"];
				this.pipeline = pipeline;
				return;
			}
			throw new NotSupportedException("Input valude for SidToUserFriendlyNameMap is mandantory.");
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00075768 File Offset: 0x00073968
		protected override void OnFill(DataTable table)
		{
			table.BeginLoadData();
			foreach (SecurityIdentifier securityIdentifier in this.pipeline)
			{
				DataRow dataRow = table.NewRow();
				dataRow[this.sidColumn] = securityIdentifier;
				dataRow[this.userColumn] = this.sidToUserFriendlyNameMap[securityIdentifier];
				table.Rows.Add(dataRow);
			}
			table.EndLoadData();
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000757D7 File Offset: 0x000739D7
		public override object Clone()
		{
			return new SecurityIdentifierResolveFiller(this.sidColumn, this.userColumn);
		}

		// Token: 0x040009DF RID: 2527
		private IDictionary<SecurityIdentifier, string> sidToUserFriendlyNameMap;

		// Token: 0x040009E0 RID: 2528
		private object[] pipeline;

		// Token: 0x040009E1 RID: 2529
		private string userColumn;

		// Token: 0x040009E2 RID: 2530
		private string sidColumn;
	}
}
