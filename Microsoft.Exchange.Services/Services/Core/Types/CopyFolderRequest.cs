using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000402 RID: 1026
	[XmlType("CopyFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CopyFolderRequest : BaseMoveCopyFolderRequest
	{
		// Token: 0x06001D20 RID: 7456 RVA: 0x0009EF37 File Offset: 0x0009D137
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CopyFolder(callContext, this);
		}

		// Token: 0x04001307 RID: 4871
		internal const string ElementName = "CopyFolder";
	}
}
