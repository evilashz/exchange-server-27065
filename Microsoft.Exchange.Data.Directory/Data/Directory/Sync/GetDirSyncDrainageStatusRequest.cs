using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000976 RID: 2422
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "GetDirSyncDrainageStatus", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	public class GetDirSyncDrainageStatusRequest
	{
		// Token: 0x060070EA RID: 28906 RVA: 0x00177936 File Offset: 0x00175B36
		public GetDirSyncDrainageStatusRequest()
		{
		}

		// Token: 0x060070EB RID: 28907 RVA: 0x0017793E File Offset: 0x00175B3E
		public GetDirSyncDrainageStatusRequest(ContextDirSyncStatus[] contextDirSyncStatusList, byte[] getChangesCookie)
		{
			this.contextDirSyncStatusList = contextDirSyncStatusList;
			this.getChangesCookie = getChangesCookie;
		}

		// Token: 0x0400496C RID: 18796
		[XmlArrayItem(IsNullable = false)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		[XmlArray(IsNullable = true)]
		public ContextDirSyncStatus[] contextDirSyncStatusList;

		// Token: 0x0400496D RID: 18797
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 1)]
		public byte[] getChangesCookie;
	}
}
