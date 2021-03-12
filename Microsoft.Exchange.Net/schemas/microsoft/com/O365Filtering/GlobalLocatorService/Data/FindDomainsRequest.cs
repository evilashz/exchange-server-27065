using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C44 RID: 3140
	[DataContract(Name = "FindDomainsRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class FindDomainsRequest : IExtensibleDataObject
	{
		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x060044D2 RID: 17618 RVA: 0x000B700C File Offset: 0x000B520C
		// (set) Token: 0x060044D3 RID: 17619 RVA: 0x000B7014 File Offset: 0x000B5214
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x060044D4 RID: 17620 RVA: 0x000B701D File Offset: 0x000B521D
		// (set) Token: 0x060044D5 RID: 17621 RVA: 0x000B7025 File Offset: 0x000B5225
		[DataMember(IsRequired = true)]
		public string[] DomainPropertyNames
		{
			get
			{
				return this.DomainPropertyNamesField;
			}
			set
			{
				this.DomainPropertyNamesField = value;
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x060044D6 RID: 17622 RVA: 0x000B702E File Offset: 0x000B522E
		// (set) Token: 0x060044D7 RID: 17623 RVA: 0x000B7036 File Offset: 0x000B5236
		[DataMember(IsRequired = true)]
		public string[] DomainsName
		{
			get
			{
				return this.DomainsNameField;
			}
			set
			{
				this.DomainsNameField = value;
			}
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x060044D8 RID: 17624 RVA: 0x000B703F File Offset: 0x000B523F
		// (set) Token: 0x060044D9 RID: 17625 RVA: 0x000B7047 File Offset: 0x000B5247
		[DataMember]
		public int ReadFlag
		{
			get
			{
				return this.ReadFlagField;
			}
			set
			{
				this.ReadFlagField = value;
			}
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x000B7050 File Offset: 0x000B5250
		// (set) Token: 0x060044DB RID: 17627 RVA: 0x000B7058 File Offset: 0x000B5258
		[DataMember]
		public string[] TenantPropertyNames
		{
			get
			{
				return this.TenantPropertyNamesField;
			}
			set
			{
				this.TenantPropertyNamesField = value;
			}
		}

		// Token: 0x04003A27 RID: 14887
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A28 RID: 14888
		private string[] DomainPropertyNamesField;

		// Token: 0x04003A29 RID: 14889
		private string[] DomainsNameField;

		// Token: 0x04003A2A RID: 14890
		private int ReadFlagField;

		// Token: 0x04003A2B RID: 14891
		private string[] TenantPropertyNamesField;
	}
}
