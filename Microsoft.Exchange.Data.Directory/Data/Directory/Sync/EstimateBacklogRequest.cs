using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200097E RID: 2430
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	[MessageContract(WrapperName = "EstimateBacklog", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class EstimateBacklogRequest
	{
		// Token: 0x060070FA RID: 28922 RVA: 0x00177A2A File Offset: 0x00175C2A
		public EstimateBacklogRequest()
		{
		}

		// Token: 0x060070FB RID: 28923 RVA: 0x00177A32 File Offset: 0x00175C32
		public EstimateBacklogRequest(byte[] latestGetChangesCookie, byte[] lastPageToken)
		{
			this.latestGetChangesCookie = latestGetChangesCookie;
			this.lastPageToken = lastPageToken;
		}

		// Token: 0x0400497C RID: 18812
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public byte[] latestGetChangesCookie;

		// Token: 0x0400497D RID: 18813
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 1)]
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		public byte[] lastPageToken;
	}
}
