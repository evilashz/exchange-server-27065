using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000467 RID: 1127
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("MoveItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class MoveItemRequest : BaseMoveCopyItemRequest
	{
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x000A23B5 File Offset: 0x000A05B5
		protected override string CommandName
		{
			get
			{
				return "moveitem";
			}
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000A23BC File Offset: 0x000A05BC
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			if (this.CanOptimizeCommandExecution(callContext))
			{
				return new MoveItemBatch(callContext, this);
			}
			return new MoveItem(callContext, this);
		}

		// Token: 0x04001481 RID: 5249
		internal const string ElementName = "MoveItem";
	}
}
