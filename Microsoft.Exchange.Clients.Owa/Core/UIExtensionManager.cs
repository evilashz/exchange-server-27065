using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200026C RID: 620
	internal sealed class UIExtensionManager
	{
		// Token: 0x060014D0 RID: 5328 RVA: 0x0007EA84 File Offset: 0x0007CC84
		private UIExtensionManager()
		{
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0007EA8C File Offset: 0x0007CC8C
		internal static List<UIExtensionManager.NewMenuExtensionItem>.Enumerator GetMenuItemEnumerator()
		{
			return UIExtensionManager.menuItemEntries.GetEnumerator();
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0007EA98 File Offset: 0x0007CC98
		internal static List<UIExtensionManager.RightClickMenuExtensionItem>.Enumerator GetMessageContextMenuItemEnumerator()
		{
			return UIExtensionManager.contextMenuItemEntries.GetEnumerator();
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0007EAA4 File Offset: 0x0007CCA4
		internal static List<UIExtensionManager.NavigationExtensionItem>.Enumerator GetNavigationBarEnumerator()
		{
			return UIExtensionManager.navigationBarEntries.GetEnumerator();
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0007EAB0 File Offset: 0x0007CCB0
		internal static void Initialize()
		{
			if (!File.Exists(UIExtensionManager.FullExtensionFileName))
			{
				return;
			}
			try
			{
				using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(UIExtensionManager.FullExtensionFileName))
				{
					xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
					while (xmlTextReader.Read())
					{
						if (xmlTextReader.NodeType == XmlNodeType.Element)
						{
							if (xmlTextReader.Name == "MainNavigationBarExtensions")
							{
								UIExtensionManager.ParseNavigationBarEntries(xmlTextReader, UIExtensionManager.navigationBarEntries);
							}
							else if (xmlTextReader.Name == "NewItemMenuEntries")
							{
								UIExtensionManager.ParseNewItemMenuEntries(xmlTextReader, UIExtensionManager.menuItemEntries);
							}
							else if (xmlTextReader.Name == "RightClickMenuExtensions")
							{
								UIExtensionManager.ParseRightClickMenuItemEntries(xmlTextReader, UIExtensionManager.contextMenuItemEntries);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_CustomizationUIExtensionParseError, string.Empty, new object[]
				{
					UIExtensionManager.FullExtensionFileName
				});
				UIExtensionManager.navigationBarEntries.Clear();
				UIExtensionManager.menuItemEntries.Clear();
				UIExtensionManager.contextMenuItemEntries.Clear();
			}
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0007EBB8 File Offset: 0x0007CDB8
		private static void ParseNavigationBarEntries(XmlTextReader reader, List<UIExtensionManager.NavigationExtensionItem> entries)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element && reader.Name == "MainNavigationBarEntry")
				{
					entries.Add(new UIExtensionManager.NavigationExtensionItem(UIExtensionManager.CreateIconPath(reader, "LargeIcon", false), UIExtensionManager.CreateIconPath(reader, "SmallIcon", false), reader.GetAttribute("URL"), UIExtensionManager.ParseMultiLanguageText(reader, "MainNavigationBarEntry")));
				}
				else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "MainNavigationBarExtensions")
				{
					return;
				}
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0007EC44 File Offset: 0x0007CE44
		private static void ParseNewItemMenuEntries(XmlTextReader reader, List<UIExtensionManager.NewMenuExtensionItem> entries)
		{
			while (reader.Read() && entries.Count < 10)
			{
				if (reader.NodeType == XmlNodeType.Element && reader.Name == "NewItemMenuEntry")
				{
					entries.Add(new UIExtensionManager.NewMenuExtensionItem(UIExtensionManager.CreateIconPath(reader, "Icon", false), reader.GetAttribute("ItemType"), UIExtensionManager.ParseMultiLanguageText(reader, "NewItemMenuEntry")));
				}
				else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "NewItemMenuEntries")
				{
					return;
				}
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0007ECCC File Offset: 0x0007CECC
		private static void ParseRightClickMenuItemEntries(XmlTextReader reader, List<UIExtensionManager.RightClickMenuExtensionItem> entries)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element && reader.Name == "RightClickMenuEntry")
				{
					entries.Add(new UIExtensionManager.RightClickMenuExtensionItem(UIExtensionManager.CreateIconPath(reader, "Icon", true), reader.GetAttribute("filter"), reader.GetAttribute("URL"), UIExtensionManager.ParseMultiLanguageText(reader, "RightClickMenuEntry")));
				}
				else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "RightClickMenuExtensions")
				{
					return;
				}
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0007ED58 File Offset: 0x0007CF58
		private static string CreateIconPath(XmlTextReader reader, string attributeName, bool isOptional)
		{
			string attribute = reader.GetAttribute(attributeName);
			if (isOptional && string.IsNullOrEmpty(attribute))
			{
				return null;
			}
			return OwaUrl.ApplicationRoot.ImplicitUrl + "forms/Customization/" + attribute;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0007ED90 File Offset: 0x0007CF90
		private static Dictionary<string, string> ParseMultiLanguageText(XmlTextReader reader, string expectedEndTag)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			while (reader.Read() && (reader.NodeType != XmlNodeType.EndElement || !(reader.Name == expectedEndTag)))
			{
				if (reader.NodeType == XmlNodeType.Element && reader.Name == "string")
				{
					string text = reader.GetAttribute("language") ?? string.Empty;
					string attribute = reader.GetAttribute("text");
					if (attribute != null)
					{
						dictionary[text.ToLowerInvariant()] = attribute;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x040010A0 RID: 4256
		internal const string ExtensionFolderName = "forms/Customization/";

		// Token: 0x040010A1 RID: 4257
		internal const string ExtensionFileName = "UIExtensions.xml";

		// Token: 0x040010A2 RID: 4258
		private const int MaxNewMenuItemCount = 10;

		// Token: 0x040010A3 RID: 4259
		internal static readonly string FullExtensionFileName = Path.Combine(HttpRuntime.AppDomainAppPath + "forms/Customization/", "UIExtensions.xml").Replace('/', '\\');

		// Token: 0x040010A4 RID: 4260
		private static List<UIExtensionManager.NavigationExtensionItem> navigationBarEntries = new List<UIExtensionManager.NavigationExtensionItem>();

		// Token: 0x040010A5 RID: 4261
		private static List<UIExtensionManager.NewMenuExtensionItem> menuItemEntries = new List<UIExtensionManager.NewMenuExtensionItem>();

		// Token: 0x040010A6 RID: 4262
		private static List<UIExtensionManager.RightClickMenuExtensionItem> contextMenuItemEntries = new List<UIExtensionManager.RightClickMenuExtensionItem>();

		// Token: 0x0200026D RID: 621
		internal abstract class MultiLanguageTextBase
		{
			// Token: 0x060014DB RID: 5339 RVA: 0x0007EE68 File Offset: 0x0007D068
			internal string GetTextByLanguage(string language)
			{
				string text = language.ToLowerInvariant();
				for (int i = text.Length; i >= 0; i = text.LastIndexOf('-'))
				{
					text = text.Substring(0, i);
					if (this.multiLanguageText.ContainsKey(text))
					{
						return this.multiLanguageText[text];
					}
				}
				if (this.multiLanguageText.ContainsKey(string.Empty))
				{
					return this.multiLanguageText[string.Empty];
				}
				return string.Empty;
			}

			// Token: 0x060014DC RID: 5340 RVA: 0x0007EEDE File Offset: 0x0007D0DE
			protected MultiLanguageTextBase(Dictionary<string, string> multiLanguageText)
			{
				this.multiLanguageText = multiLanguageText;
			}

			// Token: 0x040010A7 RID: 4263
			private readonly Dictionary<string, string> multiLanguageText;
		}

		// Token: 0x0200026E RID: 622
		internal class NavigationExtensionItem : UIExtensionManager.MultiLanguageTextBase
		{
			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x060014DD RID: 5341 RVA: 0x0007EEED File Offset: 0x0007D0ED
			internal string LargeIcon
			{
				get
				{
					return this.largeIcon;
				}
			}

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x060014DE RID: 5342 RVA: 0x0007EEF5 File Offset: 0x0007D0F5
			internal string SmallIcon
			{
				get
				{
					return this.smallIcon;
				}
			}

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x060014DF RID: 5343 RVA: 0x0007EEFD File Offset: 0x0007D0FD
			internal string TargetUrl
			{
				get
				{
					return this.targetUrl;
				}
			}

			// Token: 0x060014E0 RID: 5344 RVA: 0x0007EF05 File Offset: 0x0007D105
			internal NavigationExtensionItem(string largeIcon, string smallIcon, string targetUrl, Dictionary<string, string> multiLanguageText) : base(multiLanguageText)
			{
				this.largeIcon = largeIcon;
				this.smallIcon = smallIcon;
				this.targetUrl = targetUrl;
			}

			// Token: 0x040010A8 RID: 4264
			private readonly string largeIcon;

			// Token: 0x040010A9 RID: 4265
			private readonly string smallIcon;

			// Token: 0x040010AA RID: 4266
			private readonly string targetUrl;
		}

		// Token: 0x0200026F RID: 623
		internal class NewMenuExtensionItem : UIExtensionManager.MultiLanguageTextBase
		{
			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x060014E1 RID: 5345 RVA: 0x0007EF24 File Offset: 0x0007D124
			internal string Icon
			{
				get
				{
					return this.icon;
				}
			}

			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0007EF2C File Offset: 0x0007D12C
			internal string CustomType
			{
				get
				{
					return this.customType;
				}
			}

			// Token: 0x060014E3 RID: 5347 RVA: 0x0007EF34 File Offset: 0x0007D134
			internal NewMenuExtensionItem(string icon, string customType, Dictionary<string, string> multiLanguageText) : base(multiLanguageText)
			{
				this.icon = icon;
				this.customType = customType;
			}

			// Token: 0x040010AB RID: 4267
			private readonly string icon;

			// Token: 0x040010AC RID: 4268
			private readonly string customType;
		}

		// Token: 0x02000270 RID: 624
		internal class RightClickMenuExtensionItem : UIExtensionManager.MultiLanguageTextBase
		{
			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0007EF4B File Offset: 0x0007D14B
			internal string Icon
			{
				get
				{
					return this.icon;
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x060014E5 RID: 5349 RVA: 0x0007EF53 File Offset: 0x0007D153
			internal string CustomType
			{
				get
				{
					return this.customType;
				}
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0007EF5B File Offset: 0x0007D15B
			internal string TargetUrl
			{
				get
				{
					return this.targetUrl;
				}
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0007EF63 File Offset: 0x0007D163
			internal bool HasQueryString
			{
				get
				{
					return this.hasQueryString;
				}
			}

			// Token: 0x060014E8 RID: 5352 RVA: 0x0007EF6B File Offset: 0x0007D16B
			internal RightClickMenuExtensionItem(string icon, string customType, string targetUrl, Dictionary<string, string> multiLanguageText) : base(multiLanguageText)
			{
				this.icon = icon;
				this.customType = customType;
				this.targetUrl = targetUrl;
				this.hasQueryString = targetUrl.Contains("?");
			}

			// Token: 0x040010AD RID: 4269
			private readonly string icon;

			// Token: 0x040010AE RID: 4270
			private readonly string customType;

			// Token: 0x040010AF RID: 4271
			private readonly string targetUrl;

			// Token: 0x040010B0 RID: 4272
			private readonly bool hasQueryString;
		}

		// Token: 0x02000271 RID: 625
		private struct XmlTags
		{
			// Token: 0x040010B1 RID: 4273
			public const string MainNavigationBarExtensions = "MainNavigationBarExtensions";

			// Token: 0x040010B2 RID: 4274
			public const string MainNavigationBarEntry = "MainNavigationBarEntry";

			// Token: 0x040010B3 RID: 4275
			public const string NewItemMenuEntries = "NewItemMenuEntries";

			// Token: 0x040010B4 RID: 4276
			public const string NewItemMenuEntry = "NewItemMenuEntry";

			// Token: 0x040010B5 RID: 4277
			public const string Language = "language";

			// Token: 0x040010B6 RID: 4278
			public const string Icon = "Icon";

			// Token: 0x040010B7 RID: 4279
			public const string LargeIcon = "LargeIcon";

			// Token: 0x040010B8 RID: 4280
			public const string SmallIcon = "SmallIcon";

			// Token: 0x040010B9 RID: 4281
			public const string URL = "URL";

			// Token: 0x040010BA RID: 4282
			public const string String = "string";

			// Token: 0x040010BB RID: 4283
			public const string Text = "text";

			// Token: 0x040010BC RID: 4284
			public const string ItemType = "ItemType";

			// Token: 0x040010BD RID: 4285
			public const string Filter = "filter";

			// Token: 0x040010BE RID: 4286
			public const string OWAUICustomizations = "OWAUICustomizations";

			// Token: 0x040010BF RID: 4287
			public const string RightClickMenuExtensions = "RightClickMenuExtensions";

			// Token: 0x040010C0 RID: 4288
			public const string RightClickMenuEntry = "RightClickMenuEntry";
		}
	}
}
