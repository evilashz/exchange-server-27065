using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000312 RID: 786
	internal class GlobalActivityManager
	{
		// Token: 0x06001960 RID: 6496 RVA: 0x000672E1 File Offset: 0x000654E1
		internal static void GetScope(ActivityManager manager, out GlobalActivityManager scope)
		{
			for (scope = (manager as GlobalActivityManager); scope == null; scope = (manager as GlobalActivityManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00067314 File Offset: 0x00065514
		internal static TransitionBase ClearCaller(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0006731E File Offset: 0x0006551E
		internal static TransitionBase CreateCallee(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x00067328 File Offset: 0x00065528
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00067332 File Offset: 0x00065532
		internal static TransitionBase DoLogon(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0006733C File Offset: 0x0006553C
		internal static TransitionBase GetExtension(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00067346 File Offset: 0x00065546
		internal static TransitionBase GetName(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00067350 File Offset: 0x00065550
		internal static TransitionBase GetSummaryInfo(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0006735A File Offset: 0x0006555A
		internal static TransitionBase HandleCallSomeone(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00067364 File Offset: 0x00065564
		internal static TransitionBase OofShortcut(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0006736E File Offset: 0x0006556E
		internal static TransitionBase QuickMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00067378 File Offset: 0x00065578
		internal static TransitionBase SetInitialSearchTargetContacts(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00067382 File Offset: 0x00065582
		internal static TransitionBase SetInitialSearchTargetGAL(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0006738C File Offset: 0x0006558C
		internal static TransitionBase SetPromptProvContext(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00067396 File Offset: 0x00065596
		internal static TransitionBase StopASR(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000673A0 File Offset: 0x000655A0
		internal static TransitionBase ValidateCaller(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x000673AA File Offset: 0x000655AA
		internal static TransitionBase ValidateMailbox(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000673B4 File Offset: 0x000655B4
		internal static TransitionBase PrepareForTransferToServer(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return manager.GetTransition(globalActivityManager.PrepareForTransferToServer(vo));
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000673E0 File Offset: 0x000655E0
		internal static TransitionBase FillCallerInfo(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return manager.GetTransition(globalActivityManager.FillCallerInfo(vo));
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0006740C File Offset: 0x0006560C
		internal static TransitionBase HandleIsPinRequired(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return manager.GetTransition(globalActivityManager.HandleIsPinRequired(vo));
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00067438 File Offset: 0x00065638
		internal static AutoAttendantContext AAContext(ActivityManager manager, string variableName)
		{
			return (AutoAttendantContext)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0006744B File Offset: 0x0006564B
		internal static AutoAttendantLocationContext AALocationContext(ActivityManager manager, string variableName)
		{
			return (AutoAttendantLocationContext)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0006745E File Offset: 0x0006565E
		internal static string BusinessAddress(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00067471 File Offset: 0x00065671
		internal static object BusinessName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0006747F File Offset: 0x0006567F
		internal static UMAutoAttendant BusinessSchedule(ActivityManager manager, string variableName)
		{
			return (UMAutoAttendant)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00067492 File Offset: 0x00065692
		internal static object CalendarAccessEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000674A0 File Offset: 0x000656A0
		internal static object ContactSomeoneEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000674AE File Offset: 0x000656AE
		internal static object CurrentActivity(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000674BC File Offset: 0x000656BC
		internal static AutoAttendantContext CustomMenu(ActivityManager manager, string variableName)
		{
			return (AutoAttendantContext)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000674CF File Offset: 0x000656CF
		internal static string CustomPrompt(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000674E2 File Offset: 0x000656E2
		internal static string DepartmentName(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x000674F5 File Offset: 0x000656F5
		internal static object EmailAccessEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00067503 File Offset: 0x00065703
		internal static DayOfWeekTimeContext EndDay(ActivityManager manager, string variableName)
		{
			return (DayOfWeekTimeContext)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00067516 File Offset: 0x00065716
		internal static DayOfWeekTimeContext EndDayTime(ActivityManager manager, string variableName)
		{
			return (DayOfWeekTimeContext)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0006752C File Offset: 0x0006572C
		internal static ExDateTime EndTime(ActivityManager manager, string variableName)
		{
			object obj;
			if ((obj = manager.ReadVariable(variableName)) == null)
			{
				obj = default(ExDateTime);
			}
			return (ExDateTime)obj;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00067557 File Offset: 0x00065757
		internal static ITempWavFile CustomGreeting(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0006756A File Offset: 0x0006576A
		internal static object HaveSummary(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00067578 File Offset: 0x00065778
		internal static object IsInProgress(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00067586 File Offset: 0x00065786
		internal static object IsMaxEmail(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00067594 File Offset: 0x00065794
		internal static object LastActivity(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000675A2 File Offset: 0x000657A2
		internal static string LastInput(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000675B5 File Offset: 0x000657B5
		internal static object LastRecoEvent(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x000675C3 File Offset: 0x000657C3
		internal static string Location(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000675D6 File Offset: 0x000657D6
		internal static object MainMenuQA(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000675E4 File Offset: 0x000657E4
		internal static int NumEmail(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000675FC File Offset: 0x000657FC
		internal static int NumEmailMax(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00067614 File Offset: 0x00065814
		internal static int NumMeetings(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0006762C File Offset: 0x0006582C
		internal static int NumVoicemail(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00067644 File Offset: 0x00065844
		internal static object OcFeature(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00067652 File Offset: 0x00065852
		internal static object Oof(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00067660 File Offset: 0x00065860
		internal static object PilotNumberInfoAnnouncementEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0006766E File Offset: 0x0006586E
		internal static string PilotNumberInfoAnnouncementFilename(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00067681 File Offset: 0x00065881
		internal static object PilotNumberInfoAnnouncementInterruptible(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0006768F File Offset: 0x0006588F
		internal static object PilotNumberWelcomeGreetingEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0006769D File Offset: 0x0006589D
		internal static string PilotNumberWelcomeGreetingFilename(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x000676B0 File Offset: 0x000658B0
		internal static object Repeat(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x000676BE File Offset: 0x000658BE
		internal static List<string> SelectableDepartments(ActivityManager manager, string variableName)
		{
			return (List<string>)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000676D1 File Offset: 0x000658D1
		internal static string SelectedMenu(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000676E4 File Offset: 0x000658E4
		internal static DayOfWeekTimeContext StartDay(ActivityManager manager, string variableName)
		{
			return (DayOfWeekTimeContext)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000676F7 File Offset: 0x000658F7
		internal static DayOfWeekTimeContext StartDayTime(ActivityManager manager, string variableName)
		{
			return (DayOfWeekTimeContext)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0006770C File Offset: 0x0006590C
		internal static ExDateTime StartTime(ActivityManager manager, string variableName)
		{
			object obj;
			if ((obj = manager.ReadVariable(variableName)) == null)
			{
				obj = default(ExDateTime);
			}
			return (ExDateTime)obj;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00067737 File Offset: 0x00065937
		internal static object SkipPinCheck(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00067745 File Offset: 0x00065945
		internal static UMAutoAttendant ThisAutoAttendant(ActivityManager manager, string variableName)
		{
			return (UMAutoAttendant)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00067758 File Offset: 0x00065958
		internal static object TuiPromptEditingEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00067766 File Offset: 0x00065966
		internal static object UseAsr(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00067774 File Offset: 0x00065974
		internal static object UserName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00067782 File Offset: 0x00065982
		internal static object WaitForSourcePartyInfo(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00067790 File Offset: 0x00065990
		internal static List<ScheduleGroup> VarScheduleGroupList(ActivityManager manager, string variableName)
		{
			return (List<ScheduleGroup>)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x000677A3 File Offset: 0x000659A3
		internal static List<TimeRange> VarScheduleIntervalList(ActivityManager manager, string variableName)
		{
			return (List<TimeRange>)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x000677B6 File Offset: 0x000659B6
		internal static ExTimeZone VarTimeZone(ActivityManager manager, string variableName)
		{
			return (ExTimeZone)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x000677CC File Offset: 0x000659CC
		internal static PhoneNumber TargetPhoneNumber(ActivityManager manager, string variableName)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return globalActivityManager.TargetPhoneNumber;
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000677F4 File Offset: 0x000659F4
		internal static object SkipInitialGreetings(ActivityManager manager, string variableName)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return globalActivityManager.SkipInitialGreetings;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00067820 File Offset: 0x00065A20
		internal static object ConsumerDialPlan(ActivityManager manager, string variableName)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return globalActivityManager.ConsumerDialPlan;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0006784C File Offset: 0x00065A4C
		internal static object DialPlanType(ActivityManager manager, string variableName)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return globalActivityManager.DialPlanType;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00067874 File Offset: 0x00065A74
		internal static object ContactsAccessEnabled(ActivityManager manager, string variableName)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return globalActivityManager.ContactsAccessEnabled;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000678A0 File Offset: 0x00065AA0
		internal static object DirectoryAccessEnabled(ActivityManager manager, string variableName)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return globalActivityManager.DirectoryAccessEnabled;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000678CC File Offset: 0x00065ACC
		internal static object AddressBookEnabled(ActivityManager manager, string variableName)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return globalActivityManager.AddressBookEnabled;
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x000678F8 File Offset: 0x00065AF8
		internal static object VoiceResponseToOtherMessageTypesEnabled(ActivityManager manager, string variableName)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return globalActivityManager.VoiceResponseToOtherMessageTypesEnabled;
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00067924 File Offset: 0x00065B24
		internal static object IsPromptProvisioningCall(ActivityManager manager, string variableName)
		{
			GlobalActivityManager globalActivityManager = manager as GlobalActivityManager;
			if (globalActivityManager == null)
			{
				GlobalActivityManager.GetScope(manager, out globalActivityManager);
			}
			return globalActivityManager.IsPromptProvisioningCall;
		}
	}
}
