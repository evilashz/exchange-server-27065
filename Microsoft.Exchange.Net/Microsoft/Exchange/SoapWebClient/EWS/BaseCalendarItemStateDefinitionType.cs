using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200039B RID: 923
	[XmlInclude(typeof(DeleteFromFolderStateDefinitionType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(LocationBasedStateDefinitionType))]
	[XmlInclude(typeof(DeletedOccurrenceStateDefinitionType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class BaseCalendarItemStateDefinitionType
	{
	}
}
