using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C3A RID: 3130
	[DataContract(Name = "TenantInfo", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class TenantInfo : IExtensibleDataObject
	{
		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06004489 RID: 17545 RVA: 0x000B6DA4 File Offset: 0x000B4FA4
		// (set) Token: 0x0600448A RID: 17546 RVA: 0x000B6DAC File Offset: 0x000B4FAC
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

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x0600448B RID: 17547 RVA: 0x000B6DB5 File Offset: 0x000B4FB5
		// (set) Token: 0x0600448C RID: 17548 RVA: 0x000B6DBD File Offset: 0x000B4FBD
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

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x0600448D RID: 17549 RVA: 0x000B6DC6 File Offset: 0x000B4FC6
		// (set) Token: 0x0600448E RID: 17550 RVA: 0x000B6DCE File Offset: 0x000B4FCE
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

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x0600448F RID: 17551 RVA: 0x000B6DD7 File Offset: 0x000B4FD7
		// (set) Token: 0x06004490 RID: 17552 RVA: 0x000B6DDF File Offset: 0x000B4FDF
		[DataMember(IsRequired = true)]
		public Guid TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x04003A02 RID: 14850
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A03 RID: 14851
		private string[] NoneExistNamespacesField;

		// Token: 0x04003A04 RID: 14852
		private KeyValuePair<string, string>[] PropertiesField;

		// Token: 0x04003A05 RID: 14853
		private Guid TenantIdField;
	}
}
