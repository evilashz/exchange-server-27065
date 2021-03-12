using System;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200016E RID: 366
	[Serializable]
	internal class AirSyncXmlProperty : AirSyncProperty, IXmlProperty, IProperty
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x0005BC3A File Offset: 0x00059E3A
		public AirSyncXmlProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0005BC45 File Offset: 0x00059E45
		public XmlNode XmlData
		{
			get
			{
				return base.XmlNode;
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0005BC4D File Offset: 0x00059E4D
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
		}
	}
}
