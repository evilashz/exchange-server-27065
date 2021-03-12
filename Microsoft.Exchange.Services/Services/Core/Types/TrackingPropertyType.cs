using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200080A RID: 2058
	[XmlType(TypeName = "TrackingPropertyType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class TrackingPropertyType
	{
		// Token: 0x0400213E RID: 8510
		public string Name;

		// Token: 0x0400213F RID: 8511
		public string Value;
	}
}
