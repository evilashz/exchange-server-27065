using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000733 RID: 1843
	[Serializable]
	public class MailboxPlan : Mailbox
	{
		// Token: 0x17001DEC RID: 7660
		// (get) Token: 0x06005834 RID: 22580 RVA: 0x0013A6F7 File Offset: 0x001388F7
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return Microsoft.Exchange.Data.Directory.Management.MailboxPlan.schema;
			}
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x0013A6FE File Offset: 0x001388FE
		public MailboxPlan()
		{
			base.SetObjectClass("user");
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x0013A711 File Offset: 0x00138911
		public MailboxPlan(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x0013A71A File Offset: 0x0013891A
		internal new static MailboxPlan FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new MailboxPlan(dataObject);
		}

		// Token: 0x17001DED RID: 7661
		// (get) Token: 0x06005838 RID: 22584 RVA: 0x0013A727 File Offset: 0x00138927
		// (set) Token: 0x06005839 RID: 22585 RVA: 0x0013A739 File Offset: 0x00138939
		public bool IsDefault
		{
			get
			{
				return (bool)this[MailboxPlanSchema.IsDefault];
			}
			internal set
			{
				this[MailboxPlanSchema.IsDefault] = value;
			}
		}

		// Token: 0x17001DEE RID: 7662
		// (get) Token: 0x0600583A RID: 22586 RVA: 0x0013A74C File Offset: 0x0013894C
		// (set) Token: 0x0600583B RID: 22587 RVA: 0x0013A75E File Offset: 0x0013895E
		public bool IsDefaultForPreviousVersion
		{
			get
			{
				return (bool)this[MailboxPlanSchema.IsDefault_R3];
			}
			internal set
			{
				this[MailboxPlanSchema.IsDefault_R3] = value;
			}
		}

		// Token: 0x17001DEF RID: 7663
		// (get) Token: 0x0600583C RID: 22588 RVA: 0x0013A771 File Offset: 0x00138971
		// (set) Token: 0x0600583D RID: 22589 RVA: 0x0013A783 File Offset: 0x00138983
		public MailboxPlanRelease MailboxPlanRelease
		{
			get
			{
				return (MailboxPlanRelease)this[MailboxPlanSchema.MailboxPlanRelease];
			}
			internal set
			{
				this[MailboxPlanSchema.MailboxPlanRelease] = value;
			}
		}

		// Token: 0x17001DF0 RID: 7664
		// (get) Token: 0x0600583E RID: 22590 RVA: 0x0013A796 File Offset: 0x00138996
		// (set) Token: 0x0600583F RID: 22591 RVA: 0x0013A7A8 File Offset: 0x001389A8
		public bool IsPilotMailboxPlan
		{
			get
			{
				return (bool)this[MailboxPlanSchema.IsPilotMailboxPlan];
			}
			internal set
			{
				this[MailboxPlanSchema.IsPilotMailboxPlan] = value;
			}
		}

		// Token: 0x17001DF1 RID: 7665
		// (get) Token: 0x06005840 RID: 22592 RVA: 0x0013A7BB File Offset: 0x001389BB
		internal new MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFrom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DF2 RID: 7666
		// (get) Token: 0x06005841 RID: 22593 RVA: 0x0013A7BE File Offset: 0x001389BE
		internal new MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFromDLMembers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DF3 RID: 7667
		// (get) Token: 0x06005842 RID: 22594 RVA: 0x0013A7C1 File Offset: 0x001389C1
		internal new MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFromSendersOrMembers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DF4 RID: 7668
		// (get) Token: 0x06005843 RID: 22595 RVA: 0x0013A7C4 File Offset: 0x001389C4
		internal new ADObjectId ArbitrationMailbox
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DF5 RID: 7669
		// (get) Token: 0x06005844 RID: 22596 RVA: 0x0013A7C7 File Offset: 0x001389C7
		internal new MultiValuedProperty<ADObjectId> BypassModerationFromSendersOrMembers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DF6 RID: 7670
		// (get) Token: 0x06005845 RID: 22597 RVA: 0x0013A7CA File Offset: 0x001389CA
		internal new string CustomAttribute10
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DF7 RID: 7671
		// (get) Token: 0x06005846 RID: 22598 RVA: 0x0013A7CD File Offset: 0x001389CD
		internal new string CustomAttribute11
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DF8 RID: 7672
		// (get) Token: 0x06005847 RID: 22599 RVA: 0x0013A7D0 File Offset: 0x001389D0
		internal new string CustomAttribute12
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DF9 RID: 7673
		// (get) Token: 0x06005848 RID: 22600 RVA: 0x0013A7D3 File Offset: 0x001389D3
		internal new string CustomAttribute13
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DFA RID: 7674
		// (get) Token: 0x06005849 RID: 22601 RVA: 0x0013A7D6 File Offset: 0x001389D6
		internal new string CustomAttribute14
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DFB RID: 7675
		// (get) Token: 0x0600584A RID: 22602 RVA: 0x0013A7D9 File Offset: 0x001389D9
		internal new string CustomAttribute15
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DFC RID: 7676
		// (get) Token: 0x0600584B RID: 22603 RVA: 0x0013A7DC File Offset: 0x001389DC
		internal new string CustomAttribute2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DFD RID: 7677
		// (get) Token: 0x0600584C RID: 22604 RVA: 0x0013A7DF File Offset: 0x001389DF
		internal new string CustomAttribute3
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DFE RID: 7678
		// (get) Token: 0x0600584D RID: 22605 RVA: 0x0013A7E2 File Offset: 0x001389E2
		internal new string CustomAttribute4
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001DFF RID: 7679
		// (get) Token: 0x0600584E RID: 22606 RVA: 0x0013A7E5 File Offset: 0x001389E5
		internal new string CustomAttribute5
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E00 RID: 7680
		// (get) Token: 0x0600584F RID: 22607 RVA: 0x0013A7E8 File Offset: 0x001389E8
		internal new string CustomAttribute6
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E01 RID: 7681
		// (get) Token: 0x06005850 RID: 22608 RVA: 0x0013A7EB File Offset: 0x001389EB
		internal new string CustomAttribute7
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E02 RID: 7682
		// (get) Token: 0x06005851 RID: 22609 RVA: 0x0013A7EE File Offset: 0x001389EE
		internal new string CustomAttribute8
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E03 RID: 7683
		// (get) Token: 0x06005852 RID: 22610 RVA: 0x0013A7F1 File Offset: 0x001389F1
		internal new string CustomAttribute9
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E04 RID: 7684
		// (get) Token: 0x06005853 RID: 22611 RVA: 0x0013A7F4 File Offset: 0x001389F4
		internal new MultiValuedProperty<string> ExtensionCustomAttribute1
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E05 RID: 7685
		// (get) Token: 0x06005854 RID: 22612 RVA: 0x0013A7F7 File Offset: 0x001389F7
		internal new MultiValuedProperty<string> ExtensionCustomAttribute2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E06 RID: 7686
		// (get) Token: 0x06005855 RID: 22613 RVA: 0x0013A7FA File Offset: 0x001389FA
		internal new MultiValuedProperty<string> ExtensionCustomAttribute3
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E07 RID: 7687
		// (get) Token: 0x06005856 RID: 22614 RVA: 0x0013A7FD File Offset: 0x001389FD
		internal new MultiValuedProperty<string> ExtensionCustomAttribute4
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E08 RID: 7688
		// (get) Token: 0x06005857 RID: 22615 RVA: 0x0013A800 File Offset: 0x00138A00
		internal new MultiValuedProperty<string> ExtensionCustomAttribute5
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E09 RID: 7689
		// (get) Token: 0x06005858 RID: 22616 RVA: 0x0013A803 File Offset: 0x00138A03
		internal new ProxyAddressCollection EmailAddresses
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E0A RID: 7690
		// (get) Token: 0x06005859 RID: 22617 RVA: 0x0013A806 File Offset: 0x00138A06
		internal new ADObjectId ForwardingAddress
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E0B RID: 7691
		// (get) Token: 0x0600585A RID: 22618 RVA: 0x0013A809 File Offset: 0x00138A09
		internal new ProxyAddress ForwardingSmtpAddress
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E0C RID: 7692
		// (get) Token: 0x0600585B RID: 22619 RVA: 0x0013A80C File Offset: 0x00138A0C
		internal new MultiValuedProperty<ADObjectId> GrantSendOnBehalfTo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E0D RID: 7693
		// (get) Token: 0x0600585C RID: 22620 RVA: 0x0013A80F File Offset: 0x00138A0F
		internal new MultiValuedProperty<CultureInfo> Languages
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E0E RID: 7694
		// (get) Token: 0x0600585D RID: 22621 RVA: 0x0013A812 File Offset: 0x00138A12
		internal new string LinkedMasterAccount
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E0F RID: 7695
		// (get) Token: 0x0600585E RID: 22622 RVA: 0x0013A815 File Offset: 0x00138A15
		internal new string MailTip
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E10 RID: 7696
		// (get) Token: 0x0600585F RID: 22623 RVA: 0x0013A818 File Offset: 0x00138A18
		internal new MultiValuedProperty<string> MailTipTranslations
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E11 RID: 7697
		// (get) Token: 0x06005860 RID: 22624 RVA: 0x0013A81B File Offset: 0x00138A1B
		internal new bool ModerationEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001E12 RID: 7698
		// (get) Token: 0x06005861 RID: 22625 RVA: 0x0013A81E File Offset: 0x00138A1E
		internal new string Office
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E13 RID: 7699
		// (get) Token: 0x06005862 RID: 22626 RVA: 0x0013A821 File Offset: 0x00138A21
		internal new SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return base.PrimarySmtpAddress;
			}
		}

		// Token: 0x17001E14 RID: 7700
		// (get) Token: 0x06005863 RID: 22627 RVA: 0x0013A829 File Offset: 0x00138A29
		internal new MultiValuedProperty<ADObjectId> RejectMessagesFrom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E15 RID: 7701
		// (get) Token: 0x06005864 RID: 22628 RVA: 0x0013A82C File Offset: 0x00138A2C
		internal new MultiValuedProperty<ADObjectId> RejectMessagesFromDLMembers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E16 RID: 7702
		// (get) Token: 0x06005865 RID: 22629 RVA: 0x0013A82F File Offset: 0x00138A2F
		internal new MultiValuedProperty<ADObjectId> RejectMessagesFromSendersOrMembers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E17 RID: 7703
		// (get) Token: 0x06005866 RID: 22630 RVA: 0x0013A834 File Offset: 0x00138A34
		internal new int? ResourceCapacity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E18 RID: 7704
		// (get) Token: 0x06005867 RID: 22631 RVA: 0x0013A84A File Offset: 0x00138A4A
		internal new MultiValuedProperty<string> ResourceCustom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E19 RID: 7705
		// (get) Token: 0x06005868 RID: 22632 RVA: 0x0013A84D File Offset: 0x00138A4D
		internal new string SamAccountName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E1A RID: 7706
		// (get) Token: 0x06005869 RID: 22633 RVA: 0x0013A850 File Offset: 0x00138A50
		internal new TransportModerationNotificationFlags SendModerationNotifications
		{
			get
			{
				return base.SendModerationNotifications;
			}
		}

		// Token: 0x17001E1B RID: 7707
		// (get) Token: 0x0600586A RID: 22634 RVA: 0x0013A858 File Offset: 0x00138A58
		internal new string SimpleDisplayName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E1C RID: 7708
		// (get) Token: 0x0600586B RID: 22635 RVA: 0x0013A85B File Offset: 0x00138A5B
		internal new MultiValuedProperty<string> UMDtmfMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E1D RID: 7709
		// (get) Token: 0x0600586C RID: 22636 RVA: 0x0013A85E File Offset: 0x00138A5E
		internal new SmtpAddress WindowsEmailAddress
		{
			get
			{
				return base.WindowsEmailAddress;
			}
		}

		// Token: 0x17001E1E RID: 7710
		// (get) Token: 0x0600586D RID: 22637 RVA: 0x0013A866 File Offset: 0x00138A66
		internal new SmtpAddress WindowsLiveID
		{
			get
			{
				return base.WindowsLiveID;
			}
		}

		// Token: 0x17001E1F RID: 7711
		// (get) Token: 0x0600586E RID: 22638 RVA: 0x0013A86E File Offset: 0x00138A6E
		internal new SmtpAddress MicrosoftOnlineServicesID
		{
			get
			{
				return base.MicrosoftOnlineServicesID;
			}
		}

		// Token: 0x17001E20 RID: 7712
		// (get) Token: 0x0600586F RID: 22639 RVA: 0x0013A876 File Offset: 0x00138A76
		internal new string ImmutableId
		{
			get
			{
				return base.ImmutableId;
			}
		}

		// Token: 0x17001E21 RID: 7713
		// (get) Token: 0x06005870 RID: 22640 RVA: 0x0013A87E File Offset: 0x00138A7E
		internal new bool? SKUAssigned
		{
			get
			{
				return new bool?(false);
			}
		}

		// Token: 0x17001E22 RID: 7714
		// (get) Token: 0x06005871 RID: 22641 RVA: 0x0013A886 File Offset: 0x00138A86
		private new CountryInfo UsageLocation
		{
			get
			{
				return base.UsageLocation;
			}
		}

		// Token: 0x17001E23 RID: 7715
		// (get) Token: 0x06005872 RID: 22642 RVA: 0x0013A88E File Offset: 0x00138A8E
		// (set) Token: 0x06005873 RID: 22643 RVA: 0x0013A8A0 File Offset: 0x00138AA0
		public string MailboxPlanIndex
		{
			get
			{
				return (string)this[MailboxPlanSchema.MailboxPlanIndex];
			}
			internal set
			{
				this[MailboxPlanSchema.MailboxPlanIndex] = value;
			}
		}

		// Token: 0x17001E24 RID: 7716
		// (get) Token: 0x06005874 RID: 22644 RVA: 0x0013A8AE File Offset: 0x00138AAE
		// (set) Token: 0x06005875 RID: 22645 RVA: 0x0013A8C0 File Offset: 0x00138AC0
		[Parameter(Mandatory = false)]
		public new bool HiddenFromAddressListsEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.HiddenFromAddressListsValue];
			}
			set
			{
				this[MailEnabledRecipientSchema.HiddenFromAddressListsEnabled] = value;
			}
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x0013A8D3 File Offset: 0x00138AD3
		internal static QueryFilter IsDefaultFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 8UL));
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x0013A8E7 File Offset: 0x00138AE7
		internal static QueryFilter IsDefault_R3_FilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 4UL));
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x0013A8FC File Offset: 0x00138AFC
		internal static object GetMailboxPlanRelease(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ADRecipientSchema.ProvisioningFlags];
			return (MailboxPlanRelease)(num & 48);
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x0013A924 File Offset: 0x00138B24
		internal static void SetMailboxPlanRelease(object value, IPropertyBag propertyBag)
		{
			ProvisioningFlagValues provisioningFlagValues = (ProvisioningFlagValues)value;
			int num = (int)propertyBag[ADRecipientSchema.ProvisioningFlags];
			num &= -49;
			propertyBag[ADRecipientSchema.ProvisioningFlags] = (num | (int)provisioningFlagValues);
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x0013A964 File Offset: 0x00138B64
		internal static QueryFilter MailboxPlanReleaseFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			if (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal && ComparisonOperator.NotEqual != comparisonFilter.ComparisonOperator)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
			if (!(comparisonFilter.PropertyValue is MailboxPlanRelease))
			{
				throw new ArgumentException("filter.PropertyValue");
			}
			MailboxPlanRelease mailboxPlanRelease = (MailboxPlanRelease)comparisonFilter.PropertyValue;
			if (mailboxPlanRelease != MailboxPlanRelease.AllReleases)
			{
				if (mailboxPlanRelease != MailboxPlanRelease.CurrentRelease)
				{
					if (mailboxPlanRelease != MailboxPlanRelease.NonCurrentRelease)
					{
						throw new ArgumentException("filter.PropertyValue");
					}
					if (comparisonFilter.ComparisonOperator == ComparisonOperator.Equal)
					{
						return Microsoft.Exchange.Data.Directory.Management.MailboxPlan.NonCurrentReleaseFilter;
					}
					return new NotFilter(Microsoft.Exchange.Data.Directory.Management.MailboxPlan.NonCurrentReleaseFilter);
				}
				else
				{
					if (comparisonFilter.ComparisonOperator == ComparisonOperator.Equal)
					{
						return Microsoft.Exchange.Data.Directory.Management.MailboxPlan.CurrentReleaseFilter;
					}
					return new NotFilter(Microsoft.Exchange.Data.Directory.Management.MailboxPlan.CurrentReleaseFilter);
				}
			}
			else
			{
				QueryFilter queryFilter = new OrFilter(new QueryFilter[]
				{
					Microsoft.Exchange.Data.Directory.Management.MailboxPlan.CurrentReleaseFilter,
					Microsoft.Exchange.Data.Directory.Management.MailboxPlan.NonCurrentReleaseFilter
				});
				if (ComparisonOperator.NotEqual == comparisonFilter.ComparisonOperator)
				{
					return queryFilter;
				}
				return new NotFilter(queryFilter);
			}
		}

		// Token: 0x04003B90 RID: 15248
		private static MailboxPlanSchema schema = ObjectSchema.GetInstance<MailboxPlanSchema>();

		// Token: 0x04003B91 RID: 15249
		internal static readonly BitMaskAndFilter CurrentReleaseFilter = new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 16UL);

		// Token: 0x04003B92 RID: 15250
		internal static readonly BitMaskAndFilter NonCurrentReleaseFilter = new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 32UL);
	}
}
