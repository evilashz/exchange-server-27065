using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000005 RID: 5
	public static class OwaOptionClientStrings
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0000C7CC File Offset: 0x0000A9CC
		static OwaOptionClientStrings()
		{
			OwaOptionClientStrings.stringIDs.Add(2915177744U, "TeamMailboxMembershipString4");
			OwaOptionClientStrings.stringIDs.Add(3044047780U, "TeamMailboxYourRoleMember");
			OwaOptionClientStrings.stringIDs.Add(1220496629U, "TeamMailboxSyncSuccess");
			OwaOptionClientStrings.stringIDs.Add(1210225031U, "TeamMailboxCreationFailedPageLinkBackToSiteDisabled");
			OwaOptionClientStrings.stringIDs.Add(3318462271U, "TeamMailboxMembershipString1");
			OwaOptionClientStrings.stringIDs.Add(1582897606U, "NewTeamMailboxFailed");
			OwaOptionClientStrings.stringIDs.Add(2554950321U, "TeamMailboxYourRoleOwner");
			OwaOptionClientStrings.stringIDs.Add(1347892332U, "TeamMailboxLifecycleStatusActiveString2");
			OwaOptionClientStrings.stringIDs.Add(3789638106U, "TeamMailboxStartedMaintenanceSync");
			OwaOptionClientStrings.stringIDs.Add(1971980913U, "TeamMailboxStartedMembershipSync");
			OwaOptionClientStrings.stringIDs.Add(1904041070U, "UpdateTimeZonePrompt");
			OwaOptionClientStrings.stringIDs.Add(319275761U, "Install");
			OwaOptionClientStrings.stringIDs.Add(3457463314U, "RetrieveInfo");
			OwaOptionClientStrings.stringIDs.Add(3735025842U, "TeamMailboxCreationCompletionPageHeadingText");
			OwaOptionClientStrings.stringIDs.Add(3477886725U, "TeamMailboxLifecycleClosed");
			OwaOptionClientStrings.stringIDs.Add(310858686U, "TeamMailboxLifecycleStatusClosedString2");
			OwaOptionClientStrings.stringIDs.Add(3896101628U, "TeamMailboxSyncError");
			OwaOptionClientStrings.stringIDs.Add(1336091985U, "ClearOmsAccountPrompt");
			OwaOptionClientStrings.stringIDs.Add(1752378330U, "TeamMailboxMembershipString2");
			OwaOptionClientStrings.stringIDs.Add(3861666036U, "SiteMailboxEmailMeDiagnosticsConfirmation");
			OwaOptionClientStrings.stringIDs.Add(4213595441U, "AgaveInstallTitle");
			OwaOptionClientStrings.stringIDs.Add(4121209658U, "TeamMailboxCreationCompletionPageDetailedText");
			OwaOptionClientStrings.stringIDs.Add(186294389U, "TeamMailboxMembershipString3");
			OwaOptionClientStrings.stringIDs.Add(2400383839U, "NewTeamMailboxInProgress");
			OwaOptionClientStrings.stringIDs.Add(767215867U, "TeamMailboxCreationCompletionPageLinkTrySiteMailbox");
			OwaOptionClientStrings.stringIDs.Add(2503542192U, "TeamMailboxSyncStatus");
			OwaOptionClientStrings.stringIDs.Add(2773017722U, "TeamMailboxSyncDate");
			OwaOptionClientStrings.stringIDs.Add(1347892331U, "TeamMailboxLifecycleStatusActiveString1");
			OwaOptionClientStrings.stringIDs.Add(1300295012U, "FileUploadFailed");
			OwaOptionClientStrings.stringIDs.Add(167704095U, "TeamMailboxLifecycleActive");
			OwaOptionClientStrings.stringIDs.Add(1138644365U, "TeamMailboxYourRoleNoAccess");
			OwaOptionClientStrings.stringIDs.Add(4287680888U, "PleaseConfirm");
			OwaOptionClientStrings.stringIDs.Add(1813773069U, "TeamMailboxSharePointConnectedTrue");
			OwaOptionClientStrings.stringIDs.Add(4124603460U, "TeamMailboxCreationCompletionPageLinkBackToSite");
			OwaOptionClientStrings.stringIDs.Add(3751931040U, "TeamMailboxStartedDocumentSync");
			OwaOptionClientStrings.stringIDs.Add(489758736U, "TeamMailboxSharePointConnectedFalse");
			OwaOptionClientStrings.stringIDs.Add(3476723026U, "TeamMailboxSyncNotAvailable");
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		public static string TeamMailboxMembershipString4
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxMembershipString4");
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000CAFD File Offset: 0x0000ACFD
		public static string TeamMailboxYourRoleMember
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxYourRoleMember");
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000CB0E File Offset: 0x0000AD0E
		public static string TeamMailboxSyncSuccess
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxSyncSuccess");
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000CB1F File Offset: 0x0000AD1F
		public static string TeamMailboxCreationFailedPageLinkBackToSiteDisabled
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxCreationFailedPageLinkBackToSiteDisabled");
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000CB30 File Offset: 0x0000AD30
		public static string TeamMailboxMembershipString1
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxMembershipString1");
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000CB41 File Offset: 0x0000AD41
		public static string NewTeamMailboxFailed
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("NewTeamMailboxFailed");
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000CB52 File Offset: 0x0000AD52
		public static string TeamMailboxYourRoleOwner
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxYourRoleOwner");
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000CB63 File Offset: 0x0000AD63
		public static string TeamMailboxLifecycleStatusActiveString2
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxLifecycleStatusActiveString2");
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000CB74 File Offset: 0x0000AD74
		public static string AgaveVersion(string ver)
		{
			return string.Format(OwaOptionClientStrings.ResourceManager.GetString("AgaveVersion"), ver);
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000CB8B File Offset: 0x0000AD8B
		public static string TeamMailboxStartedMaintenanceSync
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxStartedMaintenanceSync");
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		public static string TeamMailboxStartedMembershipSync
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxStartedMembershipSync");
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000CBAD File Offset: 0x0000ADAD
		public static string UpdateTimeZonePrompt
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("UpdateTimeZonePrompt");
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000CBBE File Offset: 0x0000ADBE
		public static string TeamMailboxLifecycleStatusClosedString1(string time)
		{
			return string.Format(OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxLifecycleStatusClosedString1"), time);
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000CBD5 File Offset: 0x0000ADD5
		public static string Install
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("Install");
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000CBE6 File Offset: 0x0000ADE6
		public static string RetrieveInfo
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("RetrieveInfo");
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000CBF7 File Offset: 0x0000ADF7
		public static string TeamMailboxCreationCompletionPageHeadingText
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxCreationCompletionPageHeadingText");
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000CC08 File Offset: 0x0000AE08
		public static string TeamMailboxLifecycleClosed
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxLifecycleClosed");
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000CC19 File Offset: 0x0000AE19
		public static string TeamMailboxLifecycleStatusClosedString2
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxLifecycleStatusClosedString2");
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000CC2A File Offset: 0x0000AE2A
		public static string AgaveProvider(string publisher)
		{
			return string.Format(OwaOptionClientStrings.ResourceManager.GetString("AgaveProvider"), publisher);
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000CC41 File Offset: 0x0000AE41
		public static string TeamMailboxSyncError
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxSyncError");
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000CC52 File Offset: 0x0000AE52
		public static string AgaveDisplayName(string name)
		{
			return string.Format(OwaOptionClientStrings.ResourceManager.GetString("AgaveDisplayName"), name);
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000CC69 File Offset: 0x0000AE69
		public static string ClearOmsAccountPrompt
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("ClearOmsAccountPrompt");
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000CC7A File Offset: 0x0000AE7A
		public static string TeamMailboxMembershipString2
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxMembershipString2");
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000CC8B File Offset: 0x0000AE8B
		public static string AgaveRequirementsValue(string details)
		{
			return string.Format(OwaOptionClientStrings.ResourceManager.GetString("AgaveRequirementsValue"), details);
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000CCA2 File Offset: 0x0000AEA2
		public static string SiteMailboxEmailMeDiagnosticsConfirmation
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("SiteMailboxEmailMeDiagnosticsConfirmation");
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000CCB3 File Offset: 0x0000AEB3
		public static string AgaveInstallTitle
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("AgaveInstallTitle");
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000CCC4 File Offset: 0x0000AEC4
		public static string TeamMailboxCreationCompletionPageDetailedText
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxCreationCompletionPageDetailedText");
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000CCD5 File Offset: 0x0000AED5
		public static string TeamMailboxMembershipString3
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxMembershipString3");
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000CCE6 File Offset: 0x0000AEE6
		public static string NewTeamMailboxInProgress
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("NewTeamMailboxInProgress");
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000CCF7 File Offset: 0x0000AEF7
		public static string TeamMailboxCreationCompletionPageLinkTrySiteMailbox
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxCreationCompletionPageLinkTrySiteMailbox");
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000CD08 File Offset: 0x0000AF08
		public static string TeamMailboxSyncStatus
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxSyncStatus");
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000CD19 File Offset: 0x0000AF19
		public static string TeamMailboxSyncDate
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxSyncDate");
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000CD2A File Offset: 0x0000AF2A
		public static string SendPasscodeSucceededFormat(string phone)
		{
			return string.Format(OwaOptionClientStrings.ResourceManager.GetString("SendPasscodeSucceededFormat"), phone);
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000CD41 File Offset: 0x0000AF41
		public static string TeamMailboxLifecycleStatusActiveString1
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxLifecycleStatusActiveString1");
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000CD52 File Offset: 0x0000AF52
		public static string FileUploadFailed
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("FileUploadFailed");
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000CD63 File Offset: 0x0000AF63
		public static string TeamMailboxLifecycleActive
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxLifecycleActive");
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000CD74 File Offset: 0x0000AF74
		public static string TeamMailboxYourRoleNoAccess
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxYourRoleNoAccess");
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000CD85 File Offset: 0x0000AF85
		public static string PleaseConfirm
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("PleaseConfirm");
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000CD96 File Offset: 0x0000AF96
		public static string TeamMailboxSharePointConnectedTrue
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxSharePointConnectedTrue");
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000CDA7 File Offset: 0x0000AFA7
		public static string TeamMailboxCreationCompletionPageLinkBackToSite
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxCreationCompletionPageLinkBackToSite");
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		public static string TeamMailboxStartedDocumentSync
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxStartedDocumentSync");
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000CDC9 File Offset: 0x0000AFC9
		public static string TeamMailboxSharePointConnectedFalse
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxSharePointConnectedFalse");
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000CDDA File Offset: 0x0000AFDA
		public static string TeamMailboxSyncNotAvailable
		{
			get
			{
				return OwaOptionClientStrings.ResourceManager.GetString("TeamMailboxSyncNotAvailable");
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000CDEB File Offset: 0x0000AFEB
		public static string GetLocalizedString(OwaOptionClientStrings.IDs key)
		{
			return OwaOptionClientStrings.ResourceManager.GetString(OwaOptionClientStrings.stringIDs[(uint)key]);
		}

		// Token: 0x0400035A RID: 858
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(37);

		// Token: 0x0400035B RID: 859
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.Management.ControlPanel.OwaOptionClientStrings", typeof(OwaOptionClientStrings).GetTypeInfo().Assembly);

		// Token: 0x02000006 RID: 6
		public enum IDs : uint
		{
			// Token: 0x0400035D RID: 861
			TeamMailboxMembershipString4 = 2915177744U,
			// Token: 0x0400035E RID: 862
			TeamMailboxYourRoleMember = 3044047780U,
			// Token: 0x0400035F RID: 863
			TeamMailboxSyncSuccess = 1220496629U,
			// Token: 0x04000360 RID: 864
			TeamMailboxCreationFailedPageLinkBackToSiteDisabled = 1210225031U,
			// Token: 0x04000361 RID: 865
			TeamMailboxMembershipString1 = 3318462271U,
			// Token: 0x04000362 RID: 866
			NewTeamMailboxFailed = 1582897606U,
			// Token: 0x04000363 RID: 867
			TeamMailboxYourRoleOwner = 2554950321U,
			// Token: 0x04000364 RID: 868
			TeamMailboxLifecycleStatusActiveString2 = 1347892332U,
			// Token: 0x04000365 RID: 869
			TeamMailboxStartedMaintenanceSync = 3789638106U,
			// Token: 0x04000366 RID: 870
			TeamMailboxStartedMembershipSync = 1971980913U,
			// Token: 0x04000367 RID: 871
			UpdateTimeZonePrompt = 1904041070U,
			// Token: 0x04000368 RID: 872
			Install = 319275761U,
			// Token: 0x04000369 RID: 873
			RetrieveInfo = 3457463314U,
			// Token: 0x0400036A RID: 874
			TeamMailboxCreationCompletionPageHeadingText = 3735025842U,
			// Token: 0x0400036B RID: 875
			TeamMailboxLifecycleClosed = 3477886725U,
			// Token: 0x0400036C RID: 876
			TeamMailboxLifecycleStatusClosedString2 = 310858686U,
			// Token: 0x0400036D RID: 877
			TeamMailboxSyncError = 3896101628U,
			// Token: 0x0400036E RID: 878
			ClearOmsAccountPrompt = 1336091985U,
			// Token: 0x0400036F RID: 879
			TeamMailboxMembershipString2 = 1752378330U,
			// Token: 0x04000370 RID: 880
			SiteMailboxEmailMeDiagnosticsConfirmation = 3861666036U,
			// Token: 0x04000371 RID: 881
			AgaveInstallTitle = 4213595441U,
			// Token: 0x04000372 RID: 882
			TeamMailboxCreationCompletionPageDetailedText = 4121209658U,
			// Token: 0x04000373 RID: 883
			TeamMailboxMembershipString3 = 186294389U,
			// Token: 0x04000374 RID: 884
			NewTeamMailboxInProgress = 2400383839U,
			// Token: 0x04000375 RID: 885
			TeamMailboxCreationCompletionPageLinkTrySiteMailbox = 767215867U,
			// Token: 0x04000376 RID: 886
			TeamMailboxSyncStatus = 2503542192U,
			// Token: 0x04000377 RID: 887
			TeamMailboxSyncDate = 2773017722U,
			// Token: 0x04000378 RID: 888
			TeamMailboxLifecycleStatusActiveString1 = 1347892331U,
			// Token: 0x04000379 RID: 889
			FileUploadFailed = 1300295012U,
			// Token: 0x0400037A RID: 890
			TeamMailboxLifecycleActive = 167704095U,
			// Token: 0x0400037B RID: 891
			TeamMailboxYourRoleNoAccess = 1138644365U,
			// Token: 0x0400037C RID: 892
			PleaseConfirm = 4287680888U,
			// Token: 0x0400037D RID: 893
			TeamMailboxSharePointConnectedTrue = 1813773069U,
			// Token: 0x0400037E RID: 894
			TeamMailboxCreationCompletionPageLinkBackToSite = 4124603460U,
			// Token: 0x0400037F RID: 895
			TeamMailboxStartedDocumentSync = 3751931040U,
			// Token: 0x04000380 RID: 896
			TeamMailboxSharePointConnectedFalse = 489758736U,
			// Token: 0x04000381 RID: 897
			TeamMailboxSyncNotAvailable = 3476723026U
		}

		// Token: 0x02000007 RID: 7
		private enum ParamIDs
		{
			// Token: 0x04000383 RID: 899
			AgaveVersion,
			// Token: 0x04000384 RID: 900
			TeamMailboxLifecycleStatusClosedString1,
			// Token: 0x04000385 RID: 901
			AgaveProvider,
			// Token: 0x04000386 RID: 902
			AgaveDisplayName,
			// Token: 0x04000387 RID: 903
			AgaveRequirementsValue,
			// Token: 0x04000388 RID: 904
			SendPasscodeSucceededFormat
		}
	}
}
