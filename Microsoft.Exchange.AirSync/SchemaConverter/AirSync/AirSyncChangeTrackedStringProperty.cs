using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200013E RID: 318
	[Serializable]
	internal class AirSyncChangeTrackedStringProperty : AirSyncStringProperty
	{
		// Token: 0x06000F95 RID: 3989 RVA: 0x0005916F File Offset: 0x0005736F
		public AirSyncChangeTrackedStringProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
			base.ClientChangeTracked = true;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00059181 File Offset: 0x00057381
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
			base.XmlParentNode.AppendChild(base.XmlNode);
		}
	}
}
