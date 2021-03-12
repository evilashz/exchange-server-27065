using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200082A RID: 2090
	internal class SyncGroupSchema : SyncRecipientSchema
	{
		// Token: 0x170024B7 RID: 9399
		// (get) Token: 0x060067C1 RID: 26561 RVA: 0x0016DEAE File Offset: 0x0016C0AE
		public override DirectoryObjectClass DirectoryObjectClass
		{
			get
			{
				return DirectoryObjectClass.Group;
			}
		}

		// Token: 0x0400444D RID: 17485
		public static SyncPropertyDefinition GroupType = new SyncPropertyDefinition(ADGroupSchema.GroupType, null, typeof(object), SyncPropertyDefinitionFlags.Ignore, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400444E RID: 17486
		public static SyncPropertyDefinition CoManagedBy = new SyncPropertyDefinition(ADGroupSchema.CoManagedBy, "MSExchCoManagedByLink", typeof(SyncLink), typeof(MSExchCoManagedByLink), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400444F RID: 17487
		public static SyncPropertyDefinition IsHierarchicalGroup = new SyncPropertyDefinition(ADGroupSchema.IsOrganizationalGroup, "MSOrgIsOrganizational", typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004450 RID: 17488
		public static SyncPropertyDefinition MailEnabled = new SyncPropertyDefinition("MailEnabled", "MailEnabled", typeof(bool), typeof(bool), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.AlwaysReturned, SyncPropertyDefinition.InitialSyncPropertySetVersion, false);

		// Token: 0x04004451 RID: 17489
		public static SyncPropertyDefinition ManagedBy = new SyncPropertyDefinition(ADGroupSchema.RawManagedBy, "ManagedBy", typeof(SyncLink), typeof(ManagedBy), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004452 RID: 17490
		public static SyncPropertyDefinition Members = new SyncPropertyDefinition(ADGroupSchema.Members, "Member", typeof(SyncLink), typeof(Member), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004453 RID: 17491
		public static SyncPropertyDefinition ReportToManagerEnabled = new SyncPropertyDefinition(ADGroupSchema.ReportToManagerEnabled, "ReportToOwner", typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004454 RID: 17492
		public static SyncPropertyDefinition ReportToOriginatorEnabled = new SyncPropertyDefinition(ADGroupSchema.ReportToOriginatorEnabled, "ReportToOriginator", typeof(bool), typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004455 RID: 17493
		public static SyncPropertyDefinition SecurityEnabled = new SyncPropertyDefinition("SecurityEnabled", "SecurityEnabled", typeof(bool), typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Calculated | SyncPropertyDefinitionFlags.PersistDefaultValue, SyncPropertyDefinition.InitialSyncPropertySetVersion, false, new SyncPropertyDefinition[]
		{
			SyncGroupSchema.GroupType
		}, ADObject.FlagGetterDelegate(SyncGroupSchema.GroupType, int.MinValue), new SetterDelegate(SyncGroup.SecurityEnabledSetter));

		// Token: 0x04004456 RID: 17494
		public static SyncPropertyDefinition SendOofMessageToOriginatorEnabled = new SyncPropertyDefinition(ADGroupSchema.SendOofMessageToOriginatorEnabled, "OofReplyToOriginator", typeof(bool), typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004457 RID: 17495
		public static SyncPropertyDefinition WellKnownObject = new SyncPropertyDefinition("WellKnownObject", "WellKnownObject", typeof(string), typeof(DirectoryPropertyStringSingleLength1To40), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.AlwaysReturned, SyncPropertyDefinition.InitialSyncPropertySetVersion, string.Empty);

		// Token: 0x04004458 RID: 17496
		public new static SyncPropertyDefinition RecipientTypeDetailsValue = new SyncPropertyDefinition(ADRecipientSchema.RecipientTypeDetailsValue, "MSExchRecipientTypeDetails", typeof(DirectoryPropertyInt64Single), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.AlwaysReturned, SyncPropertyDefinition.SyncPropertySetVersion14);

		// Token: 0x04004459 RID: 17497
		public static SyncPropertyDefinition Creator = new SyncPropertyDefinition("Creator", "CreatedOnBehalfOf", typeof(string), typeof(DirectoryPropertyReferenceUserAndServicePrincipalSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.SyncPropertySetVersion15, string.Empty);

		// Token: 0x0400445A RID: 17498
		public static SyncPropertyDefinition SharePointResources = new SyncPropertyDefinition("SharePointResources", "SharepointResources", typeof(string), typeof(DirectoryPropertyStringLength1To1024), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion15, null);

		// Token: 0x0400445B RID: 17499
		public static SyncPropertyDefinition Owners = new SyncPropertyDefinition(ADUserSchema.Owners, "Owner", typeof(SyncLink), typeof(Owner), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion15);

		// Token: 0x0400445C RID: 17500
		public static SyncPropertyDefinition IsPublic = new SyncPropertyDefinition("IsPublic", "IsPublic", typeof(bool), typeof(bool), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.SyncPropertySetVersion15, false);

		// Token: 0x0400445D RID: 17501
		public static SyncPropertyDefinition Description = new SyncPropertyDefinition(ADRecipientSchema.Description, "Description", typeof(string), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.SyncPropertySetVersion17);
	}
}
