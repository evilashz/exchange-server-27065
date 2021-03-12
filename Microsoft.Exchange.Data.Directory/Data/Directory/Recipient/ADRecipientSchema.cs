using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000268 RID: 616
	internal class ADRecipientSchema : ADObjectSchema
	{
		// Token: 0x06001D7B RID: 7547 RVA: 0x0007AE48 File Offset: 0x00079048
		private static MailboxAuditOperations[] GetMailboxAuditOperationsValues()
		{
			List<MailboxAuditOperations> list = new List<MailboxAuditOperations>((MailboxAuditOperations[])Enum.GetValues(typeof(MailboxAuditOperations)));
			list.Remove(MailboxAuditOperations.None);
			return list.ToArray();
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0007AE80 File Offset: 0x00079080
		internal static MultiValuedProperty<MailboxAuditOperations> GetMailboxAuditOperationsFromFlags(IPropertyBag propertyBag, ADPropertyDefinition logonType)
		{
			MailboxAuditOperations mailboxAuditOperations = (MailboxAuditOperations)propertyBag[logonType];
			MultiValuedProperty<MailboxAuditOperations> multiValuedProperty = new MultiValuedProperty<MailboxAuditOperations>();
			foreach (MailboxAuditOperations mailboxAuditOperations2 in ADRecipientSchema.MailboxAuditOperationsValues)
			{
				if ((mailboxAuditOperations & mailboxAuditOperations2) == mailboxAuditOperations2)
				{
					multiValuedProperty.Add(mailboxAuditOperations2);
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0007AECC File Offset: 0x000790CC
		internal static void SetMailboxAuditOperationsFlags(object value, IPropertyBag propertyBag, ADPropertyDefinition logonType)
		{
			MailboxAuditOperations mailboxAuditOperations = MailboxAuditOperations.None;
			MultiValuedProperty<MailboxAuditOperations> multiValuedProperty = (MultiValuedProperty<MailboxAuditOperations>)value;
			foreach (MailboxAuditOperations mailboxAuditOperations2 in multiValuedProperty)
			{
				mailboxAuditOperations |= mailboxAuditOperations2;
			}
			propertyBag[logonType] = mailboxAuditOperations;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0007AF30 File Offset: 0x00079130
		internal static ModernGroupObjectType GetModernGroupTypeFromFlags(IPropertyBag propertyBag, ProviderPropertyDefinition propertyDefinition)
		{
			ProvisioningFlagValues provisioningFlagValues = (ProvisioningFlagValues)propertyBag[propertyDefinition];
			if (provisioningFlagValues.HasFlag(ProvisioningFlagValues.ModernGroupTypeUpperFlag))
			{
				if (provisioningFlagValues.HasFlag(ProvisioningFlagValues.ModernGroupTypeLowerFlag))
				{
					return ModernGroupObjectType.Public;
				}
				return ModernGroupObjectType.Secret;
			}
			else
			{
				if (provisioningFlagValues.HasFlag(ProvisioningFlagValues.ModernGroupTypeLowerFlag))
				{
					return ModernGroupObjectType.Private;
				}
				return ModernGroupObjectType.None;
			}
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0007AF98 File Offset: 0x00079198
		internal static void SetModernGroupTypeInFlags(object value, IPropertyBag propertyBag, ProviderPropertyDefinition propertyDefinition)
		{
			ModernGroupObjectType modernGroupObjectType = (ModernGroupObjectType)value;
			ProvisioningFlagValues provisioningFlagValues = (ProvisioningFlagValues)propertyBag[propertyDefinition];
			if (modernGroupObjectType == ModernGroupObjectType.Private)
			{
				provisioningFlagValues |= ProvisioningFlagValues.ModernGroupTypeLowerFlag;
				provisioningFlagValues &= ~ProvisioningFlagValues.ModernGroupTypeUpperFlag;
			}
			else if (modernGroupObjectType == ModernGroupObjectType.Secret)
			{
				provisioningFlagValues &= ~ProvisioningFlagValues.ModernGroupTypeLowerFlag;
				provisioningFlagValues |= ProvisioningFlagValues.ModernGroupTypeUpperFlag;
			}
			else if (modernGroupObjectType == ModernGroupObjectType.Public)
			{
				provisioningFlagValues |= ProvisioningFlagValues.ModernGroupTypeLowerFlag;
				provisioningFlagValues |= ProvisioningFlagValues.ModernGroupTypeUpperFlag;
			}
			else
			{
				provisioningFlagValues &= ~ProvisioningFlagValues.ModernGroupTypeLowerFlag;
				provisioningFlagValues &= ~ProvisioningFlagValues.ModernGroupTypeUpperFlag;
			}
			propertyBag[propertyDefinition] = (int)provisioningFlagValues;
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0007B018 File Offset: 0x00079218
		internal static MultiValuedProperty<string> GetInPlaceHoldFromBase(IPropertyBag propertyBag)
		{
			ADPropertyDefinition inPlaceHoldsRaw = ADRecipientSchema.InPlaceHoldsRaw;
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[inPlaceHoldsRaw];
			MultiValuedProperty<string> multiValuedProperty2 = new MultiValuedProperty<string>();
			foreach (string text in multiValuedProperty)
			{
				if (!text.StartsWith("98E9BABD09A04bcf8455A58C2AA74182", StringComparison.OrdinalIgnoreCase))
				{
					multiValuedProperty2.Add(text);
				}
			}
			return multiValuedProperty2;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0007B090 File Offset: 0x00079290
		internal static void SetInPlaceHoldFromBase(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)propertyBag[ADUserSchema.LitigationHoldEnabled];
			ADPropertyDefinition inPlaceHoldsRaw = ADRecipientSchema.InPlaceHoldsRaw;
			MultiValuedProperty<string> multiValuedProperty = value as MultiValuedProperty<string>;
			if (flag)
			{
				bool flag2 = false;
				if (multiValuedProperty == null)
				{
					multiValuedProperty = new MultiValuedProperty<string>();
				}
				MultiValuedProperty<string> multiValuedProperty2 = new MultiValuedProperty<string>();
				foreach (string text in multiValuedProperty)
				{
					if (text.StartsWith("98E9BABD09A04bcf8455A58C2AA74182", StringComparison.OrdinalIgnoreCase))
					{
						flag2 = true;
					}
					multiValuedProperty2.Add(text);
				}
				if (!flag2)
				{
					multiValuedProperty2.Add("98E9BABD09A04bcf8455A58C2AA74182");
				}
				propertyBag[inPlaceHoldsRaw] = multiValuedProperty2;
				return;
			}
			propertyBag[inPlaceHoldsRaw] = multiValuedProperty;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0007B148 File Offset: 0x00079348
		internal static object GetLitigationHoldDurationFromBase(IPropertyBag propertyBag)
		{
			Unlimited<EnhancedTimeSpan>? unlimited = null;
			bool flag = (bool)propertyBag[ADUserSchema.LitigationHoldEnabled];
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.InPlaceHoldsRaw];
			if (flag)
			{
				foreach (string text in multiValuedProperty)
				{
					if (text.StartsWith("98E9BABD09A04bcf8455A58C2AA74182", StringComparison.OrdinalIgnoreCase))
					{
						string text2 = text.Substring("98E9BABD09A04bcf8455A58C2AA74182".Length);
						if (!string.IsNullOrEmpty(text2))
						{
							EnhancedTimeSpan fromValue;
							if (text2.StartsWith(ADRecipientSchema.LegalHoldStrings.UnlimitedString, StringComparison.OrdinalIgnoreCase))
							{
								unlimited = new Unlimited<EnhancedTimeSpan>?(Unlimited<EnhancedTimeSpan>.UnlimitedValue);
							}
							else if (EnhancedTimeSpan.TryParse(text2, out fromValue))
							{
								unlimited = new Unlimited<EnhancedTimeSpan>?(fromValue);
							}
							else
							{
								unlimited = new Unlimited<EnhancedTimeSpan>?(Unlimited<EnhancedTimeSpan>.UnlimitedValue);
							}
						}
					}
				}
			}
			return unlimited;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0007B230 File Offset: 0x00079430
		internal static void SetLitigationHoldDurationOnBase(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)propertyBag[ADUserSchema.LitigationHoldEnabled];
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.InPlaceHoldsRaw];
			if (value != null)
			{
				Unlimited<EnhancedTimeSpan> value2 = (Unlimited<EnhancedTimeSpan>)value;
				if (flag && multiValuedProperty != null && multiValuedProperty.Count >= 1)
				{
					MultiValuedProperty<string> multiValuedProperty2 = new MultiValuedProperty<string>();
					foreach (string text in multiValuedProperty)
					{
						if (!text.StartsWith("98E9BABD09A04bcf8455A58C2AA74182", StringComparison.OrdinalIgnoreCase))
						{
							multiValuedProperty2.Add(text);
						}
					}
					if (value2 == Unlimited<EnhancedTimeSpan>.UnlimitedValue)
					{
						multiValuedProperty2.Add("98E9BABD09A04bcf8455A58C2AA74182" + ADRecipientSchema.LegalHoldStrings.UnlimitedString);
					}
					else
					{
						multiValuedProperty2.Add("98E9BABD09A04bcf8455A58C2AA74182" + value2.Value.TotalDays.ToString());
					}
					propertyBag[ADRecipientSchema.InPlaceHoldsRaw] = multiValuedProperty2;
				}
			}
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0007B408 File Offset: 0x00079608
		private static ADPropertyDefinition SCLThresholdProperty(string name, ADPropertyDefinition supportingProperty)
		{
			GetterDelegate getterDelegate = delegate(IPropertyBag bag)
			{
				int? num = (int?)bag[supportingProperty];
				return (num != null) ? new int?(num.Value & 15) : null;
			};
			SetterDelegate setterDelegate = delegate(object value, IPropertyBag bag)
			{
				int? num = value as int?;
				if (num == null)
				{
					bag[supportingProperty] = null;
					return;
				}
				int num2 = ((int?)bag[supportingProperty]) ?? int.MinValue;
				num2 &= -16;
				num2 |= (num.Value & 15);
				bag[supportingProperty] = num2;
			};
			return new ADPropertyDefinition(name, supportingProperty.VersionAdded, typeof(int?), null, ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
			{
				new RangedNullableValueConstraint<int>(0, 9)
			}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
			{
				supportingProperty
			}, null, getterDelegate, setterDelegate, null, null);
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0007B57C File Offset: 0x0007977C
		private static ADPropertyDefinition SCLThresholdEnabledProperty(string name, int defaultSCLThreshold, ADPropertyDefinition supportingProperty)
		{
			GetterDelegate getterDelegate = delegate(IPropertyBag bag)
			{
				int? num = (int?)bag[supportingProperty];
				return (num != null) ? new bool?((num.Value & int.MinValue) != 0) : null;
			};
			SetterDelegate setterDelegate = delegate(object value, IPropertyBag bag)
			{
				bool? flag = value as bool?;
				if (flag == null)
				{
					bag[supportingProperty] = null;
					return;
				}
				int num = ((int?)bag[supportingProperty]) ?? (flag.Value ? 15 : defaultSCLThreshold);
				num &= int.MaxValue;
				if (flag.Value)
				{
					num |= int.MinValue;
				}
				bag[supportingProperty] = num;
			};
			return new ADPropertyDefinition(name, supportingProperty.VersionAdded, typeof(bool?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
			{
				supportingProperty
			}, null, getterDelegate, setterDelegate, null, null);
		}

		// Token: 0x04000E46 RID: 3654
		public static readonly ADPropertyDefinition DisplayName = new ADPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2003, typeof(string), "displayName", "msExchShadowDisplayName", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new MandatoryStringLengthConstraint(1, 256),
			new NoLeadingOrTrailingWhitespaceConstraint()
		}, SimpleProviderPropertyDefinition.None, null, null, null, MServRecipientSchema.DisplayName, null);

		// Token: 0x04000E47 RID: 3655
		public static readonly ADPropertyDefinition Description = new ADPropertyDefinition("Description", ExchangeObjectVersion.Exchange2003, typeof(string), "description", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.Description);

		// Token: 0x04000E48 RID: 3656
		public static readonly ADPropertyDefinition AcceptMessagesOnlyFrom = new ADPropertyDefinition("AcceptMessagesOnlyFrom", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "authOrig", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E49 RID: 3657
		public static readonly ADPropertyDefinition AcceptMessagesOnlyFromBL = new ADPropertyDefinition("AcceptMessagesOnlyFromBL", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "authOrigBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E4A RID: 3658
		public static readonly ADPropertyDefinition AcceptMessagesOnlyFromDLMembers = new ADPropertyDefinition("AcceptMessagesOnlyFromDLMembers", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "dLMemSubmitPerms", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E4B RID: 3659
		public static readonly ADPropertyDefinition AcceptMessagesOnlyFromDLMembersBL = new ADPropertyDefinition("AcceptMessagesOnlyFromDLMembersBL", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "dLMemSubmitPermsBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E4C RID: 3660
		public static readonly ADPropertyDefinition AddressBookPolicy = new ADPropertyDefinition("AddressBookPolicy", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchAddressBookPolicyLink", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E4D RID: 3661
		public static readonly ADPropertyDefinition AddressListMembership = new ADPropertyDefinition("AddressListMembership", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "showInAddressBook", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E4E RID: 3662
		public static readonly ADPropertyDefinition Alias = new ADPropertyDefinition("Alias", ExchangeObjectVersion.Exchange2003, typeof(string), "mailNickname", "msExchShadowMailNickname", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new MandatoryStringLengthConstraint(1, 64),
			new RegexConstraint("^[!#%&'=`~\\$\\*\\+\\-\\/\\?\\^\\{\\|\\}a-zA-Z0-9_\\u00A1-\\u00FF]+(\\.[!#%&'=`~\\$\\*\\+\\-\\/\\?\\^\\{\\|\\}a-zA-Z0-9_\\u00A1-\\u00FF]+)*$", DataStrings.AliasPatternDescription),
			new CharacterConstraint("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%&'*+-/=?^_`{|}~.¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ".ToCharArray(), true)
		}, SimpleProviderPropertyDefinition.None, null, null, null, MServRecipientSchema.Alias, null);

		// Token: 0x04000E4F RID: 3663
		public static readonly ADPropertyDefinition AllowedAttributesEffective = new ADPropertyDefinition("AllowedAttributesEffective", ExchangeObjectVersion.Exchange2003, typeof(string), "allowedAttributesEffective", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E50 RID: 3664
		public static readonly ADPropertyDefinition AssistantName = new ADPropertyDefinition("AssistantName", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchAssistantName", "msExchShadowAssistantName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 256)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.AssistantName);

		// Token: 0x04000E51 RID: 3665
		public static readonly ADPropertyDefinition AttributeMetadata = new ADPropertyDefinition("AttributeMetadata", ExchangeObjectVersion.Exchange2003, typeof(AttributeMetadata), "msDS-ReplAttributeMetaData;binary", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E52 RID: 3666
		public static readonly ADPropertyDefinition BypassModerationFrom = new ADPropertyDefinition("BypassModerationFrom", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchBypassModerationLink", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E53 RID: 3667
		public static readonly ADPropertyDefinition AuthenticationType = new ADPropertyDefinition("AuthenticationType", ExchangeObjectVersion.Exchange2010, typeof(AuthenticationType?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E54 RID: 3668
		public static readonly ADPropertyDefinition BypassModerationFromBL = new ADPropertyDefinition("BypassModerationFromBL", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchBypassModerationBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E55 RID: 3669
		public static readonly ADPropertyDefinition BypassModerationFromDLMembers = new ADPropertyDefinition("BypassModerationFromDLMembers", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchBypassModerationFromDLMembersLink", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E56 RID: 3670
		public static readonly ADPropertyDefinition BypassModerationFromDLMembersBL = new ADPropertyDefinition("BypassModerationFromDLMembersBL", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchBypassModerationFromDLMembersBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E57 RID: 3671
		public static readonly ADPropertyDefinition Certificate = new ADPropertyDefinition("Certificate", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "userCertificate", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E58 RID: 3672
		public static readonly ADPropertyDefinition RawCapabilities = SharedPropertyDefinitions.RawCapabilities;

		// Token: 0x04000E59 RID: 3673
		public static readonly ADPropertyDefinition Capabilities = SharedPropertyDefinitions.Capabilities;

		// Token: 0x04000E5A RID: 3674
		public static readonly ADPropertyDefinition Notes = new ADPropertyDefinition("Notes", ExchangeObjectVersion.Exchange2003, typeof(string), "info", "msExchShadowInfo", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.Notes);

		// Token: 0x04000E5B RID: 3675
		public static readonly ADPropertyDefinition CustomAttribute1 = new ADPropertyDefinition("CustomAttribute1", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute1", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E5C RID: 3676
		public static readonly ADPropertyDefinition CustomAttribute10 = new ADPropertyDefinition("CustomAttribute10", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute10", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E5D RID: 3677
		public static readonly ADPropertyDefinition CustomAttribute11 = new ADPropertyDefinition("CustomAttribute11", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute11", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 2048)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E5E RID: 3678
		public static readonly ADPropertyDefinition CustomAttribute12 = new ADPropertyDefinition("CustomAttribute12", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute12", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 2048)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E5F RID: 3679
		public static readonly ADPropertyDefinition CustomAttribute13 = new ADPropertyDefinition("CustomAttribute13", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute13", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 2048)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E60 RID: 3680
		public static readonly ADPropertyDefinition CustomAttribute14 = new ADPropertyDefinition("CustomAttribute14", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute14", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 2048)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E61 RID: 3681
		public static readonly ADPropertyDefinition CustomAttribute15 = new ADPropertyDefinition("CustomAttribute15", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute15", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 2048)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E62 RID: 3682
		public static readonly ADPropertyDefinition CustomAttribute2 = new ADPropertyDefinition("CustomAttribute2", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute2", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E63 RID: 3683
		public static readonly ADPropertyDefinition CustomAttribute3 = new ADPropertyDefinition("CustomAttribute3", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute3", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E64 RID: 3684
		public static readonly ADPropertyDefinition CustomAttribute4 = new ADPropertyDefinition("CustomAttribute4", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute4", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E65 RID: 3685
		public static readonly ADPropertyDefinition CustomAttribute5 = new ADPropertyDefinition("CustomAttribute5", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute5", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E66 RID: 3686
		public static readonly ADPropertyDefinition CustomAttribute6 = new ADPropertyDefinition("CustomAttribute6", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute6", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E67 RID: 3687
		public static readonly ADPropertyDefinition CustomAttribute7 = new ADPropertyDefinition("CustomAttribute7", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute7", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E68 RID: 3688
		public static readonly ADPropertyDefinition CustomAttribute8 = new ADPropertyDefinition("CustomAttribute8", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute8", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E69 RID: 3689
		public static readonly ADPropertyDefinition CustomAttribute9 = new ADPropertyDefinition("CustomAttribute9", ExchangeObjectVersion.Exchange2003, typeof(string), "extensionAttribute9", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E6A RID: 3690
		public static readonly ADPropertyDefinition ExtensionCustomAttribute1 = new ADPropertyDefinition("ExtensionCustomAttribute1", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchExtensionCustomAttribute1", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E6B RID: 3691
		public static readonly ADPropertyDefinition ExtensionCustomAttribute2 = new ADPropertyDefinition("ExtensionCustomAttribute2", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchExtensionCustomAttribute2", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E6C RID: 3692
		public static readonly ADPropertyDefinition ExtensionCustomAttribute3 = new ADPropertyDefinition("ExtensionCustomAttribute3", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchExtensionCustomAttribute3", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E6D RID: 3693
		public static readonly ADPropertyDefinition ExtensionCustomAttribute4 = new ADPropertyDefinition("ExtensionCustomAttribute4", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchExtensionCustomAttribute4", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E6E RID: 3694
		public static readonly ADPropertyDefinition ExtensionCustomAttribute5 = new ADPropertyDefinition("ExtensionCustomAttribute5", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchExtensionCustomAttribute5", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E6F RID: 3695
		public static readonly ADPropertyDefinition EmailAddresses = new ADPropertyDefinition("EmailAddresses", ExchangeObjectVersion.Exchange2003, typeof(ProxyAddress), "proxyAddresses", "msExchShadowProxyAddresses", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1123)
		}, SimpleProviderPropertyDefinition.None, null, null, null, MServRecipientSchema.EmailAddresses, MbxRecipientSchema.EmailAddresses);

		// Token: 0x04000E70 RID: 3696
		public static readonly ADPropertyDefinition ExcludedFromBackSync = new ADPropertyDefinition("ExcludedFromBackSync", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.RawCapabilities
		}, new CustomFilterBuilderDelegate(ADRecipient.IsExcludedFromBacksyncFilterBuilder), (IPropertyBag propertyBag) => (((MultiValuedProperty<Capability>)propertyBag[ADRecipientSchema.RawCapabilities]) ?? new MultiValuedProperty<Capability>()).Contains(Capability.ExcludedFromBackSync), delegate(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<Capability> multiValuedProperty = (MultiValuedProperty<Capability>)propertyBag[ADRecipientSchema.RawCapabilities];
			if (multiValuedProperty == null)
			{
				multiValuedProperty = new MultiValuedProperty<Capability>();
				propertyBag[ADRecipientSchema.RawCapabilities] = multiValuedProperty;
			}
			bool flag = (bool)value;
			if (flag)
			{
				multiValuedProperty.Add(Capability.ExcludedFromBackSync);
				return;
			}
			multiValuedProperty.Remove(Capability.ExcludedFromBackSync);
		}, null, null);

		// Token: 0x04000E71 RID: 3697
		public static readonly ADPropertyDefinition ExternalDirectoryObjectId = new ADPropertyDefinition("ExternalDirectoryObjectId", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchExternalDirectoryObjectId", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E72 RID: 3698
		public static readonly ADPropertyDefinition UMAddresses = new ADPropertyDefinition("UMAddresses", ExchangeObjectVersion.Exchange2010, typeof(ProxyAddress), "msExchUMAddresses", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(1, 1123)
		}, null, null);

		// Token: 0x04000E73 RID: 3699
		public static readonly ADPropertyDefinition RawExternalEmailAddress = new ADPropertyDefinition("RawExternalEmailAddress", ExchangeObjectVersion.Exchange2003, typeof(ProxyAddress), "targetAddress", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E74 RID: 3700
		public static readonly ADPropertyDefinition ForwardingAddress = new ADPropertyDefinition("ForwardingAddress", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "altRecipient", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E75 RID: 3701
		public static readonly ADPropertyDefinition ForwardingAddressBL = new ADPropertyDefinition("ForwardingAddressBL", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "altRecipientBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E76 RID: 3702
		public static readonly ADPropertyDefinition GenericForwardingAddress = new ADPropertyDefinition("GenericForwardingAddress", ExchangeObjectVersion.Exchange2010, typeof(ProxyAddress), "msExchGenericForwardingAddress", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.GenericForwardingAddress);

		// Token: 0x04000E77 RID: 3703
		public static readonly ADPropertyDefinition ForwardingSmtpAddress = new ADPropertyDefinition("ForwardingSmtpAddress", ExchangeObjectVersion.Exchange2010, typeof(ProxyAddress), "msExchGenericForwardingAddress", ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.GenericForwardingAddress
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), (IPropertyBag propertyBag) => (ProxyAddress)propertyBag[ADRecipientSchema.GenericForwardingAddress], new SetterDelegate(ADRecipient.ForwardingSmtpAddressSetter), null, MbxRecipientSchema.ForwardingSmtpAddress);

		// Token: 0x04000E78 RID: 3704
		public static readonly ADPropertyDefinition Latitude = new ADPropertyDefinition("Latitude", ExchangeObjectVersion.Exchange2003, typeof(int?), "msDS-GeoCoordinatesLatitude", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(-90000000, 90000000)
		}, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.Latitude);

		// Token: 0x04000E79 RID: 3705
		public static readonly ADPropertyDefinition Longitude = new ADPropertyDefinition("Longitude", ExchangeObjectVersion.Exchange2003, typeof(int?), "msDS-GeoCoordinatesLongitude", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(-180000000, 180000000)
		}, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.Longitude);

		// Token: 0x04000E7A RID: 3706
		public static readonly ADPropertyDefinition Altitude = new ADPropertyDefinition("Altitude", ExchangeObjectVersion.Exchange2003, typeof(int?), "msDS-GeoCoordinatesAltitude", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.Altitude);

		// Token: 0x04000E7B RID: 3707
		public static readonly ADPropertyDefinition GeoCoordinates = new ADPropertyDefinition("GeoCoordinates", ExchangeObjectVersion.Exchange2003, typeof(GeoCoordinates), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.Latitude,
			ADRecipientSchema.Longitude,
			ADRecipientSchema.Altitude
		}, null, delegate(IPropertyBag propertyBag)
		{
			int? num = (int?)propertyBag[ADRecipientSchema.Latitude];
			int? num2 = (int?)propertyBag[ADRecipientSchema.Longitude];
			int? num3 = (int?)propertyBag[ADRecipientSchema.Altitude];
			if (num == null || num2 == null)
			{
				return null;
			}
			double latitude = (double)num.Value / 1000000.0;
			double longitude = (double)num2.Value / 1000000.0;
			if (num3 == null)
			{
				return new GeoCoordinates(latitude, longitude);
			}
			return new GeoCoordinates(latitude, longitude, (double)num3.Value / 1000.0);
		}, delegate(object value, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				propertyBag[ADRecipientSchema.Latitude] = null;
				propertyBag[ADRecipientSchema.Longitude] = null;
				propertyBag[ADRecipientSchema.Altitude] = null;
				return;
			}
			GeoCoordinates geoCoordinates = (GeoCoordinates)value;
			propertyBag[ADRecipientSchema.Latitude] = new int?((int)(geoCoordinates.Latitude * 1000000.0));
			propertyBag[ADRecipientSchema.Longitude] = new int?((int)(geoCoordinates.Longitude * 1000000.0));
			PropertyDefinition altitude = ADRecipientSchema.Altitude;
			int? num;
			if (geoCoordinates.Altitude == null)
			{
				num = null;
			}
			else
			{
				double? altitude2 = geoCoordinates.Altitude;
				double? num2 = (altitude2 != null) ? new double?(altitude2.GetValueOrDefault() * 1000.0) : null;
				num = ((num2 != null) ? new int?((int)num2.GetValueOrDefault()) : null);
			}
			propertyBag[altitude] = num;
		}, null, MbxRecipientSchema.GeoCoordinates);

		// Token: 0x04000E7C RID: 3708
		public static readonly ADPropertyDefinition GrantSendOnBehalfTo = new ADPropertyDefinition("GrantSendOnBehalfTo", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "publicDelegates", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E7D RID: 3709
		public static readonly ADPropertyDefinition GrantSendOnBehalfToBL = new ADPropertyDefinition("GrantSendOnBehalfToBL", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "publicDelegatesBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E7E RID: 3710
		public static readonly ADPropertyDefinition HiddenFromAddressListsValue = new ADPropertyDefinition("HiddenFromAddressListsValue", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchHideFromAddressLists", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.HiddenFromAddressListsValue);

		// Token: 0x04000E7F RID: 3711
		public static readonly ADPropertyDefinition InternetEncoding = new ADPropertyDefinition("InternetEncoding", ExchangeObjectVersion.Exchange2003, typeof(int), "internetEncoding", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.InternetEncoding);

		// Token: 0x04000E80 RID: 3712
		public static readonly ADPropertyDefinition IsDirSynced = new ADPropertyDefinition("IsDirSynced", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchIsMSODirsynced", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E81 RID: 3713
		public static readonly ADPropertyDefinition DirSyncAuthorityMetadata = new ADPropertyDefinition("DirSyncAuthorityMetadata", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchDirsyncAuthorityMetadata", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E82 RID: 3714
		public static readonly ADPropertyDefinition LegacyExchangeDN = new ADPropertyDefinition("LegacyExchangeDN", ExchangeObjectVersion.Exchange2003, typeof(string), "legacyExchangeDN", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, MServRecipientSchema.LegacyExchangeDN, null);

		// Token: 0x04000E83 RID: 3715
		public static readonly ADPropertyDefinition LinkMetadata = new ADPropertyDefinition("LinkMetadata", ExchangeObjectVersion.Exchange2003, typeof(LinkMetadata), "msDS-ReplValueMetaData;binary", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E84 RID: 3716
		public static readonly ADPropertyDefinition MemberOfGroup = new ADPropertyDefinition("MemberOfGroup", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "memberOf", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E85 RID: 3717
		public static readonly ADPropertyDefinition MessageHygieneFlags = new ADPropertyDefinition("MessageHygieneFlags", ExchangeObjectVersion.Exchange2007, typeof(MessageHygieneFlags), "msExchMessageHygieneFlags", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.Recipient.MessageHygieneFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E86 RID: 3718
		public static readonly ADPropertyDefinition RawOnPremisesObjectId = new ADPropertyDefinition("RawOnPremisesObjectId", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchOnPremiseObjectGuid", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E87 RID: 3719
		public static readonly ADPropertyDefinition OnPremisesObjectId = new ADPropertyDefinition("OnPremisesObjectId", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.RawOnPremisesObjectId
		}, new CustomFilterBuilderDelegate(ADRecipient.OnPremisesObjectIdFilterBuilder), new GetterDelegate(ADRecipient.OnPremisesObjectIdGetter), new SetterDelegate(ADRecipient.OnPremisesObjectIdSetter), null, null);

		// Token: 0x04000E88 RID: 3720
		public static readonly ADPropertyDefinition PhoneticCompany = new ADPropertyDefinition("PhoneticCompany", ExchangeObjectVersion.Exchange2003, typeof(string), "msDS-PhoneticCompanyName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E89 RID: 3721
		public static readonly ADPropertyDefinition PhoneticDepartment = new ADPropertyDefinition("PhoneticDepartment", ExchangeObjectVersion.Exchange2003, typeof(string), "msDS-PhoneticDepartment", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E8A RID: 3722
		public static readonly ADPropertyDefinition PhoneticDisplayName = new ADPropertyDefinition("PhoneticDisplayName", ExchangeObjectVersion.Exchange2003, typeof(string), "msDS-PhoneticDisplayName", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E8B RID: 3723
		public static readonly ADPropertyDefinition PhoneticFirstName = new ADPropertyDefinition("PhoneticFirstName", ExchangeObjectVersion.Exchange2003, typeof(string), "msDS-PhoneticFirstName", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E8C RID: 3724
		public static readonly ADPropertyDefinition PhoneticLastName = new ADPropertyDefinition("PhoneticLastName", ExchangeObjectVersion.Exchange2003, typeof(string), "msDS-PhoneticLastName", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E8D RID: 3725
		public static readonly ADPropertyDefinition PoliciesIncluded = new ADPropertyDefinition("PoliciesIncluded", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchPoliciesIncluded", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E8E RID: 3726
		public static readonly ADPropertyDefinition PoliciesExcluded = new ADPropertyDefinition("PoliciesExcluded", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchPoliciesExcluded", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E8F RID: 3727
		public static readonly ADPropertyDefinition ProtocolSettings = new ADPropertyDefinition("ProtocolSettings", ExchangeObjectVersion.Exchange2003, typeof(string), "protocolSettings", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E90 RID: 3728
		public static readonly ADPropertyDefinition DirSyncId = new ADPropertyDefinition("DirSyncId", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchDirsyncID", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E91 RID: 3729
		public static readonly ADPropertyDefinition RecipientLimits = new ADPropertyDefinition("RecipientLimits", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<int>), "msExchRecipLimit", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.RecipientLimits);

		// Token: 0x04000E92 RID: 3730
		public static readonly ADPropertyDefinition HomeMTA = new ADPropertyDefinition("HomeMTA", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, "homeMTA", null, "msExchHomeMTASL", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E93 RID: 3731
		public static readonly ADPropertyDefinition RejectMessagesFrom = new ADPropertyDefinition("RejectMessagesFrom", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "unauthOrig", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E94 RID: 3732
		public static readonly ADPropertyDefinition RejectMessagesFromBL = new ADPropertyDefinition("RejectMessagesFromBL", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "unauthOrigBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E95 RID: 3733
		public static readonly ADPropertyDefinition RejectMessagesFromDLMembers = new ADPropertyDefinition("RejectMessagesFromDLMembers", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "dLMemRejectPerms", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E96 RID: 3734
		public static readonly ADPropertyDefinition RejectMessagesFromDLMembersBL = new ADPropertyDefinition("RejectMessagesFromDLMembersBL", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "dLMemRejectPermsBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E97 RID: 3735
		public static readonly ADPropertyDefinition RequireAllSendersAreAuthenticated = new ADPropertyDefinition("RequireAllSendersAreAuthenticated", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchRequireAuthToSendTo", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.RequireAllSendersAreAuthenticated);

		// Token: 0x04000E98 RID: 3736
		public static readonly ADPropertyDefinition SCLDeleteThresholdInt = new ADPropertyDefinition("SCLDeleteThresholdInt", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchMessageHygieneSCLDeleteThreshold", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E99 RID: 3737
		public static readonly ADPropertyDefinition SCLRejectThresholdInt = new ADPropertyDefinition("SCLRejectThresholdInt", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchMessageHygieneSCLRejectThreshold", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E9A RID: 3738
		public static readonly ADPropertyDefinition SCLQuarantineThresholdInt = new ADPropertyDefinition("SCLQuarantineThresholdInt", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchMessageHygieneSCLQuarantineThreshold", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E9B RID: 3739
		public static readonly ADPropertyDefinition SCLJunkThresholdInt = new ADPropertyDefinition("SCLJunkThresholdInt", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchMessageHygieneSCLJunkThreshold", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000E9C RID: 3740
		public static readonly ADPropertyDefinition SimpleDisplayName = SharedPropertyDefinitions.SimpleDisplayName;

		// Token: 0x04000E9D RID: 3741
		public static readonly ADPropertyDefinition SMimeCertificate = new ADPropertyDefinition("SMimeCertificate", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "userSMIMECertificate", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000E9E RID: 3742
		public static readonly ADPropertyDefinition TextEncodedORAddress = new ADPropertyDefinition("TextEncodedORAddress", ExchangeObjectVersion.Exchange2003, typeof(string), "textEncodedORAddress", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.TextEncodedORAddress);

		// Token: 0x04000E9F RID: 3743
		public static readonly ADPropertyDefinition UMDtmfMap = new ADPropertyDefinition("UMDtmfMap", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchUMDtmfMap", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 256)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EA0 RID: 3744
		public static readonly ADPropertyDefinition AllowUMCallsFromNonUsers = new ADPropertyDefinition("AllowUMCallsFromNonUsers", ExchangeObjectVersion.Exchange2003, typeof(AllowUMCallsFromNonUsersFlags), "msExchUMListInDirectorySearch", ADPropertyDefinitionFlags.None, AllowUMCallsFromNonUsersFlags.SearchEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EA1 RID: 3745
		public static readonly ADPropertyDefinition UMRecipientDialPlanId = new ADPropertyDefinition("UMRecipientDialPlanId", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchUMRecipientDialPlanLink", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EA2 RID: 3746
		public static readonly ADPropertyDefinition UMSpokenName = new ADPropertyDefinition("UMSpokenName", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchUMSpokenName", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 32768)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EA3 RID: 3747
		public static readonly ADPropertyDefinition MapiRecipient = new ADPropertyDefinition("MapiRecipient", ExchangeObjectVersion.Exchange2003, typeof(bool?), "mAPIRecipient", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.MapiRecipient);

		// Token: 0x04000EA4 RID: 3748
		public static readonly ADPropertyDefinition WebPage = new ADPropertyDefinition("WebPage", ExchangeObjectVersion.Exchange2003, typeof(string), "wWWHomePage", "msExchShadowWWWHomePage", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 2048)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.WebPage);

		// Token: 0x04000EA5 RID: 3749
		public static readonly ADPropertyDefinition WindowsEmailAddress = new ADPropertyDefinition("WindowsEmailAddress", ExchangeObjectVersion.Exchange2003, typeof(SmtpAddress), "mail", ADPropertyDefinitionFlags.DoNotProvisionalClone, SmtpAddress.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValidSmtpAddressConstraint()
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EA6 RID: 3750
		public static readonly ADPropertyDefinition WindowsLiveID = new ADPropertyDefinition("WindowsLiveID", ExchangeObjectVersion.Exchange2003, typeof(SmtpAddress), "msExchWindowsLiveID", "msExchShadowWindowsLiveID", ADPropertyDefinitionFlags.DoNotProvisionalClone, SmtpAddress.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 256),
			new ValidSmtpAddressConstraint()
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EA7 RID: 3751
		public static readonly ADPropertyDefinition SafeSendersHash = new ADPropertyDefinition("SafeSendersHash", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchSafeSendersHash", ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 12288)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EA8 RID: 3752
		public static readonly ADPropertyDefinition SafeRecipientsHash = new ADPropertyDefinition("SafeRecipientsHash", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchSafeRecipientsHash", ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 8192)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EA9 RID: 3753
		public static readonly ADPropertyDefinition BlockedSendersHash = new ADPropertyDefinition("BlockedSendersHash", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchBlockedSendersHash", ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 4000)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EAA RID: 3754
		public static readonly ADPropertyDefinition UsnChanged = SharedPropertyDefinitions.UsnChanged;

		// Token: 0x04000EAB RID: 3755
		internal static readonly ADPropertyDefinition UsnCreated = new ADPropertyDefinition("UsnCreated", ExchangeObjectVersion.Exchange2003, typeof(long), "uSNCreated", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EAC RID: 3756
		public static readonly ADPropertyDefinition IsRootPublicFolderMailbox = new ADPropertyDefinition("IsRootPublicFolderMailbox", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EAD RID: 3757
		public static readonly ADPropertyDefinition ReconciliationId = new ADPropertyDefinition("ReconciliationId", ExchangeObjectVersion.Exchange2007, typeof(NetID), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EAE RID: 3758
		public static readonly ADPropertyDefinition AddressBookFlags = new ADPropertyDefinition("AddressBookFlags", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchAddressBookFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EAF RID: 3759
		public static readonly ADPropertyDefinition EndOfList = SharedPropertyDefinitions.EndOfList;

		// Token: 0x04000EB0 RID: 3760
		public static readonly ADPropertyDefinition Cookie = SharedPropertyDefinitions.Cookie;

		// Token: 0x04000EB1 RID: 3761
		public static readonly ADPropertyDefinition RecipientDisplayType = new ADPropertyDefinition("RecipientDisplayType", ExchangeObjectVersion.Exchange2003, typeof(RecipientDisplayType?), "msExchRecipientDisplayType", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(RecipientDisplayType))
		}, MServRecipientSchema.RecipientDisplayType, MbxRecipientSchema.RecipientDisplayType);

		// Token: 0x04000EB2 RID: 3762
		public static readonly ADPropertyDefinition RecipientTypeDetailsValue = IADMailStorageSchema.RecipientTypeDetailsValue;

		// Token: 0x04000EB3 RID: 3763
		public static readonly ADPropertyDefinition MailboxLocationsRaw = IADMailStorageSchema.MailboxLocationsRaw;

		// Token: 0x04000EB4 RID: 3764
		public static readonly ADPropertyDefinition MailboxLocations = IADMailStorageSchema.MailboxLocations;

		// Token: 0x04000EB5 RID: 3765
		public static readonly ADPropertyDefinition MailboxGuidsRaw = IADMailStorageSchema.MailboxGuidsRaw;

		// Token: 0x04000EB6 RID: 3766
		public static readonly ADPropertyDefinition MaxSendSize = new ADPropertyDefinition("MaxSendSize", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "submissionContLength", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2097151UL))
		}, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.MaxSendSize);

		// Token: 0x04000EB7 RID: 3767
		public static readonly ADPropertyDefinition MaxReceiveSize = new ADPropertyDefinition("MaxReceiveSize", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "delivContLength", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2097151UL))
		}, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.MaxReceiveSize);

		// Token: 0x04000EB8 RID: 3768
		public static readonly ADPropertyDefinition MailTipTranslations = new ADPropertyDefinition("MailTipTranslations", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchSenderHintTranslations", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateMailTip))
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.MailTipTranslations);

		// Token: 0x04000EB9 RID: 3769
		public static readonly ADPropertyDefinition DefaultDistributionListOU = new ADPropertyDefinition("DefaultDistributionListOU", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EBA RID: 3770
		public static readonly ADPropertyDefinition ThumbnailPhoto = new ADPropertyDefinition("ThumbnailPhoto", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "thumbnailPhoto", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 102400)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EBB RID: 3771
		public static readonly ADPropertyDefinition ThrottlingPolicy = new ADPropertyDefinition("ThrottlingPolicy", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchThrottlingPolicyDN", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EBC RID: 3772
		public static readonly ADPropertyDefinition InternalRecipientSupervisionList = new ADPropertyDefinition("InternalRecipientSupervisionList", ExchangeObjectVersion.Exchange2010, typeof(ADObjectIdWithString), "msExchSupervisionUserLink", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new SupervisionListEntryConstraint(false)
		}, null, null);

		// Token: 0x04000EBD RID: 3773
		public static readonly ADPropertyDefinition DLSupervisionList = new ADPropertyDefinition("DLSupervisionList", ExchangeObjectVersion.Exchange2010, typeof(ADObjectIdWithString), "msExchSupervisionDLLink", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new SupervisionListEntryConstraint(false)
		}, null, null);

		// Token: 0x04000EBE RID: 3774
		public static readonly ADPropertyDefinition OneOffSupervisionList = new ADPropertyDefinition("OneOffSupervisionList", ExchangeObjectVersion.Exchange2010, typeof(ADObjectIdWithString), "msExchSupervisionOneOffLink", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new SupervisionListEntryConstraint(true)
		}, null, null);

		// Token: 0x04000EBF RID: 3775
		public static readonly ADPropertyDefinition TextMessagingState = new ADPropertyDefinition("TextMessagingState", ExchangeObjectVersion.Exchange2010, typeof(TextMessagingStateBase), "msExchTextMessagingState", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.TextMessagingState);

		// Token: 0x04000EC0 RID: 3776
		public static readonly ADPropertyDefinition MasterAccountSid = new ADPropertyDefinition("MasterAccountSid", ExchangeObjectVersion.Exchange2003, typeof(SecurityIdentifier), "msExchMasterAccountSid", ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EC1 RID: 3777
		public static readonly ADPropertyDefinition LinkedMasterAccount = new ADPropertyDefinition("LinkedMasterAccount", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EC2 RID: 3778
		public static readonly ADPropertyDefinition ResourceCapacity = new ADPropertyDefinition("ResourceCapacity", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchResourceCapacity", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EC3 RID: 3779
		public static readonly ADPropertyDefinition ResourceMetaData = new ADPropertyDefinition("ResourceMetaData", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchResourceMetaData", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EC4 RID: 3780
		public static readonly ADPropertyDefinition ResourceSearchProperties = new ADPropertyDefinition("ResourceSearchProperties", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchResourceSearchProperties", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EC5 RID: 3781
		public static readonly ADPropertyDefinition ResourcePropertiesDisplay = new ADPropertyDefinition("ResourcePropertiesDisplay", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchResourceDisplay", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EC6 RID: 3782
		public static readonly ADPropertyDefinition ExternalSyncState = new ADPropertyDefinition("ExternalSyncState", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchExternalSyncState", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EC7 RID: 3783
		public static readonly ADPropertyDefinition TransportSettingFlags = new ADPropertyDefinition("TransportSettingFlags", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchTransportRecipientSettingsFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EC8 RID: 3784
		public static readonly ADPropertyDefinition ModerationFlags = new ADPropertyDefinition("ModerationFlags", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchModerationFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 6, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.ModerationFlags);

		// Token: 0x04000EC9 RID: 3785
		public static readonly ADPropertyDefinition ModerationEnabled = new ADPropertyDefinition("ModerationEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchEnableModeration", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000ECA RID: 3786
		public static readonly ADPropertyDefinition ModeratedBy = new ADPropertyDefinition("ModeratedBy", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchModeratedByLink", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000ECB RID: 3787
		public static readonly ADPropertyDefinition ModeratedObjectsBL = new ADPropertyDefinition("ModeratedObjectsBL", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchModeratedObjectsBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000ECC RID: 3788
		public static readonly ADPropertyDefinition CoManagedObjectsBL = new ADPropertyDefinition("CoManagedObjectsBL", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchCoManagedObjectsBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000ECD RID: 3789
		public static readonly ADPropertyDefinition ArbitrationMailbox = new ADPropertyDefinition("ArbitrationMailbox", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchArbitrationMailbox", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000ECE RID: 3790
		public static readonly ADPropertyDefinition MailboxPlan = new ADPropertyDefinition("MailboxPlan", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchParentPlanLink", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000ECF RID: 3791
		public static readonly ADPropertyDefinition MailboxPlanName = new ADPropertyDefinition("MailboxPlanName", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000ED0 RID: 3792
		public static readonly ADPropertyDefinition RoleAssignmentPolicy = new ADPropertyDefinition("RoleAssignmentPolicy", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchRBACPolicyLink", ADPropertyDefinitionFlags.ValidateInSharedConfig, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000ED1 RID: 3793
		public static readonly ADPropertyDefinition ProvisioningFlags = SharedPropertyDefinitions.ProvisioningFlags;

		// Token: 0x04000ED2 RID: 3794
		public static readonly ADPropertyDefinition RecipientSoftDeletedStatus = new ADPropertyDefinition("RecipientSoftDeletedStatus", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchRecipientSoftDeletedStatus", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.RecipientSoftDeletedStatus);

		// Token: 0x04000ED3 RID: 3795
		public static readonly ADPropertyDefinition IsSoftDeletedByRemove = new ADPropertyDefinition("IsSoftDeletedByRemove", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.RecipientSoftDeletedStatus
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADRecipientSchema.RecipientSoftDeletedStatus, 1UL)), ADObject.FlagGetterDelegate(ADRecipientSchema.RecipientSoftDeletedStatus, 1), ADObject.FlagSetterDelegate(ADRecipientSchema.RecipientSoftDeletedStatus, 1), null, MbxRecipientSchema.IsSoftDeletedByRemove);

		// Token: 0x04000ED4 RID: 3796
		public static readonly ADPropertyDefinition LEOEnabled = new ADPropertyDefinition("LEOEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 2048UL)), ADObject.FlagGetterDelegate(ADRecipientSchema.ProvisioningFlags, 2048), ADObject.FlagSetterDelegate(ADRecipientSchema.ProvisioningFlags, 2048), null, MbxRecipientSchema.LEOEnabled);

		// Token: 0x04000ED5 RID: 3797
		public static readonly ADPropertyDefinition IsSoftDeletedByDisable = new ADPropertyDefinition("IsSoftDeletedByDisable", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.RecipientSoftDeletedStatus
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADRecipientSchema.RecipientSoftDeletedStatus, 2UL)), ADObject.FlagGetterDelegate(ADRecipientSchema.RecipientSoftDeletedStatus, 2), ADObject.FlagSetterDelegate(ADRecipientSchema.RecipientSoftDeletedStatus, 2), null, MbxRecipientSchema.IsSoftDeletedByDisable);

		// Token: 0x04000ED6 RID: 3798
		public static readonly ADPropertyDefinition IncludeInGarbageCollection = new ADPropertyDefinition("IncludeInGarbageCollection", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.RecipientSoftDeletedStatus
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADRecipientSchema.RecipientSoftDeletedStatus, 4UL)), ADObject.FlagGetterDelegate(ADRecipientSchema.RecipientSoftDeletedStatus, 4), ADObject.FlagSetterDelegate(ADRecipientSchema.RecipientSoftDeletedStatus, 4), null, MbxRecipientSchema.IncludeInGarbageCollection);

		// Token: 0x04000ED7 RID: 3799
		public static readonly ADPropertyDefinition IsInactiveMailbox = new ADPropertyDefinition("IsInactiveMailbox", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.RecipientSoftDeletedStatus
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADRecipientSchema.RecipientSoftDeletedStatus, 8UL)), ADObject.FlagGetterDelegate(ADRecipientSchema.RecipientSoftDeletedStatus, 8), ADObject.FlagSetterDelegate(ADRecipientSchema.RecipientSoftDeletedStatus, 8), null, MbxRecipientSchema.IsInactiveMailbox);

		// Token: 0x04000ED8 RID: 3800
		public static readonly ADPropertyDefinition WhenSoftDeleted = new ADPropertyDefinition("WhenSoftDeleted", "ShadowWhenSoftDeleted", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), "msExchWhenSoftDeletedTime", "msExchShadowWhenSoftDeletedTime", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.WhenSoftDeleted);

		// Token: 0x04000ED9 RID: 3801
		public static readonly ADPropertyDefinition MailboxPlanIndex = new ADPropertyDefinition("MailboxPlanIndex", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchMailboxPlanType", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EDA RID: 3802
		public static readonly ADPropertyDefinition ImmutableId = new ADPropertyDefinition("ImmutableId", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchImmutableId", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EDB RID: 3803
		public static readonly ADPropertyDefinition AggregationSubscriptionCredential = new ADPropertyDefinition("AggregationSubscriptionCredential", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAggregationSubscriptionCredential", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.AggregationSubscriptionCredential);

		// Token: 0x04000EDC RID: 3804
		public static readonly ADPropertyDefinition MailboxPlanObject = new ADPropertyDefinition("MailboxPlanObject", ExchangeObjectVersion.Exchange2010, typeof(ADUser), null, ADPropertyDefinitionFlags.TaskPopulated | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EDD RID: 3805
		public static readonly ADPropertyDefinition HABShowInDepartments = new ADPropertyDefinition("HABShowInDepartments", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchHABShowInDepartments", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000EDE RID: 3806
		public static readonly ADPropertyDefinition LastExchangeChangedTime = IOriginatingTimestampSchema.LastExchangeChangedTime;

		// Token: 0x04000EDF RID: 3807
		public static readonly ADPropertyDefinition HABSeniorityIndex = new ADPropertyDefinition("HABSeniorityIndex", ExchangeObjectVersion.Exchange2007, typeof(int?), "msDS-HABSeniorityIndex", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.HABSeniorityIndex);

		// Token: 0x04000EE0 RID: 3808
		public static readonly ADPropertyDefinition EwsEnabled = new ADPropertyDefinition("EwsEnabled", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchEwsEnabled", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.EwsEnabled);

		// Token: 0x04000EE1 RID: 3809
		public static readonly ADPropertyDefinition EwsWellKnownApplicationAccessPolicies = new ADPropertyDefinition("EwsWellKnownApplicationAccessPolicies", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchEwsWellKnownApplicationPolicies", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.EwsWellKnownApplicationAccessPolicies);

		// Token: 0x04000EE2 RID: 3810
		public static readonly ADPropertyDefinition EwsApplicationAccessPolicy = new ADPropertyDefinition("EwsApplicationAccessPolicy", ExchangeObjectVersion.Exchange2010, typeof(EwsApplicationAccessPolicy?), "msExchEwsApplicationAccessPolicy", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(EwsApplicationAccessPolicy))
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.EwsApplicationAccessPolicy);

		// Token: 0x04000EE3 RID: 3811
		public static readonly ADPropertyDefinition EwsExceptions = new ADPropertyDefinition("EwsExceptions", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchEwsExceptions", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.EwsExceptions);

		// Token: 0x04000EE4 RID: 3812
		public static readonly ADPropertyDefinition EwsAllowOutlook = new ADPropertyDefinition("EwsAllowOutlook", ExchangeObjectVersion.Exchange2010, typeof(bool?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.EwsWellKnownApplicationAccessPolicies
		}, null, CASMailboxHelper.EwsOutlookAccessPoliciesGetterDelegate(), CASMailboxHelper.EwsOutlookAccessPoliciesSetterDelegate(), null, MbxRecipientSchema.EwsAllowOutlook);

		// Token: 0x04000EE5 RID: 3813
		public static readonly ADPropertyDefinition EwsAllowMacOutlook = new ADPropertyDefinition("EwsAllowMacOutlook", ExchangeObjectVersion.Exchange2010, typeof(bool?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.EwsWellKnownApplicationAccessPolicies
		}, null, CASMailboxHelper.EwsMacOutlookAccessPoliciesGetterDelegate(), CASMailboxHelper.EwsMacOutlookAccessPoliciesSetterDelegate(), null, MbxRecipientSchema.EwsAllowMacOutlook);

		// Token: 0x04000EE6 RID: 3814
		public static readonly ADPropertyDefinition EwsAllowEntourage = new ADPropertyDefinition("EwsAllowEntourage", ExchangeObjectVersion.Exchange2010, typeof(bool?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.EwsWellKnownApplicationAccessPolicies
		}, null, CASMailboxHelper.EwsEntourageAccessPoliciesGetterDelegate(), CASMailboxHelper.EwsEntourageAccessPoliciesSetterDelegate(), null, MbxRecipientSchema.EwsAllowEntourage);

		// Token: 0x04000EE7 RID: 3815
		internal static readonly ADPropertyDefinition InternalUsageLocation = new ADPropertyDefinition("InternalUsageLocation", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchUsageLocation", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint()
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, MbxRecipientSchema.InternalUsageLocation);

		// Token: 0x04000EE8 RID: 3816
		public static readonly ADPropertyDefinition GeneratedOfflineAddressBooks = new ADPropertyDefinition("GeneratedOfflineAddressBooks", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchOABGeneratingMailboxBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EE9 RID: 3817
		public static readonly ADPropertyDefinition AuditBypassEnabled = new ADPropertyDefinition("AuditBypassEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchBypassAudit", ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EEA RID: 3818
		public static readonly ADPropertyDefinition AuditLogAgeLimit = new ADPropertyDefinition("AuditLogAgeLimit", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan), "msExchMailboxAuditLogAgeLimit", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(90.0), new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EEB RID: 3819
		public static readonly ADPropertyDefinition AuditEnabled = new ADPropertyDefinition("AuditEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchMailboxAuditEnable", ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EEC RID: 3820
		public static readonly ADPropertyDefinition AuditAdminFlags = new ADPropertyDefinition("AuditAdminFlags", ExchangeObjectVersion.Exchange2010, typeof(MailboxAuditOperations), "msExchAuditAdmin", ADPropertyDefinitionFlags.None, MailboxAuditOperations.Update | MailboxAuditOperations.Move | MailboxAuditOperations.MoveToDeletedItems | MailboxAuditOperations.SoftDelete | MailboxAuditOperations.HardDelete | MailboxAuditOperations.FolderBind | MailboxAuditOperations.SendAs | MailboxAuditOperations.SendOnBehalf | MailboxAuditOperations.Create, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EED RID: 3821
		public static readonly ADPropertyDefinition AuditDelegateFlags = new ADPropertyDefinition("AuditDelegateFlags", ExchangeObjectVersion.Exchange2010, typeof(MailboxAuditOperations), "msExchAuditDelegate", ADPropertyDefinitionFlags.None, MailboxAuditOperations.Update | MailboxAuditOperations.SoftDelete | MailboxAuditOperations.HardDelete | MailboxAuditOperations.SendAs | MailboxAuditOperations.Create, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EEE RID: 3822
		public static readonly ADPropertyDefinition AuditDelegateAdminFlags = new ADPropertyDefinition("AuditDelegateAdminFlags", ExchangeObjectVersion.Exchange2010, typeof(MailboxAuditOperations), "msExchAuditDelegateAdmin", ADPropertyDefinitionFlags.None, MailboxAuditOperations.Update | MailboxAuditOperations.Move | MailboxAuditOperations.MoveToDeletedItems | MailboxAuditOperations.SoftDelete | MailboxAuditOperations.HardDelete | MailboxAuditOperations.FolderBind | MailboxAuditOperations.SendAs | MailboxAuditOperations.SendOnBehalf | MailboxAuditOperations.Create, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EEF RID: 3823
		public static readonly ADPropertyDefinition AuditOwnerFlags = new ADPropertyDefinition("AuditOwnerFlags", ExchangeObjectVersion.Exchange2010, typeof(MailboxAuditOperations), "msExchAuditOwner", ADPropertyDefinitionFlags.None, MailboxAuditOperations.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EF0 RID: 3824
		public static readonly ADPropertyDefinition AuditLastAdminAccess = new ADPropertyDefinition("AuditLastAdminAccess", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), "msExchMailboxAuditLastAdminAccess", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EF1 RID: 3825
		public static readonly ADPropertyDefinition AuditLastDelegateAccess = new ADPropertyDefinition("AuditLastDelegateAccess", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), "msExchMailboxAuditLastDelegateAccess", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EF2 RID: 3826
		public static readonly ADPropertyDefinition AuditLastExternalAccess = new ADPropertyDefinition("AuditLastExternalAccess", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), "msExchMailboxAuditLastExternalAccess", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000EF3 RID: 3827
		public static readonly ADPropertyDefinition InPlaceHoldsRaw = new ADPropertyDefinition("InPlaceHoldsRaw", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchUserHoldPolicies", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 40)
		}, null, null);

		// Token: 0x04000EF4 RID: 3828
		public static readonly ADPropertyDefinition InPlaceHolds = new ADPropertyDefinition("InPlaceHolds", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.InPlaceHoldsRaw,
			ADUserSchema.ElcMailboxFlags
		}, new CustomFilterBuilderDelegate(ADUser.InPlaceHoldsFilterBuilder), new GetterDelegate(ADRecipientSchema.GetInPlaceHoldFromBase), new SetterDelegate(ADRecipientSchema.SetInPlaceHoldFromBase), null, MbxRecipientSchema.InPlaceHolds);

		// Token: 0x04000EF5 RID: 3829
		public static readonly ADPropertyDefinition LitigationHoldDuration = new ADPropertyDefinition("LitigationHoldDuration", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<EnhancedTimeSpan>?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.InPlaceHoldsRaw,
			ADUserSchema.ElcMailboxFlags
		}, null, new GetterDelegate(ADRecipientSchema.GetLitigationHoldDurationFromBase), new SetterDelegate(ADRecipientSchema.SetLitigationHoldDurationOnBase), null, null);

		// Token: 0x04000EF6 RID: 3830
		public static readonly ADPropertyDefinition AuditAdmin = new ADPropertyDefinition("AuditAdmin", ExchangeObjectVersion.Exchange2010, typeof(MailboxAuditOperations), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.AuditAdminFlags
		}, null, (IPropertyBag propertyBag) => ADRecipientSchema.GetMailboxAuditOperationsFromFlags(propertyBag, ADRecipientSchema.AuditAdminFlags), delegate(object value, IPropertyBag propertyBag)
		{
			ADRecipientSchema.SetMailboxAuditOperationsFlags(value, propertyBag, ADRecipientSchema.AuditAdminFlags);
		}, null, null);

		// Token: 0x04000EF7 RID: 3831
		public static readonly ADPropertyDefinition AuditDelegate = new ADPropertyDefinition("AuditDelegate", ExchangeObjectVersion.Exchange2010, typeof(MailboxAuditOperations), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.AuditDelegateFlags
		}, null, (IPropertyBag propertyBag) => ADRecipientSchema.GetMailboxAuditOperationsFromFlags(propertyBag, ADRecipientSchema.AuditDelegateFlags), delegate(object value, IPropertyBag propertyBag)
		{
			ADRecipientSchema.SetMailboxAuditOperationsFlags(value, propertyBag, ADRecipientSchema.AuditDelegateFlags);
		}, null, null);

		// Token: 0x04000EF8 RID: 3832
		public static readonly ADPropertyDefinition AuditDelegateAdmin = new ADPropertyDefinition("AuditDelegateAdmin", ExchangeObjectVersion.Exchange2010, typeof(MailboxAuditOperations), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.AuditDelegateAdminFlags
		}, null, (IPropertyBag propertyBag) => ADRecipientSchema.GetMailboxAuditOperationsFromFlags(propertyBag, ADRecipientSchema.AuditDelegateAdminFlags), delegate(object value, IPropertyBag propertyBag)
		{
			ADRecipientSchema.SetMailboxAuditOperationsFlags(value, propertyBag, ADRecipientSchema.AuditDelegateAdminFlags);
		}, null, null);

		// Token: 0x04000EF9 RID: 3833
		public static readonly ADPropertyDefinition AuditOwner = new ADPropertyDefinition("AuditOwner", ExchangeObjectVersion.Exchange2010, typeof(MailboxAuditOperations), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.AuditOwnerFlags
		}, null, (IPropertyBag propertyBag) => ADRecipientSchema.GetMailboxAuditOperationsFromFlags(propertyBag, ADRecipientSchema.AuditOwnerFlags), delegate(object value, IPropertyBag propertyBag)
		{
			ADRecipientSchema.SetMailboxAuditOperationsFlags(value, propertyBag, ADRecipientSchema.AuditOwnerFlags);
		}, null, null);

		// Token: 0x04000EFA RID: 3834
		public static readonly ADPropertyDefinition UseUCCAuditConfig = new ADPropertyDefinition("UseUCCAuditConfig", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 65536UL)), ADObject.FlagGetterDelegate(ADRecipientSchema.ProvisioningFlags, 65536), ADObject.FlagSetterDelegate(ADRecipientSchema.ProvisioningFlags, 65536), null, MbxRecipientSchema.UseUCCAuditConfig);

		// Token: 0x04000EFB RID: 3835
		public static readonly ADPropertyDefinition JournalArchiveAddress = new ADPropertyDefinition("JournalArchiveAddress", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.DoNotProvisionalClone, SmtpAddress.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.EmailAddresses
		}, new CustomFilterBuilderDelegate(ADRecipient.JournalArchiveAddressFilterBuilder), new GetterDelegate(ADRecipient.JournalArchiveAddressGetter), new SetterDelegate(ADRecipient.JournalArchiveAddressSetter), null, MbxRecipientSchema.JournalArchiveAddress);

		// Token: 0x04000EFC RID: 3836
		private static readonly MailboxAuditOperations[] MailboxAuditOperationsValues = ADRecipientSchema.GetMailboxAuditOperationsValues();

		// Token: 0x04000EFD RID: 3837
		public static readonly ADPropertyDefinition CommonName = new ADPropertyDefinition("CommonName", ExchangeObjectVersion.Exchange2003, typeof(string), "cn", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.RawName
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(ADRecipient.CommonNameGetter), null, MServRecipientSchema.CommonName, null);

		// Token: 0x04000EFE RID: 3838
		public static readonly ADPropertyDefinition ExternalEmailAddress = new ADPropertyDefinition("ExternalEmailAddress", ExchangeObjectVersion.Exchange2003, typeof(ProxyAddress), "targetAddress", ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1123)
		}, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.RawExternalEmailAddress,
			ADRecipientSchema.EmailAddresses,
			ADObjectSchema.ObjectClass
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), (IPropertyBag propertyBag) => (ProxyAddress)propertyBag[ADRecipientSchema.RawExternalEmailAddress], new SetterDelegate(ADRecipient.ExternalEmailAddressSetter), null, null);

		// Token: 0x04000EFF RID: 3839
		public static readonly ADPropertyDefinition IsCalculatedTargetAddress = new ADPropertyDefinition("IsCalculatedTargetAddress", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchCalculatedTargetAddress", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.IsCalculatedTargetAddress);

		// Token: 0x04000F00 RID: 3840
		public static readonly ADPropertyDefinition IsExcludedFromServingHierarchy = new ADPropertyDefinition("IsExcludedFromServingHierarchy", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(ADRecipientSchema.ProvisioningFlags, 1024), ADObject.FlagSetterDelegate(ADRecipientSchema.ProvisioningFlags, 1024), null, MbxRecipientSchema.IsExcludedFromServingHierarchy);

		// Token: 0x04000F01 RID: 3841
		public static readonly ADPropertyDefinition IsHierarchyReady = new ADPropertyDefinition("IsHierarchyReady", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, null, ADObject.InvertFlagGetterDelegate(ADRecipientSchema.ProvisioningFlags, 524288), ADObject.InvertFlagSetterDelegate(ADRecipientSchema.ProvisioningFlags, 524288), null, MbxRecipientSchema.IsHierarchyReady);

		// Token: 0x04000F02 RID: 3842
		public static readonly ADPropertyDefinition ModernGroupType = new ADPropertyDefinition("ModernGroupType", ExchangeObjectVersion.Exchange2012, typeof(ModernGroupObjectType), null, ADPropertyDefinitionFlags.Calculated, ModernGroupObjectType.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, null, (IPropertyBag propertyBag) => ADRecipientSchema.GetModernGroupTypeFromFlags(propertyBag, ADRecipientSchema.ProvisioningFlags), delegate(object value, IPropertyBag propertyBag)
		{
			ADRecipientSchema.SetModernGroupTypeInFlags(value, propertyBag, ADRecipientSchema.ProvisioningFlags);
		}, null, null);

		// Token: 0x04000F03 RID: 3843
		public static readonly ADPropertyDefinition IsGroupMailboxConfigured = new ADPropertyDefinition("IsGroupMailboxConfigured", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(ADRecipientSchema.ProvisioningFlags, 16384), ADObject.FlagSetterDelegate(ADRecipientSchema.ProvisioningFlags, 16384), null, null);

		// Token: 0x04000F04 RID: 3844
		public static readonly ADPropertyDefinition GroupMailboxExternalResourcesSet = new ADPropertyDefinition("GroupMailboxExternalResourcesSet", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(ADRecipientSchema.ProvisioningFlags, 32768), ADObject.FlagSetterDelegate(ADRecipientSchema.ProvisioningFlags, 32768), null, null);

		// Token: 0x04000F05 RID: 3845
		public static readonly ADPropertyDefinition AutoSubscribeNewGroupMembers = new ADPropertyDefinition("AutoSubscribeNewGroupMembers", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(ADRecipientSchema.ProvisioningFlags, 262144), ADObject.FlagSetterDelegate(ADRecipientSchema.ProvisioningFlags, 262144), null, null);

		// Token: 0x04000F06 RID: 3846
		public static readonly ADPropertyDefinition OrganizationalUnit = new ADPropertyDefinition("OrganizationalUnit", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADRecipient.OUGetter), null, null, null);

		// Token: 0x04000F07 RID: 3847
		public static readonly ADPropertyDefinition RemotePowerShellEnabled = new ADPropertyDefinition("RemotePowerShellEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, ADRecipient.ProtocolEnabledFilterBuilder("RemotePowerShell"), CASMailboxHelper.RemotePowerShellEnabledGetterDelegate(), CASMailboxHelper.RemotePowerShellEnabledSetterDelegate(), null, MbxRecipientSchema.RemotePowerShellEnabled);

		// Token: 0x04000F08 RID: 3848
		public static readonly ADPropertyDefinition ECPEnabled = new ADPropertyDefinition("ECPEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, ADRecipient.ProtocolEnabledFilterBuilder("ECPEnabled"), CASMailboxHelper.ECPEnabledGetterDelegate(), CASMailboxHelper.ECPEnabledSetterDelegate(), null, MbxRecipientSchema.ECPEnabled);

		// Token: 0x04000F09 RID: 3849
		public static readonly ADPropertyDefinition PopEnabled = new ADPropertyDefinition("PopEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, ADRecipient.ProtocolEnabledFilterBuilder("POP3"), CASMailboxHelper.PopEnabledGetterDelegate(), CASMailboxHelper.PopEnabledSetterDelegate(), null, MbxRecipientSchema.PopEnabled);

		// Token: 0x04000F0A RID: 3850
		public static readonly ADPropertyDefinition PopUseProtocolDefaults = new ADPropertyDefinition("PopUseProtocolDefaults", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.PopUseProtocolDefaultsGetterDelegate(), CASMailboxHelper.PopUseProtocolDefaultsSetterDelegate(), null, MbxRecipientSchema.PopUseProtocolDefaults);

		// Token: 0x04000F0B RID: 3851
		public static readonly ADPropertyDefinition PopMessagesRetrievalMimeFormat = new ADPropertyDefinition("PopMessagesRetrievalMimeFormat", ExchangeObjectVersion.Exchange2003, typeof(MimeTextFormat), null, ADPropertyDefinitionFlags.Calculated, MimeTextFormat.BestBodyFormat, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.PopMessagesRetrievalMimeFormatGetterDelegate(), CASMailboxHelper.PopMessagesRetrievalMimeFormatSetterDelegate(), null, MbxRecipientSchema.PopMessagesRetrievalMimeFormat);

		// Token: 0x04000F0C RID: 3852
		public static readonly ADPropertyDefinition PopEnableExactRFC822Size = new ADPropertyDefinition("PopEnableExactRFC822Size", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.PopEnableExactRFC822SizeGetterDelegate(), CASMailboxHelper.PopEnableExactRFC822SizeSetterDelegate(), null, MbxRecipientSchema.PopEnableExactRFC822Size);

		// Token: 0x04000F0D RID: 3853
		public static readonly ADPropertyDefinition PopProtocolLoggingEnabled = new ADPropertyDefinition("PopProtocolLoggingEnabled", ExchangeObjectVersion.Exchange2010, typeof(int?), null, ADPropertyDefinitionFlags.Calculated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.PopProtocolLoggingEnabledGetterDelegate(), CASMailboxHelper.PopProtocolLoggingEnabledSetterDelegate(), null, MbxRecipientSchema.PopProtocolLoggingEnabled);

		// Token: 0x04000F0E RID: 3854
		public static readonly ADPropertyDefinition PopSuppressReadReceipt = new ADPropertyDefinition("PopSuppressReadReceipt", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.PopSuppressReadReceiptGetterDelegate(), CASMailboxHelper.PopSuppressReadReceiptSetterDelegate(), null, MbxRecipientSchema.PopSuppressReadReceipt);

		// Token: 0x04000F0F RID: 3855
		public static readonly ADPropertyDefinition PopForceICalForCalendarRetrievalOption = new ADPropertyDefinition("PopForceICalForCalendarRetrievalOption", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.PopForceICalForCalendarRetrievalOptionGetterDelegate(), CASMailboxHelper.PopForceICalForCalendarRetrievalOptionSetterDelegate(), null, MbxRecipientSchema.PopForceICalForCalendarRetrievalOption);

		// Token: 0x04000F10 RID: 3856
		public static readonly ADPropertyDefinition ImapEnabled = new ADPropertyDefinition("ImapEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, ADRecipient.ProtocolEnabledFilterBuilder("IMAP4"), CASMailboxHelper.ImapEnabledGetterDelegate(), CASMailboxHelper.ImapEnabledSetterDelegate(), null, MbxRecipientSchema.ImapEnabled);

		// Token: 0x04000F11 RID: 3857
		public static readonly ADPropertyDefinition ImapUseProtocolDefaults = new ADPropertyDefinition("ImapUseProtocolDefaults", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.ImapUseProtocolDefaultsGetterDelegate(), CASMailboxHelper.ImapUseProtocolDefaultsSetterDelegate(), null, MbxRecipientSchema.ImapUseProtocolDefaults);

		// Token: 0x04000F12 RID: 3858
		public static readonly ADPropertyDefinition ImapMessagesRetrievalMimeFormat = new ADPropertyDefinition("ImapMessagesRetrievalMimeFormat", ExchangeObjectVersion.Exchange2003, typeof(MimeTextFormat), null, ADPropertyDefinitionFlags.Calculated, MimeTextFormat.BestBodyFormat, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.ImapMessagesRetrievalMimeFormatGetterDelegate(), CASMailboxHelper.ImapMessagesRetrievalMimeFormatSetterDelegate(), null, MbxRecipientSchema.ImapMessagesRetrievalMimeFormat);

		// Token: 0x04000F13 RID: 3859
		public static readonly ADPropertyDefinition ImapEnableExactRFC822Size = new ADPropertyDefinition("ImapEnableExactRFC822Size", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.ImapEnableExactRFC822SizeGetterDelegate(), CASMailboxHelper.ImapEnableExactRFC822SizeSetterDelegate(), null, MbxRecipientSchema.ImapEnableExactRFC822Size);

		// Token: 0x04000F14 RID: 3860
		public static readonly ADPropertyDefinition ImapProtocolLoggingEnabled = new ADPropertyDefinition("ImapProtocolLoggingEnabled", ExchangeObjectVersion.Exchange2010, typeof(int?), null, ADPropertyDefinitionFlags.Calculated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.ImapProtocolLoggingEnabledGetterDelegate(), CASMailboxHelper.ImapProtocolLoggingEnabledSetterDelegate(), null, MbxRecipientSchema.ImapProtocolLoggingEnabled);

		// Token: 0x04000F15 RID: 3861
		public static readonly ADPropertyDefinition ImapSuppressReadReceipt = new ADPropertyDefinition("ImapSuppressReadReceipt", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.ImapSuppressReadReceiptGetterDelegate(), CASMailboxHelper.ImapSuppressReadReceiptSetterDelegate(), null, MbxRecipientSchema.ImapSuppressReadReceipt);

		// Token: 0x04000F16 RID: 3862
		public static readonly ADPropertyDefinition ImapForceICalForCalendarRetrievalOption = new ADPropertyDefinition("ImapForceICalForCalendarRetrievalOption", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.ImapForceICalForCalendarRetrievalOptionGetterDelegate(), CASMailboxHelper.ImapForceICalForCalendarRetrievalOptionSetterDelegate(), null, MbxRecipientSchema.ImapForceICalForCalendarRetrievalOption);

		// Token: 0x04000F17 RID: 3863
		public static readonly ADPropertyDefinition MAPIEnabled = new ADPropertyDefinition("MAPIEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, ADRecipient.ProtocolEnabledFilterBuilder("MAPI"), CASMailboxHelper.MAPIEnabledGetterDelegate(), CASMailboxHelper.MAPIEnabledSetterDelegate(), null, MbxRecipientSchema.MAPIEnabled);

		// Token: 0x04000F18 RID: 3864
		public static readonly ADPropertyDefinition MapiHttpEnabled = new ADPropertyDefinition("MapiHttpEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.MapiHttpEnabledGetterDelegate(), CASMailboxHelper.MapiHttpEnabledSetterDelegate(), null, MbxRecipientSchema.MapiHttpEnabled);

		// Token: 0x04000F19 RID: 3865
		public static readonly ADPropertyDefinition MAPIBlockOutlookNonCachedMode = new ADPropertyDefinition("MAPIBlockOutlookNonCachedMode", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.MAPIBlockOutlookNonCachedModeGetterDelegate(), CASMailboxHelper.MAPIBlockOutlookNonCachedModeSetterDelegate(), null, MbxRecipientSchema.MAPIBlockOutlookNonCachedMode);

		// Token: 0x04000F1A RID: 3866
		public static readonly ADPropertyDefinition MAPIBlockOutlookVersions = new ADPropertyDefinition("MAPIBlockOutlookVersions", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 236),
			new RegexConstraint(CASMailboxHelper.MAPIBlockOutlookVersionsPattern, RegexOptions.Compiled | RegexOptions.Singleline, DataStrings.MAPIBlockOutlookVersionsPatternDescription)
		}, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.MAPIBlockOutlookVersionsGetterDelegate(), CASMailboxHelper.MAPIBlockOutlookVersionsSetterDelegate(), null, MbxRecipientSchema.MAPIBlockOutlookVersions);

		// Token: 0x04000F1B RID: 3867
		public static readonly ADPropertyDefinition MAPIBlockOutlookRpcHttp = new ADPropertyDefinition("MAPIBlockOutlookRpcHttp", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.MAPIBlockOutlookRpcHttpGetterDelegate(), CASMailboxHelper.MAPIBlockOutlookRpcHttpSetterDelegate(), null, MbxRecipientSchema.MAPIBlockOutlookRpcHttp);

		// Token: 0x04000F1C RID: 3868
		public static readonly ADPropertyDefinition MAPIBlockOutlookExternalConnectivity = new ADPropertyDefinition("MAPIBlockOutlookExternalConnectivity", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, CASMailboxHelper.MAPIBlockOutlookExternalConnectivityGetterDelegate(), CASMailboxHelper.MAPIBlockOutlookExternalConnectivitySetterDelegate(), null, MbxRecipientSchema.MAPIBlockOutlookExternalConnectivity);

		// Token: 0x04000F1D RID: 3869
		public static readonly ADPropertyDefinition HasPicture = new ADPropertyDefinition("HasPicture", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ThumbnailPhoto
		}, null, (IPropertyBag propertyBag) => (byte[])propertyBag[ADRecipientSchema.ThumbnailPhoto] != null, null, null, null);

		// Token: 0x04000F1E RID: 3870
		public static readonly ADPropertyDefinition HasSpokenName = new ADPropertyDefinition("HasSpokenName", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.UMSpokenName
		}, null, (IPropertyBag propertyBag) => (byte[])propertyBag[ADRecipientSchema.UMSpokenName] != null, null, null, null);

		// Token: 0x04000F1F RID: 3871
		public static readonly ADPropertyDefinition AcceptMessagesOnlyFromSendersOrMembers = new ADPropertyDefinition("AcceptMessagesOnlyFromSendersOrMembers", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000F20 RID: 3872
		public static readonly ADPropertyDefinition BypassModerationFromSendersOrMembers = new ADPropertyDefinition("BypassModerationFromSendersOrMembers", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000F21 RID: 3873
		public static readonly ADPropertyDefinition RejectMessagesFromSendersOrMembers = new ADPropertyDefinition("RejectMessagesFromSendersOrMembers", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000F22 RID: 3874
		public static readonly ADPropertyDefinition UseMapiRichTextFormat = new ADPropertyDefinition("UseMapiRichTextFormat", ExchangeObjectVersion.Exchange2003, typeof(UseMapiRichTextFormat), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.Recipient.UseMapiRichTextFormat.Never, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.MapiRecipient
		}, null, new GetterDelegate(ADRecipient.UseMapiRichTextFormatGetter), new SetterDelegate(ADRecipient.UseMapiRichTextFormatSetter), null, MbxRecipientSchema.UseMapiRichTextFormat);

		// Token: 0x04000F23 RID: 3875
		public static readonly ADPropertyDefinition AntispamBypassEnabled = new ADPropertyDefinition("AntispamBypassEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.MessageHygieneFlags
		}, null, new GetterDelegate(ADRecipient.AntispamBypassEnabledGetter), new SetterDelegate(ADRecipient.AntispamBypassEnabledSetter), null, MbxRecipientSchema.AntispamBypassEnabled);

		// Token: 0x04000F24 RID: 3876
		public static readonly ADPropertyDefinition Extensions = new ADPropertyDefinition("Extensions", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.EmailAddresses
		}, null, new GetterDelegate(UMMailbox.ExtensionsGetter), null, null, null);

		// Token: 0x04000F25 RID: 3877
		public static readonly ADPropertyDefinition MailboxPlanRelease = new ADPropertyDefinition("MailboxPlanRelease", ExchangeObjectVersion.Exchange2007, typeof(MailboxPlanRelease), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.Recipient.MailboxPlanRelease.CurrentRelease, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(Microsoft.Exchange.Data.Directory.Management.MailboxPlan.MailboxPlanReleaseFilterBuilder), new GetterDelegate(Microsoft.Exchange.Data.Directory.Management.MailboxPlan.GetMailboxPlanRelease), new SetterDelegate(Microsoft.Exchange.Data.Directory.Management.MailboxPlan.SetMailboxPlanRelease), null, null);

		// Token: 0x04000F26 RID: 3878
		public static readonly ADPropertyDefinition IsAclCapable = new ADPropertyDefinition("IsAclCapable", ExchangeObjectVersion.Exchange2003, typeof(bool?), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.RecipientDisplayType
		}, null, new GetterDelegate(ADRecipient.IsAclCapableGetter), null, null, null);

		// Token: 0x04000F27 RID: 3879
		public static readonly ADPropertyDefinition IsValidSecurityPrincipal = new ADPropertyDefinition("IsValidSecurityPrincipal", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.Alias,
			IADMailStorageSchema.ServerLegacyDN,
			ADRecipientSchema.EmailAddresses,
			ADObjectSchema.ObjectClass,
			IADMailStorageSchema.Database,
			IADSecurityPrincipalSchema.GroupType,
			ADRecipientSchema.RecipientTypeDetailsValue,
			ADUserSchema.UserAccountControl,
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.MasterAccountSid,
			IADSecurityPrincipalSchema.Sid,
			ADRecipientSchema.RecipientDisplayType
		}, null, new GetterDelegate(ADRecipient.IsValidSecurityPrincipalGetter), null, null, null);

		// Token: 0x04000F28 RID: 3880
		public static readonly ADPropertyDefinition PrimarySmtpAddress = new ADPropertyDefinition("PrimarySmtpAddress", ExchangeObjectVersion.Exchange2003, typeof(SmtpAddress), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.DoNotProvisionalClone, SmtpAddress.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.EmailAddresses
		}, new CustomFilterBuilderDelegate(ADRecipient.PrimarySmtpAddressFilterBuilder), new GetterDelegate(ADRecipient.PrimarySmtpAddressGetter), new SetterDelegate(ADRecipient.PrimarySmtpAddressSetter), null, null);

		// Token: 0x04000F29 RID: 3881
		public static readonly ADPropertyDefinition RecipientType = new ADPropertyDefinition("RecipientType", ExchangeObjectVersion.Exchange2003, typeof(RecipientType), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.Recipient.RecipientType.Invalid, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.Alias,
			ADObjectSchema.ObjectClass,
			IADMailStorageSchema.ServerLegacyDN,
			IADSecurityPrincipalSchema.GroupType
		}, new CustomFilterBuilderDelegate(ADRecipient.RecipientTypeFilterBuilder), new GetterDelegate(ADRecipient.RecipientTypeGetter), null, null, null);

		// Token: 0x04000F2A RID: 3882
		public static readonly ADPropertyDefinition RecipientTypeDetails = new ADPropertyDefinition("RecipientTypeDetails", ExchangeObjectVersion.Exchange2003, typeof(RecipientTypeDetails), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.None, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(RecipientTypeDetails))
		}, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.Alias,
			IADMailStorageSchema.ServerLegacyDN,
			ADRecipientSchema.EmailAddresses,
			ADObjectSchema.ObjectClass,
			IADMailStorageSchema.Database,
			IADSecurityPrincipalSchema.GroupType,
			ADRecipientSchema.RecipientTypeDetailsValue,
			ADUserSchema.UserAccountControl
		}, new CustomFilterBuilderDelegate(ADRecipient.RecipientTypeDetailsFilterBuilder), new GetterDelegate(ADRecipient.RecipientTypeDetailsGetter), new SetterDelegate(ADRecipient.RecipientTypeDetailsSetter), null, null);

		// Token: 0x04000F2B RID: 3883
		public static readonly ADPropertyDefinition RecipientTypeDetailsRaw = new ADPropertyDefinition("RecipientTypeDetailsRaw", ExchangeObjectVersion.Exchange2003, typeof(long), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.RecipientTypeDetailsValue
		}, null, new GetterDelegate(ADRecipient.RecipientTypeDetailsRawGetter), null, null, null);

		// Token: 0x04000F2C RID: 3884
		public static readonly ADPropertyDefinition PreviousRecipientTypeDetails = IADMailStorageSchema.PreviousRecipientTypeDetails;

		// Token: 0x04000F2D RID: 3885
		public static readonly ADPropertyDefinition ReadOnlyAddressListMembership = new ADPropertyDefinition("ReadOnlyAddressListMembership", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.AddressListMembership
		}, null, new GetterDelegate(ADRecipient.ReadOnlyAddressListMembershipGetter), null, null, null);

		// Token: 0x04000F2E RID: 3886
		public static readonly ADPropertyDefinition ReadOnlyPoliciesIncluded = new ADPropertyDefinition("ReadOnlyPoliciesIncluded", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.PoliciesIncluded
		}, null, new GetterDelegate(ADRecipient.ReadOnlyPoliciesIncludedGetter), null, null, null);

		// Token: 0x04000F2F RID: 3887
		public static readonly ADPropertyDefinition ReadOnlyPoliciesExcluded = new ADPropertyDefinition("ReadOnlyPoliciesExcluded", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.PoliciesExcluded
		}, null, new GetterDelegate(ADRecipient.ReadOnlyPoliciesExcludedGetter), null, null, null);

		// Token: 0x04000F30 RID: 3888
		public static readonly ADPropertyDefinition ReadOnlyProtocolSettings = new ADPropertyDefinition("ReadOnlyProtocolSettings", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, null, new GetterDelegate(ADRecipient.ReadOnlyProtocolSettingsGetter), null, null, null);

		// Token: 0x04000F31 RID: 3889
		public static readonly ADPropertyDefinition SCLDeleteThreshold = ADRecipientSchema.SCLThresholdProperty("SCLDeleteThreshold", ADRecipientSchema.SCLDeleteThresholdInt);

		// Token: 0x04000F32 RID: 3890
		public static readonly ADPropertyDefinition SCLDeleteEnabled = ADRecipientSchema.SCLThresholdEnabledProperty("SCLDeleteEnabled", 9, ADRecipientSchema.SCLDeleteThresholdInt);

		// Token: 0x04000F33 RID: 3891
		public static readonly ADPropertyDefinition SCLRejectThreshold = ADRecipientSchema.SCLThresholdProperty("SCLRejectThreshold", ADRecipientSchema.SCLRejectThresholdInt);

		// Token: 0x04000F34 RID: 3892
		public static readonly ADPropertyDefinition SCLRejectEnabled = ADRecipientSchema.SCLThresholdEnabledProperty("SCLRejectEnabled", 7, ADRecipientSchema.SCLRejectThresholdInt);

		// Token: 0x04000F35 RID: 3893
		public static readonly ADPropertyDefinition SCLQuarantineThreshold = ADRecipientSchema.SCLThresholdProperty("SCLQuarantineThreshold", ADRecipientSchema.SCLQuarantineThresholdInt);

		// Token: 0x04000F36 RID: 3894
		public static readonly ADPropertyDefinition SCLQuarantineEnabled = ADRecipientSchema.SCLThresholdEnabledProperty("SCLQuarantineEnabled", 9, ADRecipientSchema.SCLQuarantineThresholdInt);

		// Token: 0x04000F37 RID: 3895
		public static readonly ADPropertyDefinition SCLJunkThreshold = ADRecipientSchema.SCLThresholdProperty("SCLJunkThreshold", ADRecipientSchema.SCLJunkThresholdInt);

		// Token: 0x04000F38 RID: 3896
		public static readonly ADPropertyDefinition SCLJunkEnabled = ADRecipientSchema.SCLThresholdEnabledProperty("SCLJunkEnabled", 4, ADRecipientSchema.SCLJunkThresholdInt);

		// Token: 0x04000F39 RID: 3897
		public static readonly ADPropertyDefinition HiddenFromAddressListsEnabled = new ADPropertyDefinition("HiddenFromAddressListsEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.HiddenFromAddressListsValue,
			ADRecipientSchema.RecipientTypeDetailsValue
		}, new CustomFilterBuilderDelegate(ADRecipient.HiddenFromAddressListsEnabledFilterBuilder), new GetterDelegate(ADRecipient.HiddenFromAddressListsEnabledGetter), new SetterDelegate(ADRecipient.HiddenFromAddressListsEnabledSetter), null, MbxRecipientSchema.HiddenFromAddressListsEnabled);

		// Token: 0x04000F3A RID: 3898
		public static readonly ADPropertyDefinition UsePreferMessageFormat = new ADPropertyDefinition("UsePreferMessageFormat", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.InternetEncoding
		}, null, ADObject.FlagGetterDelegate(131072, ADRecipientSchema.InternetEncoding), ADObject.FlagSetterDelegate(131072, ADRecipientSchema.InternetEncoding), null, MbxRecipientSchema.UsePreferMessageFormat);

		// Token: 0x04000F3B RID: 3899
		public static readonly ADPropertyDefinition MessageFormat = new ADPropertyDefinition("MessageFormat", ExchangeObjectVersion.Exchange2003, typeof(MessageFormat), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.Recipient.MessageFormat.Text, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(MessageFormat))
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.InternetEncoding
		}, null, new GetterDelegate(ADRecipient.MessageFormatGetter), new SetterDelegate(ADRecipient.MessageFormatSetter), null, MbxRecipientSchema.MessageFormat);

		// Token: 0x04000F3C RID: 3900
		public static readonly ADPropertyDefinition MessageBodyFormat = new ADPropertyDefinition("MessageBodyFormat", ExchangeObjectVersion.Exchange2003, typeof(MessageBodyFormat), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.Recipient.MessageBodyFormat.Text, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(MessageBodyFormat))
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.InternetEncoding
		}, null, new GetterDelegate(ADRecipient.MessageBodyFormatGetter), new SetterDelegate(ADRecipient.MessageBodyFormatSetter), null, MbxRecipientSchema.MessageBodyFormat);

		// Token: 0x04000F3D RID: 3901
		public static readonly ADPropertyDefinition MacAttachmentFormat = new ADPropertyDefinition("MacAttachmentFormat", ExchangeObjectVersion.Exchange2003, typeof(MacAttachmentFormat), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.Recipient.MacAttachmentFormat.BinHex, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(MacAttachmentFormat))
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.InternetEncoding
		}, null, new GetterDelegate(ADRecipient.MacAttachmentFormatGetter), new SetterDelegate(ADRecipient.MacAttachmentFormatSetter), null, MbxRecipientSchema.MacAttachmentFormat);

		// Token: 0x04000F3E RID: 3902
		public static readonly ADPropertyDefinition DefaultMailTip = new ADPropertyDefinition("DefaultMailTip", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.MailTipTranslations
		}, null, new GetterDelegate(ADRecipient.DefaultMailTipGetter), new SetterDelegate(ADRecipient.DefaultMailTipSetter), null, MbxRecipientSchema.DefaultMailTip);

		// Token: 0x04000F3F RID: 3903
		public static readonly ADPropertyDefinition IsPersonToPersonTextMessagingEnabled = new ADPropertyDefinition("IsPersonToPersonTextMessagingEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.TextMessagingState
		}, new CustomFilterBuilderDelegate(ADRecipient.IsPersonToPersonTextMessagingEnabledFilterBuilder), new GetterDelegate(ADRecipient.IsPersonToPersonTextMessagingEnabledGetter), null, null, null);

		// Token: 0x04000F40 RID: 3904
		public static readonly ADPropertyDefinition IsMachineToPersonTextMessagingEnabled = new ADPropertyDefinition("IsMachineToPersonTextMessagingEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.TextMessagingState
		}, new CustomFilterBuilderDelegate(ADRecipient.IsMachineToPersonTextMessagingEnabledFilterBuilder), new GetterDelegate(ADRecipient.IsMachineToPersonTextMessagingEnabledGetter), null, null, null);

		// Token: 0x04000F41 RID: 3905
		public static readonly ADPropertyDefinition ResourceCustom = new ADPropertyDefinition("ResourceCustom", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ResourceSearchProperties,
			ADRecipientSchema.ResourceMetaData,
			ADRecipientSchema.ResourcePropertiesDisplay
		}, new CustomFilterBuilderDelegate(ADRecipient.ResourceCustomFilterBuilder), new GetterDelegate(ADRecipient.ResourceCustomGetter), new SetterDelegate(ADRecipient.ResourceCustomSetter), null, null);

		// Token: 0x04000F42 RID: 3906
		public static readonly ADPropertyDefinition ResourceType = new ADPropertyDefinition("ResourceType", ExchangeObjectVersion.Exchange2007, typeof(ExchangeResourceType?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NullableEnumValueDefinedConstraint(typeof(ExchangeResourceType))
		}, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ResourceSearchProperties,
			ADRecipientSchema.ResourceMetaData
		}, new CustomFilterBuilderDelegate(ADRecipient.ResourceTypeFilterBuilder), new GetterDelegate(ADRecipient.ResourceTypeGetter), new SetterDelegate(ADRecipient.ResourceTypeSetter), null, null);

		// Token: 0x04000F43 RID: 3907
		public static readonly ADPropertyDefinition IsLinked = new ADPropertyDefinition("IsLinked", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.MasterAccountSid,
			ADUserSchema.UserAccountControl
		}, new CustomFilterBuilderDelegate(ADRecipient.IsLinkedFilterBuilder), new GetterDelegate(ADRecipient.IsLinkedGetter), null, null, null);

		// Token: 0x04000F44 RID: 3908
		public static readonly ADPropertyDefinition RecipientPersonType = new ADPropertyDefinition("RecipientPersonType", ExchangeObjectVersion.Exchange2010, typeof(PersonType), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, PersonType.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.Alias,
			ADObjectSchema.ObjectClass,
			IADMailStorageSchema.ServerLegacyDN,
			IADSecurityPrincipalSchema.GroupType,
			ADRecipientSchema.EmailAddresses,
			IADMailStorageSchema.Database,
			ADRecipientSchema.RecipientTypeDetailsValue,
			ADUserSchema.UserAccountControl,
			NspiOnlyProperties.DisplayType,
			NspiOnlyProperties.DisplayTypeEx
		}, null, new GetterDelegate(ADRecipient.RecipientPersonTypeGetter), null, null, null);

		// Token: 0x04000F45 RID: 3909
		public static readonly ADPropertyDefinition IsShared = new ADPropertyDefinition("IsShared", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.MasterAccountSid,
			ADUserSchema.UserAccountControl
		}, new CustomFilterBuilderDelegate(ADRecipient.IsSharedFilterBuilder), new GetterDelegate(ADRecipient.IsSharedGetter), null, null, null);

		// Token: 0x04000F46 RID: 3910
		public static readonly ADPropertyDefinition OWAEnabled = new ADPropertyDefinition("OWAEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProtocolSettings
		}, ADRecipient.ProtocolEnabledFilterBuilder("OWA"), ADRecipient.OwaProtocolSettingsGetterDelegate(), ADRecipient.OwaProtocolSettingsSetterDelegate(), null, MbxRecipientSchema.OWAEnabled);

		// Token: 0x04000F47 RID: 3911
		public static readonly ADPropertyDefinition IsResource = new ADPropertyDefinition("IsResource", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ResourceMetaData,
			ADRecipientSchema.ResourceSearchProperties,
			ADRecipientSchema.ResourceCapacity,
			ADRecipientSchema.MasterAccountSid
		}, new CustomFilterBuilderDelegate(ADRecipient.IsResourceFilterBuilder), new GetterDelegate(ADRecipient.IsResourceGetter), null, null, null);

		// Token: 0x04000F48 RID: 3912
		public static readonly ADPropertyDefinition EmailAddressPolicyEnabled = new ADPropertyDefinition("EmailAddressPolicyEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.PoliciesIncluded,
			ADRecipientSchema.PoliciesExcluded
		}, new CustomFilterBuilderDelegate(ADRecipient.EmailAddressPolicyEnabledFilterBuilder), new GetterDelegate(ADRecipient.EmailAddressPolicyEnabledGetter), new SetterDelegate(ADRecipient.EmailAddressPolicyEnabledSetter), null, MbxRecipientSchema.EmailAddressPolicyEnabled);

		// Token: 0x04000F49 RID: 3913
		public static readonly ADPropertyDefinition MessageTrackingReadStatusDisabled = new ADPropertyDefinition("MessageTrackingReadStatusDisabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.TransportSettingFlags
		}, null, ADObject.FlagGetterDelegate(ADRecipientSchema.TransportSettingFlags, 4), ADObject.FlagSetterDelegate(ADRecipientSchema.TransportSettingFlags, 4), null, MbxRecipientSchema.MessageTrackingReadStatusDisabled);

		// Token: 0x04000F4A RID: 3914
		public static readonly ADPropertyDefinition InternalOnly = new ADPropertyDefinition("InternalOnly", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.TransportSettingFlags
		}, null, ADObject.FlagGetterDelegate(ADRecipientSchema.TransportSettingFlags, 8), ADObject.FlagSetterDelegate(ADRecipientSchema.TransportSettingFlags, 8), null, MbxRecipientSchema.InternalOnly);

		// Token: 0x04000F4B RID: 3915
		public static readonly ADPropertyDefinition AllowArchiveAddressSync = new ADPropertyDefinition("AllowArchiveAddressSync", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.TransportSettingFlags
		}, null, ADObject.FlagGetterDelegate(ADRecipientSchema.TransportSettingFlags, 64), ADObject.FlagSetterDelegate(ADRecipientSchema.TransportSettingFlags, 64), null, MbxRecipientSchema.AllowArchiveAddressSync);

		// Token: 0x04000F4C RID: 3916
		public static readonly ADPropertyDefinition OpenDomainRoutingDisabled = new ADPropertyDefinition("OpenDomainRoutingDisabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.TransportSettingFlags
		}, null, ADObject.FlagGetterDelegate(ADRecipientSchema.TransportSettingFlags, 16), ADObject.FlagSetterDelegate(ADRecipientSchema.TransportSettingFlags, 16), null, MbxRecipientSchema.OpenDomainRoutingDisabled);

		// Token: 0x04000F4D RID: 3917
		public static readonly ADPropertyDefinition QueryBaseDNRestrictionEnabled = new ADPropertyDefinition("QueryBaseDNRestrictionEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.TransportSettingFlags
		}, null, ADObject.FlagGetterDelegate(ADRecipientSchema.TransportSettingFlags, 32), ADObject.FlagSetterDelegate(ADRecipientSchema.TransportSettingFlags, 32), null, MbxRecipientSchema.QueryBaseDNRestrictionEnabled);

		// Token: 0x04000F4E RID: 3918
		public static readonly ADPropertyDefinition MessageCopyForSentAsEnabled = new ADPropertyDefinition("MessageCopyForSentAsEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.TransportSettingFlags
		}, null, ADObject.FlagGetterDelegate(ADRecipientSchema.TransportSettingFlags, 128), ADObject.FlagSetterDelegate(ADRecipientSchema.TransportSettingFlags, 128), null, null);

		// Token: 0x04000F4F RID: 3919
		public static readonly ADPropertyDefinition MessageCopyForSendOnBehalfEnabled = new ADPropertyDefinition("MessageCopyForSendOnBehalfEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.TransportSettingFlags
		}, null, ADObject.FlagGetterDelegate(ADRecipientSchema.TransportSettingFlags, 256), ADObject.FlagSetterDelegate(ADRecipientSchema.TransportSettingFlags, 256), null, null);

		// Token: 0x04000F50 RID: 3920
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x04000F51 RID: 3921
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<UserConfigXML>(ADRecipientSchema.ConfigurationXMLRaw);

		// Token: 0x04000F52 RID: 3922
		public static readonly ADPropertyDefinition UpgradeStatus = new ADPropertyDefinition("UpgradeStatus", ExchangeObjectVersion.Exchange2003, typeof(UpgradeStatusTypes), "msExchOrganizationUpgradeStatus", ADPropertyDefinitionFlags.None, UpgradeStatusTypes.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.UpgradeStatus);

		// Token: 0x04000F53 RID: 3923
		public static readonly ADPropertyDefinition UpgradeRequest = new ADPropertyDefinition("UpgradeRequest", ExchangeObjectVersion.Exchange2003, typeof(UpgradeRequestTypes), "msExchOrganizationUpgradeRequest", ADPropertyDefinitionFlags.None, UpgradeRequestTypes.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.UpgradeRequest);

		// Token: 0x04000F54 RID: 3924
		public static readonly ADPropertyDefinition UpgradeDetails = XMLSerializableBase.ConfigXmlProperty<UserConfigXML, string>("UpgradeDetails", ExchangeObjectVersion.Exchange2003, ADRecipientSchema.ConfigurationXML, null, (UserConfigXML configXml) => configXml.UpgradeDetails, delegate(UserConfigXML configXml, string value)
		{
			configXml.UpgradeDetails = value;
		}, null, MbxRecipientSchema.UpgradeDetails);

		// Token: 0x04000F55 RID: 3925
		public static readonly ADPropertyDefinition UpgradeMessage = XMLSerializableBase.ConfigXmlProperty<UserConfigXML, string>("UpgradeMessage", ExchangeObjectVersion.Exchange2003, ADRecipientSchema.ConfigurationXML, null, (UserConfigXML configXml) => configXml.UpgradeMessage, delegate(UserConfigXML configXml, string value)
		{
			configXml.UpgradeMessage = value;
		}, null, MbxRecipientSchema.UpgradeMessage);

		// Token: 0x04000F56 RID: 3926
		public static readonly ADPropertyDefinition UpgradeStage = XMLSerializableBase.ConfigXmlProperty<UserConfigXML, UpgradeStage?>("UpgradeStage", ExchangeObjectVersion.Exchange2003, ADRecipientSchema.ConfigurationXML, null, (UserConfigXML configXml) => configXml.UpgradeStage, delegate(UserConfigXML configXml, UpgradeStage? value)
		{
			configXml.UpgradeStage = value;
		}, null, MbxRecipientSchema.UpgradeStage);

		// Token: 0x04000F57 RID: 3927
		public static readonly ADPropertyDefinition UpgradeStageTimeStamp = XMLSerializableBase.ConfigXmlProperty<UserConfigXML, DateTime?>("UpgradeStageTimeStamp", ExchangeObjectVersion.Exchange2003, ADRecipientSchema.ConfigurationXML, null, (UserConfigXML configXml) => configXml.UpgradeStageTimeStamp, delegate(UserConfigXML configXml, DateTime? value)
		{
			configXml.UpgradeStageTimeStamp = value;
		}, null, MbxRecipientSchema.UpgradeStageTimeStamp);

		// Token: 0x04000F58 RID: 3928
		public static readonly ADPropertyDefinition ReleaseTrack = XMLSerializableBase.ConfigXmlProperty<UserConfigXML, ReleaseTrack?>("ReleaseTrack", ExchangeObjectVersion.Exchange2003, ADRecipientSchema.ConfigurationXML, null, (UserConfigXML configXml) => configXml.ReleaseTrack, delegate(UserConfigXML configXml, ReleaseTrack? value)
		{
			configXml.ReleaseTrack = value;
		}, null, null);

		// Token: 0x04000F59 RID: 3929
		public static readonly ADPropertyDefinition PersistedMailboxProvisioningConstraint = new ADPropertyDefinition("PersistedMailboxProvisioningConstraint", ExchangeObjectVersion.Exchange2003, typeof(MailboxProvisioningConstraint), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, ADRecipientSchema.ConfigurationXML.SupportingProperties.ToArray<ProviderPropertyDefinition>(), null, new GetterDelegate(ADRecipient.PersistedMailboxProvisioningConstraintGetter), null, null, null);

		// Token: 0x04000F5A RID: 3930
		public static readonly ADPropertyDefinition MailboxProvisioningConstraint = new ADPropertyDefinition("MailboxProvisioningConstraint", ExchangeObjectVersion.Exchange2003, typeof(MailboxProvisioningConstraint), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, ADRecipientSchema.ConfigurationXML.SupportingProperties.ToArray<ProviderPropertyDefinition>().Concat(new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.CustomAttribute1,
			ADRecipientSchema.CustomAttribute2,
			ADRecipientSchema.CustomAttribute3,
			ADRecipientSchema.CustomAttribute4,
			ADRecipientSchema.CustomAttribute5,
			ADRecipientSchema.CustomAttribute6,
			ADRecipientSchema.CustomAttribute7,
			ADRecipientSchema.CustomAttribute8,
			ADRecipientSchema.CustomAttribute9,
			ADRecipientSchema.CustomAttribute10,
			ADRecipientSchema.CustomAttribute11,
			ADRecipientSchema.CustomAttribute12,
			ADRecipientSchema.CustomAttribute13,
			ADRecipientSchema.CustomAttribute14,
			ADRecipientSchema.CustomAttribute15,
			ADRecipientSchema.ExtensionCustomAttribute1,
			ADRecipientSchema.ExtensionCustomAttribute2,
			ADRecipientSchema.ExtensionCustomAttribute3,
			ADRecipientSchema.ExtensionCustomAttribute4,
			ADRecipientSchema.ExtensionCustomAttribute5
		}).ToArray<ProviderPropertyDefinition>(), null, new GetterDelegate(ADRecipient.MailboxProvisioningConstraintGetter), new SetterDelegate(ADRecipient.MailboxProvisioningConstraintSetter), null, null);

		// Token: 0x04000F5B RID: 3931
		public static readonly ADPropertyDefinition MailboxProvisioningPreferences = new ADPropertyDefinition("MailboxProvisioningPreferences", ExchangeObjectVersion.Exchange2003, typeof(MailboxProvisioningConstraint), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, ADRecipientSchema.ConfigurationXML.SupportingProperties.ToArray<ProviderPropertyDefinition>(), null, new GetterDelegate(ADRecipient.MailboxProvisioningPreferencesGetter), new SetterDelegate(ADRecipient.MailboxProvisioningPreferencesSetter), null, null);

		// Token: 0x04000F5C RID: 3932
		public static readonly ADPropertyDefinition IsDefault_R3 = new ADPropertyDefinition("IsDefault_R3", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(Microsoft.Exchange.Data.Directory.Management.MailboxPlan.IsDefault_R3_FilterBuilder), ADObject.FlagGetterDelegate(ADRecipientSchema.ProvisioningFlags, 4), ADObject.FlagSetterDelegate(ADRecipientSchema.ProvisioningFlags, 4), null, null);

		// Token: 0x04000F5D RID: 3933
		public static readonly ADPropertyDefinition IsDefault = new ADPropertyDefinition("IsDefault", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(Microsoft.Exchange.Data.Directory.Management.MailboxPlan.IsDefaultFilterBuilder), ADObject.FlagGetterDelegate(ADRecipientSchema.ProvisioningFlags, 8), ADObject.FlagSetterDelegate(ADRecipientSchema.ProvisioningFlags, 8), null, null);

		// Token: 0x04000F5E RID: 3934
		public static readonly ADPropertyDefinition SKUAssigned = new ADPropertyDefinition("SKUAssigned", ExchangeObjectVersion.Exchange2003, typeof(bool?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(ADUser.SKUAssignedFilterBuilder), new GetterDelegate(ADUser.SKUAssignedGetter), new SetterDelegate(ADUser.SKUAssignedSetter), null, null);

		// Token: 0x04000F5F RID: 3935
		public static readonly ADPropertyDefinition BypassNestedModerationEnabled = new ADPropertyDefinition("BypassNestedModerationEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ModerationFlags
		}, null, ADObject.FlagGetterDelegate(1, ADRecipientSchema.ModerationFlags), ADObject.FlagSetterDelegate(1, ADRecipientSchema.ModerationFlags), null, MbxRecipientSchema.BypassNestedModerationEnabled);

		// Token: 0x04000F60 RID: 3936
		public static readonly ADPropertyDefinition SendModerationNotifications = new ADPropertyDefinition("SendModerationNotifications", ExchangeObjectVersion.Exchange2010, typeof(TransportModerationNotificationFlags), null, ADPropertyDefinitionFlags.Calculated, TransportModerationNotificationFlags.Always, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ModerationFlags
		}, null, new GetterDelegate(ADRecipient.SendModerationNotificationsGetter), new SetterDelegate(ADRecipient.SendModerationNotificationsSetter), null, null);

		// Token: 0x04000F61 RID: 3937
		public static readonly ADPropertyDefinition UMProvisioningRequested = new ADPropertyDefinition("UMProvisioningRequested", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(ADRecipient.UMProvisioningFlagFilterBuilder), ADObject.FlagGetterDelegate(ADRecipientSchema.ProvisioningFlags, 2), ADObject.FlagSetterDelegate(ADRecipientSchema.ProvisioningFlags, 2), null, null);

		// Token: 0x04000F62 RID: 3938
		public static readonly ADPropertyDefinition UCSImListMigrationCompleted = new ADPropertyDefinition("UCSImListMigrationCompleted", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(ADRecipient.UCSImListMigrationCompletedFlagFilterBuilder), ADObject.FlagGetterDelegate(ADRecipientSchema.ProvisioningFlags, 256), ADObject.FlagSetterDelegate(ADRecipientSchema.ProvisioningFlags, 256), null, MbxRecipientSchema.UCSImListMigrationCompleted);

		// Token: 0x04000F63 RID: 3939
		public static readonly ADPropertyDefinition UsageLocation = new ADPropertyDefinition("UsageLocation", ExchangeObjectVersion.Exchange2003, typeof(CountryInfo), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.InternalUsageLocation
		}, new CustomFilterBuilderDelegate(ADRecipient.UsageLocationFilterBuilder), new GetterDelegate(ADRecipient.UsageLocationGetter), new SetterDelegate(ADRecipient.UsageLocationSetter), null, MbxRecipientSchema.UsageLocation);

		// Token: 0x04000F64 RID: 3940
		public static readonly ADPropertyDefinition DefaultPublicFolderMailbox = new ADPropertyDefinition("DefaultPublicFolderMailbox", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchPublicFolderMailbox", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000F65 RID: 3941
		public static readonly ADPropertyDefinition DefaultPublicFolderMailboxSmtpAddress = new ADPropertyDefinition("DefaultPublicFolderMailboxSmtpAddress", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchPublicFolderSmtpAddress", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000F66 RID: 3942
		public static readonly ADPropertyDefinition IndexedPhoneNumbers = new ADPropertyDefinition("IndexedPhoneNumbers", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADRecipientSchema.UMDtmfMap
		}, new CustomFilterBuilderDelegate(ADOrgPerson.IndexedPhoneNumbersGetterFilterBuilder), new GetterDelegate(ADOrgPerson.IndexedPhoneNumbersGetter), null, null, null);

		// Token: 0x02000269 RID: 617
		private static class SCLHelperDefinitions
		{
			// Token: 0x04000F89 RID: 3977
			public const int InvalidSCLThreshold = 15;

			// Token: 0x04000F8A RID: 3978
			public const int EnabledState = -2147483648;

			// Token: 0x04000F8B RID: 3979
			public const int DisabledState = 0;

			// Token: 0x0200026A RID: 618
			public static class Masks
			{
				// Token: 0x04000F8C RID: 3980
				public const int Threshold = 15;

				// Token: 0x04000F8D RID: 3981
				public const int State = -2147483648;
			}

			// Token: 0x0200026B RID: 619
			public static class DefaultThresholds
			{
				// Token: 0x04000F8E RID: 3982
				public const int Delete = 9;

				// Token: 0x04000F8F RID: 3983
				public const int Reject = 7;

				// Token: 0x04000F90 RID: 3984
				public const int Quarantine = 9;

				// Token: 0x04000F91 RID: 3985
				public const int Junk = 4;
			}
		}

		// Token: 0x0200026C RID: 620
		public static class LegalHoldStrings
		{
			// Token: 0x04000F92 RID: 3986
			public static string LitigationHoldDurationString = "LHD";

			// Token: 0x04000F93 RID: 3987
			public static string UnlimitedString = "Unlimit";
		}
	}
}
