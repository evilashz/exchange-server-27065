using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200045C RID: 1116
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetUserUnifiedGroupsRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUserUnifiedGroupsRequest : BaseRequest
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x000A1FFB File Offset: 0x000A01FB
		// (set) Token: 0x060020E4 RID: 8420 RVA: 0x000A2003 File Offset: 0x000A0203
		[DataMember(Name = "RequestedGroupsSets", IsRequired = false)]
		[XmlArrayItem("RequestedUnifiedGroupsSet", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArray("RequestedGroupsSets")]
		public RequestedUnifiedGroupsSet[] RequestedGroupsSets { get; set; }

		// Token: 0x060020E5 RID: 8421 RVA: 0x000A200C File Offset: 0x000A020C
		internal RequestedUnifiedGroupsSet[] ValidateParams()
		{
			if (this.RequestedGroupsSets == null || !this.RequestedGroupsSets.Any<RequestedUnifiedGroupsSet>())
			{
				return new RequestedUnifiedGroupsSet[]
				{
					new RequestedUnifiedGroupsSet
					{
						FilterType = UnifiedGroupsFilterType.All,
						SortType = UnifiedGroupsSortType.DisplayName,
						SortDirection = SortDirection.Ascending
					}
				};
			}
			if (this.RequestedGroupsSets.Length > 3)
			{
				throw new ServiceArgumentException((CoreResources.IDs)3784063568U, CoreResources.ErrorMaxRequestedUnifiedGroupsSetsExceeded);
			}
			return this.RequestedGroupsSets;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000A207C File Offset: 0x000A027C
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUserUnifiedGroups(callContext, this);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000A2085 File Offset: 0x000A0285
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000A2088 File Offset: 0x000A0288
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x04001469 RID: 5225
		private const int MaxRequestedGroupsSets = 3;
	}
}
