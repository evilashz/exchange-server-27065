using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020003A4 RID: 932
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "PropertyDomainValidationException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class PropertyDomainValidationException : PropertyValidationException
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x0008C230 File Offset: 0x0008A430
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x0008C238 File Offset: 0x0008A438
		[DataMember]
		public string ObjectKey
		{
			get
			{
				return this.ObjectKeyField;
			}
			set
			{
				this.ObjectKeyField = value;
			}
		}

		// Token: 0x0400101E RID: 4126
		private string ObjectKeyField;
	}
}
