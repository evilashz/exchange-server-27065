using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000701 RID: 1793
	internal class ExchangeRoleAssignmentPresentationSchema : ADPresentationSchema
	{
		// Token: 0x06005479 RID: 21625 RVA: 0x00131E86 File Offset: 0x00130086
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ExchangeRoleAssignmentSchema>();
		}

		// Token: 0x040038A8 RID: 14504
		public static readonly ADPropertyDefinition Role = ExchangeRoleAssignmentSchema.Role;

		// Token: 0x040038A9 RID: 14505
		public static readonly ADPropertyDefinition RoleAssignee = ExchangeRoleAssignmentSchema.User;

		// Token: 0x040038AA RID: 14506
		public static readonly ADPropertyDefinition CustomRecipientWriteScope = ExchangeRoleAssignmentSchema.CustomRecipientWriteScope;

		// Token: 0x040038AB RID: 14507
		public static readonly ADPropertyDefinition CustomConfigWriteScope = ExchangeRoleAssignmentSchema.CustomConfigWriteScope;

		// Token: 0x040038AC RID: 14508
		public static readonly ADPropertyDefinition ExchangeRoleAssignmentFlags = ExchangeRoleAssignmentSchema.ExchangeRoleAssignmentFlags;

		// Token: 0x040038AD RID: 14509
		public static readonly ADPropertyDefinition RecipientReadScope = ExchangeRoleAssignmentSchema.RecipientReadScope;

		// Token: 0x040038AE RID: 14510
		public static readonly ADPropertyDefinition ConfigReadScope = ExchangeRoleAssignmentSchema.ConfigReadScope;

		// Token: 0x040038AF RID: 14511
		public static readonly ADPropertyDefinition RecipientWriteScope = ExchangeRoleAssignmentSchema.RecipientWriteScope;

		// Token: 0x040038B0 RID: 14512
		public static readonly ADPropertyDefinition ConfigWriteScope = ExchangeRoleAssignmentSchema.ConfigWriteScope;

		// Token: 0x040038B1 RID: 14513
		public static readonly ADPropertyDefinition RoleAssignmentDelegationType = ExchangeRoleAssignmentSchema.RoleAssignmentDelegationType;

		// Token: 0x040038B2 RID: 14514
		public static readonly ADPropertyDefinition Enabled = ExchangeRoleAssignmentSchema.Enabled;

		// Token: 0x040038B3 RID: 14515
		public static readonly ADPropertyDefinition RoleAssigneeType = ExchangeRoleAssignmentSchema.RoleAssigneeType;

		// Token: 0x040038B4 RID: 14516
		public static readonly ADPropertyDefinition RoleAssigneeName = ExchangeRoleAssignmentSchema.RoleAssigneeName;
	}
}
