using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000972 RID: 2418
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(WrapperName = "GetDirectoryObjectsResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	public class GetDirectoryObjectsResponse
	{
		// Token: 0x060070E4 RID: 28900 RVA: 0x001778E3 File Offset: 0x00175AE3
		public GetDirectoryObjectsResponse()
		{
		}

		// Token: 0x060070E5 RID: 28901 RVA: 0x001778EB File Offset: 0x00175AEB
		public GetDirectoryObjectsResponse(DirectoryObjectsAndLinks GetDirectoryObjectsResult)
		{
			this.GetDirectoryObjectsResult = GetDirectoryObjectsResult;
		}

		// Token: 0x0400495E RID: 18782
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public DirectoryObjectsAndLinks GetDirectoryObjectsResult;
	}
}
