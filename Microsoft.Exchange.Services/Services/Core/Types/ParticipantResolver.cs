using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200083D RID: 2109
	internal sealed class ParticipantResolver : IParticipantResolver
	{
		// Token: 0x06003CDA RID: 15578 RVA: 0x000D6965 File Offset: 0x000D4B65
		private ParticipantResolver(ParticipantInformationDictionary participantInformationDictionary, MailboxTypeCache mailboxTypeCache, bool isOwa, IExchangePrincipal mailboxIdentity, string orgUnitName, AdParticipantLookup adParticipantLookup)
		{
			this.resolvedParticipantsMap = participantInformationDictionary;
			this.mailboxTypeCache = mailboxTypeCache;
			this.isOwa = isOwa;
			this.mailboxIdentity = mailboxIdentity;
			this.organizationalUnitName = orgUnitName;
			this.adParticipantLookup = adParticipantLookup;
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x000D699C File Offset: 0x000D4B9C
		public static IParticipantResolver Create(CallContext callContext, int maxParticipantsToResolve = 2147483647)
		{
			if (callContext != null && callContext.FeaturesManager != null && callContext.FeaturesManager.IsFeatureSupported("OptimizedParticipantResolver"))
			{
				AdParticipantLookup adParticipantLookup = new AdParticipantLookup(callContext, maxParticipantsToResolve);
				return new ParticipantResolver(EWSSettings.ParticipantInformation, MailboxTypeCache.Instance, callContext.IsOwa, callContext.MailboxIdentityPrincipal, callContext.OrganizationalUnitName, adParticipantLookup);
			}
			if (maxParticipantsToResolve == 2147483647)
			{
				return StaticParticipantResolver.DefaultInstance;
			}
			return new StaticParticipantResolver(maxParticipantsToResolve);
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003CDC RID: 15580 RVA: 0x000D6A05 File Offset: 0x000D4C05
		private IParticipant CallContextMailboxIdentity
		{
			get
			{
				if (this.callContextMailboxIdentity == null && this.mailboxIdentity != null)
				{
					this.callContextMailboxIdentity = new Participant(this.mailboxIdentity);
				}
				return this.callContextMailboxIdentity;
			}
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000D6A44 File Offset: 0x000D4C44
		public EmailAddressWrapper[] ResolveToEmailAddressWrapper(IEnumerable<IParticipant> participants)
		{
			HashSet<EmailAddressWrapper> hashSet = new HashSet<EmailAddressWrapper>(EmailAddressWrapper.AddressEqualityComparer);
			hashSet.UnionWith(from p in participants
			select EmailAddressWrapper.FromParticipantInformation(this.GetParticipantInformation(p)));
			return hashSet.ToArray<EmailAddressWrapper>();
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x000D6A7C File Offset: 0x000D4C7C
		public SingleRecipientType ResolveToSingleRecipientType(IParticipant participant)
		{
			if (participant == null)
			{
				return null;
			}
			ParticipantInformation participantInformation = this.GetParticipantInformation(participant);
			return PropertyCommand.CreateRecipientFromParticipant(participantInformation);
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x000D6A9C File Offset: 0x000D4C9C
		public SmtpAddress ResolveToSmtpAddress(IParticipant participant)
		{
			if (participant == null)
			{
				return new SmtpAddress(string.Empty);
			}
			ParticipantInformation participantInformation = this.GetParticipantInformation(participant);
			if (participantInformation.RoutingType != "SMTP")
			{
				this.LoadAdDataIfNeeded(new IParticipant[]
				{
					participant
				});
				participantInformation = this.GetParticipantInformation(participant);
			}
			return new SmtpAddress(participantInformation.EmailAddress);
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000D6AF8 File Offset: 0x000D4CF8
		public SingleRecipientType[] ResolveToSingleRecipientType(IEnumerable<IParticipant> participants)
		{
			List<SingleRecipientType> list = new List<SingleRecipientType>(participants.Count<IParticipant>());
			foreach (IParticipant participant in participants)
			{
				list.Add(this.ResolveToSingleRecipientType(participant));
			}
			return list.ToArray();
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000D6B58 File Offset: 0x000D4D58
		public void LoadAdDataIfNeeded(IEnumerable<IParticipant> pregatherParticipants)
		{
			IParticipant[] array = this.CalculateParticipantsToConvert(pregatherParticipants).ToArray<IParticipant>();
			IParticipant[] array2 = this.adParticipantLookup.LookUpAdParticipants(array);
			int num = 0;
			foreach (IParticipant participant in array2)
			{
				ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug<string, int>(0L, "ParticipantResolver.LoadAdDataIfNeeded - converted participant with EmailAddress = {0} and HashCode = {1}.", array[num].EmailAddress, array[num].GetHashCode());
				IParticipant participant2 = participant;
				bool value = false;
				if (participant == null || participant.EmailAddress == null)
				{
					participant2 = array[num];
					value = true;
				}
				ParticipantInformation info = ParticipantInformation.Create(participant2, MailboxHelper.GetMailboxType(participant2, this.isOwa), new bool?(value));
				this.UpdateCaches(array[num], info);
				num++;
			}
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000D6C04 File Offset: 0x000D4E04
		private ParticipantInformation GetParticipantInformation(IParticipant participant)
		{
			ParticipantInformation result;
			if (this.TryGetParticipantInformation(participant, out result))
			{
				return result;
			}
			return ParticipantInformation.Create(participant, MailboxHelper.GetMailboxType(participant, this.isOwa), null);
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000D6C3C File Offset: 0x000D4E3C
		private bool TryGetMailboxType(IParticipant participant, out MailboxHelper.MailboxTypeType mailboxType)
		{
			string key;
			MailboxHelper.MailboxTypeType? mailboxTypeType;
			if (MailboxTypeCache.TryGetKey(participant.SmtpEmailAddress, this.organizationalUnitName, out key) && this.mailboxTypeCache.TryGetValue(key, out mailboxTypeType))
			{
				mailboxType = mailboxTypeType.Value;
				return true;
			}
			mailboxType = MailboxHelper.GetMailboxType(participant, this.isOwa);
			return MailboxHelper.IsFullyResolvedMailboxType(mailboxType);
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x000D6C90 File Offset: 0x000D4E90
		private bool TryConstructParticipantInformation(IParticipant participant, out ParticipantInformation participantInformation)
		{
			participantInformation = null;
			bool flag = string.Equals(participant.RoutingType, "EX", StringComparison.OrdinalIgnoreCase);
			MailboxHelper.MailboxTypeType mailboxType;
			if (Global.FastParticipantResolveEnabled && flag && this.TryGetMailboxType(participant, out mailboxType))
			{
				ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug<string, int>(0L, "[ParticipantInformation::TryFastResolution] Found cached SMTP address and MailboxType: EmailAddress = {0}, HashCode = {1}", participant.SmtpEmailAddress, participant.GetHashCode());
				participantInformation = ParticipantInformation.CreateSmtpParticipant(participant, participant.DisplayName, participant.SmtpEmailAddress, mailboxType);
				return true;
			}
			if (ParticipantComparer.EmailAddress.Equals(participant, this.CallContextMailboxIdentity))
			{
				participantInformation = ParticipantInformation.CreateSmtpParticipant(participant, this.mailboxIdentity);
				return true;
			}
			if (!flag)
			{
				participantInformation = ParticipantInformation.Create(participant, MailboxHelper.GetMailboxType(participant, this.isOwa), null);
				return true;
			}
			return false;
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x000D6D44 File Offset: 0x000D4F44
		private HashSet<IParticipant> CalculateParticipantsToConvert(IEnumerable<IParticipant> participants)
		{
			HashSet<IParticipant> hashSet = new HashSet<IParticipant>(ParticipantComparer.EmailAddress);
			if (!participants.Any<IParticipant>())
			{
				return hashSet;
			}
			foreach (IParticipant participant in participants)
			{
				ParticipantInformation participantInformation;
				if (participant == null)
				{
					ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug(0L, "ParticipantResolver.CalculateParticipantsToConvert - found null entry");
				}
				else if (!this.TryGetParticipantInformation(participant, out participantInformation))
				{
					hashSet.Add(participant);
				}
			}
			return hashSet;
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000D6DC4 File Offset: 0x000D4FC4
		private bool TryGetParticipantInformation(IParticipant participant, out ParticipantInformation info)
		{
			if (this.resolvedParticipantsMap.TryGetParticipant(participant, out info))
			{
				ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug<string, int>(0L, "ToServiceObjectPropertyList.ConvertAndGetParticipantInformation - using already resolved participant for EmailAddress = {0}, HashCode = {1}", participant.EmailAddress, participant.GetHashCode());
				return true;
			}
			if (this.TryConstructParticipantInformation(participant, out info))
			{
				this.UpdateCaches(participant, info);
				return true;
			}
			return false;
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000D6E18 File Offset: 0x000D5018
		private void UpdateCaches(IParticipant participant, ParticipantInformation info)
		{
			this.UpdateMailboxTypeCache(info.EmailAddress, info.MailboxType);
			this.resolvedParticipantsMap.AddParticipant(participant, info);
			if (participant.RoutingType == "EX" && info.RoutingType == "SMTP")
			{
				Participant participant2 = new Participant(info.DisplayName, info.EmailAddress, "SMTP");
				if (!this.resolvedParticipantsMap.ContainsParticipant(participant2))
				{
					this.resolvedParticipantsMap.AddParticipant(participant2, info);
				}
			}
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x000D6E9C File Offset: 0x000D509C
		private void UpdateMailboxTypeCache(string smtpAddress, MailboxHelper.MailboxTypeType mailboxType)
		{
			string key = null;
			if (Global.FastParticipantResolveEnabled && MailboxTypeCache.TryGetKey(smtpAddress, this.organizationalUnitName, out key))
			{
				this.mailboxTypeCache.TryAdd(key, mailboxType);
			}
		}

		// Token: 0x0400218D RID: 8589
		private readonly ParticipantInformationDictionary resolvedParticipantsMap;

		// Token: 0x0400218E RID: 8590
		private readonly MailboxTypeCache mailboxTypeCache;

		// Token: 0x0400218F RID: 8591
		private readonly bool isOwa;

		// Token: 0x04002190 RID: 8592
		private readonly IExchangePrincipal mailboxIdentity;

		// Token: 0x04002191 RID: 8593
		private string organizationalUnitName;

		// Token: 0x04002192 RID: 8594
		private readonly AdParticipantLookup adParticipantLookup;

		// Token: 0x04002193 RID: 8595
		private Participant callContextMailboxIdentity;
	}
}
