using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x0200000D RID: 13
	internal class CountTrackerDiagnostics<TEntityType, TCountType> : ICountTrackerDiagnostics<TEntityType, TCountType> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00004C3B File Offset: 0x00002E3B
		public void SubscribeTo(MeteringEvent evt, ICountedEntity<TEntityType> entity, Action<ICountedEntity<TEntityType>> entityDelegate)
		{
			this.AddSubscriber(evt, new CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber(entityDelegate, entity));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004C4B File Offset: 0x00002E4B
		public void SubscribeTo(MeteringEvent evt, TCountType? measure, Action<TCountType> measureDelegate)
		{
			this.AddSubscriber(evt, new CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber(measureDelegate, measure));
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004C5B File Offset: 0x00002E5B
		public void EntityAdded(ICountedEntity<TEntityType> entity)
		{
			this.RaiseEvent(MeteringEvent.EntityAdded, entity);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004C65 File Offset: 0x00002E65
		public void EntityRemoved(ICountedEntity<TEntityType> entity)
		{
			this.RaiseEvent(MeteringEvent.EntityRemoved, entity);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004C6F File Offset: 0x00002E6F
		public void MeasureAdded(TCountType measure)
		{
			this.RaiseEvent(MeteringEvent.MeasureAdded, measure);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004C79 File Offset: 0x00002E79
		public void MeasureRemoved(TCountType measure)
		{
			this.RaiseEvent(MeteringEvent.MeasureRemoved, measure);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004C83 File Offset: 0x00002E83
		public void MeasurePromoted(TCountType measure)
		{
			this.RaiseEvent(MeteringEvent.MeasurePromoted, measure);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004C8D File Offset: 0x00002E8D
		public void MeasureExpired(TCountType measure)
		{
			this.RaiseEvent(MeteringEvent.MeasureExpired, measure);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004C98 File Offset: 0x00002E98
		private void AddSubscriber(MeteringEvent evt, CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber subscriber)
		{
			lock (this.syncObject)
			{
				List<CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber> list;
				if (!this.listeners.TryGetValue(evt, out list))
				{
					list = new List<CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber>();
					this.listeners.Add(evt, list);
				}
				list.Add(subscriber);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004CFC File Offset: 0x00002EFC
		private void RaiseEvent(MeteringEvent evt, ICountedEntity<TEntityType> filter)
		{
			List<CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber> collection;
			if (this.TryGetSubscriber(evt, out collection))
			{
				List<CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber> list = new List<CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber>(collection);
				foreach (CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber subscriber in list)
				{
					subscriber.RaiseEvent(filter);
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004D5C File Offset: 0x00002F5C
		private void RaiseEvent(MeteringEvent evt, TCountType filter)
		{
			List<CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber> collection;
			if (this.TryGetSubscriber(evt, out collection))
			{
				List<CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber> list = new List<CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber>(collection);
				foreach (CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber subscriber in list)
				{
					subscriber.RaiseEvent(filter);
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004DBC File Offset: 0x00002FBC
		private bool TryGetSubscriber(MeteringEvent evt, out List<CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber> listener)
		{
			bool result;
			lock (this.syncObject)
			{
				result = this.listeners.TryGetValue(evt, out listener);
			}
			return result;
		}

		// Token: 0x04000053 RID: 83
		private Dictionary<MeteringEvent, List<CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber>> listeners = new Dictionary<MeteringEvent, List<CountTrackerDiagnostics<TEntityType, TCountType>.Subscriber>>();

		// Token: 0x04000054 RID: 84
		private object syncObject = new object();

		// Token: 0x0200000E RID: 14
		private class Subscriber
		{
			// Token: 0x060000C8 RID: 200 RVA: 0x00004E26 File Offset: 0x00003026
			public Subscriber(Action<TCountType> measureDelegate, TCountType? measure)
			{
				this.measureDelegate = measureDelegate;
				this.measure = measure;
				this.measureBased = true;
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x00004E43 File Offset: 0x00003043
			public Subscriber(Action<ICountedEntity<TEntityType>> entityDelegate, ICountedEntity<TEntityType> entity)
			{
				this.entityDelegate = entityDelegate;
				this.entity = entity;
				this.measureBased = false;
			}

			// Token: 0x060000CA RID: 202 RVA: 0x00004E60 File Offset: 0x00003060
			public void RaiseEvent(ICountedEntity<TEntityType> countedEntity)
			{
				if (!this.measureBased && (this.entity == null || this.entity.Equals(countedEntity)))
				{
					this.entityDelegate(countedEntity);
				}
			}

			// Token: 0x060000CB RID: 203 RVA: 0x00004E8C File Offset: 0x0000308C
			public void RaiseEvent(TCountType count)
			{
				if (this.measureBased)
				{
					if (this.measure != null)
					{
						TCountType value = this.measure.Value;
						if (!value.Equals(count))
						{
							return;
						}
					}
					this.measureDelegate(count);
				}
			}

			// Token: 0x04000055 RID: 85
			private readonly Action<ICountedEntity<TEntityType>> entityDelegate;

			// Token: 0x04000056 RID: 86
			private readonly ICountedEntity<TEntityType> entity;

			// Token: 0x04000057 RID: 87
			private readonly Action<TCountType> measureDelegate;

			// Token: 0x04000058 RID: 88
			private readonly TCountType? measure;

			// Token: 0x04000059 RID: 89
			private readonly bool measureBased;
		}
	}
}
