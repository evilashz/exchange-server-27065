using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003FA RID: 1018
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RmsTemplateType : ComplianceEntryType
	{
		// Token: 0x06002106 RID: 8454 RVA: 0x0007945B File Offset: 0x0007765B
		public RmsTemplateType(string id, string name, string description) : base(id, name, description)
		{
		}
	}
}
