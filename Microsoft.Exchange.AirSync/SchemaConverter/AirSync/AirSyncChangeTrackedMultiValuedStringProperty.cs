using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	internal class AirSyncChangeTrackedMultiValuedStringProperty : AirSyncMultiValuedStringProperty
	{
		// Token: 0x06000F8F RID: 3983 RVA: 0x00059070 File Offset: 0x00057270
		public AirSyncChangeTrackedMultiValuedStringProperty(string xmlNodeNamespace, string airSyncTagName, string airSyncChildTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, airSyncChildTagName, requiresClientSupport)
		{
			base.ClientChangeTracked = true;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00059084 File Offset: 0x00057284
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
			base.XmlParentNode.AppendChild(base.XmlNode);
		}
	}
}
