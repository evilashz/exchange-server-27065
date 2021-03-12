using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000DB RID: 219
	[XmlRoot("ActiveCondition")]
	[XmlType("ActiveCondition")]
	public class ActiveConditionalMetadataResult : ConditionalMetadataResult
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00024FBF File Offset: 0x000231BF
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x00024FC7 File Offset: 0x000231C7
		public string TimeRemaining { get; set; }
	}
}
