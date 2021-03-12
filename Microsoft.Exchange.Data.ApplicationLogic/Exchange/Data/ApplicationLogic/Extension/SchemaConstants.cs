using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000130 RID: 304
	internal static class SchemaConstants
	{
		// Token: 0x04000663 RID: 1635
		public const string Xsd15FileName = "ExtensionManifestSchema.15.xsd";

		// Token: 0x04000664 RID: 1636
		public const string Xsd15_1FileName = "ExtensionManifestSchema.15.1.xsd";

		// Token: 0x04000665 RID: 1637
		public const string OweNamespacePrefix1_0 = "owe1_0";

		// Token: 0x04000666 RID: 1638
		public const string OweNamespacePrefix1_1 = "owe1_1";

		// Token: 0x04000667 RID: 1639
		public const string OweNamespaceUri1_0 = "http://schemas.microsoft.com/office/appforoffice/1.0";

		// Token: 0x04000668 RID: 1640
		public const string OweNamespaceUri1_1 = "http://schemas.microsoft.com/office/appforoffice/1.1";

		// Token: 0x04000669 RID: 1641
		public const string OweNamespaceUriNonVersionSpecificPart = "http://schemas.microsoft.com/office/appforoffice/";

		// Token: 0x0400066A RID: 1642
		public const string XsiNamespacePrefix = "xsi";

		// Token: 0x0400066B RID: 1643
		public const string XsiNamespaceUri = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x0400066C RID: 1644
		public const string ItemHasRegularExpressionMatchRuleType = "ItemHasRegularExpressionMatch";

		// Token: 0x0400066D RID: 1645
		public const string ItemIsRuleType = "ItemIs";

		// Token: 0x0400066E RID: 1646
		public const string RuleCollectionRuleType = "RuleCollection";

		// Token: 0x0400066F RID: 1647
		public const string ItemHasKnownEntityRuleType = "ItemHasKnownEntity";

		// Token: 0x04000670 RID: 1648
		public const string ItemHasAttachmentRuleType = "ItemHasAttachment";

		// Token: 0x04000671 RID: 1649
		public const string EntityTypeAttributeName = "EntityType";

		// Token: 0x04000672 RID: 1650
		public const string RegExFilterAttributeName = "RegExFilter";

		// Token: 0x04000673 RID: 1651
		public const string RegExValueAttributeName = "RegExValue";

		// Token: 0x04000674 RID: 1652
		public const string FilterNameAttributeName = "FilterName";

		// Token: 0x04000675 RID: 1653
		public const string IgnoreCaseAttributeName = "IgnoreCase";

		// Token: 0x04000676 RID: 1654
		public const string DefaultValueAttributeName = "DefaultValue";

		// Token: 0x04000677 RID: 1655
		public const string ValueAttributeName = "Value";

		// Token: 0x04000678 RID: 1656
		public const string LocaleAttributeName = "Locale";

		// Token: 0x04000679 RID: 1657
		public const string ItemClassAttributeName = "ItemClass";

		// Token: 0x0400067A RID: 1658
		public const string IncludeSubClassesAttributeName = "IncludeSubClasses";

		// Token: 0x0400067B RID: 1659
		public const string ItemIsRuleFormTypeAttributeName = "FormType";

		// Token: 0x0400067C RID: 1660
		public const string OfficeAppElementName = "OfficeApp";

		// Token: 0x0400067D RID: 1661
		public const string IconUrlElementName = "IconUrl";

		// Token: 0x0400067E RID: 1662
		public const string HighResolutionIconUrlElementName = "HighResolutionIconUrl";

		// Token: 0x0400067F RID: 1663
		public const string SourceLocationElementName = "SourceLocation";

		// Token: 0x04000680 RID: 1664
		public const string RequirementsElementName = "Requirements";

		// Token: 0x04000681 RID: 1665
		public const string SetsElementName = "Sets";

		// Token: 0x04000682 RID: 1666
		public const string SetElementName = "Set";

		// Token: 0x04000683 RID: 1667
		public const string SetElementNameAttributeName = "Name";

		// Token: 0x04000684 RID: 1668
		public const string SetElementMinVersionAttributeName = "MinVersion";

		// Token: 0x04000685 RID: 1669
		public const string RequiredSetNameForOutlookApp = "Mailbox";

		// Token: 0x04000686 RID: 1670
		public const string HostsElementName = "Hosts";

		// Token: 0x04000687 RID: 1671
		public const string HostElementName = "Host";

		// Token: 0x04000688 RID: 1672
		public const string HostElementNameAttributeName = "Name";

		// Token: 0x04000689 RID: 1673
		public const string HostExpectedValueForOutlookApp = "Mailbox";

		// Token: 0x0400068A RID: 1674
		public const string DefaultMinVersionAttributeName = "DefaultMinVersion";

		// Token: 0x0400068B RID: 1675
		public const string DefaultMinVersionAttributeValue = "1.1";

		// Token: 0x0400068C RID: 1676
		public const string IdElementName = "Id";

		// Token: 0x0400068D RID: 1677
		public const string PermissionsElementName = "Permissions";

		// Token: 0x0400068E RID: 1678
		public const string RuleElementName = "Rule";

		// Token: 0x0400068F RID: 1679
		public const string OverrideChildElementName = "Override";

		// Token: 0x04000690 RID: 1680
		public const string XsiTypeAttributeName = "type";

		// Token: 0x04000691 RID: 1681
		public const string RequestedHeightElementName = "RequestedHeight";

		// Token: 0x04000692 RID: 1682
		public const string PhoneSettingsElementName = "PhoneSettings";

		// Token: 0x04000693 RID: 1683
		public const string TabletSettingsElementName = "TabletSettings";

		// Token: 0x04000694 RID: 1684
		public const string DesktopSettingsElementName = "DesktopSettings";

		// Token: 0x04000695 RID: 1685
		public const string DisableEntityHighlightingElementName = "DisableEntityHighlighting";

		// Token: 0x04000696 RID: 1686
		public static readonly Dictionary<string, string> SchemaNamespaceUriToFile = new Dictionary<string, string>(StringComparer.Ordinal)
		{
			{
				"http://schemas.microsoft.com/office/appforoffice/1.0",
				"ExtensionManifestSchema.15.xsd"
			},
			{
				"http://schemas.microsoft.com/office/appforoffice/1.1",
				"ExtensionManifestSchema.15.1.xsd"
			}
		};

		// Token: 0x04000697 RID: 1687
		public static readonly Version SchemaVersion1_0 = new Version(1, 0);

		// Token: 0x04000698 RID: 1688
		public static readonly Version SchemaVersion1_1 = new Version(1, 1);

		// Token: 0x04000699 RID: 1689
		public static readonly Version LowestApiVersionSupportedBySchemaVersion1_1 = new Version(1, 1);

		// Token: 0x0400069A RID: 1690
		public static readonly Version HighestSupportedApiVersion = new Version(1, 1);

		// Token: 0x0400069B RID: 1691
		public static readonly Version Exchange2013RtmApiVersion = new Version(1, 0);
	}
}
