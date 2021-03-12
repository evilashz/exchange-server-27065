using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002D3 RID: 723
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractMessageItem : AbstractItem, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x00086253 File Offset: 0x00084453
		// (set) Token: 0x06001EDC RID: 7900 RVA: 0x0008625A File Offset: 0x0008445A
		public virtual bool IsDraft
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

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x00086261 File Offset: 0x00084461
		// (set) Token: 0x06001EDE RID: 7902 RVA: 0x00086268 File Offset: 0x00084468
		public virtual bool IsRead
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

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0008626F File Offset: 0x0008446F
		// (set) Token: 0x06001EE0 RID: 7904 RVA: 0x00086276 File Offset: 0x00084476
		public virtual string InReplyTo
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

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x0008627D File Offset: 0x0008447D
		public IList<Participant> ReplyTo
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x00086284 File Offset: 0x00084484
		public virtual RecipientCollection Recipients
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x0008628B File Offset: 0x0008448B
		// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x00086292 File Offset: 0x00084492
		public Participant Sender
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

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x00086299 File Offset: 0x00084499
		// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x000862A0 File Offset: 0x000844A0
		public Participant From
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

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x000862A7 File Offset: 0x000844A7
		// (set) Token: 0x06001EE8 RID: 7912 RVA: 0x000862AE File Offset: 0x000844AE
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

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x000862B5 File Offset: 0x000844B5
		public virtual string InternetMessageId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x000862BC File Offset: 0x000844BC
		// (set) Token: 0x06001EEB RID: 7915 RVA: 0x000862C3 File Offset: 0x000844C3
		public bool IsGroupEscalationMessage
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

		// Token: 0x06001EEC RID: 7916 RVA: 0x000862CA File Offset: 0x000844CA
		public virtual void SendWithoutSavingMessage()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000862D1 File Offset: 0x000844D1
		public void SuppressAllAutoResponses()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000862D8 File Offset: 0x000844D8
		public void MarkRecipientAsSubmitted(IEnumerable<Participant> submittedParticipants)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x000862DF File Offset: 0x000844DF
		public void StampMessageBodyTag()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x000862E6 File Offset: 0x000844E6
		public void CommitReplyTo()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x000862ED File Offset: 0x000844ED
		// (set) Token: 0x06001EF2 RID: 7922 RVA: 0x000862F4 File Offset: 0x000844F4
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

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x000862FB File Offset: 0x000844FB
		// (set) Token: 0x06001EF4 RID: 7924 RVA: 0x00086302 File Offset: 0x00084502
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

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00086309 File Offset: 0x00084509
		public virtual GlobalObjectId GetGlobalObjectId()
		{
			throw new NotImplementedException();
		}
	}
}
