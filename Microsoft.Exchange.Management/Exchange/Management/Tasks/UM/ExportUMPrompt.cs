using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D1A RID: 3354
	[Cmdlet("Export", "UMPrompt", SupportsShouldProcess = true, DefaultParameterSetName = "BusinessHoursWelcomeGreeting")]
	public sealed class ExportUMPrompt : UMPromptTaskBase<UMDialPlanIdParameter>
	{
		// Token: 0x170027E5 RID: 10213
		// (get) Token: 0x060080B0 RID: 32944 RVA: 0x0020E7AE File Offset: 0x0020C9AE
		// (set) Token: 0x060080B1 RID: 32945 RVA: 0x0020E7B6 File Offset: 0x0020C9B6
		private new UMDialPlanIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170027E6 RID: 10214
		// (get) Token: 0x060080B2 RID: 32946 RVA: 0x0020E7BF File Offset: 0x0020C9BF
		// (set) Token: 0x060080B3 RID: 32947 RVA: 0x0020E7D6 File Offset: 0x0020C9D6
		[Parameter(Mandatory = true, ParameterSetName = "AfterHoursWelcomeGreetingAndMenu")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "AfterHoursWelcomeGreeting")]
		[Parameter(Mandatory = true, ParameterSetName = "BusinessHours")]
		[Parameter(Mandatory = true, ParameterSetName = "BusinessLocation")]
		[Parameter(Mandatory = true, ParameterSetName = "BusinessHoursWelcomeGreeting")]
		[Parameter(Mandatory = true, ParameterSetName = "BusinessHoursWelcomeGreetingAndMenu")]
		[Parameter(Mandatory = true, ParameterSetName = "AACustomGreeting")]
		public override UMAutoAttendantIdParameter UMAutoAttendant
		{
			get
			{
				return (UMAutoAttendantIdParameter)base.Fields["UMAutoAttendant"];
			}
			set
			{
				base.Fields["UMAutoAttendant"] = value;
			}
		}

		// Token: 0x170027E7 RID: 10215
		// (get) Token: 0x060080B4 RID: 32948 RVA: 0x0020E7E9 File Offset: 0x0020C9E9
		// (set) Token: 0x060080B5 RID: 32949 RVA: 0x0020E7F1 File Offset: 0x0020C9F1
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "DPCustomGreeting")]
		public override UMDialPlanIdParameter UMDialPlan
		{
			get
			{
				return this.Identity;
			}
			set
			{
				this.Identity = value;
			}
		}

		// Token: 0x170027E8 RID: 10216
		// (get) Token: 0x060080B6 RID: 32950 RVA: 0x0020E7FA File Offset: 0x0020C9FA
		// (set) Token: 0x060080B7 RID: 32951 RVA: 0x0020E820 File Offset: 0x0020CA20
		[Parameter(Mandatory = true, ParameterSetName = "BusinessHours")]
		public SwitchParameter BusinessHours
		{
			get
			{
				return (SwitchParameter)(base.Fields["BusinessHours"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["BusinessHours"] = value;
			}
		}

		// Token: 0x170027E9 RID: 10217
		// (get) Token: 0x060080B8 RID: 32952 RVA: 0x0020E838 File Offset: 0x0020CA38
		// (set) Token: 0x060080B9 RID: 32953 RVA: 0x0020E85E File Offset: 0x0020CA5E
		[Parameter(Mandatory = true, ParameterSetName = "BusinessLocation")]
		public SwitchParameter BusinessLocation
		{
			get
			{
				return (SwitchParameter)(base.Fields["BusinessLocation"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["BusinessLocation"] = value;
			}
		}

		// Token: 0x170027EA RID: 10218
		// (get) Token: 0x060080BA RID: 32954 RVA: 0x0020E876 File Offset: 0x0020CA76
		// (set) Token: 0x060080BB RID: 32955 RVA: 0x0020E89C File Offset: 0x0020CA9C
		[Parameter(Mandatory = true, ParameterSetName = "BusinessHoursWelcomeGreeting")]
		public SwitchParameter BusinessHoursWelcomeGreeting
		{
			get
			{
				return (SwitchParameter)(base.Fields["BusinessHoursWelcomeGreeting"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["BusinessHoursWelcomeGreeting"] = value;
			}
		}

		// Token: 0x170027EB RID: 10219
		// (get) Token: 0x060080BC RID: 32956 RVA: 0x0020E8B4 File Offset: 0x0020CAB4
		// (set) Token: 0x060080BD RID: 32957 RVA: 0x0020E8DA File Offset: 0x0020CADA
		[Parameter(Mandatory = true, ParameterSetName = "BusinessHoursWelcomeGreetingAndMenu")]
		public SwitchParameter BusinessHoursWelcomeGreetingAndMenu
		{
			get
			{
				return (SwitchParameter)(base.Fields["BusinessHoursWelcomeGreetingAndMenu"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["BusinessHoursWelcomeGreetingAndMenu"] = value;
			}
		}

		// Token: 0x170027EC RID: 10220
		// (get) Token: 0x060080BE RID: 32958 RVA: 0x0020E8F2 File Offset: 0x0020CAF2
		// (set) Token: 0x060080BF RID: 32959 RVA: 0x0020E918 File Offset: 0x0020CB18
		[Parameter(Mandatory = true, ParameterSetName = "AfterHoursWelcomeGreeting")]
		public SwitchParameter AfterHoursWelcomeGreeting
		{
			get
			{
				return (SwitchParameter)(base.Fields["AfterHoursWelcomeGreeting"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["AfterHoursWelcomeGreeting"] = value;
			}
		}

		// Token: 0x170027ED RID: 10221
		// (get) Token: 0x060080C0 RID: 32960 RVA: 0x0020E930 File Offset: 0x0020CB30
		// (set) Token: 0x060080C1 RID: 32961 RVA: 0x0020E956 File Offset: 0x0020CB56
		[Parameter(Mandatory = true, ParameterSetName = "AfterHoursWelcomeGreetingAndMenu")]
		public SwitchParameter AfterHoursWelcomeGreetingAndMenu
		{
			get
			{
				return (SwitchParameter)(base.Fields["AfterHoursWelcomeGreetingAndMenu"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["AfterHoursWelcomeGreetingAndMenu"] = value;
			}
		}

		// Token: 0x170027EE RID: 10222
		// (get) Token: 0x060080C2 RID: 32962 RVA: 0x0020E96E File Offset: 0x0020CB6E
		// (set) Token: 0x060080C3 RID: 32963 RVA: 0x0020E985 File Offset: 0x0020CB85
		[Parameter(Mandatory = true, ParameterSetName = "DPCustomGreeting")]
		[Parameter(Mandatory = true, ParameterSetName = "AACustomGreeting")]
		public string PromptFileName
		{
			get
			{
				return (string)base.Fields["PromptFileName"];
			}
			set
			{
				base.Fields["PromptFileName"] = value;
			}
		}

		// Token: 0x170027EF RID: 10223
		// (get) Token: 0x060080C4 RID: 32964 RVA: 0x0020E998 File Offset: 0x0020CB98
		// (set) Token: 0x060080C5 RID: 32965 RVA: 0x0020E9AF File Offset: 0x0020CBAF
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "AfterHoursWelcomeGreeting")]
		[Parameter(Mandatory = false, ParameterSetName = "BusinessHoursWelcomeGreeting")]
		public string TestBusinessName
		{
			get
			{
				return (string)base.Fields["TestBusinessName"];
			}
			set
			{
				base.Fields["TestBusinessName"] = value;
			}
		}

		// Token: 0x170027F0 RID: 10224
		// (get) Token: 0x060080C6 RID: 32966 RVA: 0x0020E9C2 File Offset: 0x0020CBC2
		// (set) Token: 0x060080C7 RID: 32967 RVA: 0x0020E9D9 File Offset: 0x0020CBD9
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "AfterHoursWelcomeGreetingAndMenu")]
		[Parameter(Mandatory = false, ParameterSetName = "BusinessHoursWelcomeGreetingAndMenu")]
		public CustomMenuKeyMapping[] TestMenuKeyMapping
		{
			get
			{
				return (CustomMenuKeyMapping[])base.Fields["TestMenuKeyMapping"];
			}
			set
			{
				base.Fields["TestMenuKeyMapping"] = value;
			}
		}

		// Token: 0x170027F1 RID: 10225
		// (get) Token: 0x060080C8 RID: 32968 RVA: 0x0020E9EC File Offset: 0x0020CBEC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string parameterSetName;
				switch (parameterSetName = base.ParameterSetName)
				{
				case "AACustomGreeting":
				case "AfterHoursWelcomeGreeting":
				case "AfterHoursWelcomeGreetingAndMenu":
				case "BusinessHours":
				case "BusinessLocation":
				case "BusinessHoursWelcomeGreeting":
				case "BusinessHoursWelcomeGreetingAndMenu":
					return Strings.ConfirmationMessageExportUMPromptAutoAttendantPrompts(this.PromptFileName, this.UMAutoAttendant.ToString());
				case "DPCustomGreeting":
					return Strings.ConfirmationMessageExportUMPromptDialPlanPrompts(this.PromptFileName, this.UMDialPlan.ToString());
				}
				ExAssert.RetailAssert(false, "Invalid parameter set {0}", new object[]
				{
					base.ParameterSetName
				});
				return new LocalizedString(string.Empty);
			}
		}

		// Token: 0x060080C9 RID: 32969 RVA: 0x0020EB0C File Offset: 0x0020CD0C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			PromptPreviewRpcRequest request = null;
			ObjectId identity = null;
			string parameterSetName;
			switch (parameterSetName = base.ParameterSetName)
			{
			case "AACustomGreeting":
				request = new UMAACustomPromptRpcRequest(base.AutoAttendant, this.PromptFileName);
				identity = base.AutoAttendant.Identity;
				goto IL_1ED;
			case "AfterHoursWelcomeGreeting":
				request = UMAAWelcomePromptRpcRequest.AfterHoursWithCustomBusinessName(base.AutoAttendant, this.TestBusinessName);
				identity = base.AutoAttendant.Identity;
				goto IL_1ED;
			case "AfterHoursWelcomeGreetingAndMenu":
				request = UMAAWelcomePromptRpcRequest.AfterHoursWithCustomKeyMapping(base.AutoAttendant, this.TestMenuKeyMapping);
				identity = base.AutoAttendant.Identity;
				goto IL_1ED;
			case "BusinessHours":
				request = new UMAABusinessHoursPromptRpcRequest(base.AutoAttendant);
				identity = base.AutoAttendant.Identity;
				goto IL_1ED;
			case "BusinessLocation":
				request = new UMAABusinessLocationPromptRpcRequest(base.AutoAttendant);
				identity = base.AutoAttendant.Identity;
				goto IL_1ED;
			case "BusinessHoursWelcomeGreeting":
				request = UMAAWelcomePromptRpcRequest.BusinessHoursWithCustomBusinessName(base.AutoAttendant, this.TestBusinessName);
				identity = base.AutoAttendant.Identity;
				goto IL_1ED;
			case "BusinessHoursWelcomeGreetingAndMenu":
				request = UMAAWelcomePromptRpcRequest.BusinessHoursWithCustomKeyMapping(base.AutoAttendant, this.TestMenuKeyMapping);
				identity = base.AutoAttendant.Identity;
				goto IL_1ED;
			case "DPCustomGreeting":
				request = new UMDPCustomPromptRpcRequest(this.DataObject, this.PromptFileName);
				identity = this.DataObject.Identity;
				goto IL_1ED;
			}
			ExAssert.RetailAssert(false, "Invalid parameter set {0}", new object[]
			{
				base.ParameterSetName
			});
			try
			{
				IL_1ED:
				ADObjectId adobjectId = (base.AutoAttendant == null) ? ((ADObjectId)this.DataObject.Identity) : base.AutoAttendant.UMDialPlan;
				UMPrompt umprompt = new UMPrompt(identity);
				umprompt.AudioData = this.GetUMPromptPreview(request, adobjectId.ObjectGuid);
				if (base.ParameterSetName == "DPCustomGreeting" || base.ParameterSetName == "AACustomGreeting")
				{
					umprompt.Name = this.PromptFileName;
				}
				else
				{
					umprompt.Name = base.ParameterSetName;
				}
				base.WriteObject(umprompt);
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.NotSpecified, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060080CA RID: 32970 RVA: 0x0020EDB0 File Offset: 0x0020CFB0
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

		// Token: 0x02000D1B RID: 3355
		internal abstract class ParameterSet
		{
			// Token: 0x04003F07 RID: 16135
			internal const string AfterHoursWelcomeGreeting = "AfterHoursWelcomeGreeting";

			// Token: 0x04003F08 RID: 16136
			internal const string AfterHoursWelcomeGreetingAndMenu = "AfterHoursWelcomeGreetingAndMenu";

			// Token: 0x04003F09 RID: 16137
			internal const string BusinessHours = "BusinessHours";

			// Token: 0x04003F0A RID: 16138
			internal const string BusinessLocation = "BusinessLocation";

			// Token: 0x04003F0B RID: 16139
			internal const string BusinessHoursWelcomeGreeting = "BusinessHoursWelcomeGreeting";

			// Token: 0x04003F0C RID: 16140
			internal const string BusinessHoursWelcomeGreetingAndMenu = "BusinessHoursWelcomeGreetingAndMenu";

			// Token: 0x04003F0D RID: 16141
			internal const string AACustomGreeting = "AACustomGreeting";

			// Token: 0x04003F0E RID: 16142
			internal const string DPCustomGreeting = "DPCustomGreeting";
		}
	}
}
