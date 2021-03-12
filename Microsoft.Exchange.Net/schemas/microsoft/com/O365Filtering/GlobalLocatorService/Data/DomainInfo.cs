using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C3B RID: 3131
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainInfo", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class DomainInfo : IExtensibleDataObject
	{
		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x000B6DF0 File Offset: 0x000B4FF0
		// (set) Token: 0x06004493 RID: 17555 RVA: 0x000B6DF8 File Offset: 0x000B4FF8
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

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x000B6E01 File Offset: 0x000B5001
		// (set) Token: 0x06004495 RID: 17557 RVA: 0x000B6E09 File Offset: 0x000B5009
		[DataMember]
		public string DomainKey
		{
			get
			{
				return this.DomainKeyField;
			}
			set
			{
				this.DomainKeyField = value;
			}
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x000B6E12 File Offset: 0x000B5012
		// (set) Token: 0x06004497 RID: 17559 RVA: 0x000B6E1A File Offset: 0x000B501A
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

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x000B6E23 File Offset: 0x000B5023
		// (set) Token: 0x06004499 RID: 17561 RVA: 0x000B6E2B File Offset: 0x000B502B
		[DataMember]
		public string[] NoneExistNamespaces
		{
			get
			{
				return this.NoneExistNamespacesField;
			}
			set
			{
				this.NoneExistNamespacesField = value;
			}
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x000B6E34 File Offset: 0x000B5034
		// (set) Token: 0x0600449B RID: 17563 RVA: 0x000B6E3C File Offset: 0x000B503C
		[DataMember(IsRequired = true)]
		public KeyValuePair<string, string>[] Properties
		{
			get
			{
				return this.PropertiesField;
			}
			set
			{
				this.PropertiesField = value;
			}
		}

		// Token: 0x04003A06 RID: 14854
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A07 RID: 14855
		private string DomainKeyField;

		// Token: 0x04003A08 RID: 14856
		private string DomainNameField;

		// Token: 0x04003A09 RID: 14857
		private string[] NoneExistNamespacesField;

		// Token: 0x04003A0A RID: 14858
		private KeyValuePair<string, string>[] PropertiesField;
	}
}
