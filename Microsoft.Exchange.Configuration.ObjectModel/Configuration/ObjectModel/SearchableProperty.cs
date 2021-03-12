using System;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	internal class SearchableProperty : SchemaMappingEntry
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00005B38 File Offset: 0x00003D38
		public SearchableProperty()
		{
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005B40 File Offset: 0x00003D40
		public SearchableProperty(string sourceClassName, string sourcePropertyName)
		{
			this.sourceClassName = sourceClassName;
			this.sourcePropertyName = sourcePropertyName;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005B56 File Offset: 0x00003D56
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00005B5E File Offset: 0x00003D5E
		public string SourceClassName
		{
			get
			{
				return this.sourceClassName;
			}
			set
			{
				this.sourceClassName = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005B67 File Offset: 0x00003D67
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00005B6F File Offset: 0x00003D6F
		public string SourcePropertyName
		{
			get
			{
				return this.sourcePropertyName;
			}
			set
			{
				this.sourcePropertyName = value;
			}
		}

		// Token: 0x04000068 RID: 104
		private string sourceClassName;

		// Token: 0x04000069 RID: 105
		private string sourcePropertyName;
	}
}
