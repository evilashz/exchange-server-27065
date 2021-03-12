using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C3E RID: 3134
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainQuery", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[DebuggerStepThrough]
	public class DomainQuery : IExtensibleDataObject
	{
		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x060044AD RID: 17581 RVA: 0x000B6ED4 File Offset: 0x000B50D4
		// (set) Token: 0x060044AE RID: 17582 RVA: 0x000B6EDC File Offset: 0x000B50DC
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

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x000B6EE5 File Offset: 0x000B50E5
		// (set) Token: 0x060044B0 RID: 17584 RVA: 0x000B6EED File Offset: 0x000B50ED
		[DataMember(IsRequired = true)]
		public string DomainName
		{
			get
			{
				return this.DomainNameField;
			}
			set
			{
				this.DomainNameField = value;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060044B1 RID: 17585 RVA: 0x000B6EF6 File Offset: 0x000B50F6
		// (set) Token: 0x060044B2 RID: 17586 RVA: 0x000B6EFE File Offset: 0x000B50FE
		[DataMember]
		public string[] PropertyNames
		{
			get
			{
				return this.PropertyNamesField;
			}
			set
			{
				this.PropertyNamesField = value;
			}
		}

		// Token: 0x04003A12 RID: 14866
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A13 RID: 14867
		private string DomainNameField;

		// Token: 0x04003A14 RID: 14868
		private string[] PropertyNamesField;
	}
}
