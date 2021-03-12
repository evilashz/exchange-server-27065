using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000974 RID: 2420
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "MergeCookiesResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	public class MergeCookiesResponse
	{
		// Token: 0x060070E8 RID: 28904 RVA: 0x0017791F File Offset: 0x00175B1F
		public MergeCookiesResponse()
		{
		}

		// Token: 0x060070E9 RID: 28905 RVA: 0x00177927 File Offset: 0x00175B27
		public MergeCookiesResponse(DirectoryChanges MergeCookiesResult)
		{
			this.MergeCookiesResult = MergeCookiesResult;
		}

		// Token: 0x04004962 RID: 18786
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public DirectoryChanges MergeCookiesResult;
	}
}
