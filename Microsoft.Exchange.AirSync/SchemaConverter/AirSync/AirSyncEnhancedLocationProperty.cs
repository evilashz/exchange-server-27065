using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000151 RID: 337
	[Serializable]
	internal class AirSyncEnhancedLocationProperty : AirSyncNestedProperty
	{
		// Token: 0x06000FE8 RID: 4072 RVA: 0x0005A682 File Offset: 0x00058882
		public AirSyncEnhancedLocationProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport, int protocolVersion) : base(xmlNodeNamespace, airSyncTagName, new EnhancedLocationData(protocolVersion), requiresClientSupport)
		{
		}
	}
}
