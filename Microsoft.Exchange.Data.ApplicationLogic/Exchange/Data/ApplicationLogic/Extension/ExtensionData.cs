using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x020000F6 RID: 246
	internal class ExtensionData : ICloneable
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002712C File Offset: 0x0002532C
		public static string ClientFullVersion
		{
			get
			{
				string installedOwaVersion = DefaultExtensionTable.GetInstalledOwaVersion();
				if (installedOwaVersion == null)
				{
					return installedOwaVersion;
				}
				return installedOwaVersion.Replace('.', 'd');
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002714E File Offset: 0x0002534E
		public static string OfficeCallBackUrl
		{
			get
			{
				return "~/Extension/installFromURL.slab";
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00027158 File Offset: 0x00025358
		public static string ConfigServiceUrl
		{
			get
			{
				if (ExtensionData.configServiceUrl == null)
				{
					string text = ConfigurationManager.AppSettings["MarketplaceConfigServiceUrl"];
					ExtensionData.configServiceUrl = (string.IsNullOrWhiteSpace(text) ? "https://o15.officeredir.microsoft.com/r/rlidMktplcWSConfig15" : text);
				}
				return ExtensionData.configServiceUrl;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00027198 File Offset: 0x00025398
		public static string LandingPageUrl
		{
			get
			{
				if (ExtensionData.landingPageUrl == null)
				{
					string text = ConfigurationManager.AppSettings["MarketplaceLandingPageUrl"];
					ExtensionData.landingPageUrl = (string.IsNullOrWhiteSpace(text) ? "https://o15.officeredir.microsoft.com/r/rlidMktplcExchRedirect" : text);
				}
				return ExtensionData.landingPageUrl;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x000271D8 File Offset: 0x000253D8
		public static string MyAppsPageUrl
		{
			get
			{
				if (ExtensionData.myAppsPageUrl == null)
				{
					string text = ConfigurationManager.AppSettings["MarketplaceMyAppsPageUrl"];
					ExtensionData.myAppsPageUrl = (string.IsNullOrWhiteSpace(text) ? "https://o15.officeredir.microsoft.com/r/rlidMktplcMUXMyOfficeApps" : text);
				}
				return ExtensionData.myAppsPageUrl;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00027216 File Offset: 0x00025416
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0002721E File Offset: 0x0002541E
		public string MarketplaceContentMarket { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00027227 File Offset: 0x00025427
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0002722F File Offset: 0x0002542F
		public string MarketplaceAssetID { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00027238 File Offset: 0x00025438
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00027240 File Offset: 0x00025440
		public string ProviderName { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00027249 File Offset: 0x00025449
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00027251 File Offset: 0x00025451
		public Uri IconURL { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002725A File Offset: 0x0002545A
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0002726C File Offset: 0x0002546C
		public Uri HighResolutionIconURL
		{
			get
			{
				return this.highResolutionIconURL ?? this.IconURL;
			}
			private set
			{
				this.highResolutionIconURL = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00027275 File Offset: 0x00025475
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0002727D File Offset: 0x0002547D
		public string ExtensionId { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00027286 File Offset: 0x00025486
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x0002728E File Offset: 0x0002548E
		public string Etoken { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00027297 File Offset: 0x00025497
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x0002729F File Offset: 0x0002549F
		public string AppStatus { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x000272A8 File Offset: 0x000254A8
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x000272B0 File Offset: 0x000254B0
		public EntitlementTokenData EtokenData { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x000272B9 File Offset: 0x000254B9
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x000272C1 File Offset: 0x000254C1
		public string VersionAsString
		{
			get
			{
				return this.versionAsString;
			}
			set
			{
				this.versionAsString = value;
				if (!ExtensionData.TryParseVersion(this.versionAsString, out this.version))
				{
					ExtensionData.Tracer.TraceError<string>(0L, "ExtensionData.VersionAsString: TryParseVersion failed for: {0}", this.versionAsString);
				}
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x000272F4 File Offset: 0x000254F4
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x000272FC File Offset: 0x000254FC
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x00027304 File Offset: 0x00025504
		public ExtensionType? Type { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0002730D File Offset: 0x0002550D
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x00027315 File Offset: 0x00025515
		public ExtensionInstallScope? Scope { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002731E File Offset: 0x0002551E
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x00027326 File Offset: 0x00025526
		public string DisplayName { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002732F File Offset: 0x0002552F
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x00027337 File Offset: 0x00025537
		public string Description { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00027340 File Offset: 0x00025540
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x00027348 File Offset: 0x00025548
		public bool Enabled { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00027351 File Offset: 0x00025551
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00027359 File Offset: 0x00025559
		public DisableReasonType DisableReason { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00027362 File Offset: 0x00025562
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0002736A File Offset: 0x0002556A
		public string IdentityAndEwsTokenId { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00027373 File Offset: 0x00025573
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x0002737B File Offset: 0x0002557B
		public RequestedCapabilities? RequestedCapabilities { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00027384 File Offset: 0x00025584
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x0002738C File Offset: 0x0002558C
		public SafeXmlDocument Manifest { get; private set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00027395 File Offset: 0x00025595
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x0002739D File Offset: 0x0002559D
		public bool IsAvailable
		{
			get
			{
				return this.Enabled;
			}
			set
			{
				this.Enabled = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x000273A6 File Offset: 0x000255A6
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x000273AE File Offset: 0x000255AE
		public bool IsMandatory { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x000273B7 File Offset: 0x000255B7
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x000273BF File Offset: 0x000255BF
		public bool IsEnabledByDefault { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x000273C8 File Offset: 0x000255C8
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x000273D0 File Offset: 0x000255D0
		public ClientExtensionProvidedTo ProvidedTo { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x000273D9 File Offset: 0x000255D9
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x000273E1 File Offset: 0x000255E1
		public string[] SpecificUsers { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x000273EA File Offset: 0x000255EA
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x000273FB File Offset: 0x000255FB
		public Version InstalledByVersion
		{
			get
			{
				return this.installedByVersion ?? ExtensionData.MinimumInstalledByVersion;
			}
			set
			{
				this.installedByVersion = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00027404 File Offset: 0x00025604
		// (set) Token: 0x06000A37 RID: 2615 RVA: 0x0002740C File Offset: 0x0002560C
		public XmlNode MasterTableNode { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00027415 File Offset: 0x00025615
		public Version SchemaVersion
		{
			get
			{
				if (this.SchemaParser == null)
				{
					return null;
				}
				return this.SchemaParser.SchemaVersion;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002742C File Offset: 0x0002562C
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x0002743D File Offset: 0x0002563D
		public Version MinApiVersion
		{
			get
			{
				return this.minApiVersion ?? SchemaConstants.Exchange2013RtmApiVersion;
			}
			private set
			{
				this.minApiVersion = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00027448 File Offset: 0x00025648
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x000274AF File Offset: 0x000256AF
		private SchemaParser SchemaParser
		{
			get
			{
				if (this.Manifest == null)
				{
					return null;
				}
				ExtensionInstallScope extensionInstallScope = (this.Scope != null) ? this.Scope.Value : ExtensionInstallScope.None;
				if (this.schemaParser == null)
				{
					this.schemaParser = ExtensionDataHelper.GetSchemaParser(this.Manifest, extensionInstallScope);
				}
				else
				{
					this.schemaParser.ExtensionInstallScope = extensionInstallScope;
				}
				return this.schemaParser;
			}
			set
			{
				this.schemaParser = value;
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000274B8 File Offset: 0x000256B8
		private ExtensionData(string marketplaceAssetID, string marketplaceContentMarket, string providerName, Uri iconURL, string extensionId, string version, ExtensionType? type, ExtensionInstallScope? scope, string displayName, string description, bool enabled, DisableReasonType disableReason, string identityAndEwsTokenId, RequestedCapabilities? requestedCapabilities, Version installedByVersion, SafeXmlDocument manifest, string appStatus, string etoken = null, Uri highResolutionIconUrl = null, Version minApiVersion = null, SchemaParser schemaParser = null)
		{
			this.MarketplaceAssetID = marketplaceAssetID;
			this.MarketplaceContentMarket = marketplaceContentMarket;
			this.ProviderName = providerName;
			this.IconURL = iconURL;
			this.ExtensionId = extensionId;
			this.VersionAsString = version;
			this.Type = type;
			this.Scope = scope;
			this.DisplayName = displayName;
			this.Description = description;
			this.Enabled = enabled;
			this.DisableReason = disableReason;
			this.AppStatus = appStatus;
			this.IdentityAndEwsTokenId = identityAndEwsTokenId;
			this.RequestedCapabilities = requestedCapabilities;
			this.HighResolutionIconURL = highResolutionIconUrl;
			this.SchemaParser = schemaParser;
			this.MinApiVersion = minApiVersion;
			this.InstalledByVersion = installedByVersion;
			this.Manifest = manifest;
			this.Etoken = etoken;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00027570 File Offset: 0x00025770
		public ExtensionData(string marketplaceAssetID, string marketplaceContentMarket, string providerName, Uri iconURL, string extensionId, string version, ExtensionType? type, ExtensionInstallScope? scope, string displayName, string description, bool enabled, DisableReasonType disableReason, string identityAndEwsTokenId, RequestedCapabilities? requestedCapabilities, Version installedByVersion, string manifestString)
		{
			this.MarketplaceAssetID = marketplaceAssetID;
			this.MarketplaceContentMarket = marketplaceContentMarket;
			this.ProviderName = providerName;
			this.IconURL = iconURL;
			this.ExtensionId = extensionId;
			this.VersionAsString = version;
			this.Type = type;
			this.Scope = scope;
			this.DisplayName = displayName;
			this.Description = description;
			this.IdentityAndEwsTokenId = identityAndEwsTokenId;
			this.RequestedCapabilities = requestedCapabilities;
			this.Enabled = enabled;
			this.DisableReason = disableReason;
			this.InstalledByVersion = installedByVersion;
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.PreserveWhitespace = true;
			safeXmlDocument.LoadXml(manifestString);
			this.Manifest = safeXmlDocument;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00027614 File Offset: 0x00025814
		public static ExtensionData CreateForXmlStorage(string extensionId, string marketplaceAssetID, string marketplaceContentMarket, ExtensionType? type, ExtensionInstallScope? scope, bool enabled, string version, DisableReasonType disableReason, SafeXmlDocument manifest, string appStatus, string etoken = null)
		{
			return new ExtensionData(marketplaceAssetID, marketplaceContentMarket, null, null, extensionId, version, type, scope, null, null, enabled, disableReason, null, null, null, manifest, appStatus, etoken, null, null, null);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002764C File Offset: 0x0002584C
		public static ExtensionData CreateFromClientExtension(ClientExtension clientExtension)
		{
			ExtensionData extensionData = ExtensionData.ParseOsfManifest(clientExtension.ManifestStream, clientExtension.MarketplaceAssetID, clientExtension.MarketplaceContentMarket, clientExtension.Type, clientExtension.Scope, clientExtension.IsAvailable, DisableReasonType.NotDisabled, clientExtension.AppStatus, clientExtension.Etoken);
			extensionData.IsMandatory = clientExtension.IsMandatory;
			extensionData.IsEnabledByDefault = clientExtension.IsEnabledByDefault;
			extensionData.ProvidedTo = clientExtension.ProvidedTo;
			if (clientExtension.SpecificUsers != null)
			{
				extensionData.SpecificUsers = new string[clientExtension.SpecificUsers.Count];
				for (int i = 0; i < clientExtension.SpecificUsers.Count; i++)
				{
					extensionData.SpecificUsers[i] = clientExtension.SpecificUsers[i];
				}
			}
			extensionData.EtokenData = ExtensionData.ParseEtoken(extensionData.Etoken, extensionData.ExtensionId, null, extensionData.MarketplaceAssetID, true, false);
			return extensionData;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00027720 File Offset: 0x00025920
		public static EntitlementTokenData ParseEtoken(string etoken, string appId, string domain, string assetId, bool skipVerification, bool isSiteLicenseRequired)
		{
			EntitlementTokenData result = null;
			if (!string.IsNullOrWhiteSpace(etoken))
			{
				XmlNode xmlNode = new SafeXmlDocument
				{
					PreserveWhitespace = true
				}.CreateNode(XmlNodeType.Element, "entitlementToken", null);
				xmlNode.InnerXml = etoken;
				result = ExtensionData.ParseEntitlementTokenData(xmlNode, appId, domain, assetId, skipVerification, isSiteLicenseRequired);
			}
			return result;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00027768 File Offset: 0x00025968
		public static ExtensionData ConvertFromMasterTableXml(XmlNode xmlNode, bool isOrgMasterTable, string domain)
		{
			ExtensionData extensionData = null;
			bool boolTagValue = ExtensionData.GetBoolTagValue(xmlNode, "enabled");
			XmlNode xmlNode2 = xmlNode.SelectSingleNode("appstatus");
			string appStatus = (xmlNode2 != null) ? xmlNode2.InnerXml : string.Empty;
			DisableReasonType enumTagValue = ExtensionData.GetEnumTagValue<DisableReasonType>(xmlNode, "disablereason", null);
			Version versionTagValue = ExtensionData.GetVersionTagValue(xmlNode, "installedByVersion");
			XmlNode xmlNode3 = xmlNode.SelectSingleNode("manifest");
			if (xmlNode3 != null)
			{
				ExtensionType enumTagValue2 = ExtensionData.GetEnumTagValue<ExtensionType>(xmlNode, "type", null);
				ExtensionInstallScope enumTagValue3 = ExtensionData.GetEnumTagValue<ExtensionInstallScope>(xmlNode, "scope", null);
				string text = null;
				string marketplaceContentMarket = null;
				if (ExtensionType.MarketPlace == enumTagValue2)
				{
					text = ExtensionData.GetTagStringValue(xmlNode, "marketplaceAssetID", null);
					marketplaceContentMarket = ExtensionData.GetTagStringValue(xmlNode, "marketplaceContentMarket", null);
				}
				SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
				safeXmlDocument.PreserveWhitespace = true;
				safeXmlDocument.InnerXml = xmlNode3.InnerXml;
				XmlNode xmlNode4 = xmlNode.SelectSingleNode("entitlementToken");
				string etoken = (xmlNode4 != null) ? xmlNode4.InnerXml : string.Empty;
				extensionData = ExtensionData.ParseOsfManifest(safeXmlDocument, text, marketplaceContentMarket, enumTagValue2, enumTagValue3, boolTagValue, enumTagValue, versionTagValue, appStatus, etoken);
				extensionData.EtokenData = ExtensionData.ParseEntitlementTokenData(xmlNode4, extensionData.ExtensionId, domain, text, false, isOrgMasterTable);
				if (isOrgMasterTable)
				{
					if (ExtensionInstallScope.Organization != enumTagValue3)
					{
						ExtensionData.Tracer.TraceError<ExtensionInstallScope, string>(0L, "Org's master table has non-org scope '{0}' extension with manifest node, id is: {1}", enumTagValue3, extensionData.ExtensionId);
						throw new OwaExtensionOperationException(Strings.ErrorCanNotReadInstalledList(Strings.FailureReasonOrgMasterTableInvalidScope(enumTagValue3.ToString(), extensionData.ExtensionId)));
					}
				}
				else if (ExtensionInstallScope.User != enumTagValue3)
				{
					ExtensionData.Tracer.TraceError<ExtensionInstallScope, string>(0L, "User's master table has non-user scope '{0}' extension with manifest node, id is: {1}", enumTagValue3, extensionData.ExtensionId);
					throw new OwaExtensionOperationException(Strings.ErrorCanNotReadInstalledList(Strings.FailureReasonUserMasterTableInvalidScope(enumTagValue3.ToString(), extensionData.ExtensionId)));
				}
			}
			else
			{
				extensionData = ExtensionData.CreateForXmlStorage(ExtensionData.GetTagStringValue(xmlNode, "ExtensionId", null), null, null, null, null, boolTagValue, null, enumTagValue, null, null, null);
			}
			if (isOrgMasterTable)
			{
				extensionData.IsMandatory = ExtensionData.GetBoolTagValue(xmlNode, "./isMandatory");
				extensionData.IsEnabledByDefault = ExtensionData.GetBoolTagValue(xmlNode, "./isEnabledByDefault");
				extensionData.ProvidedTo = ExtensionData.GetEnumTagValue<ClientExtensionProvidedTo>(xmlNode, "./providedTo", null);
				using (XmlNodeList xmlNodeList = xmlNode.SelectNodes("./users/user"))
				{
					if (xmlNodeList != null)
					{
						List<string> list = new List<string>(xmlNodeList.Count);
						foreach (object obj in xmlNodeList)
						{
							XmlNode xmlNode5 = (XmlNode)obj;
							list.Add(xmlNode5.InnerText);
						}
						extensionData.SpecificUsers = list.ToArray();
					}
				}
			}
			return extensionData;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00027A18 File Offset: 0x00025C18
		public static ExtensionData ParseOsfManifest(byte[] manifestBytes, int byteCount, string marketplaceAssetID, string marketplaceContentMarket, ExtensionType extensionType, ExtensionInstallScope extensionScope, bool isEnabled, DisableReasonType disableReason, string appStatus, string etoken = null)
		{
			if (manifestBytes == null || manifestBytes.Length == 0)
			{
				throw new ArgumentNullException("manifestBytes");
			}
			ExtensionData result = null;
			using (Stream stream = new MemoryStream(manifestBytes, 0, byteCount))
			{
				result = ExtensionData.ParseOsfManifest(stream, marketplaceAssetID, marketplaceContentMarket, extensionType, extensionScope, isEnabled, disableReason, appStatus, etoken);
			}
			return result;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00027A78 File Offset: 0x00025C78
		public static ExtensionData ParseOsfManifest(Stream manifestStream, string marketplaceAssetID, string marketplaceContentMarket, ExtensionType extensionType, ExtensionInstallScope extensionScope, bool isEnabled, DisableReasonType disableReason, string appStatus, string etoken = null)
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.PreserveWhitespace = true;
			SafeXmlDocument xmlDoc = null;
			manifestStream.Position = 0L;
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(manifestStream))
				{
					safeXmlDocument.Load(xmlReader);
				}
				xmlDoc = ExtensionDataHelper.GetManifest(safeXmlDocument);
			}
			catch (InvalidOperationException ex)
			{
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonInvalidXml(ex.Message)));
			}
			catch (XmlException ex2)
			{
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonInvalidXml(ex2.Message)));
			}
			return ExtensionData.ParseOsfManifest(xmlDoc, marketplaceAssetID, marketplaceContentMarket, extensionType, extensionScope, isEnabled, disableReason, null, appStatus, etoken);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00027B38 File Offset: 0x00025D38
		public static string GetClientExtensionMarketplaceUrl(MailboxSession mailboxSession, Uri ecpUrl, bool withinReadWriteMailboxRole, string deploymentId, Version schemaVersionSupported, string realm = null)
		{
			string fullEncodedOfficeCallbackUrl = ExtensionData.GenerateFullEncodedOfficeCallbackUrl(ecpUrl, ExtensionData.OfficeCallBackUrl, realm, deploymentId);
			return ExtensionData.GetClientExtensionMarketplaceUrl(mailboxSession.Culture.LCID, withinReadWriteMailboxRole, fullEncodedOfficeCallbackUrl, deploymentId, schemaVersionSupported);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00027B6C File Offset: 0x00025D6C
		public static string GetClientExtensionMarketplaceUrl(int lcid, HttpRequest httpRequest, bool withinReadWriteMailboxRole, string deploymentId, string realm = null)
		{
			Uri ecpUrl = ExtensionData.GetEcpUrl(httpRequest);
			if (ecpUrl == null)
			{
				return null;
			}
			string fullEncodedOfficeCallbackUrl = ExtensionData.GenerateFullEncodedOfficeCallbackUrl(ecpUrl, ExtensionData.OfficeCallBackUrl, realm, deploymentId);
			return ExtensionData.GetClientExtensionMarketplaceUrl(lcid, withinReadWriteMailboxRole, fullEncodedOfficeCallbackUrl, deploymentId, null);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00027BA4 File Offset: 0x00025DA4
		public static string GetClientExtensionMarketplaceUrl(int lcid, bool withinOrgMarketplaceRole, string fullEncodedOfficeCallbackUrl, string deploymentId, Version schemaVersionSupported = null)
		{
			bool flag = schemaVersionSupported != null && schemaVersionSupported == SchemaConstants.SchemaVersion1_0;
			return string.Format("{0}?app={1}&ver={2}&clid={3}&p1={4}&p2={5}&p3={6}&p4={7}&p5={8}&Scope={9}&CallBackURL={10}&DeployId={11}", new object[]
			{
				ExtensionData.LandingPageUrl,
				"outlook.exe",
				"15",
				lcid,
				flag ? "15d0d516d32" : ExtensionData.ClientFullVersion,
				"4",
				"0",
				"HP",
				"0",
				withinOrgMarketplaceRole ? "3" : "1",
				fullEncodedOfficeCallbackUrl,
				deploymentId
			});
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00027C4E File Offset: 0x00025E4E
		public static string GetClientExtensionAppDetailsUrl(int lcid, HttpRequest httpRequest, bool withinOrgMarketplaceRole, string deploymentId, string market, string assetId, string realm = null)
		{
			return ExtensionData.GetOmexUrlWithParameters(ExtensionData.LandingPageUrl, lcid, httpRequest, withinOrgMarketplaceRole, deploymentId, market, assetId, realm);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00027C64 File Offset: 0x00025E64
		public static string GetClientExtensionMyAppsUrl(int lcid, HttpRequest httpRequest, bool withinOrgMarketplaceRole, string deploymentId, string market, string assetId, string realm = null)
		{
			return ExtensionData.GetOmexUrlWithParameters(ExtensionData.MyAppsPageUrl, lcid, httpRequest, withinOrgMarketplaceRole, deploymentId, market, assetId, realm);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00027C7A File Offset: 0x00025E7A
		internal static bool TryParseVersion(string versionAsString, out Version version)
		{
			version = null;
			if (!string.IsNullOrWhiteSpace(versionAsString))
			{
				if (versionAsString.Length == 1)
				{
					versionAsString += ".0";
				}
				if (!Version.TryParse(versionAsString, out version))
				{
					version = null;
				}
			}
			return version != null;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00027CB1 File Offset: 0x00025EB1
		internal static bool ValidateManifestSize(long size, bool shouldThrowOnFailure = true)
		{
			if (size <= 262144L)
			{
				return true;
			}
			if (shouldThrowOnFailure)
			{
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonManifestTooLarge(256)));
			}
			return false;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00027CDC File Offset: 0x00025EDC
		internal static bool ValidateManifestDownloadSize(long size, bool shouldThrowOnFailure = true)
		{
			if (size <= 393216L)
			{
				return true;
			}
			if (shouldThrowOnFailure)
			{
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonManifestTooLarge(384)));
			}
			return false;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00027D08 File Offset: 0x00025F08
		internal static T GetEnumTagValue<T>(XmlNode xmlNode, string tagName, XmlNamespaceManager mgr) where T : struct
		{
			string tagStringValue = ExtensionData.GetTagStringValue(xmlNode, tagName, mgr);
			T result;
			if (!EnumValidator.TryParse<T>(tagStringValue, EnumParseOptions.IgnoreCase, out result))
			{
				ExtensionData.Tracer.TraceError(0L, tagName + " tag value is invalid: " + tagStringValue);
				throw new OwaExtensionOperationException(Strings.ErrorCanNotReadInstalledList(Strings.FailureReasonTagValueInvalid(tagName, tagStringValue)));
			}
			return result;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00027D5C File Offset: 0x00025F5C
		internal static int CompareCapabilities(RequestedCapabilities capabilitiesA, RequestedCapabilities capabilitiesB)
		{
			if (!ExtensionData.IsCapabilitiesKnown(capabilitiesA))
			{
				throw new ArgumentOutOfRangeException("capabilitiesA");
			}
			if (!ExtensionData.IsCapabilitiesKnown(capabilitiesB))
			{
				throw new ArgumentOutOfRangeException("capabilitiesB");
			}
			if (capabilitiesA == capabilitiesB)
			{
				return 0;
			}
			if (capabilitiesA == Microsoft.Exchange.Data.RequestedCapabilities.ReadWriteMailbox)
			{
				return 1;
			}
			if (capabilitiesB == Microsoft.Exchange.Data.RequestedCapabilities.ReadWriteMailbox)
			{
				return -1;
			}
			return capabilitiesA.CompareTo(capabilitiesB);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00027DB4 File Offset: 0x00025FB4
		private static string GetOmexUrlWithParameters(string targetOmexUrl, int lcid, HttpRequest httpRequest, bool withinReadWriteMailboxRole, string deploymentId, string market, string assetId, string realm = null)
		{
			Uri ecpUrl = ExtensionData.GetEcpUrl(httpRequest);
			if (ecpUrl == null)
			{
				return null;
			}
			string text = ExtensionData.GenerateFullEncodedOfficeCallbackUrl(ecpUrl, ExtensionData.OfficeCallBackUrl, realm, deploymentId);
			return string.Format("{0}?app={1}&ver={2}&clid={3}&p1={4}&p2={5}&p3={6}&p4={7}&p5={8}&Scope={9}&CallBackURL={10}&DeployId={11}", new object[]
			{
				targetOmexUrl,
				"outlook.exe",
				"15",
				lcid,
				ExtensionData.ClientFullVersion,
				"4",
				"0",
				"WA",
				market + "\\" + assetId,
				withinReadWriteMailboxRole ? "3" : "1",
				text,
				deploymentId
			});
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00027E64 File Offset: 0x00026064
		private static Uri GetEcpUrl(HttpRequest httpRequest)
		{
			Uri result;
			try
			{
				string text = httpRequest.Headers["msExchProxyUri"];
				if (string.IsNullOrEmpty(text))
				{
					ExtensionData.Tracer.TraceError(0L, "No request uri to create ecp with, skipping");
					ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_EcpUriRetrievalFailed, null, new object[]
					{
						"GetEcpUrl"
					});
					result = null;
				}
				else
				{
					result = new UriBuilder(text)
					{
						Path = "/ecp/",
						Query = string.Empty
					}.Uri;
				}
			}
			catch (UriFormatException arg)
			{
				ExtensionData.Tracer.TraceError<UriFormatException>(0L, "Caught exception when trying to access request uri: {0}", arg);
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_EcpUriRetrievalFailed, null, new object[]
				{
					"GetEcpUrl"
				});
				result = null;
			}
			return result;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00027F38 File Offset: 0x00026138
		private static bool IsCapabilitiesKnown(RequestedCapabilities capabilities)
		{
			bool result;
			switch (capabilities)
			{
			case Microsoft.Exchange.Data.RequestedCapabilities.Restricted:
			case Microsoft.Exchange.Data.RequestedCapabilities.ReadItem:
			case Microsoft.Exchange.Data.RequestedCapabilities.ReadWriteMailbox:
			case Microsoft.Exchange.Data.RequestedCapabilities.ReadWriteItem:
				result = true;
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00027F68 File Offset: 0x00026168
		private static ExtensionData ParseOsfManifest(SafeXmlDocument xmlDoc, string marketplaceAssetID, string marketplaceContentMarket, ExtensionType extensionType, ExtensionInstallScope extensionScope, bool isEnabled, DisableReasonType disableReason, Version installedByVersion, string appStatus, string etoken = null)
		{
			if (xmlDoc == null)
			{
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonMissingOfficeApp));
			}
			SchemaParser schemaParser = ExtensionDataHelper.GetSchemaParser(xmlDoc, extensionScope);
			CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
			string andValidateExtensionId = schemaParser.GetAndValidateExtensionId();
			RequestedCapabilities requestedCapabilities = schemaParser.GetRequestedCapabilities();
			schemaParser.ValidateRules();
			Uri andValidateIconUrl = schemaParser.GetAndValidateIconUrl(currentUICulture);
			Uri andValidateHighResolutionIconUrl = schemaParser.GetAndValidateHighResolutionIconUrl(currentUICulture);
			schemaParser.ValidateSourceLocations();
			schemaParser.ValidateHosts();
			schemaParser.ValidateFormSettings();
			Version v = schemaParser.GetMinApiVersion();
			Version schemaVersion = schemaParser.SchemaVersion;
			if (v > SchemaConstants.HighestSupportedApiVersion)
			{
				throw new OwaExtensionOperationException(Strings.ErrorReasonMinApiVersionNotSupported(v, ExchangeSetupContext.InstalledVersion));
			}
			string oweStringElement = schemaParser.GetOweStringElement("ProviderName");
			string oweStringElement2 = schemaParser.GetOweStringElement("Version");
			string idForTokenRequests = schemaParser.GetIdForTokenRequests();
			string oweLocaleAwareSetting = schemaParser.GetOweLocaleAwareSetting("DisplayName", currentUICulture);
			string oweLocaleAwareSetting2 = schemaParser.GetOweLocaleAwareSetting("Description", currentUICulture);
			return new ExtensionData(marketplaceAssetID, marketplaceContentMarket, oweStringElement, andValidateIconUrl, andValidateExtensionId, oweStringElement2, new ExtensionType?(extensionType), new ExtensionInstallScope?(extensionScope), oweLocaleAwareSetting, oweLocaleAwareSetting2, isEnabled, disableReason, idForTokenRequests, new RequestedCapabilities?(requestedCapabilities), installedByVersion, xmlDoc, appStatus, etoken, andValidateHighResolutionIconUrl, v, schemaParser);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00028078 File Offset: 0x00026278
		public XmlNode ConvertToXml(bool shouldIncludeManifest, bool shouldIncludeOrgNodes)
		{
			XmlElement xmlElement = this.Manifest.CreateElement("Extension");
			this.AppendXmlElement(xmlElement, "ExtensionId", this.ExtensionId);
			this.AppendXmlElement(xmlElement, "enabled", this.Enabled.ToString());
			this.AppendXmlElement(xmlElement, "disablereason", this.DisableReason.ToString());
			this.AppendXmlElement(xmlElement, "installedByVersion", this.InstalledByVersion.ToString());
			if (shouldIncludeManifest)
			{
				this.AppendXmlElement(xmlElement, "marketplaceAssetID", this.MarketplaceAssetID);
				this.AppendXmlElement(xmlElement, "marketplaceContentMarket", this.MarketplaceContentMarket);
				this.AppendXmlElement(xmlElement, "type", this.Type.ToString());
				this.AppendXmlElement(xmlElement, "scope", this.Scope.ToString());
				this.AppendXmlElement(xmlElement, "manifest", this.Manifest.LastChild.Clone());
				if (!string.IsNullOrWhiteSpace(this.Etoken))
				{
					this.AppendXmlElement(xmlElement, "entitlementToken", this.Etoken);
				}
			}
			if (shouldIncludeOrgNodes)
			{
				this.AppendXmlElement(xmlElement, "isMandatory", this.IsMandatory.ToString());
				this.AppendXmlElement(xmlElement, "isEnabledByDefault", this.IsEnabledByDefault.ToString());
				this.AppendXmlElement(xmlElement, "providedTo", this.ProvidedTo.ToString());
				ExtensionData.AppendXmlElement(this.Manifest, xmlElement, "users", "user", this.SpecificUsers);
			}
			return xmlElement;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002820C File Offset: 0x0002640C
		public byte[] GetManifestBytes()
		{
			string outerXml = this.Manifest.DocumentElement.OuterXml;
			return Encoding.UTF8.GetBytes(outerXml);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00028238 File Offset: 0x00026438
		public static string GenerateOfficeCallbackUrlForReconsent(HttpRequest httpRequest, string realm, string assetId, string marketplace, ExtensionInstallScope scope, string etoken)
		{
			Uri ecpUrl = ExtensionData.GetEcpUrl(httpRequest);
			string str = ExtensionData.GenerateOfficeCallbackBaseUrl(ecpUrl, ExtensionData.OfficeCallBackUrl);
			return str + string.Format("&realm={0}&scope={1}&lc={2}&clientToken={3}&AssetId={4}", new object[]
			{
				realm,
				scope.ToString(),
				marketplace,
				etoken,
				assetId
			});
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00028290 File Offset: 0x00026490
		internal List<FormSettings> GetFormSettings(FormFactor formFactor)
		{
			return this.SchemaParser.GetFormSettings(formFactor, CultureInfo.CurrentUICulture, this.Etoken);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000282A9 File Offset: 0x000264A9
		internal bool GetDisableEntityHighlighting()
		{
			return this.SchemaParser.GetDisableEntityHighlighting();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000282B6 File Offset: 0x000264B6
		internal bool TryGetActivationRule(out ActivationRule activationRule)
		{
			return this.SchemaParser.TryCreateActivationRule(out activationRule);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000282C4 File Offset: 0x000264C4
		internal bool TryUpdateSourceLocation(IExchangePrincipal exchangePrincipal, string elementName, out Exception exception, ExtensionDataHelper.TryModifySourceLocationDelegate tryModifySourceLocationDelegate)
		{
			return this.SchemaParser.TryUpdateSourceLocation(exchangePrincipal, elementName, this, out exception, tryModifySourceLocationDelegate);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x000282D8 File Offset: 0x000264D8
		internal static bool GetBoolTagValue(XmlNode xmlNode, string tagName)
		{
			string tagStringValue = ExtensionData.GetTagStringValue(xmlNode, tagName, null);
			bool result;
			if (!bool.TryParse(tagStringValue, out result))
			{
				ExtensionData.Tracer.TraceError(0L, tagName + " tag value is invalid: " + tagStringValue);
				throw new OwaExtensionOperationException(Strings.ErrorCanNotReadInstalledList(Strings.FailureReasonTagValueInvalid(tagName, tagStringValue)));
			}
			return result;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00028328 File Offset: 0x00026528
		internal static string GetTagStringValue(XmlNode xmlNode, string tagName, XmlNamespaceManager mgr)
		{
			XmlNode xmlNode2 = (mgr == null) ? xmlNode.SelectSingleNode(tagName) : xmlNode.SelectSingleNode(tagName, mgr);
			if (xmlNode2 == null)
			{
				ExtensionData.Tracer.TraceError(0L, tagName + " tag is missing from the given node.");
				throw new OwaExtensionOperationException(Strings.ErrorCanNotReadInstalledList(Strings.FailureReasonTagMissing(tagName)));
			}
			return xmlNode2.InnerText;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00028380 File Offset: 0x00026580
		private static Version GetVersionTagValue(XmlNode xmlNode, string tagName)
		{
			XmlNode xmlNode2 = xmlNode.SelectSingleNode(tagName);
			if (xmlNode2 == null)
			{
				ExtensionData.Tracer.TraceWarning(0L, tagName + " tag is missing from the given node. Returning null");
				return null;
			}
			string innerText = xmlNode2.InnerText;
			if (string.IsNullOrEmpty(innerText))
			{
				ExtensionData.Tracer.TraceWarning(0L, tagName + " version string is missing from the given node. Returning null");
				return null;
			}
			Version result = null;
			if (Version.TryParse(innerText, out result))
			{
				return result;
			}
			ExtensionData.Tracer.TraceWarning(0L, tagName + " Failed to parse version, returning null.");
			return null;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000283FF File Offset: 0x000265FF
		internal static string GetAttributeStringValue(XmlNode xmlNode, string attributeName)
		{
			return ExtensionData.GetAttributeStringValue(xmlNode, attributeName, true);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002840C File Offset: 0x0002660C
		internal static string GetOptionalAttributeStringValue(XmlNode xmlNode, string attributeName, string defaultValue)
		{
			string attributeStringValue = ExtensionData.GetAttributeStringValue(xmlNode, attributeName, false);
			if (attributeStringValue != null)
			{
				return attributeStringValue;
			}
			return defaultValue;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00028428 File Offset: 0x00026628
		private static string GetAttributeStringValue(XmlNode xmlNode, string attributeName, bool throwOnNull)
		{
			if (xmlNode.Attributes == null)
			{
				if (throwOnNull)
				{
					ExtensionData.Tracer.TraceError(0L, "Given node has no attributes.");
					throw new OwaExtensionOperationException(Strings.ErrorCanNotReadInstalledList(Strings.FailureReasonNoAttributes));
				}
				return null;
			}
			else
			{
				XmlAttribute xmlAttribute = xmlNode.Attributes[attributeName];
				if (xmlAttribute != null)
				{
					return xmlAttribute.Value;
				}
				if (throwOnNull)
				{
					ExtensionData.Tracer.TraceError(0L, attributeName + " attribute is missing from the given node.");
					throw new OwaExtensionOperationException(Strings.ErrorCanNotReadInstalledList(Strings.FailureReasonAttributeMissing(attributeName)));
				}
				return null;
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000284B0 File Offset: 0x000266B0
		private static T GetAttributeEnumValue<T>(XmlNode xmlNode, string attributeName) where T : struct
		{
			string attributeStringValue = ExtensionData.GetAttributeStringValue(xmlNode, attributeName);
			T result;
			if (!EnumValidator.TryParse<T>(attributeStringValue, EnumParseOptions.IgnoreCase, out result))
			{
				ExtensionData.Tracer.TraceError(0L, attributeName + " attribute value is invalid: " + attributeStringValue);
				throw new OwaExtensionOperationException(Strings.ErrorCanNotReadInstalledList(Strings.FailureReasonAttributeValueInvalid(attributeName, attributeStringValue)));
			}
			return result;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00028500 File Offset: 0x00026700
		private static string GetAuthTokenValue(XmlNode xmlNode, XmlNamespaceManager mgr)
		{
			XmlNode xmlNode2 = xmlNode.SelectSingleNode("SourceLocation", mgr);
			if (xmlNode2 == null)
			{
				ExtensionData.Tracer.TraceError(0L, "SourceLocation tag is missing from the given node.");
				throw new OwaExtensionOperationException(Strings.ErrorCanNotReadInstalledList(Strings.FailureReasonSourceLocationTagMissing));
			}
			return ExtensionData.GetAttributeStringValue(xmlNode2, "DefaultValue");
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00028550 File Offset: 0x00026750
		internal static void AppendXmlElement(SafeXmlDocument document, XmlNode parent, string newChildName, string innerTagName, string[] innerTagValues)
		{
			if (innerTagValues != null && innerTagValues.Length > 0)
			{
				XmlElement xmlElement = document.CreateElement(newChildName);
				foreach (string innerText in innerTagValues)
				{
					XmlElement xmlElement2 = document.CreateElement(innerTagName);
					xmlElement2.InnerText = innerText;
					xmlElement.AppendChild(xmlElement2);
				}
				parent.AppendChild(xmlElement);
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x000285AC File Offset: 0x000267AC
		private void AppendXmlElement(XmlNode parent, string newChildName, XmlNode newChildValue)
		{
			if (newChildValue != null)
			{
				XmlElement xmlElement = this.Manifest.CreateElement(newChildName);
				xmlElement.AppendChild(newChildValue);
				parent.AppendChild(xmlElement);
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000285DC File Offset: 0x000267DC
		private void AppendXmlElement(XmlNode parent, string newChildName, string newChildValue)
		{
			if (newChildValue != null)
			{
				XmlElement xmlElement = this.Manifest.CreateElement(newChildName);
				xmlElement.InnerText = newChildValue;
				parent.AppendChild(xmlElement);
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00028608 File Offset: 0x00026808
		private static string GenerateFullEncodedOfficeCallbackUrl(Uri ecpUrl, string officeCallBackUrl, string realm, string deploymentId)
		{
			string text = ExtensionData.GenerateOfficeCallbackBaseUrl(ecpUrl, officeCallBackUrl);
			if (!string.IsNullOrWhiteSpace(realm))
			{
				text = ExtensionData.AppendEncodedQueryParameterForEcpCallback(text, "realm", realm);
			}
			if (!string.IsNullOrWhiteSpace(deploymentId))
			{
				text = ExtensionData.AppendEncodedQueryParameterForEcpCallback(text, "deployId", deploymentId);
			}
			return HttpUtility.UrlEncode(text);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00028650 File Offset: 0x00026850
		private static string GenerateOfficeCallbackBaseUrl(Uri ecpUrl, string officeCallBackUrl)
		{
			officeCallBackUrl = VirtualPathUtility.ToAbsolute(officeCallBackUrl);
			officeCallBackUrl = Regex.Replace(officeCallBackUrl, "/ews/", string.Empty, RegexOptions.IgnoreCase);
			officeCallBackUrl = Regex.Replace(officeCallBackUrl, "/owa/", string.Empty, RegexOptions.IgnoreCase);
			officeCallBackUrl = ExtensionData.AppendEncodedQueryParameterForEcpCallback(officeCallBackUrl, "exsvurl", "1");
			return new Uri(ecpUrl, officeCallBackUrl).ToString();
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000286AC File Offset: 0x000268AC
		public static string AppendEncodedQueryParameterForEcpCallback(string url, string name, string value)
		{
			StringBuilder stringBuilder = new StringBuilder(url, url.Length + name.Length + value.Length + 4);
			stringBuilder.Append((url.IndexOf('?') >= 0) ? HttpUtility.UrlEncode("&") : "?");
			stringBuilder.Append(HttpUtility.UrlEncode(name));
			stringBuilder.Append('=');
			stringBuilder.Append(HttpUtility.UrlEncode(value));
			return stringBuilder.ToString();
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00028724 File Offset: 0x00026924
		public static string AppendUnencodedQueryParameter(string url, string name, string value)
		{
			StringBuilder stringBuilder = new StringBuilder(url, url.Length + name.Length + value.Length + 2);
			stringBuilder.Append((url.IndexOf('?') >= 0) ? "&" : "?");
			stringBuilder.Append(name);
			stringBuilder.Append('=');
			stringBuilder.Append(value);
			return stringBuilder.ToString();
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002878C File Offset: 0x0002698C
		private static EntitlementTokenData ParseEntitlementTokenData(XmlNode etokenNode, string appId, string domain, string assetId, bool skipVerification, bool isSiteLicenseRequired = false)
		{
			string text = (etokenNode != null) ? etokenNode.InnerXml : string.Empty;
			EntitlementTokenData entitlementTokenData = null;
			if (!string.IsNullOrWhiteSpace(text))
			{
				try
				{
					XmlNode xmlNode = etokenNode.Clone();
					xmlNode.InnerXml = HttpUtility.UrlDecode(text);
					XmlNode xmlNode2 = xmlNode.SelectSingleNode("r");
					if (xmlNode2 == null)
					{
						ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_MissingNodeInEtoken, null, new object[]
						{
							"ProcessEntitlementToken",
							"r",
							appId
						});
						throw new OwaExtensionOperationException(Strings.ErrorMissingNodeInEtoken("r"));
					}
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode("t");
					if (xmlNode3 == null)
					{
						ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_MissingNodeInEtoken, null, new object[]
						{
							"ProcessEntitlementToken",
							"t",
							appId
						});
						throw new OwaExtensionOperationException(Strings.ErrorMissingNodeInEtoken("t"));
					}
					string attributeStringValue = ExtensionData.GetAttributeStringValue(xmlNode3, "cid");
					LicenseType attributeEnumValue = ExtensionData.GetAttributeEnumValue<LicenseType>(xmlNode3, "et");
					DateTime etokenExpirationDate = DateTime.Parse(ExtensionData.GetAttributeStringValue(xmlNode3, "te"));
					int seatsPurchased = Convert.ToInt32(ExtensionData.GetAttributeStringValue(xmlNode3, "ts"));
					string attributeStringValue2 = ExtensionData.GetAttributeStringValue(xmlNode3, "did");
					string attributeStringValue3 = ExtensionData.GetAttributeStringValue(xmlNode3, "aid");
					if (isSiteLicenseRequired && attributeEnumValue == LicenseType.Paid)
					{
						string optionalAttributeStringValue = ExtensionData.GetOptionalAttributeStringValue(xmlNode3, "sl", null);
						bool flag = false;
						bool flag2 = bool.TryParse(optionalAttributeStringValue, out flag);
						if (!flag2)
						{
							ExtensionData.Tracer.TraceError(0L, "sl tag value is invalid: " + optionalAttributeStringValue);
						}
						if (!flag2 || !flag)
						{
							ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_OrgLevelEtokenMustBeSiteLicense, null, new object[]
							{
								"ProcessEntitlementToken",
								optionalAttributeStringValue,
								appId
							});
							throw new OwaExtensionOperationException(Strings.ErrorOrgLevelAppMustBeSiteLicense);
						}
					}
					if (string.IsNullOrWhiteSpace(assetId) || !assetId.Equals(attributeStringValue3, StringComparison.OrdinalIgnoreCase))
					{
						ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_AssetIdNotMatchInEtoken, null, new object[]
						{
							"ProcessEntitlementToken",
							assetId,
							attributeStringValue3
						});
						throw new OwaExtensionOperationException(Strings.ErrorAssetIdNotMatchInEtoken(assetId, attributeStringValue3));
					}
					if (!skipVerification && !ExtensionDataHelper.VerifyDeploymentId(attributeStringValue2, domain))
					{
						ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_InvalidDeploymentIdInEtoken, null, new object[]
						{
							"ProcessEntitlementToken",
							attributeStringValue2,
							appId
						});
						throw new OwaExtensionOperationException(Strings.ErrorEtokenWithInvalidDeploymentId(attributeStringValue2));
					}
					entitlementTokenData = new EntitlementTokenData(attributeStringValue, attributeEnumValue, seatsPurchased, etokenExpirationDate);
				}
				catch (FormatException innerException)
				{
					throw new OwaExtensionOperationException(innerException);
				}
				catch (OverflowException innerException2)
				{
					throw new OwaExtensionOperationException(innerException2);
				}
				catch (ArgumentNullException innerException3)
				{
					throw new OwaExtensionOperationException(innerException3);
				}
				finally
				{
					if (entitlementTokenData == null)
					{
						ExtensionData.Tracer.TraceError<string>(0L, "Failed to parse the stored etoken for app {0} since it is corrupted.", appId);
						ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_StoredEtokenCorrupted, null, new object[]
						{
							"ProcessEntitlementToken",
							appId
						});
					}
					else
					{
						ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_ParseEtokenSuccess, null, new object[]
						{
							"ProcessEntitlementToken"
						});
					}
				}
			}
			return entitlementTokenData;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00028AF4 File Offset: 0x00026CF4
		public object Clone()
		{
			ExtensionData extensionData = ExtensionData.ParseOsfManifest(this.Manifest, this.MarketplaceAssetID, this.MarketplaceContentMarket, this.Type.Value, this.Scope.Value, this.Enabled, this.DisableReason, this.InstalledByVersion, this.AppStatus, this.Etoken);
			extensionData.IsMandatory = this.IsMandatory;
			extensionData.IsEnabledByDefault = this.IsEnabledByDefault;
			extensionData.ProvidedTo = this.ProvidedTo;
			if (this.SpecificUsers != null)
			{
				extensionData.SpecificUsers = (this.SpecificUsers.Clone() as string[]);
			}
			return extensionData;
		}

		// Token: 0x040004D4 RID: 1236
		public const string Application = "outlook.exe";

		// Token: 0x040004D5 RID: 1237
		public const string ApplicationVersion = "15";

		// Token: 0x040004D6 RID: 1238
		public const string RTMClientVersion = "15d0d516d32";

		// Token: 0x040004D7 RID: 1239
		public const string DefaultInputForQueryString = "0";

		// Token: 0x040004D8 RID: 1240
		public const string ClickContext = "4";

		// Token: 0x040004D9 RID: 1241
		public const string HomePageTargetCode = "HP";

		// Token: 0x040004DA RID: 1242
		public const string EndNodeTargetCode = "WA";

		// Token: 0x040004DB RID: 1243
		public const string OmexUserScope = "1";

		// Token: 0x040004DC RID: 1244
		public const string OmexOrganizationScope = "2";

		// Token: 0x040004DD RID: 1245
		public const string OmexUserWithinReadWriteMailboxRoleScope = "3";

		// Token: 0x040004DE RID: 1246
		private const int BytesInKB = 1024;

		// Token: 0x040004DF RID: 1247
		internal const int MaxManifestSizeInKB = 256;

		// Token: 0x040004E0 RID: 1248
		internal const int MaxManifestSize = 262144;

		// Token: 0x040004E1 RID: 1249
		internal const int MaxManifestDownloadSizeInKB = 384;

		// Token: 0x040004E2 RID: 1250
		internal const int MaxManifestDownloadSize = 393216;

		// Token: 0x040004E3 RID: 1251
		internal const int MaxTokenDownloadSizeInKB = 30;

		// Token: 0x040004E4 RID: 1252
		internal const int MaxTokenDownloadSize = 30720;

		// Token: 0x040004E5 RID: 1253
		private const string MarketplaceAssetIDTagName = "marketplaceAssetID";

		// Token: 0x040004E6 RID: 1254
		private const string MarketplaceContentMarketTagName = "marketplaceContentMarket";

		// Token: 0x040004E7 RID: 1255
		internal const string ExtensionIdTagName = "ExtensionId";

		// Token: 0x040004E8 RID: 1256
		internal const string IsMandatoryTagName = "isMandatory";

		// Token: 0x040004E9 RID: 1257
		internal const string IsMandatoryTagPath = "./isMandatory";

		// Token: 0x040004EA RID: 1258
		internal const string IsEnabledByDefaultTagName = "isEnabledByDefault";

		// Token: 0x040004EB RID: 1259
		internal const string IsEnabledByDefaultTagPath = "./isEnabledByDefault";

		// Token: 0x040004EC RID: 1260
		internal const string ProvidedToTagName = "providedTo";

		// Token: 0x040004ED RID: 1261
		internal const string ProvidedToTagPath = "./providedTo";

		// Token: 0x040004EE RID: 1262
		internal const string SpecificUsersTagName = "users";

		// Token: 0x040004EF RID: 1263
		internal const string SpecificUserTagName = "user";

		// Token: 0x040004F0 RID: 1264
		internal const string SpecificUserTagPath = "./users/user";

		// Token: 0x040004F1 RID: 1265
		internal const string EnabledTagName = "enabled";

		// Token: 0x040004F2 RID: 1266
		internal const string DisableReasonTagName = "disablereason";

		// Token: 0x040004F3 RID: 1267
		internal const string AppStatusTagName = "appstatus";

		// Token: 0x040004F4 RID: 1268
		internal const string InstalledByVersionTagName = "installedByVersion";

		// Token: 0x040004F5 RID: 1269
		internal const string EtokenTagName = "entitlementToken";

		// Token: 0x040004F6 RID: 1270
		internal const string ConfigServiceUrlKey = "MarketplaceConfigServiceUrl";

		// Token: 0x040004F7 RID: 1271
		internal const string LandingPageUrlKey = "MarketplaceLandingPageUrl";

		// Token: 0x040004F8 RID: 1272
		internal const string MyAppsPageUrlKey = "MarketplaceMyAppsPageUrl";

		// Token: 0x040004F9 RID: 1273
		internal const string XmlSchemaInstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x040004FA RID: 1274
		internal const string ManifestTagName = "manifest";

		// Token: 0x040004FB RID: 1275
		private const string ConfigServiceUrlDefault = "https://o15.officeredir.microsoft.com/r/rlidMktplcWSConfig15";

		// Token: 0x040004FC RID: 1276
		private const string LandingPageUrlDefault = "https://o15.officeredir.microsoft.com/r/rlidMktplcExchRedirect";

		// Token: 0x040004FD RID: 1277
		private const string MyAppsPageUrlDefault = "https://o15.officeredir.microsoft.com/r/rlidMktplcMUXMyOfficeApps";

		// Token: 0x040004FE RID: 1278
		private const string TypeTagName = "type";

		// Token: 0x040004FF RID: 1279
		private const string ScopeTagName = "scope";

		// Token: 0x04000500 RID: 1280
		private static string configServiceUrl;

		// Token: 0x04000501 RID: 1281
		private static string landingPageUrl;

		// Token: 0x04000502 RID: 1282
		private static string myAppsPageUrl;

		// Token: 0x04000503 RID: 1283
		private string versionAsString;

		// Token: 0x04000504 RID: 1284
		private Version version;

		// Token: 0x04000505 RID: 1285
		private SchemaParser schemaParser;

		// Token: 0x04000506 RID: 1286
		private Uri highResolutionIconURL;

		// Token: 0x04000507 RID: 1287
		private Version minApiVersion;

		// Token: 0x04000508 RID: 1288
		private Version installedByVersion;

		// Token: 0x04000509 RID: 1289
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x0400050A RID: 1290
		internal static readonly Version MinimumInstalledByVersion = new Version(15, 0, 516, 0);
	}
}
