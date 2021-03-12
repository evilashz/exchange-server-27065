using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x0200003D RID: 61
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum SuggestionQuality
	{
		// Token: 0x040000BA RID: 186
		Excellent,
		// Token: 0x040000BB RID: 187
		Good,
		// Token: 0x040000BC RID: 188
		Fair,
		// Token: 0x040000BD RID: 189
		Poor
	}
}
