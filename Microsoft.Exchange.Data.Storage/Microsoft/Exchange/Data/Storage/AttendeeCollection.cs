using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000380 RID: 896
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AttendeeCollection : RecipientBaseCollection<Attendee>, IAttendeeCollectionImpl, IAttendeeCollection, IRecipientBaseCollection<Attendee>, IList<Attendee>, ICollection<Attendee>, IEnumerable<Attendee>, IEnumerable, ILocationIdentifierController
	{
		// Token: 0x06002792 RID: 10130 RVA: 0x0009DE93 File Offset: 0x0009C093
		internal AttendeeCollection(CoreRecipientCollection coreRecipientCollection) : base(coreRecipientCollection)
		{
			if (coreRecipientCollection.CoreItem.Session != null)
			{
				this.Cleanup();
				this.LoadIsDistributionList();
			}
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x0009DF38 File Offset: 0x0009C138
		public Attendee Add(Participant participant, AttendeeType attendeeType = AttendeeType.Required, ResponseType? responseType = null, ExDateTime? replyTime = null, bool checkExisting = false)
		{
			return AttendeeCollection.InternalAdd(this, delegate
			{
				Attendee attendeeToAdd = this.GetAttendeeToAdd(participant);
				attendeeToAdd.AttendeeType = attendeeType;
				if (responseType != null)
				{
					attendeeToAdd.ResponseType = responseType.Value;
				}
				if (replyTime != null)
				{
					attendeeToAdd.ReplyTime = replyTime.Value;
				}
				this.ReportRecipientChange(LastChangeAction.RecipientAdded);
				return attendeeToAdd;
			}, participant, attendeeType, responseType, checkExisting);
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x0009DF98 File Offset: 0x0009C198
		public override Attendee Add(Participant participant)
		{
			if (participant == null)
			{
				throw new ArgumentNullException("participant");
			}
			return this.Add(participant, AttendeeType.Required, null, null, false);
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0009DFD4 File Offset: 0x0009C1D4
		public override void Add(Attendee item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.Add(item.Participant, item.AttendeeType, null, null, false);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x0009E018 File Offset: 0x0009C218
		internal static Attendee InternalAdd(IAttendeeCollection attendeeCollection, Func<Attendee> performAdd, Participant participant, AttendeeType attendeeType = AttendeeType.Required, ResponseType? responseType = null, bool checkExisting = false)
		{
			if (participant == null)
			{
				throw new ArgumentNullException("participant");
			}
			EnumValidator.ThrowIfInvalid<AttendeeType>(attendeeType, "attendeeType");
			if (responseType != null)
			{
				EnumValidator.ThrowIfInvalid<ResponseType>(responseType.Value, "responseType");
			}
			if (checkExisting)
			{
				int num = AttendeeCollection.IndexOf(attendeeCollection, participant, false);
				if (num != -1)
				{
					Attendee attendee = attendeeCollection[num];
					if (attendeeType != AttendeeType.Required || attendee.AttendeeType == AttendeeType.Required)
					{
						return attendee;
					}
					attendeeCollection.RemoveAt(num);
				}
			}
			return performAdd();
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x0009E094 File Offset: 0x0009C294
		internal static void Cleanup(IAttendeeCollection attendees)
		{
			HashSet<string> hashSet = new HashSet<string>();
			List<Attendee> list = new List<Attendee>();
			foreach (Attendee attendee in attendees)
			{
				if (attendee.Participant.RoutingType == "SMTP" || attendee.Participant.RoutingType == "EX")
				{
					RecipientFlags recipientFlags = attendee.RecipientFlags;
					bool flag = (recipientFlags & RecipientFlags.ExceptionalDeleted) == RecipientFlags.ExceptionalDeleted;
					string item = attendee.GetAttendeeKey() + (flag ? ":D" : ":ND");
					if (!hashSet.Contains(item))
					{
						hashSet.Add(item);
					}
					else
					{
						list.Add(attendee);
					}
				}
			}
			foreach (Attendee item2 in list)
			{
				attendees.Remove(item2);
			}
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x0009E1A4 File Offset: 0x0009C3A4
		private static int IndexOf(IAttendeeCollection attendeeCollection, Participant participant, bool canLookup)
		{
			int count = attendeeCollection.Count;
			for (int i = 0; i < count; i++)
			{
				if (Participant.HasSameEmail(attendeeCollection[i].Participant, participant, canLookup))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x0009E1DC File Offset: 0x0009C3DC
		internal void Cleanup()
		{
			((IAttendeeCollectionImpl)this).Cleanup();
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x0009E1E4 File Offset: 0x0009C3E4
		internal void LoadIsDistributionList()
		{
			((IAttendeeCollectionImpl)this).LoadIsDistributionList();
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x0009E1EC File Offset: 0x0009C3EC
		void IAttendeeCollectionImpl.Cleanup()
		{
			AttendeeCollection.Cleanup(this);
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x0009E1F4 File Offset: 0x0009C3F4
		void IAttendeeCollectionImpl.LoadIsDistributionList()
		{
			base.LoadAdditionalParticipantProperties(new PropertyDefinition[]
			{
				ParticipantSchema.IsDistributionList
			});
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x0009E218 File Offset: 0x0009C418
		internal Attendee AddClone(Attendee item)
		{
			CoreRecipient coreRecipient = base.CoreRecipientCollection.CreateCoreRecipient(item.CoreRecipient);
			Attendee result = this.ConstructStronglyTypedRecipient(coreRecipient);
			this.ReportRecipientChange(LastChangeAction.RecipientAdded);
			return result;
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x0009E248 File Offset: 0x0009C448
		protected override Attendee ConstructStronglyTypedRecipient(CoreRecipient coreRecipient)
		{
			return new Attendee(coreRecipient);
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x0009E250 File Offset: 0x0009C450
		public override void Clear()
		{
			this.LocationIdentifierHelperInstance.SetLocationIdentifier(49013U);
			base.Clear();
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x0009E268 File Offset: 0x0009C468
		public override void Remove(RecipientId id)
		{
			this.LocationIdentifierHelperInstance.SetLocationIdentifier(65397U);
			base.Remove(id);
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x0009E281 File Offset: 0x0009C481
		public override void RemoveAt(int index)
		{
			this.LocationIdentifierHelperInstance.SetLocationIdentifier(40821U);
			base.RemoveAt(index);
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x0009E29A File Offset: 0x0009C49A
		protected override void OnRemoveRecipient()
		{
			this.ReportRecipientChange(LastChangeAction.RecipientRemoved);
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x0009E2A4 File Offset: 0x0009C4A4
		private Attendee GetAttendeeToAdd(Participant participant)
		{
			CoreRecipient coreRecipient = base.CreateCoreRecipient(new CoreRecipient.SetDefaultPropertiesDelegate(Attendee.SetDefaultAttendeeProperties), participant);
			return this.ConstructStronglyTypedRecipient(coreRecipient);
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x0009E2CC File Offset: 0x0009C4CC
		private void ReportRecipientChange(LastChangeAction action)
		{
			this.LocationIdentifierHelperInstance.SetLocationIdentifier(59893U, action);
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x0009E2DF File Offset: 0x0009C4DF
		public LocationIdentifierHelper LocationIdentifierHelperInstance
		{
			get
			{
				return base.CoreRecipientCollection.LocationIdentifierHelperInstance;
			}
		}
	}
}
