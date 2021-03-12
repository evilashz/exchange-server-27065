using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200044C RID: 1100
	[XmlType(TypeName = "GetSearchableMailboxesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Name = "GetSearchableMailboxesRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetSearchableMailboxesRequest : BaseRequest
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x000A19D9 File Offset: 0x0009FBD9
		// (set) Token: 0x06002045 RID: 8261 RVA: 0x000A19E1 File Offset: 0x0009FBE1
		[DataMember(Name = "SearchFilter", IsRequired = false)]
		[XmlElement("SearchFilter")]
		public string SearchFilter
		{
			get
			{
				return this.searchFilter;
			}
			set
			{
				this.searchFilter = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x000A19EA File Offset: 0x0009FBEA
		// (set) Token: 0x06002047 RID: 8263 RVA: 0x000A19F2 File Offset: 0x0009FBF2
		[XmlElement("ExpandGroupMembership")]
		[DataMember(Name = "ExpandGroupMembership", IsRequired = false)]
		public bool ExpandGroupMembership
		{
			get
			{
				return this.expandGroupMembership;
			}
			set
			{
				this.expandGroupMembership = value;
			}
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000A19FB File Offset: 0x0009FBFB
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetSearchableMailboxes(callContext, this);
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000A1A04 File Offset: 0x0009FC04
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000A1A07 File Offset: 0x0009FC07
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return null;
		}

		// Token: 0x0400143A RID: 5178
		private string searchFilter;

		// Token: 0x0400143B RID: 5179
		private bool expandGroupMembership;
	}
}
