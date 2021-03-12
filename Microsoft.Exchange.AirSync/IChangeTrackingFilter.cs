using System;
using System.Xml;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200004C RID: 76
	internal interface IChangeTrackingFilter
	{
		// Token: 0x060004A1 RID: 1185
		int?[] Filter(XmlNode xmlItemRoot, int?[] oldChangeTrackInfo);

		// Token: 0x060004A2 RID: 1186
		int?[] UpdateChangeTrackingInformation(XmlNode xmlItemRoot, int?[] oldChangeTrackInfo);
	}
}
