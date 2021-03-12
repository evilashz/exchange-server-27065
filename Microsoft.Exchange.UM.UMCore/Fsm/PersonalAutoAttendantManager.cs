using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000314 RID: 788
	internal class PersonalAutoAttendantManager
	{
		// Token: 0x060019EF RID: 6639 RVA: 0x00067C85 File Offset: 0x00065E85
		internal static void GetScope(ActivityManager manager, out PersonalAutoAttendantManager scope)
		{
			for (scope = (manager as PersonalAutoAttendantManager); scope == null; scope = (manager as PersonalAutoAttendantManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00067CB8 File Offset: 0x00065EB8
		internal static TransitionBase QuickMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00067CC2 File Offset: 0x00065EC2
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00067CCC File Offset: 0x00065ECC
		internal static TransitionBase GetAutoAttendant(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.GetAutoAttendant(vo));
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00067CF8 File Offset: 0x00065EF8
		internal static TransitionBase PrepareToExecutePAA(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.PrepareToExecutePAA(vo));
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00067D24 File Offset: 0x00065F24
		internal static TransitionBase PrepareForVoiceMail(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.PrepareForVoiceMail(vo));
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00067D50 File Offset: 0x00065F50
		internal static TransitionBase SelectNextAction(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.SelectNextAction(vo));
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00067D7C File Offset: 0x00065F7C
		internal static TransitionBase GetGreeting(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.GetGreeting(vo));
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00067DA8 File Offset: 0x00065FA8
		internal static TransitionBase ProcessSelection(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.ProcessSelection(vo));
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00067DD4 File Offset: 0x00065FD4
		internal static TransitionBase HandleTimeout(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.HandleTimeout(vo));
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x00067E00 File Offset: 0x00066000
		internal static TransitionBase PrepareForTransfer(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.PrepareForTransfer(vo));
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x00067E2C File Offset: 0x0006602C
		internal static TransitionBase PrepareForTransferToVoicemail(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.PrepareForTransferToVoicemail(vo));
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00067E58 File Offset: 0x00066058
		internal static TransitionBase PrepareForFindMe(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.PrepareForFindMe(vo));
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00067E84 File Offset: 0x00066084
		internal static TransitionBase PrepareForTransferToMailbox(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.PrepareForTransferToMailbox(vo));
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00067EB0 File Offset: 0x000660B0
		internal static TransitionBase Reset(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.Reset(vo));
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00067EDC File Offset: 0x000660DC
		internal static TransitionBase PrepareForTransferToPaa(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.PrepareForTransferToPaa(vo));
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00067F08 File Offset: 0x00066108
		internal static TransitionBase TransferToPAASiteFailed(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.TransferToPAASiteFailed(vo));
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00067F34 File Offset: 0x00066134
		internal static TransitionBase ContinueFindMe(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.ContinueFindMe(vo));
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00067F60 File Offset: 0x00066160
		internal static TransitionBase CleanupFindMe(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.CleanupFindMe(vo));
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00067F8C File Offset: 0x0006618C
		internal static TransitionBase StartFindMe(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.StartFindMe(vo));
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00067FB8 File Offset: 0x000661B8
		internal static TransitionBase SetOperatorNumber(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.SetOperatorNumber(vo));
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00067FE4 File Offset: 0x000661E4
		internal static TransitionBase TerminateFindMe(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return manager.GetTransition(personalAutoAttendantManager.TerminateFindMe(vo));
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00068010 File Offset: 0x00066210
		internal static ITempWavFile PersonalGreeting(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.PersonalGreeting;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00068038 File Offset: 0x00066238
		internal static object RecordedName(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.RecordedName;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00068060 File Offset: 0x00066260
		internal static string Context1(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Context1;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00068088 File Offset: 0x00066288
		internal static object TargetName1(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetName1;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000680B0 File Offset: 0x000662B0
		internal static PhoneNumber TargetPhone1(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetPhone1;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000680D8 File Offset: 0x000662D8
		internal static string Context2(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Context2;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00068100 File Offset: 0x00066300
		internal static object TargetName2(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetName2;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00068128 File Offset: 0x00066328
		internal static PhoneNumber TargetPhone2(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetPhone2;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00068150 File Offset: 0x00066350
		internal static string Context3(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Context3;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00068178 File Offset: 0x00066378
		internal static object TargetName3(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetName3;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000681A0 File Offset: 0x000663A0
		internal static PhoneNumber TargetPhone3(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetPhone3;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000681C8 File Offset: 0x000663C8
		internal static string Context4(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Context4;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000681F0 File Offset: 0x000663F0
		internal static object TargetName4(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetName4;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00068218 File Offset: 0x00066418
		internal static PhoneNumber TargetPhone4(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetPhone4;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00068240 File Offset: 0x00066440
		internal static string Context5(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Context5;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00068268 File Offset: 0x00066468
		internal static object TargetName5(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetName5;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00068290 File Offset: 0x00066490
		internal static PhoneNumber TargetPhone5(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetPhone5;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x000682B8 File Offset: 0x000664B8
		internal static string Context6(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Context6;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x000682E0 File Offset: 0x000664E0
		internal static object TargetName6(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetName6;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00068308 File Offset: 0x00066508
		internal static PhoneNumber TargetPhone6(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetPhone6;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00068330 File Offset: 0x00066530
		internal static string Context7(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Context7;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00068358 File Offset: 0x00066558
		internal static object TargetName7(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetName7;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00068380 File Offset: 0x00066580
		internal static PhoneNumber TargetPhone7(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetPhone7;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000683A8 File Offset: 0x000665A8
		internal static string Context8(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Context8;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000683D0 File Offset: 0x000665D0
		internal static object TargetName8(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetName8;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000683F8 File Offset: 0x000665F8
		internal static PhoneNumber TargetPhone8(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetPhone8;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00068420 File Offset: 0x00066620
		internal static string Context9(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Context9;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00068448 File Offset: 0x00066648
		internal static object TargetName9(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetName9;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00068470 File Offset: 0x00066670
		internal static PhoneNumber TargetPhone9(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetPhone9;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00068498 File Offset: 0x00066698
		internal static PhoneNumber TargetPhoneNumber(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetPhoneNumber;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x000684C0 File Offset: 0x000666C0
		internal static object EvaluationStatus(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.EvaluationStatus;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x000684E8 File Offset: 0x000666E8
		internal static object HaveAutoActions(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.HaveAutoActions;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00068514 File Offset: 0x00066714
		internal static object MainMenuUninterruptible(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.MainMenuUninterruptible;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00068540 File Offset: 0x00066740
		internal static object HaveActions(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.HaveActions;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0006856C File Offset: 0x0006676C
		internal static object LastActivity(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.LastActivity;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00068594 File Offset: 0x00066794
		internal static object HaveGreeting(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.HaveGreeting;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000685C0 File Offset: 0x000667C0
		internal static object Key1Enabled(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Key1Enabled;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000685EC File Offset: 0x000667EC
		internal static object MenuType1(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.MenuType1;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00068614 File Offset: 0x00066814
		internal static object Key2Enabled(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Key2Enabled;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00068640 File Offset: 0x00066840
		internal static object MenuType2(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.MenuType2;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00068668 File Offset: 0x00066868
		internal static object Key3Enabled(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Key3Enabled;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00068694 File Offset: 0x00066894
		internal static object MenuType3(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.MenuType3;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000686BC File Offset: 0x000668BC
		internal static object Key4Enabled(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Key4Enabled;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000686E8 File Offset: 0x000668E8
		internal static object MenuType4(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.MenuType4;
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00068710 File Offset: 0x00066910
		internal static object Key5Enabled(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Key5Enabled;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0006873C File Offset: 0x0006693C
		internal static object MenuType5(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.MenuType5;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00068764 File Offset: 0x00066964
		internal static object Key6Enabled(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Key6Enabled;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00068790 File Offset: 0x00066990
		internal static object MenuType6(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.MenuType6;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000687B8 File Offset: 0x000669B8
		internal static object Key7Enabled(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Key7Enabled;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000687E4 File Offset: 0x000669E4
		internal static object MenuType7(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.MenuType7;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0006880C File Offset: 0x00066A0C
		internal static object Key8Enabled(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Key8Enabled;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00068838 File Offset: 0x00066A38
		internal static object MenuType8(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.MenuType8;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00068860 File Offset: 0x00066A60
		internal static object Key9Enabled(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.Key9Enabled;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0006888C File Offset: 0x00066A8C
		internal static object MenuType9(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.MenuType9;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000688B4 File Offset: 0x00066AB4
		internal static object TransferToVoiceMessageEnabled(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TransferToVoiceMessageEnabled;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000688E0 File Offset: 0x00066AE0
		internal static object TimeOut(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TimeOut;
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0006890C File Offset: 0x00066B0C
		internal static object HavePersonalOperator(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.HavePersonalOperator;
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00068938 File Offset: 0x00066B38
		internal static object ExecuteBlindTransfer(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.ExecuteBlindTransfer;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00068964 File Offset: 0x00066B64
		internal static object PermissionCheckFailure(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.PermissionCheckFailure;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00068990 File Offset: 0x00066B90
		internal static object ExecuteTransferToVoiceMessage(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.ExecuteTransferToVoiceMessage;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000689BC File Offset: 0x00066BBC
		internal static object FindMeEnabled(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.FindMeEnabled;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000689E8 File Offset: 0x00066BE8
		internal static object ExecuteTransferToMailbox(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.ExecuteTransferToMailbox;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00068A14 File Offset: 0x00066C14
		internal static object InvalidADContact(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.InvalidADContact;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00068A40 File Offset: 0x00066C40
		internal static object TargetHasValidPAA(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetHasValidPAA;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00068A6C File Offset: 0x00066C6C
		internal static object TargetPAAInDifferentSite(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.TargetPAAInDifferentSite;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00068A98 File Offset: 0x00066C98
		internal static object CallerIsResolvedToADContact(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.CallerIsResolvedToADContact;
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00068AC4 File Offset: 0x00066CC4
		internal static object RecordedNameOfCaller(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.RecordedNameOfCaller;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00068AEC File Offset: 0x00066CEC
		internal static object IsFirstFindMeTry(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.IsFirstFindMeTry;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00068B18 File Offset: 0x00066D18
		internal static object FindMeSuccessful(ActivityManager manager, string variableName)
		{
			PersonalAutoAttendantManager personalAutoAttendantManager = manager as PersonalAutoAttendantManager;
			if (personalAutoAttendantManager == null)
			{
				PersonalAutoAttendantManager.GetScope(manager, out personalAutoAttendantManager);
			}
			return personalAutoAttendantManager.FindMeSuccessful;
		}
	}
}
