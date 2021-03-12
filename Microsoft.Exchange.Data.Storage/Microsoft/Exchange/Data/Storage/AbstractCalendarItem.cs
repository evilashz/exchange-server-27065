using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002C7 RID: 711
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractCalendarItem : AbstractCalendarItemInstance, ICalendarItem, ICalendarItemInstance, ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x000860A8 File Offset: 0x000842A8
		public virtual string InternetMessageId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x000860AF File Offset: 0x000842AF
		// (set) Token: 0x06001E91 RID: 7825 RVA: 0x000860B6 File Offset: 0x000842B6
		public int InstanceCreationIndex
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x000860BD File Offset: 0x000842BD
		// (set) Token: 0x06001E93 RID: 7827 RVA: 0x000860C4 File Offset: 0x000842C4
		public virtual bool HasExceptionalInboxReminders
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x000860CB File Offset: 0x000842CB
		// (set) Token: 0x06001E95 RID: 7829 RVA: 0x000860D2 File Offset: 0x000842D2
		public virtual Recurrence Recurrence
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000860D9 File Offset: 0x000842D9
		public CalendarItemOccurrence OpenOccurrence(StoreObjectId id, params PropertyDefinition[] prefetchPropertyDefinitions)
		{
			throw new NotImplementedException();
		}
	}
}
