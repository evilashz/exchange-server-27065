using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000798 RID: 1944
	[Serializable]
	public class RecipientEnforcementProvisioningPolicy : EnforcementProvisioningPolicy
	{
		// Token: 0x170022A0 RID: 8864
		// (get) Token: 0x060060BC RID: 24764 RVA: 0x00148D2F File Offset: 0x00146F2F
		internal override ADObjectSchema Schema
		{
			get
			{
				return RecipientEnforcementProvisioningPolicy.schema;
			}
		}

		// Token: 0x170022A1 RID: 8865
		// (get) Token: 0x060060BD RID: 24765 RVA: 0x00148D36 File Offset: 0x00146F36
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RecipientEnforcementProvisioningPolicy.MostDerivedClass;
			}
		}

		// Token: 0x170022A2 RID: 8866
		// (get) Token: 0x060060BE RID: 24766 RVA: 0x00148D3D File Offset: 0x00146F3D
		internal override ICollection<Type> SupportedPresentationObjectTypes
		{
			get
			{
				return ProvisioningHelper.AllSupportedRecipientTypes;
			}
		}

		// Token: 0x170022A3 RID: 8867
		// (get) Token: 0x060060BF RID: 24767 RVA: 0x00148D44 File Offset: 0x00146F44
		internal override IEnumerable<IProvisioningEnforcement> ProvisioningEnforcementRules
		{
			get
			{
				return RecipientEnforcementProvisioningPolicy.provisioningEnforcements;
			}
		}

		// Token: 0x170022A4 RID: 8868
		// (get) Token: 0x060060C0 RID: 24768 RVA: 0x00148D4B File Offset: 0x00146F4B
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x00148D4E File Offset: 0x00146F4E
		public RecipientEnforcementProvisioningPolicy()
		{
			base.Name = "Recipient Quota Policy";
		}

		// Token: 0x170022A5 RID: 8869
		// (get) Token: 0x060060C2 RID: 24770 RVA: 0x00148D61 File Offset: 0x00146F61
		// (set) Token: 0x060060C3 RID: 24771 RVA: 0x00148D73 File Offset: 0x00146F73
		[Parameter(Mandatory = false)]
		public Unlimited<int> DistributionListCountQuota
		{
			get
			{
				return (Unlimited<int>)this[RecipientEnforcementProvisioningPolicySchema.DistributionListCountQuota];
			}
			set
			{
				this[RecipientEnforcementProvisioningPolicySchema.DistributionListCountQuota] = value;
			}
		}

		// Token: 0x170022A6 RID: 8870
		// (get) Token: 0x060060C4 RID: 24772 RVA: 0x00148D86 File Offset: 0x00146F86
		// (set) Token: 0x060060C5 RID: 24773 RVA: 0x00148D98 File Offset: 0x00146F98
		public int? DistributionListCount
		{
			get
			{
				return (int?)this[RecipientEnforcementProvisioningPolicySchema.DistributionListCount];
			}
			internal set
			{
				this[RecipientEnforcementProvisioningPolicySchema.DistributionListCount] = value;
			}
		}

		// Token: 0x170022A7 RID: 8871
		// (get) Token: 0x060060C6 RID: 24774 RVA: 0x00148DAB File Offset: 0x00146FAB
		// (set) Token: 0x060060C7 RID: 24775 RVA: 0x00148DBD File Offset: 0x00146FBD
		[Parameter(Mandatory = false)]
		public Unlimited<int> MailboxCountQuota
		{
			get
			{
				return (Unlimited<int>)this[RecipientEnforcementProvisioningPolicySchema.MailboxCountQuota];
			}
			set
			{
				this[RecipientEnforcementProvisioningPolicySchema.MailboxCountQuota] = value;
			}
		}

		// Token: 0x170022A8 RID: 8872
		// (get) Token: 0x060060C8 RID: 24776 RVA: 0x00148DD0 File Offset: 0x00146FD0
		// (set) Token: 0x060060C9 RID: 24777 RVA: 0x00148DE2 File Offset: 0x00146FE2
		public int? MailboxCount
		{
			get
			{
				return (int?)this[RecipientEnforcementProvisioningPolicySchema.MailboxCount];
			}
			internal set
			{
				this[RecipientEnforcementProvisioningPolicySchema.MailboxCount] = value;
			}
		}

		// Token: 0x170022A9 RID: 8873
		// (get) Token: 0x060060CA RID: 24778 RVA: 0x00148DF5 File Offset: 0x00146FF5
		// (set) Token: 0x060060CB RID: 24779 RVA: 0x00148E07 File Offset: 0x00147007
		[Parameter(Mandatory = false)]
		public Unlimited<int> MailUserCountQuota
		{
			get
			{
				return (Unlimited<int>)this[RecipientEnforcementProvisioningPolicySchema.MailUserCountQuota];
			}
			set
			{
				this[RecipientEnforcementProvisioningPolicySchema.MailUserCountQuota] = value;
			}
		}

		// Token: 0x170022AA RID: 8874
		// (get) Token: 0x060060CC RID: 24780 RVA: 0x00148E1A File Offset: 0x0014701A
		// (set) Token: 0x060060CD RID: 24781 RVA: 0x00148E2C File Offset: 0x0014702C
		public int? MailUserCount
		{
			get
			{
				return (int?)this[RecipientEnforcementProvisioningPolicySchema.MailUserCount];
			}
			internal set
			{
				this[RecipientEnforcementProvisioningPolicySchema.MailUserCount] = value;
			}
		}

		// Token: 0x170022AB RID: 8875
		// (get) Token: 0x060060CE RID: 24782 RVA: 0x00148E3F File Offset: 0x0014703F
		// (set) Token: 0x060060CF RID: 24783 RVA: 0x00148E51 File Offset: 0x00147051
		[Parameter(Mandatory = false)]
		public Unlimited<int> ContactCountQuota
		{
			get
			{
				return (Unlimited<int>)this[RecipientEnforcementProvisioningPolicySchema.ContactCountQuota];
			}
			set
			{
				this[RecipientEnforcementProvisioningPolicySchema.ContactCountQuota] = value;
			}
		}

		// Token: 0x170022AC RID: 8876
		// (get) Token: 0x060060D0 RID: 24784 RVA: 0x00148E64 File Offset: 0x00147064
		// (set) Token: 0x060060D1 RID: 24785 RVA: 0x00148E76 File Offset: 0x00147076
		public int? ContactCount
		{
			get
			{
				return (int?)this[RecipientEnforcementProvisioningPolicySchema.ContactCount];
			}
			internal set
			{
				this[RecipientEnforcementProvisioningPolicySchema.ContactCount] = value;
			}
		}

		// Token: 0x170022AD RID: 8877
		// (get) Token: 0x060060D2 RID: 24786 RVA: 0x00148E89 File Offset: 0x00147089
		// (set) Token: 0x060060D3 RID: 24787 RVA: 0x00148E9B File Offset: 0x0014709B
		[Parameter(Mandatory = false)]
		public Unlimited<int> TeamMailboxCountQuota
		{
			get
			{
				return (Unlimited<int>)this[RecipientEnforcementProvisioningPolicySchema.TeamMailboxCountQuota];
			}
			set
			{
				this[RecipientEnforcementProvisioningPolicySchema.TeamMailboxCountQuota] = value;
			}
		}

		// Token: 0x170022AE RID: 8878
		// (get) Token: 0x060060D4 RID: 24788 RVA: 0x00148EAE File Offset: 0x001470AE
		// (set) Token: 0x060060D5 RID: 24789 RVA: 0x00148EC0 File Offset: 0x001470C0
		public int? TeamMailboxCount
		{
			get
			{
				return (int?)this[RecipientEnforcementProvisioningPolicySchema.TeamMailboxCount];
			}
			internal set
			{
				this[RecipientEnforcementProvisioningPolicySchema.TeamMailboxCount] = value;
			}
		}

		// Token: 0x170022AF RID: 8879
		// (get) Token: 0x060060D6 RID: 24790 RVA: 0x00148ED3 File Offset: 0x001470D3
		// (set) Token: 0x060060D7 RID: 24791 RVA: 0x00148EE5 File Offset: 0x001470E5
		[Parameter(Mandatory = false)]
		public Unlimited<int> PublicFolderMailboxCountQuota
		{
			get
			{
				return (Unlimited<int>)this[RecipientEnforcementProvisioningPolicySchema.PublicFolderMailboxCountQuota];
			}
			set
			{
				this[RecipientEnforcementProvisioningPolicySchema.PublicFolderMailboxCountQuota] = value;
			}
		}

		// Token: 0x170022B0 RID: 8880
		// (get) Token: 0x060060D8 RID: 24792 RVA: 0x00148EF8 File Offset: 0x001470F8
		// (set) Token: 0x060060D9 RID: 24793 RVA: 0x00148F0A File Offset: 0x0014710A
		public int? PublicFolderMailboxCount
		{
			get
			{
				return (int?)this[RecipientEnforcementProvisioningPolicySchema.PublicFolderMailboxCount];
			}
			internal set
			{
				this[RecipientEnforcementProvisioningPolicySchema.PublicFolderMailboxCount] = value;
			}
		}

		// Token: 0x170022B1 RID: 8881
		// (get) Token: 0x060060DA RID: 24794 RVA: 0x00148F1D File Offset: 0x0014711D
		// (set) Token: 0x060060DB RID: 24795 RVA: 0x00148F2F File Offset: 0x0014712F
		[Parameter(Mandatory = false)]
		public Unlimited<int> MailPublicFolderCountQuota
		{
			get
			{
				return (Unlimited<int>)this[RecipientEnforcementProvisioningPolicySchema.MailPublicFolderCountQuota];
			}
			set
			{
				this[RecipientEnforcementProvisioningPolicySchema.MailPublicFolderCountQuota] = value;
			}
		}

		// Token: 0x170022B2 RID: 8882
		// (get) Token: 0x060060DC RID: 24796 RVA: 0x00148F42 File Offset: 0x00147142
		// (set) Token: 0x060060DD RID: 24797 RVA: 0x00148F54 File Offset: 0x00147154
		public int? MailPublicFolderCount
		{
			get
			{
				return (int?)this[RecipientEnforcementProvisioningPolicySchema.MailPublicFolderCount];
			}
			internal set
			{
				this[RecipientEnforcementProvisioningPolicySchema.MailPublicFolderCount] = value;
			}
		}

		// Token: 0x170022B3 RID: 8883
		// (get) Token: 0x060060DE RID: 24798 RVA: 0x00148F67 File Offset: 0x00147167
		// (set) Token: 0x060060DF RID: 24799 RVA: 0x00148F79 File Offset: 0x00147179
		internal MultiValuedProperty<string> ObjectCountQuota
		{
			get
			{
				return (MultiValuedProperty<string>)this[RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota];
			}
			set
			{
				this[RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota] = value;
			}
		}

		// Token: 0x060060E0 RID: 24800 RVA: 0x00148F87 File Offset: 0x00147187
		internal static object DistributionListCountQuotaGetter(IPropertyBag propertyBag)
		{
			return RecipientEnforcementProvisioningPolicy.ObjectCountQuotaGetter(RecipientEnforcementProvisioningPolicySchema.DistributionListCountQuota, "All Groups(VLV)", propertyBag);
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x00148F9E File Offset: 0x0014719E
		internal static void DistributionListCountQuotaSetter(object value, IPropertyBag propertyBag)
		{
			RecipientEnforcementProvisioningPolicy.ObjectCountQuotaSetter("All Groups(VLV)", (Unlimited<int>)value, propertyBag);
		}

		// Token: 0x060060E2 RID: 24802 RVA: 0x00148FB1 File Offset: 0x001471B1
		internal static object MailboxCountQuotaGetter(IPropertyBag propertyBag)
		{
			return RecipientEnforcementProvisioningPolicy.ObjectCountQuotaGetter(RecipientEnforcementProvisioningPolicySchema.MailboxCountQuota, "All Mailboxes(VLV)", propertyBag);
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x00148FC8 File Offset: 0x001471C8
		internal static void MailboxCountQuotaSetter(object value, IPropertyBag propertyBag)
		{
			RecipientEnforcementProvisioningPolicy.ObjectCountQuotaSetter("All Mailboxes(VLV)", (Unlimited<int>)value, propertyBag);
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x00148FDB File Offset: 0x001471DB
		internal static object MailUserCountQuotaGetter(IPropertyBag propertyBag)
		{
			return RecipientEnforcementProvisioningPolicy.ObjectCountQuotaGetter(RecipientEnforcementProvisioningPolicySchema.MailUserCountQuota, "All Mail Users(VLV)", propertyBag);
		}

		// Token: 0x060060E5 RID: 24805 RVA: 0x00148FF2 File Offset: 0x001471F2
		internal static void MailUserCountQuotaSetter(object value, IPropertyBag propertyBag)
		{
			RecipientEnforcementProvisioningPolicy.ObjectCountQuotaSetter("All Mail Users(VLV)", (Unlimited<int>)value, propertyBag);
		}

		// Token: 0x060060E6 RID: 24806 RVA: 0x00149005 File Offset: 0x00147205
		internal static object ContactCountQuotaGetter(IPropertyBag propertyBag)
		{
			return RecipientEnforcementProvisioningPolicy.ObjectCountQuotaGetter(RecipientEnforcementProvisioningPolicySchema.ContactCountQuota, "All Contacts(VLV)", propertyBag);
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x0014901C File Offset: 0x0014721C
		internal static void ContactCountQuotaSetter(object value, IPropertyBag propertyBag)
		{
			RecipientEnforcementProvisioningPolicy.ObjectCountQuotaSetter("All Contacts(VLV)", (Unlimited<int>)value, propertyBag);
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x0014902F File Offset: 0x0014722F
		internal static object TeamMailboxCountQuotaGetter(IPropertyBag propertyBag)
		{
			return RecipientEnforcementProvisioningPolicy.ObjectCountQuotaGetter(RecipientEnforcementProvisioningPolicySchema.TeamMailboxCountQuota, "TeamMailboxes(VLV)", propertyBag);
		}

		// Token: 0x060060E9 RID: 24809 RVA: 0x00149046 File Offset: 0x00147246
		internal static void TeamMailboxCountQuotaSetter(object value, IPropertyBag propertyBag)
		{
			RecipientEnforcementProvisioningPolicy.ObjectCountQuotaSetter("TeamMailboxes(VLV)", (Unlimited<int>)value, propertyBag);
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x00149059 File Offset: 0x00147259
		internal static object PublicFolderMailboxCountQuotaGetter(IPropertyBag propertyBag)
		{
			return RecipientEnforcementProvisioningPolicy.ObjectCountQuotaGetter(RecipientEnforcementProvisioningPolicySchema.PublicFolderMailboxCountQuota, "PublicFolderMailboxes(VLV)", propertyBag);
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x00149070 File Offset: 0x00147270
		internal static void PublicFolderMailboxCountQuotaSetter(object value, IPropertyBag propertyBag)
		{
			RecipientEnforcementProvisioningPolicy.ObjectCountQuotaSetter("PublicFolderMailboxes(VLV)", (Unlimited<int>)value, propertyBag);
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x00149083 File Offset: 0x00147283
		internal static object MailPublicFolderCountQuotaGetter(IPropertyBag propertyBag)
		{
			return RecipientEnforcementProvisioningPolicy.ObjectCountQuotaGetter(RecipientEnforcementProvisioningPolicySchema.MailPublicFolderCountQuota, "MailPublicFolders(VLV)", propertyBag);
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x0014909A File Offset: 0x0014729A
		internal static void MailPublicFolderCountQuotaSetter(object value, IPropertyBag propertyBag)
		{
			RecipientEnforcementProvisioningPolicy.ObjectCountQuotaSetter("MailPublicFolders(VLV)", (Unlimited<int>)value, propertyBag);
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x001490B0 File Offset: 0x001472B0
		internal static Unlimited<int> ObjectCountQuotaGetter(ADPropertyDefinition adPropertyDefinition, string key, IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota];
			if (multiValuedProperty == null || multiValuedProperty.Count == 0)
			{
				return Unlimited<int>.UnlimitedValue;
			}
			foreach (string text in multiValuedProperty)
			{
				string[] array = text.Split(new char[]
				{
					':'
				});
				if (array.Length != 2)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculatePropertyGeneric(adPropertyDefinition.Name), adPropertyDefinition, propertyBag[RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota]));
				}
				if (string.Equals(key, array[0], StringComparison.OrdinalIgnoreCase))
				{
					Unlimited<int> result = 0;
					Exception ex = null;
					try
					{
						result = Unlimited<int>.Parse(array[1]);
					}
					catch (ArgumentNullException ex2)
					{
						ex = ex2;
					}
					catch (FormatException ex3)
					{
						ex = ex3;
					}
					catch (OverflowException ex4)
					{
						ex = ex4;
					}
					if (ex != null)
					{
						throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(adPropertyDefinition.Name, ex.Message), adPropertyDefinition, propertyBag[RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota]), ex);
					}
					return result;
				}
			}
			return (Unlimited<int>)adPropertyDefinition.DefaultValue;
		}

		// Token: 0x060060EF RID: 24815 RVA: 0x00149200 File Offset: 0x00147400
		internal static void ObjectCountQuotaSetter(string key, Unlimited<int> countQuota, IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota];
			foreach (string text in multiValuedProperty)
			{
				string[] array = text.Split(new char[]
				{
					':'
				});
				if (array.Length == 2 && string.Equals(key, array[0], StringComparison.OrdinalIgnoreCase))
				{
					multiValuedProperty.Remove(text);
					break;
				}
			}
			if (countQuota != Unlimited<int>.UnlimitedValue)
			{
				string item = string.Format("{0}:{1}", key, countQuota);
				multiValuedProperty.Add(item);
			}
			propertyBag[RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota] = multiValuedProperty;
		}

		// Token: 0x040040F3 RID: 16627
		internal const string PolicyName = "Recipient Quota Policy";

		// Token: 0x040040F4 RID: 16628
		private static RecipientEnforcementProvisioningPolicySchema schema = ObjectSchema.GetInstance<RecipientEnforcementProvisioningPolicySchema>();

		// Token: 0x040040F5 RID: 16629
		private static IProvisioningEnforcement[] provisioningEnforcements = new IProvisioningEnforcement[]
		{
			new RecipientResourceCountQuota(RecipientEnforcementProvisioningPolicySchema.DistributionListCountQuota, "All Groups(VLV)", new Type[]
			{
				typeof(DistributionGroup),
				typeof(DynamicDistributionGroup),
				typeof(SyncDistributionGroup)
			}),
			new RecipientResourceCountQuota(RecipientEnforcementProvisioningPolicySchema.MailboxCountQuota, "All Mailboxes(VLV)", new Type[]
			{
				typeof(Mailbox),
				typeof(SyncMailbox)
			}, CannedSystemAddressLists.RecipientTypeDetailsForAllMailboxesAL),
			new RecipientResourceCountQuota(RecipientEnforcementProvisioningPolicySchema.MailUserCountQuota, "All Mail Users(VLV)", new Type[]
			{
				typeof(MailUser),
				typeof(SyncMailUser),
				typeof(RemoteMailbox)
			}),
			new RecipientResourceCountQuota(RecipientEnforcementProvisioningPolicySchema.ContactCountQuota, "All Contacts(VLV)", new Type[]
			{
				typeof(MailContact),
				typeof(SyncMailContact)
			}),
			new RecipientResourceCountQuota(RecipientEnforcementProvisioningPolicySchema.TeamMailboxCountQuota, "TeamMailboxes(VLV)", new Type[]
			{
				typeof(Mailbox),
				typeof(SyncMailbox)
			}, CannedSystemAddressLists.RecipientTypeDetailsForAllTeamMailboxesAL),
			new RecipientResourceCountQuota(RecipientEnforcementProvisioningPolicySchema.PublicFolderMailboxCountQuota, "PublicFolderMailboxes(VLV)", new Type[]
			{
				typeof(Mailbox),
				typeof(SyncMailbox)
			}, CannedSystemAddressLists.RecipientTypeDetailsForAllPublicFolderMailboxesAL),
			new RecipientResourceCountQuota(RecipientEnforcementProvisioningPolicySchema.MailPublicFolderCountQuota, "MailPublicFolders(VLV)", new Type[]
			{
				typeof(MailPublicFolder),
				typeof(ADPublicFolder)
			})
		};

		// Token: 0x040040F6 RID: 16630
		internal new static string MostDerivedClass = "msExchRecipientEnforcementPolicy";
	}
}
