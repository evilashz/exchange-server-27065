using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200010E RID: 270
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(GetUserAvailabilityRequest))]
	[Serializable]
	public class BaseRequestType
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001F856 File Offset: 0x0001DA56
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0001F85E File Offset: 0x0001DA5E
		public string XmlElementName
		{
			get
			{
				return this.xmlElementNameField;
			}
			set
			{
				this.xmlElementNameField = value;
			}
		}

		// Token: 0x04000467 RID: 1127
		private string xmlElementNameField;
	}
}
