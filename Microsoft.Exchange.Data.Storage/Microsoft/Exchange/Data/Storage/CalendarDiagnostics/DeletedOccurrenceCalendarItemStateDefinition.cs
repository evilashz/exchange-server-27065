using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x0200036B RID: 875
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DeletedOccurrenceCalendarItemStateDefinition : AtomicCalendarItemStateDefinition<bool>, IEquatable<DeletedOccurrenceCalendarItemStateDefinition>
	{
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x0009B290 File Offset: 0x00099490
		// (set) Token: 0x060026B4 RID: 9908 RVA: 0x0009B298 File Offset: 0x00099498
		public ExDateTime OccurrenceDateId { get; private set; }

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x0009B2A1 File Offset: 0x000994A1
		// (set) Token: 0x060026B6 RID: 9910 RVA: 0x0009B2A9 File Offset: 0x000994A9
		public bool IsOccurrencePresent { get; private set; }

		// Token: 0x060026B7 RID: 9911 RVA: 0x0009B2B2 File Offset: 0x000994B2
		public DeletedOccurrenceCalendarItemStateDefinition(ExDateTime occurrenceDateId, bool isOccurrencePresent) : base(occurrenceDateId.ToShortDateString())
		{
			this.OccurrenceDateId = occurrenceDateId;
			this.IsOccurrencePresent = isOccurrencePresent;
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x0009B2CF File Offset: 0x000994CF
		public override bool IsRecurringMasterSpecific
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x0009B2D2 File Offset: 0x000994D2
		public override string SchemaKey
		{
			get
			{
				return "{77D2323B-C6EE-44b7-A00E-CEB0465DC109}";
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x0009B2D9 File Offset: 0x000994D9
		public override StorePropertyDefinition[] RequiredProperties
		{
			get
			{
				return DeletedOccurrenceCalendarItemStateDefinition.requiredProperties;
			}
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x0009B2E0 File Offset: 0x000994E0
		protected override bool GetValueFromPropertyBag(PropertyBag propertyBag, MailboxSession session)
		{
			Recurrence recurrence;
			return Recurrence.TryFromMasterPropertyBag(propertyBag, session, out recurrence) && recurrence.IsValidOccurrenceId(this.OccurrenceDateId) && !recurrence.IsOccurrenceDeleted(this.OccurrenceDateId);
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x0009B319 File Offset: 0x00099519
		protected override bool Evaluate(bool value)
		{
			return this.IsOccurrencePresent == value;
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x0009B324 File Offset: 0x00099524
		public override bool Equals(ICalendarItemStateDefinition other)
		{
			return other is DeletedOccurrenceCalendarItemStateDefinition && this.Equals((DeletedOccurrenceCalendarItemStateDefinition)other);
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x0009B33C File Offset: 0x0009953C
		public bool Equals(DeletedOccurrenceCalendarItemStateDefinition other)
		{
			return other != null && (object.ReferenceEquals(this, other) || this.OccurrenceDateId.Equals(other.OccurrenceDateId));
		}

		// Token: 0x0400170C RID: 5900
		private static readonly StorePropertyDefinition[] requiredProperties = ((IEnumerable<StorePropertyDefinition>)Recurrence.RequiredRecurrenceProperties).Concat(new StorePropertyDefinition[]
		{
			CalendarItemBaseSchema.ClientIntent
		}).ToArray<StorePropertyDefinition>();
	}
}
