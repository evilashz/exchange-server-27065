using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000135 RID: 309
	[Serializable]
	internal class AirSyncBooleanProperty : AirSyncProperty, IBooleanProperty, IProperty
	{
		// Token: 0x06000F7E RID: 3966 RVA: 0x00058B75 File Offset: 0x00056D75
		public AirSyncBooleanProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00058B80 File Offset: 0x00056D80
		public bool BooleanData
		{
			get
			{
				string innerText;
				if ((innerText = base.XmlNode.InnerText) != null)
				{
					if (innerText == "0")
					{
						return false;
					}
					if (innerText == "1")
					{
						return true;
					}
				}
				throw new ConversionException("Incorrectly-formatted boolean");
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00058BC8 File Offset: 0x00056DC8
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IBooleanProperty booleanProperty = srcProperty as IBooleanProperty;
			if (booleanProperty == null)
			{
				throw new UnexpectedTypeException("IBooleanProperty", srcProperty);
			}
			if (booleanProperty.BooleanData)
			{
				base.CreateAirSyncNode("1");
				return;
			}
			base.CreateAirSyncNode("0");
		}
	}
}
