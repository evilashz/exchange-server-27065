using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005F6 RID: 1526
	internal class XmlSerializationReaderAutoAttendantSettings : XmlSerializationReader
	{
		// Token: 0x060048A7 RID: 18599 RVA: 0x0010C59C File Offset: 0x0010A79C
		public object Read5_AutoAttendantSettings()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_AutoAttendantSettings || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read4_AutoAttendantSettings(true, true);
			}
			else
			{
				base.UnknownNode(null, ":AutoAttendantSettings");
			}
			return result;
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x0010C60C File Offset: 0x0010A80C
		private AutoAttendantSettings Read4_AutoAttendantSettings(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id1_AutoAttendantSettings || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			AutoAttendantSettings autoAttendantSettings = new AutoAttendantSettings();
			bool[] array = new bool[11];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(autoAttendantSettings);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return autoAttendantSettings;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id3_TimeZoneKeyName && base.Reader.NamespaceURI == this.id2_Item)
					{
						autoAttendantSettings.TimeZoneKeyName = base.Reader.ReadElementString();
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id4_IsBusinessHourSetting && base.Reader.NamespaceURI == this.id2_Item)
					{
						autoAttendantSettings.IsBusinessHourSetting = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id5_WelcomeGreetingFilename && base.Reader.NamespaceURI == this.id2_Item)
					{
						autoAttendantSettings.WelcomeGreetingFilename = base.Reader.ReadElementString();
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id6_WelcomeGreetingEnabled && base.Reader.NamespaceURI == this.id2_Item)
					{
						autoAttendantSettings.WelcomeGreetingEnabled = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id7_GlobalInfoAnnouncementFilename && base.Reader.NamespaceURI == this.id2_Item)
					{
						autoAttendantSettings.GlobalInfoAnnouncementFilename = base.Reader.ReadElementString();
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id8_MainMenuCustomPromptEnabled && base.Reader.NamespaceURI == this.id2_Item)
					{
						autoAttendantSettings.MainMenuCustomPromptEnabled = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id9_MainMenuCustomPromptFilename && base.Reader.NamespaceURI == this.id2_Item)
					{
						autoAttendantSettings.MainMenuCustomPromptFilename = base.Reader.ReadElementString();
						array[6] = true;
					}
					else if (!array[7] && base.Reader.LocalName == this.id10_TransferToOperatorEnabled && base.Reader.NamespaceURI == this.id2_Item)
					{
						autoAttendantSettings.TransferToOperatorEnabled = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[7] = true;
					}
					else if (!array[8] && base.Reader.LocalName == this.id11_GlobalOperatorExtension && base.Reader.NamespaceURI == this.id2_Item)
					{
						autoAttendantSettings.GlobalOperatorExtension = base.Reader.ReadElementString();
						array[8] = true;
					}
					else if (!array[9] && base.Reader.LocalName == this.id12_KeyMappingEnabled && base.Reader.NamespaceURI == this.id2_Item)
					{
						autoAttendantSettings.KeyMappingEnabled = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[9] = true;
					}
					else if (base.Reader.LocalName == this.id13_KeyMapping && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (!base.ReadNull())
						{
							CustomMenuKeyMapping[] array2 = null;
							int num2 = 0;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num3 = 0;
								int readerCount2 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id14_CustomMenuKeyMapping && base.Reader.NamespaceURI == this.id2_Item)
										{
											array2 = (CustomMenuKeyMapping[])base.EnsureArrayIndex(array2, num2, typeof(CustomMenuKeyMapping));
											array2[num2++] = this.Read3_CustomMenuKeyMapping(true, true);
										}
										else
										{
											base.UnknownNode(null, ":CustomMenuKeyMapping");
										}
									}
									else
									{
										base.UnknownNode(null, ":CustomMenuKeyMapping");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num3, ref readerCount2);
								}
								base.ReadEndElement();
							}
							autoAttendantSettings.KeyMapping = (CustomMenuKeyMapping[])base.ShrinkArray(array2, num2, typeof(CustomMenuKeyMapping), false);
						}
					}
					else
					{
						base.UnknownNode(autoAttendantSettings, ":TimeZoneKeyName, :IsBusinessHourSetting, :WelcomeGreetingFilename, :WelcomeGreetingEnabled, :GlobalInfoAnnouncementFilename, :MainMenuCustomPromptEnabled, :MainMenuCustomPromptFilename, :TransferToOperatorEnabled, :GlobalOperatorExtension, :KeyMappingEnabled, :KeyMapping");
					}
				}
				else
				{
					base.UnknownNode(autoAttendantSettings, ":TimeZoneKeyName, :IsBusinessHourSetting, :WelcomeGreetingFilename, :WelcomeGreetingEnabled, :GlobalInfoAnnouncementFilename, :MainMenuCustomPromptEnabled, :MainMenuCustomPromptFilename, :TransferToOperatorEnabled, :GlobalOperatorExtension, :KeyMappingEnabled, :KeyMapping");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return autoAttendantSettings;
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x0010CB7C File Offset: 0x0010AD7C
		private CustomMenuKeyMapping Read3_CustomMenuKeyMapping(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id14_CustomMenuKeyMapping || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			CustomMenuKeyMapping customMenuKeyMapping = new CustomMenuKeyMapping();
			bool[] array = new bool[12];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(customMenuKeyMapping);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return customMenuKeyMapping;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id15_MappedKey && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.MappedKey = this.Read1_CustomMenuKey(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id16_Description && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.Description = base.Reader.ReadElementString();
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id17_Extension && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.Extension = base.Reader.ReadElementString();
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id18_AutoAttendantName && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.AutoAttendantName = base.Reader.ReadElementString();
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id19_LeaveVoicemailFor && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.LeaveVoicemailFor = base.Reader.ReadElementString();
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id20_Item && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.LegacyDNToUseForLeaveVoicemailFor = base.Reader.ReadElementString();
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id21_TransferToMailbox && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.TransferToMailbox = base.Reader.ReadElementString();
						array[6] = true;
					}
					else if (!array[7] && base.Reader.LocalName == this.id22_Item && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.LegacyDNToUseForTransferToMailbox = base.Reader.ReadElementString();
						array[7] = true;
					}
					else if (!array[8] && base.Reader.LocalName == this.id23_PromptFileName && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.PromptFileName = base.Reader.ReadElementString();
						array[8] = true;
					}
					else if (!array[9] && base.Reader.LocalName == this.id24_AsrPhrases && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.AsrPhrases = base.Reader.ReadElementString();
						array[9] = true;
					}
					else if (!array[10] && base.Reader.LocalName == this.id25_AnnounceBusinessLocation && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.AnnounceBusinessLocation = base.Reader.ReadElementString();
						array[10] = true;
					}
					else if (!array[11] && base.Reader.LocalName == this.id26_AnnounceBusinessHours && base.Reader.NamespaceURI == this.id2_Item)
					{
						customMenuKeyMapping.AnnounceBusinessHours = base.Reader.ReadElementString();
						array[11] = true;
					}
					else
					{
						base.UnknownNode(customMenuKeyMapping, ":MappedKey, :Description, :Extension, :AutoAttendantName, :LeaveVoicemailFor, :LegacyDNToUseForLeaveVoicemailFor, :TransferToMailbox, :LegacyDNToUseForTransferToMailbox, :PromptFileName, :AsrPhrases, :AnnounceBusinessLocation, :AnnounceBusinessHours");
					}
				}
				else
				{
					base.UnknownNode(customMenuKeyMapping, ":MappedKey, :Description, :Extension, :AutoAttendantName, :LeaveVoicemailFor, :LegacyDNToUseForLeaveVoicemailFor, :TransferToMailbox, :LegacyDNToUseForTransferToMailbox, :PromptFileName, :AsrPhrases, :AnnounceBusinessLocation, :AnnounceBusinessHours");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return customMenuKeyMapping;
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x0010D004 File Offset: 0x0010B204
		private CustomMenuKey Read1_CustomMenuKey(string s)
		{
			switch (s)
			{
			case "InvalidKey":
				return CustomMenuKey.InvalidKey;
			case "One":
				return CustomMenuKey.One;
			case "Two":
				return CustomMenuKey.Two;
			case "Three":
				return CustomMenuKey.Three;
			case "Four":
				return CustomMenuKey.Four;
			case "Five":
				return CustomMenuKey.Five;
			case "Six":
				return CustomMenuKey.Six;
			case "Seven":
				return CustomMenuKey.Seven;
			case "Eight":
				return CustomMenuKey.Eight;
			case "Nine":
				return CustomMenuKey.Nine;
			case "NotSpecified":
				return CustomMenuKey.NotSpecified;
			case "Timeout":
				return CustomMenuKey.Timeout;
			}
			throw base.CreateUnknownConstantException(s, typeof(CustomMenuKey));
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x0010D13B File Offset: 0x0010B33B
		protected override void InitCallbacks()
		{
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x0010D140 File Offset: 0x0010B340
		protected override void InitIDs()
		{
			this.id26_AnnounceBusinessHours = base.Reader.NameTable.Add("AnnounceBusinessHours");
			this.id23_PromptFileName = base.Reader.NameTable.Add("PromptFileName");
			this.id19_LeaveVoicemailFor = base.Reader.NameTable.Add("LeaveVoicemailFor");
			this.id1_AutoAttendantSettings = base.Reader.NameTable.Add("AutoAttendantSettings");
			this.id22_Item = base.Reader.NameTable.Add("LegacyDNToUseForTransferToMailbox");
			this.id21_TransferToMailbox = base.Reader.NameTable.Add("TransferToMailbox");
			this.id15_MappedKey = base.Reader.NameTable.Add("MappedKey");
			this.id13_KeyMapping = base.Reader.NameTable.Add("KeyMapping");
			this.id18_AutoAttendantName = base.Reader.NameTable.Add("AutoAttendantName");
			this.id11_GlobalOperatorExtension = base.Reader.NameTable.Add("GlobalOperatorExtension");
			this.id17_Extension = base.Reader.NameTable.Add("Extension");
			this.id7_GlobalInfoAnnouncementFilename = base.Reader.NameTable.Add("GlobalInfoAnnouncementFilename");
			this.id9_MainMenuCustomPromptFilename = base.Reader.NameTable.Add("MainMenuCustomPromptFilename");
			this.id2_Item = base.Reader.NameTable.Add(string.Empty);
			this.id6_WelcomeGreetingEnabled = base.Reader.NameTable.Add("WelcomeGreetingEnabled");
			this.id4_IsBusinessHourSetting = base.Reader.NameTable.Add("IsBusinessHourSetting");
			this.id5_WelcomeGreetingFilename = base.Reader.NameTable.Add("WelcomeGreetingFilename");
			this.id8_MainMenuCustomPromptEnabled = base.Reader.NameTable.Add("MainMenuCustomPromptEnabled");
			this.id20_Item = base.Reader.NameTable.Add("LegacyDNToUseForLeaveVoicemailFor");
			this.id12_KeyMappingEnabled = base.Reader.NameTable.Add("KeyMappingEnabled");
			this.id3_TimeZoneKeyName = base.Reader.NameTable.Add("TimeZoneKeyName");
			this.id16_Description = base.Reader.NameTable.Add("Description");
			this.id24_AsrPhrases = base.Reader.NameTable.Add("AsrPhrases");
			this.id10_TransferToOperatorEnabled = base.Reader.NameTable.Add("TransferToOperatorEnabled");
			this.id25_AnnounceBusinessLocation = base.Reader.NameTable.Add("AnnounceBusinessLocation");
			this.id14_CustomMenuKeyMapping = base.Reader.NameTable.Add("CustomMenuKeyMapping");
		}

		// Token: 0x0400320E RID: 12814
		private string id26_AnnounceBusinessHours;

		// Token: 0x0400320F RID: 12815
		private string id23_PromptFileName;

		// Token: 0x04003210 RID: 12816
		private string id19_LeaveVoicemailFor;

		// Token: 0x04003211 RID: 12817
		private string id1_AutoAttendantSettings;

		// Token: 0x04003212 RID: 12818
		private string id22_Item;

		// Token: 0x04003213 RID: 12819
		private string id21_TransferToMailbox;

		// Token: 0x04003214 RID: 12820
		private string id15_MappedKey;

		// Token: 0x04003215 RID: 12821
		private string id13_KeyMapping;

		// Token: 0x04003216 RID: 12822
		private string id18_AutoAttendantName;

		// Token: 0x04003217 RID: 12823
		private string id11_GlobalOperatorExtension;

		// Token: 0x04003218 RID: 12824
		private string id17_Extension;

		// Token: 0x04003219 RID: 12825
		private string id7_GlobalInfoAnnouncementFilename;

		// Token: 0x0400321A RID: 12826
		private string id9_MainMenuCustomPromptFilename;

		// Token: 0x0400321B RID: 12827
		private string id2_Item;

		// Token: 0x0400321C RID: 12828
		private string id6_WelcomeGreetingEnabled;

		// Token: 0x0400321D RID: 12829
		private string id4_IsBusinessHourSetting;

		// Token: 0x0400321E RID: 12830
		private string id5_WelcomeGreetingFilename;

		// Token: 0x0400321F RID: 12831
		private string id8_MainMenuCustomPromptEnabled;

		// Token: 0x04003220 RID: 12832
		private string id20_Item;

		// Token: 0x04003221 RID: 12833
		private string id12_KeyMappingEnabled;

		// Token: 0x04003222 RID: 12834
		private string id3_TimeZoneKeyName;

		// Token: 0x04003223 RID: 12835
		private string id16_Description;

		// Token: 0x04003224 RID: 12836
		private string id24_AsrPhrases;

		// Token: 0x04003225 RID: 12837
		private string id10_TransferToOperatorEnabled;

		// Token: 0x04003226 RID: 12838
		private string id25_AnnounceBusinessLocation;

		// Token: 0x04003227 RID: 12839
		private string id14_CustomMenuKeyMapping;
	}
}
