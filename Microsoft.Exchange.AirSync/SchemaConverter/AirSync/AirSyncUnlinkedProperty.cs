using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200016B RID: 363
	[Serializable]
	internal class AirSyncUnlinkedProperty : AirSyncProperty, IUnlinkedProperty, IProperty
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x0005BB46 File Offset: 0x00059D46
		public AirSyncUnlinkedProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0005BB51 File Offset: 0x00059D51
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
		}
	}
}
