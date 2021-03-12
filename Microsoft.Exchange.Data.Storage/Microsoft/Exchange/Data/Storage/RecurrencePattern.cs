using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003BD RID: 957
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RecurrencePattern : IEquatable<RecurrencePattern>
	{
		// Token: 0x06002BA2 RID: 11170 RVA: 0x000AE11E File Offset: 0x000AC31E
		public override string ToString()
		{
			return this.When();
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000AE12B File Offset: 0x000AC32B
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000AE133 File Offset: 0x000AC333
		public override bool Equals(object obj)
		{
			if (obj is RecurrencePattern)
			{
				return this.Equals((RecurrencePattern)obj);
			}
			return base.Equals(obj);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000AE151 File Offset: 0x000AC351
		public bool Equals(RecurrencePattern value)
		{
			return this.Equals(value, false);
		}

		// Token: 0x06002BA6 RID: 11174
		public abstract bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth);

		// Token: 0x06002BA7 RID: 11175
		internal abstract LocalizedString When();

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000AE15B File Offset: 0x000AC35B
		// (set) Token: 0x06002BA9 RID: 11177 RVA: 0x000AE163 File Offset: 0x000AC363
		internal RecurrenceObjectType RecurrenceObjectType
		{
			get
			{
				return this.recurrenceObjectType;
			}
			set
			{
				this.recurrenceObjectType = value;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000AE16C File Offset: 0x000AC36C
		internal virtual RecurrenceType MapiRecurrenceType
		{
			get
			{
				return RecurrenceType.None;
			}
		}

		// Token: 0x04001862 RID: 6242
		private RecurrenceObjectType recurrenceObjectType;
	}
}
