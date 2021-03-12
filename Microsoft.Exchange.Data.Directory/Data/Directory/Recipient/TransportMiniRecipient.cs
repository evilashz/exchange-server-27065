using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200027F RID: 639
	[Serializable]
	public class TransportMiniRecipient : MiniRecipient
	{
		// Token: 0x06001E17 RID: 7703 RVA: 0x00088487 File Offset: 0x00086687
		internal TransportMiniRecipient(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00088491 File Offset: 0x00086691
		public TransportMiniRecipient()
		{
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00088499 File Offset: 0x00086699
		public ADObjectId AcceptMessagesOnlyFrom
		{
			get
			{
				return (ADObjectId)this[TransportMiniRecipientSchema.AcceptMessagesOnlyFrom];
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x000884AB File Offset: 0x000866AB
		public ADMultiValuedProperty<ADObjectId> AcceptMessagesOnlyFromDLMembers
		{
			get
			{
				return (ADMultiValuedProperty<ADObjectId>)this[TransportMiniRecipientSchema.AcceptMessagesOnlyFromDLMembers];
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x000884BD File Offset: 0x000866BD
		public bool AntispamBypassEnabled
		{
			get
			{
				return (bool)this[TransportMiniRecipientSchema.AntispamBypassEnabled];
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x000884CF File Offset: 0x000866CF
		public ADMultiValuedProperty<ADObjectId> ApprovalApplications
		{
			get
			{
				return (ADMultiValuedProperty<ADObjectId>)this[TransportMiniRecipientSchema.ApprovalApplications];
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x000884E1 File Offset: 0x000866E1
		public ADObjectId ArbitrationMailbox
		{
			get
			{
				return (ADObjectId)this[TransportMiniRecipientSchema.ArbitrationMailbox];
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x000884F3 File Offset: 0x000866F3
		public byte[] BlockedSendersHash
		{
			get
			{
				return (byte[])this[TransportMiniRecipientSchema.BlockedSendersHash];
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x00088505 File Offset: 0x00086705
		public ADMultiValuedProperty<ADObjectId> BypassModerationFrom
		{
			get
			{
				return (ADMultiValuedProperty<ADObjectId>)this[TransportMiniRecipientSchema.BypassModerationFrom];
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x00088517 File Offset: 0x00086717
		public ADMultiValuedProperty<ADObjectId> BypassModerationFromDLMembers
		{
			get
			{
				return (ADMultiValuedProperty<ADObjectId>)this[TransportMiniRecipientSchema.BypassModerationFromDLMembers];
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x00088529 File Offset: 0x00086729
		public string C
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.C];
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x0008853B File Offset: 0x0008673B
		public string City
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.City];
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x0008854D File Offset: 0x0008674D
		public string Company
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.Company];
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x0008855F File Offset: 0x0008675F
		public string CustomAttribute1
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute1];
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x00088571 File Offset: 0x00086771
		public string CustomAttribute2
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute2];
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x00088583 File Offset: 0x00086783
		public string CustomAttribute3
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute3];
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x00088595 File Offset: 0x00086795
		public string CustomAttribute4
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute4];
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x000885A7 File Offset: 0x000867A7
		public string CustomAttribute5
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute5];
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x000885B9 File Offset: 0x000867B9
		public string CustomAttribute6
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute6];
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x000885CB File Offset: 0x000867CB
		public string CustomAttribute7
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute7];
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x000885DD File Offset: 0x000867DD
		public string CustomAttribute8
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute8];
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x000885EF File Offset: 0x000867EF
		public string CustomAttribute9
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute9];
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x00088601 File Offset: 0x00086801
		public string CustomAttribute10
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute10];
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x00088613 File Offset: 0x00086813
		public string CustomAttribute11
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute11];
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x00088625 File Offset: 0x00086825
		public string CustomAttribute12
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute12];
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x00088637 File Offset: 0x00086837
		public string CustomAttribute13
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute13];
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x00088649 File Offset: 0x00086849
		public string CustomAttribute14
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute14];
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0008865B File Offset: 0x0008685B
		public string CustomAttribute15
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.CustomAttribute15];
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x0008866D File Offset: 0x0008686D
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return (bool)this[TransportMiniRecipientSchema.DeliverToMailboxAndForward];
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x0008867F File Offset: 0x0008687F
		public string Department
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.Department];
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x00088691 File Offset: 0x00086891
		public ADMultiValuedProperty<ADObjectIdWithString> DLSupervisionList
		{
			get
			{
				return (ADMultiValuedProperty<ADObjectIdWithString>)this[TransportMiniRecipientSchema.DLSupervisionList];
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x000886A3 File Offset: 0x000868A3
		public bool DowngradeHighPriorityMessagesEnabled
		{
			get
			{
				return (bool)this[TransportMiniRecipientSchema.DowngradeHighPriorityMessagesEnabled];
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x000886B5 File Offset: 0x000868B5
		public ElcMailboxFlags ElcMailboxFlags
		{
			get
			{
				return (ElcMailboxFlags)this[TransportMiniRecipientSchema.ElcMailboxFlags];
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x000886C7 File Offset: 0x000868C7
		public ADObjectId ElcPolicyTemplate
		{
			get
			{
				return (ADObjectId)this[TransportMiniRecipientSchema.ElcPolicyTemplate];
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x000886D9 File Offset: 0x000868D9
		public MultiValuedProperty<string> ExtensionCustomAttribute1
		{
			get
			{
				return (MultiValuedProperty<string>)this[TransportMiniRecipientSchema.ExtensionCustomAttribute1];
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x000886EB File Offset: 0x000868EB
		public MultiValuedProperty<string> ExtensionCustomAttribute2
		{
			get
			{
				return (MultiValuedProperty<string>)this[TransportMiniRecipientSchema.ExtensionCustomAttribute2];
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x000886FD File Offset: 0x000868FD
		public MultiValuedProperty<string> ExtensionCustomAttribute3
		{
			get
			{
				return (MultiValuedProperty<string>)this[TransportMiniRecipientSchema.ExtensionCustomAttribute3];
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0008870F File Offset: 0x0008690F
		public MultiValuedProperty<string> ExtensionCustomAttribute4
		{
			get
			{
				return (MultiValuedProperty<string>)this[TransportMiniRecipientSchema.ExtensionCustomAttribute4];
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x00088721 File Offset: 0x00086921
		public MultiValuedProperty<string> ExtensionCustomAttribute5
		{
			get
			{
				return (MultiValuedProperty<string>)this[TransportMiniRecipientSchema.ExtensionCustomAttribute5];
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x00088733 File Offset: 0x00086933
		public ExternalOofOptions ExternalOofOptions
		{
			get
			{
				return (ExternalOofOptions)this[TransportMiniRecipientSchema.ExternalOofOptions];
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x00088745 File Offset: 0x00086945
		public string Fax
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.Fax];
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x00088757 File Offset: 0x00086957
		public string FirstName
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.FirstName];
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x00088769 File Offset: 0x00086969
		public ADObjectId ForwardingAddress
		{
			get
			{
				return (ADObjectId)this[TransportMiniRecipientSchema.ForwardingAddress];
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0008877B File Offset: 0x0008697B
		public ProxyAddress ForwardingSmtpAddress
		{
			get
			{
				return (ProxyAddress)this[TransportMiniRecipientSchema.ForwardingSmtpAddress];
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x0008878D File Offset: 0x0008698D
		public ADObjectId HomeMtaServerId
		{
			get
			{
				return (ADObjectId)this[TransportMiniRecipientSchema.HomeMtaServerId];
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0008879F File Offset: 0x0008699F
		public string HomePhone
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.HomePhone];
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x000887B1 File Offset: 0x000869B1
		public string Initials
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.Initials];
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x000887C3 File Offset: 0x000869C3
		public ADMultiValuedProperty<ADObjectIdWithString> InternalRecipientSupervisionList
		{
			get
			{
				return (ADMultiValuedProperty<ADObjectIdWithString>)this[TransportMiniRecipientSchema.InternalRecipientSupervisionList];
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x000887D5 File Offset: 0x000869D5
		public int InternetEncoding
		{
			get
			{
				return (int)this[TransportMiniRecipientSchema.InternetEncoding];
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x000887E7 File Offset: 0x000869E7
		public string LanguagesRaw
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.LanguagesRaw];
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x000887F9 File Offset: 0x000869F9
		public string LastName
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.LastName];
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0008880B File Offset: 0x00086A0B
		public ADMultiValuedProperty<ADObjectId> ManagedBy
		{
			get
			{
				return (ADMultiValuedProperty<ADObjectId>)this[TransportMiniRecipientSchema.ManagedBy];
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x0008881D File Offset: 0x00086A1D
		public ADObjectId Manager
		{
			get
			{
				return (ADObjectId)this[TransportMiniRecipientSchema.Manager];
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x0008882F File Offset: 0x00086A2F
		public bool? MapiRecipient
		{
			get
			{
				return (bool?)this[TransportMiniRecipientSchema.MapiRecipient];
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x00088841 File Offset: 0x00086A41
		public Unlimited<ByteQuantifiedSize> MaxReceiveSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportMiniRecipientSchema.MaxReceiveSize];
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x00088853 File Offset: 0x00086A53
		public Unlimited<ByteQuantifiedSize> MaxSendSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportMiniRecipientSchema.MaxSendSize];
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x00088865 File Offset: 0x00086A65
		public string MobilePhone
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.MobilePhone];
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001E50 RID: 7760 RVA: 0x00088877 File Offset: 0x00086A77
		public ADMultiValuedProperty<ADObjectId> ModeratedBy
		{
			get
			{
				return (ADMultiValuedProperty<ADObjectId>)this[TransportMiniRecipientSchema.ModeratedBy];
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x00088889 File Offset: 0x00086A89
		public bool ModerationEnabled
		{
			get
			{
				return (bool)this[TransportMiniRecipientSchema.ModerationEnabled];
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x0008889B File Offset: 0x00086A9B
		public int ModerationFlags
		{
			get
			{
				return (int)this[TransportMiniRecipientSchema.ModerationFlags];
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x000888AD File Offset: 0x00086AAD
		public string Notes
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.Notes];
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x000888BF File Offset: 0x00086ABF
		public string Office
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.Office];
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x000888D1 File Offset: 0x00086AD1
		public ADMultiValuedProperty<ADObjectIdWithString> OneOffSupervisionList
		{
			get
			{
				return (ADMultiValuedProperty<ADObjectIdWithString>)this[TransportMiniRecipientSchema.OneOffSupervisionList];
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x000888E3 File Offset: 0x00086AE3
		public bool OpenDomainRoutingDisabled
		{
			get
			{
				return (bool)this[TransportMiniRecipientSchema.OpenDomainRoutingDisabled];
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x000888F5 File Offset: 0x00086AF5
		public MultiValuedProperty<string> OtherFax
		{
			get
			{
				return (MultiValuedProperty<string>)this[TransportMiniRecipientSchema.OtherFax];
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x00088907 File Offset: 0x00086B07
		public MultiValuedProperty<string> OtherHomePhone
		{
			get
			{
				return (MultiValuedProperty<string>)this[TransportMiniRecipientSchema.OtherHomePhone];
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x00088919 File Offset: 0x00086B19
		public MultiValuedProperty<string> OtherTelephone
		{
			get
			{
				return (MultiValuedProperty<string>)this[TransportMiniRecipientSchema.OtherTelephone];
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0008892B File Offset: 0x00086B2B
		public string Pager
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.Pager];
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x0008893D File Offset: 0x00086B3D
		public string Phone
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.Phone];
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0008894F File Offset: 0x00086B4F
		public string PostalCode
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.PostalCode];
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x00088961 File Offset: 0x00086B61
		public string PostOfficeBox
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.PostOfficeBox];
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x00088973 File Offset: 0x00086B73
		public Unlimited<ByteQuantifiedSize> ProhibitSendQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportMiniRecipientSchema.ProhibitSendQuota];
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x00088985 File Offset: 0x00086B85
		public ADObjectId PublicFolderContentMailbox
		{
			get
			{
				return (ADObjectId)this[TransportMiniRecipientSchema.PublicFolderContentMailbox];
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x00088997 File Offset: 0x00086B97
		public string PublicFolderEntryId
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.PublicFolderEntryId];
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x000889A9 File Offset: 0x00086BA9
		public RecipientDisplayType? RecipientDisplayType
		{
			get
			{
				return (RecipientDisplayType?)this[TransportMiniRecipientSchema.RecipientDisplayType];
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x000889BB File Offset: 0x00086BBB
		public Unlimited<int> RecipientLimits
		{
			get
			{
				return (Unlimited<int>)this[TransportMiniRecipientSchema.RecipientLimits];
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x000889CD File Offset: 0x00086BCD
		public RecipientTypeDetails RecipientTypeDetailsValue
		{
			get
			{
				return (RecipientTypeDetails)this[TransportMiniRecipientSchema.RecipientTypeDetailsValue];
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x000889DF File Offset: 0x00086BDF
		public ADMultiValuedProperty<ADObjectId> RejectMessagesFrom
		{
			get
			{
				return (ADMultiValuedProperty<ADObjectId>)this[TransportMiniRecipientSchema.RejectMessagesFrom];
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x000889F1 File Offset: 0x00086BF1
		public ADMultiValuedProperty<ADObjectId> RejectMessagesFromDLMembers
		{
			get
			{
				return (ADMultiValuedProperty<ADObjectId>)this[TransportMiniRecipientSchema.RejectMessagesFromDLMembers];
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x00088A03 File Offset: 0x00086C03
		public bool RequireAllSendersAreAuthenticated
		{
			get
			{
				return (bool)this[TransportMiniRecipientSchema.RequireAllSendersAreAuthenticated];
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x00088A15 File Offset: 0x00086C15
		public ByteQuantifiedSize RulesQuota
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportMiniRecipientSchema.RulesQuota];
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x00088A27 File Offset: 0x00086C27
		public byte[] SafeRecipientsHash
		{
			get
			{
				return (byte[])this[TransportMiniRecipientSchema.SafeRecipientsHash];
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x00088A39 File Offset: 0x00086C39
		public byte[] SafeSendersHash
		{
			get
			{
				return (byte[])this[TransportMiniRecipientSchema.SafeSendersHash];
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x00088A4B File Offset: 0x00086C4B
		public string SamAccountName
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.SamAccountName];
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x00088A5D File Offset: 0x00086C5D
		public bool? SCLDeleteEnabled
		{
			get
			{
				return (bool?)this[TransportMiniRecipientSchema.SCLDeleteEnabled];
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x00088A6F File Offset: 0x00086C6F
		public int? SCLDeleteThreshold
		{
			get
			{
				return (int?)this[TransportMiniRecipientSchema.SCLDeleteThreshold];
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x00088A81 File Offset: 0x00086C81
		public bool? SCLJunkEnabled
		{
			get
			{
				return (bool?)this[TransportMiniRecipientSchema.SCLJunkEnabled];
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x00088A93 File Offset: 0x00086C93
		public int? SCLJunkThreshold
		{
			get
			{
				return (int?)this[TransportMiniRecipientSchema.SCLJunkThreshold];
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x00088AA5 File Offset: 0x00086CA5
		public bool? SCLQuarantineEnabled
		{
			get
			{
				return (bool?)this[TransportMiniRecipientSchema.SCLQuarantineEnabled];
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x00088AB7 File Offset: 0x00086CB7
		public int? SCLQuarantineThreshold
		{
			get
			{
				return (int?)this[TransportMiniRecipientSchema.SCLQuarantineThreshold];
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x00088AC9 File Offset: 0x00086CC9
		public bool? SCLRejectEnabled
		{
			get
			{
				return (bool?)this[TransportMiniRecipientSchema.SCLRejectEnabled];
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x00088ADB File Offset: 0x00086CDB
		public int? SCLRejectThreshold
		{
			get
			{
				return (int?)this[TransportMiniRecipientSchema.SCLRejectThreshold];
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x00088AED File Offset: 0x00086CED
		public DeliveryReportsReceiver SendDeliveryReportsTo
		{
			get
			{
				return (DeliveryReportsReceiver)this[TransportMiniRecipientSchema.SendDeliveryReportsTo];
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00088AFF File Offset: 0x00086CFF
		public bool SendOofMessageToOriginatorEnabled
		{
			get
			{
				return (bool)this[TransportMiniRecipientSchema.SendOofMessageToOriginatorEnabled];
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x00088B11 File Offset: 0x00086D11
		public string ServerName
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.ServerName];
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x00088B23 File Offset: 0x00086D23
		public string SimpleDisplayName
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.SimpleDisplayName];
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x00088B35 File Offset: 0x00086D35
		public string StateOrProvince
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.StateOrProvince];
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x00088B47 File Offset: 0x00086D47
		public string StreetAddress
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.StreetAddress];
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x00088B59 File Offset: 0x00086D59
		public string Title
		{
			get
			{
				return (string)this[TransportMiniRecipientSchema.Title];
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x00088B6B File Offset: 0x00086D6B
		public SmtpAddress WindowsEmailAddress
		{
			get
			{
				return (SmtpAddress)this[TransportMiniRecipientSchema.WindowsEmailAddress];
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x00088B7D File Offset: 0x00086D7D
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x00088B85 File Offset: 0x00086D85
		internal bool IsSenderOrP2RecipientEntry { get; private set; }

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x00088B8E File Offset: 0x00086D8E
		internal override ADObjectSchema Schema
		{
			get
			{
				return TransportMiniRecipientSchema.Schema;
			}
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x00088B95 File Offset: 0x00086D95
		internal void SetSenderOrP2RecipientEntry()
		{
			this.IsSenderOrP2RecipientEntry = true;
		}
	}
}
