using System;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	internal class MasterSchemaMappingEntry
	{
		// Token: 0x0600016D RID: 365 RVA: 0x000068CC File Offset: 0x00004ACC
		public MasterSchemaMappingEntry()
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000068D4 File Offset: 0x00004AD4
		public MasterSchemaMappingEntry(string className, string dataSourceInfoName, string schemaFileName, string dataSourceManagerAssemblyName, string dataSourceInfoPath)
		{
			this.className = className;
			this.dataSourceInfoName = dataSourceInfoName;
			this.schemaFileName = schemaFileName;
			this.dataSourceManagerAssemblyName = dataSourceManagerAssemblyName;
			this.dataSourceInfoPath = dataSourceInfoPath;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00006901 File Offset: 0x00004B01
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00006909 File Offset: 0x00004B09
		public string ClassName
		{
			get
			{
				return this.className;
			}
			set
			{
				this.className = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00006912 File Offset: 0x00004B12
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000691A File Offset: 0x00004B1A
		public string DataSourceInfoName
		{
			get
			{
				return this.dataSourceInfoName;
			}
			set
			{
				this.dataSourceInfoName = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00006923 File Offset: 0x00004B23
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000692B File Offset: 0x00004B2B
		public string SchemaFileName
		{
			get
			{
				return this.schemaFileName;
			}
			set
			{
				this.schemaFileName = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006934 File Offset: 0x00004B34
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000693C File Offset: 0x00004B3C
		public string DataSourceManagerAssemblyName
		{
			get
			{
				return this.dataSourceManagerAssemblyName;
			}
			set
			{
				this.dataSourceManagerAssemblyName = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00006945 File Offset: 0x00004B45
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000694D File Offset: 0x00004B4D
		public string DataSourceInfoPath
		{
			get
			{
				return this.dataSourceInfoPath;
			}
			set
			{
				this.dataSourceInfoPath = value;
			}
		}

		// Token: 0x04000074 RID: 116
		private string className;

		// Token: 0x04000075 RID: 117
		private string dataSourceInfoName;

		// Token: 0x04000076 RID: 118
		private string schemaFileName;

		// Token: 0x04000077 RID: 119
		private string dataSourceManagerAssemblyName;

		// Token: 0x04000078 RID: 120
		private string dataSourceInfoPath;
	}
}
