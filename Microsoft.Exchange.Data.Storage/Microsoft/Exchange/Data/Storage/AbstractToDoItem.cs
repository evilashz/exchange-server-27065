using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002E5 RID: 741
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractToDoItem : AbstractItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x0008660C File Offset: 0x0008480C
		// (set) Token: 0x06001F8C RID: 8076 RVA: 0x00086613 File Offset: 0x00084813
		public virtual string Subject
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

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x0008661A File Offset: 0x0008481A
		public virtual string InternetMessageId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x00086621 File Offset: 0x00084821
		// (set) Token: 0x06001F8F RID: 8079 RVA: 0x00086628 File Offset: 0x00084828
		public virtual Reminders<ModernReminder> ModernReminders
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

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x0008662F File Offset: 0x0008482F
		// (set) Token: 0x06001F91 RID: 8081 RVA: 0x00086636 File Offset: 0x00084836
		public virtual RemindersState<ModernReminderState> ModernRemindersState
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

		// Token: 0x06001F92 RID: 8082 RVA: 0x0008663D File Offset: 0x0008483D
		public virtual GlobalObjectId GetGlobalObjectId()
		{
			throw new NotImplementedException();
		}
	}
}
