using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006FF RID: 1791
	[Serializable]
	public class ExchangeRoleAssignmentPresentation : ADPresentationObject
	{
		// Token: 0x0600544E RID: 21582 RVA: 0x00131AE0 File Offset: 0x0012FCE0
		public ExchangeRoleAssignmentPresentation()
		{
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x00131AE8 File Offset: 0x0012FCE8
		public ExchangeRoleAssignmentPresentation(ExchangeRoleAssignment dataObject, ADObjectId userId, AssignmentMethod assignmentMethod, string userName, ADObjectId assigneeId, OrganizationId sharedOrgId) : base(dataObject)
		{
			if (assigneeId != null && null == sharedOrgId)
			{
				throw new ArgumentException("AssigneeID isnt null and sharedOrgId is null. sharedOrgId cannot be null if assigneeId isn't null.");
			}
			this.User = userId;
			this.assignmentMethod = assignmentMethod;
			if (!string.IsNullOrEmpty(userName))
			{
				this.EffectiveUserName = userName;
			}
			if (assigneeId != null)
			{
				this.roleAssignee = assigneeId;
			}
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x00131B40 File Offset: 0x0012FD40
		public ExchangeRoleAssignmentPresentation(ExchangeRoleAssignment dataObject, ADObjectId userId, AssignmentMethod assignmentMethod) : this(dataObject, userId, assignmentMethod, null, null, null)
		{
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x00131B4E File Offset: 0x0012FD4E
		public ExchangeRoleAssignmentPresentation(ExchangeRoleAssignment dataObject, ADObjectId userId, AssignmentMethod assignmentMethod, string userName) : this(dataObject, userId, assignmentMethod, userName, null, null)
		{
		}

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x06005452 RID: 21586 RVA: 0x00131B5D File Offset: 0x0012FD5D
		public new ADObject DataObject
		{
			get
			{
				return base.DataObject;
			}
		}

		// Token: 0x17001C02 RID: 7170
		// (get) Token: 0x06005453 RID: 21587 RVA: 0x00131B68 File Offset: 0x0012FD68
		// (set) Token: 0x06005454 RID: 21588 RVA: 0x00131B95 File Offset: 0x0012FD95
		public ADObjectId User
		{
			get
			{
				ADObjectId adobjectId = this.user;
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					adobjectId = (ADObjectId)SuppressingPiiProperty.TryRedact(ADObjectSchema.Id, adobjectId);
				}
				return adobjectId;
			}
			private set
			{
				this.user = value;
			}
		}

		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x06005455 RID: 21589 RVA: 0x00131B9E File Offset: 0x0012FD9E
		// (set) Token: 0x06005456 RID: 21590 RVA: 0x00131BA6 File Offset: 0x0012FDA6
		public AssignmentMethod AssignmentMethod
		{
			get
			{
				return this.assignmentMethod;
			}
			internal set
			{
				this.assignmentMethod = value;
			}
		}

		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x06005457 RID: 21591 RVA: 0x00131BAF File Offset: 0x0012FDAF
		public override ObjectId Identity
		{
			get
			{
				if (this.isCompositeIdentity)
				{
					return this.compositeIdentity;
				}
				return base.Identity;
			}
		}

		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x06005458 RID: 21592 RVA: 0x00131BC8 File Offset: 0x0012FDC8
		// (set) Token: 0x06005459 RID: 21593 RVA: 0x00131BF5 File Offset: 0x0012FDF5
		public string EffectiveUserName
		{
			get
			{
				string text = this.effectiveUserName;
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					text = (string)SuppressingPiiProperty.TryRedact(ExchangeRoleAssignmentPresentationSchema.RoleAssigneeName, text);
				}
				return text;
			}
			internal set
			{
				this.effectiveUserName = value;
			}
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x0600545A RID: 21594 RVA: 0x00131BFE File Offset: 0x0012FDFE
		// (set) Token: 0x0600545B RID: 21595 RVA: 0x00131C06 File Offset: 0x0012FE06
		public MultiValuedProperty<FormattedADObjectIdCollection> AssignmentChain
		{
			get
			{
				return this.assignmentChain;
			}
			internal set
			{
				this.assignmentChain = value;
			}
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x0600545C RID: 21596 RVA: 0x00131C0F File Offset: 0x0012FE0F
		// (set) Token: 0x0600545D RID: 21597 RVA: 0x00131C21 File Offset: 0x0012FE21
		public RoleAssigneeType RoleAssigneeType
		{
			get
			{
				return (RoleAssigneeType)this[ExchangeRoleAssignmentPresentationSchema.RoleAssigneeType];
			}
			internal set
			{
				this[ExchangeRoleAssignmentPresentationSchema.RoleAssigneeType] = value;
			}
		}

		// Token: 0x17001C08 RID: 7176
		// (get) Token: 0x0600545E RID: 21598 RVA: 0x00131C34 File Offset: 0x0012FE34
		// (set) Token: 0x0600545F RID: 21599 RVA: 0x00131C55 File Offset: 0x0012FE55
		public ADObjectId RoleAssignee
		{
			get
			{
				if (this.roleAssignee == null)
				{
					return (ADObjectId)this[ExchangeRoleAssignmentPresentationSchema.RoleAssignee];
				}
				return this.roleAssignee;
			}
			protected set
			{
				this[ExchangeRoleAssignmentPresentationSchema.RoleAssignee] = value;
			}
		}

		// Token: 0x17001C09 RID: 7177
		// (get) Token: 0x06005460 RID: 21600 RVA: 0x00131C63 File Offset: 0x0012FE63
		// (set) Token: 0x06005461 RID: 21601 RVA: 0x00131C75 File Offset: 0x0012FE75
		public ADObjectId Role
		{
			get
			{
				return (ADObjectId)this[ExchangeRoleAssignmentPresentationSchema.Role];
			}
			internal set
			{
				this[ExchangeRoleAssignmentPresentationSchema.Role] = value;
			}
		}

		// Token: 0x17001C0A RID: 7178
		// (get) Token: 0x06005462 RID: 21602 RVA: 0x00131C83 File Offset: 0x0012FE83
		// (set) Token: 0x06005463 RID: 21603 RVA: 0x00131C95 File Offset: 0x0012FE95
		public RoleAssignmentDelegationType RoleAssignmentDelegationType
		{
			get
			{
				return (RoleAssignmentDelegationType)this[ExchangeRoleAssignmentPresentationSchema.RoleAssignmentDelegationType];
			}
			internal set
			{
				this[ExchangeRoleAssignmentPresentationSchema.RoleAssignmentDelegationType] = value;
			}
		}

		// Token: 0x17001C0B RID: 7179
		// (get) Token: 0x06005464 RID: 21604 RVA: 0x00131CA8 File Offset: 0x0012FEA8
		// (set) Token: 0x06005465 RID: 21605 RVA: 0x00131CBA File Offset: 0x0012FEBA
		public ADObjectId CustomRecipientWriteScope
		{
			get
			{
				return (ADObjectId)this[ExchangeRoleAssignmentPresentationSchema.CustomRecipientWriteScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentPresentationSchema.CustomRecipientWriteScope] = value;
			}
		}

		// Token: 0x17001C0C RID: 7180
		// (get) Token: 0x06005466 RID: 21606 RVA: 0x00131CC8 File Offset: 0x0012FEC8
		// (set) Token: 0x06005467 RID: 21607 RVA: 0x00131CDA File Offset: 0x0012FEDA
		public ADObjectId CustomConfigWriteScope
		{
			get
			{
				return (ADObjectId)this[ExchangeRoleAssignmentPresentationSchema.CustomConfigWriteScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentPresentationSchema.CustomConfigWriteScope] = value;
			}
		}

		// Token: 0x17001C0D RID: 7181
		// (get) Token: 0x06005468 RID: 21608 RVA: 0x00131CE8 File Offset: 0x0012FEE8
		// (set) Token: 0x06005469 RID: 21609 RVA: 0x00131CFA File Offset: 0x0012FEFA
		public ScopeType RecipientReadScope
		{
			get
			{
				return (ScopeType)this[ExchangeRoleAssignmentPresentationSchema.RecipientReadScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentPresentationSchema.RecipientReadScope] = value;
			}
		}

		// Token: 0x17001C0E RID: 7182
		// (get) Token: 0x0600546A RID: 21610 RVA: 0x00131D0D File Offset: 0x0012FF0D
		// (set) Token: 0x0600546B RID: 21611 RVA: 0x00131D1F File Offset: 0x0012FF1F
		public ScopeType ConfigReadScope
		{
			get
			{
				return (ScopeType)this[ExchangeRoleAssignmentPresentationSchema.ConfigReadScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentPresentationSchema.ConfigReadScope] = value;
			}
		}

		// Token: 0x17001C0F RID: 7183
		// (get) Token: 0x0600546C RID: 21612 RVA: 0x00131D32 File Offset: 0x0012FF32
		// (set) Token: 0x0600546D RID: 21613 RVA: 0x00131D44 File Offset: 0x0012FF44
		public RecipientWriteScopeType RecipientWriteScope
		{
			get
			{
				return (RecipientWriteScopeType)this[ExchangeRoleAssignmentPresentationSchema.RecipientWriteScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentPresentationSchema.RecipientWriteScope] = value;
			}
		}

		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x0600546E RID: 21614 RVA: 0x00131D57 File Offset: 0x0012FF57
		// (set) Token: 0x0600546F RID: 21615 RVA: 0x00131D69 File Offset: 0x0012FF69
		public ConfigWriteScopeType ConfigWriteScope
		{
			get
			{
				return (ConfigWriteScopeType)this[ExchangeRoleAssignmentPresentationSchema.ConfigWriteScope];
			}
			internal set
			{
				this[ExchangeRoleAssignmentPresentationSchema.ConfigWriteScope] = value;
			}
		}

		// Token: 0x17001C11 RID: 7185
		// (get) Token: 0x06005470 RID: 21616 RVA: 0x00131D7C File Offset: 0x0012FF7C
		// (set) Token: 0x06005471 RID: 21617 RVA: 0x00131D8E File Offset: 0x0012FF8E
		public bool Enabled
		{
			get
			{
				return (bool)this[ExchangeRoleAssignmentPresentationSchema.Enabled];
			}
			internal set
			{
				this[ExchangeRoleAssignmentPresentationSchema.Enabled] = value;
			}
		}

		// Token: 0x17001C12 RID: 7186
		// (get) Token: 0x06005472 RID: 21618 RVA: 0x00131DA1 File Offset: 0x0012FFA1
		public string RoleAssigneeName
		{
			get
			{
				return (string)this[ExchangeRoleAssignmentPresentationSchema.RoleAssigneeName];
			}
		}

		// Token: 0x17001C13 RID: 7187
		// (get) Token: 0x06005473 RID: 21619 RVA: 0x00131DB3 File Offset: 0x0012FFB3
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return ExchangeRoleAssignmentPresentation.schema;
			}
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x00131DBC File Offset: 0x0012FFBC
		internal void UpdatePresentationObjectWithEffectiveUser(ADObjectId effectiveUser, MultiValuedProperty<FormattedADObjectIdCollection> assignmentChains, bool isCompositeidentity, AssignmentMethod assignmentMethod)
		{
			this.EffectiveUserName = effectiveUser.Name;
			this.AssignmentChain = assignmentChains;
			this.isCompositeIdentity = isCompositeidentity;
			this.AssignmentMethod = assignmentMethod;
			this.User = effectiveUser;
			if (this.isCompositeIdentity)
			{
				this.compositeIdentity = new EffectiveUserObjectId(base.OriginalId, this.User);
			}
		}

		// Token: 0x0400389D RID: 14493
		internal const char ElementSeparatorChar = '\\';

		// Token: 0x0400389E RID: 14494
		private static ExchangeRoleAssignmentPresentationSchema schema = ObjectSchema.GetInstance<ExchangeRoleAssignmentPresentationSchema>();

		// Token: 0x0400389F RID: 14495
		private AssignmentMethod assignmentMethod;

		// Token: 0x040038A0 RID: 14496
		private ADObjectId user;

		// Token: 0x040038A1 RID: 14497
		private string effectiveUserName;

		// Token: 0x040038A2 RID: 14498
		private bool isCompositeIdentity;

		// Token: 0x040038A3 RID: 14499
		private ObjectId compositeIdentity;

		// Token: 0x040038A4 RID: 14500
		private MultiValuedProperty<FormattedADObjectIdCollection> assignmentChain;

		// Token: 0x040038A5 RID: 14501
		private ADObjectId roleAssignee;
	}
}
