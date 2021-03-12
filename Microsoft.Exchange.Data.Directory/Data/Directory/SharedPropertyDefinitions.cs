using System;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200019B RID: 411
	internal static class SharedPropertyDefinitions
	{
		// Token: 0x06001186 RID: 4486 RVA: 0x000546FD File Offset: 0x000528FD
		internal static MultiValuedProperty<Capability> PersistedCapabilitiesGetter(IPropertyBag propertyBag)
		{
			return (MultiValuedProperty<Capability>)propertyBag[SharedPropertyDefinitions.RawCapabilities];
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0005470F File Offset: 0x0005290F
		internal static void PersistedCapabilitiesSetter(object capabilitiesValue, IPropertyBag propertyBag)
		{
			propertyBag[SharedPropertyDefinitions.RawCapabilities] = capabilitiesValue;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00054720 File Offset: 0x00052920
		internal static QueryFilter CapabilitiesFilterBuilder(SinglePropertyFilter filter)
		{
			if (filter is ComparisonFilter)
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				return new ComparisonFilter(comparisonFilter.ComparisonOperator, SharedPropertyDefinitions.RawCapabilities, comparisonFilter.PropertyValue);
			}
			throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00054778 File Offset: 0x00052978
		internal static QueryFilter ProvisioningFlagsFilterBuilder(DatabaseProvisioningFlags provisioningFlag, SinglePropertyFilter filter)
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
			QueryFilter queryFilter = new BitMaskAndFilter(SharedPropertyDefinitions.ProvisioningFlags, (ulong)((long)provisioningFlag));
			if ((comparisonFilter.ComparisonOperator == ComparisonOperator.Equal && (bool)comparisonFilter.PropertyValue) || (ComparisonOperator.NotEqual == comparisonFilter.ComparisonOperator && !(bool)comparisonFilter.PropertyValue))
			{
				return queryFilter;
			}
			return new NotFilter(queryFilter);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00054831 File Offset: 0x00052A31
		internal static QueryFilter IsOutOfServiceFilterBuilder(SinglePropertyFilter filter)
		{
			return SharedPropertyDefinitions.ProvisioningFlagsFilterBuilder(DatabaseProvisioningFlags.IsOutOfService, filter);
		}

		// Token: 0x040009EF RID: 2543
		public const string PrintableStringValidCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789\"'()+,-./:? ";

		// Token: 0x040009F0 RID: 2544
		internal static readonly ADPropertyDefinition ADAllowedFileTypes = new ADPropertyDefinition("ADAllowedFileTypes", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWAAllowedFileTypes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009F1 RID: 2545
		internal static readonly ADPropertyDefinition ADAllowedMimeTypes = new ADPropertyDefinition("ADAllowedMimeTypes", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWAAllowedMimeTypes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009F2 RID: 2546
		internal static readonly ADPropertyDefinition ADBlockedFileTypes = new ADPropertyDefinition("ADBlockedFileTypes", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWABlockedFileTypes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009F3 RID: 2547
		internal static readonly ADPropertyDefinition ADBlockedMimeTypes = new ADPropertyDefinition("ADBlockedMimeTypes", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWABlockedMimeTypes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009F4 RID: 2548
		internal static readonly ADPropertyDefinition ADForceSaveFileTypes = new ADPropertyDefinition("ADForceSaveFileTypes", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWAForceSaveFileTypes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009F5 RID: 2549
		internal static readonly ADPropertyDefinition ADForceSaveMimeTypes = new ADPropertyDefinition("ADForceSaveMimeTypes", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWAForceSaveMimeTypes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009F6 RID: 2550
		internal static readonly ADPropertyDefinition ADWebReadyFileTypes = new ADPropertyDefinition("ADWebReadyFileTypes", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWATranscodingFileTypes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009F7 RID: 2551
		internal static readonly ADPropertyDefinition ADWebReadyMimeTypes = new ADPropertyDefinition("ADWebReadyMimeTypes", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWATranscodingMimeTypes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009F8 RID: 2552
		public static readonly ADPropertyDefinition AllowedInCountryOrRegionGroups = new ADPropertyDefinition("AllowedInCountryOrRegionGroups", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchUMAllowedInCountryGroups", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009F9 RID: 2553
		public static readonly ADPropertyDefinition AllowedInternationalGroups = new ADPropertyDefinition("AllowedInternationalGroups", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchUMAllowedInternationalGroups", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009FA RID: 2554
		public static readonly ADPropertyDefinition BypassedRecipients = new ADPropertyDefinition("BypassedRecipients", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress), "msExchMessageHygieneBypassedRecipient", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 320),
			new ValidSmtpAddressConstraint()
		}, null, null);

		// Token: 0x040009FB RID: 2555
		public static readonly ADPropertyDefinition RawCapabilities = new ADPropertyDefinition("RawCapabilities", ExchangeObjectVersion.Exchange2003, typeof(Capability), "msExchCapabilityIdentifiers", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009FC RID: 2556
		public static readonly ADPropertyDefinition Capabilities = new ADPropertyDefinition("Capabilities", ExchangeObjectVersion.Exchange2003, typeof(Capability), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009FD RID: 2557
		public static readonly ADPropertyDefinition Comment = new ADPropertyDefinition("Comment", ExchangeObjectVersion.Exchange2007, typeof(string), "adminDescription", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 255)
		}, null, null);

		// Token: 0x040009FE RID: 2558
		public static readonly ADPropertyDefinition ContactAddressLists = new ADPropertyDefinition("ContactAddressLists", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchUMQueryBaseDN", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040009FF RID: 2559
		public static readonly ADPropertyDefinition Cookie = new ADPropertyDefinition("Cookie", ExchangeObjectVersion.Exchange2003, typeof(byte[]), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000A00 RID: 2560
		public static readonly ADPropertyDefinition CopyEdbFilePath = new ADPropertyDefinition("CopyEdbFilePath", ExchangeObjectVersion.Exchange2007, typeof(EdbFilePath), "msExchCopyEDBFile", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint(),
			new AsciiCharactersOnlyConstraint()
		}, null, null);

		// Token: 0x04000A01 RID: 2561
		public static readonly ADPropertyDefinition Description = new ADPropertyDefinition("Description", ExchangeObjectVersion.Exchange2003, typeof(string), "description", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, null, null);

		// Token: 0x04000A02 RID: 2562
		public static readonly ADPropertyDefinition RawDescription = new ADPropertyDefinition("RawDescription", ExchangeObjectVersion.Exchange2003, typeof(string), "description", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A03 RID: 2563
		public static readonly ADPropertyDefinition EdbFilePath = new ADPropertyDefinition("EdbFilePath", ExchangeObjectVersion.Exchange2003, typeof(EdbFilePath), "msExchEDBFile", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint()
		}, null, null);

		// Token: 0x04000A04 RID: 2564
		public static readonly ADPropertyDefinition EdgeSyncCookies = new ADPropertyDefinition("EdgeSyncCookies", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchEdgeSyncCookies", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A05 RID: 2565
		public static readonly ADPropertyDefinition ElcFlags = new ADPropertyDefinition("ELCFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchELCFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A06 RID: 2566
		public static readonly ADPropertyDefinition EndOfList = new ADPropertyDefinition("EndOfList", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000A07 RID: 2567
		public static readonly ADPropertyDefinition ExchangeLegacyDN = new ADPropertyDefinition("ExchangeLegacyDN", ExchangeObjectVersion.Exchange2003, typeof(string), "legacyExchangeDN", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, new PropertyDefinitionConstraint[]
		{
			new ValidLegacyDNConstraint()
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A08 RID: 2568
		public static readonly ADPropertyDefinition InfoAnnouncementFilename = new ADPropertyDefinition("InfoAnnouncementFilename", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchUMInfoAnnouncementFile", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 255),
			new RegexConstraint("^$|\\.wav|\\.wma$", RegexOptions.IgnoreCase, DataStrings.CustomGreetingFilePatternDescription)
		}, null, null);

		// Token: 0x04000A09 RID: 2569
		public static readonly ADPropertyDefinition JournalRecipient = new ADPropertyDefinition("JournalRecipient", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchMessageJournalRecipient", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A0A RID: 2570
		public static readonly ADPropertyDefinition LastUpdatedRecipientFilter = new ADPropertyDefinition("LastUpdatedRecipientFilter", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchLastAppliedRecipientFilter", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A0B RID: 2571
		public static readonly ADPropertyDefinition LdapRecipientFilter = new ADPropertyDefinition("LdapRecipientFilter", ExchangeObjectVersion.Exchange2003, typeof(string), "purportedSearch", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A0C RID: 2572
		public static readonly ADPropertyDefinition LegacyExchangeDN = new ADPropertyDefinition("LegacyExchangeDN", ExchangeObjectVersion.Exchange2003, typeof(string), "legacyExchangeDN", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A0D RID: 2573
		public static readonly ADPropertyDefinition LocalizedComment = new ADPropertyDefinition("LocalizedComment", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchELCAdminDescriptionLocalized", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A0E RID: 2574
		public static readonly ADPropertyDefinition MailboxMoveTargetMDB = new ADPropertyDefinition("MailboxMoveTargetMDB", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, "msExchMailboxMoveTargetMDBLink", null, "msExchMailboxMoveTargetMDBLinkSL", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000A0F RID: 2575
		public static readonly ADPropertyDefinition MailboxMoveSourceMDB = new ADPropertyDefinition("MailboxMoveSourceMDB", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, "msExchMailboxMoveSourceMDBLink", null, "msExchMailboxMoveSourceMDBLinkSL", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000A10 RID: 2576
		public static readonly ADPropertyDefinition MailboxMoveFlags = new ADPropertyDefinition("MailboxMoveFlags", ExchangeObjectVersion.Exchange2003, typeof(RequestFlags), "msExchMailboxMoveFlags", ADPropertyDefinitionFlags.None, RequestFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A11 RID: 2577
		public static readonly ADPropertyDefinition MailboxMoveRemoteHostName = new ADPropertyDefinition("MailboxMoveRemoteHostName", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchMailboxMoveRemoteHostName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.MailboxMoveRemoteHostName);

		// Token: 0x04000A12 RID: 2578
		public static readonly ADPropertyDefinition MailboxMoveBatchName = new ADPropertyDefinition("MailboxMoveBatchName", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchMailboxMoveBatchName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.MailboxMoveBatchName);

		// Token: 0x04000A13 RID: 2579
		public static readonly ADPropertyDefinition MailboxMoveStatus = new ADPropertyDefinition("MailboxMoveStatus", ExchangeObjectVersion.Exchange2003, typeof(RequestStatus), "msExchMailboxMoveStatus", ADPropertyDefinitionFlags.None, RequestStatus.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.MailboxMoveStatus);

		// Token: 0x04000A14 RID: 2580
		public static readonly ADPropertyDefinition MailboxRelease = new ADPropertyDefinition("MailboxRelease", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchMailboxRelease", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.MailboxRelease);

		// Token: 0x04000A15 RID: 2581
		public static readonly ADPropertyDefinition ArchiveRelease = new ADPropertyDefinition("ArchiveRelease", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchArchiveRelease", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.ArchiveRelease);

		// Token: 0x04000A16 RID: 2582
		public static readonly ADPropertyDefinition MailboxPublicFolderDatabase = new ADPropertyDefinition("PublicFolderDatabase", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchHomePublicMDB", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A17 RID: 2583
		public static readonly ADPropertyDefinition MaintenanceScheduleBitmaps = new ADPropertyDefinition("MaintenanceScheduleBitmaps", ExchangeObjectVersion.Exchange2003, typeof(Schedule), "activationSchedule", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A18 RID: 2584
		public static readonly ADPropertyDefinition ManagedObjects = new ADPropertyDefinition("ManagedObjects", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "managedObjects", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A19 RID: 2585
		public static readonly ADPropertyDefinition MandatoryDisplayName = new ADPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2003, typeof(string), "displayName", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new MandatoryStringLengthConstraint(1, 256)
		}, null, null);

		// Token: 0x04000A1A RID: 2586
		public static readonly ADPropertyDefinition OfflineAddressBook = new ADPropertyDefinition("OfflineAddressBook", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchUseOAB", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A1B RID: 2587
		public static readonly ADPropertyDefinition OptionalDisplayName = new ADPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2003, typeof(string), "displayName", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A1C RID: 2588
		public static readonly ADPropertyDefinition OrgLeadersBL = new ADPropertyDefinition("OrgLeadersBL", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msOrg-LeadersBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A1D RID: 2589
		public static readonly ADPropertyDefinition OriginalDatabase = new ADPropertyDefinition("OriginalDatabase", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchOrigMDB", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A1E RID: 2590
		public static readonly ADPropertyDefinition OtherWellKnownObjects = new ADPropertyDefinition("OtherWellKnownObjects", ExchangeObjectVersion.Exchange2003, typeof(DNWithBinary), "otherWellKnownObjects", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A1F RID: 2591
		public static readonly ADPropertyDefinition PublicFolderDefaultAdminAcl = new ADPropertyDefinition("PublicFolderDefaultAdminAcl", ExchangeObjectVersion.Exchange2003, typeof(RawSecurityDescriptor), "msExchPFDefaultAdminACL", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A20 RID: 2592
		public static readonly ADPropertyDefinition PublicFolderHierarchy = new ADPropertyDefinition("PublicFolderHierarchy", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchOwningPFTree", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A21 RID: 2593
		public static readonly ADPropertyDefinition PurportedSearchUI = new ADPropertyDefinition("PurportedSearchUI", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchPurportedSearchUI", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A22 RID: 2594
		public static readonly ADPropertyDefinition QuotaNotificationScheduleBitmaps = new ADPropertyDefinition("QuotaNotificationScheduleBitmaps", ExchangeObjectVersion.Exchange2003, typeof(Schedule), "quotaNotificationSchedule", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A23 RID: 2595
		public static readonly ADPropertyDefinition RecipientContainer = new ADPropertyDefinition("RecipientContainer", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchSearchBase", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A24 RID: 2596
		public static readonly ADPropertyDefinition RecipientFilter = new ADPropertyDefinition("RecipientFilter", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchQueryFilter", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A25 RID: 2597
		public static readonly ADPropertyDefinition RecipientFilterMetadata = new ADPropertyDefinition("RecipientFilterMetadata", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchQueryFilterMetadata", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A26 RID: 2598
		public static readonly ADPropertyDefinition ReplicationScheduleBitmaps = new ADPropertyDefinition("ReplicationScheduleBitmaps", ExchangeObjectVersion.Exchange2003, typeof(Schedule), "msExchReplicationSchedule", ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.Binary, Schedule.Always, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A27 RID: 2599
		public static readonly ADPropertyDefinition Server = new ADPropertyDefinition("Server", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchOwningServer", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A28 RID: 2600
		public static readonly ADPropertyDefinition SimpleDisplayName = new ADPropertyDefinition("SimpleDisplayName", ExchangeObjectVersion.Exchange2003, typeof(string), "displayNamePrintable", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 256),
			new CharacterConstraint("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789\"'()+,-./:? ".ToCharArray(), true)
		}, null, MbxRecipientSchema.SimpleDisplayName);

		// Token: 0x04000A29 RID: 2601
		public static readonly ADPropertyDefinition SitePublicFolderDatabase = new ADPropertyDefinition("PublicFolderDatabase", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "siteFolderServer", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A2A RID: 2602
		public static readonly ADPropertyDefinition UPNSuffixes = new ADPropertyDefinition("UPNSuffixes", ExchangeObjectVersion.Exchange2003, typeof(string), "uPNSuffixes", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A2B RID: 2603
		public static readonly ADPropertyDefinition ProvisioningFlags = new ADPropertyDefinition("ProvisioningFlags", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchProvisioningFlags", ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.DoNotProvisionalClone, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A2C RID: 2604
		public static readonly ADPropertyDefinition IsOutOfService = new ADPropertyDefinition("IsOutOfService", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SharedPropertyDefinitions.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(SharedPropertyDefinitions.IsOutOfServiceFilterBuilder), ADObject.FlagGetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 8), ADObject.FlagSetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 8), null, null);

		// Token: 0x04000A2D RID: 2605
		public static readonly ADPropertyDefinition PersistedCapabilities = new ADPropertyDefinition("PersistedCapabilities", ExchangeObjectVersion.Exchange2003, typeof(Capability), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
		{
			new CollectionDelegateConstraint(new CollectionValidationDelegate(ConstraintDelegates.ValidateCapabilities))
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SharedPropertyDefinitions.RawCapabilities
		}, new CustomFilterBuilderDelegate(SharedPropertyDefinitions.CapabilitiesFilterBuilder), new GetterDelegate(SharedPropertyDefinitions.PersistedCapabilitiesGetter), new SetterDelegate(SharedPropertyDefinitions.PersistedCapabilitiesSetter), null, MbxRecipientSchema.PersistedCapabilities);

		// Token: 0x04000A2E RID: 2606
		public static readonly ADPropertyDefinition UsnChanged = new ADPropertyDefinition("UsnChanged", ExchangeObjectVersion.Exchange2003, typeof(long), "uSNChanged", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000A2F RID: 2607
		public static readonly ADPropertyDefinition FfoExpansionSizeUpperBoundFilter = new ADPropertyDefinition("FfoExpansionSizeUpperBound", ExchangeObjectVersion.Exchange2003, typeof(int?), "FfoExpansionSizeUpperBound", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
