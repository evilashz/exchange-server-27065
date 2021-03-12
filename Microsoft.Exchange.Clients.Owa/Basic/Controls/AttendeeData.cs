using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x020000A7 RID: 167
	internal class AttendeeData
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00030559 File Offset: 0x0002E759
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x00030561 File Offset: 0x0002E761
		public AttendeeType AttendeeType
		{
			get
			{
				return this.attendeeType;
			}
			set
			{
				this.attendeeType = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0003056A File Offset: 0x0002E76A
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00030572 File Offset: 0x0002E772
		public Participant Participant
		{
			get
			{
				return this.participant;
			}
			set
			{
				this.participant = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0003057B File Offset: 0x0002E77B
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00030583 File Offset: 0x0002E783
		public byte[] RecipientIdBytes
		{
			get
			{
				return this.recipientIdBytes;
			}
			set
			{
				this.recipientIdBytes = value;
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0003058C File Offset: 0x0002E78C
		public AttendeeData()
		{
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00030594 File Offset: 0x0002E794
		public AttendeeData(AttendeeData other)
		{
			this.attendeeType = other.attendeeType;
			this.participant = AttendeeData.CloneParticipant(other.participant);
			this.recipientIdBytes = (byte[])other.recipientIdBytes.Clone();
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000305CF File Offset: 0x0002E7CF
		public AttendeeData(Participant participant, AttendeeType attendeeType)
		{
			this.attendeeType = attendeeType;
			this.participant = AttendeeData.CloneParticipant(participant);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000305EA File Offset: 0x0002E7EA
		public AttendeeData(Attendee attendee)
		{
			this.attendeeType = attendee.AttendeeType;
			this.participant = AttendeeData.CloneParticipant(attendee.Participant);
			this.recipientIdBytes = attendee.Id.GetBytes();
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00030620 File Offset: 0x0002E820
		public static Participant CloneParticipant(Participant participant)
		{
			Participant result = null;
			if (participant != null)
			{
				result = new Participant(participant.DisplayName, participant.EmailAddress, participant.RoutingType, participant.Origin, new KeyValuePair<PropertyDefinition, object>[0]);
			}
			return result;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00030660 File Offset: 0x0002E860
		public static bool AreListsEqual(List<AttendeeData> list1, List<AttendeeData> list2)
		{
			if (list1 == null != (list2 == null))
			{
				return false;
			}
			if (list1 == null)
			{
				return true;
			}
			if (list1.Count != list2.Count)
			{
				return false;
			}
			for (int i = 0; i < list1.Count; i++)
			{
				AttendeeData attendeeData = list1[i];
				AttendeeData obj = list2[i];
				if (!attendeeData.Equals(obj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000306BC File Offset: 0x0002E8BC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			AttendeeData attendeeData = obj as AttendeeData;
			if (attendeeData == null)
			{
				return false;
			}
			if (this.attendeeType != attendeeData.attendeeType)
			{
				return false;
			}
			if (this.participant == null != (attendeeData.participant == null))
			{
				return false;
			}
			if (this.participant != null && !this.participant.Equals(attendeeData.participant))
			{
				return false;
			}
			if (this.recipientIdBytes == null != (attendeeData.recipientIdBytes == null))
			{
				return false;
			}
			if (this.recipientIdBytes != null)
			{
				if (this.recipientIdBytes.Length != attendeeData.recipientIdBytes.Length)
				{
					return false;
				}
				for (int i = 0; i < this.recipientIdBytes.Length; i++)
				{
					if (this.recipientIdBytes[i] != attendeeData.recipientIdBytes[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00030784 File Offset: 0x0002E984
		public override int GetHashCode()
		{
			int num = (int)this.attendeeType;
			if (this.participant != null)
			{
				num += this.participant.GetHashCode();
			}
			if (this.recipientIdBytes != null)
			{
				num += this.recipientIdBytes.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000465 RID: 1125
		private AttendeeType attendeeType;

		// Token: 0x04000466 RID: 1126
		private Participant participant;

		// Token: 0x04000467 RID: 1127
		private byte[] recipientIdBytes;
	}
}
