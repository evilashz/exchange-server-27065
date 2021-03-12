using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200058B RID: 1419
	[XmlType(TypeName = "RuleOperationErrorType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class RuleOperationError
	{
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x000A789F File Offset: 0x000A5A9F
		// (set) Token: 0x0600278C RID: 10124 RVA: 0x000A78A7 File Offset: 0x000A5AA7
		[XmlElement(Order = 0)]
		public int OperationIndex { get; set; }

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x000A78B0 File Offset: 0x000A5AB0
		// (set) Token: 0x0600278E RID: 10126 RVA: 0x000A78B8 File Offset: 0x000A5AB8
		[XmlArrayItem("Error", Type = typeof(RuleValidationError))]
		[XmlArray(Order = 1)]
		public RuleValidationError[] ValidationErrors { get; set; }
	}
}
