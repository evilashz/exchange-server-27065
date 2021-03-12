using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C56 RID: 3158
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DistributionListSchema : ContactBaseSchema
	{
		// Token: 0x17001E0F RID: 7695
		// (get) Token: 0x06006F67 RID: 28519 RVA: 0x001DFBDF File Offset: 0x001DDDDF
		public new static DistributionListSchema Instance
		{
			get
			{
				if (DistributionListSchema.instance == null)
				{
					DistributionListSchema.instance = new DistributionListSchema();
				}
				return DistributionListSchema.instance;
			}
		}

		// Token: 0x17001E10 RID: 7696
		// (get) Token: 0x06006F68 RID: 28520 RVA: 0x001DFBF7 File Offset: 0x001DDDF7
		protected override ICollection<PropertyRule> PropertyRules
		{
			get
			{
				return DistributionListSchema.DistributionListSchemaPropertyRules;
			}
		}

		// Token: 0x04004380 RID: 17280
		[Autoload]
		internal static readonly StorePropertyDefinition DLStream = InternalSchema.DLStream;

		// Token: 0x04004381 RID: 17281
		[Autoload]
		internal static readonly StorePropertyDefinition DLChecksum = InternalSchema.DLChecksum;

		// Token: 0x04004382 RID: 17282
		[Autoload]
		public static readonly StorePropertyDefinition DLName = InternalSchema.DLName;

		// Token: 0x04004383 RID: 17283
		[Autoload]
		public static readonly StorePropertyDefinition DLAlias = InternalSchema.DLAlias;

		// Token: 0x04004384 RID: 17284
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition Members = InternalSchema.Members;

		// Token: 0x04004385 RID: 17285
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition OneOffMembers = InternalSchema.OneOffMembers;

		// Token: 0x04004386 RID: 17286
		[Autoload]
		public static readonly StorePropertyDefinition Email1EmailAddress = InternalSchema.Email1EmailAddress;

		// Token: 0x04004387 RID: 17287
		[Autoload]
		public static readonly StorePropertyDefinition AsParticipant = InternalSchema.DistributionListParticipant;

		// Token: 0x04004388 RID: 17288
		[Autoload]
		internal static readonly StorePropertyDefinition MapiSubject = InternalSchema.MapiSubject;

		// Token: 0x04004389 RID: 17289
		private static readonly List<PropertyRule> DistributionListSchemaPropertyRules = new List<PropertyRule>
		{
			PropertyRuleLibrary.PersonIdRule,
			PropertyRuleLibrary.PDLDisplayNameRule,
			PropertyRuleLibrary.PDLMembershipRule
		};

		// Token: 0x0400438A RID: 17290
		private static DistributionListSchema instance = null;
	}
}
