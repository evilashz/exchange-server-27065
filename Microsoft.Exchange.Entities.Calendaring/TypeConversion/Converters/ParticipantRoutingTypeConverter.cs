using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.Entities;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x0200007E RID: 126
	internal class ParticipantRoutingTypeConverter : IParticipantRoutingTypeConverter
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0000B5B6 File Offset: 0x000097B6
		public ParticipantRoutingTypeConverter(IStoreSession session)
		{
			this.session = session.AssertNotNull("session");
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000B5DD File Offset: 0x000097DD
		public ConvertValue<Participant[], Participant[]> ConvertToEntity
		{
			get
			{
				return (Participant[] value) => this.Convert(value, "SMTP");
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000B5F9 File Offset: 0x000097F9
		public ConvertValue<Participant[], Participant[]> ConvertToStorage
		{
			get
			{
				return (Participant[] value) => this.Convert(value, "EX");
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000B607 File Offset: 0x00009807
		protected virtual IStoreSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B610 File Offset: 0x00009810
		public static Participant[] ConvertToSmtp(Participant[] participants, IMailboxSession mailboxSession)
		{
			Dictionary<string, Participant> dictionary = new Dictionary<string, Participant>();
			List<Participant> list = new List<Participant>();
			foreach (Participant participant in participants)
			{
				if (!dictionary.ContainsKey(participant.EmailAddress))
				{
					if (string.Equals(participant.RoutingType, "SMTP", StringComparison.OrdinalIgnoreCase))
					{
						dictionary.Add(participant.EmailAddress, participant);
					}
					else
					{
						string valueOrDefault = participant.GetValueOrDefault<string>(ParticipantSchema.SmtpAddress);
						if (!string.IsNullOrEmpty(valueOrDefault))
						{
							Participant value = new Participant(participant.DisplayName, valueOrDefault, "SMTP", participant.Origin, new KeyValuePair<PropertyDefinition, object>[0]);
							dictionary.Add(participant.EmailAddress, value);
						}
						else
						{
							list.Add(participant);
						}
					}
				}
			}
			if (list.Count > 0)
			{
				Participant[] array = list.ToArray();
				Participant[] array2 = ParticipantRoutingTypeConverter.ResolveParticipantsFromAD(array, "SMTP", mailboxSession);
				for (int j = 0; j < array.Length; j++)
				{
					if (!dictionary.ContainsKey(array[j].EmailAddress))
					{
						dictionary.Add(array[j].EmailAddress, array2[j] ?? array[j]);
					}
				}
			}
			Participant[] array3 = new Participant[participants.Length];
			for (int k = 0; k < participants.Length; k++)
			{
				array3[k] = dictionary[participants[k].EmailAddress];
			}
			return array3;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B75C File Offset: 0x0000995C
		protected virtual Participant[] Convert(Participant[] value, string destinationRoutingType)
		{
			IMailboxSession mailboxSession = this.Session as IMailboxSession;
			if (mailboxSession == null)
			{
				ExTraceGlobals.ConvertersTracer.TraceDebug<string>(0L, "Provided session ({0}) is not supported for participant conversion.", this.Session.GetType().Name);
				return value;
			}
			if (string.Equals(destinationRoutingType, "SMTP", StringComparison.OrdinalIgnoreCase))
			{
				return ParticipantRoutingTypeConverter.ConvertToSmtp(value, mailboxSession);
			}
			Participant[] array = ParticipantRoutingTypeConverter.ResolveParticipantsFromAD(value, destinationRoutingType, mailboxSession);
			Participant[] array2 = new Participant[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = (array[i] ?? value[i]);
			}
			return array2;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B7E0 File Offset: 0x000099E0
		private static Participant[] ResolveParticipantsFromAD(Participant[] participants, string destinationRoutingType, IMailboxSession mailboxSession)
		{
			ADObjectId searchRoot = null;
			IExchangePrincipal mailboxOwner = mailboxSession.MailboxOwner;
			if (mailboxOwner.MailboxInfo.Configuration.AddressBookPolicy != null)
			{
				searchRoot = DirectoryHelper.GetGlobalAddressListFromAddressBookPolicy(mailboxOwner.MailboxInfo.Configuration.AddressBookPolicy, mailboxSession.GetADConfigurationSession(true, ConsistencyMode.IgnoreInvalid));
			}
			Participant[] array = Participant.TryConvertTo(participants, destinationRoutingType, mailboxOwner, searchRoot);
			if (array == null)
			{
				MailboxSession mailboxSession2 = mailboxSession as MailboxSession;
				array = Participant.TryConvertTo(participants, destinationRoutingType, mailboxSession2);
			}
			if (array == null)
			{
				ExTraceGlobals.ConvertersTracer.TraceDebug<string>(0L, "Provided session ({0}) does not support participant conversion.", mailboxSession.GetType().Name);
				return participants;
			}
			return array;
		}

		// Token: 0x040000DD RID: 221
		public const string EntitiesRouting = "SMTP";

		// Token: 0x040000DE RID: 222
		public const string StorageRouting = "EX";

		// Token: 0x040000DF RID: 223
		private readonly IStoreSession session;

		// Token: 0x0200007F RID: 127
		public class PassThru : IParticipantRoutingTypeConverter
		{
			// Token: 0x06000325 RID: 805 RVA: 0x0000B865 File Offset: 0x00009A65
			private PassThru()
			{
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000326 RID: 806 RVA: 0x0000B86D File Offset: 0x00009A6D
			public static ParticipantRoutingTypeConverter.PassThru SingletonInstance
			{
				get
				{
					return ParticipantRoutingTypeConverter.PassThru.Instance;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000327 RID: 807 RVA: 0x0000B877 File Offset: 0x00009A77
			public ConvertValue<Participant[], Participant[]> ConvertToEntity
			{
				get
				{
					return (Participant[] value) => value;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B899 File Offset: 0x00009A99
			public ConvertValue<Participant[], Participant[]> ConvertToStorage
			{
				get
				{
					return (Participant[] value) => value;
				}
			}

			// Token: 0x040000E0 RID: 224
			private static readonly ParticipantRoutingTypeConverter.PassThru Instance = new ParticipantRoutingTypeConverter.PassThru();
		}
	}
}
