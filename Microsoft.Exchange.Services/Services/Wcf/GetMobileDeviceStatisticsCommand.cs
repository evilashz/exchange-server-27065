using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009B9 RID: 2489
	internal sealed class GetMobileDeviceStatisticsCommand : SingleCmdletCommandBase<GetMobileDeviceStatisticsRequest, GetMobileDeviceStatisticsResponse, GetMobileDeviceStatistics, MobileDeviceConfiguration>
	{
		// Token: 0x060046A4 RID: 18084 RVA: 0x000FAE89 File Offset: 0x000F9089
		public GetMobileDeviceStatisticsCommand(CallContext callContext, GetMobileDeviceStatisticsRequest request) : base(callContext, request, "Get-MobileDeviceStatistics", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x000FAE9C File Offset: 0x000F909C
		protected override void PopulateTaskParameters()
		{
			GetMobileDeviceStatistics task = this.cmdletRunner.TaskWrapper.Task;
			MobileDeviceStatisticsOptions options = this.request.Options;
			this.cmdletRunner.SetTaskParameter("Mailbox", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			this.cmdletRunner.SetTaskParameterIfModified("ActiveSync", this.request.Options, task, new SwitchParameter(options.ActiveSync));
			this.cmdletRunner.SetTaskParameterIfModified("GetMailboxLog", this.request.Options, task, new SwitchParameter(options.GetMailboxLog));
			this.cmdletRunner.SetTaskParameterIfModified("ShowRecoveryPassword", this.request.Options, task, new SwitchParameter(options.ShowRecoveryPassword));
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x000FB170 File Offset: 0x000F9370
		protected override void PopulateResponseData(GetMobileDeviceStatisticsResponse response)
		{
			IEnumerable<MobileDeviceConfiguration> allResults = this.cmdletRunner.TaskWrapper.AllResults;
			if (allResults == null)
			{
				response.MobileDeviceStatisticsCollection = null;
				return;
			}
			IEnumerable<MobileDeviceStatistics> source = from result in allResults
			select new MobileDeviceStatistics
			{
				FirstSyncTime = this.GetFormattedDate(result.FirstSyncTime),
				DeviceType = result.DeviceType,
				DeviceId = result.DeviceID,
				DeviceUserAgent = result.DeviceUserAgent,
				DeviceModel = result.DeviceModel,
				DeviceImei = result.DeviceImei,
				DeviceOS = result.DeviceOS,
				DeviceOSLanguage = result.DeviceOSLanguage,
				DevicePhoneNumber = result.DevicePhoneNumber,
				DeviceAccessState = result.DeviceAccessState,
				DeviceAccessStateReason = result.DeviceAccessStateReason,
				DeviceAccessControlRule = result.DeviceAccessControlRule.ToIdentity((result.DeviceAccessControlRule == null) ? null : result.DeviceAccessControlRule.ToString()),
				ClientVersion = result.ClientVersion,
				ClientType = result.ClientType,
				DeviceMobileOperator = result.DeviceMobileOperator,
				DeviceFriendlyName = result.DeviceFriendlyName,
				LastPolicyUpdateTime = this.GetFormattedDate(result.LastPolicyUpdateTime),
				LastSyncAttemptTime = this.GetFormattedDate(result.LastSyncAttemptTime),
				LastSuccessSync = this.GetFormattedDate(result.LastSuccessSync),
				DeviceWipeSentTime = this.GetFormattedDate(result.DeviceWipeSentTime),
				DeviceWipeRequestTime = this.GetFormattedDate(result.DeviceWipeRequestTime),
				DeviceWipeAckTime = this.GetFormattedDate(result.DeviceWipeAckTime),
				LastPingHeartBeat = result.LastPingHeartbeat,
				RecoveryPassword = result.RecoveryPassword,
				Identity = new Identity((result.Identity != null) ? result.Identity.ToString() : null),
				IsRemoteWipeSupported = result.IsRemoteWipeSupported,
				Status = result.Status,
				StatusNote = result.StatusNote,
				DevicePolicyApplied = result.DevicePolicyApplied.ToIdentity((result.DevicePolicyApplied != null) ? result.DevicePolicyApplied.ToString() : null),
				NumberOfFoldersSynced = result.NumberOfFoldersSynced,
				DevicePolicyApplicationStatus = result.DevicePolicyApplicationStatus
			};
			response.MobileDeviceStatisticsCollection.MobileDevices = source.ToArray<MobileDeviceStatistics>();
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x000FB1C4 File Offset: 0x000F93C4
		protected override PSLocalTask<GetMobileDeviceStatistics, MobileDeviceConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetMobileDeviceStatisticsTask(base.CallContext.AccessingPrincipal);
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x000FB1DB File Offset: 0x000F93DB
		private string GetFormattedDate(DateTime? date)
		{
			if (date == null)
			{
				return null;
			}
			return ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime((ExDateTime)date.Value);
		}
	}
}
