using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000549 RID: 1353
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RmsComplianceEntry : ComplianceEntryType
	{
		// Token: 0x0600263C RID: 9788 RVA: 0x000A643C File Offset: 0x000A463C
		public RmsComplianceEntry(string id, string name, string description) : base(id, name, description)
		{
		}
	}
}
