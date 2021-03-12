using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200048A RID: 1162
	[DataContract]
	public abstract class SyncPersonaContactsRequestBase : BaseRequest
	{
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x000A3255 File Offset: 0x000A1455
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x000A325D File Offset: 0x000A145D
		[XmlElement("SyncState", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "SyncState", IsRequired = false)]
		public string SyncState { get; set; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x000A3266 File Offset: 0x000A1466
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x000A326E File Offset: 0x000A146E
		[XmlElement("MaxChangesReturned", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "MaxChangesReturned", IsRequired = true)]
		public int MaxChangesReturned { get; set; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x000A3277 File Offset: 0x000A1477
		// (set) Token: 0x06002287 RID: 8839 RVA: 0x000A327F File Offset: 0x000A147F
		[XmlElement("MaxPersonas", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "MaxPersonas", IsRequired = false)]
		public int MaxPersonas { get; set; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x000A3288 File Offset: 0x000A1488
		// (set) Token: 0x06002289 RID: 8841 RVA: 0x000A3290 File Offset: 0x000A1490
		[DataMember(Name = "JumpHeaderValues", IsRequired = false)]
		[XmlElement("JumpHeaderValues", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string[] JumpHeaderValues { get; set; }

		// Token: 0x0600228A RID: 8842 RVA: 0x000A3299 File Offset: 0x000A1499
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000A329C File Offset: 0x000A149C
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
