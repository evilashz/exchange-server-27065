using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200001F RID: 31
	internal class AsrDepartmentSearchResult : AsrSearchResult
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00007874 File Offset: 0x00005A74
		internal AsrDepartmentSearchResult(CustomMenuKeyMapping menu)
		{
			this.optionType = AutoAttendantCustomOptionType.None;
			if (!string.IsNullOrEmpty(menu.Extension))
			{
				this.optionType = AutoAttendantCustomOptionType.TransferToExtension;
				this.transferTarget = menu.Extension;
			}
			else if (!string.IsNullOrEmpty(menu.AutoAttendantName))
			{
				this.transferTarget = menu.AutoAttendantName;
				this.optionType = AutoAttendantCustomOptionType.TransferToAutoAttendant;
			}
			else if (!string.IsNullOrEmpty(menu.LegacyDNToUseForLeaveVoicemailFor))
			{
				this.transferTarget = menu.LegacyDNToUseForLeaveVoicemailFor;
				this.optionType = AutoAttendantCustomOptionType.TransferToVoicemailDirectly;
			}
			else if (!string.IsNullOrEmpty(menu.LegacyDNToUseForTransferToMailbox))
			{
				this.transferTarget = menu.LegacyDNToUseForTransferToMailbox;
				this.optionType = AutoAttendantCustomOptionType.TransferToVoicemailPAA;
			}
			else if (!string.IsNullOrEmpty(menu.AnnounceBusinessLocation))
			{
				this.transferTarget = menu.AnnounceBusinessLocation;
				this.optionType = AutoAttendantCustomOptionType.ReadBusinessLocation;
			}
			else if (!string.IsNullOrEmpty(menu.AnnounceBusinessHours))
			{
				this.transferTarget = menu.AnnounceBusinessHours;
				this.optionType = AutoAttendantCustomOptionType.ReadBusinessHours;
			}
			this.promptFileName = menu.PromptFileName;
			this.keyPress = menu.MappedKey;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007978 File Offset: 0x00005B78
		internal AsrDepartmentSearchResult(IUMRecognitionPhrase recognitionPhrase)
		{
			this.transferTarget = (string)recognitionPhrase["Extension"];
			this.departmentName = (string)recognitionPhrase["DepartmentName"];
			string value = (string)recognitionPhrase["CustomMenuTarget"];
			this.promptFileName = (string)recognitionPhrase["PromptFileName"];
			this.optionType = (AutoAttendantCustomOptionType)Enum.Parse(typeof(AutoAttendantCustomOptionType), value);
			value = (string)recognitionPhrase["MappedKey"];
			this.keyPress = (CustomMenuKey)Enum.Parse(typeof(CustomMenuKey), value);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00007A25 File Offset: 0x00005C25
		public PhoneNumber PhoneNumber
		{
			get
			{
				return this.selectedPhoneNumber;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00007A2D File Offset: 0x00005C2D
		public string DepartmentName
		{
			get
			{
				return this.departmentName;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007A35 File Offset: 0x00005C35
		public CustomMenuKey KeyPress
		{
			get
			{
				return this.keyPress;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007A3D File Offset: 0x00005C3D
		public AutoAttendantCustomOptionType OptionType
		{
			get
			{
				return this.optionType;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007A45 File Offset: 0x00005C45
		public string PromptFileName
		{
			get
			{
				return this.promptFileName;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007A4D File Offset: 0x00005C4D
		public string TransferTarget
		{
			get
			{
				return this.transferTarget;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007A58 File Offset: 0x00005C58
		internal override void SetManagerVariables(ActivityManager manager, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "For department {0}, type= {1} returning phone, or aa: {2}.", new object[]
			{
				this.departmentName,
				this.optionType,
				this.transferTarget
			});
			if (this.optionType == AutoAttendantCustomOptionType.TransferToExtension)
			{
				this.selectedPhoneNumber = AutoAttendantCore.GetCustomExtensionNumberToDial(vo.CurrentCallContext, this.transferTarget);
			}
			manager.WriteVariable("resultType", ResultType.Department);
			manager.WriteVariable("resultTypeString", ResultType.Department.ToString());
			manager.WriteVariable("selectedUser", null);
			if (this.selectedPhoneNumber != null)
			{
				manager.WriteVariable("selectedPhoneNumber", this.selectedPhoneNumber);
			}
			bool flag = !string.IsNullOrEmpty(this.promptFileName);
			manager.WriteVariable("haveCustomMenuOptionPrompt", flag);
			if (flag)
			{
				manager.WriteVariable("customMenuOptionPrompt", vo.CurrentCallContext.UMConfigCache.GetPrompt<UMAutoAttendant>(vo.CurrentCallContext.AutoAttendantInfo, this.promptFileName));
			}
			manager.WriteVariable("customMenuOption", this.optionType.ToString());
		}

		// Token: 0x04000076 RID: 118
		private PhoneNumber selectedPhoneNumber;

		// Token: 0x04000077 RID: 119
		private string departmentName;

		// Token: 0x04000078 RID: 120
		private AutoAttendantCustomOptionType optionType;

		// Token: 0x04000079 RID: 121
		private CustomMenuKey keyPress;

		// Token: 0x0400007A RID: 122
		private string transferTarget;

		// Token: 0x0400007B RID: 123
		private string promptFileName;
	}
}
