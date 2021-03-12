using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003E9 RID: 1001
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OccurrenceAttendeeCollection : IAttendeeCollectionImpl, IAttendeeCollection, IRecipientBaseCollection<Attendee>, IList<Attendee>, ICollection<Attendee>, IEnumerable<Attendee>, IEnumerable
	{
		// Token: 0x06002D98 RID: 11672 RVA: 0x000BBCB4 File Offset: 0x000B9EB4
		internal OccurrenceAttendeeCollection(CalendarItemOccurrence occurrence)
		{
			this.occurrence = occurrence;
			this.LoadMasterAttendeeCollection();
			CoreRecipientCollection recipients;
			if (occurrence.OccurrencePropertyBag.ExceptionMessage != null)
			{
				recipients = occurrence.OccurrencePropertyBag.ExceptionMessage.CoreItem.Recipients;
			}
			else
			{
				recipients = occurrence.OccurrencePropertyBag.MasterCalendarItem.CoreItem.Recipients;
				CalendarItem masterCalendarItem = occurrence.OccurrencePropertyBag.MasterCalendarItem;
			}
			this.exceptionAttendeeCollection = new AttendeeCollection(recipients);
			this.BuildOccurrenceAttendeeCollection();
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000BBE68 File Offset: 0x000BA068
		public Attendee Add(Participant participant, AttendeeType attendeeType = AttendeeType.Required, ResponseType? responseType = null, ExDateTime? replyTime = null, bool checkExisting = false)
		{
			return AttendeeCollection.InternalAdd(this, delegate
			{
				int num;
				Attendee attendee = OccurrenceAttendeeCollection.FindAttendee(this.exceptionAttendeeCollection, participant, this.occurrence.Session as MailboxSession, out num);
				if (attendee != null && attendee.HasFlags(RecipientFlags.ExceptionalDeleted))
				{
					attendee.RecipientFlags &= ~RecipientFlags.ExceptionalDeleted;
					this.exceptionAttendeeCollection.Remove(attendee);
					this.exceptionAttendeeCollection.LocationIdentifierHelperInstance.SetLocationIdentifier(46965U);
					attendee = this.exceptionAttendeeCollection.AddClone(attendee);
					attendee.AttendeeType = attendeeType;
					if (responseType != null)
					{
						attendee.ResponseType = responseType.Value;
					}
					if (replyTime != null)
					{
						attendee.ReplyTime = replyTime.Value;
					}
				}
				else
				{
					attendee = this.exceptionAttendeeCollection.Add(participant, attendeeType, responseType, replyTime, checkExisting);
				}
				this.attendeeCollection.Add(attendee);
				return attendee;
			}, participant, attendeeType, responseType, checkExisting);
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000BBED1 File Offset: 0x000BA0D1
		void IAttendeeCollectionImpl.Cleanup()
		{
			this.Cleanup();
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000BBED9 File Offset: 0x000BA0D9
		void IAttendeeCollectionImpl.LoadIsDistributionList()
		{
			this.LoadIsDistributionList();
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000BBEE4 File Offset: 0x000BA0E4
		public Attendee Add(Participant participant)
		{
			if (participant == null)
			{
				throw new ArgumentNullException("participant");
			}
			return this.Add(participant, AttendeeType.Required, null, null, false);
		}

		// Token: 0x17000EAE RID: 3758
		public Attendee this[RecipientId id]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000BBF27 File Offset: 0x000BA127
		public void Remove(RecipientId id)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000BBF2E File Offset: 0x000BA12E
		public int IndexOf(Attendee item)
		{
			return this.attendeeCollection.IndexOf(item);
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000BBF3C File Offset: 0x000BA13C
		public void Insert(int index, Attendee item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000BBF44 File Offset: 0x000BA144
		public void RemoveAt(int index)
		{
			Attendee match = this.attendeeCollection[index];
			this.attendeeCollection.RemoveAt(index);
			MailboxSession session = this.occurrence.Session as MailboxSession;
			int num;
			Attendee attendee = OccurrenceAttendeeCollection.FindAttendee(this.masterAttendeeCollection, match, session, out num);
			int index2;
			Attendee attendee2 = OccurrenceAttendeeCollection.FindAttendee(this.exceptionAttendeeCollection, match, session, out index2);
			if (attendee == null && attendee2 != null)
			{
				this.exceptionAttendeeCollection.LocationIdentifierHelperInstance.SetLocationIdentifier(50677U);
				this.exceptionAttendeeCollection.RemoveAt(index2);
			}
			else if (attendee != null)
			{
				if (attendee2 == null)
				{
					this.exceptionAttendeeCollection.LocationIdentifierHelperInstance.SetLocationIdentifier(47605U);
					attendee2 = this.exceptionAttendeeCollection.AddClone(attendee);
				}
				attendee2.RecipientFlags |= RecipientFlags.ExceptionalDeleted;
			}
			if (index < this.masterAttendeeCount)
			{
				this.masterAttendeeCount--;
			}
		}

		// Token: 0x17000EAF RID: 3759
		public Attendee this[int index]
		{
			get
			{
				return this.attendeeCollection[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000BC030 File Offset: 0x000BA230
		public void Add(Attendee attendee)
		{
			if (attendee == null)
			{
				throw new ArgumentNullException("attendee");
			}
			this.Add(attendee.Participant, attendee.AttendeeType, null, null, false);
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000BC074 File Offset: 0x000BA274
		public bool Remove(Attendee attendee)
		{
			if (attendee == null)
			{
				throw new ArgumentNullException("attendee");
			}
			int num = this.IndexOf(attendee);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
			return num != -1;
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000BC0AC File Offset: 0x000BA2AC
		public void Clear()
		{
			for (int i = this.attendeeCollection.Count - 1; i >= 0; i--)
			{
				this.RemoveAt(i);
			}
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000BC0D8 File Offset: 0x000BA2D8
		public bool Contains(Attendee item)
		{
			return this.attendeeCollection.Contains(item);
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000BC0E6 File Offset: 0x000BA2E6
		public void CopyTo(Attendee[] array, int arrayIndex)
		{
			this.attendeeCollection.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x000BC0F5 File Offset: 0x000BA2F5
		public int Count
		{
			get
			{
				return this.attendeeCollection.Count;
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06002DAA RID: 11690 RVA: 0x000BC102 File Offset: 0x000BA302
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000BC105 File Offset: 0x000BA305
		public IEnumerator<Attendee> GetEnumerator()
		{
			return this.attendeeCollection.GetEnumerator();
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000BC117 File Offset: 0x000BA317
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.attendeeCollection.GetEnumerator();
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x000BC129 File Offset: 0x000BA329
		internal bool IsDirty
		{
			get
			{
				return this.masterAttendeeCollection.IsDirty || this.exceptionAttendeeCollection.IsDirty;
			}
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000BC148 File Offset: 0x000BA348
		internal void ApplyChangesToExceptionAttendeeCollection(MapiMessage exceptionMessage)
		{
			if (this.IsDirty)
			{
				this.Cleanup();
				for (int i = 0; i < this.masterAttendeeCount; i++)
				{
					Attendee attendee = this.attendeeCollection[i];
					if (attendee.IsDirty && !this.exceptionAttendeeCollection.Contains(attendee))
					{
						this.exceptionAttendeeCollection.AddClone(attendee);
					}
				}
				if (this.exceptionAttendeeCollection.CoreItem.MapiMessage != exceptionMessage)
				{
					CoreRecipientCollection recipients = this.occurrence.OccurrencePropertyBag.ExceptionMessage.CoreItem.Recipients;
					AttendeeCollection attendeeCollection = new AttendeeCollection(recipients);
					foreach (Attendee attendee2 in this.exceptionAttendeeCollection)
					{
						if (attendee2.IsDirty)
						{
							attendeeCollection.AddClone(attendee2);
						}
					}
					this.exceptionAttendeeCollection = attendeeCollection;
					this.occurrence.OccurrencePropertyBag.MasterCalendarItem.AbandonRecipientChanges();
					this.LoadMasterAttendeeCollection();
					this.BuildOccurrenceAttendeeCollection();
				}
			}
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x000BC258 File Offset: 0x000BA458
		private static Attendee FindAttendee(IList<Attendee> list, Participant participant, MailboxSession session, out int index)
		{
			index = -1;
			Attendee result = null;
			for (int i = 0; i < list.Count; i++)
			{
				Attendee attendee = list[i];
				if (Participant.HasSameEmail(attendee.Participant, participant, session, false))
				{
					result = attendee;
					index = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000BC29C File Offset: 0x000BA49C
		private static Attendee FindAttendee(IList<Attendee> list, Attendee match, MailboxSession session, out int index)
		{
			index = -1;
			foreach (Attendee attendee in list)
			{
				index++;
				if (Participant.HasSameEmail(attendee.Participant, match.Participant, session, false) && attendee.AttendeeType == match.AttendeeType)
				{
					return attendee;
				}
			}
			index = -1;
			return null;
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000BC314 File Offset: 0x000BA514
		private void Cleanup()
		{
			AttendeeCollection.Cleanup(this.masterAttendeeCollection);
			AttendeeCollection.Cleanup(this.exceptionAttendeeCollection);
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000BC32C File Offset: 0x000BA52C
		private void LoadIsDistributionList()
		{
			this.masterAttendeeCollection.LoadAdditionalParticipantProperties(new PropertyDefinition[]
			{
				ParticipantSchema.IsDistributionList
			});
			this.exceptionAttendeeCollection.LoadAdditionalParticipantProperties(new PropertyDefinition[]
			{
				ParticipantSchema.IsDistributionList
			});
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000BC370 File Offset: 0x000BA570
		private void LoadMasterAttendeeCollection()
		{
			CoreRecipientCollection recipients = this.occurrence.OccurrencePropertyBag.MasterCalendarItem.CoreItem.Recipients;
			this.masterAttendeeCollection = new AttendeeCollection(recipients);
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000BC3A4 File Offset: 0x000BA5A4
		private void MergeCollections()
		{
			Dictionary<string, Attendee> uniqueExceptionAttendees = this.GetUniqueExceptionAttendees();
			this.JoinAttendees(uniqueExceptionAttendees);
			this.RemoveExceptionalDeletedAttendees(uniqueExceptionAttendees);
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000BC3C8 File Offset: 0x000BA5C8
		private Dictionary<string, Attendee> GetUniqueExceptionAttendees()
		{
			Dictionary<string, Attendee> dictionary = new Dictionary<string, Attendee>();
			foreach (Attendee attendee in this.exceptionAttendeeCollection)
			{
				string attendeeKey = attendee.GetAttendeeKey();
				if (!dictionary.ContainsKey(attendeeKey))
				{
					dictionary.Add(attendeeKey, attendee);
				}
				else
				{
					Attendee attendee2;
					dictionary.TryGetValue(attendeeKey, out attendee2);
					RecipientFlags recipientFlags = attendee2.RecipientFlags;
					if ((recipientFlags & RecipientFlags.ExceptionalDeleted) == RecipientFlags.ExceptionalDeleted)
					{
						dictionary.Remove(attendeeKey);
						dictionary.Add(attendeeKey, attendee);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000BC460 File Offset: 0x000BA660
		private void JoinAttendees(Dictionary<string, Attendee> uniqueExceptionAttendees)
		{
			foreach (Attendee item in this.masterAttendeeCollection)
			{
				this.attendeeCollection.Add(item);
			}
			this.masterAttendeeCount = this.masterAttendeeCollection.Count;
			if (this.masterAttendeeCollection.CoreItem != this.exceptionAttendeeCollection.CoreItem)
			{
				foreach (KeyValuePair<string, Attendee> keyValuePair in uniqueExceptionAttendees)
				{
					RecipientFlags recipientFlags = keyValuePair.Value.RecipientFlags;
					if ((recipientFlags & RecipientFlags.ExceptionalDeleted) != RecipientFlags.ExceptionalDeleted)
					{
						this.AddExceptionalAttendee(keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000BC534 File Offset: 0x000BA734
		private void RemoveExceptionalDeletedAttendees(Dictionary<string, Attendee> uniqueExceptionAttendees)
		{
			int num = 0;
			List<Attendee> list = new List<Attendee>();
			foreach (KeyValuePair<string, Attendee> keyValuePair in uniqueExceptionAttendees)
			{
				Attendee value = keyValuePair.Value;
				if (value.Participant.RoutingType != null && value.Participant.EmailAddress != null)
				{
					string key = keyValuePair.Key;
					RecipientFlags recipientFlags = value.RecipientFlags;
					if ((recipientFlags & RecipientFlags.ExceptionalDeleted) == RecipientFlags.ExceptionalDeleted)
					{
						int i = 0;
						while (i < this.attendeeCollection.Count)
						{
							Attendee attendee = this.attendeeCollection[i];
							string attendeeKey = attendee.GetAttendeeKey();
							if (key == attendeeKey)
							{
								list.Add(attendee);
								if (i < this.masterAttendeeCount)
								{
									num++;
									break;
								}
								break;
							}
							else
							{
								i++;
							}
						}
					}
				}
			}
			this.masterAttendeeCount -= num;
			foreach (Attendee item in list)
			{
				this.attendeeCollection.Remove(item);
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000BC670 File Offset: 0x000BA870
		private void AddExceptionalAttendee(Attendee attendee)
		{
			int num;
			Attendee attendee2 = OccurrenceAttendeeCollection.FindAttendee(this.attendeeCollection, attendee, this.occurrence.Session as MailboxSession, out num);
			if (attendee2 != null && num < this.masterAttendeeCount)
			{
				this.attendeeCollection.RemoveAt(num);
			}
			this.attendeeCollection.Add(attendee);
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000BC6C0 File Offset: 0x000BA8C0
		private void BuildOccurrenceAttendeeCollection()
		{
			this.attendeeCollection.Clear();
			this.Cleanup();
			this.LoadIsDistributionList();
			this.MergeCollections();
		}

		// Token: 0x04001906 RID: 6406
		private readonly CalendarItemOccurrence occurrence;

		// Token: 0x04001907 RID: 6407
		private readonly List<Attendee> attendeeCollection = new List<Attendee>();

		// Token: 0x04001908 RID: 6408
		private int masterAttendeeCount;

		// Token: 0x04001909 RID: 6409
		private AttendeeCollection masterAttendeeCollection;

		// Token: 0x0400190A RID: 6410
		private AttendeeCollection exceptionAttendeeCollection;
	}
}
