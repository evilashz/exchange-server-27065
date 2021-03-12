using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D17 RID: 3351
	[Cmdlet("Export", "UMMailboxPrompt", SupportsShouldProcess = true, DefaultParameterSetName = "DefaultVoicemailGreeting")]
	public sealed class ExportUMMailboxPrompt : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x170027DA RID: 10202
		// (get) Token: 0x06008094 RID: 32916 RVA: 0x0020E308 File Offset: 0x0020C508
		// (set) Token: 0x06008095 RID: 32917 RVA: 0x0020E31F File Offset: 0x0020C51F
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "CustomAwayGreeting", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(Mandatory = true, ParameterSetName = "DefaultVoicemailGreeting", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(Mandatory = true, ParameterSetName = "DefaultAwayGreeting", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(Mandatory = true, ParameterSetName = "CustomVoicemailGreeting", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170027DB RID: 10203
		// (get) Token: 0x06008096 RID: 32918 RVA: 0x0020E332 File Offset: 0x0020C532
		// (set) Token: 0x06008097 RID: 32919 RVA: 0x0020E358 File Offset: 0x0020C558
		[Parameter(Mandatory = true, ParameterSetName = "DefaultVoicemailGreeting")]
		public SwitchParameter DefaultVoicemailGreeting
		{
			get
			{
				return (SwitchParameter)(base.Fields["DefaultVoicemailGreeting"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DefaultVoicemailGreeting"] = value;
			}
		}

		// Token: 0x170027DC RID: 10204
		// (get) Token: 0x06008098 RID: 32920 RVA: 0x0020E370 File Offset: 0x0020C570
		// (set) Token: 0x06008099 RID: 32921 RVA: 0x0020E396 File Offset: 0x0020C596
		[Parameter(Mandatory = true, ParameterSetName = "DefaultAwayGreeting")]
		public SwitchParameter DefaultAwayGreeting
		{
			get
			{
				return (SwitchParameter)(base.Fields["DefaultAwayGreeting"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DefaultAwayGreeting"] = value;
			}
		}

		// Token: 0x170027DD RID: 10205
		// (get) Token: 0x0600809A RID: 32922 RVA: 0x0020E3AE File Offset: 0x0020C5AE
		// (set) Token: 0x0600809B RID: 32923 RVA: 0x0020E3D4 File Offset: 0x0020C5D4
		[Parameter(Mandatory = true, ParameterSetName = "CustomVoicemailGreeting")]
		public SwitchParameter CustomVoicemailGreeting
		{
			get
			{
				return (SwitchParameter)(base.Fields["CustomVoicemailGreeting"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["CustomVoicemailGreeting"] = value;
			}
		}

		// Token: 0x170027DE RID: 10206
		// (get) Token: 0x0600809C RID: 32924 RVA: 0x0020E3EC File Offset: 0x0020C5EC
		// (set) Token: 0x0600809D RID: 32925 RVA: 0x0020E412 File Offset: 0x0020C612
		[Parameter(Mandatory = true, ParameterSetName = "CustomAwayGreeting")]
		public SwitchParameter CustomAwayGreeting
		{
			get
			{
				return (SwitchParameter)(base.Fields["CustomAwayGreeting"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["CustomAwayGreeting"] = value;
			}
		}

		// Token: 0x170027DF RID: 10207
		// (get) Token: 0x0600809E RID: 32926 RVA: 0x0020E42A File Offset: 0x0020C62A
		// (set) Token: 0x0600809F RID: 32927 RVA: 0x0020E441 File Offset: 0x0020C641
		[Parameter(Mandatory = false, ParameterSetName = "DefaultAwayGreeting")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "DefaultVoicemailGreeting")]
		public string TestPhoneticDisplayName
		{
			get
			{
				return (string)base.Fields["TestPhoneticDisplayName"];
			}
			set
			{
				base.Fields["TestPhoneticDisplayName"] = value;
			}
		}

		// Token: 0x170027E0 RID: 10208
		// (get) Token: 0x060080A0 RID: 32928 RVA: 0x0020E454 File Offset: 0x0020C654
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageExportUMMailboxPrompt(base.ParameterSetName);
			}
		}

		// Token: 0x060080A1 RID: 32929 RVA: 0x0020E464 File Offset: 0x0020C664
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			using (UMSubscriber umsubscriber = UMRecipient.Factory.FromADRecipient<UMSubscriber>(this.DataObject))
			{
				if (umsubscriber != null)
				{
					UMMailboxPromptRpcRequest request = null;
					string empty = string.Empty;
					string parameterSetName;
					if ((parameterSetName = base.ParameterSetName) != null)
					{
						if (parameterSetName == "DefaultVoicemailGreeting")
						{
							request = UMMailboxPromptRpcRequest.CreateVoicemailPromptRequest(this.DataObject, this.TestPhoneticDisplayName);
							goto IL_CD;
						}
						if (parameterSetName == "DefaultAwayGreeting")
						{
							request = UMMailboxPromptRpcRequest.CreateAwayPromptRequest(this.DataObject, this.TestPhoneticDisplayName);
							goto IL_CD;
						}
						if (parameterSetName == "CustomVoicemailGreeting")
						{
							request = UMMailboxPromptRpcRequest.CreateCustomVoicemailPromptRequest(this.DataObject);
							goto IL_CD;
						}
						if (parameterSetName == "CustomAwayGreeting")
						{
							request = UMMailboxPromptRpcRequest.CreateCustomAwayPromptRequest(this.DataObject);
							goto IL_CD;
						}
					}
					ExAssert.RetailAssert(false, "Invalid parameter set {0}", new object[]
					{
						base.ParameterSetName
					});
					try
					{
						IL_CD:
						base.WriteObject(new UMPrompt(this.DataObject.Identity)
						{
							AudioData = this.GetUMPromptPreview(request, ((ADObjectId)umsubscriber.DialPlan.Identity).ObjectGuid),
							Name = base.ParameterSetName
						});
						goto IL_13D;
					}
					catch (LocalizedException exception)
					{
						base.WriteError(exception, ErrorCategory.NotSpecified, null);
						goto IL_13D;
					}
				}
				base.WriteError(new UserNotUmEnabledException(this.Identity.ToString()), (ErrorCategory)1000, null);
				IL_13D:;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060080A2 RID: 32930 RVA: 0x0020E5F4 File Offset: 0x0020C7F4
		private byte[] GetUMPromptPreview(PromptPreviewRpcRequest request, Guid dialPlanGuid)
		{
			UMPromptPreviewRpcTarget umpromptPreviewRpcTarget = (UMPromptPreviewRpcTarget)UMPromptPreviewRpcTargetPicker.Instance.PickNextServer(dialPlanGuid);
			if (umpromptPreviewRpcTarget == null)
			{
				throw new RpcUMServerNotFoundException();
			}
			byte[] audioData;
			try
			{
				ResponseBase responseBase = umpromptPreviewRpcTarget.ExecuteRequest(request) as ResponseBase;
				if (responseBase == null)
				{
					throw new InvalidResponseException(umpromptPreviewRpcTarget.Name, string.Empty);
				}
				ErrorResponse errorResponse = responseBase as ErrorResponse;
				if (errorResponse != null)
				{
					throw errorResponse.GetException();
				}
				PromptPreviewRpcResponse promptPreviewRpcResponse = responseBase as PromptPreviewRpcResponse;
				if (promptPreviewRpcResponse == null)
				{
					throw new InvalidResponseException(umpromptPreviewRpcTarget.Name, string.Empty);
				}
				audioData = promptPreviewRpcResponse.AudioData;
			}
			catch (RpcException ex)
			{
				throw new InvalidResponseException(umpromptPreviewRpcTarget.Name, ex.Message, ex);
			}
			return audioData;
		}

		// Token: 0x02000D18 RID: 3352
		internal abstract class ParameterSet
		{
			// Token: 0x04003F02 RID: 16130
			internal const string DefaultVoicemailGreeting = "DefaultVoicemailGreeting";

			// Token: 0x04003F03 RID: 16131
			internal const string DefaultAwayGreeting = "DefaultAwayGreeting";

			// Token: 0x04003F04 RID: 16132
			internal const string CustomVoicemailGreeting = "CustomVoicemailGreeting";

			// Token: 0x04003F05 RID: 16133
			internal const string CustomAwayGreeting = "CustomAwayGreeting";
		}
	}
}
