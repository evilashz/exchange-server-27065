using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class UpdateGroupMailboxBase
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x00015BA0 File Offset: 0x00013DA0
		protected UpdateGroupMailboxBase(ADUser group, ADUser executingUser, GroupMailboxConfigurationActionType forceActionMask, int? permissionsVersion)
		{
			ArgumentValidator.ThrowIfNull("group", group);
			ArgumentValidator.ThrowIfInvalidValue<ADUser>("group", group, (ADUser adUser) => adUser.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox);
			this.group = group;
			this.executingUser = executingUser;
			this.forceActionMask = forceActionMask;
			this.permissionsVersion = permissionsVersion;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00015C03 File Offset: 0x00013E03
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00015C0B File Offset: 0x00013E0B
		public string Error { get; protected set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00015C14 File Offset: 0x00013E14
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00015C1C File Offset: 0x00013E1C
		public ResponseCodeType? ResponseCode { get; protected set; }

		// Token: 0x060004E8 RID: 1256
		public abstract void Execute();

		// Token: 0x04000137 RID: 311
		protected readonly ADUser group;

		// Token: 0x04000138 RID: 312
		protected readonly ADUser executingUser;

		// Token: 0x04000139 RID: 313
		protected readonly GroupMailboxConfigurationActionType forceActionMask;

		// Token: 0x0400013A RID: 314
		protected readonly int? permissionsVersion;
	}
}
