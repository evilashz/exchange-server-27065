using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000995 RID: 2453
	internal sealed class AddActiveRetentionPolicyTagsCommand : RetentionPolicyTagsCommand<IdentityCollectionRequest, OptionsResponseBase>
	{
		// Token: 0x06004608 RID: 17928 RVA: 0x000F6745 File Offset: 0x000F4945
		public AddActiveRetentionPolicyTagsCommand(CallContext callContext, IdentityCollectionRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x000F674F File Offset: 0x000F494F
		protected override OptionsResponseBase CreateTaskAndExecute()
		{
			this.GetAllUserAssociatedTags();
			this.MergeOldTagsWithNewTags();
			this.SetMergedUserTags();
			return this.GenerateSuccessResponse();
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x000F676C File Offset: 0x000F496C
		private void GetAllUserAssociatedTags()
		{
			this.allUserAssociatedTags = base.GetRetentionPolicyTags(true, new ElcFolderType[]
			{
				ElcFolderType.Personal
			}, false);
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x000F6794 File Offset: 0x000F4994
		private void MergeOldTagsWithNewTags()
		{
			this.mergedUserTags = new Dictionary<string, RetentionPolicyTagIdParameter>(StringComparer.InvariantCultureIgnoreCase);
			foreach (PresentationRetentionPolicyTag presentationRetentionPolicyTag in this.allUserAssociatedTags)
			{
				this.mergedUserTags[presentationRetentionPolicyTag.Guid.ToString()] = new RetentionPolicyTagIdParameter(presentationRetentionPolicyTag.Guid.ToString());
			}
			foreach (Identity identity in this.request.IdentityCollection.Identities)
			{
				this.mergedUserTags[identity.RawIdentity] = new RetentionPolicyTagIdParameter(identity.RawIdentity);
			}
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x000F6870 File Offset: 0x000F4A70
		private void SetMergedUserTags()
		{
			base.SetRetentionPolicyTags(this.mergedUserTags.Values.ToArray<RetentionPolicyTagIdParameter>());
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x000F6888 File Offset: 0x000F4A88
		private OptionsResponseBase GenerateSuccessResponse()
		{
			return new OptionsResponseBase
			{
				WasSuccessful = true
			};
		}

		// Token: 0x0400288D RID: 10381
		private List<PresentationRetentionPolicyTag> allUserAssociatedTags;

		// Token: 0x0400288E RID: 10382
		private Dictionary<string, RetentionPolicyTagIdParameter> mergedUserTags;
	}
}
