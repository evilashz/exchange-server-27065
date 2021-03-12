using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000172 RID: 370
	internal class MailboxSchemaOptionsParser
	{
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0005BD66 File Offset: 0x00059F66
		public MIMESupportValue MIMESupport
		{
			get
			{
				return this.mimeSupport;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0005BD6E File Offset: 0x00059F6E
		public bool RightsManagementSupport
		{
			get
			{
				return this.rightsManagementSupport;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0005BD76 File Offset: 0x00059F76
		internal List<BodyPreference> BodyPreferences
		{
			get
			{
				return this.bodyPreferences;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0005BD7E File Offset: 0x00059F7E
		internal List<BodyPartPreference> BodyPartPreferences
		{
			get
			{
				return this.bodyPartPreferences;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0005BD86 File Offset: 0x00059F86
		internal Dictionary<string, bool> SchemaTags
		{
			get
			{
				return this.schemaTags;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0005BD8E File Offset: 0x00059F8E
		internal bool HasBodyPreferences
		{
			get
			{
				return this.BodyPreferences != null && this.BodyPreferences.Count > 0;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0005BDA8 File Offset: 0x00059FA8
		internal bool HasBodyPartPreferences
		{
			get
			{
				return this.BodyPartPreferences != null && this.BodyPartPreferences.Count > 0;
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0005BDC2 File Offset: 0x00059FC2
		public IDictionary BuildOptionsCollection(string deviceType)
		{
			return this.PopulateOptionsCollection(deviceType, new Hashtable(8));
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0005BDE0 File Offset: 0x00059FE0
		public void Parse(XmlNode parentNode)
		{
			bool[] bodyTypeSet = new bool[256];
			bool[] bodyTypeSet2 = new bool[256];
			foreach (object obj in parentNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string namespaceURI;
				if ("Schema" == xmlNode.Name)
				{
					this.ParseSchemaTags(xmlNode);
				}
				else if ((namespaceURI = xmlNode.NamespaceURI) != null)
				{
					string localName3;
					if (!(namespaceURI == "AirSync:"))
					{
						string localName2;
						if (!(namespaceURI == "AirSyncBase:"))
						{
							if (namespaceURI == "RightsManagement:")
							{
								string localName;
								if ((localName = xmlNode.LocalName) != null && localName == "RightsManagementSupport")
								{
									string innerText;
									if ((innerText = xmlNode.InnerText) != null)
									{
										if (innerText == "0")
										{
											this.rightsManagementSupport = false;
											continue;
										}
										if (innerText == "1")
										{
											this.rightsManagementSupport = true;
											continue;
										}
									}
									throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
									{
										ErrorStringForProtocolLogger = "InvalidValueRightsManagementSupport"
									};
								}
							}
						}
						else if ((localName2 = xmlNode.LocalName) != null)
						{
							if (!(localName2 == "BodyPreference"))
							{
								if (localName2 == "BodyPartPreference")
								{
									BodyPartPreference bodyPartPreference = MailboxSchemaOptionsParser.ParsePreference<BodyPartPreference>(xmlNode, bodyTypeSet2, () => new BodyPartPreference());
									if (bodyPartPreference != null)
									{
										this.bodyPartPreferences.Add(bodyPartPreference);
									}
								}
							}
							else
							{
								BodyPreference bodyPreference = MailboxSchemaOptionsParser.ParsePreference<BodyPreference>(xmlNode, bodyTypeSet, () => new BodyPreference());
								if (bodyPreference != null)
								{
									this.bodyPreferences.Add(bodyPreference);
								}
							}
						}
					}
					else if ((localName3 = xmlNode.LocalName) != null)
					{
						if (!(localName3 == "Truncation"))
						{
							if (!(localName3 == "RTFTruncation"))
							{
								if (!(localName3 == "MIMETruncation"))
								{
									if (localName3 == "MIMESupport")
									{
										this.ParseMIMESupport(xmlNode.InnerText);
									}
								}
								else
								{
									this.ParseMIMETruncation(xmlNode.InnerText);
								}
							}
							else
							{
								this.ParseRtfTruncationSetting(xmlNode.InnerText);
							}
						}
						else
						{
							this.ParseTextTruncationSetting(xmlNode.InnerText);
						}
					}
				}
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0005C064 File Offset: 0x0005A264
		public IDictionary PopulateOptionsCollection(string deviceType, IDictionary options)
		{
			if ("PocketPC" == deviceType)
			{
				options["ClientSupportsRtf"] = true;
				if (this.truncateRtfBody)
				{
					options["MaxRtfBodySize"] = this.maxRtfBodySize;
				}
			}
			if (this.truncateTextBody)
			{
				options["MaxTextBodySize"] = this.maxTextBodySize;
			}
			options["BodyPreference"] = this.bodyPreferences;
			options["BodyPartPreference"] = this.bodyPartPreferences;
			options["MIMESupport"] = this.mimeSupport;
			if (this.mimeTruncation != null)
			{
				options["MIMETruncation"] = this.mimeTruncation.Value;
			}
			else
			{
				foreach (BodyPreference bodyPreference in this.bodyPreferences)
				{
					if (bodyPreference.Type == BodyType.Mime)
					{
						options["MIMETruncation"] = (int)bodyPreference.TruncationSize;
					}
				}
			}
			options["RightsManagementSupport"] = this.rightsManagementSupport;
			return options;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0005C1A4 File Offset: 0x0005A3A4
		private static T ParsePreference<T>(XmlNode prefNode, bool[] bodyTypeSet, Func<T> creator) where T : BodyPreference
		{
			T result = creator();
			foreach (object obj in prefNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string localName;
				if ((localName = xmlNode.LocalName) != null)
				{
					if (!(localName == "Type"))
					{
						if (!(localName == "TruncationSize"))
						{
							if (!(localName == "AllOrNone"))
							{
								if (localName == "Restriction")
								{
									continue;
								}
								if (localName == "Preview")
								{
									int preview;
									if (!int.TryParse(xmlNode.InnerText, out preview))
									{
										throw new ConversionException(4, "Invalid Preview");
									}
									result.Preview = preview;
									continue;
								}
							}
							else
							{
								if (xmlNode.InnerText.Equals("1"))
								{
									result.AllOrNone = true;
									continue;
								}
								continue;
							}
						}
						else
						{
							long truncationSize;
							if (!long.TryParse(xmlNode.InnerText, out truncationSize))
							{
								throw new ConversionException(4, "Invalid Truncation Size");
							}
							result.TruncationSize = truncationSize;
							continue;
						}
					}
					else
					{
						byte b;
						if (!byte.TryParse(xmlNode.InnerText, out b))
						{
							throw new ConversionException(4, "Invalid Body Type");
						}
						if (bodyTypeSet[(int)b])
						{
							throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
							{
								ErrorStringForProtocolLogger = "InvalidNode(Type)InMbxSchemaOptionsParser"
							};
						}
						bodyTypeSet[(int)b] = true;
						switch (b)
						{
						case 1:
							result.Type = BodyType.PlainText;
							continue;
						case 2:
							result.Type = BodyType.Html;
							continue;
						case 3:
							result.Type = BodyType.Rtf;
							continue;
						case 4:
							result.Type = BodyType.Mime;
							continue;
						default:
							return default(T);
						}
					}
				}
				throw new ConversionException(4, "Invalid Element");
			}
			return result;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0005C3BC File Offset: 0x0005A5BC
		private void ParseMIMESupport(string nodeText)
		{
			int num = -1;
			if (!int.TryParse(nodeText, out num))
			{
				throw new ConversionException(4, "MIMESupport is in bad format: " + nodeText);
			}
			switch (num)
			{
			case 0:
				this.mimeSupport = MIMESupportValue.NeverSendMimeData;
				return;
			case 1:
				this.mimeSupport = MIMESupportValue.SendMimeDataForSmimeMessagesOnly;
				return;
			case 2:
				this.mimeSupport = MIMESupportValue.SendMimeDataForAllMessages;
				return;
			default:
				throw new ConversionException(4, "MIMESupport is in bad format: " + nodeText);
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0005C428 File Offset: 0x0005A628
		private void ParseMIMETruncation(string nodeText)
		{
			int num = -1;
			if (!int.TryParse(nodeText, out num))
			{
				throw new ConversionException(4, "MIMETruncation is in bad format: " + nodeText);
			}
			switch (num)
			{
			case 0:
				this.mimeTruncation = new int?(0);
				return;
			case 1:
				this.mimeTruncation = new int?(4096);
				return;
			case 2:
				this.mimeTruncation = new int?(5120);
				return;
			case 3:
				this.mimeTruncation = new int?(7168);
				return;
			case 4:
				this.mimeTruncation = new int?(10240);
				return;
			case 5:
				this.mimeTruncation = new int?(20480);
				return;
			case 6:
				this.mimeTruncation = new int?(51200);
				return;
			case 7:
				this.mimeTruncation = new int?(102400);
				return;
			case 8:
				this.mimeTruncation = null;
				return;
			default:
				throw new ConversionException(4, "MIMETruncation is in bad format: " + nodeText);
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0005C528 File Offset: 0x0005A728
		private void ParseRtfTruncationSetting(string rtfTruncationSetting)
		{
			this.truncateRtfBody = true;
			int num = 0;
			if (!int.TryParse(rtfTruncationSetting, out num))
			{
				throw new ConversionException(4, "RTFTruncation bad format");
			}
			switch (num)
			{
			case 0:
				this.maxRtfBodySize = 0;
				return;
			case 1:
				this.maxRtfBodySize = 512;
				return;
			case 2:
				this.maxRtfBodySize = 1024;
				return;
			case 3:
				this.maxRtfBodySize = 2048;
				return;
			case 4:
				this.maxRtfBodySize = 5120;
				return;
			case 5:
				this.maxRtfBodySize = 10240;
				return;
			case 6:
				this.maxRtfBodySize = 20480;
				return;
			case 7:
				this.maxRtfBodySize = 51200;
				return;
			case 8:
				this.maxRtfBodySize = 102400;
				return;
			case 9:
				this.truncateTextBody = false;
				return;
			default:
				throw new ConversionException(4, "RTFTruncation, bad value: " + num.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0005C614 File Offset: 0x0005A814
		private void ParseSchemaTags(XmlNode schemaNode)
		{
			foreach (object obj in schemaNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (this.schemaTags == null)
				{
					this.schemaTags = new Dictionary<string, bool>(schemaNode.ChildNodes.Count);
				}
				this.schemaTags.Add(xmlNode.NamespaceURI + xmlNode.LocalName, true);
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0005C6A4 File Offset: 0x0005A8A4
		private void ParseTextTruncationSetting(string textTruncationSetting)
		{
			this.truncateTextBody = true;
			int num = 0;
			if (!int.TryParse(textTruncationSetting, out num))
			{
				throw new ConversionException(4, "Truncation bad format");
			}
			switch (num)
			{
			case 0:
				this.maxTextBodySize = 0;
				return;
			case 1:
				this.maxTextBodySize = 512;
				return;
			case 2:
				this.maxTextBodySize = 1024;
				return;
			case 3:
				this.maxTextBodySize = 2048;
				return;
			case 4:
				this.maxTextBodySize = 5120;
				return;
			case 5:
				this.maxTextBodySize = 10240;
				return;
			case 6:
				this.maxTextBodySize = 20480;
				return;
			case 7:
				this.maxTextBodySize = 51200;
				return;
			case 8:
				this.maxTextBodySize = 102400;
				return;
			case 9:
				this.truncateTextBody = false;
				return;
			default:
				throw new ConversionException(4, "Truncation, bad value: " + num.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x04000A89 RID: 2697
		private List<BodyPreference> bodyPreferences = new List<BodyPreference>(5);

		// Token: 0x04000A8A RID: 2698
		private List<BodyPartPreference> bodyPartPreferences = new List<BodyPartPreference>(1);

		// Token: 0x04000A8B RID: 2699
		private Dictionary<string, bool> schemaTags;

		// Token: 0x04000A8C RID: 2700
		private int maxRtfBodySize;

		// Token: 0x04000A8D RID: 2701
		private int maxTextBodySize;

		// Token: 0x04000A8E RID: 2702
		private MIMESupportValue mimeSupport;

		// Token: 0x04000A8F RID: 2703
		private int? mimeTruncation;

		// Token: 0x04000A90 RID: 2704
		private bool truncateRtfBody;

		// Token: 0x04000A91 RID: 2705
		private bool truncateTextBody;

		// Token: 0x04000A92 RID: 2706
		private bool rightsManagementSupport;

		// Token: 0x02000173 RID: 371
		private enum AirSyncV25RtfTruncationSetting
		{
			// Token: 0x04000A96 RID: 2710
			Invalid = -1,
			// Token: 0x04000A97 RID: 2711
			TruncAlways,
			// Token: 0x04000A98 RID: 2712
			TruncHalfk,
			// Token: 0x04000A99 RID: 2713
			Trunc1k,
			// Token: 0x04000A9A RID: 2714
			Trunc2k,
			// Token: 0x04000A9B RID: 2715
			Trunc5k,
			// Token: 0x04000A9C RID: 2716
			Trunc10k,
			// Token: 0x04000A9D RID: 2717
			Trunc20k,
			// Token: 0x04000A9E RID: 2718
			Trunc50k,
			// Token: 0x04000A9F RID: 2719
			Trunc100k,
			// Token: 0x04000AA0 RID: 2720
			TruncNone
		}

		// Token: 0x02000174 RID: 372
		private enum AirSyncV25TextTruncationSetting
		{
			// Token: 0x04000AA2 RID: 2722
			Invalid = -1,
			// Token: 0x04000AA3 RID: 2723
			TruncAlways,
			// Token: 0x04000AA4 RID: 2724
			TruncHalfk,
			// Token: 0x04000AA5 RID: 2725
			Trunc1k,
			// Token: 0x04000AA6 RID: 2726
			Trunc2k,
			// Token: 0x04000AA7 RID: 2727
			Trunc5k,
			// Token: 0x04000AA8 RID: 2728
			Trunc10k,
			// Token: 0x04000AA9 RID: 2729
			Trunc20k,
			// Token: 0x04000AAA RID: 2730
			Trunc50k,
			// Token: 0x04000AAB RID: 2731
			Trunc100k,
			// Token: 0x04000AAC RID: 2732
			TruncNone
		}
	}
}
