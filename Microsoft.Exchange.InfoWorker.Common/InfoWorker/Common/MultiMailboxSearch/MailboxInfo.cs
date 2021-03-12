using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001F0 RID: 496
	internal class MailboxInfo
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x00036D2E File Offset: 0x00034F2E
		public MailboxInfo(Dictionary<PropertyDefinition, object> propertyMap, MailboxType type)
		{
			Util.ThrowOnNull(propertyMap, "propertyMap");
			this.propertyMap = propertyMap;
			this.type = type;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00036D4F File Offset: 0x00034F4F
		public MailboxInfo(ConfigurableObject configurableObject, MailboxType type)
		{
			Util.ThrowOnNull(configurableObject, "configurableObject");
			this.type = type;
			this.ParseConfigurableObject(configurableObject);
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00036D70 File Offset: 0x00034F70
		public ADObjectId OwnerId
		{
			get
			{
				return (ADObjectId)this[ADObjectSchema.Id];
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00036D82 File Offset: 0x00034F82
		public MailboxType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00036D8A File Offset: 0x00034F8A
		public bool IsPrimary
		{
			get
			{
				return this.type == MailboxType.Primary;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00036D95 File Offset: 0x00034F95
		public bool IsArchive
		{
			get
			{
				return this.type == MailboxType.Archive;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00036DA0 File Offset: 0x00034FA0
		internal bool IsEmpty
		{
			get
			{
				return null == this.propertyMap;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00036DAB File Offset: 0x00034FAB
		internal RecipientType RecipientType
		{
			get
			{
				return (RecipientType)this[ADRecipientSchema.RecipientType];
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00036DBD File Offset: 0x00034FBD
		internal RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[ADRecipientSchema.RecipientTypeDetails];
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00036DCF File Offset: 0x00034FCF
		internal SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[ADRecipientSchema.PrimarySmtpAddress];
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00036DE1 File Offset: 0x00034FE1
		internal string LegacyExchangeDN
		{
			get
			{
				return (string)this[ADRecipientSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00036DF3 File Offset: 0x00034FF3
		internal string DisplayName
		{
			get
			{
				return (string)this[ADRecipientSchema.DisplayName];
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00036E05 File Offset: 0x00035005
		internal string DistinguishedName
		{
			get
			{
				return this.OwnerId.DistinguishedName;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00036E12 File Offset: 0x00035012
		internal ProxyAddress ExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)this[ADRecipientSchema.ExternalEmailAddress];
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00036E24 File Offset: 0x00035024
		internal ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[ADRecipientSchema.EmailAddresses];
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00036E36 File Offset: 0x00035036
		internal Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[ADMailboxRecipientSchema.ExchangeGuid];
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00036E48 File Offset: 0x00035048
		internal ExchangeObjectVersion ExchangeVersion
		{
			get
			{
				return (ExchangeObjectVersion)this[ADObjectSchema.ExchangeVersion];
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00036E5A File Offset: 0x0003505A
		internal SecurityIdentifier MasterAccountSid
		{
			get
			{
				return (SecurityIdentifier)this[ADRecipientSchema.MasterAccountSid];
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00036E6C File Offset: 0x0003506C
		internal OrganizationId OrganizationId
		{
			get
			{
				return (OrganizationId)this[ADObjectSchema.OrganizationId];
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00036E7E File Offset: 0x0003507E
		internal Guid ArchiveGuid
		{
			get
			{
				return (Guid)this[ADUserSchema.ArchiveGuid];
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00036E90 File Offset: 0x00035090
		internal ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				return (ArchiveStatusFlags)this[ADUserSchema.ArchiveStatus];
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00036EA2 File Offset: 0x000350A2
		internal Guid MailboxGuid
		{
			get
			{
				if (this.Type != MailboxType.Primary)
				{
					return this.ArchiveGuid;
				}
				return this.ExchangeGuid;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00036EBA File Offset: 0x000350BA
		internal SmtpDomain ArchiveDomain
		{
			get
			{
				return (SmtpDomain)this[ADUserSchema.ArchiveDomain];
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00036ECC File Offset: 0x000350CC
		internal Guid ArchiveDatabase
		{
			get
			{
				ADObjectId adobjectId = this[ADUserSchema.ArchiveDatabaseRaw] as ADObjectId;
				if (adobjectId != null)
				{
					return adobjectId.ObjectGuid;
				}
				return Guid.Empty;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00036EFC File Offset: 0x000350FC
		internal Guid MdbGuid
		{
			get
			{
				ADObjectId adobjectId = this[ADMailboxRecipientSchema.Database] as ADObjectId;
				if (adobjectId != null)
				{
					return adobjectId.ObjectGuid;
				}
				return Guid.Empty;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00036F29 File Offset: 0x00035129
		internal bool IsRemoteMailbox
		{
			get
			{
				return this.IsCrossPremiseMailbox || this.IsCrossForestMailbox;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00036F3C File Offset: 0x0003513C
		internal virtual bool IsCrossPremiseMailbox
		{
			get
			{
				switch (this.RecipientType)
				{
				case RecipientType.UserMailbox:
					return !this.IsPrimary && this.ArchiveDomain != null;
				case RecipientType.MailUser:
					return this.MdbGuid == Guid.Empty && ((this.RecipientTypeDetails & (RecipientTypeDetails)((ulong)int.MinValue)) == (RecipientTypeDetails)((ulong)int.MinValue) || (this.RecipientTypeDetails & RecipientTypeDetails.RemoteRoomMailbox) == RecipientTypeDetails.RemoteRoomMailbox || (this.RecipientTypeDetails & RecipientTypeDetails.RemoteEquipmentMailbox) == RecipientTypeDetails.RemoteEquipmentMailbox || (this.RecipientTypeDetails & RecipientTypeDetails.RemoteTeamMailbox) == RecipientTypeDetails.RemoteTeamMailbox || (this.RecipientTypeDetails & RecipientTypeDetails.RemoteSharedMailbox) == RecipientTypeDetails.RemoteSharedMailbox);
				default:
					return false;
				}
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00037014 File Offset: 0x00035214
		internal virtual bool IsCrossForestMailbox
		{
			get
			{
				switch (this.RecipientType)
				{
				case RecipientType.MailUser:
				case RecipientType.MailContact:
					return !this.IsCrossPremiseMailbox && !this.IsCloudArchive;
				}
				return false;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00037053 File Offset: 0x00035253
		internal virtual bool IsCloudArchive
		{
			get
			{
				return this.RecipientType == RecipientType.MailUser && this.ArchiveGuid != Guid.Empty && this.ArchiveStatus == ArchiveStatusFlags.Active;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0003707C File Offset: 0x0003527C
		internal ExchangePrincipal ExchangePrincipal
		{
			get
			{
				if (this.exchangePrincipal == null)
				{
					this.exchangePrincipal = ExchangePrincipal.FromAnyVersionMailboxData(this.DisplayName, (this.Type == MailboxType.Primary) ? this.ExchangeGuid : this.ArchiveGuid, (this.Type == MailboxType.Primary) ? this.MdbGuid : this.ArchiveDatabase, this.PrimarySmtpAddress.ToString(), this.LegacyExchangeDN, this.OwnerId, this.RecipientType, this.MasterAccountSid, this.OrganizationId, RemotingOptions.AllowCrossSite, this.Type == MailboxType.Archive);
				}
				return this.exchangePrincipal;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00037112 File Offset: 0x00035312
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x0003711A File Offset: 0x0003531A
		internal string Folder { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00037123 File Offset: 0x00035323
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x0003712B File Offset: 0x0003532B
		internal object SourceMailbox { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00037134 File Offset: 0x00035334
		internal Dictionary<PropertyDefinition, object> PropertyMap
		{
			get
			{
				return this.propertyMap;
			}
		}

		// Token: 0x1700035B RID: 859
		internal object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				if (this.HasPropertyDefinition(propertyDefinition))
				{
					return this.propertyMap[propertyDefinition];
				}
				return null;
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00037158 File Offset: 0x00035358
		public string GetDomain()
		{
			if (!this.IsRemoteMailbox)
			{
				Factory.Current.GeneralTracer.TraceDebug((long)this.GetHashCode(), "MailboxInfo.GetDomain: Not a remote mailbox, returning primary smtp domain.");
				return this.PrimarySmtpAddress.Domain;
			}
			if (this.IsCrossForestMailbox || (this.IsCrossPremiseMailbox && this.IsPrimary))
			{
				Factory.Current.GeneralTracer.TraceDebug((long)this.GetHashCode(), "MailboxInfo.GetDomain: Remote primary mailbox, returning external email address domain.");
				return new SmtpAddress(this.ExternalEmailAddress.AddressString).Domain;
			}
			if (this.RecipientType == RecipientType.MailUser)
			{
				return new SmtpAddress(this.ExternalEmailAddress.AddressString).Domain;
			}
			if (this.RecipientType != RecipientType.UserMailbox)
			{
				return null;
			}
			if (this.ArchiveDomain == null)
			{
				return null;
			}
			return this.ArchiveDomain.ToString();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00037225 File Offset: 0x00035425
		public string GetUniqueKey()
		{
			return string.Format("{0}{1}", this.LegacyExchangeDN, this.Folder);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00037240 File Offset: 0x00035440
		private void ParseConfigurableObject(ConfigurableObject configurableObject)
		{
			Util.ThrowOnNull(configurableObject, "configurableObject");
			this.propertyMap = new Dictionary<PropertyDefinition, object>(MailboxInfo.PropertyDefinitionCollection.Length);
			foreach (PropertyDefinition propertyDefinition in MailboxInfo.PropertyDefinitionCollection)
			{
				object obj = null;
				if (!configurableObject.TryGetValueWithoutDefault(propertyDefinition, out obj))
				{
					Factory.Current.GeneralTracer.TraceDebug<string>((long)this.GetHashCode(), "PropertyDefinition {0} not found while creating MailboxInfo", propertyDefinition.ToString());
				}
				this.propertyMap[propertyDefinition] = configurableObject[propertyDefinition];
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000372C3 File Offset: 0x000354C3
		protected bool HasPropertyDefinition(PropertyDefinition propertyDefinition)
		{
			return this.propertyMap != null && 0 < this.propertyMap.Count && this.propertyMap.ContainsKey(propertyDefinition);
		}

		// Token: 0x0400092C RID: 2348
		internal static PropertyDefinition[] PropertyDefinitionCollection = new PropertyDefinition[]
		{
			ADObjectSchema.ExchangeVersion,
			ADObjectSchema.Id,
			ADObjectSchema.OrganizationId,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.EmailAddresses,
			ADRecipientSchema.ExternalEmailAddress,
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.RecipientTypeDetails,
			ADUserSchema.ArchiveGuid,
			ADUserSchema.ArchiveDomain,
			ADUserSchema.ArchiveDatabaseRaw,
			ADUserSchema.ArchiveStatus,
			ADMailboxRecipientSchema.Database,
			ADMailboxRecipientSchema.ExchangeGuid,
			ADRecipientSchema.MasterAccountSid
		};

		// Token: 0x0400092D RID: 2349
		protected Dictionary<PropertyDefinition, object> propertyMap;

		// Token: 0x0400092E RID: 2350
		private MailboxType type;

		// Token: 0x0400092F RID: 2351
		private ExchangePrincipal exchangePrincipal;
	}
}
