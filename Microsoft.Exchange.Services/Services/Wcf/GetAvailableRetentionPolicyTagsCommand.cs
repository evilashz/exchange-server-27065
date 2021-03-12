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
	// Token: 0x02000999 RID: 2457
	internal sealed class GetAvailableRetentionPolicyTagsCommand : RetentionPolicyTagsCommand<GetRetentionPolicyTagsRequest, GetRetentionPolicyTagsResponse>
	{
		// Token: 0x0600461C RID: 17948 RVA: 0x000F6C47 File Offset: 0x000F4E47
		public GetAvailableRetentionPolicyTagsCommand(CallContext callContext, GetRetentionPolicyTagsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x000F6C51 File Offset: 0x000F4E51
		protected override GetRetentionPolicyTagsResponse CreateTaskAndExecute()
		{
			this.GetAllTags();
			this.GetAllUserAssociatedTags();
			this.SubtractUserTagsFromAllTags();
			this.RemoveArchiveTagsIfUserDoesNotHaveArchive();
			return this.GenerateSuccessResponse();
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x000F6C74 File Offset: 0x000F4E74
		private void GetAllTags()
		{
			this.allTags = base.GetRetentionPolicyTags(false, new Microsoft.Exchange.Services.Core.Types.ElcFolderType[]
			{
				Microsoft.Exchange.Services.Core.Types.ElcFolderType.Personal
			}, false);
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x000F6C9C File Offset: 0x000F4E9C
		private void GetAllUserAssociatedTags()
		{
			this.allUserAssociatedTags = base.GetRetentionPolicyTags(true, new Microsoft.Exchange.Services.Core.Types.ElcFolderType[]
			{
				Microsoft.Exchange.Services.Core.Types.ElcFolderType.Personal
			}, false);
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x000F6CEC File Offset: 0x000F4EEC
		private void SubtractUserTagsFromAllTags()
		{
			HashSet<Guid> allUserAssociatedTagIds = new HashSet<Guid>(from tag in this.allUserAssociatedTags
			select tag.RetentionId);
			this.allTagsAvailableForAdding = (from tag in this.allTags
			where !allUserAssociatedTagIds.Contains(tag.RetentionId)
			select tag).ToList<PresentationRetentionPolicyTag>();
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x000F6D62 File Offset: 0x000F4F62
		private void RemoveArchiveTagsIfUserDoesNotHaveArchive()
		{
			if (!base.UserHasArchive)
			{
				this.allTagsAvailableForAdding = (from tag in this.allTagsAvailableForAdding
				where tag.RetentionAction != Microsoft.Exchange.Data.Directory.SystemConfiguration.RetentionActionType.MoveToArchive
				select tag).ToList<PresentationRetentionPolicyTag>();
			}
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x000F6E78 File Offset: 0x000F5078
		private GetRetentionPolicyTagsResponse GenerateSuccessResponse()
		{
			GetRetentionPolicyTagsResponse getRetentionPolicyTagsResponse = new GetRetentionPolicyTagsResponse();
			getRetentionPolicyTagsResponse.RetentionPolicyTagDisplayCollection.RetentionPolicyTags = (from tag in this.allTagsAvailableForAdding
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
				OptionalTag = true,
				RetentionAction = (Microsoft.Exchange.Services.Core.Types.RetentionActionType)tag.RetentionAction,
				RetentionEnabled = tag.RetentionEnabled,
				RetentionId = tag.RetentionId,
				Type = (Microsoft.Exchange.Services.Core.Types.ElcFolderType)tag.Type
			}).ToArray<RetentionPolicyTagDisplay>();
			getRetentionPolicyTagsResponse.WasSuccessful = true;
			return getRetentionPolicyTagsResponse;
		}

		// Token: 0x04002893 RID: 10387
		private List<PresentationRetentionPolicyTag> allTags;

		// Token: 0x04002894 RID: 10388
		private List<PresentationRetentionPolicyTag> allUserAssociatedTags;

		// Token: 0x04002895 RID: 10389
		private List<PresentationRetentionPolicyTag> allTagsAvailableForAdding;
	}
}
