using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000265 RID: 613
	public sealed class Theme
	{
		// Token: 0x06001483 RID: 5251 RVA: 0x0007D3B9 File Offset: 0x0007B5B9
		private Theme(string folderPath)
		{
			this.Load(folderPath);
			this.InitializeForCssSprites();
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0007D3E4 File Offset: 0x0007B5E4
		private static bool IsValidThemeFolder(string folderPath)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < Theme.RequiredFiles.Length; i++)
			{
				string path = Path.Combine(folderPath, Theme.RequiredFiles[i]);
				if (!File.Exists(path))
				{
					list.Add(Theme.RequiredFiles[i]);
				}
			}
			if (list.Count > 0)
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_InvalidThemeFolder, string.Empty, new object[]
				{
					folderPath,
					string.Join(", ", list.ToArray())
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0007D467 File Offset: 0x0007B667
		public static Theme Create(string folderPath)
		{
			if (folderPath == null)
			{
				throw new ArgumentNullException("folderPath");
			}
			if (!Theme.IsValidThemeFolder(folderPath))
			{
				return null;
			}
			return new Theme(folderPath);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0007D488 File Offset: 0x0007B688
		private static void ParseThemeInfoFile(string themeInfoFilePath, string folderName, out string displayName, out Strings.IDs localizedDisplayName, out int sortOrder)
		{
			XmlTextReader xmlTextReader = null;
			displayName = null;
			localizedDisplayName = -1018465893;
			sortOrder = int.MaxValue;
			try
			{
				xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(themeInfoFilePath);
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.All;
				if (!xmlTextReader.Read() || xmlTextReader.NodeType != XmlNodeType.Element || !string.Equals("Theme", xmlTextReader.Name, StringComparison.OrdinalIgnoreCase))
				{
					Theme.ThrowParserException(xmlTextReader, folderName, string.Format("Expected root element '{0}' not found.", "Theme"), ClientsEventLogConstants.Tuple_ThemeInfoExpectedElement, new object[]
					{
						"themeinfo.xml",
						folderName,
						"Theme"
					});
				}
				if (xmlTextReader.MoveToFirstAttribute())
				{
					do
					{
						if (string.Equals("DisplayName", xmlTextReader.Name, StringComparison.OrdinalIgnoreCase))
						{
							if (displayName != null)
							{
								Theme.ThrowParserException(xmlTextReader, folderName, string.Format("Duplicated attribute '{0}' found.", "DisplayName"), ClientsEventLogConstants.Tuple_ThemeInfoDuplicatedAttribute, new object[]
								{
									"themeinfo.xml",
									folderName,
									"DisplayName",
									xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture),
									xmlTextReader.LinePosition.ToString(CultureInfo.InvariantCulture)
								});
							}
							displayName = xmlTextReader.Value;
							if (string.IsNullOrEmpty(displayName))
							{
								Theme.ThrowParserException(xmlTextReader, folderName, string.Format("Empty attribute '{0}' not allowed.", "DisplayName"), ClientsEventLogConstants.Tuple_ThemeInfoEmptyAttribute, new object[]
								{
									"themeinfo.xml",
									folderName,
									"DisplayName",
									xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture),
									xmlTextReader.LinePosition.ToString(CultureInfo.InvariantCulture)
								});
							}
							if (displayName.Length > 512)
							{
								Theme.ThrowParserException(xmlTextReader, folderName, string.Format("Attribute '{0}' exceedes the maximum limit of {1} characters.", "DisplayName", 512), ClientsEventLogConstants.Tuple_ThemeInfoAttributeExceededMaximumLength, new object[]
								{
									"themeinfo.xml",
									folderName,
									"DisplayName",
									512,
									xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture),
									xmlTextReader.LinePosition.ToString(CultureInfo.InvariantCulture)
								});
							}
							localizedDisplayName = ThemeManager.GetLocalizedThemeName(displayName);
						}
						if (string.Equals("SortOrder", xmlTextReader.Name, StringComparison.OrdinalIgnoreCase))
						{
							try
							{
								sortOrder = int.Parse(xmlTextReader.Value);
							}
							catch
							{
								Theme.ThrowParserException(xmlTextReader, folderName, string.Format("Attribute '{0}' is not a valid integer.", "SortOrder"), ClientsEventLogConstants.Tuple_ThemeInfoErrorParsingXml, new object[]
								{
									"themeinfo.xml",
									folderName,
									"SortOrder",
									xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture),
									xmlTextReader.LinePosition.ToString(CultureInfo.InvariantCulture)
								});
							}
						}
					}
					while (xmlTextReader.MoveToNextAttribute());
					if (displayName == null)
					{
						Theme.ThrowParserException(xmlTextReader, folderName, string.Format("Attribute '{0}' was not found.", "DisplayName"), ClientsEventLogConstants.Tuple_ThemeInfoMissingAttribute, new object[]
						{
							"themeinfo.xml",
							folderName,
							"DisplayName",
							xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture),
							xmlTextReader.LinePosition.ToString(CultureInfo.InvariantCulture)
						});
					}
				}
			}
			catch (XmlException ex)
			{
				Theme.ThrowParserException(xmlTextReader, folderName, string.Format("XML parser error. {0}", ex.Message), ClientsEventLogConstants.Tuple_ThemeInfoErrorParsingXml, new object[]
				{
					"themeinfo.xml",
					folderName,
					xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture),
					xmlTextReader.LinePosition.ToString(CultureInfo.InvariantCulture)
				});
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0007D888 File Offset: 0x0007BA88
		private static void ThrowParserException(XmlTextReader reader, string folderName, string description, ExEventLog.EventTuple tuple, params object[] eventMessageArgs)
		{
			OwaDiagnostics.LogEvent(tuple, string.Empty, eventMessageArgs);
			throw new OwaThemeManagerInitializationException(string.Format(CultureInfo.InvariantCulture, "Invalid theme info file in folder '{0}'. Line  {1} Position {2}.{3}", new object[]
			{
				folderName,
				reader.LineNumber.ToString(CultureInfo.InvariantCulture),
				reader.LinePosition.ToString(CultureInfo.InvariantCulture),
				(description != null) ? (" " + description) : string.Empty
			}), null, null);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0007D90C File Offset: 0x0007BB0C
		private void InitializeForCssSprites()
		{
			this.classNameForCssSpritesTable = new Dictionary<int, string>(ThemeFileList.Count);
			this.shouldUseCssSpritesTable = new Dictionary<int, bool>(ThemeFileList.Count);
			for (int i = 1; i < ThemeFileList.Count; i++)
			{
				this.classNameForCssSpritesTable[i] = this.InternalGetThemeFileClass((ThemeFileId)i);
				this.shouldUseCssSpritesTable[i] = this.InternalShouldUseCssSprites((ThemeFileId)i);
			}
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0007D970 File Offset: 0x0007BB70
		private void Load(string folderPath)
		{
			ExTraceGlobals.ThemesCallTracer.TraceDebug<string>(0L, "Theme.Load. folderPath={0}", folderPath);
			string text = Path.Combine(folderPath, "themeinfo.xml");
			this.folderName = Path.GetFileNameWithoutExtension(folderPath);
			this.folderPath = folderPath;
			string text2 = null;
			Strings.IDs ds = -1018465893;
			int maxValue = int.MaxValue;
			if (File.Exists(text))
			{
				Theme.ParseThemeInfoFile(text, this.folderName, out text2, out ds, out maxValue);
			}
			if (text2 != null)
			{
				this.displayName = text2;
			}
			else
			{
				this.displayName = this.folderName;
			}
			this.localizedDisplayName = ds;
			this.sortOrder = maxValue;
			if (string.Equals(this.folderName, ThemeManager.BaseThemeFolderName, StringComparison.OrdinalIgnoreCase))
			{
				this.isBase = true;
			}
			this.themeFileTable = new Dictionary<int, bool>(ThemeFileList.Count);
			for (int i = 0; i < ThemeFileList.Count; i++)
			{
				this.themeFileTable[i] = false;
			}
			string[] array = null;
			try
			{
				array = Directory.GetFiles(folderPath);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.ThemesTracer.TraceDebug<Exception, string>(0L, "Exception thrown by Directory.GetFiles. {0}. Callstack = {1}", ex, ex.StackTrace);
				throw;
			}
			ExTraceGlobals.ThemesTracer.TraceDebug<int, string>(0L, "Inspecting {0} files in theme folder '{1}'", array.Length, this.folderName);
			for (int j = 0; j < array.Length; j++)
			{
				string fileName = Path.GetFileName(array[j]);
				if (!string.IsNullOrEmpty(fileName))
				{
					int idFromName = ThemeFileList.GetIdFromName(fileName);
					if (idFromName == 0)
					{
						ExTraceGlobals.ThemesTracer.TraceDebug<string>(0L, "Skipping unknown file '{0}'", fileName);
					}
					else
					{
						ExTraceGlobals.ThemesTracer.TraceDebug<string>(0L, "Succesfully added theme file '{0}'", fileName);
						this.themeFileTable[idFromName] = true;
					}
				}
			}
			this.url = ThemeManager.ThemesFolderPath + this.folderName + "/";
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0007DB24 File Offset: 0x0007BD24
		public bool ShouldUseCssSprites(ThemeFileId themeFileId)
		{
			if (this.shouldUseCssSpritesTable.ContainsKey((int)themeFileId))
			{
				return this.shouldUseCssSpritesTable[(int)themeFileId];
			}
			bool flag = this.InternalShouldUseCssSprites(themeFileId);
			this.shouldUseCssSpritesTable[(int)themeFileId] = flag;
			return flag;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0007DB62 File Offset: 0x0007BD62
		private bool InternalShouldUseCssSprites(ThemeFileId themeFileId)
		{
			return ThemeFileList.CanUseCssSprites(themeFileId) && (this.IsFileInTheme(ThemeFileId.CssSpritesCss) || !this.IsFileInTheme(themeFileId));
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0007DB84 File Offset: 0x0007BD84
		public string GetThemeFileClass(ThemeFileId themeFileId)
		{
			if (this.classNameForCssSpritesTable.ContainsKey((int)themeFileId))
			{
				return this.classNameForCssSpritesTable[(int)themeFileId];
			}
			string text = this.InternalGetThemeFileClass(themeFileId);
			this.classNameForCssSpritesTable[(int)themeFileId] = text;
			return text;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0007DBC4 File Offset: 0x0007BDC4
		private string InternalGetThemeFileClass(ThemeFileId themeFileId)
		{
			string classNameFromId = ThemeFileList.GetClassNameFromId((int)themeFileId);
			StringBuilder stringBuilder = new StringBuilder(classNameFromId.Length + 16);
			stringBuilder.Append("csimgbg");
			stringBuilder.Append(" ");
			stringBuilder.Append(classNameFromId);
			return stringBuilder.ToString();
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0007DC0D File Offset: 0x0007BE0D
		internal bool IsFileInTheme(ThemeFileId themeFileId)
		{
			return this.IsFileInTheme((int)themeFileId);
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0007DC18 File Offset: 0x0007BE18
		internal bool IsFileInTheme(int themeFileIndex)
		{
			if (this.themeFileTable.ContainsKey(themeFileIndex))
			{
				return this.themeFileTable[themeFileIndex];
			}
			string nameFromId = ThemeFileList.GetNameFromId(themeFileIndex);
			bool flag = File.Exists(Path.Combine(this.folderPath, nameFromId));
			this.themeFileTable[themeFileIndex] = flag;
			return flag;
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x0007DC67 File Offset: 0x0007BE67
		public bool IsBase
		{
			get
			{
				return this.isBase;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0007DC6F File Offset: 0x0007BE6F
		public string DisplayName
		{
			get
			{
				if (this.localizedDisplayName != -1018465893)
				{
					return LocalizedStrings.GetNonEncoded(this.localizedDisplayName);
				}
				return this.displayName;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x0007DC90 File Offset: 0x0007BE90
		public int SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0007DC98 File Offset: 0x0007BE98
		public string StorageId
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0007DCA0 File Offset: 0x0007BEA0
		public uint Id
		{
			get
			{
				return ThemeManager.GetIdFromStorageId(this.StorageId);
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0007DCAD File Offset: 0x0007BEAD
		public string FolderName
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0007DCB5 File Offset: 0x0007BEB5
		public string Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x04000E16 RID: 3606
		private const string ThemeInfoFileName = "themeinfo.xml";

		// Token: 0x04000E17 RID: 3607
		private const string ThemeDisplayNameAttribute = "DisplayName";

		// Token: 0x04000E18 RID: 3608
		private const string ThemeSortOrderAttribute = "SortOrder";

		// Token: 0x04000E19 RID: 3609
		private const string ThemeRootElement = "Theme";

		// Token: 0x04000E1A RID: 3610
		private const int MaxThemeDisplayNameLength = 512;

		// Token: 0x04000E1B RID: 3611
		private static readonly string[] RequiredFiles = new string[]
		{
			"premium.css",
			"gradienth.png",
			"gradientv.png",
			"csssprites.css",
			"csssprites.png",
			"csssprites2.css",
			"csssprites2.png"
		};

		// Token: 0x04000E1C RID: 3612
		private string url;

		// Token: 0x04000E1D RID: 3613
		private bool isBase;

		// Token: 0x04000E1E RID: 3614
		private string folderName;

		// Token: 0x04000E1F RID: 3615
		private string displayName;

		// Token: 0x04000E20 RID: 3616
		private int sortOrder = int.MaxValue;

		// Token: 0x04000E21 RID: 3617
		private Strings.IDs localizedDisplayName = -1018465893;

		// Token: 0x04000E22 RID: 3618
		private Dictionary<int, bool> themeFileTable;

		// Token: 0x04000E23 RID: 3619
		private Dictionary<int, bool> shouldUseCssSpritesTable;

		// Token: 0x04000E24 RID: 3620
		private Dictionary<int, string> classNameForCssSpritesTable;

		// Token: 0x04000E25 RID: 3621
		private string folderPath;
	}
}
