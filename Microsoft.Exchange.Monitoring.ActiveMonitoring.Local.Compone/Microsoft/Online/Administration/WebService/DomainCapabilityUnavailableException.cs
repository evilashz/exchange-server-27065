using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200038A RID: 906
	[DebuggerStepThrough]
	[DataContract(Name = "DomainCapabilityUnavailableException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DomainCapabilityUnavailableException : DomainDataOperationException
	{
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0008BFD9 File Offset: 0x0008A1D9
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x0008BFE1 File Offset: 0x0008A1E1
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

		// Token: 0x04001007 RID: 4103
		private DomainCapabilities CapabilityField;
	}
}
