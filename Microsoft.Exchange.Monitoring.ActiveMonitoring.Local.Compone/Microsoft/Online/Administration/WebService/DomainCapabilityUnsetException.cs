using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000381 RID: 897
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainCapabilityUnsetException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class DomainCapabilityUnsetException : DomainDataOperationException
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0008BF80 File Offset: 0x0008A180
		// (set) Token: 0x06001658 RID: 5720 RVA: 0x0008BF88 File Offset: 0x0008A188
		[DataMember]
		public DomainCapabilities Capability
		{
			get
			{
				return this.CapabilityField;
			}
			set
			{
				this.CapabilityField = value;
			}
		}

		// Token: 0x04001006 RID: 4102
		private DomainCapabilities CapabilityField;
	}
}
