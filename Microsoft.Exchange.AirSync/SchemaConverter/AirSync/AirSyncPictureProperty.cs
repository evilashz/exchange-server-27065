using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200015B RID: 347
	[Serializable]
	internal class AirSyncPictureProperty : AirSyncProperty, IPictureProperty, IProperty
	{
		// Token: 0x06001003 RID: 4099 RVA: 0x0005B108 File Offset: 0x00059308
		public AirSyncPictureProperty(string xmlNodeNamespace, string pictureBodyTag, bool requiresClientSupport) : base(xmlNodeNamespace, pictureBodyTag, requiresClientSupport)
		{
			base.ClientChangeTracked = true;
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0005B11A File Offset: 0x0005931A
		public string PictureData
		{
			get
			{
				return base.XmlNode.InnerText;
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0005B128 File Offset: 0x00059328
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IPictureProperty pictureProperty = srcProperty as IPictureProperty;
			if (pictureProperty == null)
			{
				throw new UnexpectedTypeException("IPictureProperty", srcProperty);
			}
			string pictureData = pictureProperty.PictureData;
			if (PropertyState.Modified == srcProperty.State && pictureData != null)
			{
				base.CreateAirSyncNode(pictureData);
			}
		}
	}
}
