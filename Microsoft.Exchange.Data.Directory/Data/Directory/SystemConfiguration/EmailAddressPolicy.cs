using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200043B RID: 1083
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class EmailAddressPolicy : ADLegacyVersionableObject, ISupportRecipientFilter
	{
		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x000C4F36 File Offset: 0x000C3136
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchRecipientPolicy";
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x000C4F3D File Offset: 0x000C313D
		internal override ADObjectSchema Schema
		{
			get
			{
				return EmailAddressPolicy.schema;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060030CC RID: 12492 RVA: 0x000C4F44 File Offset: 0x000C3144
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000C4F48 File Offset: 0x000C3148
		internal override void StampPersistableDefaultValues()
		{
			base.StampPersistableDefaultValues();
			if (!base.IsModified(EmailAddressPolicySchema.PolicyOptionListValue))
			{
				this[EmailAddressPolicySchema.PolicyOptionListValue] = new MultiValuedProperty<byte[]>(new object[]
				{
					EmailAddressPolicy.PolicyGuid.ToByteArray()
				});
			}
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000C4F90 File Offset: 0x000C3190
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			ProxyAddressTemplateCollection proxyAddressTemplateCollection = (ProxyAddressTemplateCollection)this[EmailAddressPolicySchema.RawEnabledEmailAddressTemplates];
			ProxyAddressTemplateCollection disabledEmailAddressTemplates = this.DisabledEmailAddressTemplates;
			List<ProxyAddressTemplate> list = new List<ProxyAddressTemplate>();
			list.AddRange(proxyAddressTemplateCollection);
			list.AddRange(disabledEmailAddressTemplates);
			if (list.Count > 0)
			{
				if (proxyAddressTemplateCollection != null && disabledEmailAddressTemplates != null)
				{
					foreach (ProxyAddressTemplate proxyAddressTemplate in proxyAddressTemplateCollection)
					{
						if (disabledEmailAddressTemplates.Contains(proxyAddressTemplate))
						{
							errors.Add(new ObjectValidationError(DirectoryStrings.EapDuplicatedEmailAddressTemplate(proxyAddressTemplate.ToString()), base.Id, string.Empty));
						}
					}
				}
				Dictionary<ProxyAddressPrefix, int> dictionary = new Dictionary<ProxyAddressPrefix, int>();
				foreach (ProxyAddressTemplate proxyAddressTemplate2 in list)
				{
					if (!dictionary.ContainsKey(proxyAddressTemplate2.Prefix))
					{
						dictionary[proxyAddressTemplate2.Prefix] = 0;
					}
					if (proxyAddressTemplate2.IsPrimaryAddress)
					{
						Dictionary<ProxyAddressPrefix, int> dictionary2;
						ProxyAddressPrefix prefix;
						(dictionary2 = dictionary)[prefix = proxyAddressTemplate2.Prefix] = dictionary2[prefix] + 1;
					}
				}
				foreach (ProxyAddressPrefix proxyAddressPrefix in dictionary.Keys)
				{
					if ((!(proxyAddressPrefix == ProxyAddressPrefix.Smtp) || dictionary[proxyAddressPrefix] != 0) && dictionary[proxyAddressPrefix] != 1)
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.EapMustHaveOnePrimaryAddressTemplate(proxyAddressPrefix.ToString()), base.Id, string.Empty));
					}
				}
			}
			ValidationError validationError = RecipientFilterHelper.ValidatePrecannedRecipientFilter(this.propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.IncludedRecipients, this.Identity);
			if (validationError != null)
			{
				errors.Add(validationError);
			}
			if (this.Priority == 0 && (base.IsChanged(EmailAddressPolicySchema.Priority) || base.ObjectState == ObjectState.New))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.CannotSetZeroAsEapPriority, EmailAddressPolicySchema.Priority, string.Empty));
			}
			if (string.IsNullOrEmpty(this.RecipientFilter) && (base.IsModified(EmailAddressPolicySchema.RecipientFilter) || base.ObjectState == ObjectState.New))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorInvalidOpathFilter(this.RecipientFilter ?? string.Empty), EmailAddressPolicySchema.RecipientFilter, string.Empty));
			}
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000C573C File Offset: 0x000C393C
		internal IEnumerable<ADRecipient> FindMatchingRecipientsPaged(IRecipientSession recipientSession, OrganizationId organizationId, ADObjectId rootId, bool fixMissingAlias)
		{
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			if (organizationId != OrganizationId.ForestWideOrgId)
			{
			}
			bool isSearch = true;
			List<QueryFilter> filters = new List<QueryFilter>();
			filters.Add(new ExistsFilter(ADRecipientSchema.Alias));
			if (!string.IsNullOrEmpty(this.LdapRecipientFilter))
			{
				filters.Add(new CustomLdapFilter(this.LdapRecipientFilter));
			}
			QueryFilter filter = null;
			if (fixMissingAlias)
			{
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ADRecipientSchema.Alias)),
					new OrFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchDynamicDistributionList"),
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "publicFolder"),
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchPublicMDB"),
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchSystemMailbox"),
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchExchangeServerRecipient"),
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "exchangeAdminService")
					})
				});
				if (string.IsNullOrEmpty(this.LdapRecipientFilter))
				{
					filter = queryFilter;
				}
				else
				{
					filter = new OrFilter(new QueryFilter[]
					{
						new AndFilter(filters.ToArray()),
						queryFilter
					});
				}
			}
			else if (!string.IsNullOrEmpty(this.LdapRecipientFilter))
			{
				filter = new AndFilter(filters.ToArray());
			}
			else
			{
				isSearch = false;
			}
			if (isSearch)
			{
				ADPagedReader<ADRecipient> pagedReader = recipientSession.FindPaged(rootId ?? this.RecipientContainer, QueryScope.SubTree, filter, null, 0);
				foreach (ADRecipient item in pagedReader)
				{
					yield return item;
				}
				if (organizationId != OrganizationId.ForestWideOrgId && this.RecipientContainer == null && rootId == null)
				{
					bool originalUseConfigNC = recipientSession.UseConfigNC;
					recipientSession.UseConfigNC = !originalUseConfigNC;
					try
					{
						pagedReader = recipientSession.FindPaged(null, QueryScope.SubTree, filter, null, 0);
						foreach (ADRecipient item2 in pagedReader)
						{
							yield return item2;
						}
					}
					finally
					{
						recipientSession.UseConfigNC = originalUseConfigNC;
					}
				}
			}
			yield break;
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060030D1 RID: 12497 RVA: 0x000C5776 File Offset: 0x000C3976
		public string RecipientFilter
		{
			get
			{
				return (string)this[EmailAddressPolicySchema.RecipientFilter];
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000C5788 File Offset: 0x000C3988
		public string LdapRecipientFilter
		{
			get
			{
				return (string)this[EmailAddressPolicySchema.LdapRecipientFilter];
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000C579A File Offset: 0x000C399A
		public string LastUpdatedRecipientFilter
		{
			get
			{
				return (string)this[EmailAddressPolicySchema.LastUpdatedRecipientFilter];
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x000C57AC File Offset: 0x000C39AC
		// (set) Token: 0x060030D5 RID: 12501 RVA: 0x000C57BE File Offset: 0x000C39BE
		public bool RecipientFilterApplied
		{
			get
			{
				return (bool)this[EmailAddressPolicySchema.RecipientFilterApplied];
			}
			internal set
			{
				this[EmailAddressPolicySchema.RecipientFilterApplied] = value;
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x000C57D1 File Offset: 0x000C39D1
		// (set) Token: 0x060030D7 RID: 12503 RVA: 0x000C57E3 File Offset: 0x000C39E3
		[Parameter]
		public WellKnownRecipientType? IncludedRecipients
		{
			get
			{
				return (WellKnownRecipientType?)this[EmailAddressPolicySchema.IncludedRecipients];
			}
			set
			{
				this[EmailAddressPolicySchema.IncludedRecipients] = value;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x000C57F6 File Offset: 0x000C39F6
		// (set) Token: 0x060030D9 RID: 12505 RVA: 0x000C5808 File Offset: 0x000C3A08
		[Parameter]
		public MultiValuedProperty<string> ConditionalDepartment
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalDepartment];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalDepartment] = value;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x000C5816 File Offset: 0x000C3A16
		// (set) Token: 0x060030DB RID: 12507 RVA: 0x000C5828 File Offset: 0x000C3A28
		[Parameter]
		public MultiValuedProperty<string> ConditionalCompany
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCompany];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCompany] = value;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x000C5836 File Offset: 0x000C3A36
		// (set) Token: 0x060030DD RID: 12509 RVA: 0x000C5848 File Offset: 0x000C3A48
		[Parameter]
		public MultiValuedProperty<string> ConditionalStateOrProvince
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalStateOrProvince];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalStateOrProvince] = value;
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x060030DE RID: 12510 RVA: 0x000C5856 File Offset: 0x000C3A56
		// (set) Token: 0x060030DF RID: 12511 RVA: 0x000C5868 File Offset: 0x000C3A68
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute1
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute1];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute1] = value;
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060030E0 RID: 12512 RVA: 0x000C5876 File Offset: 0x000C3A76
		// (set) Token: 0x060030E1 RID: 12513 RVA: 0x000C5888 File Offset: 0x000C3A88
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute2
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute2];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute2] = value;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x000C5896 File Offset: 0x000C3A96
		// (set) Token: 0x060030E3 RID: 12515 RVA: 0x000C58A8 File Offset: 0x000C3AA8
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute3
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute3];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute3] = value;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x000C58B6 File Offset: 0x000C3AB6
		// (set) Token: 0x060030E5 RID: 12517 RVA: 0x000C58C8 File Offset: 0x000C3AC8
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute4
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute4];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute4] = value;
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x000C58D6 File Offset: 0x000C3AD6
		// (set) Token: 0x060030E7 RID: 12519 RVA: 0x000C58E8 File Offset: 0x000C3AE8
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute5
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute5];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute5] = value;
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x000C58F6 File Offset: 0x000C3AF6
		// (set) Token: 0x060030E9 RID: 12521 RVA: 0x000C5908 File Offset: 0x000C3B08
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute6
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute6];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute6] = value;
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x000C5916 File Offset: 0x000C3B16
		// (set) Token: 0x060030EB RID: 12523 RVA: 0x000C5928 File Offset: 0x000C3B28
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute7
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute7];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute7] = value;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x000C5936 File Offset: 0x000C3B36
		// (set) Token: 0x060030ED RID: 12525 RVA: 0x000C5948 File Offset: 0x000C3B48
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute8
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute8];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute8] = value;
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x000C5956 File Offset: 0x000C3B56
		// (set) Token: 0x060030EF RID: 12527 RVA: 0x000C5968 File Offset: 0x000C3B68
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute9
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute9];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute9] = value;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x000C5976 File Offset: 0x000C3B76
		// (set) Token: 0x060030F1 RID: 12529 RVA: 0x000C5988 File Offset: 0x000C3B88
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute10
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute10];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute10] = value;
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000C5996 File Offset: 0x000C3B96
		// (set) Token: 0x060030F3 RID: 12531 RVA: 0x000C59A8 File Offset: 0x000C3BA8
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute11
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute11];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute11] = value;
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x060030F4 RID: 12532 RVA: 0x000C59B6 File Offset: 0x000C3BB6
		// (set) Token: 0x060030F5 RID: 12533 RVA: 0x000C59C8 File Offset: 0x000C3BC8
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute12
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute12];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute12] = value;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x000C59D6 File Offset: 0x000C3BD6
		// (set) Token: 0x060030F7 RID: 12535 RVA: 0x000C59E8 File Offset: 0x000C3BE8
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute13
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute13];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute13] = value;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x000C59F6 File Offset: 0x000C3BF6
		// (set) Token: 0x060030F9 RID: 12537 RVA: 0x000C5A08 File Offset: 0x000C3C08
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute14
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute14];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute14] = value;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x000C5A16 File Offset: 0x000C3C16
		// (set) Token: 0x060030FB RID: 12539 RVA: 0x000C5A28 File Offset: 0x000C3C28
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute15
		{
			get
			{
				return (MultiValuedProperty<string>)this[EmailAddressPolicySchema.ConditionalCustomAttribute15];
			}
			set
			{
				this[EmailAddressPolicySchema.ConditionalCustomAttribute15] = value;
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x000C5A36 File Offset: 0x000C3C36
		// (set) Token: 0x060030FD RID: 12541 RVA: 0x000C5A48 File Offset: 0x000C3C48
		public ADObjectId RecipientContainer
		{
			get
			{
				return (ADObjectId)this[EmailAddressPolicySchema.RecipientContainer];
			}
			set
			{
				this[EmailAddressPolicySchema.RecipientContainer] = value;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000C5A56 File Offset: 0x000C3C56
		public WellKnownRecipientFilterType RecipientFilterType
		{
			get
			{
				return (WellKnownRecipientFilterType)this[EmailAddressPolicySchema.RecipientFilterType];
			}
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000C5A68 File Offset: 0x000C3C68
		internal void SetRecipientFilter(QueryFilter filter)
		{
			if (filter == null)
			{
				this[EmailAddressPolicySchema.RecipientFilter] = string.Empty;
				this[EmailAddressPolicySchema.LdapRecipientFilter] = string.Empty;
			}
			else
			{
				this[EmailAddressPolicySchema.RecipientFilter] = filter.GenerateInfixString(FilterLanguage.Monad);
				this[EmailAddressPolicySchema.LdapRecipientFilter] = LdapFilterBuilder.LdapFilterFromQueryFilter(filter);
			}
			RecipientFilterHelper.SetRecipientFilterType(WellKnownRecipientFilterType.Custom, this.propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata);
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000C5ACE File Offset: 0x000C3CCE
		// (set) Token: 0x06003101 RID: 12545 RVA: 0x000C5AE0 File Offset: 0x000C3CE0
		[Parameter]
		public EmailAddressPolicyPriority Priority
		{
			get
			{
				return (EmailAddressPolicyPriority)this[EmailAddressPolicySchema.Priority];
			}
			set
			{
				this[EmailAddressPolicySchema.Priority] = value;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000C5AF3 File Offset: 0x000C3CF3
		// (set) Token: 0x06003103 RID: 12547 RVA: 0x000C5B05 File Offset: 0x000C3D05
		[Parameter]
		public string EnabledPrimarySMTPAddressTemplate
		{
			get
			{
				return (string)this[EmailAddressPolicySchema.EnabledPrimarySMTPAddressTemplate];
			}
			set
			{
				this[EmailAddressPolicySchema.EnabledPrimarySMTPAddressTemplate] = value;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000C5B13 File Offset: 0x000C3D13
		// (set) Token: 0x06003105 RID: 12549 RVA: 0x000C5B25 File Offset: 0x000C3D25
		[Parameter]
		public ProxyAddressTemplateCollection EnabledEmailAddressTemplates
		{
			get
			{
				return (ProxyAddressTemplateCollection)this[EmailAddressPolicySchema.EnabledEmailAddressTemplates];
			}
			set
			{
				this[EmailAddressPolicySchema.EnabledEmailAddressTemplates] = value;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000C5B33 File Offset: 0x000C3D33
		// (set) Token: 0x06003107 RID: 12551 RVA: 0x000C5B45 File Offset: 0x000C3D45
		[Parameter]
		public ProxyAddressTemplateCollection DisabledEmailAddressTemplates
		{
			get
			{
				return (ProxyAddressTemplateCollection)this[EmailAddressPolicySchema.DisabledEmailAddressTemplates];
			}
			set
			{
				this[EmailAddressPolicySchema.DisabledEmailAddressTemplates] = value;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000C5B53 File Offset: 0x000C3D53
		internal bool Enabled
		{
			get
			{
				return (bool)this[EmailAddressPolicySchema.Enabled];
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000C5B65 File Offset: 0x000C3D65
		public bool HasEmailAddressSetting
		{
			get
			{
				return (bool)this[EmailAddressPolicySchema.HasEmailAddressSetting];
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000C5B77 File Offset: 0x000C3D77
		public bool HasMailboxManagerSetting
		{
			get
			{
				return (bool)this[EmailAddressPolicySchema.HasMailboxManagerSetting];
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000C5B89 File Offset: 0x000C3D89
		// (set) Token: 0x0600310C RID: 12556 RVA: 0x000C5B9B File Offset: 0x000C3D9B
		public ProxyAddressTemplateCollection NonAuthoritativeDomains
		{
			get
			{
				return (ProxyAddressTemplateCollection)this[EmailAddressPolicySchema.NonAuthoritativeDomains];
			}
			internal set
			{
				this[EmailAddressPolicySchema.NonAuthoritativeDomains] = value;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x000C5BA9 File Offset: 0x000C3DA9
		public string AdminDescription
		{
			get
			{
				return (string)this[EmailAddressPolicySchema.AdminDescription];
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x000C5BBB File Offset: 0x000C3DBB
		ADPropertyDefinition ISupportRecipientFilter.RecipientFilterSchema
		{
			get
			{
				return EmailAddressPolicySchema.RecipientFilter;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x000C5BC2 File Offset: 0x000C3DC2
		ADPropertyDefinition ISupportRecipientFilter.LdapRecipientFilterSchema
		{
			get
			{
				return EmailAddressPolicySchema.LdapRecipientFilter;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000C5BC9 File Offset: 0x000C3DC9
		ADPropertyDefinition ISupportRecipientFilter.IncludedRecipientsSchema
		{
			get
			{
				return EmailAddressPolicySchema.IncludedRecipients;
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003111 RID: 12561 RVA: 0x000C5BD0 File Offset: 0x000C3DD0
		ADPropertyDefinition ISupportRecipientFilter.ConditionalDepartmentSchema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalDepartment;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x000C5BD7 File Offset: 0x000C3DD7
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCompanySchema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCompany;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x000C5BDE File Offset: 0x000C3DDE
		ADPropertyDefinition ISupportRecipientFilter.ConditionalStateOrProvinceSchema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalStateOrProvince;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x000C5BE5 File Offset: 0x000C3DE5
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute1Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute1;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x000C5BEC File Offset: 0x000C3DEC
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute2Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute2;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x000C5BF3 File Offset: 0x000C3DF3
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute3Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute3;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x000C5BFA File Offset: 0x000C3DFA
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute4Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute4;
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x000C5C01 File Offset: 0x000C3E01
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute5Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute5;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x000C5C08 File Offset: 0x000C3E08
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute6Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute6;
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x000C5C0F File Offset: 0x000C3E0F
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute7Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute7;
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x000C5C16 File Offset: 0x000C3E16
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute8Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute8;
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x000C5C1D File Offset: 0x000C3E1D
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute9Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute9;
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x000C5C24 File Offset: 0x000C3E24
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute10Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute10;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x000C5C2B File Offset: 0x000C3E2B
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute11Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute11;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x000C5C32 File Offset: 0x000C3E32
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute12Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute12;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000C5C39 File Offset: 0x000C3E39
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute13Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute13;
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x000C5C40 File Offset: 0x000C3E40
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute14Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute14;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x000C5C47 File Offset: 0x000C3E47
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute15Schema
		{
			get
			{
				return EmailAddressPolicySchema.ConditionalCustomAttribute15;
			}
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000C5C50 File Offset: 0x000C3E50
		internal static object EnabledEmailAddressTemplatesGetter(IPropertyBag propertyBag)
		{
			MultiValuedPropertyBase multiValuedPropertyBase = (MultiValuedPropertyBase)propertyBag[EmailAddressPolicySchema.RawEnabledEmailAddressTemplates];
			return multiValuedPropertyBase.Clone();
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000C5C74 File Offset: 0x000C3E74
		internal static void EnabledEmailAddressTemplatesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[EmailAddressPolicySchema.RawEnabledEmailAddressTemplates] = value;
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000C5C84 File Offset: 0x000C3E84
		internal static object EnabledPrimarySMTPAddressTemplateGetter(IPropertyBag propertyBag)
		{
			ProxyAddressTemplateCollection proxyAddressTemplateCollection = (ProxyAddressTemplateCollection)propertyBag[EmailAddressPolicySchema.RawEnabledEmailAddressTemplates];
			ProxyAddressTemplate proxyAddressTemplate = proxyAddressTemplateCollection.FindPrimary(ProxyAddressPrefix.Smtp);
			if (!(null == proxyAddressTemplate))
			{
				return proxyAddressTemplate.AddressTemplateString;
			}
			return string.Empty;
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000C5CC4 File Offset: 0x000C3EC4
		internal static void EnabledPrimarySMTPAddressTemplateSetter(object value, IPropertyBag propertyBag)
		{
			ProxyAddressTemplateCollection proxyAddressTemplateCollection = (ProxyAddressTemplateCollection)propertyBag[EmailAddressPolicySchema.RawEnabledEmailAddressTemplates];
			ProxyAddressTemplate proxyAddressTemplate = null;
			try
			{
				proxyAddressTemplate = ProxyAddressTemplate.Parse((string)value);
			}
			catch (ArgumentException ex)
			{
				throw new DataValidationException(new PropertyValidationError(new LocalizedString(ex.Message), EmailAddressPolicySchema.EnabledPrimarySMTPAddressTemplate, null), ex);
			}
			if (!(proxyAddressTemplate is SmtpProxyAddressTemplate))
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ErrorPrimarySmtpTemplateInvalid((string)value), EmailAddressPolicySchema.EnabledPrimarySMTPAddressTemplate, value), null);
			}
			ProxyAddressTemplate proxyAddressTemplate2 = proxyAddressTemplateCollection.FindPrimary(ProxyAddressPrefix.Smtp);
			if (proxyAddressTemplate2 != null && !StringComparer.CurrentCultureIgnoreCase.Equals(proxyAddressTemplate.AddressTemplateString, proxyAddressTemplate2.AddressTemplateString))
			{
				proxyAddressTemplateCollection.Remove(proxyAddressTemplate2);
			}
			proxyAddressTemplateCollection.MakePrimary(proxyAddressTemplate);
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000C5D84 File Offset: 0x000C3F84
		internal static bool IsOfPolicyType(MultiValuedProperty<byte[]> values, Guid policyGuid)
		{
			if (values != null)
			{
				byte[] array = policyGuid.ToByteArray();
				foreach (byte[] array2 in values)
				{
					if (array2.Length == array.Length)
					{
						bool flag = true;
						int num = 0;
						while (array.Length > num)
						{
							if (array2[num] != array[num])
							{
								flag = false;
								break;
							}
							num++;
						}
						if (flag)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x040020FF RID: 8447
		private const string MostDerivedObjectClassInternal = "msExchRecipientPolicy";

		// Token: 0x04002100 RID: 8448
		public static readonly string DefaultName = "Default Policy";

		// Token: 0x04002101 RID: 8449
		internal static readonly Guid MailboxSettingPolicyGuid = new Guid("3b6813ec-ce89-42ba-9442-d87d4aa30dbc");

		// Token: 0x04002102 RID: 8450
		internal static readonly Guid PolicyGuid = new Guid("26491CFC-9E50-4857-861B-0CB8DF22B5D7");

		// Token: 0x04002103 RID: 8451
		public static readonly IComparer<EmailAddressPolicy> PriorityComparer = new EmailAddressPolicy.InternalPriorityComparer();

		// Token: 0x04002104 RID: 8452
		private static EmailAddressPolicySchema schema = ObjectSchema.GetInstance<EmailAddressPolicySchema>();

		// Token: 0x04002105 RID: 8453
		public static QueryFilter RecipientFilterForDefaultPolicy = new ExistsFilter(ADRecipientSchema.Alias);

		// Token: 0x04002106 RID: 8454
		public static readonly ADObjectId RdnEapContainerToOrganization = new ADObjectId("CN=Recipient Policies");

		// Token: 0x0200043C RID: 1084
		private class InternalPriorityComparer : IComparer, IComparer<EmailAddressPolicy>
		{
			// Token: 0x06003129 RID: 12585 RVA: 0x000C5E6F File Offset: 0x000C406F
			int IComparer.Compare(object x, object y)
			{
				return ((IComparer<EmailAddressPolicy>)this).Compare((EmailAddressPolicy)x, (EmailAddressPolicy)y);
			}

			// Token: 0x0600312A RID: 12586 RVA: 0x000C5E83 File Offset: 0x000C4083
			int IComparer<EmailAddressPolicy>.Compare(EmailAddressPolicy x, EmailAddressPolicy y)
			{
				if (x == null)
				{
					if (y != null)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (y != null)
					{
						return x.Priority - y.Priority;
					}
					return 1;
				}
			}
		}
	}
}
