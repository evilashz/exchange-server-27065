using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200047C RID: 1148
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ForwardOnlyFilteredReader : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003338 RID: 13112 RVA: 0x000D034C File Offset: 0x000CE54C
		protected ForwardOnlyFilteredReader(ForwardOnlyFilteredReader.PropertySetMixer propertySets, bool usePrefetchMode)
		{
			this.allPropertySets = propertySets;
			this.usePrefetchMode = usePrefetchMode;
			this.queryPropertySets = new ForwardOnlyFilteredReader.PropertySetMixer(new Predicate<PropertyDefinition>(this.ShouldIntercept));
			this.queryPropertySets.MigrateSets(this.allPropertySets, new ForwardOnlyFilteredReader.PropertySet[]
			{
				ForwardOnlyFilteredReader.PropertySet.Identification,
				ForwardOnlyFilteredReader.PropertySet.ForFilter
			});
			if (this.usePrefetchMode)
			{
				this.queryPropertySets.MigrateSets(this.allPropertySets, new ForwardOnlyFilteredReader.PropertySet[]
				{
					ForwardOnlyFilteredReader.PropertySet.Requested
				});
			}
			else
			{
				this.queryPropertySets.AddSet(ForwardOnlyFilteredReader.PropertySet.Requested, this.allPropertySets.GetSet(ForwardOnlyFilteredReader.PropertySet.Identification));
				this.populationPropertySets = new ForwardOnlyFilteredReader.PropertySetMixer(new Predicate<PropertyDefinition>(this.ShouldIntercept));
				this.populationPropertySets.MigrateSets(this.allPropertySets, new ForwardOnlyFilteredReader.PropertySet[]
				{
					ForwardOnlyFilteredReader.PropertySet.Identification,
					ForwardOnlyFilteredReader.PropertySet.Requested
				});
			}
			this.query = new LazilyInitialized<QueryResult>(() => this.MakeQuery(this.queryPropertySets.GetFilteredMergedSet()));
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000D0478 File Offset: 0x000CE678
		internal virtual object[][] GetNextAsView(int rowcount)
		{
			return this.GetNextAsView(delegate(object[] transformedForFilterRow)
			{
				if (transformedForFilterRow != null)
				{
					return rowcount-- > 0;
				}
				return rowcount > 0;
			});
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000D04A8 File Offset: 0x000CE6A8
		internal virtual object[][] GetNextAsView(ForwardOnlyFilteredReader.StatefulRowPredicate fetchWhile)
		{
			List<object[]> list = new List<object[]>();
			if (fetchWhile == null)
			{
				fetchWhile = ((object[] forFilterRow) => true);
			}
			while (this.InternalGetNextAsView(list, fetchWhile))
			{
			}
			if (!this.usePrefetchMode)
			{
				this.PopulateRows(list);
			}
			return list.ToArray();
		}

		// Token: 0x0600333B RID: 13115
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x0600333C RID: 13116 RVA: 0x000D04FC File Offset: 0x000CE6FC
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000D0511 File Offset: 0x000CE711
		public virtual void Dispose()
		{
			if (this.query.IsInitialized)
			{
				this.query.Value.Dispose();
			}
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x000D0543 File Offset: 0x000CE743
		protected QueryResult Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000D0550 File Offset: 0x000CE750
		protected ForwardOnlyFilteredReader.PropertySetMixer PropertySets
		{
			get
			{
				return this.allPropertySets;
			}
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000D0558 File Offset: 0x000CE758
		protected virtual bool EvaluateFilterCriteria(object[] forFilterRow)
		{
			return true;
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000D055C File Offset: 0x000CE75C
		protected virtual object[][] TransformRow(object[] unfilteredRow)
		{
			if (!this.EvaluateFilterCriteria(this.PropertySets.FilterRow(unfilteredRow, ForwardOnlyFilteredReader.PropertySet.ForFilter)))
			{
				return null;
			}
			return new object[][]
			{
				unfilteredRow
			};
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000D058C File Offset: 0x000CE78C
		protected bool InternalGetNextAsView(IList<object[]> results, ForwardOnlyFilteredReader.StatefulRowPredicate fetchWhile)
		{
			bool flag = fetchWhile(null);
			if (!flag)
			{
				return false;
			}
			object[][] rows = this.Query.GetRows(int.MaxValue);
			int num = 0;
			if (rows.Length == 0)
			{
				return false;
			}
			foreach (object[] filteredRow in rows)
			{
				object[] unfilteredRow = this.queryPropertySets.RemitFilteredOffProperties(filteredRow);
				num++;
				foreach (object[] unfilteredRow2 in this.TransformRow(unfilteredRow) ?? Array<object[]>.Empty)
				{
					flag = fetchWhile(this.queryPropertySets.FilterRow(unfilteredRow2, ForwardOnlyFilteredReader.PropertySet.ForFilter));
					if (!flag)
					{
						break;
					}
					results.Add(this.queryPropertySets.FilterRow(unfilteredRow2, ForwardOnlyFilteredReader.PropertySet.Requested));
				}
			}
			this.Query.SeekToOffset(SeekReference.OriginCurrent, num - rows.Length);
			return flag;
		}

		// Token: 0x06003343 RID: 13123
		protected abstract QueryResult MakeQuery(params PropertyDefinition[] propertiesToReturn);

		// Token: 0x06003344 RID: 13124 RVA: 0x000D065C File Offset: 0x000CE85C
		private void PopulateRows(IList<object[]> rows)
		{
			PropertyDefinition[] set = this.allPropertySets.GetSet(ForwardOnlyFilteredReader.PropertySet.Identification);
			using (QueryResult queryResult = this.MakeQuery(this.populationPropertySets.GetFilteredMergedSet()))
			{
				for (int i = 0; i < rows.Count; i++)
				{
					ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "PopulateRows: old row index is {0}", queryResult.CurrentRow);
					queryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
					queryResult.SeekToCondition(SeekReference.OriginCurrent, new ComparisonFilter(ComparisonOperator.Equal, set[0], this.populationPropertySets.GetProperties(rows[i], new PropertyDefinition[]
					{
						set[0]
					})[0]));
					object[][] rows2 = queryResult.GetRows(1);
					if (rows2.Length == 0)
					{
						throw new ObjectNotFoundException(ServerStrings.ExItemDeletedInRace);
					}
					rows[i] = this.populationPropertySets.FilterRow(this.populationPropertySets.RemitFilteredOffProperties(rows2[0]), ForwardOnlyFilteredReader.PropertySet.Requested);
					ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "PopulateRows: new row index is {0}", queryResult.CurrentRow);
				}
			}
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000D076C File Offset: 0x000CE96C
		protected virtual bool ShouldIntercept(PropertyDefinition property)
		{
			return property is SimpleVirtualPropertyDefinition;
		}

		// Token: 0x04001B9F RID: 7071
		private readonly ForwardOnlyFilteredReader.PropertySetMixer allPropertySets;

		// Token: 0x04001BA0 RID: 7072
		private LazilyInitialized<QueryResult> query;

		// Token: 0x04001BA1 RID: 7073
		private readonly ForwardOnlyFilteredReader.PropertySetMixer queryPropertySets;

		// Token: 0x04001BA2 RID: 7074
		private readonly ForwardOnlyFilteredReader.PropertySetMixer populationPropertySets;

		// Token: 0x04001BA3 RID: 7075
		private readonly bool usePrefetchMode;

		// Token: 0x04001BA4 RID: 7076
		private readonly DisposeTracker disposeTracker;

		// Token: 0x0200047D RID: 1149
		protected enum PropertySet
		{
			// Token: 0x04001BA7 RID: 7079
			Identification,
			// Token: 0x04001BA8 RID: 7080
			ForFilter,
			// Token: 0x04001BA9 RID: 7081
			Requested
		}

		// Token: 0x02000483 RID: 1155
		protected sealed class PropertySetMixer : PropertySetMixer<PropertyDefinition, ForwardOnlyFilteredReader.PropertySet>
		{
			// Token: 0x0600335E RID: 13150 RVA: 0x000D0CD8 File Offset: 0x000CEED8
			public PropertySetMixer()
			{
			}

			// Token: 0x0600335F RID: 13151 RVA: 0x000D0CE0 File Offset: 0x000CEEE0
			public PropertySetMixer(Predicate<PropertyDefinition> shouldIntercept) : base(shouldIntercept)
			{
			}

			// Token: 0x06003360 RID: 13152 RVA: 0x000D0CE9 File Offset: 0x000CEEE9
			public void DeleteProperty(object[] unfilteredRow, PropertyDefinition propertyDefinition)
			{
				base.SetProperty(unfilteredRow, propertyDefinition, new PropertyError(propertyDefinition, PropertyErrorCode.NotFound));
			}
		}

		// Token: 0x02000484 RID: 1156
		// (Invoke) Token: 0x06003362 RID: 13154
		internal delegate bool StatefulRowPredicate(object[] transformedForFilterRow);
	}
}
