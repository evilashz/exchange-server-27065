using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000266 RID: 614
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "DistinguishedGroupByType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class DistinguishedGroupByType : GroupByType
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0004DBB5 File Offset: 0x0004BDB5
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x0004DBBD File Offset: 0x0004BDBD
		[IgnoreDataMember]
		[XmlElement(ElementName = "StandardGroupBy", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public StandardGroupBys StandardGroupBy
		{
			get
			{
				return this.standardGroupBy;
			}
			set
			{
				this.standardGroupBy = value;
				this.ConfigureGroupByProperties();
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0004DBCC File Offset: 0x0004BDCC
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x0004DBD9 File Offset: 0x0004BDD9
		[XmlIgnore]
		[DataMember(Name = "StandardGroupBy")]
		public string StandardGroupByString
		{
			get
			{
				return EnumUtilities.ToString<StandardGroupBys>(this.StandardGroupBy);
			}
			set
			{
				this.StandardGroupBy = EnumUtilities.Parse<StandardGroupBys>(value);
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0004DBE8 File Offset: 0x0004BDE8
		private void ConfigureGroupByProperties()
		{
			StandardGroupBys standardGroupBys = this.standardGroupBy;
			if (standardGroupBys != StandardGroupBys.ConversationTopic)
			{
				return;
			}
			base.AggregateOn = new AggregateOnType(DistinguishedGroupByType.conversationTopicAggregationProperty, AggregateType.Maximum);
			base.GroupByProperty = DistinguishedGroupByType.conversationTopicGroupByProperty;
		}

		// Token: 0x04000BFB RID: 3067
		private static PropertyPath conversationTopicAggregationProperty = new PropertyUri(PropertyUriEnum.DateTimeReceived);

		// Token: 0x04000BFC RID: 3068
		private static PropertyPath conversationTopicGroupByProperty = new PropertyUri(PropertyUriEnum.ConversationTopic);

		// Token: 0x04000BFD RID: 3069
		private StandardGroupBys standardGroupBy;
	}
}
