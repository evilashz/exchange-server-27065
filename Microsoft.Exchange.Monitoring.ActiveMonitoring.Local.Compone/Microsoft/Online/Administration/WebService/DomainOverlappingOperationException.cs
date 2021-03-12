using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200038B RID: 907
	[DataContract(Name = "DomainOverlappingOperationException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DomainOverlappingOperationException : DomainDataOperationException
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0008BFF2 File Offset: 0x0008A1F2
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x0008BFFA File Offset: 0x0008A1FA
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

		// Token: 0x04001008 RID: 4104
		private DomainCapabilities CapabilityField;
	}
}
