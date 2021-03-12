using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000313 RID: 787
	internal class MessagePartManager
	{
		// Token: 0x060019B0 RID: 6576 RVA: 0x00067956 File Offset: 0x00065B56
		internal static void GetScope(ActivityManager manager, out MessagePartManager scope)
		{
			for (scope = (manager as MessagePartManager); scope == null; scope = (manager as MessagePartManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00067989 File Offset: 0x00065B89
		internal static TransitionBase AcceptMeeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00067993 File Offset: 0x00065B93
		internal static TransitionBase AcceptMeetingTentative(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0006799D File Offset: 0x00065B9D
		internal static TransitionBase DeclineMeeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000679A7 File Offset: 0x00065BA7
		internal static TransitionBase DeleteMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000679B1 File Offset: 0x00065BB1
		internal static TransitionBase DeleteThread(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x000679BB File Offset: 0x00065BBB
		internal static TransitionBase DeleteVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x000679C5 File Offset: 0x00065BC5
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000679CF File Offset: 0x00065BCF
		internal static TransitionBase FastForward(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000679D9 File Offset: 0x00065BD9
		internal static TransitionBase FirstMessagePart(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000679E3 File Offset: 0x00065BE3
		internal static TransitionBase FlagMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000679ED File Offset: 0x00065BED
		internal static TransitionBase FlagVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000679F7 File Offset: 0x00065BF7
		internal static TransitionBase Forward(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00067A01 File Offset: 0x00065C01
		internal static TransitionBase ForwardVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00067A0B File Offset: 0x00065C0B
		internal static TransitionBase GetEnvelopInfo(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00067A15 File Offset: 0x00065C15
		internal static TransitionBase HideThread(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00067A1F File Offset: 0x00065C1F
		internal static TransitionBase MarkUnread(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00067A29 File Offset: 0x00065C29
		internal static TransitionBase MarkUnreadVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00067A33 File Offset: 0x00065C33
		internal static TransitionBase More(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00067A3D File Offset: 0x00065C3D
		internal static TransitionBase NextLanguage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00067A47 File Offset: 0x00065C47
		internal static TransitionBase NextLanguagePause(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00067A51 File Offset: 0x00065C51
		internal static TransitionBase NextMessagePart(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00067A5B File Offset: 0x00065C5B
		internal static TransitionBase NextMessageSection(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00067A65 File Offset: 0x00065C65
		internal static TransitionBase Pause(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00067A6F File Offset: 0x00065C6F
		internal static TransitionBase Reply(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00067A79 File Offset: 0x00065C79
		internal static TransitionBase ReplyAll(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00067A83 File Offset: 0x00065C83
		internal static TransitionBase ReplyVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00067A8D File Offset: 0x00065C8D
		internal static TransitionBase ResetPlayback(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00067A97 File Offset: 0x00065C97
		internal static TransitionBase Rewind(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00067AA1 File Offset: 0x00065CA1
		internal static TransitionBase SaveMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00067AAB File Offset: 0x00065CAB
		internal static TransitionBase SaveVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00067AB5 File Offset: 0x00065CB5
		internal static TransitionBase SelectLanguage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00067ABF File Offset: 0x00065CBF
		internal static TransitionBase SelectLanguagePause(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00067AC9 File Offset: 0x00065CC9
		internal static TransitionBase SkipHeader(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00067AD3 File Offset: 0x00065CD3
		internal static TransitionBase SlowDown(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00067ADD File Offset: 0x00065CDD
		internal static TransitionBase SpeedUp(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00067AE7 File Offset: 0x00065CE7
		internal static TransitionBase UndeleteMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00067AF1 File Offset: 0x00065CF1
		internal static TransitionBase UndeleteVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00067AFB File Offset: 0x00065CFB
		internal static object CanUndelete(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00067B09 File Offset: 0x00065D09
		internal static CultureInfo DefaultLanguage(ActivityManager manager, string variableName)
		{
			return (CultureInfo)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00067B1C File Offset: 0x00065D1C
		internal static object IsEmptyText(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00067B2A File Offset: 0x00065D2A
		internal static object IsEmptyWave(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00067B38 File Offset: 0x00065D38
		internal static object IsMissedCall(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00067B46 File Offset: 0x00065D46
		internal static object KnowVoicemailSender(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00067B54 File Offset: 0x00065D54
		internal static object Protected(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00067B62 File Offset: 0x00065D62
		internal static object LanguageDetected(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00067B70 File Offset: 0x00065D70
		internal static object LastActivity(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00067B7E File Offset: 0x00065D7E
		internal static object MeetingRequest(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00067B8C File Offset: 0x00065D8C
		internal static CultureInfo MessageLanguage(ActivityManager manager, string variableName)
		{
			return (CultureInfo)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00067B9F File Offset: 0x00065D9F
		internal static object NamesGrammar(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00067BAD File Offset: 0x00065DAD
		internal static object Owner(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00067BBB File Offset: 0x00065DBB
		internal static object OcFeature(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00067BC9 File Offset: 0x00065DC9
		internal static object PlayAudioContentIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00067BD7 File Offset: 0x00065DD7
		internal static object PlayMixedContentIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00067BE5 File Offset: 0x00065DE5
		internal static object PlayTextContentIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00067BF3 File Offset: 0x00065DF3
		internal static object Repeat(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00067C01 File Offset: 0x00065E01
		internal static List<CultureInfo> SelectableLanguages(ActivityManager manager, string variableName)
		{
			return (List<CultureInfo>)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00067C14 File Offset: 0x00065E14
		internal static EmailNormalizedText TextMessagePart(ActivityManager manager, string variableName)
		{
			return (EmailNormalizedText)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00067C27 File Offset: 0x00065E27
		internal static object TextPart(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00067C35 File Offset: 0x00065E35
		internal static ITempWavFile WaveMessagePart(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00067C48 File Offset: 0x00065E48
		internal static object WavePart(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00067C58 File Offset: 0x00065E58
		internal static object TargetPhoneNumber(ActivityManager manager, string variableName)
		{
			MessagePartManager messagePartManager = manager as MessagePartManager;
			if (messagePartManager == null)
			{
				MessagePartManager.GetScope(manager, out messagePartManager);
			}
			return messagePartManager.TargetPhoneNumber;
		}
	}
}
