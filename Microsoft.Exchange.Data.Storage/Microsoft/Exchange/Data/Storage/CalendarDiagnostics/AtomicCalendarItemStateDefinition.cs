using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x0200035F RID: 863
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AtomicCalendarItemStateDefinition<TValue> : ICalendarItemStateDefinition, IEquatable<ICalendarItemStateDefinition>, IEqualityComparer<CalendarItemState>
	{
		// Token: 0x06002670 RID: 9840 RVA: 0x0009A58F File Offset: 0x0009878F
		public AtomicCalendarItemStateDefinition(string keyPart)
		{
			this.stateKey = string.Format("{0}({1})", this.SchemaKey, keyPart);
		}

		// Token: 0x06002671 RID: 9841
		protected abstract TValue GetValueFromPropertyBag(PropertyBag propertyBag, MailboxSession session);

		// Token: 0x06002672 RID: 9842
		protected abstract bool Evaluate(TValue value);

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06002673 RID: 9843
		public abstract bool IsRecurringMasterSpecific { get; }

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06002674 RID: 9844
		public abstract string SchemaKey { get; }

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06002675 RID: 9845
		public abstract StorePropertyDefinition[] RequiredProperties { get; }

		// Token: 0x06002676 RID: 9846 RVA: 0x0009A5B0 File Offset: 0x000987B0
		public bool Evaluate(CalendarItemState state, PropertyBag propertyBag, MailboxSession session)
		{
			Util.ThrowOnNullArgument(state, "state");
			Util.ThrowOnNullArgument(propertyBag, "propertyBag");
			Util.ThrowOnNullArgument(session, "session");
			TValue tvalue;
			if (state.ContainsKey(this.stateKey))
			{
				tvalue = (TValue)((object)state[this.stateKey]);
			}
			else
			{
				tvalue = this.GetValueFromPropertyBag(propertyBag, session);
				state[this.stateKey] = tvalue;
			}
			return this.Evaluate(tvalue);
		}

		// Token: 0x06002677 RID: 9847
		public abstract bool Equals(ICalendarItemStateDefinition other);

		// Token: 0x06002678 RID: 9848 RVA: 0x0009A624 File Offset: 0x00098824
		public bool Equals(CalendarItemState x, CalendarItemState y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (!x.ContainsKey(this.stateKey) || !y.ContainsKey(this.stateKey))
			{
				throw new ArgumentException("The states don't have the required data.");
			}
			object obj = x[this.stateKey];
			object obj2 = y[this.stateKey];
			if (obj == null)
			{
				return obj2 == null;
			}
			return obj.Equals(obj2);
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0009A68F File Offset: 0x0009888F
		public int GetHashCode(CalendarItemState obj)
		{
			if (obj == null || !obj.ContainsKey(this.stateKey))
			{
				return 0;
			}
			return obj[this.stateKey].GetHashCode();
		}

		// Token: 0x040016FA RID: 5882
		private string stateKey;
	}
}
