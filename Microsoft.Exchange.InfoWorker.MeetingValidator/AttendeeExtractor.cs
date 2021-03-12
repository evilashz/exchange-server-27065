using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AttendeeExtractor
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00005044 File Offset: 0x00003244
		public AttendeeExtractor(IRecipientSession recipientSession)
		{
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			this.recipientSession = recipientSession;
			this.expansionManager = new ADRecipientExpansion(recipientSession, true, AttendeeExtractor.DLExpansionHandler.RecipientAdditionalRequiredProperties);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005073 File Offset: 0x00003273
		public IEnumerable<UserObject> ExtractAttendees(CalendarItemBase calendarItem, bool dlParticipantsOnly = false)
		{
			return this.ExtractAttendees(calendarItem.AttendeeCollection, calendarItem.Organizer, dlParticipantsOnly);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005088 File Offset: 0x00003288
		private static void RevisitAttendee(Dictionary<ADObjectId, UserObject> extractedAttendeeTable, Attendee attendee, ADRecipient attendeeRecipient)
		{
			UserObject userObject;
			if (!(attendeeRecipient is ADGroup) && extractedAttendeeTable.TryGetValue(attendeeRecipient.Id, out userObject) && (userObject.Attendee == null || userObject.Attendee.ReplyTime < attendee.ReplyTime))
			{
				userObject.Attendee = attendee;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000050FC File Offset: 0x000032FC
		private IEnumerable<UserObject> ExtractAttendees(IAttendeeCollection attendeeCollection, Participant organizerParticipant, bool dlParticipantsOnly)
		{
			Dictionary<ADObjectId, UserObject> dictionary = new Dictionary<ADObjectId, UserObject>(attendeeCollection.Count);
			HashSet<ADObjectId> hashSet = new HashSet<ADObjectId>();
			Dictionary<ProxyAddress, UserObject> dictionary2 = new Dictionary<ProxyAddress, UserObject>();
			int expandedDLCount = 0;
			foreach (Attendee attendee in attendeeCollection)
			{
				if (CalendarValidator.IsValidParticipant(attendee.Participant))
				{
					ProxyAddress attendeeProxyAddress = ProxyAddress.Parse(attendee.Participant.RoutingType, attendee.Participant.EmailAddress);
					ADRecipient attendeeRecipient = null;
					ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						attendeeRecipient = this.recipientSession.FindByProxyAddress(attendeeProxyAddress);
					});
					if (!adoperationResult.Succeeded || attendeeRecipient == null)
					{
						this.ExtractUnaccessibleAttendee(organizerParticipant, dictionary2, attendee, attendeeProxyAddress);
					}
					else if (hashSet.Contains(attendeeRecipient.Id))
					{
						AttendeeExtractor.RevisitAttendee(dictionary, attendee, attendeeRecipient);
					}
					else if (attendeeRecipient is ADGroup)
					{
						AttendeeExtractor.DLExpansionHandler dlexpansionHandler = new AttendeeExtractor.DLExpansionHandler(organizerParticipant, dictionary, hashSet, expandedDLCount, attendee, attendeeRecipient, this.recipientSession, this.expansionManager);
						expandedDLCount = dlexpansionHandler.ExpandDL();
					}
					else if (!dlParticipantsOnly)
					{
						hashSet.Add(attendeeRecipient.Id);
						AttendeeExtractor.DLExpansionHandler.AddOrganizerFilteredAttendee<ADObjectId>(dictionary, attendeeRecipient.Id, new UserObject(attendee, attendeeRecipient, this.recipientSession), organizerParticipant, this.recipientSession);
					}
				}
			}
			return dictionary.Values.Concat(dictionary2.Values);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000052A8 File Offset: 0x000034A8
		private void ExtractUnaccessibleAttendee(Participant organizerParticipant, Dictionary<ProxyAddress, UserObject> unaccessibleUsers, Attendee attendee, ProxyAddress attendeeProxyAddress)
		{
			if (!unaccessibleUsers.ContainsKey(attendeeProxyAddress))
			{
				AttendeeExtractor.DLExpansionHandler.AddOrganizerFilteredAttendee<ProxyAddress>(unaccessibleUsers, attendeeProxyAddress, new UserObject(attendee, null, this.recipientSession), organizerParticipant, this.recipientSession);
			}
		}

		// Token: 0x04000037 RID: 55
		private IRecipientSession recipientSession;

		// Token: 0x04000038 RID: 56
		private ADRecipientExpansion expansionManager;

		// Token: 0x02000019 RID: 25
		private sealed class DLExpansionHandler
		{
			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x000052D0 File Offset: 0x000034D0
			// (set) Token: 0x060000B9 RID: 185 RVA: 0x000052D8 File Offset: 0x000034D8
			public Participant OrganizerParticipant { get; private set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000BA RID: 186 RVA: 0x000052E1 File Offset: 0x000034E1
			// (set) Token: 0x060000BB RID: 187 RVA: 0x000052E9 File Offset: 0x000034E9
			public Dictionary<ADObjectId, UserObject> IndividualAttendees { get; private set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000BC RID: 188 RVA: 0x000052F2 File Offset: 0x000034F2
			// (set) Token: 0x060000BD RID: 189 RVA: 0x000052FA File Offset: 0x000034FA
			public HashSet<ADObjectId> AllVisitedAttendees { get; private set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000BE RID: 190 RVA: 0x00005303 File Offset: 0x00003503
			// (set) Token: 0x060000BF RID: 191 RVA: 0x0000530B File Offset: 0x0000350B
			public int ExpandedDLCount { get; private set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005314 File Offset: 0x00003514
			// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000531C File Offset: 0x0000351C
			public Attendee Attendee { get; private set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005325 File Offset: 0x00003525
			// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000532D File Offset: 0x0000352D
			public ADRawEntry AttendeeRawEntry { get; private set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005336 File Offset: 0x00003536
			// (set) Token: 0x060000C5 RID: 197 RVA: 0x0000533E File Offset: 0x0000353E
			public IRecipientSession Session { get; private set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005347 File Offset: 0x00003547
			// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000534F File Offset: 0x0000354F
			public ADRecipientExpansion ExpansionManager { get; private set; }

			// Token: 0x060000C8 RID: 200 RVA: 0x00005358 File Offset: 0x00003558
			public DLExpansionHandler(Participant organizerParticipant, Dictionary<ADObjectId, UserObject> individualAttendees, HashSet<ADObjectId> allVisitedAttendees, int expandedDLCount, Attendee attendee, ADRawEntry attendeeRawEntry, IRecipientSession session, ADRecipientExpansion expansionManager)
			{
				this.OrganizerParticipant = organizerParticipant;
				this.IndividualAttendees = individualAttendees;
				this.AllVisitedAttendees = allVisitedAttendees;
				this.ExpandedDLCount = expandedDLCount;
				this.Attendee = attendee;
				this.AttendeeRawEntry = attendeeRawEntry;
				this.Session = session;
				this.ExpansionManager = expansionManager;
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x000053A8 File Offset: 0x000035A8
			private bool ExpandAnotherDL()
			{
				if (this.ExpandedDLCount < Configuration.DLExpansionLimit)
				{
					this.ExpandedDLCount++;
					return true;
				}
				return false;
			}

			// Token: 0x060000CA RID: 202 RVA: 0x000053C8 File Offset: 0x000035C8
			private ExpansionControl OnRecipientExtracted(ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType)
			{
				if (!this.AllVisitedAttendees.Add(recipient.Id))
				{
					return ExpansionControl.Skip;
				}
				if (recipientExpansionType != ExpansionType.GroupMembership)
				{
					AttendeeExtractor.DLExpansionHandler.AddOrganizerFilteredAttendee<ADObjectId>(this.IndividualAttendees, recipient.Id, new UserObject(recipient, this.Session), this.OrganizerParticipant, this.Session);
					return ExpansionControl.Continue;
				}
				if (this.ExpandAnotherDL())
				{
					return ExpansionControl.Continue;
				}
				AttendeeExtractor.DLExpansionHandler.AddOrganizerFilteredAttendee<ADObjectId>(this.IndividualAttendees, recipient.Id, new UserObject(recipient, this.Session), this.OrganizerParticipant, this.Session);
				return ExpansionControl.Skip;
			}

			// Token: 0x060000CB RID: 203 RVA: 0x0000544F File Offset: 0x0000364F
			private ExpansionControl OnFailureEncountered(ExpansionFailure failure, ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType)
			{
				this.AllVisitedAttendees.Add(recipient.Id);
				return ExpansionControl.Skip;
			}

			// Token: 0x060000CC RID: 204 RVA: 0x00005464 File Offset: 0x00003664
			public static void AddOrganizerFilteredAttendee<KeyType>(Dictionary<KeyType, UserObject> attendeeTable, KeyType key, UserObject user, Participant organizer, IRecipientSession session)
			{
				if (!Participant.HasSameEmail(user.Participant, organizer, session))
				{
					attendeeTable.Add(key, user);
				}
			}

			// Token: 0x060000CD RID: 205 RVA: 0x0000547E File Offset: 0x0000367E
			public int ExpandDL()
			{
				this.ExpansionManager.Expand(this.AttendeeRawEntry, new ADRecipientExpansion.HandleRecipientDelegate(this.OnRecipientExtracted), new ADRecipientExpansion.HandleFailureDelegate(this.OnFailureEncountered));
				return this.ExpandedDLCount;
			}

			// Token: 0x04000039 RID: 57
			public static readonly PropertyDefinition[] RecipientAdditionalRequiredProperties = new PropertyDefinition[]
			{
				ADRecipientSchema.DisplayName,
				ADRecipientSchema.LegacyExchangeDN,
				ADRecipientSchema.RecipientTypeDetails,
				ADRecipientSchema.Alias,
				ADMailboxRecipientSchema.ExchangeGuid,
				ADMailboxRecipientSchema.Database,
				ADRecipientSchema.MasterAccountSid,
				ADObjectSchema.OrganizationId,
				ADUserSchema.CalendarRepairDisabled
			};
		}
	}
}
