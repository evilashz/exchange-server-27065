using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x02000012 RID: 18
	public class CsvTable
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00002A84 File Offset: 0x00000C84
		public CsvTable(CsvField[] fields)
		{
			this.fields = fields;
			CsvField[] array = new CsvField[this.fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				array[i] = this.fields[i].CsvFieldImpl;
			}
			this.csvTableImpl = new CsvTable(array);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002AD6 File Offset: 0x00000CD6
		public CsvField[] Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002ADE File Offset: 0x00000CDE
		internal CsvTable CsvTableImpl
		{
			get
			{
				return this.csvTableImpl;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002AE6 File Offset: 0x00000CE6
		public int NameToIndex(string name)
		{
			return this.csvTableImpl.NameToIndex(name);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public bool TryGetTypeByName(string name, out Type type)
		{
			return this.csvTableImpl.TryGetTypeByName(name, out type);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002B03 File Offset: 0x00000D03
		public int[] GetFieldsAddedAfterVersion(Version lowerBoundVersion)
		{
			return this.csvTableImpl.GetFieldsAddedAfterVersion(lowerBoundVersion);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002B11 File Offset: 0x00000D11
		public byte[] SerializeFieldNameList(Version highestBuildToInclude)
		{
			return this.csvTableImpl.SerializeFieldNameList(highestBuildToInclude);
		}

		// Token: 0x0400002F RID: 47
		private CsvTable csvTableImpl;

		// Token: 0x04000030 RID: 48
		private CsvField[] fields;
	}
}
