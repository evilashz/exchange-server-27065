using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000353 RID: 851
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "TrackingHeader", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class TrackingHeader : IExtensibleDataObject
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x0008BCEF File Offset: 0x00089EEF
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x0008BCF7 File Offset: 0x00089EF7
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

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0008BD00 File Offset: 0x00089F00
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x0008BD08 File Offset: 0x00089F08
		[DataMember]
		public Guid CorrelationId
		{
			get
			{
				return this.CorrelationIdField;
			}
			set
			{
				this.CorrelationIdField = value;
			}
		}

		// Token: 0x04000FF5 RID: 4085
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000FF6 RID: 4086
		private Guid CorrelationIdField;
	}
}
