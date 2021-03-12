using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Storage.PublicFolder;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x0200016B RID: 363
	internal interface IPublicFolderSplitState
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000E99 RID: 3737
		// (set) Token: 0x06000E9A RID: 3738
		Version VersionNumber { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000E9B RID: 3739
		// (set) Token: 0x06000E9C RID: 3740
		SplitProgressState ProgressState { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000E9D RID: 3741
		// (set) Token: 0x06000E9E RID: 3742
		Guid TargetMailboxGuid { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000E9F RID: 3743
		// (set) Token: 0x06000EA0 RID: 3744
		IPublicFolderSplitPlan SplitPlan { get; set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000EA1 RID: 3745
		// (set) Token: 0x06000EA2 RID: 3746
		string PublicFolderMoveRequestName { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000EA3 RID: 3747
		// (set) Token: 0x06000EA4 RID: 3748
		ISplitOperationState IdentifyTargetMailboxState { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000EA5 RID: 3749
		// (set) Token: 0x06000EA6 RID: 3750
		ISplitOperationState PrepareTargetMailboxState { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000EA7 RID: 3751
		// (set) Token: 0x06000EA8 RID: 3752
		ISplitOperationState PrepareSplitPlanState { get; set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000EA9 RID: 3753
		// (set) Token: 0x06000EAA RID: 3754
		ISplitOperationState MoveContentState { get; set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000EAB RID: 3755
		// (set) Token: 0x06000EAC RID: 3756
		ISplitOperationState OverallSplitState { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000EAD RID: 3757
		// (set) Token: 0x06000EAE RID: 3758
		IPublicFolderSplitState PreviousSplitJobState { get; set; }

		// Token: 0x06000EAF RID: 3759
		XElement ToXElement();
	}
}
