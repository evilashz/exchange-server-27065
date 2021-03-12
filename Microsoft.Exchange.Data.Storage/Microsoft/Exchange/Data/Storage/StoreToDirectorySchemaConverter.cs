using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CD5 RID: 3285
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class StoreToDirectorySchemaConverter : SchemaConverter
	{
		// Token: 0x060071C2 RID: 29122 RVA: 0x001F7EFC File Offset: 0x001F60FC
		private StoreToDirectorySchemaConverter()
		{
			base.Add(ParticipantSchema.LegacyExchangeDN, new SchemaConverter.Getter(StoreToDirectorySchemaConverter.GetLegacyExchangeDN), null);
			base.Add(ParticipantSchema.SmtpAddress, new SchemaConverter.Getter(StoreToDirectorySchemaConverter.GetSmtpAddress), null);
			base.Add(InternalSchema.DisplayTypeExInternal, new SchemaConverter.Getter(StoreToDirectorySchemaConverter.GetDisplayTypeEx), null);
			base.Add(ParticipantSchema.DisplayName, new SchemaConverter.Getter(StoreToDirectorySchemaConverter.GetDisplayName), null);
			base.Add(ParticipantSchema.Alias, new SchemaConverter.Getter(StoreToDirectorySchemaConverter.GetAlias), null);
			base.Add(ParticipantSchema.SimpleDisplayName, new SchemaConverter.Getter(StoreToDirectorySchemaConverter.GetSimpleDisplayName), null);
			base.Add(ParticipantSchema.SipUri, new SchemaConverter.Getter(StoreToDirectorySchemaConverter.GetSipUri), null);
			base.Add(ParticipantSchema.ParticipantSID, new SchemaConverter.Getter(StoreToDirectorySchemaConverter.GetParticipantSid), null);
			base.Add(ParticipantSchema.ParticipantGuid, new SchemaConverter.Getter(StoreToDirectorySchemaConverter.GetGuid), null);
		}

		// Token: 0x060071C3 RID: 29123 RVA: 0x001F7FE7 File Offset: 0x001F61E7
		private static object GetLegacyExchangeDN(IReadOnlyPropertyBag propertyBag)
		{
			return StoreToDirectorySchemaConverter.DefaultToNotFound(propertyBag, ADRecipientSchema.LegacyExchangeDN);
		}

		// Token: 0x060071C4 RID: 29124 RVA: 0x001F7FF4 File Offset: 0x001F61F4
		private static object GetSmtpAddress(IReadOnlyPropertyBag propertyBag)
		{
			return ((SmtpAddress)propertyBag[ADRecipientSchema.PrimarySmtpAddress]).ToString();
		}

		// Token: 0x060071C5 RID: 29125 RVA: 0x001F8020 File Offset: 0x001F6220
		private static object GetDisplayTypeEx(IReadOnlyPropertyBag propertyBag)
		{
			RecipientDisplayType? recipientDisplayType = (RecipientDisplayType?)StoreToDirectorySchemaConverter.TryGetValueOrDefault(propertyBag, ADRecipientSchema.RecipientDisplayType);
			RecipientDisplayType? recipientDisplayType2 = (recipientDisplayType != null) ? new RecipientDisplayType?(recipientDisplayType.GetValueOrDefault()) : StoreToDirectorySchemaConverter.ToRecipientDisplayType((RecipientType)StoreToDirectorySchemaConverter.TryGetValueOrDefault(propertyBag, ADRecipientSchema.RecipientType));
			if (recipientDisplayType2 == null)
			{
				return PropertyErrorCode.NotFound;
			}
			return (int)recipientDisplayType2.Value;
		}

		// Token: 0x060071C6 RID: 29126 RVA: 0x001F8087 File Offset: 0x001F6287
		private static object GetDisplayName(IReadOnlyPropertyBag propertyBag)
		{
			return propertyBag[ADRecipientSchema.DisplayName] ?? PropertyErrorCode.NotFound;
		}

		// Token: 0x060071C7 RID: 29127 RVA: 0x001F809E File Offset: 0x001F629E
		private static object GetAlias(IReadOnlyPropertyBag propertyBag)
		{
			return StoreToDirectorySchemaConverter.DefaultToNotFound(propertyBag, ADRecipientSchema.Alias);
		}

		// Token: 0x060071C8 RID: 29128 RVA: 0x001F80AB File Offset: 0x001F62AB
		private static object GetSimpleDisplayName(IReadOnlyPropertyBag propertyBag)
		{
			return propertyBag[ADRecipientSchema.SimpleDisplayName] ?? PropertyErrorCode.NotFound;
		}

		// Token: 0x060071C9 RID: 29129 RVA: 0x001F80C4 File Offset: 0x001F62C4
		private static object GetSipUri(IReadOnlyPropertyBag propertyBag)
		{
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)propertyBag[ADRecipientSchema.EmailAddresses];
			foreach (ProxyAddress proxyAddress in proxyAddressCollection)
			{
				if (proxyAddress.PrefixString.Equals("sip", StringComparison.OrdinalIgnoreCase))
				{
					return proxyAddress.ProxyAddressString.ToLower();
				}
			}
			return " ";
		}

		// Token: 0x060071CA RID: 29130 RVA: 0x001F8144 File Offset: 0x001F6344
		private static object GetParticipantSid(IReadOnlyPropertyBag propertyBag)
		{
			SecurityIdentifier userSid = (SecurityIdentifier)propertyBag[ADMailboxRecipientSchema.Sid];
			SecurityIdentifier masterAccountSid = (SecurityIdentifier)propertyBag[ADRecipientSchema.MasterAccountSid];
			SecurityIdentifier securityIdentifier = IdentityHelper.CalculateEffectiveSid(userSid, masterAccountSid);
			if (securityIdentifier == null)
			{
				return PropertyErrorCode.NotFound;
			}
			byte[] array = new byte[securityIdentifier.BinaryLength];
			securityIdentifier.GetBinaryForm(array, 0);
			return array;
		}

		// Token: 0x060071CB RID: 29131 RVA: 0x001F81A0 File Offset: 0x001F63A0
		private static object GetGuid(IReadOnlyPropertyBag propertyBag)
		{
			if ((Guid)propertyBag[ADObjectSchema.Guid] == Guid.Empty)
			{
				return PropertyErrorCode.NotFound;
			}
			return ((Guid)propertyBag[ADObjectSchema.Guid]).ToByteArray();
		}

		// Token: 0x060071CC RID: 29132 RVA: 0x001F81E8 File Offset: 0x001F63E8
		private static object DefaultToNotFound(IReadOnlyPropertyBag properyBag, ADPropertyDefinition propDef)
		{
			object obj = properyBag[propDef];
			if (object.Equals(obj, propDef.DefaultValue))
			{
				return PropertyErrorCode.NotFound;
			}
			return obj;
		}

		// Token: 0x060071CD RID: 29133 RVA: 0x001F8214 File Offset: 0x001F6414
		private static object TryGetValueOrDefault(IReadOnlyPropertyBag properyBag, ADPropertyDefinition propDef)
		{
			object result;
			try
			{
				result = properyBag[propDef];
			}
			catch (ValueNotPresentException)
			{
				result = propDef.DefaultValue;
			}
			return result;
		}

		// Token: 0x060071CE RID: 29134 RVA: 0x001F8248 File Offset: 0x001F6448
		private static RecipientDisplayType? ToRecipientDisplayType(RecipientType recipientType)
		{
			switch (recipientType)
			{
			case RecipientType.UserMailbox:
			case RecipientType.MailUser:
				return new RecipientDisplayType?(RecipientDisplayType.MailboxUser);
			case RecipientType.MailContact:
				return new RecipientDisplayType?(RecipientDisplayType.RemoteMailUser);
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailNonUniversalGroup:
				return new RecipientDisplayType?(RecipientDisplayType.DistributionGroup);
			case RecipientType.MailUniversalSecurityGroup:
				return new RecipientDisplayType?(RecipientDisplayType.SecurityDistributionGroup);
			case RecipientType.DynamicDistributionGroup:
				return new RecipientDisplayType?(RecipientDisplayType.DynamicDistributionGroup);
			case RecipientType.PublicFolder:
				return new RecipientDisplayType?(RecipientDisplayType.PublicFolder);
			}
			return null;
		}

		// Token: 0x04004F0E RID: 20238
		internal static readonly StoreToDirectorySchemaConverter Instance = new StoreToDirectorySchemaConverter();
	}
}
