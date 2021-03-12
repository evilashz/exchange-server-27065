using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200096A RID: 2410
	[DebuggerStepThrough]
	[MessageContract(WrapperName = "GetChangesResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class GetChangesResponse
	{
		// Token: 0x060070D8 RID: 28888 RVA: 0x00177835 File Offset: 0x00175A35
		public GetChangesResponse()
		{
		}

		// Token: 0x060070D9 RID: 28889 RVA: 0x0017783D File Offset: 0x00175A3D
		public GetChangesResponse(DirectoryChanges GetChangesResult)
		{
			this.GetChangesResult = GetChangesResult;
		}

		// Token: 0x04004945 RID: 18757
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public DirectoryChanges GetChangesResult;
	}
}
