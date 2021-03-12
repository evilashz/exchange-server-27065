using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000728 RID: 1832
	[XmlType("ConflictResolutionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum ConflictResolutionType
	{
		// Token: 0x04001EDE RID: 7902
		NeverOverwrite,
		// Token: 0x04001EDF RID: 7903
		AutoResolve,
		// Token: 0x04001EE0 RID: 7904
		AlwaysOverwrite
	}
}
