using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200039A RID: 922
	internal sealed class UpdateMailboxAssociation : SingleStepServiceCommand<UpdateMailboxAssociationRequest, ServiceResultNone>
	{
		// Token: 0x060019F4 RID: 6644 RVA: 0x000954AC File Offset: 0x000936AC
		public UpdateMailboxAssociation(CallContext callContext, UpdateMailboxAssociationRequest request) : base(callContext, request)
		{
			OwsLogRegistry.Register(base.GetType().Name, typeof(UpdateMailboxAssociationMetadata), new Type[0]);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000954D8 File Offset: 0x000936D8
		internal override IExchangeWebMethodResponse GetResponse()
		{
			UpdateMailboxAssociationResponse updateMailboxAssociationResponse = new UpdateMailboxAssociationResponse();
			updateMailboxAssociationResponse.ProcessServiceResult(base.Result);
			return updateMailboxAssociationResponse;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000954F8 File Offset: 0x000936F8
		internal override bool InternalPreExecute()
		{
			if (CallContext.Current.LogonType != LogonType.Admin || CallContext.Current.LogonTypeSource != LogonTypeSource.OpenAsAdminOrSystemServiceHeader)
			{
				throw new ServiceAccessDeniedException();
			}
			return base.InternalPreExecute();
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0009556C File Offset: 0x0009376C
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			GroupMailboxAccessLayer.Execute("UpdateSlaveDataFromMailboxAssociation", adrecipientSession, base.MailboxIdentityMailboxSession, delegate(GroupMailboxAccessLayer accessLayer)
			{
				accessLayer.UpdateSlaveDataFromMailboxAssociation(base.Request.Master, base.Request.Association);
				if (base.Request.Master.MailboxType == GroupMailboxLocator.MailboxLocatorType)
				{
					this.TryFindGroupInAD(accessLayer);
				}
			});
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x000955B4 File Offset: 0x000937B4
		private void TryFindGroupInAD(GroupMailboxAccessLayer accessLayer)
		{
			if (base.Request.Master == null)
			{
				this.LogWarning(accessLayer, "UpdateMailboxAssociation.TryFindGroupInAD", "Request.Master is null; Skipping group AD lookup.");
				return;
			}
			string domainController = base.Request.Master.DomainController;
			if (string.IsNullOrWhiteSpace(domainController))
			{
				UpdateMailboxAssociation.Tracer.TraceDebug((long)this.GetHashCode(), "UpdateMailboxAssociation.TryFindGroupInAD: DomainController hint is empty; Skipping group AD lookup.");
				return;
			}
			if (string.IsNullOrWhiteSpace(base.Request.Master.SmtpAddress))
			{
				this.LogWarning(accessLayer, "UpdateMailboxAssociation.TryFindGroupInAD", "Request.Master.SmtpAddress is null or whitespace; Skipping group AD lookup.");
				return;
			}
			ProxyAddress proxyAddress = new SmtpProxyAddress(base.Request.Master.SmtpAddress, true);
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			IRecipientSession tenantOrRootRecipientReadOnlySession = DirectorySessionFactory.Default.GetTenantOrRootRecipientReadOnlySession(adrecipientSession, domainController, 143, "TryFindGroupInAD", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\UpdateMailboxAssociation.cs");
			ADUser aduser = tenantOrRootRecipientReadOnlySession.FindByProxyAddress(proxyAddress) as ADUser;
			base.CallContext.ProtocolLog.Set(UpdateMailboxAssociationMetadata.IsPopulateADUserInCacheSuccessful, aduser != null);
			if (aduser == null)
			{
				string message = string.Format("Unable to find ADUser identified by ProxyAddress={0}, LastUsedDC={1}", proxyAddress, tenantOrRootRecipientReadOnlySession.LastUsedDc);
				this.LogWarning(accessLayer, "UpdateMailboxAssociation.TryFindGroupInAD", message);
			}
			else
			{
				base.CallContext.ProtocolLog.Set(UpdateMailboxAssociationMetadata.ExchangeGuid, aduser.ExchangeGuid);
				base.CallContext.ProtocolLog.Set(UpdateMailboxAssociationMetadata.ExternalDirectoryObjectId, aduser.ExternalDirectoryObjectId);
			}
			OWAMiniRecipient owaminiRecipient = tenantOrRootRecipientReadOnlySession.FindMiniRecipientByProxyAddress<OWAMiniRecipient>(proxyAddress, OWAMiniRecipientSchema.AdditionalProperties);
			base.CallContext.ProtocolLog.Set(UpdateMailboxAssociationMetadata.IsPopulateMiniRecipientInCacheSuccessful, aduser != null);
			if (owaminiRecipient == null)
			{
				string message2 = string.Format("Unable to find OWAMiniRecipient identified by ProxyAddress={0}, LastUsedDC={1}", proxyAddress, tenantOrRootRecipientReadOnlySession.LastUsedDc);
				this.LogWarning(accessLayer, "UpdateMailboxAssociation.TryFindGroupInAD", message2);
			}
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x00095770 File Offset: 0x00093970
		private void LogWarning(GroupMailboxAccessLayer accessLayer, string context, string message)
		{
			UpdateMailboxAssociation.Tracer.TraceWarning((long)this.GetHashCode(), string.Format("{0}: {1}", context, message));
			accessLayer.Logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.Warning>
			{
				{
					MailboxAssociationLogSchema.Warning.Context,
					context
				},
				{
					MailboxAssociationLogSchema.Warning.Message,
					message
				}
			});
		}

		// Token: 0x04001146 RID: 4422
		private static readonly Trace Tracer = ExTraceGlobals.AssociationReplicationTracer;
	}
}
