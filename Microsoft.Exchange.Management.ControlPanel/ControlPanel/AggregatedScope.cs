using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004D7 RID: 1239
	[DataContract]
	public class AggregatedScope
	{
		// Token: 0x040027B2 RID: 10162
		[DataMember]
		public bool IsOrganizationalUnit;

		// Token: 0x040027B3 RID: 10163
		[DataMember]
		public string ID;
	}
}
