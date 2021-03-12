using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000403 RID: 1027
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CopyItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CopyItemRequest : BaseMoveCopyItemRequest
	{
		// Token: 0x06001D22 RID: 7458 RVA: 0x0009EF48 File Offset: 0x0009D148
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			if (this.CanOptimizeCommandExecution(callContext))
			{
				return new CopyItemBatch(callContext, this);
			}
			return new CopyItem(callContext, this);
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0009EF62 File Offset: 0x0009D162
		protected override string CommandName
		{
			get
			{
				return "copyitem";
			}
		}

		// Token: 0x04001308 RID: 4872
		internal const string ElementName = "CopyItem";
	}
}
