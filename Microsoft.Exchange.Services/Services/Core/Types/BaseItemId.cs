using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006BD RID: 1725
	[XmlInclude(typeof(OccurrenceItemId))]
	[XmlInclude(typeof(RecurringMasterItemId))]
	[XmlInclude(typeof(RecurringMasterItemIdRanges))]
	[KnownType(typeof(RecurringMasterItemId))]
	[KnownType(typeof(RecurringMasterItemIdRanges))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlInclude(typeof(ItemId))]
	[XmlInclude(typeof(RootItemIdType))]
	[XmlType(TypeName = "BaseItemIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(ItemId))]
	[KnownType(typeof(OccurrenceItemId))]
	[KnownType(typeof(RootItemIdType))]
	public class BaseItemId : ServiceObjectId
	{
		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000BF7FE File Offset: 0x000BD9FE
		internal override BasicTypes BasicType
		{
			get
			{
				return BasicTypes.ItemOrAttachment;
			}
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000BF801 File Offset: 0x000BDA01
		public BaseItemId()
		{
			if (base.GetType() == typeof(BaseItemId))
			{
				throw new NotImplementedException("Don't call me!");
			}
		}
	}
}
