using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200031F RID: 799
	internal sealed class GetPersona : SingleStepServiceCommand<GetPersonaRequest, GetPersonaResponseMessage>
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x00076D7C File Offset: 0x00074F7C
		public GetPersona(CallContext callContext, GetPersonaRequest request) : base(callContext, request)
		{
			this.personaId = request.PersonaId;
			this.emailAddress = request.EmailAddress;
			this.parentFolderId = request.ParentFolderId;
			if (this.personaId != null && this.personaId.Id != null)
			{
				this.hashCode = this.personaId.Id.GetHashCode();
				return;
			}
			if (this.emailAddress != null && !string.IsNullOrEmpty(this.emailAddress.EmailAddress))
			{
				this.hashCode = this.emailAddress.EmailAddress.GetHashCode();
				return;
			}
			this.hashCode = 0;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00076E19 File Offset: 0x00075019
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetPersonaResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00076E44 File Offset: 0x00075044
		internal override ServiceResult<GetPersonaResponseMessage> Execute()
		{
			GetPersona.Tracer.TraceDebug<string, string>((long)this.hashCode, "GetPersona.Execute: Entering, with PersonaId {0}, EmailAddress: {1}.", (this.personaId == null) ? "(null)" : this.personaId.Id, (this.emailAddress == null) ? "(null)" : this.emailAddress.EmailAddress);
			GetPersonaResponseMessage getPersonaResponseMessage = new GetPersonaResponseMessage();
			MailboxSession mailboxIdentityMailboxSession = base.GetMailboxIdentityMailboxSession();
			if ((this.personaId == null || string.IsNullOrEmpty(this.personaId.Id)) && (this.emailAddress == null || string.IsNullOrEmpty(this.emailAddress.EmailAddress)))
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)3784063568U);
			}
			Persona persona;
			if (this.parentFolderId != null && this.parentFolderId.BaseFolderId != null)
			{
				IdAndSession idAndSession = new IdConverter(base.CallContext).ConvertFolderIdToIdAndSession(this.parentFolderId.BaseFolderId, IdConverter.ConvertOption.IgnoreChangeKey);
				persona = Persona.LoadFromPersonaId((PublicFolderSession)idAndSession.Session, null, this.personaId, Persona.FullPersonaShape, null, idAndSession.Id);
			}
			else if (this.emailAddress != null)
			{
				persona = Persona.LoadFromEmailAddressWithGalAggregation(mailboxIdentityMailboxSession, base.CallContext.ADRecipientSessionContext.GetGALScopedADRecipientSession(base.CallContext.EffectiveCaller.ClientSecurityContext), this.emailAddress, Persona.FullPersonaShape);
			}
			else
			{
				persona = Persona.LoadFromPersonaIdWithGalAggregation(mailboxIdentityMailboxSession, base.CallContext.ADRecipientSessionContext.GetGALScopedADRecipientSession(base.CallContext.EffectiveCaller.ClientSecurityContext), this.personaId, Persona.FullPersonaShape, null);
			}
			if (persona == null)
			{
				GetPersona.Tracer.TraceDebug((long)this.hashCode, "GetPersona.Execute: No Persona found for the given identity, throwing object not found error.");
				throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
			}
			getPersonaResponseMessage.Persona = persona;
			GetPersona.Tracer.TraceDebug((long)this.hashCode, "GetPersona.Execute: Exiting, Persona found for the given identity.");
			return new ServiceResult<GetPersonaResponseMessage>(getPersonaResponseMessage);
		}

		// Token: 0x04000F34 RID: 3892
		private static readonly Trace Tracer = ExTraceGlobals.GetPersonaCallTracer;

		// Token: 0x04000F35 RID: 3893
		private readonly ItemId personaId;

		// Token: 0x04000F36 RID: 3894
		private readonly EmailAddressWrapper emailAddress;

		// Token: 0x04000F37 RID: 3895
		private TargetFolderId parentFolderId;

		// Token: 0x04000F38 RID: 3896
		private readonly int hashCode;
	}
}
