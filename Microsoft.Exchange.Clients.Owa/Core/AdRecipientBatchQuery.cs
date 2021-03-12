using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000B9 RID: 185
	internal sealed class AdRecipientBatchQuery
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x0003623E File Offset: 0x0003443E
		public AdRecipientBatchQuery(IEnumerator<Participant> recipients, UserContext userContext)
		{
			this.legacyExchangeDNToRecipientDictionary = AdRecipientBatchQuery.FindByParticipantLegacyExchangeDNs(recipients, userContext);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00036253 File Offset: 0x00034453
		public AdRecipientBatchQuery(UserContext userContext, params string[] legacyDNs)
		{
			this.legacyExchangeDNToRecipientDictionary = AdRecipientBatchQuery.FindByLegacyExchangeDNs(legacyDNs, userContext);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00036268 File Offset: 0x00034468
		public static Result<ADRawEntry>[] FindAdResultsByLegacyExchangeDNs(IEnumerator<Participant> recipients, UserContext userContext)
		{
			List<string> list = new List<string>();
			while (recipients.MoveNext())
			{
				Participant participant = recipients.Current;
				if (participant.RoutingType == "EX" && !string.IsNullOrEmpty(participant.EmailAddress))
				{
					list.Add(participant.EmailAddress);
				}
			}
			if (list.Count > 0)
			{
				string[] legacyExchangeDNs = list.ToArray();
				IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
				return recipientSession.FindByLegacyExchangeDNs(legacyExchangeDNs, AdRecipientBatchQuery.recipientQueryProperties);
			}
			return null;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000362E4 File Offset: 0x000344E4
		public ADRecipient GetAdRecipient(string legacyExchangeDN)
		{
			if (legacyExchangeDN == null)
			{
				return null;
			}
			ADRecipient result = null;
			this.legacyExchangeDNToRecipientDictionary.TryGetValue(legacyExchangeDN, out result);
			return result;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00036308 File Offset: 0x00034508
		private static Dictionary<string, ADRecipient> FindByParticipantLegacyExchangeDNs(IEnumerator<Participant> recipients, UserContext userContext)
		{
			List<string> list = new List<string>();
			while (recipients.MoveNext())
			{
				Participant participant = recipients.Current;
				if (participant.RoutingType == "EX" && !string.IsNullOrEmpty(participant.EmailAddress))
				{
					list.Add(participant.EmailAddress.ToLowerInvariant());
				}
			}
			return AdRecipientBatchQuery.FindByLegacyExchangeDNs(list.ToArray(), userContext);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00036368 File Offset: 0x00034568
		private static Dictionary<string, ADRecipient> FindByLegacyExchangeDNs(string[] legacyExchangeDNs, UserContext userContext)
		{
			Dictionary<string, ADRecipient> dictionary = new Dictionary<string, ADRecipient>(StringComparer.OrdinalIgnoreCase);
			if (legacyExchangeDNs.Length > 0)
			{
				IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
				Result<ADRecipient>[] array = recipientSession.FindADRecipientsByLegacyExchangeDNs(legacyExchangeDNs);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Data != null)
					{
						dictionary[legacyExchangeDNs[i]] = array[i].Data;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x040004C1 RID: 1217
		private static PropertyDefinition[] recipientQueryProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.PrimarySmtpAddress,
			ADObjectSchema.Id,
			ADRecipientSchema.Alias,
			ADRecipientSchema.RecipientDisplayType,
			ADObjectSchema.ObjectClass,
			ADOrgPersonSchema.MobilePhone
		};

		// Token: 0x040004C2 RID: 1218
		private Dictionary<string, ADRecipient> legacyExchangeDNToRecipientDictionary;
	}
}
