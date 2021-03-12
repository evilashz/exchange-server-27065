using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003E4 RID: 996
	[XmlInclude(typeof(CalendarViewType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(SeekToConditionPageViewType))]
	[XmlInclude(typeof(FractionalPageViewType))]
	[XmlInclude(typeof(IndexedPageViewType))]
	[XmlInclude(typeof(ContactsViewType))]
	[Serializable]
	public abstract class BasePagingType
	{
		// Token: 0x04001593 RID: 5523
		[XmlAttribute]
		public int MaxEntriesReturned;

		// Token: 0x04001594 RID: 5524
		[XmlIgnore]
		public bool MaxEntriesReturnedSpecified;
	}
}
