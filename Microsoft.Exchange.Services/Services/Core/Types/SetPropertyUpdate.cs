using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006B2 RID: 1714
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class SetPropertyUpdate : PropertyUpdate
	{
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060034DB RID: 13531
		[XmlIgnore]
		[IgnoreDataMember]
		internal abstract ServiceObject ServiceObject { get; }
	}
}
