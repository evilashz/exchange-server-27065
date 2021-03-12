using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000051 RID: 81
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UpdateGroupMailboxViaXSO : UpdateGroupMailboxBase
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x000160E0 File Offset: 0x000142E0
		public UpdateGroupMailboxViaXSO(IRecipientSession adSession, ADUser group, ExchangePrincipal groupMailboxPrincipal, ADUser executingUser, GroupMailboxConfigurationActionType forceActionMask, UserMailboxLocator[] addedMembers, UserMailboxLocator[] removedMembers, int? permissionsVersion) : base(group, executingUser, forceActionMask, permissionsVersion)
		{
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			this.addedMembers = addedMembers;
			this.removedMembers = removedMembers;
			this.adSession = adSession;
			this.groupMailboxPrincipal = groupMailboxPrincipal;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00016118 File Offset: 0x00014318
		public override void Execute()
		{
			using (MailboxSession mailboxSession = ConfigureGroupMailbox.CreateMailboxSessionForConfiguration(this.groupMailboxPrincipal, this.group.OriginatingServer))
			{
				this.ConfigureGroupMailboxIfRequired(mailboxSession);
				string arg = GroupMailboxContext.EnsureGroupIsInDirectoryCache("UpdateGroupMailboxViaXSO.Execute", this.adSession, this.group);
				UpdateGroupMailboxViaXSO.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}", arg);
				this.WriteMembersToGroupIfRequired(mailboxSession);
				this.SetPermissionsVersionIfRequired(mailboxSession);
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001619C File Offset: 0x0001439C
		private void ConfigureGroupMailboxIfRequired(MailboxSession mailboxSession)
		{
			if (this.group.IsGroupMailboxConfigured && this.forceActionMask == (GroupMailboxConfigurationActionType)0)
			{
				return;
			}
			ConfigureGroupMailbox configureGroupMailbox = new ConfigureGroupMailbox(this.adSession, this.group, this.executingUser, mailboxSession);
			try
			{
				GroupMailboxConfigurationReport groupMailboxConfigurationReport = configureGroupMailbox.Execute((GroupMailboxConfigurationAction)this.forceActionMask);
				foreach (KeyValuePair<GroupMailboxConfigurationAction, LatencyStatistics> keyValuePair in groupMailboxConfigurationReport.ConfigurationActionLatencyStatistics)
				{
					LatencyStatistics value = keyValuePair.Value;
					this.AppendAggregatedOperationStatisticsToCmdletLog(keyValuePair.Key, "AD", value.ADLatency);
					this.AppendAggregatedOperationStatisticsToCmdletLog(keyValuePair.Key, "Rpc", value.RpcLatency);
					this.AppendGenericLatencyToCmdletLog(keyValuePair.Key, (long)value.ElapsedTime.TotalMilliseconds);
				}
			}
			catch (LocalizedException ex)
			{
				UpdateGroupMailboxViaXSO.Tracer.TraceError<string>((long)this.GetHashCode(), "UpdateGroupMailboxViaXSO.ConfigureGroupMailboxIfRequired - Caught LocalizedException: {0}", ex.Message);
				base.Error = ex.LocalizedString;
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00016308 File Offset: 0x00014508
		private void WriteMembersToGroupIfRequired(MailboxSession mailboxSession)
		{
			if (this.addedMembers == null && this.removedMembers == null)
			{
				return;
			}
			GroupMailboxAccessLayer.Execute("UpdateGroupMailboxViaXSO.WriteMembersToGroupIfRequired", this.adSession, mailboxSession, delegate(GroupMailboxAccessLayer accessLayer)
			{
				GroupMailboxLocator group = GroupMailboxLocator.Instantiate(this.adSession, this.group);
				accessLayer.SetMembershipState(this.executingUser, this.addedMembers, this.removedMembers, group);
			});
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00016338 File Offset: 0x00014538
		private void SetPermissionsVersionIfRequired(MailboxSession mailboxSession)
		{
			if (this.permissionsVersion != null)
			{
				mailboxSession.Mailbox[MailboxSchema.GroupMailboxPermissionsVersion] = this.permissionsVersion.Value;
				mailboxSession.Mailbox.Save();
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00016384 File Offset: 0x00014584
		private void AppendAggregatedOperationStatisticsToCmdletLog(object key, string metric, AggregatedOperationStatistics? stats)
		{
			if (stats == null)
			{
				return;
			}
			this.AppendGenericLatencyToCmdletLog(string.Format("{0}.{1}C", key, metric), stats.Value.Count);
			this.AppendGenericLatencyToCmdletLog(string.Format("{0}.{1}", key, metric), (long)stats.Value.TotalMilliseconds);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000163D8 File Offset: 0x000145D8
		private void AppendGenericLatencyToCmdletLog(object key, long value)
		{
			CmdletLogger.SafeAppendColumn(RpsCommonMetadata.GenericLatency, key.ToString(), value.ToString(NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x04000142 RID: 322
		private static readonly Trace Tracer = ExTraceGlobals.CmdletsTracer;

		// Token: 0x04000143 RID: 323
		private readonly IRecipientSession adSession;

		// Token: 0x04000144 RID: 324
		private readonly ExchangePrincipal groupMailboxPrincipal;

		// Token: 0x04000145 RID: 325
		private readonly UserMailboxLocator[] addedMembers;

		// Token: 0x04000146 RID: 326
		private readonly UserMailboxLocator[] removedMembers;
	}
}
