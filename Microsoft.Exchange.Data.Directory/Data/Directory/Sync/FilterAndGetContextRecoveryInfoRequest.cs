using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200097C RID: 2428
	[MessageContract(WrapperName = "FilterAndGetContextRecoveryInfo", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	public class FilterAndGetContextRecoveryInfoRequest
	{
		// Token: 0x060070F6 RID: 28918 RVA: 0x001779F5 File Offset: 0x00175BF5
		public FilterAndGetContextRecoveryInfoRequest()
		{
		}

		// Token: 0x060070F7 RID: 28919 RVA: 0x001779FD File Offset: 0x00175BFD
		public FilterAndGetContextRecoveryInfoRequest(byte[] getChangesCookie, string contextId)
		{
			this.getChangesCookie = getChangesCookie;
			this.contextId = contextId;
		}

		// Token: 0x04004979 RID: 18809
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public byte[] getChangesCookie;

		// Token: 0x0400497A RID: 18810
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 1)]
		public string contextId;
	}
}
