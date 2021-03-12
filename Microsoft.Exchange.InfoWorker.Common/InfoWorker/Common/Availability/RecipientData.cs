using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000BD RID: 189
	internal sealed class RecipientData
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001476B File Offset: 0x0001296B
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00014773 File Offset: 0x00012973
		public long ExchangePrincipalLatency { get; private set; }

		// Token: 0x0600047F RID: 1151 RVA: 0x0001477C File Offset: 0x0001297C
		public override string ToString()
		{
			if (this.initialEmailAddress == null)
			{
				if (this.emailAddress == null)
				{
					return "null";
				}
				return this.emailAddress.Address;
			}
			else
			{
				if (this.emailAddress == null)
				{
					return this.initialEmailAddress.Address;
				}
				if (StringComparer.OrdinalIgnoreCase.Equals(this.initialEmailAddress.Address, this.emailAddress.Address))
				{
					return this.emailAddress.Address;
				}
				return this.emailAddress.Address + " (" + this.initialEmailAddress.Address + ")";
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00014812 File Offset: 0x00012A12
		private RecipientData(EmailAddress emailAddress)
		{
			this.initialEmailAddress = emailAddress;
			this.emailAddress = emailAddress;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00014828 File Offset: 0x00012A28
		internal static RecipientData Create(EmailAddress emailAddress)
		{
			return new RecipientData(emailAddress);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00014830 File Offset: 0x00012A30
		internal static RecipientData Create(EmailAddress emailAddress, ConfigurableObject configurableObject, ICollection<PropertyDefinition> propertyDefinitionCollection)
		{
			if (Testability.HandleSmtpAddressAsContact(emailAddress.Address))
			{
				return RecipientData.CreateAsContact(emailAddress);
			}
			RecipientData recipientData = new RecipientData(emailAddress);
			recipientData.ParseConfigurableObject(configurableObject, propertyDefinitionCollection);
			return recipientData;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00014864 File Offset: 0x00012A64
		internal static RecipientData Create(EmailAddress emailAddress, Dictionary<PropertyDefinition, object> propertyMap)
		{
			if (Testability.HandleSmtpAddressAsContact(emailAddress.Address))
			{
				return RecipientData.CreateAsContact(emailAddress);
			}
			return new RecipientData(emailAddress)
			{
				propertyMap = propertyMap
			};
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00014894 File Offset: 0x00012A94
		internal static RecipientData Create(EmailAddress emailAddress, Exception exception)
		{
			return new RecipientData(emailAddress)
			{
				exception = exception
			};
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000148B0 File Offset: 0x00012AB0
		internal static RecipientData Create(EmailAddress emailAddress, ProviderError providerError)
		{
			return new RecipientData(emailAddress)
			{
				providerError = providerError
			};
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000148CC File Offset: 0x00012ACC
		internal EmailAddress InitialEmailAddress
		{
			get
			{
				return this.initialEmailAddress;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000148D4 File Offset: 0x00012AD4
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x000148DC File Offset: 0x00012ADC
		internal EmailAddress EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
			set
			{
				this.emailAddress = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000148E5 File Offset: 0x00012AE5
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x000148ED File Offset: 0x00012AED
		internal StoreObjectId AssociatedFolderId
		{
			get
			{
				return this.associatedFolderId;
			}
			set
			{
				this.associatedFolderId = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000148F6 File Offset: 0x00012AF6
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000148FE File Offset: 0x00012AFE
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00014907 File Offset: 0x00012B07
		internal ProviderError ProviderError
		{
			get
			{
				return this.providerError;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0001490F File Offset: 0x00012B0F
		internal bool IsEmpty
		{
			get
			{
				return null == this.propertyMap;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001491A File Offset: 0x00012B1A
		internal RecipientType RecipientType
		{
			get
			{
				return (RecipientType)this[ADRecipientSchema.RecipientType];
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0001492C File Offset: 0x00012B2C
		internal RecipientDisplayType? RecipientDisplayType
		{
			get
			{
				return (RecipientDisplayType?)this[ADRecipientSchema.RecipientDisplayType];
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00014940 File Offset: 0x00012B40
		internal SmtpAddress PrimarySmtpAddress
		{
			get
			{
				object obj = this[ADRecipientSchema.PrimarySmtpAddress];
				if (obj == null)
				{
					return SmtpAddress.Empty;
				}
				return (SmtpAddress)obj;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00014968 File Offset: 0x00012B68
		internal string LegacyExchangeDN
		{
			get
			{
				return (string)this[ADRecipientSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001497A File Offset: 0x00012B7A
		internal string DisplayName
		{
			get
			{
				return (string)this[ADRecipientSchema.DisplayName];
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0001498C File Offset: 0x00012B8C
		internal ProxyAddress ExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)this[ADRecipientSchema.ExternalEmailAddress];
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001499E File Offset: 0x00012B9E
		internal string ServerLegacyDN
		{
			get
			{
				return (string)this[ADMailboxRecipientSchema.ServerLegacyDN];
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000149B0 File Offset: 0x00012BB0
		internal Guid MailboxGuid
		{
			get
			{
				return (Guid)this[ADMailboxRecipientSchema.ExchangeGuid];
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000149C4 File Offset: 0x00012BC4
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000149F1 File Offset: 0x00012BF1
		internal ExchangeObjectVersion ExchangeVersion
		{
			get
			{
				return (ExchangeObjectVersion)this[ADObjectSchema.ExchangeVersion];
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00014A03 File Offset: 0x00012C03
		internal Guid Guid
		{
			get
			{
				return (Guid)this[ADObjectSchema.Guid];
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00014A15 File Offset: 0x00012C15
		internal ADObjectId Id
		{
			get
			{
				return (ADObjectId)this[ADObjectSchema.Id];
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00014A28 File Offset: 0x00012C28
		internal int DistributionGroupMembersCount
		{
			get
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty = this[ADGroupSchema.Members] as MultiValuedProperty<ADObjectId>;
				if (multiValuedProperty != null)
				{
					return multiValuedProperty.Count;
				}
				return 0;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00014A51 File Offset: 0x00012C51
		internal int GroupMemberCount
		{
			get
			{
				return (int)this[ADGroupSchema.GroupMemberCount];
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00014A63 File Offset: 0x00012C63
		internal int GroupExternalMemberCount
		{
			get
			{
				return (int)this[ADGroupSchema.GroupExternalMemberCount];
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00014A75 File Offset: 0x00012C75
		internal MultiValuedProperty<ADObjectId> RejectMessagesFrom
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.RejectMessagesFrom];
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00014A87 File Offset: 0x00012C87
		internal MultiValuedProperty<ADObjectId> RejectMessagesFromDLMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.RejectMessagesFromDLMembers];
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00014A99 File Offset: 0x00012C99
		internal MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFrom
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.AcceptMessagesOnlyFrom];
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00014AAB File Offset: 0x00012CAB
		internal MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFromDLMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.AcceptMessagesOnlyFromDLMembers];
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00014ABD File Offset: 0x00012CBD
		internal MultiValuedProperty<ADObjectId> BypassModerationFrom
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.BypassModerationFrom];
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00014ACF File Offset: 0x00012CCF
		internal MultiValuedProperty<ADObjectId> BypassModerationFromDLMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.BypassModerationFromDLMembers];
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00014AE1 File Offset: 0x00012CE1
		internal MultiValuedProperty<ADObjectId> ModeratedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.ModeratedBy];
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00014AF3 File Offset: 0x00012CF3
		internal MultiValuedProperty<ADObjectId> ManagedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADGroupSchema.ManagedBy];
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00014B05 File Offset: 0x00012D05
		internal Unlimited<ByteQuantifiedSize> MaxReceiveSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADRecipientSchema.MaxReceiveSize];
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00014B17 File Offset: 0x00012D17
		internal OrganizationId OrganizationId
		{
			get
			{
				return (OrganizationId)this[ADObjectSchema.OrganizationId];
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00014B29 File Offset: 0x00012D29
		internal SecurityIdentifier Sid
		{
			get
			{
				return (SecurityIdentifier)this[ADMailboxRecipientSchema.Sid];
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00014B3B File Offset: 0x00012D3B
		internal SecurityIdentifier MasterAccountSid
		{
			get
			{
				return (SecurityIdentifier)this[ADRecipientSchema.MasterAccountSid];
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00014B4D File Offset: 0x00012D4D
		internal RawSecurityDescriptor ExchangeSecurityDescriptor
		{
			get
			{
				return (RawSecurityDescriptor)this[ADMailboxRecipientSchema.ExchangeSecurityDescriptor];
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00014B5F File Offset: 0x00012D5F
		internal MultiValuedProperty<string> MailTipTranslations
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.MailTipTranslations];
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00014B71 File Offset: 0x00012D71
		internal bool ModerationEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.ModerationEnabled];
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00014B84 File Offset: 0x00012D84
		internal bool IsDistributionGroup
		{
			get
			{
				switch (this.RecipientType)
				{
				case RecipientType.Group:
				case RecipientType.MailUniversalDistributionGroup:
				case RecipientType.MailUniversalSecurityGroup:
				case RecipientType.MailNonUniversalGroup:
				case RecipientType.DynamicDistributionGroup:
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00014BBC File Offset: 0x00012DBC
		internal bool IsIndividual
		{
			get
			{
				switch (this.RecipientType)
				{
				case RecipientType.User:
				case RecipientType.UserMailbox:
				case RecipientType.MailUser:
				case RecipientType.Contact:
				case RecipientType.MailContact:
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00014BF4 File Offset: 0x00012DF4
		internal bool IsRemoteMailbox
		{
			get
			{
				if (this.RecipientDisplayType == null)
				{
					return false;
				}
				RecipientDisplayType value = this.RecipientDisplayType.Value;
				RecipientDisplayType recipientDisplayType = value;
				if (recipientDisplayType <= Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.SyncedPublicFolder)
				{
					if (recipientDisplayType != Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.SyncedMailboxUser && recipientDisplayType != Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.SyncedPublicFolder)
					{
						return false;
					}
				}
				else if (recipientDisplayType != Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.SyncedConferenceRoomMailbox && recipientDisplayType != Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.SyncedEquipmentMailbox && recipientDisplayType != Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.ACLableSyncedMailboxUser)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00014C5C File Offset: 0x00012E5C
		internal ExchangePrincipal ExchangePrincipal
		{
			get
			{
				if (this.exchangePrincipal == null)
				{
					Stopwatch stopwatch = Stopwatch.StartNew();
					this.exchangePrincipal = ExchangePrincipal.FromAnyVersionMailboxData(this.DisplayName, this.MailboxGuid, this.MdbGuid, this.PrimarySmtpAddress.ToString(), this.LegacyExchangeDN, this.Id, this.RecipientType, this.MasterAccountSid, this.OrganizationId, RemotingOptions.AllowCrossSite, false);
					stopwatch.Stop();
					this.ExchangePrincipalLatency = stopwatch.ElapsedMilliseconds;
				}
				return this.exchangePrincipal;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00014CE0 File Offset: 0x00012EE0
		internal byte[] ThumbnailPhoto
		{
			get
			{
				return (byte[])this[ADRecipientSchema.ThumbnailPhoto];
			}
		}

		// Token: 0x1700010E RID: 270
		private object this[PropertyDefinition propertyDefinition]
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

		// Token: 0x060004B3 RID: 1203 RVA: 0x00014D0C File Offset: 0x00012F0C
		private void ParseConfigurableObject(ConfigurableObject configurableObject, ICollection<PropertyDefinition> propertyDefinitionCollection)
		{
			if (configurableObject == null)
			{
				ExTraceGlobals.RequestRoutingTracer.TraceDebug((long)this.GetHashCode(), "{0}: Trying to parse null configurable object.", new object[]
				{
					TraceContext.Get()
				});
				return;
			}
			if (0 < propertyDefinitionCollection.Count)
			{
				this.propertyMap = new Dictionary<PropertyDefinition, object>(propertyDefinitionCollection.Count);
				using (IEnumerator<PropertyDefinition> enumerator = propertyDefinitionCollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PropertyDefinition propertyDefinition = enumerator.Current;
						if (configurableObject[propertyDefinition] == null)
						{
							ExTraceGlobals.RequestRoutingTracer.TraceDebug<object, PropertyDefinition>((long)this.GetHashCode(), "{0}: {1} property was null.", TraceContext.Get(), propertyDefinition);
						}
						this.propertyMap[propertyDefinition] = configurableObject[propertyDefinition];
					}
					return;
				}
			}
			ExTraceGlobals.RequestRoutingTracer.TraceError<object, int>((long)this.GetHashCode(), "{0}: Property definition collection contains {1} property definitions, nothing is parsed in RecipientData.", TraceContext.Get(), propertyDefinitionCollection.Count);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00014DEC File Offset: 0x00012FEC
		private bool HasPropertyDefinition(PropertyDefinition propertyDefinition)
		{
			return this.propertyMap != null && 0 < this.propertyMap.Count && this.propertyMap.ContainsKey(propertyDefinition);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00014E14 File Offset: 0x00013014
		internal static RecipientData CreateAsContact(EmailAddress emailAddress)
		{
			RecipientData recipientData = new RecipientData(emailAddress);
			SmtpProxyAddress value = new SmtpProxyAddress(emailAddress.Address, true);
			recipientData.propertyMap = new Dictionary<PropertyDefinition, object>();
			recipientData.propertyMap[ADRecipientSchema.RecipientType] = RecipientType.MailUser;
			recipientData.propertyMap[ADRecipientSchema.ExternalEmailAddress] = value;
			SmtpAddress smtpAddress = new SmtpAddress(emailAddress.Address);
			recipientData.propertyMap[ADRecipientSchema.PrimarySmtpAddress] = smtpAddress;
			return recipientData;
		}

		// Token: 0x040002B8 RID: 696
		private Dictionary<PropertyDefinition, object> propertyMap;

		// Token: 0x040002B9 RID: 697
		private EmailAddress emailAddress;

		// Token: 0x040002BA RID: 698
		private EmailAddress initialEmailAddress;

		// Token: 0x040002BB RID: 699
		private StoreObjectId associatedFolderId;

		// Token: 0x040002BC RID: 700
		private Exception exception;

		// Token: 0x040002BD RID: 701
		private ExchangePrincipal exchangePrincipal;

		// Token: 0x040002BE RID: 702
		private ProviderError providerError;
	}
}
