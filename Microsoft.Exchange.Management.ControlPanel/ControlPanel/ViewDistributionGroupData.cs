using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200023D RID: 573
	[DataContract]
	public class ViewDistributionGroupData : DistributionGroup
	{
		// Token: 0x0600281E RID: 10270 RVA: 0x0007D6A3 File Offset: 0x0007B8A3
		public ViewDistributionGroupData(DistributionGroup distributionGroup) : base(distributionGroup)
		{
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x0007D6AC File Offset: 0x0007B8AC
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			if (base.WindowsGroup != null)
			{
				this.currentUserIsMemberOfGroup = new bool?(base.WindowsGroup.Members.Contains(RbacPrincipal.Current.ExecutingUserId));
			}
		}

		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x0007D6DB File Offset: 0x0007B8DB
		// (set) Token: 0x06002821 RID: 10273 RVA: 0x0007D6F7 File Offset: 0x0007B8F7
		[DataMember]
		public int TotalMembers
		{
			get
			{
				if (base.WindowsGroup == null)
				{
					return 0;
				}
				return base.WindowsGroup.Members.Count;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x0007D700 File Offset: 0x0007B900
		// (set) Token: 0x06002823 RID: 10275 RVA: 0x0007D753 File Offset: 0x0007B953
		[DataMember]
		public string MemberJoinRestrictionDetails
		{
			get
			{
				switch (base.OriginalDistributionGroup.MemberJoinRestriction)
				{
				case MemberUpdateType.Closed:
					return OwaOptionStrings.JoinRestrictionClosedDetails;
				case MemberUpdateType.Open:
					return OwaOptionStrings.JoinRestrictionOpenDetails;
				case MemberUpdateType.ApprovalRequired:
					return OwaOptionStrings.JoinRestrictionApprovalRequiredDetails;
				default:
					throw new NotSupportedException();
				}
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x0007D75A File Offset: 0x0007B95A
		// (set) Token: 0x06002825 RID: 10277 RVA: 0x0007D792 File Offset: 0x0007B992
		[DataMember]
		public string ActionShown
		{
			get
			{
				if (this.currentUserIsMemberOfGroup != null)
				{
					return this.currentUserIsMemberOfGroup.Value ? OwaOptionStrings.Depart : OwaOptionStrings.Join;
				}
				return OwaOptionStrings.OkButtonText;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C51 RID: 7249
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x0007D79C File Offset: 0x0007B99C
		// (set) Token: 0x06002827 RID: 10279 RVA: 0x0007D7D0 File Offset: 0x0007B9D0
		[DataMember]
		public bool? CurrentUserIsMemberOfGroup
		{
			get
			{
				if (this.currentUserIsMemberOfGroup != null)
				{
					return new bool?(this.currentUserIsMemberOfGroup.Value);
				}
				return null;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x0007D7D8 File Offset: 0x0007B9D8
		// (set) Token: 0x06002829 RID: 10281 RVA: 0x0007D807 File Offset: 0x0007BA07
		[DataMember]
		public string CommitConfirmMessage
		{
			get
			{
				if (!this.currentUserIsMemberOfGroup.GetValueOrDefault())
				{
					return null;
				}
				return OwaOptionStrings.DepartGroupConfirmation.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C53 RID: 7251
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x0007D80E File Offset: 0x0007BA0E
		// (set) Token: 0x0600282B RID: 10283 RVA: 0x0007D825 File Offset: 0x0007BA25
		[DataMember]
		public string CommitConfirmMessageTargetName
		{
			get
			{
				if (!this.currentUserIsMemberOfGroup.GetValueOrDefault())
				{
					return null;
				}
				return base.DisplayName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04002037 RID: 8247
		private bool? currentUserIsMemberOfGroup;
	}
}
