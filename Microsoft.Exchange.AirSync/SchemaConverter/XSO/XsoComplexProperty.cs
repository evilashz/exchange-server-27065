using System;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200020C RID: 524
	[Serializable]
	internal class XsoComplexProperty : XsoProperty, IXmlProperty, IProperty
	{
		// Token: 0x06001437 RID: 5175 RVA: 0x00074A8E File Offset: 0x00072C8E
		public XsoComplexProperty() : base(null)
		{
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00074A97 File Offset: 0x00072C97
		public virtual XmlNode XmlData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00074A9A File Offset: 0x00072C9A
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
		}
	}
}
