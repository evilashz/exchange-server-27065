using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000051 RID: 81
	[KnownType(typeof(FalseRestrictionData))]
	[KnownType(typeof(PropertyRestrictionData))]
	[KnownType(typeof(BitMaskRestrictionData))]
	[KnownType(typeof(ComparePropertyRestrictionData))]
	[KnownType(typeof(ExistRestrictionData))]
	[KnownType(typeof(CountRestrictionData))]
	[KnownType(typeof(AttachmentRestrictionData))]
	[KnownType(typeof(RecipientRestrictionData))]
	[KnownType(typeof(CommentRestrictionData))]
	[KnownType(typeof(TrueRestrictionData))]
	[KnownType(typeof(NotRestrictionData))]
	[KnownType(typeof(NullRestrictionData))]
	[KnownType(typeof(ContentRestrictionData))]
	[KnownType(typeof(SizeRestrictionData))]
	[KnownType(typeof(NearRestrictionData))]
	[DataContract]
	[KnownType(typeof(AndRestrictionData))]
	[KnownType(typeof(OrRestrictionData))]
	internal abstract class RestrictionData
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x000078DC File Offset: 0x00005ADC
		public RestrictionData()
		{
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000078E4 File Offset: 0x00005AE4
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x000078EC File Offset: 0x00005AEC
		[DataMember(EmitDefaultValue = false)]
		public int LCID { get; set; }

		// Token: 0x0600041C RID: 1052 RVA: 0x000078F8 File Offset: 0x00005AF8
		static RestrictionData()
		{
			foreach (KeyValuePair<int, int> keyValuePair in RestrictionData.ComparisonOperatorToRelOp)
			{
				RestrictionData.RelOpToComparisonOperator.Add(keyValuePair.Value, keyValuePair.Key);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000079B4 File Offset: 0x00005BB4
		public override string ToString()
		{
			if (this.LCID != 0)
			{
				return string.Format("Restriction: LCID=0x{0:X}, {1}", this.LCID, this.ToStringInternal());
			}
			return string.Format("Restriction: {0}", this.ToStringInternal());
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000079EC File Offset: 0x00005BEC
		internal static RestrictionData GetRestrictionData(Restriction restriction)
		{
			if (restriction is Restriction.AndRestriction)
			{
				return new AndRestrictionData((Restriction.AndRestriction)restriction);
			}
			if (restriction is Restriction.OrRestriction)
			{
				return new OrRestrictionData((Restriction.OrRestriction)restriction);
			}
			if (restriction is Restriction.NotRestriction)
			{
				return new NotRestrictionData((Restriction.NotRestriction)restriction);
			}
			if (restriction is Restriction.CountRestriction)
			{
				return new CountRestrictionData((Restriction.CountRestriction)restriction);
			}
			if (restriction is Restriction.PropertyRestriction)
			{
				return new PropertyRestrictionData((Restriction.PropertyRestriction)restriction);
			}
			if (restriction is Restriction.ContentRestriction)
			{
				return new ContentRestrictionData((Restriction.ContentRestriction)restriction);
			}
			if (restriction is Restriction.BitMaskRestriction)
			{
				return new BitMaskRestrictionData((Restriction.BitMaskRestriction)restriction);
			}
			if (restriction is Restriction.ComparePropertyRestriction)
			{
				return new ComparePropertyRestrictionData((Restriction.ComparePropertyRestriction)restriction);
			}
			if (restriction is Restriction.ExistRestriction)
			{
				return new ExistRestrictionData((Restriction.ExistRestriction)restriction);
			}
			if (restriction is Restriction.SizeRestriction)
			{
				return new SizeRestrictionData((Restriction.SizeRestriction)restriction);
			}
			if (restriction is Restriction.AttachmentRestriction)
			{
				return new AttachmentRestrictionData((Restriction.AttachmentRestriction)restriction);
			}
			if (restriction is Restriction.RecipientRestriction)
			{
				return new RecipientRestrictionData((Restriction.RecipientRestriction)restriction);
			}
			if (restriction is Restriction.CommentRestriction)
			{
				return new CommentRestrictionData((Restriction.CommentRestriction)restriction);
			}
			if (restriction is Restriction.TrueRestriction)
			{
				return new TrueRestrictionData();
			}
			if (restriction is Restriction.FalseRestriction)
			{
				return new FalseRestrictionData();
			}
			if (restriction is Restriction.NearRestriction)
			{
				return new NearRestrictionData((Restriction.NearRestriction)restriction);
			}
			if (restriction == null)
			{
				return new TrueRestrictionData();
			}
			string type = restriction.GetType().ToString();
			throw new UnknownRestrictionTypeException(type);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00007B48 File Offset: 0x00005D48
		internal static RestrictionData GetRestrictionData(StoreSession storeSession, QueryFilter queryFilter)
		{
			if (queryFilter is AndFilter)
			{
				return new AndRestrictionData(storeSession, (AndFilter)queryFilter);
			}
			if (queryFilter is OrFilter)
			{
				return new OrRestrictionData(storeSession, (OrFilter)queryFilter);
			}
			if (queryFilter is NotFilter)
			{
				return new NotRestrictionData(storeSession, (NotFilter)queryFilter);
			}
			if (queryFilter is CountFilter)
			{
				return new CountRestrictionData(storeSession, (CountFilter)queryFilter);
			}
			if (queryFilter is ComparisonFilter)
			{
				return new PropertyRestrictionData(storeSession, (ComparisonFilter)queryFilter);
			}
			if (queryFilter is ContentFilter)
			{
				return new ContentRestrictionData(storeSession, (ContentFilter)queryFilter);
			}
			if (queryFilter is BitMaskFilter)
			{
				return new BitMaskRestrictionData(storeSession, (BitMaskFilter)queryFilter);
			}
			if (queryFilter is PropertyComparisonFilter)
			{
				return new ComparePropertyRestrictionData(storeSession, (PropertyComparisonFilter)queryFilter);
			}
			if (queryFilter is ExistsFilter)
			{
				return new ExistRestrictionData(storeSession, (ExistsFilter)queryFilter);
			}
			if (queryFilter is SizeFilter)
			{
				return new SizeRestrictionData(storeSession, (SizeFilter)queryFilter);
			}
			if (queryFilter is SubFilter && ((SubFilter)queryFilter).SubFilterProperty == SubFilterProperties.Attachments)
			{
				return new AttachmentRestrictionData(storeSession, (SubFilter)queryFilter);
			}
			if (queryFilter is SubFilter && ((SubFilter)queryFilter).SubFilterProperty == SubFilterProperties.Recipients)
			{
				return new RecipientRestrictionData(storeSession, (SubFilter)queryFilter);
			}
			if (queryFilter is CommentFilter)
			{
				return new CommentRestrictionData(storeSession, (CommentFilter)queryFilter);
			}
			if (queryFilter is TrueFilter)
			{
				return new TrueRestrictionData();
			}
			if (queryFilter is FalseFilter)
			{
				return new FalseRestrictionData();
			}
			if (queryFilter is NearFilter)
			{
				return new NearRestrictionData(storeSession, (NearFilter)queryFilter);
			}
			if (queryFilter is NullFilter || queryFilter == null)
			{
				return new TrueRestrictionData();
			}
			string type = queryFilter.GetType().ToString();
			throw new UnknownRestrictionTypeException(type);
		}

		// Token: 0x06000420 RID: 1056
		internal abstract Restriction GetRestriction();

		// Token: 0x06000421 RID: 1057
		internal abstract QueryFilter GetQueryFilter(StoreSession storeSession);

		// Token: 0x06000422 RID: 1058
		internal abstract string ToStringInternal();

		// Token: 0x06000423 RID: 1059 RVA: 0x00007CD8 File Offset: 0x00005ED8
		internal void Enumerate(RestrictionData.EnumRestrictionDelegate del)
		{
			del(this);
			HierRestrictionData hierRestrictionData = this as HierRestrictionData;
			if (hierRestrictionData != null)
			{
				foreach (RestrictionData restrictionData in hierRestrictionData.Restrictions)
				{
					if (restrictionData == null)
					{
						throw new CorruptRestrictionDataException();
					}
					restrictionData.Enumerate(del);
				}
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00007D1F File Offset: 0x00005F1F
		internal virtual void InternalEnumPropTags(CommonUtils.EnumPropTagDelegate del)
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00007D21 File Offset: 0x00005F21
		internal virtual void InternalEnumPropValues(CommonUtils.EnumPropValueDelegate del)
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00007D3C File Offset: 0x00005F3C
		internal void EnumeratePropTags(CommonUtils.EnumPropTagDelegate del)
		{
			this.Enumerate(delegate(RestrictionData r)
			{
				r.InternalEnumPropTags(del);
			});
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00007D80 File Offset: 0x00005F80
		internal void EnumeratePropValues(CommonUtils.EnumPropValueDelegate del)
		{
			this.Enumerate(delegate(RestrictionData r)
			{
				r.InternalEnumPropValues(del);
			});
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00007DAC File Offset: 0x00005FAC
		internal virtual int GetApproximateSize()
		{
			return 4;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00007DB0 File Offset: 0x00005FB0
		protected int GetPropTagFromDefinition(StoreSession storeSession, PropertyDefinition definition)
		{
			uint[] array = new uint[1];
			PropertyTagCache.Cache.PropertyTagsFromPropertyDefinitions(storeSession, new List<NativeStorePropertyDefinition>
			{
				(NativeStorePropertyDefinition)definition
			}).CopyTo(array, 0);
			return (int)array[0];
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00007DEC File Offset: 0x00005FEC
		protected int GetRelOpFromComparisonOperator(ComparisonOperator comparisonOperator)
		{
			int result;
			if (!RestrictionData.ComparisonOperatorToRelOp.TryGetValue((int)comparisonOperator, out result))
			{
				MrsTracer.Common.Error("Cannot convert comparison operator '{0}' to relop.", new object[]
				{
					comparisonOperator
				});
				throw new CorruptRestrictionDataException();
			}
			return result;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00007E30 File Offset: 0x00006030
		protected ComparisonOperator GetComparisonOperatorFromRelOp(int relOp)
		{
			int result;
			if (!RestrictionData.RelOpToComparisonOperator.TryGetValue(relOp, out result))
			{
				MrsTracer.Common.Error("Cannot convert relop '{0}' to comparison operator.", new object[]
				{
					relOp
				});
				throw new CorruptRestrictionDataException();
			}
			return (ComparisonOperator)result;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00007E74 File Offset: 0x00006074
		protected NativeStorePropertyDefinition GetPropertyDefinitionFromPropTag(StoreSession storeSession, int propTag)
		{
			return MapiUtils.ConvertPropTagsToDefinitions(storeSession, new PropTag[]
			{
				(PropTag)propTag
			})[0];
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00007E98 File Offset: 0x00006098
		protected ContentFlags GetContentFlags(MatchFlags matchFlags, MatchOptions matchOptions)
		{
			ContentFlags contentFlags;
			switch (matchOptions)
			{
			case MatchOptions.SubString:
				contentFlags = ContentFlags.SubString;
				goto IL_34;
			case MatchOptions.Prefix:
				contentFlags = ContentFlags.Prefix;
				goto IL_34;
			case MatchOptions.PrefixOnWords:
				contentFlags = ContentFlags.PrefixOnWords;
				goto IL_34;
			case MatchOptions.ExactPhrase:
				contentFlags = ContentFlags.ExactPhrase;
				goto IL_34;
			}
			contentFlags = ContentFlags.FullString;
			IL_34:
			if ((matchFlags & MatchFlags.IgnoreCase) != MatchFlags.Default)
			{
				contentFlags |= ContentFlags.IgnoreCase;
			}
			if ((matchFlags & MatchFlags.IgnoreNonSpace) != MatchFlags.Default)
			{
				contentFlags |= ContentFlags.IgnoreNonSpace;
			}
			if ((matchFlags & MatchFlags.Loose) != MatchFlags.Default)
			{
				contentFlags |= ContentFlags.Loose;
			}
			return contentFlags;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00007F04 File Offset: 0x00006104
		protected void GetMatchFlagsAndOptions(ContentFlags contentFlags, out MatchFlags matchFlags, out MatchOptions matchOptions)
		{
			ContentFlags contentFlags2 = contentFlags & (ContentFlags)51;
			switch (contentFlags2)
			{
			case ContentFlags.SubString:
				matchOptions = MatchOptions.SubString;
				break;
			case ContentFlags.Prefix:
				matchOptions = MatchOptions.Prefix;
				break;
			default:
				if (contentFlags2 != ContentFlags.PrefixOnWords)
				{
					if (contentFlags2 != ContentFlags.ExactPhrase)
					{
						matchOptions = MatchOptions.FullString;
					}
					else
					{
						matchOptions = MatchOptions.ExactPhrase;
					}
				}
				else
				{
					matchOptions = MatchOptions.PrefixOnWords;
				}
				break;
			}
			matchFlags = MatchFlags.Default;
			if ((contentFlags & ContentFlags.IgnoreCase) != ContentFlags.FullString)
			{
				matchFlags |= MatchFlags.IgnoreCase;
			}
			if ((contentFlags & ContentFlags.IgnoreNonSpace) != ContentFlags.FullString)
			{
				matchFlags |= MatchFlags.IgnoreNonSpace;
			}
			if ((contentFlags & ContentFlags.Loose) != ContentFlags.FullString)
			{
				matchFlags |= MatchFlags.Loose;
			}
		}

		// Token: 0x04000310 RID: 784
		private static readonly Dictionary<int, int> ComparisonOperatorToRelOp = new Dictionary<int, int>
		{
			{
				0,
				4
			},
			{
				1,
				5
			},
			{
				2,
				0
			},
			{
				3,
				1
			},
			{
				4,
				2
			},
			{
				5,
				3
			},
			{
				6,
				6
			},
			{
				7,
				100
			}
		};

		// Token: 0x04000311 RID: 785
		private static readonly Dictionary<int, int> RelOpToComparisonOperator = new Dictionary<int, int>();

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x06000430 RID: 1072
		internal delegate void EnumRestrictionDelegate(RestrictionData rd);
	}
}
