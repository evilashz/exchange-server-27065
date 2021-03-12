using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000432 RID: 1074
	[XmlType(TypeName = "GetDiscoverySearchConfigurationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Name = "GetDiscoverySearchConfigurationRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetDiscoverySearchConfigurationRequest : BaseRequest
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x000A0DE7 File Offset: 0x0009EFE7
		// (set) Token: 0x06001F85 RID: 8069 RVA: 0x000A0DEF File Offset: 0x0009EFEF
		[XmlElement("SearchId")]
		[DataMember(Name = "SearchId", IsRequired = true)]
		public string SearchId
		{
			get
			{
				return this.searchId;
			}
			set
			{
				this.searchId = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x000A0DF8 File Offset: 0x0009EFF8
		// (set) Token: 0x06001F87 RID: 8071 RVA: 0x000A0E00 File Offset: 0x0009F000
		[DataMember(Name = "ExpandGroupMembership", IsRequired = false)]
		[XmlElement("ExpandGroupMembership")]
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

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x000A0E09 File Offset: 0x0009F009
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x000A0E11 File Offset: 0x0009F011
		[DataMember(Name = "InPlaceHoldConfigurationOnly", IsRequired = false)]
		[XmlElement("InPlaceHoldConfigurationOnly")]
		public bool InPlaceHoldConfigurationOnly
		{
			get
			{
				return this.inPlaceHoldConfigurationOnly;
			}
			set
			{
				this.inPlaceHoldConfigurationOnly = value;
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000A0E1A File Offset: 0x0009F01A
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetDiscoverySearchConfiguration(callContext, this);
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000A0E23 File Offset: 0x0009F023
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000A0E26 File Offset: 0x0009F026
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return null;
		}

		// Token: 0x040013E9 RID: 5097
		private string searchId;

		// Token: 0x040013EA RID: 5098
		private bool expandGroupMembership;

		// Token: 0x040013EB RID: 5099
		private bool inPlaceHoldConfigurationOnly;
	}
}
