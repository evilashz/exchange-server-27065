using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000069 RID: 105
	public class Keyword
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000DBF7 File Offset: 0x0000BDF7
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000DBFF File Offset: 0x0000BDFF
		[XmlText]
		public string KeywordString { get; set; }
	}
}
