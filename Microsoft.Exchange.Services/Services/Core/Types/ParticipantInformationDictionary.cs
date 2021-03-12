using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200083C RID: 2108
	internal class ParticipantInformationDictionary
	{
		// Token: 0x06003CD2 RID: 15570 RVA: 0x000D6758 File Offset: 0x000D4958
		internal ParticipantInformationDictionary()
		{
			ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug<int>((long)this.GetHashCode(), "ParticipantInformationDictionary constructed. Hashcode = {0}", this.GetHashCode());
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x000D678C File Offset: 0x000D498C
		internal bool ContainsParticipant(IParticipant participant)
		{
			return this.dictionary.ContainsKey(participant);
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x000D679A File Offset: 0x000D499A
		internal void AddParticipant(IParticipant participant, ParticipantInformation participantInformation)
		{
			if (!this.dictionary.ContainsKey(participant))
			{
				this.dictionary.Add(participant, participantInformation);
			}
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x000D67B7 File Offset: 0x000D49B7
		internal ParticipantInformation GetParticipant(IParticipant participant)
		{
			return this.dictionary[participant];
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x000D67C8 File Offset: 0x000D49C8
		internal ParticipantInformation GetParticipantInformationOrCreateNew(IParticipant participant)
		{
			ParticipantInformation participantInformation;
			if (!this.TryGetParticipant(participant, out participantInformation))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "Participant is null. Name='{0}';", participant.DisplayName);
				participantInformation = ParticipantInformationDictionary.ConvertToParticipantInformation(participant);
				this.AddParticipant(participant, participantInformation);
			}
			return participantInformation;
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x000D680C File Offset: 0x000D4A0C
		internal bool TryGetParticipantFromDictionary(IParticipant participant, out ParticipantInformation participantInformation)
		{
			return this.dictionary.TryGetValue(participant, out participantInformation) && participantInformation != null;
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000D6828 File Offset: 0x000D4A28
		internal bool TryGetParticipant(IParticipant participant, out ParticipantInformation participantInformation)
		{
			bool flag = this.dictionary.TryGetValue(participant, out participantInformation) && participantInformation != null;
			if (flag && participantInformation.DisplayName != null && participant.DisplayName != null && string.Compare(participantInformation.DisplayName, participant.DisplayName, false) != 0)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, string.Format("ParticipantInformationDictionary.TryGetParticipant - Using display name {0} for emailAddress {1}", participant.DisplayName, participantInformation.EmailAddress));
				ParticipantInformation participantInformation2 = new ParticipantInformation(participant.DisplayName, participantInformation.RoutingType, participantInformation.EmailAddress, participantInformation.Origin, participantInformation.Demoted, participantInformation.SipUri, participantInformation.Submitted, new MailboxHelper.MailboxTypeType?(participantInformation.MailboxType));
				participantInformation = participantInformation2;
			}
			return flag;
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x000D68E8 File Offset: 0x000D4AE8
		internal static ParticipantInformation ConvertToParticipantInformation(IParticipant participant)
		{
			if (participant.SmtpEmailAddress != null)
			{
				return new ParticipantInformation(participant.DisplayName, "SMTP", participant.SmtpEmailAddress, participant.Origin, new bool?(true), participant.SipUri, new bool?(participant.Submitted));
			}
			return new ParticipantInformation(participant.DisplayName, participant.RoutingType, participant.EmailAddress, participant.Origin, new bool?(true), participant.SipUri, new bool?(participant.Submitted));
		}

		// Token: 0x0400218C RID: 8588
		private readonly Dictionary<IParticipant, ParticipantInformation> dictionary = new Dictionary<IParticipant, ParticipantInformation>(ParticipantComparer.EmailAddress);
	}
}
