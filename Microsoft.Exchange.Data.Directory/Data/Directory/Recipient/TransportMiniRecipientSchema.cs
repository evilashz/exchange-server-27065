using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000280 RID: 640
	internal class TransportMiniRecipientSchema : MiniRecipientSchema
	{
		// Token: 0x06001E7F RID: 7807 RVA: 0x00088BA0 File Offset: 0x00086DA0
		static TransportMiniRecipientSchema()
		{
			TransportMiniRecipientSchema.Properties = new ADPropertyDefinition[TransportMiniRecipientSchema.schema.AllProperties.Count];
			TransportMiniRecipientSchema.schema.AllProperties.CopyTo(TransportMiniRecipientSchema.Properties, 0);
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x00089330 File Offset: 0x00087530
		public static TransportMiniRecipientSchema Schema
		{
			get
			{
				return TransportMiniRecipientSchema.schema;
			}
		}

		// Token: 0x040011F2 RID: 4594
		public static readonly ADPropertyDefinition AcceptMessagesOnlyFrom = ADRecipientSchema.AcceptMessagesOnlyFrom;

		// Token: 0x040011F3 RID: 4595
		public static readonly ADPropertyDefinition AcceptMessagesOnlyFromDLMembers = ADRecipientSchema.AcceptMessagesOnlyFromDLMembers;

		// Token: 0x040011F4 RID: 4596
		public static readonly ADPropertyDefinition AntispamBypassEnabled = ADRecipientSchema.AntispamBypassEnabled;

		// Token: 0x040011F5 RID: 4597
		public static readonly ADPropertyDefinition ApprovalApplications = ADUserSchema.ApprovalApplications;

		// Token: 0x040011F6 RID: 4598
		public static readonly ADPropertyDefinition ArbitrationMailbox = ADRecipientSchema.ArbitrationMailbox;

		// Token: 0x040011F7 RID: 4599
		public static readonly ADPropertyDefinition BlockedSendersHash = ADRecipientSchema.BlockedSendersHash;

		// Token: 0x040011F8 RID: 4600
		public static readonly ADPropertyDefinition BypassModerationFrom = ADRecipientSchema.BypassModerationFrom;

		// Token: 0x040011F9 RID: 4601
		public static readonly ADPropertyDefinition BypassModerationFromDLMembers = ADRecipientSchema.BypassModerationFromDLMembers;

		// Token: 0x040011FA RID: 4602
		public static readonly ADPropertyDefinition C = ADOrgPersonSchema.C;

		// Token: 0x040011FB RID: 4603
		public static readonly ADPropertyDefinition City = ADOrgPersonSchema.City;

		// Token: 0x040011FC RID: 4604
		public static readonly ADPropertyDefinition Company = ADOrgPersonSchema.Company;

		// Token: 0x040011FD RID: 4605
		public static readonly ADPropertyDefinition CustomAttribute1 = ADRecipientSchema.CustomAttribute1;

		// Token: 0x040011FE RID: 4606
		public static readonly ADPropertyDefinition CustomAttribute2 = ADRecipientSchema.CustomAttribute2;

		// Token: 0x040011FF RID: 4607
		public static readonly ADPropertyDefinition CustomAttribute3 = ADRecipientSchema.CustomAttribute3;

		// Token: 0x04001200 RID: 4608
		public static readonly ADPropertyDefinition CustomAttribute4 = ADRecipientSchema.CustomAttribute4;

		// Token: 0x04001201 RID: 4609
		public static readonly ADPropertyDefinition CustomAttribute5 = ADRecipientSchema.CustomAttribute5;

		// Token: 0x04001202 RID: 4610
		public static readonly ADPropertyDefinition CustomAttribute6 = ADRecipientSchema.CustomAttribute6;

		// Token: 0x04001203 RID: 4611
		public static readonly ADPropertyDefinition CustomAttribute7 = ADRecipientSchema.CustomAttribute7;

		// Token: 0x04001204 RID: 4612
		public static readonly ADPropertyDefinition CustomAttribute8 = ADRecipientSchema.CustomAttribute8;

		// Token: 0x04001205 RID: 4613
		public static readonly ADPropertyDefinition CustomAttribute9 = ADRecipientSchema.CustomAttribute9;

		// Token: 0x04001206 RID: 4614
		public static readonly ADPropertyDefinition CustomAttribute10 = ADRecipientSchema.CustomAttribute10;

		// Token: 0x04001207 RID: 4615
		public static readonly ADPropertyDefinition CustomAttribute11 = ADRecipientSchema.CustomAttribute11;

		// Token: 0x04001208 RID: 4616
		public static readonly ADPropertyDefinition CustomAttribute12 = ADRecipientSchema.CustomAttribute12;

		// Token: 0x04001209 RID: 4617
		public static readonly ADPropertyDefinition CustomAttribute13 = ADRecipientSchema.CustomAttribute13;

		// Token: 0x0400120A RID: 4618
		public static readonly ADPropertyDefinition CustomAttribute14 = ADRecipientSchema.CustomAttribute14;

		// Token: 0x0400120B RID: 4619
		public static readonly ADPropertyDefinition CustomAttribute15 = ADRecipientSchema.CustomAttribute15;

		// Token: 0x0400120C RID: 4620
		public static readonly ADPropertyDefinition DeliverToMailboxAndForward = IADMailStorageSchema.DeliverToMailboxAndForward;

		// Token: 0x0400120D RID: 4621
		public static readonly ADPropertyDefinition Department = ADOrgPersonSchema.Department;

		// Token: 0x0400120E RID: 4622
		public static readonly ADPropertyDefinition DLSupervisionList = ADRecipientSchema.DLSupervisionList;

		// Token: 0x0400120F RID: 4623
		public static readonly ADPropertyDefinition DowngradeHighPriorityMessagesEnabled = ADUserSchema.DowngradeHighPriorityMessagesEnabled;

		// Token: 0x04001210 RID: 4624
		public static readonly ADPropertyDefinition ElcMailboxFlags = ADUserSchema.ElcMailboxFlags;

		// Token: 0x04001211 RID: 4625
		public static readonly ADPropertyDefinition ElcPolicyTemplate = ADUserSchema.ElcPolicyTemplate;

		// Token: 0x04001212 RID: 4626
		public static readonly ADPropertyDefinition ExtensionCustomAttribute1 = ADRecipientSchema.ExtensionCustomAttribute1;

		// Token: 0x04001213 RID: 4627
		public static readonly ADPropertyDefinition ExtensionCustomAttribute2 = ADRecipientSchema.ExtensionCustomAttribute2;

		// Token: 0x04001214 RID: 4628
		public static readonly ADPropertyDefinition ExtensionCustomAttribute3 = ADRecipientSchema.ExtensionCustomAttribute3;

		// Token: 0x04001215 RID: 4629
		public static readonly ADPropertyDefinition ExtensionCustomAttribute4 = ADRecipientSchema.ExtensionCustomAttribute4;

		// Token: 0x04001216 RID: 4630
		public static readonly ADPropertyDefinition ExtensionCustomAttribute5 = ADRecipientSchema.ExtensionCustomAttribute5;

		// Token: 0x04001217 RID: 4631
		public static readonly ADPropertyDefinition ExternalOofOptions = IADMailStorageSchema.ExternalOofOptions;

		// Token: 0x04001218 RID: 4632
		public static readonly ADPropertyDefinition Fax = ADOrgPersonSchema.Fax;

		// Token: 0x04001219 RID: 4633
		public static readonly ADPropertyDefinition FirstName = ADOrgPersonSchema.FirstName;

		// Token: 0x0400121A RID: 4634
		public static readonly ADPropertyDefinition ForwardingAddress = ADRecipientSchema.ForwardingAddress;

		// Token: 0x0400121B RID: 4635
		public static readonly ADPropertyDefinition ForwardingSmtpAddress = ADRecipientSchema.ForwardingSmtpAddress;

		// Token: 0x0400121C RID: 4636
		public static readonly ADPropertyDefinition HomeMtaServerId = ADGroupSchema.HomeMtaServerId;

		// Token: 0x0400121D RID: 4637
		public static readonly ADPropertyDefinition HomePhone = ADOrgPersonSchema.HomePhone;

		// Token: 0x0400121E RID: 4638
		public static readonly ADPropertyDefinition Initials = ADOrgPersonSchema.Initials;

		// Token: 0x0400121F RID: 4639
		public static readonly ADPropertyDefinition InternalRecipientSupervisionList = ADRecipientSchema.InternalRecipientSupervisionList;

		// Token: 0x04001220 RID: 4640
		public static readonly ADPropertyDefinition InternetEncoding = ADRecipientSchema.InternetEncoding;

		// Token: 0x04001221 RID: 4641
		public static readonly ADPropertyDefinition LanguagesRaw = ADUserSchema.LanguagesRaw;

		// Token: 0x04001222 RID: 4642
		public static readonly ADPropertyDefinition LastName = ADOrgPersonSchema.LastName;

		// Token: 0x04001223 RID: 4643
		public static readonly ADPropertyDefinition ManagedBy = ADGroupSchema.ManagedBy;

		// Token: 0x04001224 RID: 4644
		public static readonly ADPropertyDefinition Manager = ADOrgPersonSchema.Manager;

		// Token: 0x04001225 RID: 4645
		public static readonly ADPropertyDefinition MapiRecipient = ADRecipientSchema.MapiRecipient;

		// Token: 0x04001226 RID: 4646
		public static readonly ADPropertyDefinition MaxReceiveSize = ADRecipientSchema.MaxReceiveSize;

		// Token: 0x04001227 RID: 4647
		public static readonly ADPropertyDefinition MaxSendSize = ADRecipientSchema.MaxSendSize;

		// Token: 0x04001228 RID: 4648
		public static readonly ADPropertyDefinition MobilePhone = ADOrgPersonSchema.MobilePhone;

		// Token: 0x04001229 RID: 4649
		public static readonly ADPropertyDefinition ModeratedBy = ADRecipientSchema.ModeratedBy;

		// Token: 0x0400122A RID: 4650
		public static readonly ADPropertyDefinition ModerationEnabled = ADRecipientSchema.ModerationEnabled;

		// Token: 0x0400122B RID: 4651
		public static readonly ADPropertyDefinition ModerationFlags = ADRecipientSchema.ModerationFlags;

		// Token: 0x0400122C RID: 4652
		public static readonly ADPropertyDefinition Notes = ADRecipientSchema.Notes;

		// Token: 0x0400122D RID: 4653
		public static readonly ADPropertyDefinition Office = ADOrgPersonSchema.Office;

		// Token: 0x0400122E RID: 4654
		public static readonly ADPropertyDefinition OneOffSupervisionList = ADRecipientSchema.OneOffSupervisionList;

		// Token: 0x0400122F RID: 4655
		public static readonly ADPropertyDefinition OpenDomainRoutingDisabled = ADRecipientSchema.OpenDomainRoutingDisabled;

		// Token: 0x04001230 RID: 4656
		public static readonly ADPropertyDefinition OtherFax = ADOrgPersonSchema.OtherFax;

		// Token: 0x04001231 RID: 4657
		public static readonly ADPropertyDefinition OtherHomePhone = ADOrgPersonSchema.OtherHomePhone;

		// Token: 0x04001232 RID: 4658
		public static readonly ADPropertyDefinition OtherTelephone = ADOrgPersonSchema.OtherTelephone;

		// Token: 0x04001233 RID: 4659
		public static readonly ADPropertyDefinition Pager = ADOrgPersonSchema.Pager;

		// Token: 0x04001234 RID: 4660
		public static readonly ADPropertyDefinition Phone = ADOrgPersonSchema.Phone;

		// Token: 0x04001235 RID: 4661
		public static readonly ADPropertyDefinition PostalCode = ADOrgPersonSchema.PostalCode;

		// Token: 0x04001236 RID: 4662
		public static readonly ADPropertyDefinition PostOfficeBox = ADOrgPersonSchema.PostOfficeBox;

		// Token: 0x04001237 RID: 4663
		public static readonly ADPropertyDefinition ProhibitSendQuota = ADMailboxRecipientSchema.ProhibitSendQuota;

		// Token: 0x04001238 RID: 4664
		public static readonly ADPropertyDefinition PublicFolderContentMailbox = ADRecipientSchema.DefaultPublicFolderMailbox;

		// Token: 0x04001239 RID: 4665
		public static readonly ADPropertyDefinition PublicFolderEntryId = ADPublicFolderSchema.EntryId;

		// Token: 0x0400123A RID: 4666
		public static readonly ADPropertyDefinition RecipientDisplayType = ADRecipientSchema.RecipientDisplayType;

		// Token: 0x0400123B RID: 4667
		public static readonly ADPropertyDefinition RecipientLimits = ADRecipientSchema.RecipientLimits;

		// Token: 0x0400123C RID: 4668
		public static readonly ADPropertyDefinition RecipientTypeDetailsValue = ADRecipientSchema.RecipientTypeDetailsValue;

		// Token: 0x0400123D RID: 4669
		public static readonly ADPropertyDefinition RejectMessagesFrom = ADRecipientSchema.RejectMessagesFrom;

		// Token: 0x0400123E RID: 4670
		public static readonly ADPropertyDefinition RejectMessagesFromDLMembers = ADRecipientSchema.RejectMessagesFromDLMembers;

		// Token: 0x0400123F RID: 4671
		public static readonly ADPropertyDefinition RequireAllSendersAreAuthenticated = ADRecipientSchema.RequireAllSendersAreAuthenticated;

		// Token: 0x04001240 RID: 4672
		public static readonly ADPropertyDefinition RulesQuota = IADMailStorageSchema.RulesQuota;

		// Token: 0x04001241 RID: 4673
		public static readonly ADPropertyDefinition SafeRecipientsHash = ADRecipientSchema.SafeRecipientsHash;

		// Token: 0x04001242 RID: 4674
		public static readonly ADPropertyDefinition SafeSendersHash = ADRecipientSchema.SafeSendersHash;

		// Token: 0x04001243 RID: 4675
		public static readonly ADPropertyDefinition SamAccountName = ADMailboxRecipientSchema.SamAccountName;

		// Token: 0x04001244 RID: 4676
		public static readonly ADPropertyDefinition SCLDeleteEnabled = ADRecipientSchema.SCLDeleteEnabled;

		// Token: 0x04001245 RID: 4677
		public static readonly ADPropertyDefinition SCLDeleteThreshold = ADRecipientSchema.SCLDeleteThreshold;

		// Token: 0x04001246 RID: 4678
		public static readonly ADPropertyDefinition SCLJunkEnabled = ADRecipientSchema.SCLJunkEnabled;

		// Token: 0x04001247 RID: 4679
		public static readonly ADPropertyDefinition SCLJunkThreshold = ADRecipientSchema.SCLJunkThreshold;

		// Token: 0x04001248 RID: 4680
		public static readonly ADPropertyDefinition SCLQuarantineEnabled = ADRecipientSchema.SCLQuarantineEnabled;

		// Token: 0x04001249 RID: 4681
		public static readonly ADPropertyDefinition SCLQuarantineThreshold = ADRecipientSchema.SCLQuarantineThreshold;

		// Token: 0x0400124A RID: 4682
		public static readonly ADPropertyDefinition SCLRejectEnabled = ADRecipientSchema.SCLRejectEnabled;

		// Token: 0x0400124B RID: 4683
		public static readonly ADPropertyDefinition SCLRejectThreshold = ADRecipientSchema.SCLRejectThreshold;

		// Token: 0x0400124C RID: 4684
		public static readonly ADPropertyDefinition SendDeliveryReportsTo = IADDistributionListSchema.SendDeliveryReportsTo;

		// Token: 0x0400124D RID: 4685
		public static readonly ADPropertyDefinition SendOofMessageToOriginatorEnabled = ADGroupSchema.SendOofMessageToOriginatorEnabled;

		// Token: 0x0400124E RID: 4686
		public static readonly ADPropertyDefinition ServerName = IADMailStorageSchema.ServerName;

		// Token: 0x0400124F RID: 4687
		public static readonly ADPropertyDefinition SimpleDisplayName = ADRecipientSchema.SimpleDisplayName;

		// Token: 0x04001250 RID: 4688
		public static readonly ADPropertyDefinition StateOrProvince = ADOrgPersonSchema.StateOrProvince;

		// Token: 0x04001251 RID: 4689
		public static readonly ADPropertyDefinition StreetAddress = ADOrgPersonSchema.StreetAddress;

		// Token: 0x04001252 RID: 4690
		public static readonly ADPropertyDefinition Title = ADOrgPersonSchema.Title;

		// Token: 0x04001253 RID: 4691
		public static readonly ADPropertyDefinition WindowsEmailAddress = ADRecipientSchema.WindowsEmailAddress;

		// Token: 0x04001254 RID: 4692
		internal static readonly ADPropertyDefinition[] Properties;

		// Token: 0x04001255 RID: 4693
		internal static readonly ADPropertyDefinition[] TransportProperties = new ADPropertyDefinition[]
		{
			TransportMiniRecipientSchema.AcceptMessagesOnlyFrom,
			TransportMiniRecipientSchema.AcceptMessagesOnlyFromDLMembers,
			TransportMiniRecipientSchema.AntispamBypassEnabled,
			TransportMiniRecipientSchema.ApprovalApplications,
			TransportMiniRecipientSchema.ArbitrationMailbox,
			TransportMiniRecipientSchema.BlockedSendersHash,
			TransportMiniRecipientSchema.BypassModerationFrom,
			TransportMiniRecipientSchema.BypassModerationFromDLMembers,
			TransportMiniRecipientSchema.C,
			TransportMiniRecipientSchema.City,
			TransportMiniRecipientSchema.Company,
			TransportMiniRecipientSchema.CustomAttribute1,
			TransportMiniRecipientSchema.CustomAttribute2,
			TransportMiniRecipientSchema.CustomAttribute3,
			TransportMiniRecipientSchema.CustomAttribute4,
			TransportMiniRecipientSchema.CustomAttribute5,
			TransportMiniRecipientSchema.CustomAttribute6,
			TransportMiniRecipientSchema.CustomAttribute7,
			TransportMiniRecipientSchema.CustomAttribute8,
			TransportMiniRecipientSchema.CustomAttribute9,
			TransportMiniRecipientSchema.CustomAttribute10,
			TransportMiniRecipientSchema.CustomAttribute11,
			TransportMiniRecipientSchema.CustomAttribute12,
			TransportMiniRecipientSchema.CustomAttribute13,
			TransportMiniRecipientSchema.CustomAttribute14,
			TransportMiniRecipientSchema.CustomAttribute15,
			TransportMiniRecipientSchema.DeliverToMailboxAndForward,
			TransportMiniRecipientSchema.Department,
			TransportMiniRecipientSchema.DLSupervisionList,
			TransportMiniRecipientSchema.DowngradeHighPriorityMessagesEnabled,
			TransportMiniRecipientSchema.ElcMailboxFlags,
			TransportMiniRecipientSchema.ElcPolicyTemplate,
			TransportMiniRecipientSchema.ExtensionCustomAttribute1,
			TransportMiniRecipientSchema.ExtensionCustomAttribute2,
			TransportMiniRecipientSchema.ExtensionCustomAttribute3,
			TransportMiniRecipientSchema.ExtensionCustomAttribute4,
			TransportMiniRecipientSchema.ExtensionCustomAttribute5,
			TransportMiniRecipientSchema.ExternalOofOptions,
			TransportMiniRecipientSchema.Fax,
			TransportMiniRecipientSchema.FirstName,
			TransportMiniRecipientSchema.ForwardingAddress,
			TransportMiniRecipientSchema.ForwardingSmtpAddress,
			TransportMiniRecipientSchema.HomeMtaServerId,
			TransportMiniRecipientSchema.HomePhone,
			TransportMiniRecipientSchema.Initials,
			TransportMiniRecipientSchema.InternalRecipientSupervisionList,
			TransportMiniRecipientSchema.InternetEncoding,
			TransportMiniRecipientSchema.LanguagesRaw,
			TransportMiniRecipientSchema.LastName,
			TransportMiniRecipientSchema.ManagedBy,
			TransportMiniRecipientSchema.Manager,
			TransportMiniRecipientSchema.MapiRecipient,
			TransportMiniRecipientSchema.MaxReceiveSize,
			TransportMiniRecipientSchema.MaxSendSize,
			TransportMiniRecipientSchema.MobilePhone,
			TransportMiniRecipientSchema.ModeratedBy,
			TransportMiniRecipientSchema.ModerationEnabled,
			TransportMiniRecipientSchema.ModerationFlags,
			TransportMiniRecipientSchema.Notes,
			TransportMiniRecipientSchema.Office,
			TransportMiniRecipientSchema.OneOffSupervisionList,
			TransportMiniRecipientSchema.OpenDomainRoutingDisabled,
			TransportMiniRecipientSchema.OtherFax,
			TransportMiniRecipientSchema.OtherHomePhone,
			TransportMiniRecipientSchema.OtherTelephone,
			TransportMiniRecipientSchema.Pager,
			TransportMiniRecipientSchema.Phone,
			TransportMiniRecipientSchema.PostalCode,
			TransportMiniRecipientSchema.PostOfficeBox,
			TransportMiniRecipientSchema.ProhibitSendQuota,
			TransportMiniRecipientSchema.PublicFolderContentMailbox,
			TransportMiniRecipientSchema.PublicFolderEntryId,
			TransportMiniRecipientSchema.RecipientDisplayType,
			TransportMiniRecipientSchema.RecipientLimits,
			TransportMiniRecipientSchema.RecipientTypeDetailsValue,
			TransportMiniRecipientSchema.RejectMessagesFrom,
			TransportMiniRecipientSchema.RejectMessagesFromDLMembers,
			TransportMiniRecipientSchema.RequireAllSendersAreAuthenticated,
			TransportMiniRecipientSchema.RulesQuota,
			TransportMiniRecipientSchema.SafeRecipientsHash,
			TransportMiniRecipientSchema.SafeSendersHash,
			TransportMiniRecipientSchema.SCLDeleteEnabled,
			TransportMiniRecipientSchema.SCLDeleteThreshold,
			TransportMiniRecipientSchema.SCLJunkEnabled,
			TransportMiniRecipientSchema.SCLJunkThreshold,
			TransportMiniRecipientSchema.SCLQuarantineEnabled,
			TransportMiniRecipientSchema.SCLQuarantineThreshold,
			TransportMiniRecipientSchema.SCLRejectEnabled,
			TransportMiniRecipientSchema.SCLRejectThreshold,
			TransportMiniRecipientSchema.SendDeliveryReportsTo,
			TransportMiniRecipientSchema.SendOofMessageToOriginatorEnabled,
			TransportMiniRecipientSchema.ServerName,
			TransportMiniRecipientSchema.SimpleDisplayName,
			TransportMiniRecipientSchema.StateOrProvince,
			TransportMiniRecipientSchema.StreetAddress,
			TransportMiniRecipientSchema.Title,
			TransportMiniRecipientSchema.WindowsEmailAddress,
			MiniRecipientSchema.WindowsLiveID
		};

		// Token: 0x04001256 RID: 4694
		private static TransportMiniRecipientSchema schema = ObjectSchema.GetInstance<TransportMiniRecipientSchema>();
	}
}
