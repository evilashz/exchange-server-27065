using System;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000111 RID: 273
	internal sealed class AppStateResponseAsset
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0002EDF8 File Offset: 0x0002CFF8
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x0002EE00 File Offset: 0x0002D000
		public string MarketplaceAssetID { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002EE09 File Offset: 0x0002D009
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x0002EE11 File Offset: 0x0002D011
		public string ExtensionID { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002EE1A File Offset: 0x0002D01A
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x0002EE22 File Offset: 0x0002D022
		public Version Version { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0002EE2B File Offset: 0x0002D02B
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x0002EE33 File Offset: 0x0002D033
		public OmexConstants.AppState? State { get; set; }

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002EE3C File Offset: 0x0002D03C
		internal AppStateResponseAsset()
		{
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002EE44 File Offset: 0x0002D044
		internal AppStateResponseAsset(XElement element, BaseAsyncCommand.LogResponseParseFailureEventCallback logParseFailureCallback)
		{
			this.MarketplaceAssetID = this.ParseMarketplaceAssetID(element, OmexConstants.OfficeNamespace + "assetid", logParseFailureCallback);
			if (!string.IsNullOrWhiteSpace(this.MarketplaceAssetID))
			{
				this.ExtensionID = this.ParseExtensionID(element, OmexConstants.OfficeNamespace + "prodid", logParseFailureCallback);
				this.Version = this.ParseVersion(element, OmexConstants.OfficeNamespace + "ver", logParseFailureCallback);
				this.State = this.ParseState(element, OmexConstants.OfficeNamespace + "state", logParseFailureCallback);
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002EED8 File Offset: 0x0002D0D8
		private string ParseMarketplaceAssetID(XElement element, XName assetIDKey, BaseAsyncCommand.LogResponseParseFailureEventCallback logParseFailureCallback)
		{
			string text = (string)element.Attribute(assetIDKey);
			if (string.IsNullOrWhiteSpace(text))
			{
				AppStateResponseAsset.Tracer.TraceError<XElement>(0L, "AppStateResponseAsset.ParseMarketplaceAssetID: Marketplace asset id was not returned: {0}", element);
				logParseFailureCallback(ApplicationLogicEventLogConstants.Tuple_AppStateResponseInvalidMarketplaceAssetID, text, element);
			}
			return text;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002EF1C File Offset: 0x0002D11C
		private string ParseExtensionID(XElement element, XName extensionIDKey, BaseAsyncCommand.LogResponseParseFailureEventCallback logParseFailureCallback)
		{
			string text = null;
			string text2 = (string)element.Attribute(extensionIDKey);
			if (!string.IsNullOrWhiteSpace(text2))
			{
				text = ExtensionDataHelper.FormatExtensionId(text2);
				Guid guid;
				if (!GuidHelper.TryParseGuid(text, out guid))
				{
					text = null;
				}
			}
			if (text == null)
			{
				AppStateResponseAsset.Tracer.TraceError<XElement>(0L, "AppStateResponseAsset.ParseExtensionID: Extension id is invalid: {0}", element);
				logParseFailureCallback(ApplicationLogicEventLogConstants.Tuple_AppStateResponseInvalidExtensionID, this.MarketplaceAssetID, element);
			}
			return text;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002EF7C File Offset: 0x0002D17C
		private Version ParseVersion(XElement element, XName versionKey, BaseAsyncCommand.LogResponseParseFailureEventCallback logParseFailureCallback)
		{
			Version result = null;
			string versionAsString = (string)element.Attribute(versionKey);
			if (!ExtensionData.TryParseVersion(versionAsString, out result))
			{
				AppStateResponseAsset.Tracer.TraceError<XElement>(0L, "AppStateResponseAsset.ParseVersion: Unable to parse version for: {0}", element);
				logParseFailureCallback(ApplicationLogicEventLogConstants.Tuple_AppStateResponseInvalidVersion, this.MarketplaceAssetID, element);
			}
			return result;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002EFC8 File Offset: 0x0002D1C8
		private OmexConstants.AppState? ParseState(XElement element, XName stateKey, BaseAsyncCommand.LogResponseParseFailureEventCallback logParseFailureCallback)
		{
			OmexConstants.AppState? result = null;
			string text = (string)element.Attribute(stateKey);
			int value;
			if (!string.IsNullOrWhiteSpace(text) && int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
			{
				result = new OmexConstants.AppState?((OmexConstants.AppState)value);
			}
			if (result == null)
			{
				AppStateResponseAsset.Tracer.TraceError<XElement>(0L, "AppStateResponseAsset.ParseState: Unable to parse state for: {0}", element);
				logParseFailureCallback(ApplicationLogicEventLogConstants.Tuple_AppStateResponseInvalidState, this.MarketplaceAssetID, element);
			}
			return result;
		}

		// Token: 0x040005CF RID: 1487
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;
	}
}
