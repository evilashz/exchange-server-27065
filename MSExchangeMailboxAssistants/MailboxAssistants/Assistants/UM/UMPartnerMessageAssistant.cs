using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.UM
{
	// Token: 0x0200010D RID: 269
	internal sealed class UMPartnerMessageAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase, IEventSkipNotification
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x0004783E File Offset: 0x00045A3E
		public UMPartnerMessageAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00047849 File Offset: 0x00045A49
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.NewMail) == MapiEventTypeFlags.NewMail && ObjectClass.IsUMPartnerMessage(mapiEvent.ObjectClass);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00047868 File Offset: 0x00045A68
		public void OnSkipEvent(MapiEvent mapiEvent, Exception ex)
		{
			if (this.IsEventInteresting(mapiEvent) && ex != null && !(ex is DeadMailboxException))
			{
				UMPartnerMessageAssistant.Tracer.TraceError<MapiEvent, Exception>((long)this.GetHashCode(), "UMPMA.OnSkipEvent({0}) - Error={1}", mapiEvent, ex);
				string obj = (ex != null) ? ex.Message : string.Empty;
				string obj2 = (ex != null && ex.InnerException != null) ? ex.InnerException.Message : string.Empty;
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMPartnerMessageEventSkippedError, null, new object[]
				{
					CommonUtil.ToEventLogString(obj),
					CommonUtil.ToEventLogString(obj2)
				});
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000478FC File Offset: 0x00045AFC
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession mailboxSession, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (item == null)
			{
				UMPartnerMessageAssistant.Tracer.TraceWarning<MapiEvent, StoreObject>((long)this.GetHashCode(), "UMPMA.HandleEvent: Ignoring evt={0}, item={1}", mapiEvent, item);
				return;
			}
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(mailboxSession.MailboxOwner.MailboxInfo.OrganizationId, null);
			ADUser aduser = iadrecipientLookup.LookupByExchangeGuid(mailboxSession.MailboxGuid) as ADUser;
			if (aduser == null)
			{
				UMPartnerMessageAssistant.Tracer.TraceWarning<Guid>((long)this.GetHashCode(), "UMPMA.HandleEvent: Could not find ADUser for mailbox {0} {1}", mailboxSession.MailboxGuid);
				throw new SkipException(new UserNotUmEnabledException(mailboxSession.MailboxGuid.ToString()));
			}
			if (!aduser.UMEnabled || aduser.UMMailboxPolicy == null || aduser.UMRecipientDialPlanId == null)
			{
				UMPartnerMessageAssistant.Tracer.TraceWarning(0L, "UMPMA.HandleEvent({0} - {1}) - Invalid user: UMEnabled({2}) UMMbxPol({3}) DialPlan({4})", new object[]
				{
					mailboxSession.MailboxGuid,
					aduser.DistinguishedName,
					aduser.UMEnabled,
					aduser.UMMailboxPolicy,
					aduser.UMRecipientDialPlanId
				});
				throw new SkipException(new UserNotUmEnabledException(aduser.DistinguishedName));
			}
			try
			{
				this.ProcessEvent(aduser, mailboxSession, item);
			}
			catch (Exception arg)
			{
				UMPartnerMessageAssistant.Tracer.TraceWarning<Guid, string, Exception>(0L, "UMPMA.HandleEvent({0} - {1}) - Exception:{2}", mailboxSession.MailboxGuid, aduser.DistinguishedName, arg);
				throw;
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00047A58 File Offset: 0x00045C58
		private static LocalizedString GetPartnerMessageDescription(StoreObject item)
		{
			return Strings.descUMPartnerMessage((string)XsoUtil.SafeGetProperty(item, MessageItemSchema.XMsExchangeUMPartnerContent, string.Empty), (string)XsoUtil.SafeGetProperty(item, MessageItemSchema.XMsExchangeUMPartnerStatus, string.Empty), (string)XsoUtil.SafeGetProperty(item, ItemSchema.InternetMessageId, string.Empty));
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00047AAC File Offset: 0x00045CAC
		private static void SafeDeleteItem(MailboxSession session, StoreId itemId)
		{
			Exception arg = null;
			try
			{
				session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					itemId
				});
			}
			catch (StorageTransientException ex)
			{
				arg = ex;
			}
			catch (StoragePermanentException ex2)
			{
				arg = ex2;
			}
			catch (InvalidOperationException ex3)
			{
				arg = ex3;
			}
			UMPartnerMessageAssistant.Tracer.TraceWarning<StoreId, Exception>(0L, "UMPMA.SafeDelete({0}): Exception={1}", itemId, arg);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00047B20 File Offset: 0x00045D20
		private static void LogMessageProcessingSuccessEvent(LocalizedString partnerMsg, ADUser user, IVersionedRpcTarget server)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMPartnerMessageSucceeded, null, new object[]
			{
				CommonUtil.ToEventLogString(partnerMsg),
				CommonUtil.ToEventLogString(user.DistinguishedName),
				CommonUtil.ToEventLogString(server.Name)
			});
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00047B70 File Offset: 0x00045D70
		private static void LogNoServersAvailableEvent(LocalizedString partnerMsg, ADUser user)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMPartnerMessageNoServersAvailable, partnerMsg.ToString(), new object[]
			{
				CommonUtil.ToEventLogString(partnerMsg),
				CommonUtil.ToEventLogString(user.DistinguishedName)
			});
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00047BC0 File Offset: 0x00045DC0
		private static void LogMessageProcessingFailedEvent(LocalizedString partnerMsg, ADUser user, string additionalInformation)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMPartnerMessageServerFailed, partnerMsg.ToString(), new object[]
			{
				CommonUtil.ToEventLogString(partnerMsg),
				CommonUtil.ToEventLogString(user.DistinguishedName),
				CommonUtil.ToEventLogString(additionalInformation)
			});
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00047C18 File Offset: 0x00045E18
		private void ProcessEvent(ADUser user, MailboxSession mailboxSession, StoreObject item)
		{
			Exception ex = null;
			StringBuilder stringBuilder = new StringBuilder();
			UMPartnerMessageAssistant.UMPartnerMessageRpcTargetPicker instance = UMPartnerMessageAssistant.UMPartnerMessageRpcTargetPicker.Instance;
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(user.OrganizationId);
			ProcessPartnerMessageRequest processPartnerMessageRequest = new ProcessPartnerMessageRequest();
			processPartnerMessageRequest.MailboxGuid = mailboxSession.MailboxGuid;
			processPartnerMessageRequest.TenantGuid = iadsystemConfigurationLookup.GetExternalDirectoryOrganizationId();
			processPartnerMessageRequest.ItemId = item.Id.ToBase64String();
			LocalizedString partnerMessageDescription = UMPartnerMessageAssistant.GetPartnerMessageDescription(item);
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				IVersionedRpcTarget versionedRpcTarget = instance.PickNextServer(user.UMRecipientDialPlanId.ObjectGuid, out num);
				if (versionedRpcTarget == null)
				{
					break;
				}
				try
				{
					ex = null;
					versionedRpcTarget.ExecuteRequest(processPartnerMessageRequest);
					UMPartnerMessageAssistant.LogMessageProcessingSuccessEvent(partnerMessageDescription, user, versionedRpcTarget);
					UMPartnerMessageAssistant.SafeDeleteItem(mailboxSession, item.Id);
					goto IL_151;
				}
				catch (RpcException ex2)
				{
					ex = ex2;
					UMPartnerMessageAssistant.Tracer.TraceWarning<string, RpcException>(0L, "UMPMA.ProcessEvent({0}): {1}", versionedRpcTarget.Name, ex2);
					stringBuilder.AppendLine(Strings.UMRpcError(versionedRpcTarget.Name, ex2.ErrorCode, ex2.Message));
					if (UMErrorCode.IsPermanent(ex2.ErrorCode))
					{
						throw new SkipException(Strings.descUMServerFailure(versionedRpcTarget.Name, partnerMessageDescription, user.DistinguishedName), ex2);
					}
					if (UMErrorCode.IsNetworkError(ex2.ErrorCode))
					{
						instance.ServerUnavailable(versionedRpcTarget);
					}
				}
				if (++num2 >= num)
				{
					goto IL_151;
				}
			}
			UMPartnerMessageAssistant.LogNoServersAvailableEvent(partnerMessageDescription, user);
			throw new TransientMailboxException(Strings.descUMServerNotAvailable(partnerMessageDescription, user.DistinguishedName), null, UMPartnerMessageAssistant.RetrySchedule);
			IL_151:
			if (ex != null)
			{
				UMPartnerMessageAssistant.LogMessageProcessingFailedEvent(partnerMessageDescription, user, stringBuilder.ToString());
				throw new TransientMailboxException(Strings.descUMAllServersFailed(partnerMessageDescription, user.DistinguishedName), ex, UMPartnerMessageAssistant.RetrySchedule);
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00047E7B File Offset: 0x0004607B
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00047E83 File Offset: 0x00046083
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00047E8B File Offset: 0x0004608B
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x040006FF RID: 1791
		private static readonly Trace Tracer = ExTraceGlobals.UMPartnerMessageTracer;

		// Token: 0x04000700 RID: 1792
		private static readonly RetrySchedule RetrySchedule = new RetrySchedule(FinalAction.Skip, TimeSpan.FromDays(1.0), new TimeSpan[]
		{
			TimeSpan.Zero,
			TimeSpan.FromSeconds(5.0),
			TimeSpan.FromMinutes(1.0),
			TimeSpan.FromMinutes(5.0),
			TimeSpan.FromMinutes(15.0),
			TimeSpan.FromHours(1.0)
		});

		// Token: 0x0200010E RID: 270
		private class UMPartnerMessageRpcTarget : UMVersionedRpcTargetBase
		{
			// Token: 0x06000B10 RID: 2832 RVA: 0x00047E93 File Offset: 0x00046093
			internal UMPartnerMessageRpcTarget(Server server) : base(server)
			{
			}

			// Token: 0x06000B11 RID: 2833 RVA: 0x00047E9C File Offset: 0x0004609C
			protected override UMVersionedRpcClientBase CreateRpcClient()
			{
				return new UMPartnerMessageRpcClient(base.Server.Fqdn);
			}
		}

		// Token: 0x0200010F RID: 271
		private class UMPartnerMessageRpcTargetPicker : UMServerRpcTargetPickerBase<IVersionedRpcTarget>
		{
			// Token: 0x06000B12 RID: 2834 RVA: 0x00047EAE File Offset: 0x000460AE
			protected override IVersionedRpcTarget CreateTarget(Server server)
			{
				return new UMPartnerMessageAssistant.UMPartnerMessageRpcTarget(server);
			}

			// Token: 0x04000701 RID: 1793
			public static readonly UMPartnerMessageAssistant.UMPartnerMessageRpcTargetPicker Instance = new UMPartnerMessageAssistant.UMPartnerMessageRpcTargetPicker();
		}
	}
}
