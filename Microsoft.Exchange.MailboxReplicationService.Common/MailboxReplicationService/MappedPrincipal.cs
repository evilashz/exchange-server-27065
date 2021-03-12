using System;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200003F RID: 63
	[DataContract]
	internal sealed class MappedPrincipal : IEquatable<MappedPrincipal>
	{
		// Token: 0x06000306 RID: 774 RVA: 0x0000550D File Offset: 0x0000370D
		public MappedPrincipal()
		{
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000552B File Offset: 0x0000372B
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00005533 File Offset: 0x00003733
		public SecurityIdentifier ObjectSid
		{
			get
			{
				return this.objectSid;
			}
			set
			{
				this.objectSid = value;
				this.UpdatePresentFields(MappedPrincipalFields.ObjectSid, value != null);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000554A File Offset: 0x0000374A
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00005552 File Offset: 0x00003752
		[DataMember(EmitDefaultValue = false)]
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
			set
			{
				this.mailboxGuid = value;
				this.UpdatePresentFields(MappedPrincipalFields.MailboxGuid, value != Guid.Empty);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000556D File Offset: 0x0000376D
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000558A File Offset: 0x0000378A
		[DataMember(EmitDefaultValue = false, Name = "ObjectSid")]
		public string ObjectSidString
		{
			get
			{
				if (!(this.ObjectSid != null))
				{
					return null;
				}
				return this.ObjectSid.ToString();
			}
			set
			{
				this.ObjectSid = (string.IsNullOrEmpty(value) ? null : new SecurityIdentifier(value));
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000055A3 File Offset: 0x000037A3
		// (set) Token: 0x0600030E RID: 782 RVA: 0x000055AB File Offset: 0x000037AB
		[DataMember(EmitDefaultValue = false)]
		public Guid ObjectGuid
		{
			get
			{
				return this.objectGuid;
			}
			set
			{
				this.objectGuid = value;
				this.UpdatePresentFields(MappedPrincipalFields.ObjectGuid, value != Guid.Empty);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600030F RID: 783 RVA: 0x000055C6 File Offset: 0x000037C6
		// (set) Token: 0x06000310 RID: 784 RVA: 0x000055CE File Offset: 0x000037CE
		[DataMember(EmitDefaultValue = false)]
		public string ObjectDN
		{
			get
			{
				return this.objectDN;
			}
			set
			{
				this.objectDN = value;
				this.UpdatePresentFields(MappedPrincipalFields.ObjectDN, !string.IsNullOrEmpty(value));
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000311 RID: 785 RVA: 0x000055E7 File Offset: 0x000037E7
		// (set) Token: 0x06000312 RID: 786 RVA: 0x000055EF File Offset: 0x000037EF
		[DataMember(EmitDefaultValue = false)]
		public string LegacyDN
		{
			get
			{
				return this.legacyDN;
			}
			set
			{
				this.legacyDN = value;
				this.UpdatePresentFields(MappedPrincipalFields.LegacyDN, !string.IsNullOrEmpty(value));
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00005609 File Offset: 0x00003809
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00005611 File Offset: 0x00003811
		[DataMember(EmitDefaultValue = false)]
		public string[] ProxyAddresses
		{
			get
			{
				return this.proxyAddresses;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					value = null;
				}
				this.proxyAddresses = value;
				this.UpdatePresentFields(MappedPrincipalFields.ProxyAddresses, value != null);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00005634 File Offset: 0x00003834
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000563C File Offset: 0x0000383C
		[DataMember(EmitDefaultValue = false)]
		public string Alias
		{
			get
			{
				return this.alias;
			}
			set
			{
				this.alias = value;
				this.UpdatePresentFields(MappedPrincipalFields.Alias, !string.IsNullOrEmpty(value));
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00005656 File Offset: 0x00003856
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000565E File Offset: 0x0000385E
		[DataMember(EmitDefaultValue = false)]
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
				this.UpdatePresentFields(MappedPrincipalFields.DisplayName, !string.IsNullOrEmpty(value));
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000567B File Offset: 0x0000387B
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00005683 File Offset: 0x00003883
		[DataMember(EmitDefaultValue = false)]
		public MappedPrincipal NextEntry
		{
			get
			{
				return this.nextEntry;
			}
			set
			{
				this.nextEntry = value;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000568C File Offset: 0x0000388C
		private void UpdatePresentFields(MappedPrincipalFields field, bool isPresent)
		{
			if (isPresent)
			{
				this.presentFields |= field;
				return;
			}
			this.presentFields &= ~field;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000056AF File Offset: 0x000038AF
		public MappedPrincipal(Guid mailboxGuid)
		{
			this.MailboxGuid = mailboxGuid;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000056D4 File Offset: 0x000038D4
		public MappedPrincipal(SecurityIdentifier objectSid)
		{
			this.ObjectSid = objectSid;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000056FC File Offset: 0x000038FC
		public MappedPrincipal(ADRawEntry entry)
		{
			this.ObjectGuid = entry.Id.ObjectGuid;
			this.ObjectDN = entry.Id.DistinguishedName;
			object[] properties = entry.GetProperties(MappedPrincipal.PrincipalProperties);
			this.MailboxGuid = ((properties[0] is Guid) ? ((Guid)properties[0]) : Guid.Empty);
			this.LegacyDN = (properties[1] as string);
			this.ObjectSid = (properties[2] as SecurityIdentifier);
			ProxyAddressCollection proxyAddressCollection = properties[3] as ProxyAddressCollection;
			this.ProxyAddresses = ((proxyAddressCollection != null) ? proxyAddressCollection.ToStringArray() : null);
			SecurityIdentifier securityIdentifier = properties[4] as SecurityIdentifier;
			if (securityIdentifier != null && !securityIdentifier.IsWellKnown(WellKnownSidType.SelfSid))
			{
				this.ObjectSid = securityIdentifier;
			}
			this.Alias = (properties[5] as string);
			this.DisplayName = (properties[6] as string);
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000057E8 File Offset: 0x000039E8
		public MappedPrincipalFields PresentFields
		{
			get
			{
				return this.presentFields;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000057F0 File Offset: 0x000039F0
		public bool HasField(MappedPrincipalFields field)
		{
			return (this.presentFields & field) != MappedPrincipalFields.None;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00005800 File Offset: 0x00003A00
		public override int GetHashCode()
		{
			if (this.HasField(MappedPrincipalFields.MailboxGuid))
			{
				return this.MailboxGuid.GetHashCode();
			}
			if (this.HasField(MappedPrincipalFields.ObjectSid))
			{
				return this.ObjectSid.GetHashCode();
			}
			if (this.HasField(MappedPrincipalFields.ObjectGuid))
			{
				return this.ObjectGuid.GetHashCode();
			}
			if (this.HasField(MappedPrincipalFields.LegacyDN))
			{
				return this.LegacyDN.GetHashCode();
			}
			if (this.HasField(MappedPrincipalFields.ProxyAddresses))
			{
				return this.ProxyAddresses.GetHashCode();
			}
			if (this.HasField(MappedPrincipalFields.Alias))
			{
				return this.Alias.GetHashCode();
			}
			if (this.HasField(MappedPrincipalFields.DisplayName))
			{
				return this.DisplayName.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000058C0 File Offset: 0x00003AC0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.HasField(MappedPrincipalFields.Alias))
			{
				stringBuilder.AppendFormat("Alias: {0}; ", this.Alias);
			}
			if (this.HasField(MappedPrincipalFields.DisplayName))
			{
				stringBuilder.AppendFormat("DisplayName: {0}; ", this.DisplayName);
			}
			if (this.HasField(MappedPrincipalFields.MailboxGuid))
			{
				stringBuilder.AppendFormat("MailboxGuid: {0}; ", this.MailboxGuid);
			}
			if (this.HasField(MappedPrincipalFields.ObjectSid))
			{
				stringBuilder.AppendFormat("SID: {0}; ", this.ObjectSid);
			}
			if (this.HasField(MappedPrincipalFields.ObjectGuid))
			{
				stringBuilder.AppendFormat("ObjectGuid: {0}; ", this.ObjectGuid);
			}
			if (this.HasField(MappedPrincipalFields.LegacyDN))
			{
				stringBuilder.AppendFormat("LegDN: {0}; ", this.LegacyDN);
			}
			if (this.HasField(MappedPrincipalFields.ProxyAddresses))
			{
				stringBuilder.AppendFormat("Proxies: [{0}]; ", string.Join("; ", this.ProxyAddresses));
			}
			if (this.NextEntry != null)
			{
				stringBuilder.AppendFormat("Next: {0}; ", this.NextEntry.ToString());
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000059D8 File Offset: 0x00003BD8
		public bool Equals(MappedPrincipal other)
		{
			if (this.HasField(MappedPrincipalFields.MailboxGuid))
			{
				return other.HasField(MappedPrincipalFields.MailboxGuid) && this.MailboxGuid.Equals(other.MailboxGuid);
			}
			if (this.HasField(MappedPrincipalFields.ObjectSid))
			{
				return other.HasField(MappedPrincipalFields.ObjectSid) && this.ObjectSid.Equals(other.ObjectSid);
			}
			return this.HasField(MappedPrincipalFields.ObjectGuid) && other.HasField(MappedPrincipalFields.ObjectGuid) && this.ObjectGuid.Equals(other.ObjectGuid);
		}

		// Token: 0x0400025E RID: 606
		private MappedPrincipalFields presentFields;

		// Token: 0x0400025F RID: 607
		private Guid mailboxGuid = Guid.Empty;

		// Token: 0x04000260 RID: 608
		private SecurityIdentifier objectSid;

		// Token: 0x04000261 RID: 609
		private Guid objectGuid = Guid.Empty;

		// Token: 0x04000262 RID: 610
		private string objectDN;

		// Token: 0x04000263 RID: 611
		private string legacyDN;

		// Token: 0x04000264 RID: 612
		private string[] proxyAddresses;

		// Token: 0x04000265 RID: 613
		private string alias;

		// Token: 0x04000266 RID: 614
		private string displayName;

		// Token: 0x04000267 RID: 615
		private MappedPrincipal nextEntry;

		// Token: 0x04000268 RID: 616
		public static readonly PropertyDefinition[] PrincipalProperties = new PropertyDefinition[]
		{
			ADMailboxRecipientSchema.ExchangeGuid,
			ADRecipientSchema.LegacyExchangeDN,
			ADMailboxRecipientSchema.Sid,
			ADRecipientSchema.EmailAddresses,
			ADRecipientSchema.MasterAccountSid,
			ADRecipientSchema.Alias,
			ADRecipientSchema.DisplayName
		};
	}
}
