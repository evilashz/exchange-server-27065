using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E79 RID: 3705
	internal class ResponseStatus
	{
		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x0600605F RID: 24671 RVA: 0x0012CEA8 File Offset: 0x0012B0A8
		// (set) Token: 0x06006060 RID: 24672 RVA: 0x0012CEB0 File Offset: 0x0012B0B0
		public ResponseType Response { get; set; }

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06006061 RID: 24673 RVA: 0x0012CEB9 File Offset: 0x0012B0B9
		// (set) Token: 0x06006062 RID: 24674 RVA: 0x0012CEC1 File Offset: 0x0012B0C1
		public DateTimeOffset Time { get; set; }

		// Token: 0x04003444 RID: 13380
		internal static readonly LazyMember<EdmComplexType> EdmComplexType = new LazyMember<EdmComplexType>(delegate()
		{
			EdmComplexType edmComplexType = new EdmComplexType(typeof(ResponseStatus).Namespace, typeof(ResponseStatus).Name, Recipient.EdmComplexType.Member, false);
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Response", new EdmEnumTypeReference(EnumTypes.GetEdmEnumType(typeof(ResponseType)), true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Time", EdmCoreModel.Instance.GetDateTimeOffset(true)));
			return edmComplexType;
		});
	}
}
