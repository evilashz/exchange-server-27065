using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000252 RID: 594
	public static class SmallIconManager
	{
		// Token: 0x060013EB RID: 5099 RVA: 0x0007A114 File Offset: 0x00078314
		private static Dictionary<FolderSharingFlag, Dictionary<string, SmallIconManager.SmallIcon>> CreateSharingFolderIconMapping()
		{
			Dictionary<FolderSharingFlag, Dictionary<string, SmallIconManager.SmallIcon>> dictionary = new Dictionary<FolderSharingFlag, Dictionary<string, SmallIconManager.SmallIcon>>();
			dictionary[FolderSharingFlag.SharedIn] = new Dictionary<string, SmallIconManager.SmallIcon>();
			dictionary[FolderSharingFlag.SharedOut] = new Dictionary<string, SmallIconManager.SmallIcon>();
			dictionary[FolderSharingFlag.WebCalendar] = new Dictionary<string, SmallIconManager.SmallIcon>();
			dictionary[FolderSharingFlag.SharedIn]["IPF.Note"] = new SmallIconManager.SmallIcon(355, -1018465893);
			dictionary[FolderSharingFlag.SharedOut]["IPF.Note"] = new SmallIconManager.SmallIcon(359, -1018465893);
			dictionary[FolderSharingFlag.SharedIn]["IPF.Appointment"] = new SmallIconManager.SmallIcon(356, -1018465893);
			dictionary[FolderSharingFlag.SharedOut]["IPF.Appointment"] = new SmallIconManager.SmallIcon(360, -1018465893);
			dictionary[FolderSharingFlag.SharedIn]["IPF.Contact"] = new SmallIconManager.SmallIcon(357, -1018465893);
			dictionary[FolderSharingFlag.SharedOut]["IPF.Contact"] = new SmallIconManager.SmallIcon(361, -1018465893);
			dictionary[FolderSharingFlag.SharedIn]["IPF.Task"] = new SmallIconManager.SmallIcon(358, -1018465893);
			dictionary[FolderSharingFlag.SharedOut]["IPF.Task"] = new SmallIconManager.SmallIcon(362, -1018465893);
			dictionary[FolderSharingFlag.WebCalendar]["IPF.Appointment"] = new SmallIconManager.SmallIcon(458, -1018465893);
			return dictionary;
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0007A26C File Offset: 0x0007846C
		internal static void Initialize()
		{
			SmallIconManager.smallIconTable = new Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>>();
			SmallIconManager.prefixMatchSmallIconTable = new Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>>();
			string xmlFilePath = Path.Combine(HttpRuntime.AppDomainAppPath, "SmallIcons.xml");
			SmallIconManager.LoadXmlData(xmlFilePath, null, SmallIconManager.prefixMatchSmallIconTable, SmallIconManager.smallIconTable);
			string fullExtensionFileName = UIExtensionManager.FullExtensionFileName;
			if (!File.Exists(fullExtensionFileName))
			{
				return;
			}
			try
			{
				Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>> dictionary = new Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>>();
				Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>> dictionary2 = new Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>>();
				SmallIconManager.LoadXmlData(fullExtensionFileName, "forms/Customization/", dictionary2, dictionary);
				SmallIconManager.MergeSmallIconTable(SmallIconManager.smallIconTable, dictionary);
				SmallIconManager.MergeSmallIconTable(SmallIconManager.prefixMatchSmallIconTable, dictionary2);
			}
			catch (Exception)
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_CustomizationUIExtensionParseError, string.Empty, new object[]
				{
					fullExtensionFileName
				});
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0007A320 File Offset: 0x00078520
		internal static void RenderItemIconUrl(TextWriter writer, UserContext userContext, string itemClass)
		{
			SmallIconManager.RenderItemIconUrl(writer, userContext, itemClass, false, false);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0007A32C File Offset: 0x0007852C
		internal static void RenderItemIconUrl(TextWriter writer, UserContext userContext, string itemClass, string defaultItemClass)
		{
			SmallIconManager.RenderItemIconUrl(writer, userContext, itemClass, defaultItemClass, false, false, -1);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0007A33A File Offset: 0x0007853A
		internal static void RenderItemIconUrl(TextWriter writer, UserContext userContext, string itemClass, int iconFlag)
		{
			SmallIconManager.RenderItemIconUrl(writer, userContext, itemClass, null, false, false, iconFlag);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0007A348 File Offset: 0x00078548
		internal static void RenderItemIconUrl(TextWriter writer, UserContext userContext, string itemClass, bool isInConflict, bool isRead)
		{
			SmallIconManager.RenderItemIconUrl(writer, userContext, itemClass, null, isInConflict, isRead, -1);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0007A357 File Offset: 0x00078557
		private static void RenderIconUrl(TextWriter writer, UserContext userContext, SmallIconManager.SmallIcon smallIcon)
		{
			if (smallIcon.IsCustom)
			{
				writer.Write(smallIcon.CustomUrl);
				return;
			}
			userContext.RenderThemeFileUrl(writer, smallIcon.ThemeId);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0007A37C File Offset: 0x0007857C
		internal static void RenderIcon(TextWriter writer, UserContext userContext, SmallIconManager.SmallIcon smallIcon, bool showTooltip, string styleClass, params string[] extraAttributes)
		{
			if (smallIcon.IsCustom)
			{
				writer.Write("<img src=\"");
				writer.Write(smallIcon.CustomUrl);
				writer.Write("\"");
				foreach (string value in extraAttributes)
				{
					writer.Write(" ");
					writer.Write(value);
				}
				writer.Write(">");
				return;
			}
			if (showTooltip)
			{
				userContext.RenderThemeImageWithToolTip(writer, (ThemeFileId)smallIcon.ThemeId, null, smallIcon.AltId, extraAttributes);
				return;
			}
			userContext.RenderThemeImage(writer, (ThemeFileId)smallIcon.ThemeId, null, extraAttributes);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0007A410 File Offset: 0x00078610
		internal static void RenderItemIconUrl(TextWriter writer, UserContext userContext, string itemClass, string defaultItemClass, bool isInConflict, bool isRead, int iconFlag)
		{
			SmallIconManager.SmallIcon itemSmallIcon = SmallIconManager.GetItemSmallIcon(itemClass, defaultItemClass, isInConflict, isRead, iconFlag);
			SmallIconManager.RenderIconUrl(writer, userContext, itemSmallIcon);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0007A433 File Offset: 0x00078633
		internal static void RenderItemIcon(TextWriter writer, UserContext userContext, string itemClass, params string[] extraAttributes)
		{
			SmallIconManager.RenderItemIcon(writer, userContext, itemClass, false, string.Empty, extraAttributes);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0007A444 File Offset: 0x00078644
		internal static void RenderItemIcon(TextWriter writer, UserContext userContext, string itemClass, bool showTooltip, string styleClass, params string[] extraAttributes)
		{
			SmallIconManager.RenderItemIcon(writer, userContext, itemClass, showTooltip, null, styleClass, extraAttributes);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0007A454 File Offset: 0x00078654
		internal static void RenderItemIcon(TextWriter writer, UserContext userContext, string itemClass, bool showTooltip, string defaultItemClass, string styleClass, params string[] extraAttributes)
		{
			SmallIconManager.RenderItemIcon(writer, userContext, itemClass, showTooltip, defaultItemClass, false, false, -1, styleClass, extraAttributes);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0007A474 File Offset: 0x00078674
		internal static void RenderItemIcon(TextWriter writer, UserContext userContext, string itemClass, bool showTooltip, int iconFlag, string styleClass, params string[] extraAttributes)
		{
			SmallIconManager.RenderItemIcon(writer, userContext, itemClass, showTooltip, null, false, false, iconFlag, styleClass, extraAttributes);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0007A494 File Offset: 0x00078694
		internal static void RenderItemIcon(TextWriter writer, UserContext userContext, string itemClass, bool showTooltip, bool isInConflict, bool isRead, string styleClass, params string[] extraAttributes)
		{
			SmallIconManager.RenderItemIcon(writer, userContext, itemClass, showTooltip, null, isInConflict, isRead, -1, styleClass, extraAttributes);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0007A4B4 File Offset: 0x000786B4
		internal static void RenderItemIcon(TextWriter writer, UserContext userContext, string itemClass, bool showTooltip, string defaultItemClass, bool isInConflict, bool isRead, int iconFlag, string styleClass, params string[] extraAttributes)
		{
			SmallIconManager.SmallIcon itemSmallIcon = SmallIconManager.GetItemSmallIcon(itemClass, defaultItemClass, isInConflict, isRead, iconFlag);
			SmallIconManager.RenderIcon(writer, userContext, itemSmallIcon, showTooltip, styleClass, extraAttributes);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0007A4E0 File Offset: 0x000786E0
		public static SmallIconManager.SmallIcon GetItemSmallIcon(string itemClass, string defaultItemClass, bool isInConflict, bool isRead, int iconFlag)
		{
			if (isInConflict)
			{
				return new SmallIconManager.SmallIcon(82, -1240042979);
			}
			if (!EnumValidator.IsValidValue<IconIndex>((IconIndex)iconFlag))
			{
				iconFlag = -1;
			}
			if (iconFlag == -1 && isRead)
			{
				iconFlag = 256;
			}
			else if (iconFlag == 306 && isRead)
			{
				iconFlag = 256;
			}
			SmallIconManager.SmallIcon smallIcon = SmallIconManager.LookupSmallIcon(itemClass, iconFlag);
			if (smallIcon == null && iconFlag == 256)
			{
				iconFlag = -1;
				smallIcon = SmallIconManager.LookupSmallIcon(itemClass, iconFlag);
			}
			if (iconFlag == -1 && isRead && smallIcon == null)
			{
				smallIcon = SmallIconManager.PrefixMatchLookupSmallIcon(itemClass, 256);
			}
			if (smallIcon == null)
			{
				smallIcon = SmallIconManager.PrefixMatchLookupSmallIcon(itemClass, iconFlag);
			}
			if (smallIcon == null)
			{
				smallIcon = SmallIconManager.LookupSmallIcon(itemClass, -1);
			}
			if (smallIcon == null && defaultItemClass != null)
			{
				smallIcon = SmallIconManager.LookupSmallIcon(defaultItemClass, iconFlag);
			}
			if (smallIcon == null && defaultItemClass != null)
			{
				smallIcon = SmallIconManager.PrefixMatchLookupSmallIcon(defaultItemClass, iconFlag);
			}
			if (smallIcon == null && (iconFlag == 261 || iconFlag == 262))
			{
				smallIcon = SmallIconManager.LookupSmallIcon("IPM.Note", iconFlag);
			}
			if (smallIcon == null)
			{
				smallIcon = (isRead ? new SmallIconManager.SmallIcon(132, -1075414859) : new SmallIconManager.SmallIcon(133, -1679159840));
			}
			return smallIcon;
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0007A5E4 File Offset: 0x000787E4
		internal static void RenderFolderIconUrl(TextWriter writer, UserContext userContext, string containerClass)
		{
			SmallIconManager.RenderFolderIconUrl(writer, userContext, containerClass, FolderSharingFlag.None);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0007A5F0 File Offset: 0x000787F0
		private static SmallIconManager.SmallIcon GetFolderSmallIcon(UserContext userContext, string containerClass, FolderSharingFlag sharingFlag)
		{
			SmallIconManager.SmallIcon smallIcon;
			if (containerClass == null)
			{
				smallIcon = SmallIconManager.normalFolderIcon;
			}
			else
			{
				smallIcon = SmallIconManager.LookupSmallIcon(containerClass, -1);
				if (smallIcon == null)
				{
					smallIcon = SmallIconManager.PrefixMatchLookupSmallIcon(containerClass, -1);
					if (smallIcon == null)
					{
						smallIcon = SmallIconManager.normalFolderIcon;
					}
				}
			}
			if (!smallIcon.IsCustom && sharingFlag != FolderSharingFlag.None)
			{
				smallIcon = (SmallIconManager.GetIconForSharedFolder(containerClass, sharingFlag) ?? smallIcon);
			}
			return smallIcon;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0007A640 File Offset: 0x00078840
		internal static void RenderFolderIconUrl(TextWriter writer, UserContext userContext, string containerClass, FolderSharingFlag sharingFlag)
		{
			SmallIconManager.SmallIcon folderSmallIcon = SmallIconManager.GetFolderSmallIcon(userContext, containerClass, sharingFlag);
			SmallIconManager.RenderIconUrl(writer, userContext, folderSmallIcon);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0007A660 File Offset: 0x00078860
		internal static void RenderFolderIcon(TextWriter writer, UserContext userContext, string containerClass, FolderSharingFlag sharingFlag, bool showTooltip, params string[] extraAttributes)
		{
			SmallIconManager.SmallIcon folderSmallIcon = SmallIconManager.GetFolderSmallIcon(userContext, containerClass, sharingFlag);
			SmallIconManager.RenderIcon(writer, userContext, folderSmallIcon, showTooltip, string.Empty, extraAttributes);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0007A688 File Offset: 0x00078888
		private static SmallIconManager.SmallIcon GetIconForSharedFolder(string containerClass, FolderSharingFlag sharingFlag)
		{
			if (sharingFlag == FolderSharingFlag.None)
			{
				return null;
			}
			if (string.IsNullOrEmpty(containerClass))
			{
				containerClass = "IPF.Note";
			}
			foreach (KeyValuePair<string, SmallIconManager.SmallIcon> keyValuePair in SmallIconManager.sharingFolderIconMapping[sharingFlag])
			{
				if (ObjectClass.IsOfClass(containerClass, keyValuePair.Key))
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0007A70C File Offset: 0x0007890C
		private static SmallIconManager.SmallIcon GetFileSmallIcon(UserContext userContext, string fileExtension, int iconFlag)
		{
			SmallIconManager.SmallIcon smallIcon = SmallIconManager.LookupSmallIcon(fileExtension, iconFlag);
			if (smallIcon == null)
			{
				smallIcon = new SmallIconManager.SmallIcon(129, -1018465893);
			}
			return smallIcon;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0007A735 File Offset: 0x00078935
		internal static void RenderFileIcon(TextWriter writer, UserContext userContext, string fileExtension, params string[] extraAttributes)
		{
			SmallIconManager.RenderFileIcon(writer, userContext, fileExtension, -1, false, string.Empty, extraAttributes);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0007A747 File Offset: 0x00078947
		internal static void RenderFileIcon(TextWriter writer, UserContext userContext, string fileExtension, string styleClass, params string[] extraAttributes)
		{
			SmallIconManager.RenderFileIcon(writer, userContext, fileExtension, -1, false, styleClass, extraAttributes);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0007A758 File Offset: 0x00078958
		internal static void RenderFileIcon(TextWriter writer, UserContext userContext, string fileExtension, int iconFlag, bool showTooltip, string styleClass, params string[] extraAttributes)
		{
			SmallIconManager.SmallIcon fileSmallIcon = SmallIconManager.GetFileSmallIcon(userContext, fileExtension, iconFlag);
			SmallIconManager.RenderIcon(writer, userContext, fileSmallIcon, showTooltip, styleClass, extraAttributes);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0007A77C File Offset: 0x0007897C
		internal static void RenderFileIconUrl(TextWriter writer, UserContext userContext, string fileExtension)
		{
			SmallIconManager.RenderFileIconUrl(writer, userContext, fileExtension, -1);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0007A788 File Offset: 0x00078988
		internal static void RenderFileIconUrl(TextWriter writer, UserContext userContext, string fileExtension, int iconFlag)
		{
			SmallIconManager.SmallIcon fileSmallIcon = SmallIconManager.GetFileSmallIcon(userContext, fileExtension, iconFlag);
			SmallIconManager.RenderIconUrl(writer, userContext, fileSmallIcon);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0007A7A8 File Offset: 0x000789A8
		private static XmlTextReader InitializeXmlTextReader(string xmlFilePath)
		{
			ExTraceGlobals.SmallIconCallTracer.TraceDebug<string>(0L, "InitializeXmlTextReader: XmlFilePath = '{0}'", xmlFilePath);
			if (!File.Exists(xmlFilePath))
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_SmallIconsFileNotFound, string.Empty, new object[]
				{
					xmlFilePath
				});
				throw new OwaSmallIconManagerInitializationException("SmallIcon XML file is not found: '" + xmlFilePath + "'");
			}
			XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(xmlFilePath);
			xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
			xmlTextReader.NameTable.Add("SmallIconMappings");
			xmlTextReader.NameTable.Add("Mapping");
			xmlTextReader.NameTable.Add("ItemClass");
			xmlTextReader.NameTable.Add("IconFlag");
			xmlTextReader.NameTable.Add("SmallIcon");
			xmlTextReader.NameTable.Add("PrefixMatch");
			xmlTextReader.NameTable.Add("Alt");
			return xmlTextReader;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0007A888 File Offset: 0x00078A88
		private static void LoadXmlData(string xmlFilePath, string folderName, Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>> prefixIconTable, Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>> iconTable)
		{
			ExTraceGlobals.SmallIconCallTracer.TraceDebug<string>(0L, "LoadXmlData: XmlFilePath = '{0}'", xmlFilePath);
			using (XmlTextReader xmlTextReader = SmallIconManager.InitializeXmlTextReader(xmlFilePath))
			{
				bool flag = false;
				StringBuilder stringBuilder = new StringBuilder();
				while (xmlTextReader.Read())
				{
					XmlNodeType nodeType = xmlTextReader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if (xmlTextReader.Name == xmlTextReader.NameTable.Get("SmallIconMappings"))
							{
								flag = false;
							}
						}
					}
					else if (xmlTextReader.Name == xmlTextReader.NameTable.Get("SmallIconMappings"))
					{
						flag = true;
					}
					else if (flag && xmlTextReader.Name == xmlTextReader.NameTable.Get("Mapping"))
					{
						SmallIconManager.ParseMappingElement(xmlTextReader, folderName, stringBuilder, prefixIconTable, iconTable);
					}
				}
				if (stringBuilder.Length != 0)
				{
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_SmallIconsAltReferenceInvalid, string.Empty, new object[]
					{
						stringBuilder.ToString()
					});
				}
			}
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0007A98C File Offset: 0x00078B8C
		private static void ParseMappingElement(XmlTextReader xmlTextReader, string folderName, StringBuilder invalidAltIds, Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>> prefixIconTable, Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>> iconTable)
		{
			ExTraceGlobals.SmallIconCallTracer.TraceDebug(0L, "ParseMappingElement");
			string text = null;
			int num = -1;
			string text2 = null;
			bool flag = false;
			Strings.IDs ds = -1018465893;
			if (xmlTextReader.MoveToAttribute(xmlTextReader.NameTable.Get("ItemClass")))
			{
				text = xmlTextReader.Value;
			}
			if (xmlTextReader.MoveToAttribute(xmlTextReader.NameTable.Get("SmallIcon")))
			{
				text2 = xmlTextReader.Value;
			}
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
			{
				ExTraceGlobals.SmallIconTracer.TraceDebug<string, string>(0L, "Either ItemClass:'{0}' or SmallIcon:'{1}' is not valid.", text, text2);
				return;
			}
			if (xmlTextReader.MoveToAttribute(xmlTextReader.NameTable.Get("IconFlag")))
			{
				string value = xmlTextReader.Value;
				try
				{
					num = (int)Enum.Parse(typeof(IconIndex), value, true);
					goto IL_C9;
				}
				catch (ArgumentException)
				{
					num = value.GetHashCode();
					goto IL_C9;
				}
			}
			num = -1;
			IL_C9:
			flag = (xmlTextReader.MoveToAttribute(xmlTextReader.NameTable.Get("PrefixMatch")) && string.Equals(xmlTextReader.Value, "true", StringComparison.OrdinalIgnoreCase));
			if (xmlTextReader.MoveToAttribute(xmlTextReader.NameTable.Get("Alt")))
			{
				string value2 = xmlTextReader.Value;
				try
				{
					ds = (Strings.IDs)Enum.Parse(typeof(Strings.IDs), value2, true);
					goto IL_160;
				}
				catch (ArgumentException)
				{
					if (invalidAltIds.Length != 0)
					{
						invalidAltIds.Append(",");
					}
					invalidAltIds.Append(value2);
					ds = -1018465893;
					goto IL_160;
				}
			}
			ds = -1018465893;
			IL_160:
			SmallIconManager.SmallIcon smallIcon = (folderName == null) ? new SmallIconManager.SmallIcon(ThemeFileList.Add(text2, true), ds) : new SmallIconManager.SmallIcon(OwaUrl.ApplicationRoot.ImplicitUrl + folderName + text2, ds);
			if (flag)
			{
				if (!prefixIconTable.ContainsKey(num))
				{
					prefixIconTable[num] = new Dictionary<string, SmallIconManager.SmallIcon>(StringComparer.OrdinalIgnoreCase);
				}
				prefixIconTable[num][text] = smallIcon;
			}
			else
			{
				if (!iconTable.ContainsKey(num))
				{
					iconTable[num] = new Dictionary<string, SmallIconManager.SmallIcon>(StringComparer.OrdinalIgnoreCase);
				}
				iconTable[num][text] = smallIcon;
			}
			ExTraceGlobals.SmallIconDataTracer.TraceDebug(0L, "Add {0}PrefixMatch mapping: IconFlag = '{1}', ItemClass = '{2}' or SmallIcon:'{3}', Alt:'{4}'.", new object[]
			{
				flag ? string.Empty : "Non-",
				num,
				text,
				(folderName == null) ? ("Index " + smallIcon.ThemeId) : text2,
				ds
			});
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0007AC04 File Offset: 0x00078E04
		private static SmallIconManager.SmallIcon PrefixMatchLookupSmallIcon(string itemClass, int iconFlag)
		{
			ExTraceGlobals.SmallIconCallTracer.TraceDebug<string, int>(0L, "PrefixMatchLookupSmallIcon: ItemClass = '{0}', IconFlag = '{1}'", itemClass, iconFlag);
			Dictionary<string, SmallIconManager.SmallIcon> dictionary = null;
			if (!SmallIconManager.prefixMatchSmallIconTable.TryGetValue(iconFlag, out dictionary))
			{
				ExTraceGlobals.SmallIconTracer.TraceDebug<int>(0L, "Can not find IconFlag from the prefixMatchSmallIconTable: IconFlag = '{0}'", iconFlag);
				return null;
			}
			if (dictionary.ContainsKey(itemClass))
			{
				SmallIconManager.SmallIcon smallIcon = dictionary[itemClass];
				ExTraceGlobals.SmallIconTracer.TraceDebug<string, int, Strings.IDs>(0L, "Found exact match of ItemClass: ItemClass = '{0}', ThemeFileIndex = '{1}', Alt = '{2}'", itemClass, smallIcon.ThemeId, smallIcon.AltId);
				return smallIcon;
			}
			foreach (string text in dictionary.Keys)
			{
				if (string.Compare(text, 0, itemClass, 0, text.Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					SmallIconManager.SmallIcon smallIcon2 = dictionary[text];
					ExTraceGlobals.SmallIconTracer.TraceDebug<string, int, Strings.IDs>(0L, "Found prefix match of ItemClass: ItemClass = '{0}', ThemeFileIndex = '{1}', Alt = '{2}'", itemClass, smallIcon2.ThemeId, smallIcon2.AltId);
					return smallIcon2;
				}
			}
			return null;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0007ACFC File Offset: 0x00078EFC
		private static SmallIconManager.SmallIcon LookupSmallIcon(string itemClass, int iconFlag)
		{
			ExTraceGlobals.SmallIconCallTracer.TraceDebug<string, int>(0L, "LookupSmallIcon: ItemClass = '{0}', IconFlag = '{1}'", itemClass, iconFlag);
			Dictionary<string, SmallIconManager.SmallIcon> dictionary = null;
			if (SmallIconManager.smallIconTable.TryGetValue(iconFlag, out dictionary) && dictionary.ContainsKey(itemClass))
			{
				SmallIconManager.SmallIcon smallIcon = dictionary[itemClass];
				ExTraceGlobals.SmallIconTracer.TraceDebug<string, int, Strings.IDs>(0L, "Found exact match of ItemClass: ItemClass = '{0}', ThemeFileIndex = '{1}', Alt = {2}", itemClass, smallIcon.ThemeId, smallIcon.AltId);
				return smallIcon;
			}
			return null;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0007AD60 File Offset: 0x00078F60
		private static void MergeSmallIconTable(Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>> iconTable, Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>> tempIconTable)
		{
			foreach (KeyValuePair<int, Dictionary<string, SmallIconManager.SmallIcon>> keyValuePair in tempIconTable)
			{
				Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>>.Enumerator enumerator;
				if (iconTable.ContainsKey(keyValuePair.Key))
				{
					KeyValuePair<int, Dictionary<string, SmallIconManager.SmallIcon>> keyValuePair2 = enumerator.Current;
					using (Dictionary<string, SmallIconManager.SmallIcon>.Enumerator enumerator2 = keyValuePair2.Value.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<int, Dictionary<string, SmallIconManager.SmallIcon>> keyValuePair3 = enumerator.Current;
							Dictionary<string, SmallIconManager.SmallIcon> dictionary = iconTable[keyValuePair3.Key];
							KeyValuePair<string, SmallIconManager.SmallIcon> keyValuePair4 = enumerator2.Current;
							string key = keyValuePair4.Key;
							KeyValuePair<string, SmallIconManager.SmallIcon> keyValuePair5 = enumerator2.Current;
							dictionary[key] = keyValuePair5.Value;
						}
						continue;
					}
				}
				KeyValuePair<int, Dictionary<string, SmallIconManager.SmallIcon>> keyValuePair6 = enumerator.Current;
				int key2 = keyValuePair6.Key;
				KeyValuePair<int, Dictionary<string, SmallIconManager.SmallIcon>> keyValuePair7 = enumerator.Current;
				iconTable[key2] = keyValuePair7.Value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0007AE5C File Offset: 0x0007905C
		public static Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>>.Enumerator SmallIconTable
		{
			get
			{
				return SmallIconManager.smallIconTable.GetEnumerator();
			}
		}

		// Token: 0x04000DB0 RID: 3504
		private const string SmallIconXmlFile = "SmallIcons.xml";

		// Token: 0x04000DB1 RID: 3505
		private static Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>> smallIconTable;

		// Token: 0x04000DB2 RID: 3506
		private static Dictionary<int, Dictionary<string, SmallIconManager.SmallIcon>> prefixMatchSmallIconTable;

		// Token: 0x04000DB3 RID: 3507
		private static readonly SmallIconManager.SmallIcon normalFolderIcon = new SmallIconManager.SmallIcon(118, -1018465893);

		// Token: 0x04000DB4 RID: 3508
		private static readonly Dictionary<FolderSharingFlag, Dictionary<string, SmallIconManager.SmallIcon>> sharingFolderIconMapping = SmallIconManager.CreateSharingFolderIconMapping();

		// Token: 0x02000253 RID: 595
		private struct XmlTags
		{
			// Token: 0x04000DB5 RID: 3509
			public const string Mapping = "Mapping";

			// Token: 0x04000DB6 RID: 3510
			public const string ItemClass = "ItemClass";

			// Token: 0x04000DB7 RID: 3511
			public const string IconFlag = "IconFlag";

			// Token: 0x04000DB8 RID: 3512
			public const string SmallIcon = "SmallIcon";

			// Token: 0x04000DB9 RID: 3513
			public const string PrefixMatch = "PrefixMatch";

			// Token: 0x04000DBA RID: 3514
			public const string SmallIconMappings = "SmallIconMappings";

			// Token: 0x04000DBB RID: 3515
			public const string Alt = "Alt";
		}

		// Token: 0x02000254 RID: 596
		public class SmallIcon
		{
			// Token: 0x0600140E RID: 5134 RVA: 0x0007AE85 File Offset: 0x00079085
			public SmallIcon(int themeId, Strings.IDs altId)
			{
				this.themeId = themeId;
				this.altId = altId;
				this.customUrl = null;
			}

			// Token: 0x0600140F RID: 5135 RVA: 0x0007AEA2 File Offset: 0x000790A2
			public SmallIcon(string customUrl, Strings.IDs altId)
			{
				this.themeId = 0;
				this.altId = altId;
				this.customUrl = customUrl;
			}

			// Token: 0x17000550 RID: 1360
			// (get) Token: 0x06001410 RID: 5136 RVA: 0x0007AEBF File Offset: 0x000790BF
			public int ThemeId
			{
				get
				{
					return this.themeId;
				}
			}

			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x06001411 RID: 5137 RVA: 0x0007AEC7 File Offset: 0x000790C7
			public Strings.IDs AltId
			{
				get
				{
					return this.altId;
				}
			}

			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x06001412 RID: 5138 RVA: 0x0007AECF File Offset: 0x000790CF
			public string CustomUrl
			{
				get
				{
					return this.customUrl;
				}
			}

			// Token: 0x17000553 RID: 1363
			// (get) Token: 0x06001413 RID: 5139 RVA: 0x0007AED7 File Offset: 0x000790D7
			public bool IsCustom
			{
				get
				{
					return this.customUrl != null;
				}
			}

			// Token: 0x04000DBC RID: 3516
			private int themeId;

			// Token: 0x04000DBD RID: 3517
			private Strings.IDs altId;

			// Token: 0x04000DBE RID: 3518
			private string customUrl;
		}
	}
}
