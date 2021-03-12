using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000806 RID: 2054
	[XmlType(TypeName = "MessageTrackingReportTemplateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum MessageTrackingReportTemplate
	{
		// Token: 0x04002120 RID: 8480
		Summary,
		// Token: 0x04002121 RID: 8481
		RecipientPath
	}
}
