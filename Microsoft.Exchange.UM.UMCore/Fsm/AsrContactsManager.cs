using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000309 RID: 777
	internal class AsrContactsManager
	{
		// Token: 0x06001788 RID: 6024 RVA: 0x000654A2 File Offset: 0x000636A2
		internal static void GetScope(ActivityManager manager, out AsrContactsManager scope)
		{
			for (scope = (manager as AsrContactsManager); scope == null; scope = (manager as AsrContactsManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x000654D5 File Offset: 0x000636D5
		internal static TransitionBase CanonicalizeNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x000654DF File Offset: 0x000636DF
		internal static TransitionBase CheckDialPermissions(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000654E9 File Offset: 0x000636E9
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000654F3 File Offset: 0x000636F3
		internal static TransitionBase HandlePlatformFailure(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000654FD File Offset: 0x000636FD
		internal static TransitionBase InitializeNamesGrammar(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00065507 File Offset: 0x00063707
		internal static TransitionBase PrepareForTransferToCell(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00065511 File Offset: 0x00063711
		internal static TransitionBase PrepareForTransferToHome(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0006551B File Offset: 0x0006371B
		internal static TransitionBase PrepareForTransferToOffice(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00065525 File Offset: 0x00063725
		internal static TransitionBase ProcessResult(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0006552F File Offset: 0x0006372F
		internal static TransitionBase RetryAsrSearch(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00065539 File Offset: 0x00063739
		internal static TransitionBase SaveRecoEvent(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00065543 File Offset: 0x00063743
		internal static TransitionBase SelectEmailAddress(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0006554D File Offset: 0x0006374D
		internal static TransitionBase SetContactInfo(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00065557 File Offset: 0x00063757
		internal static TransitionBase SetContactInfoVariables(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00065561 File Offset: 0x00063761
		internal static TransitionBase SetEmailAddress(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0006556B File Offset: 0x0006376B
		internal static TransitionBase SetInitialSearchTargetContacts(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00065575 File Offset: 0x00063775
		internal static TransitionBase SetInitialSearchTargetGAL(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0006557F File Offset: 0x0006377F
		internal static TransitionBase SetName(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00065589 File Offset: 0x00063789
		internal static TransitionBase SetSpeechError(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00065593 File Offset: 0x00063793
		internal static object CallingType(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x000655A1 File Offset: 0x000637A1
		internal static string Email1(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x000655B4 File Offset: 0x000637B4
		internal static string Email2(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000655C7 File Offset: 0x000637C7
		internal static string Email3(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000655DA File Offset: 0x000637DA
		internal static int EmailAddressSelection(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000655F2 File Offset: 0x000637F2
		internal static object HasCell(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00065600 File Offset: 0x00063800
		internal static object HasHome(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0006560E File Offset: 0x0006380E
		internal static object HasOffice(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0006561C File Offset: 0x0006381C
		internal static object HaveEmail1(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0006562A File Offset: 0x0006382A
		internal static object HaveEmail2(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00065638 File Offset: 0x00063838
		internal static object HaveEmail3(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00065646 File Offset: 0x00063846
		internal static object InitialSearchTarget(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00065654 File Offset: 0x00063854
		internal static object LastActivity(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00065662 File Offset: 0x00063862
		internal static object LastRecoEvent(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00065670 File Offset: 0x00063870
		internal static PhoneNumber MobileNumber(ActivityManager manager, string variableName)
		{
			return (PhoneNumber)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00065683 File Offset: 0x00063883
		internal static string OtherAddress(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00065696 File Offset: 0x00063896
		internal static object Repeat(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x000656A4 File Offset: 0x000638A4
		internal static object SavedRecoEvent(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x000656B2 File Offset: 0x000638B2
		internal static object UserName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x000656C0 File Offset: 0x000638C0
		internal static ContactSearchItem SelectedSearchItem(ActivityManager manager, string variableName)
		{
			AsrContactsManager asrContactsManager = manager as AsrContactsManager;
			if (asrContactsManager == null)
			{
				AsrContactsManager.GetScope(manager, out asrContactsManager);
			}
			return asrContactsManager.SelectedSearchItem;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000656E8 File Offset: 0x000638E8
		internal static PhoneNumber TargetPhoneNumber(ActivityManager manager, string variableName)
		{
			AsrContactsManager asrContactsManager = manager as AsrContactsManager;
			if (asrContactsManager == null)
			{
				AsrContactsManager.GetScope(manager, out asrContactsManager);
			}
			return asrContactsManager.TargetPhoneNumber;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00065710 File Offset: 0x00063910
		internal static object CanSendEmail(ActivityManager manager, string variableName)
		{
			AsrContactsManager asrContactsManager = manager as AsrContactsManager;
			if (asrContactsManager == null)
			{
				AsrContactsManager.GetScope(manager, out asrContactsManager);
			}
			return asrContactsManager.CanSendEmail;
		}
	}
}
