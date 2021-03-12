using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009AC RID: 2476
	internal sealed class RemoveActiveRetentionPolicyTagsCommand : RetentionPolicyTagsCommand<IdentityCollectionRequest, OptionsResponseBase>
	{
		// Token: 0x06004676 RID: 18038 RVA: 0x000F9C68 File Offset: 0x000F7E68
		public RemoveActiveRetentionPolicyTagsCommand(CallContext callContext, IdentityCollectionRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x000F9C72 File Offset: 0x000F7E72
		protected override OptionsResponseBase CreateTaskAndExecute()
		{
			this.GetAllUserAssociatedTags();
			this.SubtractInputTagsFromOldTags();
			this.SetUpdateUserTags();
			return this.GenerateSuccessResponse();
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x000F9C8C File Offset: 0x000F7E8C
		private void GetAllUserAssociatedTags()
		{
			this.allUserAssociatedTags = base.GetRetentionPolicyTags(true, new ElcFolderType[]
			{
				ElcFolderType.Personal
			}, false);
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x000F9D1C File Offset: 0x000F7F1C
		private void SubtractInputTagsFromOldTags()
		{
			HashSet<string> tagIdsToBeRemoved = new HashSet<string>(from identity in this.request.IdentityCollection.Identities
			select identity.RawIdentity, StringComparer.InvariantCultureIgnoreCase);
			this.updatedUserTags = (from tag in this.allUserAssociatedTags
			where !tagIdsToBeRemoved.Contains(tag.Guid.ToString())
			select new RetentionPolicyTagIdParameter(tag.Guid.ToString())).ToArray<RetentionPolicyTagIdParameter>();
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x000F9DB5 File Offset: 0x000F7FB5
		private void SetUpdateUserTags()
		{
			base.SetRetentionPolicyTags(this.updatedUserTags);
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x000F9DC4 File Offset: 0x000F7FC4
		private OptionsResponseBase GenerateSuccessResponse()
		{
			return new OptionsResponseBase
			{
				WasSuccessful = true
			};
		}

		// Token: 0x040028A4 RID: 10404
		private List<PresentationRetentionPolicyTag> allUserAssociatedTags;

		// Token: 0x040028A5 RID: 10405
		private RetentionPolicyTagIdParameter[] updatedUserTags;
	}
}
