using System;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x0200000B RID: 11
	internal class ConfigurationDataRowData : DataRowAccessBase<ConfigurationDataTable, ConfigurationDataRowData>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000288D File Offset: 0x00000A8D
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000028A5 File Offset: 0x00000AA5
		[PrimaryKey]
		public string ConfigName
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[0]).Value;
			}
			private set
			{
				((ColumnCache<string>)base.Columns[0]).Value = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000028BE File Offset: 0x00000ABE
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000028D6 File Offset: 0x00000AD6
		public string ConfigValue
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[1]).Value;
			}
			set
			{
				((ColumnCache<string>)base.Columns[1]).Value = value;
			}
		}
	}
}
