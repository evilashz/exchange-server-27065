using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200001F RID: 31
	internal abstract class WorkDefinitionIndex<TWorkDefinition> where TWorkDefinition : WorkDefinition
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000A461 File Offset: 0x00008661
		internal static IIndexDescriptor<TWorkDefinition, int> Id(int id)
		{
			return new WorkDefinitionIndex<TWorkDefinition>.WorkDefinitionIndexDescriptorForId<TWorkDefinition>(id);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A469 File Offset: 0x00008669
		internal static IIndexDescriptor<TWorkDefinition, bool> Enabled(bool enabled)
		{
			return new WorkDefinitionIndex<TWorkDefinition>.WorkDefinitionIndexDescriptorForEnabled<TWorkDefinition>(enabled);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A471 File Offset: 0x00008671
		internal static IIndexDescriptor<TWorkDefinition, DateTime> StartTime(DateTime startTime)
		{
			return new WorkDefinitionIndex<TWorkDefinition>.WorkDefinitionIndexDescriptorForStartTime<TWorkDefinition>(startTime);
		}

		// Token: 0x02000020 RID: 32
		protected abstract class WorkDefinitionIndexBase<TDefinition, TKey> : IIndexDescriptor<TDefinition, TKey>, IIndexDescriptor where TDefinition : WorkDefinition
		{
			// Token: 0x060002B2 RID: 690 RVA: 0x0000A481 File Offset: 0x00008681
			protected WorkDefinitionIndexBase(TKey key)
			{
				this.key = key;
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000A490 File Offset: 0x00008690
			public TKey Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x060002B4 RID: 692
			public abstract IEnumerable<TKey> GetKeyValues(TDefinition item);

			// Token: 0x060002B5 RID: 693
			public abstract IDataAccessQuery<TDefinition> ApplyIndexRestriction(IDataAccessQuery<TDefinition> query);

			// Token: 0x040000F4 RID: 244
			private TKey key;
		}

		// Token: 0x02000021 RID: 33
		private class WorkDefinitionIndexDescriptorForId<TDefinition> : WorkDefinitionIndex<TWorkDefinition>.WorkDefinitionIndexBase<TDefinition, int> where TDefinition : WorkDefinition
		{
			// Token: 0x060002B6 RID: 694 RVA: 0x0000A498 File Offset: 0x00008698
			internal WorkDefinitionIndexDescriptorForId(int key) : base(key)
			{
			}

			// Token: 0x060002B7 RID: 695 RVA: 0x0000A588 File Offset: 0x00008788
			public override IEnumerable<int> GetKeyValues(TDefinition item)
			{
				yield return item.Id;
				yield break;
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x0000A5C4 File Offset: 0x000087C4
			public override IDataAccessQuery<TDefinition> ApplyIndexRestriction(IDataAccessQuery<TDefinition> query)
			{
				IEnumerable<TDefinition> query2 = from d in query
				where d.Id == base.Key
				select d;
				return query.AsDataAccessQuery<TDefinition>(query2);
			}
		}

		// Token: 0x02000022 RID: 34
		private class WorkDefinitionIndexDescriptorForEnabled<TDefinition> : WorkDefinitionIndex<TWorkDefinition>.WorkDefinitionIndexBase<TDefinition, bool> where TDefinition : WorkDefinition
		{
			// Token: 0x060002BA RID: 698 RVA: 0x0000A5EB File Offset: 0x000087EB
			internal WorkDefinitionIndexDescriptorForEnabled(bool key) : base(key)
			{
			}

			// Token: 0x060002BB RID: 699 RVA: 0x0000A5F4 File Offset: 0x000087F4
			public override IEnumerable<bool> GetKeyValues(TDefinition item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002BC RID: 700 RVA: 0x0000A614 File Offset: 0x00008814
			public override IDataAccessQuery<TDefinition> ApplyIndexRestriction(IDataAccessQuery<TDefinition> query)
			{
				IEnumerable<TDefinition> query2 = from d in query
				where d.Enabled == base.Key
				select d;
				return query.AsDataAccessQuery<TDefinition>(query2);
			}
		}

		// Token: 0x02000023 RID: 35
		private class WorkDefinitionIndexDescriptorForStartTime<TDefinition> : WorkDefinitionIndex<TWorkDefinition>.WorkDefinitionIndexBase<TDefinition, DateTime> where TDefinition : WorkDefinition
		{
			// Token: 0x060002BE RID: 702 RVA: 0x0000A63B File Offset: 0x0000883B
			internal WorkDefinitionIndexDescriptorForStartTime(DateTime key) : base(key)
			{
			}

			// Token: 0x060002BF RID: 703 RVA: 0x0000A644 File Offset: 0x00008844
			public override IEnumerable<DateTime> GetKeyValues(TDefinition item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x0000A668 File Offset: 0x00008868
			public override IDataAccessQuery<TDefinition> ApplyIndexRestriction(IDataAccessQuery<TDefinition> query)
			{
				IEnumerable<TDefinition> query2 = from d in query
				where d.StartTime <= base.Key
				select d;
				return query.AsDataAccessQuery<TDefinition>(query2);
			}
		}
	}
}
