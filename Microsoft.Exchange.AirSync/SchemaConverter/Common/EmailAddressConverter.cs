using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000183 RID: 387
	internal static class EmailAddressConverter
	{
		// Token: 0x060010AE RID: 4270 RVA: 0x0005CF04 File Offset: 0x0005B104
		private static Participant GetCachedParticipant(string legDn)
		{
			Participant result = null;
			if (Command.CurrentCommand != null)
			{
				result = (Command.CurrentCommand.Context as AirSyncContext).GetFullParticipant(legDn);
			}
			return result;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0005CF31 File Offset: 0x0005B131
		private static void CacheParticipant(string legDN, Participant fullParticipant)
		{
			if (Command.CurrentCommand != null)
			{
				(Command.CurrentCommand.Context as AirSyncContext).CacheParticipant(legDN, fullParticipant);
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0005CF50 File Offset: 0x0005B150
		public static Participant CreateParticipant(string textString)
		{
			if (string.IsNullOrEmpty(textString))
			{
				throw new ArgumentNullException("Participant string null");
			}
			AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.CommonTracer, null, "CreateParticipant = {0}", textString);
			Participant participant = Participant.Parse(textString);
			EmailAddressConverter.ValidateParticipant(participant);
			return participant;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0005CF90 File Offset: 0x0005B190
		public static string GetParticipantString(Participant participant, IExchangePrincipal exchangePrincipal)
		{
			if (null == participant)
			{
				throw new ArgumentNullException("participant null");
			}
			StringBuilder stringBuilder = new StringBuilder(100);
			if (participant.DisplayName.StartsWith("\"") && participant.DisplayName.EndsWith("\""))
			{
				stringBuilder.Append(participant.DisplayName);
				stringBuilder.Append(" ");
			}
			else
			{
				stringBuilder.Append('"');
				stringBuilder.Append(participant.DisplayName);
				stringBuilder.Append("\" ");
			}
			stringBuilder.Append(EmailAddressConverter.LookupEmailAddressString(participant, exchangePrincipal, true));
			AirSyncDiagnostics.TraceInfo<StringBuilder>(ExTraceGlobals.CommonTracer, null, "GetParticipantString = {0}", stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0005D040 File Offset: 0x0005B240
		public static string GetRecipientString(RecipientCollection collection, RecipientItemType recipientItemType, IExchangePrincipal exchangePrincipal)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("Recipient collection null");
			}
			if (collection.Count == 0)
			{
				return string.Empty;
			}
			int num = 0;
			Participant[] array = new Participant[collection.Count];
			Participant[] array2 = new Participant[collection.Count];
			foreach (Recipient recipient in collection)
			{
				if (recipient == null)
				{
					throw new ArgumentNullException("recipient is null");
				}
				array2[num] = recipient.Participant;
				if (recipient.Participant.RoutingType == "EX" && !string.IsNullOrEmpty(recipient.Participant.EmailAddress))
				{
					Participant cachedParticipant = EmailAddressConverter.GetCachedParticipant(recipient.Participant.EmailAddress);
					array[num++] = ((cachedParticipant == null) ? recipient.Participant : cachedParticipant);
				}
				else
				{
					array[num++] = recipient.Participant;
				}
			}
			ParticipantUpdater.GetSMTPAddressesForParticipantsIfNecessary(array, collection);
			for (int i = 0; i < array.Length; i++)
			{
				Participant participant = array2[i];
				Participant participant2 = array[i];
				if (participant.RoutingType != participant2.RoutingType)
				{
					EmailAddressConverter.CacheParticipant(participant.EmailAddress, participant2);
				}
			}
			StringBuilder stringBuilder = new StringBuilder(collection.Count * 100);
			int num2 = 0;
			foreach (Recipient recipient2 in collection)
			{
				Participant participant3 = array[num2++];
				if (recipientItemType == RecipientItemType.Unknown || recipient2.RecipientItemType == recipientItemType)
				{
					if (participant3.DisplayName.StartsWith("\"") && participant3.DisplayName.EndsWith("\""))
					{
						stringBuilder.Append(participant3.DisplayName);
						stringBuilder.Append(" <");
					}
					else
					{
						stringBuilder.Append('"');
						stringBuilder.Append(participant3.DisplayName);
						stringBuilder.Append("\" ");
					}
					stringBuilder.Append(EmailAddressConverter.LookupEmailAddressString(participant3, exchangePrincipal, true));
					stringBuilder.Append(", ");
				}
				if (stringBuilder.Length > 32000)
				{
					break;
				}
			}
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Length -= 2;
			}
			AirSyncDiagnostics.TraceInfo<StringBuilder>(ExTraceGlobals.CommonTracer, null, "GetRecipientString = {0}", stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0005D2BC File Offset: 0x0005B4BC
		public static string LookupEmailAddressString(Participant participant, IExchangePrincipal exchangePrincipal)
		{
			return EmailAddressConverter.LookupEmailAddressString(participant, exchangePrincipal, false);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0005D2C8 File Offset: 0x0005B4C8
		public static string LookupEmailAddressString(Participant participant, IExchangePrincipal exchangePrincipal, bool addBrackets)
		{
			AirSyncDiagnostics.TraceDebug(ExTraceGlobals.CommonTracer, null, "Calling EmailAddressConverter.LookupEmailAddressString()");
			if (null == participant)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.CommonTracer, null, "participant is null.");
				throw new ArgumentNullException("participant null");
			}
			if (exchangePrincipal == null)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.CommonTracer, null, "value of exchange principal is null.");
				throw new ArgumentNullException("exchangePrincipal null");
			}
			if (string.IsNullOrEmpty(participant.EmailAddress))
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.CommonTracer, null, "participant's emailAddress is null. Returning Display name instead.");
				return EmailAddressConverter.AddBrackets(participant.DisplayName, "<{0}>", addBrackets);
			}
			if (participant.RoutingType == "EX")
			{
				Participant cachedParticipant = EmailAddressConverter.GetCachedParticipant(participant.EmailAddress);
				if (cachedParticipant != null)
				{
					participant = cachedParticipant;
				}
			}
			if (participant.RoutingType == "SMTP")
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.CommonTracer, null, "participant's is already of Routing Type SMTP. return participant's email address.");
				return EmailAddressConverter.AddBrackets(participant.EmailAddress, "<{0}>", addBrackets);
			}
			Participant participant2 = null;
			try
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.CommonTracer, null, "Fetching SmtpAddress property from MAPI");
				object obj = participant.TryGetProperty(ParticipantSchema.SmtpAddress);
				if (obj != null && obj is string)
				{
					return EmailAddressConverter.AddBrackets((string)obj, "<{0}>", addBrackets);
				}
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.CommonTracer, null, "calling TryConveryTo to convert Participant to SMTP");
				participant2 = EmailAddressConverter.DoADLookup(exchangePrincipal, participant);
			}
			catch (StoragePermanentException)
			{
				AirSyncDiagnostics.TraceError(ExTraceGlobals.CommonTracer, null, "StorePermanentException during ADLookUp while converting Participant to SMTP");
			}
			if (participant2 != null)
			{
				AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.CommonTracer, null, "Convert to SMTP successful. Returned EmailAddress {0} , Format {1}", participant2.EmailAddress, "<{0}>");
				if (participant.RoutingType == "EX")
				{
					EmailAddressConverter.CacheParticipant(participant.EmailAddress, participant2);
				}
				return EmailAddressConverter.AddBrackets(participant2.EmailAddress, "<{0}>", addBrackets);
			}
			AirSyncDiagnostics.TraceDebug<string, string, string>(ExTraceGlobals.CommonTracer, null, "Could not convert participant to SMTP routing type. Returned RoutingType:{0}, EmailAddress:{1} and Format: {2}", participant.RoutingType, participant.EmailAddress, "[{0}]");
			return EmailAddressConverter.AddBrackets(participant.RoutingType + ":" + participant.EmailAddress, "[{0}]", addBrackets);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0005D4CC File Offset: 0x0005B6CC
		private static Participant DoADLookup(IExchangePrincipal exchangePrincipal, Participant participant)
		{
			if (TestHooks.EmailAddressConverter_ADLookup != null)
			{
				return TestHooks.EmailAddressConverter_ADLookup(participant);
			}
			Participant[] array = Participant.TryConvertTo(new Participant[]
			{
				participant
			}, "SMTP", exchangePrincipal, null);
			if (array == null || array.Length == 0)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.CommonTracer, null, ("Convert to SMTP failed. Received null smtpParticipant for participant: " + participant.EmailAddress) ?? "<NULL>");
				return null;
			}
			return array[0];
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0005D538 File Offset: 0x0005B738
		public static void SetRecipientCollection(RecipientCollection collection, RecipientItemType recipientItemType, string textString)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("Recipient collection null");
			}
			if (textString == null)
			{
				throw new ArgumentNullException("Recipient string null");
			}
			string[] array = ParseRecipientHelper.ParseRecipientChunk(textString);
			EmailAddressConverter.ClearRecipients(collection, recipientItemType);
			for (int i = 0; i < array.Length; i++)
			{
				if (!string.IsNullOrEmpty(array[i]))
				{
					Participant participant = Participant.Parse(array[i]);
					EmailAddressConverter.ValidateParticipant(participant);
					collection.Add(participant, recipientItemType);
				}
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0005D5A0 File Offset: 0x0005B7A0
		public static void ClearRecipients(RecipientCollection collection, RecipientItemType recipientItemType)
		{
			for (int i = collection.Count - 1; i >= 0; i--)
			{
				if (collection[i].RecipientItemType == recipientItemType)
				{
					collection.RemoveAt(i);
				}
			}
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0005D5D6 File Offset: 0x0005B7D6
		private static string AddBrackets(string address, string bracketFormat, bool addBrackets)
		{
			if (addBrackets)
			{
				return string.Format(bracketFormat, address);
			}
			return address;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0005D5E4 File Offset: 0x0005B7E4
		private static void ValidateParticipant(Participant participant)
		{
			E164Number e164Number;
			if (string.Equals(participant.RoutingType, "MOBILE", StringComparison.OrdinalIgnoreCase) && !E164Number.TryParse(participant.EmailAddress, out e164Number))
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, false)
				{
					ErrorStringForProtocolLogger = "BadMobileParticipantString"
				};
			}
		}

		// Token: 0x04000AE0 RID: 2784
		public const int MaxRecipientStringLength = 32000;

		// Token: 0x04000AE1 RID: 2785
		private const string SmtpFormat = "<{0}>";

		// Token: 0x04000AE2 RID: 2786
		private const string OneoffFormat = "[{0}]";
	}
}
