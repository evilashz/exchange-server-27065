using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200030E RID: 782
	internal class DirectorySearchManager
	{
		// Token: 0x060018B5 RID: 6325 RVA: 0x0006677D File Offset: 0x0006497D
		internal static void GetScope(ActivityManager manager, out DirectorySearchManager scope)
		{
			for (scope = (manager as DirectorySearchManager); scope == null; scope = (manager as DirectorySearchManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000667B0 File Offset: 0x000649B0
		internal static TransitionBase AnyMoreResultsToPlay(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x000667BA File Offset: 0x000649BA
		internal static TransitionBase CanonicalizeNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x000667C4 File Offset: 0x000649C4
		internal static TransitionBase ChangeSearchMode(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x000667CE File Offset: 0x000649CE
		internal static TransitionBase ChangeSearchTarget(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000667D8 File Offset: 0x000649D8
		internal static TransitionBase CheckDialPermissions(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x000667E2 File Offset: 0x000649E2
		internal static TransitionBase ContinueSearch(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x000667EC File Offset: 0x000649EC
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000667F6 File Offset: 0x000649F6
		internal static TransitionBase HandleFaxTone(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00066800 File Offset: 0x00064A00
		internal static TransitionBase HandleInvalidSearchKey(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0006680A File Offset: 0x00064A0A
		internal static TransitionBase PlayContactDetails(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00066814 File Offset: 0x00064A14
		internal static TransitionBase PrepareForTransferToSendMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0006681E File Offset: 0x00064A1E
		internal static TransitionBase PrepareForProtectedSubscriberOperatorTransfer(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00066828 File Offset: 0x00064A28
		internal static TransitionBase PrepareForUserInitiatedOperatorTransferFromOpeningMenu(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00066832 File Offset: 0x00064A32
		internal static TransitionBase PrepareForUserInitiatedOperatorTransfer(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0006683C File Offset: 0x00064A3C
		internal static TransitionBase ProcessResult(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00066846 File Offset: 0x00064A46
		internal static TransitionBase QuickMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00066850 File Offset: 0x00064A50
		internal static TransitionBase ReplayResults(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0006685A File Offset: 0x00064A5A
		internal static TransitionBase SearchDirectory(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00066864 File Offset: 0x00064A64
		internal static TransitionBase SearchDirectoryByExtension(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0006686E File Offset: 0x00064A6E
		internal static TransitionBase SetBusinessNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00066878 File Offset: 0x00064A78
		internal static TransitionBase SetHomeNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00066882 File Offset: 0x00064A82
		internal static TransitionBase SetMobileNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0006688C File Offset: 0x00064A8C
		internal static TransitionBase SetOperatorNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00066896 File Offset: 0x00064A96
		internal static TransitionBase SetSearchTargetToContacts(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000668A0 File Offset: 0x00064AA0
		internal static TransitionBase SetSearchTargetToGlobalAddressList(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000668AA File Offset: 0x00064AAA
		internal static TransitionBase StartNewSearch(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x000668B4 File Offset: 0x00064AB4
		internal static TransitionBase ValidateSearchSelection(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x000668BE File Offset: 0x00064ABE
		internal static TransitionBase CheckNonUmExtension(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000668C8 File Offset: 0x00064AC8
		internal static object Aa_customizedMenuEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x000668D6 File Offset: 0x00064AD6
		internal static object Aa_transferToOperatorEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x000668E4 File Offset: 0x00064AE4
		internal static object AllowCall(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000668F2 File Offset: 0x00064AF2
		internal static object AllowMessage(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00066900 File Offset: 0x00064B00
		internal static PhoneNumber BusinessNumber(ActivityManager manager, string variableName)
		{
			return (PhoneNumber)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00066913 File Offset: 0x00064B13
		internal static string ContactLocation(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00066926 File Offset: 0x00064B26
		internal static object CurrentSearchMode(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00066934 File Offset: 0x00064B34
		internal static object EmailAlias(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x00066942 File Offset: 0x00064B42
		internal static object ExceedRetryLimit(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00066950 File Offset: 0x00064B50
		internal static object FirstNameLastName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0006695E File Offset: 0x00064B5E
		internal static object GlobalAddressList(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0006696C File Offset: 0x00064B6C
		internal static object HaveDialableBusinessNumber(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0006697A File Offset: 0x00064B7A
		internal static object HaveDialableHomeNumber(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x00066988 File Offset: 0x00064B88
		internal static object HaveDialableMobileNumber(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00066996 File Offset: 0x00064B96
		internal static object HaveMorePartialMatches(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x000669A4 File Offset: 0x00064BA4
		internal static object InitialSearchTarget(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000669B2 File Offset: 0x00064BB2
		internal static string InvalidSearchSelection(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x000669C5 File Offset: 0x00064BC5
		internal static object LastInput(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000669D3 File Offset: 0x00064BD3
		internal static object LastNameFirstName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000669E1 File Offset: 0x00064BE1
		internal static object NewSearch(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x000669EF File Offset: 0x00064BEF
		internal static object None(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x000669FD File Offset: 0x00064BFD
		internal static object PersonalContacts(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00066A0B File Offset: 0x00064C0B
		internal static object PilotNumberTransferToOperatorEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00066A19 File Offset: 0x00064C19
		internal static object PrimarySearchMode(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00066A27 File Offset: 0x00064C27
		internal static object Promptindex(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00066A35 File Offset: 0x00064C35
		internal static object SearchByExtension(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00066A43 File Offset: 0x00064C43
		internal static string SearchInput(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00066A56 File Offset: 0x00064C56
		internal static object SearchTarget(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00066A64 File Offset: 0x00064C64
		internal static object SecondarySearchMode(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00066A72 File Offset: 0x00064C72
		internal static object UseAsr(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00066A80 File Offset: 0x00064C80
		internal static object UserName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00066A90 File Offset: 0x00064C90
		internal static PhoneNumber TargetPhoneNumber(ActivityManager manager, string variableName)
		{
			DirectorySearchManager directorySearchManager = manager as DirectorySearchManager;
			if (directorySearchManager == null)
			{
				DirectorySearchManager.GetScope(manager, out directorySearchManager);
			}
			return directorySearchManager.TargetPhoneNumber;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00066AB8 File Offset: 0x00064CB8
		internal static ContactSearchItem SelectedSearchItem(ActivityManager manager, string variableName)
		{
			DirectorySearchManager directorySearchManager = manager as DirectorySearchManager;
			if (directorySearchManager == null)
			{
				DirectorySearchManager.GetScope(manager, out directorySearchManager);
			}
			return directorySearchManager.SelectedSearchItem;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00066AE0 File Offset: 0x00064CE0
		internal static object CanSendEmail(ActivityManager manager, string variableName)
		{
			DirectorySearchManager directorySearchManager = manager as DirectorySearchManager;
			if (directorySearchManager == null)
			{
				DirectorySearchManager.GetScope(manager, out directorySearchManager);
			}
			return directorySearchManager.CanSendEmail;
		}
	}
}
