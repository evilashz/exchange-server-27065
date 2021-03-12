using System;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000276 RID: 630
	internal sealed class SharingLevelOfDetails
	{
		// Token: 0x0600120E RID: 4622 RVA: 0x000545A9 File Offset: 0x000527A9
		public SharingLevelOfDetails()
		{
			this.currentLevel = LevelOfDetails.Unknown;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000545B8 File Offset: 0x000527B8
		public SharingLevelOfDetails(BaseFolderType folder)
		{
			this.currentLevel = LevelOfDetails.Unknown;
			CalendarFolderType calendarFolderType = folder as CalendarFolderType;
			if (calendarFolderType == null)
			{
				ContactsFolderType contactsFolderType = folder as ContactsFolderType;
				if (contactsFolderType != null)
				{
					switch (contactsFolderType.SharingEffectiveRights)
					{
					case PermissionReadAccessType.None:
						this.currentLevel = LevelOfDetails.None;
						return;
					case PermissionReadAccessType.FullDetails:
						this.currentLevel = LevelOfDetails.Full;
						break;
					default:
						return;
					}
				}
				return;
			}
			switch (calendarFolderType.SharingEffectiveRights)
			{
			case CalendarPermissionReadAccessType.None:
				this.currentLevel = LevelOfDetails.None;
				return;
			case CalendarPermissionReadAccessType.TimeOnly:
				this.currentLevel = LevelOfDetails.Availability;
				return;
			case CalendarPermissionReadAccessType.TimeAndSubjectAndLocation:
				this.currentLevel = LevelOfDetails.Limited;
				return;
			case CalendarPermissionReadAccessType.FullDetails:
				this.currentLevel = LevelOfDetails.Full;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00054649 File Offset: 0x00052849
		public SharingLevelOfDetails(LevelOfDetails levelOfDetails)
		{
			this.currentLevel = levelOfDetails;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00054658 File Offset: 0x00052858
		public static implicit operator LevelOfDetails(SharingLevelOfDetails levelOfDetails)
		{
			return levelOfDetails.currentLevel;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00054660 File Offset: 0x00052860
		public static bool operator ==(SharingLevelOfDetails left, LevelOfDetails right)
		{
			return left.currentLevel == right;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0005466B File Offset: 0x0005286B
		public static bool operator !=(SharingLevelOfDetails left, LevelOfDetails right)
		{
			return left.currentLevel != right;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00054679 File Offset: 0x00052879
		public static bool operator >(SharingLevelOfDetails left, SharingLevelOfDetails right)
		{
			return left.currentLevel > right.currentLevel;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00054689 File Offset: 0x00052889
		public static bool operator <(SharingLevelOfDetails left, SharingLevelOfDetails right)
		{
			return left.currentLevel < right.currentLevel;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0005469C File Offset: 0x0005289C
		public override bool Equals(object otherObject)
		{
			SharingLevelOfDetails sharingLevelOfDetails = otherObject as SharingLevelOfDetails;
			return sharingLevelOfDetails != null && this == sharingLevelOfDetails;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000546C1 File Offset: 0x000528C1
		public override int GetHashCode()
		{
			return this.currentLevel.GetHashCode();
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x000546D3 File Offset: 0x000528D3
		public override string ToString()
		{
			return this.currentLevel.ToString();
		}

		// Token: 0x04000BDE RID: 3038
		private LevelOfDetails currentLevel;
	}
}
