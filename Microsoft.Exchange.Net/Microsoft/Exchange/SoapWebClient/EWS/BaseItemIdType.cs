using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000193 RID: 403
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(ItemIdType))]
	[XmlInclude(typeof(RecurringMasterItemIdRangesType))]
	[XmlInclude(typeof(RootItemIdType))]
	[XmlInclude(typeof(OccurrenceItemIdType))]
	[DesignerCategory("code")]
	[XmlInclude(typeof(AttachmentIdType))]
	[XmlInclude(typeof(RequestAttachmentIdType))]
	[XmlInclude(typeof(RecurringMasterItemIdType))]
	[Serializable]
	public abstract class BaseItemIdType
	{
	}
}
