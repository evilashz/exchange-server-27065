using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003B9 RID: 953
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class QueryResumptionPoint<TSortKey> where TSortKey : IEquatable<TSortKey>
	{
		// Token: 0x06002B86 RID: 11142 RVA: 0x000AD9B7 File Offset: 0x000ABBB7
		protected QueryResumptionPoint(byte[] instanceKey, PropertyDefinition sortKey, TSortKey sortKeyValue, bool hasSortKeyValue)
		{
			Util.ThrowOnNullArgument(sortKey, "sortKey");
			this.instanceKey = instanceKey;
			this.sortKey = sortKey;
			this.sortKeyValue = sortKeyValue;
			this.hasSortKeyValue = hasSortKeyValue;
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x000AD9E7 File Offset: 0x000ABBE7
		public string Version
		{
			get
			{
				return QueryResumptionPoint<TSortKey>.GetVersion(this.MinorVersion);
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x000AD9F4 File Offset: 0x000ABBF4
		public virtual bool IsEmpty
		{
			get
			{
				return this.instanceKey == null;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x000AD9FF File Offset: 0x000ABBFF
		public byte[] InstanceKey
		{
			get
			{
				return this.instanceKey;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x000ADA07 File Offset: 0x000ABC07
		public TSortKey SortKeyValue
		{
			get
			{
				return this.sortKeyValue;
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000ADA10 File Offset: 0x000ABC10
		public bool TryResume(QueryResult result, int sortKeyIndex, SeekReference reference, int rowCountToFetch, out object[][] rows)
		{
			Util.ThrowOnNullArgument(result, "result");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(sortKeyIndex, 0, "sortKeyIndex");
			EnumValidator.ThrowIfInvalid<SeekReference>(reference, "reference");
			if (rowCountToFetch <= 0)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<int>((long)this.GetHashCode(), "QueryResumptionPoint::TryResume. Unable to seek to the resumption location. (rowCountToFetch: {0})", rowCountToFetch);
				rows = null;
				return false;
			}
			rows = null;
			bool flag = false;
			if (this.instanceKey != null)
			{
				SeekReference seekReference = reference & SeekReference.SeekBackward;
				ComparisonFilter seekFilter = null;
				if (this.hasSortKeyValue)
				{
					ComparisonOperator comparisonOperator = (seekReference == SeekReference.SeekBackward) ? ComparisonOperator.LessThanOrEqual : ComparisonOperator.GreaterThanOrEqual;
					seekFilter = new ComparisonFilter(comparisonOperator, this.sortKey, this.sortKeyValue);
				}
				if (result.SeekToCondition(reference, new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.InstanceKey, this.instanceKey)))
				{
					ExTraceGlobals.StorageTracer.TraceDebug<byte[]>((long)this.GetHashCode(), "QueryResumptionPoint::TryResume. Successfully seeked to the resumption location (InstanceKey: {0}).", this.instanceKey);
					rows = result.GetRows(rowCountToFetch);
					object obj = rows[0][sortKeyIndex];
					if (!this.hasSortKeyValue)
					{
						if (obj is PropertyError)
						{
							flag = true;
						}
					}
					else if (!(obj is TSortKey))
					{
						ExTraceGlobals.StorageTracer.TraceDebug<string, TSortKey>((long)this.GetHashCode(), "QueryResumptionPoint::TryResume. Unable to seek to the resumption location. Will restart from beginning. {0}: ({0})", this.sortKey.Name, this.sortKeyValue);
					}
					else
					{
						TSortKey tsortKey = (TSortKey)((object)obj);
						if (tsortKey.Equals(this.sortKeyValue))
						{
							flag = true;
						}
						else
						{
							ExTraceGlobals.StorageTracer.TraceDebug<string, TSortKey>((long)this.GetHashCode(), "QueryResumptionPoint::TryResume. Unable to seek to the resumption location. Will restart from beginning. {0}: ({0})", this.sortKey.Name, this.sortKeyValue);
						}
					}
				}
				else if (this.hasSortKeyValue)
				{
					if (result.SeekToCondition(reference, seekFilter))
					{
						rows = result.GetRows(rowCountToFetch);
						flag = true;
					}
					else
					{
						ExTraceGlobals.StorageTracer.TraceDebug<string, TSortKey>((long)this.GetHashCode(), "QueryResumptionPoint::TryResume. Unable to seek to the resumption location. Will restart from beginning. {0}: ({0})", this.sortKey.Name, this.sortKeyValue);
					}
				}
				else
				{
					ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "QueryResumptionPoint::TryResume. No sort key specified. Will restart from beginning.");
				}
			}
			if (flag)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, TSortKey>((long)this.GetHashCode(), "QueryResumptionPoint::TryResume. Successfully seeked to the resumption location ({0}: {1}).", this.sortKey.Name, this.sortKeyValue);
			}
			else
			{
				rows = null;
			}
			return flag;
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06002B8C RID: 11148
		protected abstract string MinorVersion { get; }

		// Token: 0x06002B8D RID: 11149 RVA: 0x000ADC1E File Offset: 0x000ABE1E
		protected static string GetVersion(string minor)
		{
			return string.Format("{0}.{1}", "1", minor);
		}

		// Token: 0x04001855 RID: 6229
		private const string CurrentMajorVersion = "1";

		// Token: 0x04001856 RID: 6230
		private readonly PropertyDefinition sortKey;

		// Token: 0x04001857 RID: 6231
		private readonly byte[] instanceKey;

		// Token: 0x04001858 RID: 6232
		private readonly TSortKey sortKeyValue;

		// Token: 0x04001859 RID: 6233
		private readonly bool hasSortKeyValue;
	}
}
