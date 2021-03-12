using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000B6 RID: 182
	[DataContract]
	public class Theme : IComparable<Theme>
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x00015CCC File Offset: 0x00013ECC
		private Theme(string folderPath)
		{
			this.Load(folderPath);
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00015CE6 File Offset: 0x00013EE6
		public bool IsBase
		{
			get
			{
				return this.isBase;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00015CEE File Offset: 0x00013EEE
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x00015CF6 File Offset: 0x00013EF6
		[DataMember]
		public string FolderName
		{
			get
			{
				return this.folderName;
			}
			set
			{
				this.folderName = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00015CFF File Offset: 0x00013EFF
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x00015D07 File Offset: 0x00013F07
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00015D10 File Offset: 0x00013F10
		public int SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00015D18 File Offset: 0x00013F18
		public string StorageId
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00015D20 File Offset: 0x00013F20
		public static Theme Create(string folderPath)
		{
			if (folderPath == null)
			{
				throw new ArgumentNullException("folderPath");
			}
			return new Theme(folderPath);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00015D38 File Offset: 0x00013F38
		public int CompareTo(Theme otherTheme)
		{
			if (otherTheme == null)
			{
				return 1;
			}
			if (this.SortOrder == otherTheme.SortOrder)
			{
				return this.StorageId.CompareTo(otherTheme.StorageId);
			}
			return this.SortOrder.CompareTo(otherTheme.SortOrder);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00015D80 File Offset: 0x00013F80
		private static void ParseThemeInfoFile(string themeInfoFilePath, string folderName, out string displayName, out int sortOrder)
		{
			XmlTextReader xmlTextReader = null;
			displayName = null;
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
								Theme.ThrowParserException(xmlTextReader, folderName, string.Format("Attribute '{0}' exceeds the maximum limit of {1} characters.", "DisplayName", 512), ClientsEventLogConstants.Tuple_ThemeInfoAttributeExceededMaximumLength, new object[]
								{
									"themeinfo.xml",
									folderName,
									"DisplayName",
									512,
									xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture),
									xmlTextReader.LinePosition.ToString(CultureInfo.InvariantCulture)
								});
							}
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

		// Token: 0x0600072A RID: 1834 RVA: 0x0001616C File Offset: 0x0001436C
		private static void ThrowParserException(XmlTextReader reader, string folderName, string description, ExEventLog.EventTuple tuple, params object[] eventMessageArgs)
		{
			OwaDiagnostics.LogEvent(tuple, string.Empty, eventMessageArgs);
			throw new OwaThemeManagerInitializationException(string.Format(CultureInfo.InvariantCulture, "Invalid theme info file in folder '{0}'. Line {1} Position {2}.{3}", new object[]
			{
				folderName,
				reader.LineNumber.ToString(CultureInfo.InvariantCulture),
				reader.LinePosition.ToString(CultureInfo.InvariantCulture),
				(description != null) ? (" " + description) : string.Empty
			}), null, null);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000161F0 File Offset: 0x000143F0
		private void Load(string folderPath)
		{
			ExTraceGlobals.ThemesCallTracer.TraceDebug<string>(0L, "Theme.Load. folderPath={0}", folderPath);
			this.folderName = Path.GetFileNameWithoutExtension(folderPath);
			this.displayName = this.folderName;
			string text = Path.Combine(folderPath, "themeinfo.xml");
			string text2 = null;
			int maxValue = int.MaxValue;
			if (File.Exists(text))
			{
				Theme.ParseThemeInfoFile(text, this.folderName, out text2, out maxValue);
			}
			if (text2 != null)
			{
				this.displayName = text2;
			}
			else
			{
				this.displayName = this.folderName;
			}
			this.sortOrder = maxValue;
			if (string.Equals(this.folderName, ThemeManager.BaseThemeFolderName, StringComparison.OrdinalIgnoreCase))
			{
				this.isBase = true;
			}
		}

		// Token: 0x040003EB RID: 1003
		private const string ThemeInfoFileName = "themeinfo.xml";

		// Token: 0x040003EC RID: 1004
		private const string ThemeDisplayNameAttribute = "DisplayName";

		// Token: 0x040003ED RID: 1005
		private const string ThemeSortOrderAttribute = "SortOrder";

		// Token: 0x040003EE RID: 1006
		private const string ThemeRootElement = "Theme";

		// Token: 0x040003EF RID: 1007
		private const int MaxThemeDisplayNameLength = 512;

		// Token: 0x040003F0 RID: 1008
		private bool isBase;

		// Token: 0x040003F1 RID: 1009
		private string folderName;

		// Token: 0x040003F2 RID: 1010
		private string displayName;

		// Token: 0x040003F3 RID: 1011
		private int sortOrder = int.MaxValue;
	}
}
