using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003BD RID: 957
	[DataContract(Name = "ContactSearchDefinition", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ContactSearchDefinition : SearchDefinition
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0008C5DD File Offset: 0x0008A7DD
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x0008C5E5 File Offset: 0x0008A7E5
		[DataMember]
		public bool? HasErrorsOnly
		{
			get
			{
				return this.HasErrorsOnlyField;
			}
			set
			{
				this.HasErrorsOnlyField = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x0008C5EE File Offset: 0x0008A7EE
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x0008C5F6 File Offset: 0x0008A7F6
		[DataMember]
		public string[] IncludedProperties
		{
			get
			{
				return this.IncludedPropertiesField;
			}
			set
			{
				this.IncludedPropertiesField = value;
			}
		}

		// Token: 0x04001057 RID: 4183
		private bool? HasErrorsOnlyField;

		// Token: 0x04001058 RID: 4184
		private string[] IncludedPropertiesField;
	}
}
