using System;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class EwsAssociationDataConverter
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00009F94 File Offset: 0x00008194
		internal static MailboxAssociationType Convert(MailboxAssociation association)
		{
			ArgumentValidator.ThrowIfNull("association", association);
			return new MailboxAssociationType
			{
				User = EwsAssociationDataConverter.Convert(association.User),
				Group = EwsAssociationDataConverter.Convert(association.Group),
				IsMember = association.IsMember,
				IsMemberSpecified = true,
				IsPin = association.IsPin,
				IsPinSpecified = true,
				JoinDate = (DateTime)association.JoinDate,
				JoinDateSpecified = true,
				JoinedBy = association.JoinedBy
			};
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000A020 File Offset: 0x00008220
		internal static MailboxAssociation Convert(MailboxAssociationType associationType, IRecipientSession adSession)
		{
			ArgumentValidator.ThrowIfNull("associationType", associationType);
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			return new MailboxAssociation
			{
				Group = EwsAssociationDataConverter.Convert(associationType.Group, adSession),
				User = EwsAssociationDataConverter.Convert(associationType.User, adSession),
				IsMember = associationType.IsMember,
				IsPin = associationType.IsPin,
				JoinDate = (ExDateTime)associationType.JoinDate,
				JoinedBy = associationType.JoinedBy
			};
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000A0A4 File Offset: 0x000082A4
		internal static GroupLocatorType Convert(GroupMailboxLocator locator)
		{
			ArgumentValidator.ThrowIfNull("locator", locator);
			return new GroupLocatorType
			{
				ExternalDirectoryObjectId = locator.ExternalId,
				LegacyDn = locator.LegacyDn
			};
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000A0DB File Offset: 0x000082DB
		internal static GroupMailboxLocator Convert(GroupLocatorType locatorType, IRecipientSession adSession)
		{
			ArgumentValidator.ThrowIfNull("locatorType", locatorType);
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			return new GroupMailboxLocator(adSession, locatorType.ExternalDirectoryObjectId, locatorType.LegacyDn);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000A108 File Offset: 0x00008308
		internal static UserLocatorType Convert(UserMailboxLocator locator)
		{
			ArgumentValidator.ThrowIfNull("locator", locator);
			return new UserLocatorType
			{
				ExternalDirectoryObjectId = locator.ExternalId,
				LegacyDn = locator.LegacyDn
			};
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000A13F File Offset: 0x0000833F
		internal static UserMailboxLocator Convert(UserLocatorType locatorType, IRecipientSession adSession)
		{
			ArgumentValidator.ThrowIfNull("locatorType", locatorType);
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			return new UserMailboxLocator(adSession, locatorType.ExternalDirectoryObjectId, locatorType.LegacyDn);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000A16C File Offset: 0x0000836C
		internal static MailboxLocatorType Convert(IMailboxLocator locator)
		{
			ArgumentValidator.ThrowIfNull("locator", locator);
			GroupMailboxLocator groupMailboxLocator = locator as GroupMailboxLocator;
			if (groupMailboxLocator != null)
			{
				return EwsAssociationDataConverter.Convert(groupMailboxLocator);
			}
			UserMailboxLocator userMailboxLocator = locator as UserMailboxLocator;
			if (userMailboxLocator != null)
			{
				return EwsAssociationDataConverter.Convert(userMailboxLocator);
			}
			throw new NotImplementedException(string.Format("Conversion of '{0}' is not yet supported.", locator.GetType()));
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A1BC File Offset: 0x000083BC
		internal static MailboxLocator Convert(MailboxLocatorType locatorType, IRecipientSession adSession)
		{
			ArgumentValidator.ThrowIfNull("locatorType", locatorType);
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			GroupLocatorType groupLocatorType = locatorType as GroupLocatorType;
			if (groupLocatorType != null)
			{
				return EwsAssociationDataConverter.Convert(groupLocatorType, adSession);
			}
			UserLocatorType userLocatorType = locatorType as UserLocatorType;
			if (userLocatorType != null)
			{
				return EwsAssociationDataConverter.Convert(userLocatorType, adSession);
			}
			throw new InvalidOperationException("Unsupported type of Mailbox Locator");
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A210 File Offset: 0x00008410
		internal static ModernGroupTypeType Convert(ModernGroupObjectType type)
		{
			switch (type)
			{
			case ModernGroupObjectType.Private:
				return ModernGroupTypeType.Private;
			case ModernGroupObjectType.Secret:
				return ModernGroupTypeType.Secret;
			case ModernGroupObjectType.Public:
				return ModernGroupTypeType.Public;
			}
			return ModernGroupTypeType.None;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A240 File Offset: 0x00008440
		internal static ModernGroupObjectType Convert(ModernGroupTypeType type)
		{
			switch (type)
			{
			case ModernGroupTypeType.Private:
				return ModernGroupObjectType.Private;
			case ModernGroupTypeType.Secret:
				return ModernGroupObjectType.Secret;
			case ModernGroupTypeType.Public:
				return ModernGroupObjectType.Public;
			}
			return ModernGroupObjectType.None;
		}

		// Token: 0x0400009F RID: 159
		private static readonly Trace Tracer = ExTraceGlobals.AssociationReplicationTracer;
	}
}
