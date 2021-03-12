using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020002E6 RID: 742
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[Serializable]
	public class ArrayOfTrackingPropertiesType
	{
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x00066DCB File Offset: 0x00064FCB
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x00066DD3 File Offset: 0x00064FD3
		[XmlElement("TrackingPropertyType")]
		public TrackingPropertyType[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x04000E2D RID: 3629
		private TrackingPropertyType[] itemsField;
	}
}
