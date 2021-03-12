using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003E5 RID: 997
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MessageClassificationType : ComplianceEntryType
	{
		// Token: 0x0600201D RID: 8221 RVA: 0x000786DB File Offset: 0x000768DB
		public MessageClassificationType(string id, string name, string description) : base(id, name, description)
		{
		}
	}
}
