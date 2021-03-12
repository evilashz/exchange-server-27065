using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000998 RID: 2456
	internal sealed class GetActiveRetentionPolicyTagsCommand : RetentionPolicyTagsCommand<GetRetentionPolicyTagsRequest, GetRetentionPolicyTagsResponse>
	{
		// Token: 0x06004614 RID: 17940 RVA: 0x000F6A0A File Offset: 0x000F4C0A
		public GetActiveRetentionPolicyTagsCommand(CallContext callContext, GetRetentionPolicyTagsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x000F6A14 File Offset: 0x000F4C14
		protected override GetRetentionPolicyTagsResponse CreateTaskAndExecute()
		{
			this.GetAllUserAssociatedTags();
			this.GetAllOptionalUserAssociatedTags();
			this.RemoveArchiveTagsIfUserDoesNotHaveArchive();
			return this.MergeResultsAndGenerateSuccessResponse();
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x000F6A30 File Offset: 0x000F4C30
		private void GetAllUserAssociatedTags()
		{
			this.allUserAssociatedTags = base.GetRetentionPolicyTags(true, new Microsoft.Exchange.Services.Core.Types.ElcFolderType[]
			{
				Microsoft.Exchange.Services.Core.Types.ElcFolderType.Personal,
				Microsoft.Exchange.Services.Core.Types.ElcFolderType.All
			}, false);
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x000F6A60 File Offset: 0x000F4C60
		private void GetAllOptionalUserAssociatedTags()
		{
			this.allOptionalUserAssociatedTags = base.GetRetentionPolicyTags(true, new Microsoft.Exchange.Services.Core.Types.ElcFolderType[]
			{
				Microsoft.Exchange.Services.Core.Types.ElcFolderType.Personal
			}, true);
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x000F6A96 File Offset: 0x000F4C96
		private void RemoveArchiveTagsIfUserDoesNotHaveArchive()
		{
			if (!base.UserHasArchive)
			{
				this.allUserAssociatedTags = (from tag in this.allUserAssociatedTags
				where tag.RetentionAction != Microsoft.Exchange.Data.Directory.SystemConfiguration.RetentionActionType.MoveToArchive
				select tag).ToList<PresentationRetentionPolicyTag>();
			}
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x000F6BCC File Offset: 0x000F4DCC
		private GetRetentionPolicyTagsResponse MergeResultsAndGenerateSuccessResponse()
		{
			HashSet<Guid> allOptionalUserAssociatedTagIds = new HashSet<Guid>(from tag in this.allOptionalUserAssociatedTags
			select tag.RetentionId);
			return new GetRetentionPolicyTagsResponse
			{
				RetentionPolicyTagDisplayCollection = 
				{
					RetentionPolicyTags = (from tag in this.allUserAssociatedTags
					select new RetentionPolicyTagDisplay
					{
						AgeLimitForRetentionDays = ((tag.AgeLimitForRetention != null) ? new int?(tag.AgeLimitForRetention.Value.Days) : null),
						Description = tag.GetLocalizedFolderComment(new CultureInfo[]
						{
							EWSSettings.ClientCulture
						}),
						DisplayName = tag.GetLocalizedFolderName(new CultureInfo[]
						{
							EWSSettings.ClientCulture
						}),
						Identity = new Identity(tag.Id),
						OptionalTag = allOptionalUserAssociatedTagIds.Contains(tag.RetentionId),
						RetentionAction = (Microsoft.Exchange.Services.Core.Types.RetentionActionType)tag.RetentionAction,
						RetentionEnabled = tag.RetentionEnabled,
						RetentionId = tag.RetentionId,
						Type = (Microsoft.Exchange.Services.Core.Types.ElcFolderType)tag.Type
					}).ToArray<RetentionPolicyTagDisplay>()
				},
				WasSuccessful = true
			};
		}

		// Token: 0x0400288F RID: 10383
		private List<PresentationRetentionPolicyTag> allUserAssociatedTags;

		// Token: 0x04002890 RID: 10384
		private List<PresentationRetentionPolicyTag> allOptionalUserAssociatedTags;
	}
}
