using System;
using System.IO;
using System.Xml;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200003E RID: 62
	internal interface IMultipartResponse
	{
		// Token: 0x060003AD RID: 941
		void BuildResponse(XmlNode responseNode, int partnumber);

		// Token: 0x060003AE RID: 942
		Stream GetResponseStream();
	}
}
