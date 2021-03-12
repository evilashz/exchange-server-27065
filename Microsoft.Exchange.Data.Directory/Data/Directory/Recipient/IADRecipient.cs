using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001E2 RID: 482
	public interface IADRecipient : IADObject, IADRawEntry, IConfigurable, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600141E RID: 5150
		// (set) Token: 0x0600141F RID: 5151
		ADObjectId AddressBookPolicy { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001420 RID: 5152
		// (set) Token: 0x06001421 RID: 5153
		string Alias { get; set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001422 RID: 5154
		// (set) Token: 0x06001423 RID: 5155
		string AssistantName { get; set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001424 RID: 5156
		// (set) Token: 0x06001425 RID: 5157
		MailboxAuditOperations AuditAdminOperations { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001426 RID: 5158
		// (set) Token: 0x06001427 RID: 5159
		MailboxAuditOperations AuditDelegateAdminOperations { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001428 RID: 5160
		// (set) Token: 0x06001429 RID: 5161
		MailboxAuditOperations AuditDelegateOperations { get; set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600142A RID: 5162
		DateTime? AuditLastAdminAccess { get; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600142B RID: 5163
		DateTime? AuditLastDelegateAccess { get; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600142C RID: 5164
		DateTime? AuditLastExternalAccess { get; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600142D RID: 5165
		// (set) Token: 0x0600142E RID: 5166
		MailboxAuditOperations AuditOwnerOperations { get; set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600142F RID: 5167
		// (set) Token: 0x06001430 RID: 5168
		bool BypassAudit { get; set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001431 RID: 5169
		// (set) Token: 0x06001432 RID: 5170
		string DisplayName { get; set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001433 RID: 5171
		ADObjectId DefaultPublicFolderMailbox { get; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001434 RID: 5172
		// (set) Token: 0x06001435 RID: 5173
		ProxyAddressCollection EmailAddresses { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001436 RID: 5174
		string ExternalDirectoryObjectId { get; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001437 RID: 5175
		// (set) Token: 0x06001438 RID: 5176
		ProxyAddress ExternalEmailAddress { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001439 RID: 5177
		ADObjectId GlobalAddressListFromAddressBookPolicy { get; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600143A RID: 5178
		// (set) Token: 0x0600143B RID: 5179
		MultiValuedProperty<ADObjectId> GrantSendOnBehalfTo { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600143C RID: 5180
		// (set) Token: 0x0600143D RID: 5181
		string ImmutableId { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600143E RID: 5182
		string ImmutableIdPartial { get; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600143F RID: 5183
		bool IsMachineToPersonTextMessagingEnabled { get; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001440 RID: 5184
		bool IsPersonToPersonTextMessagingEnabled { get; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001441 RID: 5185
		bool IsResource { get; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001442 RID: 5186
		// (set) Token: 0x06001443 RID: 5187
		string LegacyExchangeDN { get; set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001444 RID: 5188
		// (set) Token: 0x06001445 RID: 5189
		bool MailboxAuditEnabled { get; set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001446 RID: 5190
		// (set) Token: 0x06001447 RID: 5191
		EnhancedTimeSpan MailboxAuditLogAgeLimit { get; set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001448 RID: 5192
		// (set) Token: 0x06001449 RID: 5193
		bool MAPIEnabled { get; set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600144A RID: 5194
		SecurityIdentifier MasterAccountSid { get; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600144B RID: 5195
		// (set) Token: 0x0600144C RID: 5196
		bool MOWAEnabled { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600144D RID: 5197
		// (set) Token: 0x0600144E RID: 5198
		string Notes { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600144F RID: 5199
		OrganizationId OrganizationId { get; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001450 RID: 5200
		// (set) Token: 0x06001451 RID: 5201
		bool OWAEnabled { get; set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001452 RID: 5202
		// (set) Token: 0x06001453 RID: 5203
		string PhoneticCompany { get; set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001454 RID: 5204
		// (set) Token: 0x06001455 RID: 5205
		string PhoneticDepartment { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001456 RID: 5206
		// (set) Token: 0x06001457 RID: 5207
		string PhoneticDisplayName { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001458 RID: 5208
		// (set) Token: 0x06001459 RID: 5209
		string PhoneticFirstName { get; set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600145A RID: 5210
		// (set) Token: 0x0600145B RID: 5211
		string PhoneticLastName { get; set; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600145C RID: 5212
		// (set) Token: 0x0600145D RID: 5213
		SmtpAddress PrimarySmtpAddress { get; set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600145E RID: 5214
		RecipientType RecipientType { get; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600145F RID: 5215
		RecipientTypeDetails RecipientTypeDetails { get; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001460 RID: 5216
		// (set) Token: 0x06001461 RID: 5217
		ADObjectId ThrottlingPolicy { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001462 RID: 5218
		string UMExtension { get; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001463 RID: 5219
		// (set) Token: 0x06001464 RID: 5220
		ADObjectId UMRecipientDialPlanId { get; set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001465 RID: 5221
		// (set) Token: 0x06001466 RID: 5222
		byte[] UMSpokenName { get; set; }
	}
}
