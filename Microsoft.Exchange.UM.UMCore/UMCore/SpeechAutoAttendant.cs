using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001DB RID: 475
	internal class SpeechAutoAttendant : AutoAttendantCore
	{
		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003D78B File Offset: 0x0003B98B
		internal SpeechAutoAttendant(IAutoAttendantUI autoAttendantManager, BaseUMCallSession voiceObject) : base(autoAttendantManager, voiceObject)
		{
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003D798 File Offset: 0x0003B998
		internal override void Configure()
		{
			base.Configure();
			this.dtmfFallbackEnabled = false;
			if (base.Config.DTMFFallbackAutoAttendant != null)
			{
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(base.Config.OrganizationId);
				UMAutoAttendant autoAttendantFromId = iadsystemConfigurationLookup.GetAutoAttendantFromId(base.Config.DTMFFallbackAutoAttendant);
				LocalizedString localizedString;
				if (AutoAttendantCore.IsRunnableAutoAttendant(autoAttendantFromId, out localizedString))
				{
					this.dtmfFallbackEnabled = true;
				}
			}
			base.WriteVariable("aa_dtmfFallbackEnabled", this.dtmfFallbackEnabled);
			this.ConfigureDtmfFallbackAction();
			bool flag = base.NameLookupConfigured && (base.Config.CallSomeoneEnabled || base.Config.SendVoiceMsgEnabled);
			base.WriteVariable("contacts_nameLookupEnabled", flag);
			this.ConfigureDepartmentPrompt();
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0003D84E File Offset: 0x0003BA4E
		internal override void Initialize()
		{
			this.asrNameSpoken = false;
			this.dtmfInputForDeptMenu = false;
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0003D860 File Offset: 0x0003BA60
		internal override bool ExecuteAction(string action, BaseUMCallSession voiceObject, ref string autoEvent)
		{
			bool result;
			if (string.Compare(action, "processResult", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_ProcessResult();
			}
			else if (string.Compare(action, "setCustomExtensionNumber", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_SetCustomExtensionNumber();
			}
			else if (string.Compare(action, "prepareForANROperatorTransfer", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_PrepareForANROperatorTransfer();
			}
			else
			{
				if (string.Compare(action, "setFallbackAutoAttendant", true, CultureInfo.InvariantCulture) != 0)
				{
					return base.ExecuteAction(action, base.VoiceObject, ref autoEvent);
				}
				result = true;
				if (!base.VoiceObject.CurrentCallContext.SwitchToFallbackAutoAttendant())
				{
					autoEvent = "fallbackAutoAttendantFailure";
				}
			}
			return result;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0003D90E File Offset: 0x0003BB0E
		internal override void OnSpeech()
		{
			base.OnSpeech();
			if (this.incrementedSpeechCallCounter)
			{
				return;
			}
			this.incrementedSpeechCallCounter = true;
			base.PerfCounters.IncrementSpeechCallsCounter();
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003D931 File Offset: 0x0003BB31
		internal override void OnNameSpoken()
		{
			base.OnNameSpoken();
			this.asrNameSpoken = true;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003D940 File Offset: 0x0003BB40
		internal override string Action_ProcessCustomMenuExtension()
		{
			string dtmfDigits = ((ActivityManager)base.AutoAttendantManager).DtmfDigits;
			string result = null;
			if (base.TimeoutPending)
			{
				base.TimeoutPending = false;
				base.SelectedMenu = base.CustomMenuTimeoutOption;
				this.dtmfInputForDeptMenu = true;
			}
			else if (Constants.RegularExpressions.ValidDigitRegex.IsMatch(dtmfDigits))
			{
				int selectedMenuOption = int.Parse(dtmfDigits, CultureInfo.InvariantCulture);
				if (this.SetSelectedMenuOption(selectedMenuOption))
				{
					this.dtmfInputForDeptMenu = true;
				}
				else
				{
					result = "invalidOption";
					base.WriteVariable("invalidExtension", dtmfDigits);
				}
			}
			else
			{
				result = "invalidOption";
				base.WriteVariable("invalidExtension", dtmfDigits);
			}
			AsrSearchResult value = AsrSearchResult.Create(base.SelectedMenu);
			base.AutoAttendantManager.WriteProperty("searchResult", value);
			return result;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003D9F5 File Offset: 0x0003BBF5
		internal override string GetCustomMenuAutoAttendant()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "SpeechAutoAttendant::GetCustomMenuAutoAttendant()", new object[0]);
			return this.GetTarget();
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003DA13 File Offset: 0x0003BC13
		internal override string GetCustomMenuVoicemailTarget()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "SpeechAutoAttendant::GetCustomMenuVoicemailTarget()", new object[0]);
			return this.GetTarget();
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003DA31 File Offset: 0x0003BC31
		internal override string GetCustomMenuTargetPAA()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "SpeechAutoAttendant::GetCustomMenuTargetPAA()", new object[0]);
			return this.GetTarget();
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003DA50 File Offset: 0x0003BC50
		internal string Action_ProcessResult()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "SpeechAutoAttendant::Action_ProcessResult().", new object[0]);
			AsrSearchResult asrSearchResult = (AsrSearchResult)base.AutoAttendantManager.ReadProperty("searchResult");
			string text;
			if (asrSearchResult is AsrDepartmentSearchResult)
			{
				text = this.Action_ProcessResult((AsrDepartmentSearchResult)asrSearchResult);
			}
			else if (asrSearchResult is AsrDirectorySearchResult)
			{
				text = this.Action_ProcessResult((AsrDirectorySearchResult)asrSearchResult);
			}
			else
			{
				if (!(asrSearchResult is AsrExtensionSearchResult))
				{
					throw new InvalidOperationException("Invalid searchResult: " + asrSearchResult.GetType().ToString());
				}
				text = this.Action_ProcessResult((AsrExtensionSearchResult)asrSearchResult);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "SpeechAutoAttendant::Action_ProcessResult() returning autoevent: {0}", new object[]
			{
				text ?? "<null>"
			});
			return text;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003DB14 File Offset: 0x0003BD14
		internal string Action_ProcessResult(AsrDepartmentSearchResult departmentResult)
		{
			string result = null;
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "SpeechAutoAttendant::Action_ProcessResult(<Department>).", new object[0]);
			base.IncrementKeyMappingPerfCounters(departmentResult.KeyPress);
			if (this.asrNameSpoken)
			{
				base.PerfCounters.Increment(DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedSuccessfullyBySpokenName);
			}
			else if (this.dtmfInputForDeptMenu)
			{
				base.PerfCounters.IncrementSingleCounter(DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccess);
			}
			this.SetSelectedMenuOption((int)departmentResult.KeyPress);
			departmentResult.SetManagerVariables((ActivityManager)base.AutoAttendantManager, base.VoiceObject);
			return result;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003DB94 File Offset: 0x0003BD94
		internal string Action_ProcessResult(AsrExtensionSearchResult extensionResult)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "SpeechAutoAttendant::Action_ProcessResult(<Extension>).", new object[0]);
			RecipientLookup recipientLookup = RecipientLookup.Create(base.VoiceObject);
			DialPermissionWrapper dialPermissionWrapper = DialPermissionWrapperFactory.Create(base.VoiceObject);
			extensionResult.SetManagerVariables((ActivityManager)base.AutoAttendantManager, base.VoiceObject);
			ADRecipient adrecipient = recipientLookup.LookupByExtension(extensionResult.Extension.Number, base.VoiceObject, DirectorySearchPurpose.Both, dialPermissionWrapper.ContactScope);
			string result;
			if (adrecipient == null)
			{
				result = "unreachableUser";
				if (dialPermissionWrapper.CallingNonUmExtensionsAllowed && extensionResult.Extension.IsValid(base.VoiceObject.CurrentCallContext.DialPlan) && !extensionResult.Extension.StartsWithTrunkAccessCode(base.VoiceObject.CurrentCallContext.DialPlan))
				{
					ActivityManager manager = (ActivityManager)base.AutoAttendantManager;
					PhoneUtil.SetTransferTargetPhone(manager, TransferExtension.UserExtension, extensionResult.Extension);
					result = "allowCallOnly";
				}
			}
			else
			{
				result = this.ProcessDialingPermissions(adrecipient);
			}
			return result;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003DC80 File Offset: 0x0003BE80
		internal string Action_ProcessResult(AsrDirectorySearchResult userResult)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "SpeechAutoAttendant::Action_ProcessResult(<DirectoryResult>).", new object[0]);
			base.PerfCounters.Increment(DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedSuccessfullyBySpokenName);
			userResult.SetManagerVariables((ActivityManager)base.AutoAttendantManager, base.VoiceObject);
			ADRecipient recipient = userResult.Recipient;
			return this.ProcessDialingPermissions(recipient);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003DCD4 File Offset: 0x0003BED4
		internal string Action_PrepareForANROperatorTransfer()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "Action_PrepareForANROperatorTransfer().", new object[0]);
			base.PerfCounters.IncrementANRTransfersToOperatorCounter();
			return null;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003DCF8 File Offset: 0x0003BEF8
		internal string ProcessDialingPermissions(ADRecipient recipient)
		{
			DialingPermissionsCheck dialingPermissionsCheck = DialingPermissionsCheckFactory.Create(base.VoiceObject);
			DialingPermissionsCheck.DialingPermissionsCheckResult perms = dialingPermissionsCheck.CheckDirectoryUser(recipient, null);
			AnonCallerUtils.SetVariables(recipient, perms, (ActivityManager)base.AutoAttendantManager);
			ActivityManager activityManager = (ActivityManager)base.AutoAttendantManager;
			bool flag = false;
			ActivityManager activityManager2 = activityManager;
			string variableName = "userName";
			string recipientName;
			if ((recipientName = recipient.DisplayName) == null)
			{
				recipientName = (recipient.Alias ?? string.Empty);
			}
			activityManager2.SetRecordedName(variableName, recipientName, recipient, true, Util.GetDisambiguationField(base.CallContext), ref flag);
			object varValue = activityManager.ReadVariable("userName");
			activityManager.GlobalManager.WriteVariable("userName", varValue);
			return AnonCallerUtils.GetAutoEvent(perms);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0003DD94 File Offset: 0x0003BF94
		internal string ConfigureDtmfFallbackAction()
		{
			bool flag = false;
			bool flag2 = false;
			if (base.BusinessHoursCall)
			{
				if (this.dtmfFallbackEnabled)
				{
					flag = true;
				}
				else if (base.OperatorEnabled)
				{
					flag2 = true;
				}
			}
			else if (this.dtmfFallbackEnabled)
			{
				flag = true;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "DTMFFallbackAction: ToOperator:{0} ToDtmfAA:{1}.", new object[]
			{
				flag2,
				flag
			});
			base.WriteVariable("aa_goto_dtmf_autoattendant", flag);
			base.WriteVariable("aa_goto_operator", flag2);
			return null;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003DE1C File Offset: 0x0003C01C
		internal void SetVariables(AsrSearchManager autoAttendantManager)
		{
			if (base.CustomizedMenuConfigured)
			{
				for (int i = 0; i < base.CustomMenu.Length; i++)
				{
					CustomMenuKeyMapping customMenuKeyMapping = base.CustomMenu[i];
					if (customMenuKeyMapping.MappedKey == CustomMenuKey.Timeout)
					{
						autoAttendantManager.WriteVariable("TimeoutOption", true);
					}
					else
					{
						int mappedKey = (int)customMenuKeyMapping.MappedKey;
						if (mappedKey >= 1 && mappedKey <= 9)
						{
							autoAttendantManager.WriteVariable(string.Format(CultureInfo.InvariantCulture, "DtmfKey{0}", new object[]
							{
								mappedKey
							}), true);
						}
					}
				}
			}
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003DEA8 File Offset: 0x0003C0A8
		private string GetTarget()
		{
			AsrDepartmentSearchResult asrDepartmentSearchResult = (AsrDepartmentSearchResult)base.AutoAttendantManager.ReadProperty("searchResult");
			return asrDepartmentSearchResult.TransferTarget;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003DED4 File Offset: 0x0003C0D4
		private string Action_SetCustomExtensionNumber()
		{
			AsrSearchResult asrSearchResult = (AsrSearchResult)base.AutoAttendantManager.ReadProperty("searchResult");
			AsrDepartmentSearchResult asrDepartmentSearchResult = asrSearchResult as AsrDepartmentSearchResult;
			string result;
			if (asrDepartmentSearchResult != null)
			{
				PhoneNumber phoneNumber = asrDepartmentSearchResult.PhoneNumber;
				if (phoneNumber == null)
				{
					return "cannotTransferToCustomExtension";
				}
				PhoneUtil.SetTransferTargetPhone((ActivityManager)base.AutoAttendantManager, TransferExtension.CustomMenuExtension, phoneNumber);
				result = null;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003DF2C File Offset: 0x0003C12C
		private void ConfigureDepartmentPrompt()
		{
			if (!base.CustomizedMenuConfigured)
			{
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "Customized menu: #options = {0}.", new object[]
			{
				base.NumCustomizedMenuOptions
			});
			List<string> list = new List<string>();
			foreach (CustomMenuKeyMapping customMenuKeyMapping in base.CustomMenu)
			{
				if (customMenuKeyMapping.MappedKey == CustomMenuKey.Timeout)
				{
					base.WriteVariable(string.Format(CultureInfo.InvariantCulture, "nameOfDepartment{0}", new object[]
					{
						"TimeOut"
					}), customMenuKeyMapping.Description);
				}
				else
				{
					list.Add(customMenuKeyMapping.Description);
				}
			}
			if (list.Count > 0)
			{
				base.WriteVariable("selectableDepartments", list);
			}
			string description = base.CustomMenu[0].Description;
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "Generating first department prompt.", new object[0]);
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "First Department: {0}.", new object[]
			{
				description
			});
			base.AutoAttendantManager.SetTextPrompt("firstDepartment", description);
			CustomGrammarFile value = new CustomGrammarFile(base.CustomMenu, "Department", base.CallContext.Culture, base.Config.Name);
			base.WriteVariable("customizedMenuGrammar", value);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0003E074 File Offset: 0x0003C274
		private bool SetSelectedMenuOption(int key)
		{
			CustomMenuKeyMapping[] keyMapping = base.Settings.KeyMapping;
			CustomMenuKeyMapping selectedMenu;
			if (AutoAttendantCore.GetExtensionForKey(key, keyMapping, out selectedMenu))
			{
				base.SelectedMenu = selectedMenu;
				return true;
			}
			return false;
		}

		// Token: 0x04000AAF RID: 2735
		private bool incrementedSpeechCallCounter;

		// Token: 0x04000AB0 RID: 2736
		private bool asrNameSpoken;

		// Token: 0x04000AB1 RID: 2737
		private bool dtmfFallbackEnabled;

		// Token: 0x04000AB2 RID: 2738
		private bool dtmfInputForDeptMenu;
	}
}
