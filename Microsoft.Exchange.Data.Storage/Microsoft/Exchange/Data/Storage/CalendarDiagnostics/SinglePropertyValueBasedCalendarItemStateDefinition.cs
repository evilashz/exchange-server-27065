using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000360 RID: 864
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SinglePropertyValueBasedCalendarItemStateDefinition<TValue> : AtomicCalendarItemStateDefinition<TValue>, IEquatable<SinglePropertyValueBasedCalendarItemStateDefinition<TValue>>
	{
		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x0009A6B5 File Offset: 0x000988B5
		// (set) Token: 0x0600267B RID: 9851 RVA: 0x0009A6BD File Offset: 0x000988BD
		public StorePropertyDefinition TargetProperty { get; private set; }

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x0009A6C6 File Offset: 0x000988C6
		// (set) Token: 0x0600267D RID: 9853 RVA: 0x0009A6CE File Offset: 0x000988CE
		public HashSet<TValue> TargetValueSet { get; private set; }

		// Token: 0x0600267E RID: 9854 RVA: 0x0009A6D8 File Offset: 0x000988D8
		private static HashSet<TValue> GetValueSetFromSingleValue(TValue singleTargetValue, IEqualityComparer<TValue> equalityComparer)
		{
			return new HashSet<TValue>(equalityComparer)
			{
				singleTargetValue
			};
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x0009A6F5 File Offset: 0x000988F5
		public SinglePropertyValueBasedCalendarItemStateDefinition(StorePropertyDefinition targetProperty, HashSet<TValue> targetValueSet) : base(targetProperty.Name)
		{
			Util.ThrowOnNullArgument(targetValueSet, "targetValueSet");
			this.TargetProperty = targetProperty;
			this.TargetValueSet = targetValueSet;
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x0009A71C File Offset: 0x0009891C
		public SinglePropertyValueBasedCalendarItemStateDefinition(StorePropertyDefinition targetProperty, TValue targetValue) : this(targetProperty, targetValue, null)
		{
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x0009A727 File Offset: 0x00098927
		public SinglePropertyValueBasedCalendarItemStateDefinition(StorePropertyDefinition targetProperty, TValue targetValue, IEqualityComparer<TValue> equalityComparer) : this(targetProperty, SinglePropertyValueBasedCalendarItemStateDefinition<TValue>.GetValueSetFromSingleValue(targetValue, equalityComparer))
		{
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x0009A738 File Offset: 0x00098938
		private bool IsTargetValueSetsEqual(SinglePropertyValueBasedCalendarItemStateDefinition<TValue> first, SinglePropertyValueBasedCalendarItemStateDefinition<TValue> second)
		{
			if (first.TargetValueSet.Count == second.TargetValueSet.Count)
			{
				foreach (TValue item in first.TargetValueSet)
				{
					if (!second.TargetValueSet.Contains(item))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0009A7B4 File Offset: 0x000989B4
		protected UnderlyingType GetUnderlyingValue<UnderlyingType>(PropertyBag propertyBag)
		{
			return propertyBag.GetValueOrDefault<UnderlyingType>(this.TargetProperty);
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x0009A7C2 File Offset: 0x000989C2
		protected override TValue GetValueFromPropertyBag(PropertyBag propertyBag, MailboxSession session)
		{
			return this.GetUnderlyingValue<TValue>(propertyBag);
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x0009A7CB File Offset: 0x000989CB
		public override bool IsRecurringMasterSpecific
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0009A7CE File Offset: 0x000989CE
		protected override bool Evaluate(TValue value)
		{
			return this.TargetValueSet.Contains(value);
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0009A7DC File Offset: 0x000989DC
		public override bool Equals(ICalendarItemStateDefinition other)
		{
			return other is SinglePropertyValueBasedCalendarItemStateDefinition<TValue> && this.Equals((SinglePropertyValueBasedCalendarItemStateDefinition<TValue>)other);
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x0009A7F4 File Offset: 0x000989F4
		public bool Equals(SinglePropertyValueBasedCalendarItemStateDefinition<TValue> other)
		{
			return other != null && (object.ReferenceEquals(this, other) || (this.TargetProperty.Equals(other.TargetProperty) && this.IsTargetValueSetsEqual(this, other)));
		}
	}
}
