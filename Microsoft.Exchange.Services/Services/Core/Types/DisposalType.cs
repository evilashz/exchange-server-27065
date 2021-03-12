using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000752 RID: 1874
	[XmlType("DisposalType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum DisposalType
	{
		// Token: 0x04001F2E RID: 7982
		HardDelete = 2,
		// Token: 0x04001F2F RID: 7983
		SoftDelete = 1,
		// Token: 0x04001F30 RID: 7984
		MoveToDeletedItems = 4
	}
}
