using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200064E RID: 1614
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SearchFolderTraversalType
	{
		// Token: 0x04001C8E RID: 7310
		Shallow,
		// Token: 0x04001C8F RID: 7311
		Deep
	}
}
