using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000369 RID: 873
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CompositeCalendarItemStateDefinition : ICalendarItemStateDefinition, IEquatable<ICalendarItemStateDefinition>, IEqualityComparer<CalendarItemState>, IEquatable<CompositeCalendarItemStateDefinition>
	{
		// Token: 0x060026A9 RID: 9897 RVA: 0x0009AF38 File Offset: 0x00099138
		private CompositeCalendarItemStateDefinition(CompositeCalendarItemStateDefinition.Operator stateOperator, params ICalendarItemStateDefinition[] states)
		{
			Util.ThrowOnNullArgument(states, "states");
			this.stateOperator = stateOperator;
			HashSet<StorePropertyDefinition> hashSet = new HashSet<StorePropertyDefinition>();
			StringBuilder stringBuilder = new StringBuilder(this.stateOperator.ToString());
			this.operands = new List<ICalendarItemStateDefinition>(states.Length);
			foreach (ICalendarItemStateDefinition calendarItemStateDefinition in states)
			{
				if (calendarItemStateDefinition == null)
				{
					throw new InvalidOperationException("Cannot combine null definitions.");
				}
				if (calendarItemStateDefinition.IsRecurringMasterSpecific)
				{
					throw new NotSupportedException("Recurring specific state definitions are not supported.");
				}
				this.operands.Add(calendarItemStateDefinition);
				stringBuilder.Append(calendarItemStateDefinition.SchemaKey);
				foreach (StorePropertyDefinition item in calendarItemStateDefinition.RequiredProperties)
				{
					if (!hashSet.Contains(item))
					{
						hashSet.Add(item);
					}
				}
			}
			this.schemaKey = stringBuilder.ToString();
			int num = 0;
			this.requiredProperties = new StorePropertyDefinition[hashSet.Count];
			foreach (StorePropertyDefinition storePropertyDefinition in hashSet)
			{
				this.requiredProperties[num] = storePropertyDefinition;
				num++;
			}
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x0009B07C File Offset: 0x0009927C
		public static CompositeCalendarItemStateDefinition GetDeletedFromFolderStateDefinition(byte[] folderId)
		{
			ICalendarItemStateDefinition calendarItemStateDefinition = ActionBasedCalendarItemStateDefinition.CreateDeletedNoneOccurrenceCalendarItemStateDefinition();
			ICalendarItemStateDefinition calendarItemStateDefinition2 = new FolderBasedCalendarItemStateDefinition(folderId);
			return new CompositeCalendarItemStateDefinition(CompositeCalendarItemStateDefinition.Operator.And, new ICalendarItemStateDefinition[]
			{
				calendarItemStateDefinition,
				calendarItemStateDefinition2
			});
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x060026AB RID: 9899 RVA: 0x0009B0AC File Offset: 0x000992AC
		public bool IsRecurringMasterSpecific
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x0009B0AF File Offset: 0x000992AF
		public string SchemaKey
		{
			get
			{
				return this.schemaKey;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x0009B0B7 File Offset: 0x000992B7
		public StorePropertyDefinition[] RequiredProperties
		{
			get
			{
				return this.requiredProperties;
			}
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0009B0E4 File Offset: 0x000992E4
		public bool Evaluate(CalendarItemState state, PropertyBag propertyBag, MailboxSession session)
		{
			return this.operands.TrueForAll((ICalendarItemStateDefinition definition) => definition.Evaluate(state, propertyBag, session));
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0009B123 File Offset: 0x00099323
		public bool Equals(ICalendarItemStateDefinition other)
		{
			return other is CompositeCalendarItemStateDefinition && this.Equals((CompositeCalendarItemStateDefinition)other);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x0009B158 File Offset: 0x00099358
		public bool Equals(CompositeCalendarItemStateDefinition other)
		{
			if (other == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (this.stateOperator == other.stateOperator && this.operands.Count == other.operands.Count)
			{
				return this.operands.TrueForAll((ICalendarItemStateDefinition definition) => other.operands.Contains(definition));
			}
			return false;
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x0009B1F8 File Offset: 0x000993F8
		public bool Equals(CalendarItemState x, CalendarItemState y)
		{
			return this.operands.TrueForAll((ICalendarItemStateDefinition definition) => definition.Equals(x, y));
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x0009B230 File Offset: 0x00099430
		public int GetHashCode(CalendarItemState obj)
		{
			if (obj == null)
			{
				return 0;
			}
			int num = 0;
			foreach (ICalendarItemStateDefinition calendarItemStateDefinition in this.operands)
			{
				num = (num << 1 ^ calendarItemStateDefinition.GetHashCode(obj));
			}
			return num;
		}

		// Token: 0x04001706 RID: 5894
		private List<ICalendarItemStateDefinition> operands;

		// Token: 0x04001707 RID: 5895
		private CompositeCalendarItemStateDefinition.Operator stateOperator;

		// Token: 0x04001708 RID: 5896
		private string schemaKey;

		// Token: 0x04001709 RID: 5897
		private StorePropertyDefinition[] requiredProperties;

		// Token: 0x0200036A RID: 874
		internal enum Operator
		{
			// Token: 0x0400170B RID: 5899
			And
		}
	}
}
