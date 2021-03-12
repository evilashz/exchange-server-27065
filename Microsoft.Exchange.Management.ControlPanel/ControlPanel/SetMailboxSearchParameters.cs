using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003DD RID: 989
	[DataContract]
	public class SetMailboxSearchParameters : BaseMailboxSearchParameters
	{
		// Token: 0x06003306 RID: 13062 RVA: 0x0009E468 File Offset: 0x0009C668
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.SearchAllDates != null)
			{
				if (this.SearchAllDates.Value)
				{
					base[SearchObjectSchema.StartDate] = null;
					base[SearchObjectSchema.EndDate] = null;
				}
				else if (string.IsNullOrEmpty(this.SearchStartDate) && string.IsNullOrEmpty(this.SearchEndDate))
				{
					throw new FaultException(Strings.MailboxSearchSpecifySearchDate);
				}
			}
			if (this.SearchAllMailboxes != null && this.SearchAllMailboxes.Value)
			{
				base[SearchObjectSchema.SourceMailboxes] = null;
			}
			else if (base.ParameterIsSpecified(SearchObjectSchema.SourceMailboxes.ToString()) && this.SourceMailboxes.IsNullOrEmpty())
			{
				throw new FaultException(Strings.MailboxSearchSpecifySourceMailboxesError);
			}
			if (base.EstimateOnly)
			{
				base.ExcludeDuplicateMessages = false;
				base[SearchObjectSchema.LogLevel] = LoggingLevel.Suppress;
			}
		}

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x0009E556 File Offset: 0x0009C756
		// (set) Token: 0x06003308 RID: 13064 RVA: 0x0009E55E File Offset: 0x0009C75E
		[DataMember]
		public bool? SearchAllMailboxes { get; set; }

		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x06003309 RID: 13065 RVA: 0x0009E567 File Offset: 0x0009C767
		// (set) Token: 0x0600330A RID: 13066 RVA: 0x0009E56F File Offset: 0x0009C76F
		[DataMember]
		public bool? SearchAllDates { get; set; }

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x0600330B RID: 13067 RVA: 0x0009E578 File Offset: 0x0009C778
		// (set) Token: 0x0600330C RID: 13068 RVA: 0x0009E58A File Offset: 0x0009C78A
		[DataMember]
		public string SearchStartDate
		{
			get
			{
				return base[SearchObjectSchema.StartDate].ToStringWithNull();
			}
			set
			{
				base[SearchObjectSchema.StartDate] = value.ToEcpExDateTime();
			}
		}

		// Token: 0x17002003 RID: 8195
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x0009E5A2 File Offset: 0x0009C7A2
		// (set) Token: 0x0600330E RID: 13070 RVA: 0x0009E5B4 File Offset: 0x0009C7B4
		[DataMember]
		public string SearchEndDate
		{
			get
			{
				return base[SearchObjectSchema.EndDate].ToStringWithNull();
			}
			set
			{
				base[SearchObjectSchema.EndDate] = value.ToEcpExDateTime();
			}
		}

		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x0009E5CC File Offset: 0x0009C7CC
		// (set) Token: 0x06003310 RID: 13072 RVA: 0x0009E5DE File Offset: 0x0009C7DE
		[DataMember]
		public Identity[] SourceMailboxes
		{
			get
			{
				return Identity.FromIdParameters(base[SearchObjectSchema.SourceMailboxes]);
			}
			set
			{
				base[SearchObjectSchema.SourceMailboxes] = value.ToIdParameters();
			}
		}

		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x0009E5F1 File Offset: 0x0009C7F1
		// (set) Token: 0x06003312 RID: 13074 RVA: 0x0009E60D File Offset: 0x0009C80D
		[DataMember]
		public bool IncludeKeywordStatistics
		{
			get
			{
				return (bool)(base[SearchObjectSchema.IncludeKeywordStatistics] ?? false);
			}
			set
			{
				base[SearchObjectSchema.IncludeKeywordStatistics] = value;
			}
		}

		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x0009E620 File Offset: 0x0009C820
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-MailboxSearch";
			}
		}

		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x0009E627 File Offset: 0x0009C827
		public override string RbacScope
		{
			get
			{
				return string.Empty;
			}
		}
	}
}
