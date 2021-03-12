using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000042 RID: 66
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UserObject : IEquatable<UserObject>
	{
		// Token: 0x06000268 RID: 616 RVA: 0x0000DB9C File Offset: 0x0000BD9C
		internal UserObject(ADRecipient adRecipient)
		{
			UserObject.ThrowOnNullArgument(adRecipient, "adRecipient");
			this.exchangePrincipal = UserObject.GetExchangePrincipalFromAdRecipient(adRecipient);
			this.Participant = new Participant(adRecipient);
			this.Alias = adRecipient.Alias;
			this.recipientType = adRecipient.RecipientType;
			this.IsGroupMailbox = (adRecipient.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox);
			this.objectId = adRecipient.Id;
			if (this.exchangePrincipal != null)
			{
				this.EmailAddress = this.exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
				this.CalendarRepairDisabled = UserObject.IsCalendarRepairDisabled(adRecipient, this.exchangePrincipal);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000DC4C File Offset: 0x0000BE4C
		internal UserObject(Participant participant, IRecipientSession session)
		{
			UserObject.ThrowOnNullArgument(participant, "participant");
			this.Participant = participant;
			this.EmailAddress = participant.EmailAddress;
			ADRecipient adrecipient;
			if (!participant.TryGetADRecipient(session, out adrecipient))
			{
				Globals.ValidatorTracer.TraceDebug<Participant>(0L, "Unable to get the AD recipient with the specified EmailAddress ({0}).", participant);
			}
			if (adrecipient != null)
			{
				this.exchangePrincipal = UserObject.GetExchangePrincipalFromAdRecipient(adrecipient);
				this.objectId = adrecipient.Id;
				this.recipientType = adrecipient.RecipientType;
				this.IsGroupMailbox = (adrecipient.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox);
				this.EmailAddress = adrecipient.PrimarySmtpAddress.ToString();
				this.Alias = adrecipient.Alias;
			}
			this.CalendarRepairDisabled = UserObject.IsCalendarRepairDisabled(adrecipient, this.exchangePrincipal);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000DD10 File Offset: 0x0000BF10
		internal UserObject(ADRawEntry rawEntry, IRecipientSession session)
		{
			UserObject.ThrowOnNullArgument(rawEntry, "rawEntry");
			this.Participant = new Participant(rawEntry);
			this.recipientType = (RecipientType)rawEntry[ADRecipientSchema.RecipientType];
			this.IsGroupMailbox = ((RecipientTypeDetails)rawEntry[ADRecipientSchema.RecipientTypeDetails] == RecipientTypeDetails.GroupMailbox);
			this.EmailAddress = ((SmtpAddress)rawEntry[ADRecipientSchema.PrimarySmtpAddress]).ToString();
			this.Alias = (rawEntry[ADRecipientSchema.Alias] as string);
			this.objectId = rawEntry.Id;
			this.CalendarRepairDisabled = true;
			if (this.recipientType == RecipientType.UserMailbox)
			{
				ADObjectId adobjectId = rawEntry[ADMailboxRecipientSchema.Database] as ADObjectId;
				if (adobjectId != null)
				{
					this.exchangePrincipal = ExchangePrincipal.FromAnyVersionMailboxData((string)rawEntry[ADRecipientSchema.DisplayName], (Guid)rawEntry[ADMailboxRecipientSchema.ExchangeGuid], adobjectId.ObjectGuid, this.EmailAddress, (string)rawEntry[ADRecipientSchema.LegacyExchangeDN], this.objectId, this.recipientType, (SecurityIdentifier)rawEntry[ADRecipientSchema.MasterAccountSid], (OrganizationId)rawEntry[ADObjectSchema.OrganizationId], RemotingOptions.AllowCrossSite, false);
					if (this.exchangePrincipal != null)
					{
						this.CalendarRepairDisabled = (bool)rawEntry[ADUserSchema.CalendarRepairDisabled];
					}
				}
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000DE74 File Offset: 0x0000C074
		internal UserObject(Attendee attendee, ADRecipient attendeeRecipient, IRecipientSession session)
		{
			UserObject.ThrowOnNullArgument(attendee, "attendee");
			this.Attendee = attendee;
			if (attendeeRecipient != null)
			{
				this.objectId = attendeeRecipient.Id;
				this.recipientType = attendeeRecipient.RecipientType;
				this.IsGroupMailbox = (attendeeRecipient.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox);
				this.exchangePrincipal = UserObject.GetExchangePrincipalFromAdRecipient(attendeeRecipient);
				this.EmailAddress = attendeeRecipient.PrimarySmtpAddress.ToString();
				this.Alias = attendeeRecipient.Alias;
			}
			else
			{
				this.recipientType = RecipientType.Invalid;
				this.EmailAddress = attendee.Participant.EmailAddress;
				this.Alias = this.EmailAddress;
			}
			this.CalendarRepairDisabled = UserObject.IsCalendarRepairDisabled(attendeeRecipient, this.exchangePrincipal);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000DF38 File Offset: 0x0000C138
		private static ExchangePrincipal GetExchangePrincipalFromAdRecipient(ADRecipient recipient)
		{
			ExchangePrincipal result = null;
			if (recipient is ADUser && recipient.RecipientType == RecipientType.UserMailbox)
			{
				try
				{
					result = ExchangePrincipal.FromADUser(recipient.OrganizationId.ToADSessionSettings(), (ADUser)recipient, RemotingOptions.AllowCrossSite);
				}
				catch (UnableToFindServerForDatabaseException)
				{
					Globals.ValidatorTracer.TraceDebug<ADRecipient>(0L, "Unable to get the ExchangePrincipal for the specified ADRecipient: {0}. Server was not found for Database.", recipient);
				}
			}
			return result;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000DF98 File Offset: 0x0000C198
		private static void ThrowOnNullArgument(object parameter, string parameterName)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		private static bool IsCalendarRepairDisabled(ADRecipient adRecipient, ExchangePrincipal exchangePrincipal)
		{
			if (exchangePrincipal == null)
			{
				return true;
			}
			ADUser aduser = adRecipient as ADUser;
			return aduser == null || aduser.CalendarRepairDisabled;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
		internal void SetResponse(ResponseType response, ExDateTime time)
		{
			this.ResponseType = response;
			this.ReplyTime = time;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		public override string ToString()
		{
			return this.Alias ?? this.EmailAddress;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000DFEA File Offset: 0x0000C1EA
		internal ADObjectId ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000DFF2 File Offset: 0x0000C1F2
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000DFFA File Offset: 0x0000C1FA
		internal string EmailAddress { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000E003 File Offset: 0x0000C203
		internal ExchangePrincipal ExchangePrincipal
		{
			get
			{
				return this.exchangePrincipal;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000E00B File Offset: 0x0000C20B
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000E013 File Offset: 0x0000C213
		internal string Alias { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000E01C File Offset: 0x0000C21C
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000E024 File Offset: 0x0000C224
		internal Participant Participant { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000E02D File Offset: 0x0000C22D
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000E035 File Offset: 0x0000C235
		internal Attendee Attendee
		{
			get
			{
				return this.attendee;
			}
			set
			{
				this.attendee = value;
				this.SetResponse(value.ResponseType, value.ReplyTime);
				this.Participant = value.Participant;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000E05C File Offset: 0x0000C25C
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000E064 File Offset: 0x0000C264
		internal ResponseType ResponseType { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000E06D File Offset: 0x0000C26D
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000E075 File Offset: 0x0000C275
		internal ExDateTime ReplyTime { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000E07E File Offset: 0x0000C27E
		internal RecipientType RecipientType
		{
			get
			{
				return this.recipientType;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000E086 File Offset: 0x0000C286
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000E08E File Offset: 0x0000C28E
		internal bool CalendarRepairDisabled { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000E097 File Offset: 0x0000C297
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000E09F File Offset: 0x0000C29F
		internal bool IsGroupMailbox { get; private set; }

		// Token: 0x06000284 RID: 644 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			UserObject userObject = obj as UserObject;
			return userObject != null && this.Equals(userObject);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		public bool Equals(UserObject other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.ObjectId != null)
			{
				return this.ObjectId.Equals(other.ObjectId);
			}
			if (other.ObjectId != null)
			{
				return false;
			}
			if (this.Alias != null)
			{
				return string.Equals(this.Alias, other.Alias, StringComparison.OrdinalIgnoreCase);
			}
			return other.Alias == null && string.Equals(this.EmailAddress, other.EmailAddress, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000E13D File Offset: 0x0000C33D
		public override int GetHashCode()
		{
			if (this.ObjectId != null)
			{
				return this.ObjectId.GetHashCode();
			}
			if (this.Alias != null)
			{
				return this.Alias.GetHashCode();
			}
			return this.EmailAddress.GetHashCode();
		}

		// Token: 0x0400017C RID: 380
		private Attendee attendee;

		// Token: 0x0400017D RID: 381
		private readonly ADObjectId objectId;

		// Token: 0x0400017E RID: 382
		private readonly ExchangePrincipal exchangePrincipal;

		// Token: 0x0400017F RID: 383
		private readonly RecipientType recipientType;
	}
}
