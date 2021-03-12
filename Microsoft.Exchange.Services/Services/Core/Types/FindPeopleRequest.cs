using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000424 RID: 1060
	[KnownType(typeof(IndexedPageView))]
	[XmlType(TypeName = "FindPeopleType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class FindPeopleRequest : BaseRequest
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x000A084F File Offset: 0x0009EA4F
		// (set) Token: 0x06001F02 RID: 7938 RVA: 0x000A0857 File Offset: 0x0009EA57
		[DataMember(Name = "PersonaShape", IsRequired = false)]
		public PersonaResponseShape PersonaShape { get; set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x000A0860 File Offset: 0x0009EA60
		// (set) Token: 0x06001F04 RID: 7940 RVA: 0x000A0868 File Offset: 0x0009EA68
		[DataMember(Name = "IndexedPageItemView", IsRequired = true)]
		[XmlElement("IndexedPageItemView", typeof(IndexedPageView))]
		public BasePagingType Paging { get; set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x000A0871 File Offset: 0x0009EA71
		// (set) Token: 0x06001F06 RID: 7942 RVA: 0x000A0879 File Offset: 0x0009EA79
		[XmlArrayItem("FieldOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(Name = "SortOrder", IsRequired = false)]
		public SortResults[] SortOrder { get; set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x000A0882 File Offset: 0x0009EA82
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x000A088A File Offset: 0x0009EA8A
		[DataMember(Name = "ParentFolderId", IsRequired = false)]
		[XmlElement("ParentFolderId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public TargetFolderId ParentFolderId { get; set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x000A0893 File Offset: 0x0009EA93
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x000A089B File Offset: 0x0009EA9B
		[XmlElement("Restriction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "Restriction", IsRequired = false)]
		public RestrictionType Restriction { get; set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x000A08A4 File Offset: 0x0009EAA4
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x000A08AC File Offset: 0x0009EAAC
		[XmlElement("AggregationRestriction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "AggregationRestriction", IsRequired = false)]
		public RestrictionType AggregationRestriction { get; set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x000A08B5 File Offset: 0x0009EAB5
		// (set) Token: 0x06001F0E RID: 7950 RVA: 0x000A08BD File Offset: 0x0009EABD
		[XmlElement("QueryString", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "QueryString", IsRequired = false)]
		public string QueryString { get; set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x000A08C6 File Offset: 0x0009EAC6
		// (set) Token: 0x06001F10 RID: 7952 RVA: 0x000A08CE File Offset: 0x0009EACE
		[XmlIgnore]
		[DataMember(Name = "ShouldResolveOneOffEmailAddress", IsRequired = false)]
		public bool ShouldResolveOneOffEmailAddress { get; set; }

		// Token: 0x06001F11 RID: 7953 RVA: 0x000A08D7 File Offset: 0x0009EAD7
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new FindPeople(callContext, this);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000A08E0 File Offset: 0x0009EAE0
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.ParentFolderId == null)
			{
				return IdConverter.GetServerInfoForCallContext(callContext);
			}
			return BaseRequest.GetServerInfoForFolderId(callContext, this.ParentFolderId.BaseFolderId);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000A0902 File Offset: 0x0009EB02
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
