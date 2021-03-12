using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000A0 RID: 160
	[KnownType(typeof(BaseDirectoryCacheRequest))]
	[DataContract]
	internal class DiagnosticsCacheRequest : BaseDirectoryCacheRequest, IExtensibleDataObject
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x00027EDE File Offset: 0x000260DE
		public DiagnosticsCacheRequest() : base(TopologyProvider.LocalForestFqdn)
		{
		}
	}
}
