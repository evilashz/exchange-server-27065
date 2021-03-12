using System;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000F6 RID: 246
	internal class SetCommandSettings : CommandSettings
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x00022A72 File Offset: 0x00020C72
		public SetCommandSettings(ServiceObject serviceObject, StoreObject storeObject)
		{
			this.serviceObject = serviceObject;
			this.storeObject = storeObject;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00022A88 File Offset: 0x00020C88
		public StoreObject StoreObject
		{
			get
			{
				return this.storeObject;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00022A90 File Offset: 0x00020C90
		public ServiceObject ServiceObject
		{
			get
			{
				return this.serviceObject;
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00022A98 File Offset: 0x00020C98
		public SetCommandSettings(XmlElement serviceProperty, StoreObject storeObject)
		{
			this.serviceProperty = serviceProperty;
			this.storeObject = storeObject;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00022AAE File Offset: 0x00020CAE
		public XmlElement ServiceProperty
		{
			get
			{
				return this.serviceProperty;
			}
		}

		// Token: 0x040006D3 RID: 1747
		private StoreObject storeObject;

		// Token: 0x040006D4 RID: 1748
		private ServiceObject serviceObject;

		// Token: 0x040006D5 RID: 1749
		private XmlElement serviceProperty;
	}
}
