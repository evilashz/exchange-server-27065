using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000053 RID: 83
	[KnownType(typeof(SizeRestrictionData))]
	[KnownType(typeof(AttachmentRestrictionData))]
	[KnownType(typeof(CommentRestrictionData))]
	[KnownType(typeof(TrueRestrictionData))]
	[KnownType(typeof(FalseRestrictionData))]
	[KnownType(typeof(AndRestrictionData))]
	[DataContract]
	[KnownType(typeof(NotRestrictionData))]
	[KnownType(typeof(RecipientRestrictionData))]
	[KnownType(typeof(CountRestrictionData))]
	[KnownType(typeof(PropertyRestrictionData))]
	[KnownType(typeof(OrRestrictionData))]
	[KnownType(typeof(ContentRestrictionData))]
	[KnownType(typeof(BitMaskRestrictionData))]
	[KnownType(typeof(ComparePropertyRestrictionData))]
	[KnownType(typeof(ExistRestrictionData))]
	internal abstract class HierRestrictionData : RestrictionData
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x00007F79 File Offset: 0x00006179
		public HierRestrictionData()
		{
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00007F81 File Offset: 0x00006181
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x00007F89 File Offset: 0x00006189
		[DataMember(Name = "restrictions")]
		public RestrictionData[] Restrictions { get; set; }

		// Token: 0x06000436 RID: 1078 RVA: 0x00007F94 File Offset: 0x00006194
		internal void ParseRestrictions(params Restriction[] rest)
		{
			this.Restrictions = new RestrictionData[rest.Length];
			for (int i = 0; i < rest.Length; i++)
			{
				this.Restrictions[i] = RestrictionData.GetRestrictionData(rest[i]);
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00007FD0 File Offset: 0x000061D0
		internal void ParseQueryFilters(StoreSession storeSession, ReadOnlyCollection<QueryFilter> queryFilters)
		{
			this.Restrictions = new RestrictionData[queryFilters.Count];
			for (int i = 0; i < queryFilters.Count; i++)
			{
				this.Restrictions[i] = RestrictionData.GetRestrictionData(storeSession, queryFilters[i]);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00008014 File Offset: 0x00006214
		internal void ParseQueryFilter(StoreSession storeSession, QueryFilter queryFilter)
		{
			this.Restrictions = new RestrictionData[]
			{
				RestrictionData.GetRestrictionData(storeSession, queryFilter)
			};
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000803C File Offset: 0x0000623C
		internal Restriction[] GetRestrictions()
		{
			Restriction[] array = new Restriction[this.Restrictions.Length];
			for (int i = 0; i < this.Restrictions.Length; i++)
			{
				array[i] = this.Restrictions[i].GetRestriction();
			}
			return array;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000807C File Offset: 0x0000627C
		internal QueryFilter[] GetQueryFilters(StoreSession storeSession)
		{
			QueryFilter[] array = new QueryFilter[this.Restrictions.Length];
			for (int i = 0; i < this.Restrictions.Length; i++)
			{
				array[i] = this.Restrictions[i].GetQueryFilter(storeSession);
			}
			return array;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000080CD File Offset: 0x000062CD
		internal string ConcatSubRestrictions(string prefix)
		{
			return string.Format("{0}{1}", prefix, CommonUtils.ConcatEntries<RestrictionData>(this.Restrictions, delegate(RestrictionData rd)
			{
				if (rd != null)
				{
					return rd.ToStringInternal();
				}
				return "(null)";
			}));
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00008104 File Offset: 0x00006304
		internal override int GetApproximateSize()
		{
			int num = base.GetApproximateSize();
			foreach (RestrictionData restrictionData in this.Restrictions)
			{
				num += restrictionData.GetApproximateSize();
			}
			return num;
		}
	}
}
