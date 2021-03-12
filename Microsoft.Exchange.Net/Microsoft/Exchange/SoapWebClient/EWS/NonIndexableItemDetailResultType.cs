using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200026F RID: 623
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class NonIndexableItemDetailResultType
	{
		// Token: 0x04001023 RID: 4131
		[XmlArrayItem("NonIndexableItemDetail", IsNullable = false)]
		public NonIndexableItemDetailType[] Items;

		// Token: 0x04001024 RID: 4132
		[XmlArrayItem("FailedMailbox", IsNullable = false)]
		public FailedSearchMailboxType[] FailedMailboxes;
	}
}
