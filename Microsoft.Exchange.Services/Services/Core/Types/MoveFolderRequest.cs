using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000466 RID: 1126
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("MoveFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class MoveFolderRequest : BaseMoveCopyFolderRequest
	{
		// Token: 0x06002133 RID: 8499 RVA: 0x000A23A4 File Offset: 0x000A05A4
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new MoveFolder(callContext, this);
		}

		// Token: 0x04001480 RID: 5248
		internal const string ElementName = "MoveFolder";
	}
}
