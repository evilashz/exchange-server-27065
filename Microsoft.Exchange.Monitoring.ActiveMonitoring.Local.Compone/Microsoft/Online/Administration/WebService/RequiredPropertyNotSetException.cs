using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020003AB RID: 939
	[DataContract(Name = "RequiredPropertyNotSetException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class RequiredPropertyNotSetException : MsolAdministrationException
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0008C279 File Offset: 0x0008A479
		// (set) Token: 0x060016B4 RID: 5812 RVA: 0x0008C281 File Offset: 0x0008A481
		[DataMember]
		public string ParentObjectType
		{
			get
			{
				return this.ParentObjectTypeField;
			}
			set
			{
				this.ParentObjectTypeField = value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0008C28A File Offset: 0x0008A48A
		// (set) Token: 0x060016B6 RID: 5814 RVA: 0x0008C292 File Offset: 0x0008A492
		[DataMember]
		public string PropertyName
		{
			get
			{
				return this.PropertyNameField;
			}
			set
			{
				this.PropertyNameField = value;
			}
		}

		// Token: 0x0400101F RID: 4127
		private string ParentObjectTypeField;

		// Token: 0x04001020 RID: 4128
		private string PropertyNameField;
	}
}
