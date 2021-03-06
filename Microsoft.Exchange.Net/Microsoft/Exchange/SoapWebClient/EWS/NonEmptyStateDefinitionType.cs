using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200039F RID: 927
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class NonEmptyStateDefinitionType
	{
		// Token: 0x040014A5 RID: 5285
		[XmlElement("LocationBasedStateDefinition", typeof(LocationBasedStateDefinitionType))]
		[XmlElement("DeletedOccurrenceStateDefinition", typeof(DeletedOccurrenceStateDefinitionType))]
		[XmlElement("DeleteFromFolderStateDefinition", typeof(DeleteFromFolderStateDefinitionType))]
		public BaseCalendarItemStateDefinitionType Item;
	}
}
