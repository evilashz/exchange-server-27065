using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B30 RID: 2864
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "PerformInstantSearchRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class PerformInstantSearchRequest : BaseRequest
	{
		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06005117 RID: 20759 RVA: 0x0010A2C3 File Offset: 0x001084C3
		// (set) Token: 0x06005118 RID: 20760 RVA: 0x0010A2CB File Offset: 0x001084CB
		[DataMember(IsRequired = false)]
		public string DeviceId { get; set; }

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06005119 RID: 20761 RVA: 0x0010A2D4 File Offset: 0x001084D4
		// (set) Token: 0x0600511A RID: 20762 RVA: 0x0010A2DC File Offset: 0x001084DC
		[DataMember(IsRequired = false)]
		public string ApplicationId { get; set; }

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x0600511B RID: 20763 RVA: 0x0010A2E5 File Offset: 0x001084E5
		// (set) Token: 0x0600511C RID: 20764 RVA: 0x0010A2ED File Offset: 0x001084ED
		[DataMember(IsRequired = true)]
		public string SearchSessionId { get; set; }

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x0600511D RID: 20765 RVA: 0x0010A2F6 File Offset: 0x001084F6
		// (set) Token: 0x0600511E RID: 20766 RVA: 0x0010A2FE File Offset: 0x001084FE
		[DataMember(IsRequired = true)]
		public InstantSearchItemType ItemType { get; set; }

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x0600511F RID: 20767 RVA: 0x0010A307 File Offset: 0x00108507
		// (set) Token: 0x06005120 RID: 20768 RVA: 0x0010A30F File Offset: 0x0010850F
		[DataMember(IsRequired = true)]
		public QueryOptionsType QueryOptions { get; set; }

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06005121 RID: 20769 RVA: 0x0010A318 File Offset: 0x00108518
		// (set) Token: 0x06005122 RID: 20770 RVA: 0x0010A320 File Offset: 0x00108520
		[DataMember(IsRequired = true)]
		public long SearchRequestId { get; set; }

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06005123 RID: 20771 RVA: 0x0010A329 File Offset: 0x00108529
		// (set) Token: 0x06005124 RID: 20772 RVA: 0x0010A331 File Offset: 0x00108531
		[DataMember(IsRequired = true)]
		public string KqlQuery { get; set; }

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06005125 RID: 20773 RVA: 0x0010A33A File Offset: 0x0010853A
		// (set) Token: 0x06005126 RID: 20774 RVA: 0x0010A342 File Offset: 0x00108542
		[DataMember(IsRequired = true)]
		public FolderId[] FolderScope { get; set; }

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06005127 RID: 20775 RVA: 0x0010A34B File Offset: 0x0010854B
		// (set) Token: 0x06005128 RID: 20776 RVA: 0x0010A353 File Offset: 0x00108553
		[DataMember]
		public int MaxSuggestionsCount { get; set; }

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06005129 RID: 20777 RVA: 0x0010A35C File Offset: 0x0010855C
		// (set) Token: 0x0600512A RID: 20778 RVA: 0x0010A364 File Offset: 0x00108564
		[DataMember]
		public int MaximumResultCount { get; set; }

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x0600512B RID: 20779 RVA: 0x0010A36D File Offset: 0x0010856D
		// (set) Token: 0x0600512C RID: 20780 RVA: 0x0010A375 File Offset: 0x00108575
		[Deprecated(ExchangeVersionType.V2_4)]
		[DataMember]
		[XmlIgnore]
		public PropertyPath[] RequestedRefiners { get; set; }

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x0600512D RID: 20781 RVA: 0x0010A37E File Offset: 0x0010857E
		// (set) Token: 0x0600512E RID: 20782 RVA: 0x0010A386 File Offset: 0x00108586
		[DataMember]
		public RefinementFilterType RefinementFilter { get; set; }

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x0600512F RID: 20783 RVA: 0x0010A38F File Offset: 0x0010858F
		// (set) Token: 0x06005130 RID: 20784 RVA: 0x0010A397 File Offset: 0x00108597
		[DataMember]
		public SuggestionSourceType SuggestionSources { get; set; }

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06005131 RID: 20785 RVA: 0x0010A3A0 File Offset: 0x001085A0
		// (set) Token: 0x06005132 RID: 20786 RVA: 0x0010A3A8 File Offset: 0x001085A8
		[DataMember]
		public RestrictionType DateRestriction { get; set; }

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06005133 RID: 20787 RVA: 0x0010A3B1 File Offset: 0x001085B1
		// (set) Token: 0x06005134 RID: 20788 RVA: 0x0010A3B9 File Offset: 0x001085B9
		[DataMember]
		public bool IsDeepTraversal { get; set; }

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06005135 RID: 20789 RVA: 0x0010A3C2 File Offset: 0x001085C2
		// (set) Token: 0x06005136 RID: 20790 RVA: 0x0010A3CA File Offset: 0x001085CA
		[DataMember]
		public bool WaitOnSearchResults { get; set; }

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06005137 RID: 20791 RVA: 0x0010A3D3 File Offset: 0x001085D3
		public bool IsWarmUpRequest
		{
			get
			{
				return string.IsNullOrEmpty(this.KqlQuery) && (this.QueryOptions & QueryOptionsType.Results) == QueryOptionsType.Results;
			}
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x0010A3EF File Offset: 0x001085EF
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new PerformInstantSearch(callContext, this);
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x0010A3F8 File Offset: 0x001085F8
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x0010A3FB File Offset: 0x001085FB
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x0010A3FE File Offset: 0x001085FE
		internal override void Validate()
		{
			base.Validate();
		}
	}
}
