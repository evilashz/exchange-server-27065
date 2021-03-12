using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200080B RID: 2059
	[XmlType(TypeName = "ArrayOfTrackingPropertiesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class ArrayOfTrackingPropertiesType
	{
		// Token: 0x04002140 RID: 8512
		[XmlElement("TrackingPropertyType", IsNullable = false)]
		public TrackingPropertyType[] Items;
	}
}
