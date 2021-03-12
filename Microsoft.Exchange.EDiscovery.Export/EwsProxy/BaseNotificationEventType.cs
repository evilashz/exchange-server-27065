using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200022F RID: 559
	[XmlInclude(typeof(BaseObjectChangedEventType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(MovedCopiedEventType))]
	[XmlInclude(typeof(ModifiedEventType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class BaseNotificationEventType
	{
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0002643A File Offset: 0x0002463A
		// (set) Token: 0x06001562 RID: 5474 RVA: 0x00026442 File Offset: 0x00024642
		public string Watermark
		{
			get
			{
				return this.watermarkField;
			}
			set
			{
				this.watermarkField = value;
			}
		}

		// Token: 0x04000EB7 RID: 3767
		private string watermarkField;
	}
}
