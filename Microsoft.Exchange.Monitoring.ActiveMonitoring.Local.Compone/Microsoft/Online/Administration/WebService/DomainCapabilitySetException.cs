using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000376 RID: 886
	[DataContract(Name = "DomainCapabilitySetException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DomainCapabilitySetException : DomainDataOperationException
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0008BF17 File Offset: 0x0008A117
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x0008BF1F File Offset: 0x0008A11F
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

		// Token: 0x04001005 RID: 4101
		private DomainCapabilities CapabilityField;
	}
}
