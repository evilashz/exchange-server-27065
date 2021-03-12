using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000977 RID: 2423
	[DebuggerStepThrough]
	[MessageContract(WrapperName = "GetDirSyncDrainageStatusResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class GetDirSyncDrainageStatusResponse
	{
		// Token: 0x060070EC RID: 28908 RVA: 0x00177954 File Offset: 0x00175B54
		public GetDirSyncDrainageStatusResponse()
		{
		}

		// Token: 0x060070ED RID: 28909 RVA: 0x0017795C File Offset: 0x00175B5C
		public GetDirSyncDrainageStatusResponse(DirSyncDrainageCode[] GetDirSyncDrainageStatusResult)
		{
			this.GetDirSyncDrainageStatusResult = GetDirSyncDrainageStatusResult;
		}

		// Token: 0x0400496E RID: 18798
		[XmlArray(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public DirSyncDrainageCode[] GetDirSyncDrainageStatusResult;
	}
}
