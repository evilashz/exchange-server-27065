using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000238 RID: 568
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetServerTimeZonesResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00026571 File Offset: 0x00024771
		// (set) Token: 0x06001587 RID: 5511 RVA: 0x00026579 File Offset: 0x00024779
		public ArrayOfTimeZoneDefinitionType TimeZoneDefinitions
		{
			get
			{
				return this.timeZoneDefinitionsField;
			}
			set
			{
				this.timeZoneDefinitionsField = value;
			}
		}

		// Token: 0x04000ED2 RID: 3794
		private ArrayOfTimeZoneDefinitionType timeZoneDefinitionsField;
	}
}
