using System;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006A7 RID: 1703
	internal sealed class AdParticipantLookup
	{
		// Token: 0x06003499 RID: 13465 RVA: 0x000BDC44 File Offset: 0x000BBE44
		public AdParticipantLookup(CallContext callContext, int maxParticipantsToResolve = 2147483647)
		{
			this.callContext = callContext;
			this.maxBatchSizeToAdResolve = ((maxParticipantsToResolve > 0) ? maxParticipantsToResolve : int.MaxValue);
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000BDC98 File Offset: 0x000BBE98
		public IParticipant[] LookUpAdParticipants(IParticipant[] pregatherParticipants)
		{
			IParticipant[] convertedParticipants = new IParticipant[0];
			if (RequestDetailsLogger.Current != null)
			{
				RequestDetailsLogger.Current.TrackLatency<IParticipant[]>(EwsMetadata.ParticipantResolveLatency, () => convertedParticipants = this.InternalLookUpAdParticipants(pregatherParticipants));
			}
			else
			{
				convertedParticipants = this.InternalLookUpAdParticipants(pregatherParticipants);
			}
			return convertedParticipants;
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000BDD0C File Offset: 0x000BBF0C
		private IParticipant[] InternalLookUpAdParticipants(IParticipant[] participantsToConvert)
		{
			if (!participantsToConvert.Any<IParticipant>())
			{
				return new IParticipant[0];
			}
			IParticipant[] participantsSentToBeConverted = participantsToConvert.Take(this.maxBatchSizeToAdResolve).ToArray<IParticipant>();
			ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug<int, int>(0L, "AdParticipantLookup.InternalLookUpAdParticipants - {0} participants identified as needed to resolve. The set may be trimmed due to MaxBatchSizeToAdResolve({1})", participantsToConvert.Length, this.maxBatchSizeToAdResolve);
			IParticipant[] result;
			if (this.TryAdResolve(participantsSentToBeConverted, out result))
			{
				return result;
			}
			ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug(0L, "AdParticipantLookup.InternalLookUpAdParticipants - AD resolution didnt succeed. Or the AdRecipientSession was not present or the call returned null as result");
			return new IParticipant[0];
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000BDD78 File Offset: 0x000BBF78
		private bool TryAdResolve(IParticipant[] participantsSentToBeConverted, out IParticipant[] convertedParticipants)
		{
			convertedParticipants = null;
			IRecipientSession recipientSession = (this.callContext != null) ? this.callContext.ADRecipientSessionContext.GetADRecipientSession() : null;
			if (recipientSession == null)
			{
				return false;
			}
			convertedParticipants = Participant.TryConvertTo(participantsSentToBeConverted.Cast<Participant>().ToArray<Participant>(), "SMTP", recipientSession);
			return convertedParticipants != null;
		}

		// Token: 0x04001DA8 RID: 7592
		private readonly int maxBatchSizeToAdResolve;

		// Token: 0x04001DA9 RID: 7593
		private readonly CallContext callContext;
	}
}
