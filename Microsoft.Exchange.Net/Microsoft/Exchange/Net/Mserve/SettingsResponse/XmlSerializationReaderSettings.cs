using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008FD RID: 2301
	internal class XmlSerializationReaderSettings : XmlSerializationReader
	{
		// Token: 0x060031B4 RID: 12724 RVA: 0x000754A8 File Offset: 0x000736A8
		public object Read43_Settings()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_Settings || base.Reader.NamespaceURI != this.id2_HMSETTINGS)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read42_Settings(false, true);
			}
			else
			{
				base.UnknownNode(null, "HMSETTINGS::Settings");
			}
			return result;
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x00075518 File Offset: 0x00073718
		private Settings Read42_Settings(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			Settings settings = new Settings();
			bool[] array = new bool[5];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(settings);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return settings;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id4_Status && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settings.Status = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id5_Fault && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settings.Fault = this.Read2_SettingsFault(false, true);
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id6_AuthPolicy && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settings.AuthPolicy = this.Read3_SettingsAuthPolicy(false, true);
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id7_ServiceSettings && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settings.ServiceSettings = this.Read10_SettingsServiceSettings(false, true);
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id8_AccountSettings && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settings.AccountSettings = this.Read41_SettingsAccountSettings(false, true);
						array[4] = true;
					}
					else
					{
						base.UnknownNode(settings, "HMSETTINGS::Status, HMSETTINGS::Fault, HMSETTINGS::AuthPolicy, HMSETTINGS::ServiceSettings, HMSETTINGS::AccountSettings");
					}
				}
				else
				{
					base.UnknownNode(settings, "HMSETTINGS::Status, HMSETTINGS::Fault, HMSETTINGS::AuthPolicy, HMSETTINGS::ServiceSettings, HMSETTINGS::AccountSettings");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settings;
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000757AC File Offset: 0x000739AC
		private SettingsAccountSettings Read41_SettingsAccountSettings(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SettingsAccountSettings settingsAccountSettings = new SettingsAccountSettings();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(settingsAccountSettings);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return settingsAccountSettings;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id4_Status && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAccountSettings.StatusSpecified = true;
						settingsAccountSettings.Status = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id9_Get && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAccountSettings.Item = this.Read33_SettingsAccountSettingsGet(false, true);
						array[1] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id10_Set && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAccountSettings.Item = this.Read40_SettingsAccountSettingsSet(false, true);
						array[1] = true;
					}
					else
					{
						base.UnknownNode(settingsAccountSettings, "HMSETTINGS::Status, HMSETTINGS::Get, HMSETTINGS::Set");
					}
				}
				else
				{
					base.UnknownNode(settingsAccountSettings, "HMSETTINGS::Status, HMSETTINGS::Get, HMSETTINGS::Set");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settingsAccountSettings;
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000759C0 File Offset: 0x00073BC0
		private SettingsAccountSettingsSet Read40_SettingsAccountSettingsSet(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SettingsAccountSettingsSet settingsAccountSettingsSet = new SettingsAccountSettingsSet();
			bool[] array = new bool[4];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(settingsAccountSettingsSet);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return settingsAccountSettingsSet;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id11_Filters && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAccountSettingsSet.Filters = this.Read35_SettingsCategoryResponseType(false, true);
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id12_Lists && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAccountSettingsSet.Lists = this.Read39_ListsSetResponseType(false, true);
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id13_Options && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAccountSettingsSet.Options = this.Read35_SettingsCategoryResponseType(false, true);
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id14_UserSignature && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAccountSettingsSet.UserSignature = this.Read35_SettingsCategoryResponseType(false, true);
						array[3] = true;
					}
					else
					{
						base.UnknownNode(settingsAccountSettingsSet, "HMSETTINGS::Filters, HMSETTINGS::Lists, HMSETTINGS::Options, HMSETTINGS::UserSignature");
					}
				}
				else
				{
					base.UnknownNode(settingsAccountSettingsSet, "HMSETTINGS::Filters, HMSETTINGS::Lists, HMSETTINGS::Options, HMSETTINGS::UserSignature");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settingsAccountSettingsSet;
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x00075C08 File Offset: 0x00073E08
		private SettingsCategoryResponseType Read35_SettingsCategoryResponseType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id15_SettingsCategoryResponseType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SettingsCategoryResponseType settingsCategoryResponseType = new SettingsCategoryResponseType();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(settingsCategoryResponseType);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return settingsCategoryResponseType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id4_Status && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsCategoryResponseType.Status = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id5_Fault && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsCategoryResponseType.Fault = this.Read34_Fault(false, true);
						array[1] = true;
					}
					else
					{
						base.UnknownNode(settingsCategoryResponseType, "HMSETTINGS::Status, HMSETTINGS::Fault");
					}
				}
				else
				{
					base.UnknownNode(settingsCategoryResponseType, "HMSETTINGS::Status, HMSETTINGS::Fault");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settingsCategoryResponseType;
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x00075DD4 File Offset: 0x00073FD4
		private Fault Read34_Fault(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			Fault fault = new Fault();
			bool[] array = new bool[3];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(fault);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return fault;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id16_Faultcode && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						fault.Faultcode = base.Reader.ReadElementString();
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id17_Faultstring && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						fault.Faultstring = base.Reader.ReadElementString();
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id18_Detail && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						fault.Detail = base.Reader.ReadElementString();
						array[2] = true;
					}
					else
					{
						base.UnknownNode(fault, "HMSETTINGS::Faultcode, HMSETTINGS::Faultstring, HMSETTINGS::Detail");
					}
				}
				else
				{
					base.UnknownNode(fault, "HMSETTINGS::Faultcode, HMSETTINGS::Faultstring, HMSETTINGS::Detail");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return fault;
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x00075FE4 File Offset: 0x000741E4
		private ListsSetResponseType Read39_ListsSetResponseType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id19_ListsSetResponseType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ListsSetResponseType listsSetResponseType = new ListsSetResponseType();
			ListsSetResponseTypeList[] array = null;
			int num = 0;
			bool[] array2 = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(listsSetResponseType);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				listsSetResponseType.List = (ListsSetResponseTypeList[])base.ShrinkArray(array, num, typeof(ListsSetResponseTypeList), true);
				return listsSetResponseType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num2 = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array2[0] && base.Reader.LocalName == this.id4_Status && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						listsSetResponseType.Status = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array2[0] = true;
					}
					else if (base.Reader.LocalName == this.id20_List && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						array = (ListsSetResponseTypeList[])base.EnsureArrayIndex(array, num, typeof(ListsSetResponseTypeList));
						array[num++] = this.Read38_ListsSetResponseTypeList(false, true);
					}
					else
					{
						base.UnknownNode(listsSetResponseType, "HMSETTINGS::Status, HMSETTINGS::List");
					}
				}
				else
				{
					base.UnknownNode(listsSetResponseType, "HMSETTINGS::Status, HMSETTINGS::List");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num2, ref readerCount);
			}
			listsSetResponseType.List = (ListsSetResponseTypeList[])base.ShrinkArray(array, num, typeof(ListsSetResponseTypeList), true);
			base.ReadEndElement();
			return listsSetResponseType;
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x00076208 File Offset: 0x00074408
		private ListsSetResponseTypeList Read38_ListsSetResponseTypeList(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ListsSetResponseTypeList listsSetResponseTypeList = new ListsSetResponseTypeList();
			StatusType[] array = null;
			int num = 0;
			ItemsChoiceType[] array2 = null;
			int num2 = 0;
			bool[] array3 = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array3[1] && base.Reader.LocalName == this.id21_name && base.Reader.NamespaceURI == this.id3_Item)
				{
					listsSetResponseTypeList.name = base.Reader.Value;
					array3[1] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(listsSetResponseTypeList, ":name");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				listsSetResponseTypeList.Items = (StatusType[])base.ShrinkArray(array, num, typeof(StatusType), true);
				listsSetResponseTypeList.ItemsElementName = (ItemsChoiceType[])base.ShrinkArray(array2, num2, typeof(ItemsChoiceType), true);
				return listsSetResponseTypeList;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num3 = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (base.Reader.LocalName == this.id22_Delete && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						array = (StatusType[])base.EnsureArrayIndex(array, num, typeof(StatusType));
						array[num++] = this.Read37_StatusType(false, true);
						array2 = (ItemsChoiceType[])base.EnsureArrayIndex(array2, num2, typeof(ItemsChoiceType));
						array2[num2++] = ItemsChoiceType.Delete;
					}
					else if (base.Reader.LocalName == this.id10_Set && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						array = (StatusType[])base.EnsureArrayIndex(array, num, typeof(StatusType));
						array[num++] = this.Read37_StatusType(false, true);
						array2 = (ItemsChoiceType[])base.EnsureArrayIndex(array2, num2, typeof(ItemsChoiceType));
						array2[num2++] = ItemsChoiceType.Set;
					}
					else if (base.Reader.LocalName == this.id23_Add && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						array = (StatusType[])base.EnsureArrayIndex(array, num, typeof(StatusType));
						array[num++] = this.Read37_StatusType(false, true);
						array2 = (ItemsChoiceType[])base.EnsureArrayIndex(array2, num2, typeof(ItemsChoiceType));
						array2[num2++] = ItemsChoiceType.Add;
					}
					else
					{
						base.UnknownNode(listsSetResponseTypeList, "HMSETTINGS::Delete, HMSETTINGS::Set, HMSETTINGS::Add");
					}
				}
				else
				{
					base.UnknownNode(listsSetResponseTypeList, "HMSETTINGS::Delete, HMSETTINGS::Set, HMSETTINGS::Add");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num3, ref readerCount);
			}
			listsSetResponseTypeList.Items = (StatusType[])base.ShrinkArray(array, num, typeof(StatusType), true);
			listsSetResponseTypeList.ItemsElementName = (ItemsChoiceType[])base.ShrinkArray(array2, num2, typeof(ItemsChoiceType), true);
			base.ReadEndElement();
			return listsSetResponseTypeList;
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x00076590 File Offset: 0x00074790
		private StatusType Read37_StatusType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id24_StatusType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			StatusType statusType = new StatusType();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(statusType);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return statusType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id4_Status && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						statusType.Status = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id5_Fault && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						statusType.Fault = this.Read34_Fault(false, true);
						array[1] = true;
					}
					else
					{
						base.UnknownNode(statusType, "HMSETTINGS::Status, HMSETTINGS::Fault");
					}
				}
				else
				{
					base.UnknownNode(statusType, "HMSETTINGS::Status, HMSETTINGS::Fault");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return statusType;
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x0007675C File Offset: 0x0007495C
		private SettingsAccountSettingsGet Read33_SettingsAccountSettingsGet(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SettingsAccountSettingsGet settingsAccountSettingsGet = new SettingsAccountSettingsGet();
			bool[] array = new bool[5];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(settingsAccountSettingsGet);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return settingsAccountSettingsGet;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (base.Reader.LocalName == this.id11_Filters && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						if (!base.ReadNull())
						{
							FiltersResponseTypeFilter[] array2 = null;
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
										if (base.Reader.LocalName == this.id25_Filter && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array2 = (FiltersResponseTypeFilter[])base.EnsureArrayIndex(array2, num2, typeof(FiltersResponseTypeFilter));
											array2[num2++] = this.Read19_FiltersResponseTypeFilter(false, true);
										}
										else
										{
											base.UnknownNode(null, "HMSETTINGS::Filter");
										}
									}
									else
									{
										base.UnknownNode(null, "HMSETTINGS::Filter");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num3, ref readerCount2);
								}
								base.ReadEndElement();
							}
							settingsAccountSettingsGet.Filters = (FiltersResponseTypeFilter[])base.ShrinkArray(array2, num2, typeof(FiltersResponseTypeFilter), false);
						}
					}
					else if (base.Reader.LocalName == this.id12_Lists && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						if (!base.ReadNull())
						{
							ListsGetResponseTypeList[] array3 = null;
							int num4 = 0;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num5 = 0;
								int readerCount3 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id20_List && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array3 = (ListsGetResponseTypeList[])base.EnsureArrayIndex(array3, num4, typeof(ListsGetResponseTypeList));
											array3[num4++] = this.Read7_ListsGetResponseTypeList(false, true);
										}
										else
										{
											base.UnknownNode(null, "HMSETTINGS::List");
										}
									}
									else
									{
										base.UnknownNode(null, "HMSETTINGS::List");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num5, ref readerCount3);
								}
								base.ReadEndElement();
							}
							settingsAccountSettingsGet.Lists = (ListsGetResponseTypeList[])base.ShrinkArray(array3, num4, typeof(ListsGetResponseTypeList), false);
						}
					}
					else if (!array[2] && base.Reader.LocalName == this.id13_Options && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAccountSettingsGet.Options = this.Read29_OptionsType(false, true);
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id26_Properties && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAccountSettingsGet.Properties = this.Read32_PropertiesType(false, true);
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id14_UserSignature && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAccountSettingsGet.UserSignature = this.Read14_StringWithVersionType(false, true);
						array[4] = true;
					}
					else
					{
						base.UnknownNode(settingsAccountSettingsGet, "HMSETTINGS::Filters, HMSETTINGS::Lists, HMSETTINGS::Options, HMSETTINGS::Properties, HMSETTINGS::UserSignature");
					}
				}
				else
				{
					base.UnknownNode(settingsAccountSettingsGet, "HMSETTINGS::Filters, HMSETTINGS::Lists, HMSETTINGS::Options, HMSETTINGS::Properties, HMSETTINGS::UserSignature");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settingsAccountSettingsGet;
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x00076C20 File Offset: 0x00074E20
		private StringWithVersionType Read14_StringWithVersionType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id27_StringWithVersionType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			StringWithVersionType stringWithVersionType = new StringWithVersionType();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id28_version && base.Reader.NamespaceURI == this.id3_Item)
				{
					stringWithVersionType.version = XmlConvert.ToInt32(base.Reader.Value);
					array[0] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(stringWithVersionType, ":version");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return stringWithVersionType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				string value = null;
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(stringWithVersionType, "");
				}
				else if (base.Reader.NodeType == XmlNodeType.Text || base.Reader.NodeType == XmlNodeType.CDATA || base.Reader.NodeType == XmlNodeType.Whitespace || base.Reader.NodeType == XmlNodeType.SignificantWhitespace)
				{
					value = base.ReadString(value, false);
					stringWithVersionType.Value = value;
				}
				else
				{
					base.UnknownNode(stringWithVersionType, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return stringWithVersionType;
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x00076E00 File Offset: 0x00075000
		private PropertiesType Read32_PropertiesType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id29_PropertiesType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			PropertiesType propertiesType = new PropertiesType();
			bool[] array = new bool[18];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(propertiesType);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return propertiesType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id30_AccountStatus && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.AccountStatus = this.Read30_AccountStatusType(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id31_ParentalControlStatus && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.ParentalControlStatus = this.Read31_ParentalControlStatusType(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id32_MailBoxSize && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.MailBoxSize = XmlConvert.ToInt64(base.Reader.ReadElementString());
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id33_MaxMailBoxSize && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.MaxMailBoxSize = XmlConvert.ToInt64(base.Reader.ReadElementString());
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id34_MaxAttachments && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.MaxAttachments = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id35_MaxMessageSize && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.MaxMessageSize = XmlConvert.ToInt64(base.Reader.ReadElementString());
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id36_MaxUnencodedMessageSize && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.MaxUnencodedMessageSize = XmlConvert.ToInt64(base.Reader.ReadElementString());
						array[6] = true;
					}
					else if (!array[7] && base.Reader.LocalName == this.id37_MaxFilters && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.MaxFilters = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[7] = true;
					}
					else if (!array[8] && base.Reader.LocalName == this.id38_MaxFilterClauseValueLength && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.MaxFilterClauseValueLength = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[8] = true;
					}
					else if (!array[9] && base.Reader.LocalName == this.id39_MaxRecipients && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.MaxRecipients = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[9] = true;
					}
					else if (!array[10] && base.Reader.LocalName == this.id40_MaxUserSignatureLength && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.MaxUserSignatureLength = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[10] = true;
					}
					else if (!array[11] && base.Reader.LocalName == this.id41_BlockListAddressMax && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.BlockListAddressMax = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[11] = true;
					}
					else if (!array[12] && base.Reader.LocalName == this.id42_BlockListDomainMax && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.BlockListDomainMax = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[12] = true;
					}
					else if (!array[13] && base.Reader.LocalName == this.id43_WhiteListAddressMax && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.WhiteListAddressMax = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[13] = true;
					}
					else if (!array[14] && base.Reader.LocalName == this.id44_WhiteListDomainMax && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.WhiteListDomainMax = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[14] = true;
					}
					else if (!array[15] && base.Reader.LocalName == this.id45_WhiteToListMax && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.WhiteToListMax = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[15] = true;
					}
					else if (!array[16] && base.Reader.LocalName == this.id46_AlternateFromListMax && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.AlternateFromListMax = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[16] = true;
					}
					else if (!array[17] && base.Reader.LocalName == this.id47_MaxDailySendMessages && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						propertiesType.MaxDailySendMessages = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[17] = true;
					}
					else
					{
						base.UnknownNode(propertiesType, "HMSETTINGS::AccountStatus, HMSETTINGS::ParentalControlStatus, HMSETTINGS::MailBoxSize, HMSETTINGS::MaxMailBoxSize, HMSETTINGS::MaxAttachments, HMSETTINGS::MaxMessageSize, HMSETTINGS::MaxUnencodedMessageSize, HMSETTINGS::MaxFilters, HMSETTINGS::MaxFilterClauseValueLength, HMSETTINGS::MaxRecipients, HMSETTINGS::MaxUserSignatureLength, HMSETTINGS::BlockListAddressMax, HMSETTINGS::BlockListDomainMax, HMSETTINGS::WhiteListAddressMax, HMSETTINGS::WhiteListDomainMax, HMSETTINGS::WhiteToListMax, HMSETTINGS::AlternateFromListMax, HMSETTINGS::MaxDailySendMessages");
					}
				}
				else
				{
					base.UnknownNode(propertiesType, "HMSETTINGS::AccountStatus, HMSETTINGS::ParentalControlStatus, HMSETTINGS::MailBoxSize, HMSETTINGS::MaxMailBoxSize, HMSETTINGS::MaxAttachments, HMSETTINGS::MaxMessageSize, HMSETTINGS::MaxUnencodedMessageSize, HMSETTINGS::MaxFilters, HMSETTINGS::MaxFilterClauseValueLength, HMSETTINGS::MaxRecipients, HMSETTINGS::MaxUserSignatureLength, HMSETTINGS::BlockListAddressMax, HMSETTINGS::BlockListDomainMax, HMSETTINGS::WhiteListAddressMax, HMSETTINGS::WhiteListDomainMax, HMSETTINGS::WhiteToListMax, HMSETTINGS::AlternateFromListMax, HMSETTINGS::MaxDailySendMessages");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return propertiesType;
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x00077488 File Offset: 0x00075688
		private ParentalControlStatusType Read31_ParentalControlStatusType(string s)
		{
			if (s != null)
			{
				if (s == "None")
				{
					return ParentalControlStatusType.None;
				}
				if (s == "FullAccess")
				{
					return ParentalControlStatusType.FullAccess;
				}
				if (s == "RestrictedAccess")
				{
					return ParentalControlStatusType.RestrictedAccess;
				}
				if (s == "NoAccess")
				{
					return ParentalControlStatusType.NoAccess;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(ParentalControlStatusType));
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000774EC File Offset: 0x000756EC
		private AccountStatusType Read30_AccountStatusType(string s)
		{
			if (s != null)
			{
				if (s == "OK")
				{
					return AccountStatusType.OK;
				}
				if (s == "Blocked")
				{
					return AccountStatusType.Blocked;
				}
				if (s == "RequiresHIP")
				{
					return AccountStatusType.RequiresHIP;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(AccountStatusType));
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x00077540 File Offset: 0x00075740
		private OptionsType Read29_OptionsType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id48_OptionsType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			OptionsType optionsType = new OptionsType();
			bool[] array = new bool[11];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(optionsType);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return optionsType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id49_ConfirmSent && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.ConfirmSent = XmlConvert.ToByte(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id50_HeaderDisplay && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.HeaderDisplay = this.Read20_HeaderDisplayType(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id51_IncludeOriginalInReply && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.IncludeOriginalInReply = this.Read21_IncludeOriginalInReplyType(base.Reader.ReadElementString());
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id52_JunkLevel && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.JunkLevel = this.Read22_JunkLevelType(base.Reader.ReadElementString());
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id53_JunkMailDestination && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.JunkMailDestination = this.Read23_JunkMailDestinationType(base.Reader.ReadElementString());
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id54_ReplyIndicator && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.ReplyIndicator = this.Read24_ReplyIndicatorType(base.Reader.ReadElementString());
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id55_ReplyToAddress && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.ReplyToAddress = base.Reader.ReadElementString();
						array[6] = true;
					}
					else if (!array[7] && base.Reader.LocalName == this.id56_SaveSentMail && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.SaveSentMail = XmlConvert.ToByte(base.Reader.ReadElementString());
						array[7] = true;
					}
					else if (!array[8] && base.Reader.LocalName == this.id57_UseReplyToAddress && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.UseReplyToAddress = XmlConvert.ToByte(base.Reader.ReadElementString());
						array[8] = true;
					}
					else if (!array[9] && base.Reader.LocalName == this.id58_VacationResponse && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.VacationResponse = this.Read26_OptionsTypeVacationResponse(false, true);
						array[9] = true;
					}
					else if (!array[10] && base.Reader.LocalName == this.id59_MailForwarding && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.MailForwarding = this.Read28_OptionsTypeMailForwarding(false, true);
						array[10] = true;
					}
					else
					{
						base.UnknownNode(optionsType, "HMSETTINGS::ConfirmSent, HMSETTINGS::HeaderDisplay, HMSETTINGS::IncludeOriginalInReply, HMSETTINGS::JunkLevel, HMSETTINGS::JunkMailDestination, HMSETTINGS::ReplyIndicator, HMSETTINGS::ReplyToAddress, HMSETTINGS::SaveSentMail, HMSETTINGS::UseReplyToAddress, HMSETTINGS::VacationResponse, HMSETTINGS::MailForwarding");
					}
				}
				else
				{
					base.UnknownNode(optionsType, "HMSETTINGS::ConfirmSent, HMSETTINGS::HeaderDisplay, HMSETTINGS::IncludeOriginalInReply, HMSETTINGS::JunkLevel, HMSETTINGS::JunkMailDestination, HMSETTINGS::ReplyIndicator, HMSETTINGS::ReplyToAddress, HMSETTINGS::SaveSentMail, HMSETTINGS::UseReplyToAddress, HMSETTINGS::VacationResponse, HMSETTINGS::MailForwarding");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return optionsType;
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000779A4 File Offset: 0x00075BA4
		private OptionsTypeMailForwarding Read28_OptionsTypeMailForwarding(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			OptionsTypeMailForwarding optionsTypeMailForwarding = new OptionsTypeMailForwarding();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(optionsTypeMailForwarding);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return optionsTypeMailForwarding;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id60_Mode && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsTypeMailForwarding.Mode = this.Read27_ForwardingMode(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (base.Reader.LocalName == this.id61_Addresses && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						if (!base.ReadNull())
						{
							string[] array2 = null;
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
										if (base.Reader.LocalName == this.id62_Address && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array2 = (string[])base.EnsureArrayIndex(array2, num2, typeof(string));
											array2[num2++] = base.Reader.ReadElementString();
										}
										else
										{
											base.UnknownNode(null, "HMSETTINGS::Address");
										}
									}
									else
									{
										base.UnknownNode(null, "HMSETTINGS::Address");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num3, ref readerCount2);
								}
								base.ReadEndElement();
							}
							optionsTypeMailForwarding.Addresses = (string[])base.ShrinkArray(array2, num2, typeof(string), false);
						}
					}
					else
					{
						base.UnknownNode(optionsTypeMailForwarding, "HMSETTINGS::Mode, HMSETTINGS::Addresses");
					}
				}
				else
				{
					base.UnknownNode(optionsTypeMailForwarding, "HMSETTINGS::Mode, HMSETTINGS::Addresses");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return optionsTypeMailForwarding;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x00077C94 File Offset: 0x00075E94
		private ForwardingMode Read27_ForwardingMode(string s)
		{
			if (s != null)
			{
				if (s == "NoForwarding")
				{
					return ForwardingMode.NoForwarding;
				}
				if (s == "ForwardOnly")
				{
					return ForwardingMode.ForwardOnly;
				}
				if (s == "StoreAndForward")
				{
					return ForwardingMode.StoreAndForward;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(ForwardingMode));
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x00077CE8 File Offset: 0x00075EE8
		private OptionsTypeVacationResponse Read26_OptionsTypeVacationResponse(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			OptionsTypeVacationResponse optionsTypeVacationResponse = new OptionsTypeVacationResponse();
			bool[] array = new bool[4];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(optionsTypeVacationResponse);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return optionsTypeVacationResponse;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id60_Mode && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsTypeVacationResponse.Mode = this.Read25_VacationResponseMode(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id63_StartTime && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsTypeVacationResponse.StartTime = base.Reader.ReadElementString();
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id64_EndTime && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsTypeVacationResponse.EndTime = base.Reader.ReadElementString();
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id65_Message && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsTypeVacationResponse.Message = base.Reader.ReadElementString();
						array[3] = true;
					}
					else
					{
						base.UnknownNode(optionsTypeVacationResponse, "HMSETTINGS::Mode, HMSETTINGS::StartTime, HMSETTINGS::EndTime, HMSETTINGS::Message");
					}
				}
				else
				{
					base.UnknownNode(optionsTypeVacationResponse, "HMSETTINGS::Mode, HMSETTINGS::StartTime, HMSETTINGS::EndTime, HMSETTINGS::Message");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return optionsTypeVacationResponse;
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x00077F44 File Offset: 0x00076144
		private VacationResponseMode Read25_VacationResponseMode(string s)
		{
			if (s != null)
			{
				if (s == "NoVacationResponse")
				{
					return VacationResponseMode.NoVacationResponse;
				}
				if (s == "OncePerSender")
				{
					return VacationResponseMode.OncePerSender;
				}
				if (s == "OncePerContact")
				{
					return VacationResponseMode.OncePerContact;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(VacationResponseMode));
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x00077F98 File Offset: 0x00076198
		private ReplyIndicatorType Read24_ReplyIndicatorType(string s)
		{
			if (s != null)
			{
				if (s == "None")
				{
					return ReplyIndicatorType.None;
				}
				if (s == "Line")
				{
					return ReplyIndicatorType.Line;
				}
				if (s == "Arrow")
				{
					return ReplyIndicatorType.Arrow;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(ReplyIndicatorType));
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x00077FEC File Offset: 0x000761EC
		private JunkMailDestinationType Read23_JunkMailDestinationType(string s)
		{
			if (s != null)
			{
				if (s == "Immediate Deletion")
				{
					return JunkMailDestinationType.ImmediateDeletion;
				}
				if (s == "Junk Mail")
				{
					return JunkMailDestinationType.JunkMail;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(JunkMailDestinationType));
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x00078030 File Offset: 0x00076230
		private JunkLevelType Read22_JunkLevelType(string s)
		{
			if (s != null)
			{
				if (s == "Off")
				{
					return JunkLevelType.Off;
				}
				if (s == "Low")
				{
					return JunkLevelType.Low;
				}
				if (s == "High")
				{
					return JunkLevelType.High;
				}
				if (s == "Exclusive")
				{
					return JunkLevelType.Exclusive;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(JunkLevelType));
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x00078094 File Offset: 0x00076294
		private IncludeOriginalInReplyType Read21_IncludeOriginalInReplyType(string s)
		{
			if (s != null)
			{
				if (s == "Auto")
				{
					return IncludeOriginalInReplyType.Auto;
				}
				if (s == "Manual")
				{
					return IncludeOriginalInReplyType.Manual;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(IncludeOriginalInReplyType));
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000780D8 File Offset: 0x000762D8
		private HeaderDisplayType Read20_HeaderDisplayType(string s)
		{
			if (s != null)
			{
				if (s == "No Header")
				{
					return HeaderDisplayType.NoHeader;
				}
				if (s == "Basic")
				{
					return HeaderDisplayType.Basic;
				}
				if (s == "Full")
				{
					return HeaderDisplayType.Full;
				}
				if (s == "Advanced")
				{
					return HeaderDisplayType.Advanced;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(HeaderDisplayType));
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x0007813C File Offset: 0x0007633C
		private ListsGetResponseTypeList Read7_ListsGetResponseTypeList(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ListsGetResponseTypeList listsGetResponseTypeList = new ListsGetResponseTypeList();
			bool[] array = new bool[4];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[3] && base.Reader.LocalName == this.id21_name && base.Reader.NamespaceURI == this.id3_Item)
				{
					listsGetResponseTypeList.name = base.Reader.Value;
					array[3] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(listsGetResponseTypeList, ":name");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return listsGetResponseTypeList;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (base.Reader.LocalName == this.id61_Addresses && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						if (!base.ReadNull())
						{
							string[] array2 = null;
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
										if (base.Reader.LocalName == this.id62_Address && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array2 = (string[])base.EnsureArrayIndex(array2, num2, typeof(string));
											array2[num2++] = base.Reader.ReadElementString();
										}
										else
										{
											base.UnknownNode(null, "HMSETTINGS::Address");
										}
									}
									else
									{
										base.UnknownNode(null, "HMSETTINGS::Address");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num3, ref readerCount2);
								}
								base.ReadEndElement();
							}
							listsGetResponseTypeList.Addresses = (string[])base.ShrinkArray(array2, num2, typeof(string), false);
						}
					}
					else if (base.Reader.LocalName == this.id66_Domains && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						if (!base.ReadNull())
						{
							string[] array3 = null;
							int num4 = 0;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num5 = 0;
								int readerCount3 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id67_Domain && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array3 = (string[])base.EnsureArrayIndex(array3, num4, typeof(string));
											array3[num4++] = base.Reader.ReadElementString();
										}
										else
										{
											base.UnknownNode(null, "HMSETTINGS::Domain");
										}
									}
									else
									{
										base.UnknownNode(null, "HMSETTINGS::Domain");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num5, ref readerCount3);
								}
								base.ReadEndElement();
							}
							listsGetResponseTypeList.Domains = (string[])base.ShrinkArray(array3, num4, typeof(string), false);
						}
					}
					else if (base.Reader.LocalName == this.id68_LocalParts && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						if (!base.ReadNull())
						{
							string[] array4 = null;
							int num6 = 0;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num7 = 0;
								int readerCount4 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id69_LocalPart && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array4 = (string[])base.EnsureArrayIndex(array4, num6, typeof(string));
											array4[num6++] = base.Reader.ReadElementString();
										}
										else
										{
											base.UnknownNode(null, "HMSETTINGS::LocalPart");
										}
									}
									else
									{
										base.UnknownNode(null, "HMSETTINGS::LocalPart");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num7, ref readerCount4);
								}
								base.ReadEndElement();
							}
							listsGetResponseTypeList.LocalParts = (string[])base.ShrinkArray(array4, num6, typeof(string), false);
						}
					}
					else
					{
						base.UnknownNode(listsGetResponseTypeList, "HMSETTINGS::Addresses, HMSETTINGS::Domains, HMSETTINGS::LocalParts");
					}
				}
				else
				{
					base.UnknownNode(listsGetResponseTypeList, "HMSETTINGS::Addresses, HMSETTINGS::Domains, HMSETTINGS::LocalParts");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return listsGetResponseTypeList;
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000786E8 File Offset: 0x000768E8
		private FiltersResponseTypeFilter Read19_FiltersResponseTypeFilter(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			FiltersResponseTypeFilter filtersResponseTypeFilter = new FiltersResponseTypeFilter();
			bool[] array = new bool[5];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(filtersResponseTypeFilter);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return filtersResponseTypeFilter;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id70_ExecutionOrder && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersResponseTypeFilter.ExecutionOrder = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id71_Enabled && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersResponseTypeFilter.Enabled = XmlConvert.ToByte(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id72_RunWhen && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersResponseTypeFilter.RunWhen = this.Read11_RunWhenType(base.Reader.ReadElementString());
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id73_Condition && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersResponseTypeFilter.Condition = this.Read16_Item(false, true);
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id74_Actions && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersResponseTypeFilter.Actions = this.Read18_Item(false, true);
						array[4] = true;
					}
					else
					{
						base.UnknownNode(filtersResponseTypeFilter, "HMSETTINGS::ExecutionOrder, HMSETTINGS::Enabled, HMSETTINGS::RunWhen, HMSETTINGS::Condition, HMSETTINGS::Actions");
					}
				}
				else
				{
					base.UnknownNode(filtersResponseTypeFilter, "HMSETTINGS::ExecutionOrder, HMSETTINGS::Enabled, HMSETTINGS::RunWhen, HMSETTINGS::Condition, HMSETTINGS::Actions");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return filtersResponseTypeFilter;
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x0007898C File Offset: 0x00076B8C
		private FiltersResponseTypeFilterActions Read18_Item(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			FiltersResponseTypeFilterActions filtersResponseTypeFilterActions = new FiltersResponseTypeFilterActions();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(filtersResponseTypeFilterActions);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return filtersResponseTypeFilterActions;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id75_MoveToFolder && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersResponseTypeFilterActions.MoveToFolder = this.Read17_Item(false, true);
						array[0] = true;
					}
					else
					{
						base.UnknownNode(filtersResponseTypeFilterActions, "HMSETTINGS::MoveToFolder");
					}
				}
				else
				{
					base.UnknownNode(filtersResponseTypeFilterActions, "HMSETTINGS::MoveToFolder");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return filtersResponseTypeFilterActions;
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x00078B0C File Offset: 0x00076D0C
		private FiltersResponseTypeFilterActionsMoveToFolder Read17_Item(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			FiltersResponseTypeFilterActionsMoveToFolder filtersResponseTypeFilterActionsMoveToFolder = new FiltersResponseTypeFilterActionsMoveToFolder();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(filtersResponseTypeFilterActionsMoveToFolder);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return filtersResponseTypeFilterActionsMoveToFolder;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id76_FolderId && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersResponseTypeFilterActionsMoveToFolder.FolderId = base.Reader.ReadElementString();
						array[0] = true;
					}
					else
					{
						base.UnknownNode(filtersResponseTypeFilterActionsMoveToFolder, "HMSETTINGS::FolderId");
					}
				}
				else
				{
					base.UnknownNode(filtersResponseTypeFilterActionsMoveToFolder, "HMSETTINGS::FolderId");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return filtersResponseTypeFilterActionsMoveToFolder;
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x00078C90 File Offset: 0x00076E90
		private FiltersResponseTypeFilterCondition Read16_Item(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			FiltersResponseTypeFilterCondition filtersResponseTypeFilterCondition = new FiltersResponseTypeFilterCondition();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(filtersResponseTypeFilterCondition);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return filtersResponseTypeFilterCondition;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id77_Clause && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersResponseTypeFilterCondition.Clause = this.Read15_Item(false, true);
						array[0] = true;
					}
					else
					{
						base.UnknownNode(filtersResponseTypeFilterCondition, "HMSETTINGS::Clause");
					}
				}
				else
				{
					base.UnknownNode(filtersResponseTypeFilterCondition, "HMSETTINGS::Clause");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return filtersResponseTypeFilterCondition;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x00078E10 File Offset: 0x00077010
		private FiltersResponseTypeFilterConditionClause Read15_Item(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			FiltersResponseTypeFilterConditionClause filtersResponseTypeFilterConditionClause = new FiltersResponseTypeFilterConditionClause();
			bool[] array = new bool[3];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(filtersResponseTypeFilterConditionClause);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return filtersResponseTypeFilterConditionClause;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id78_Field && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersResponseTypeFilterConditionClause.Field = this.Read12_FilterKeyType(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id79_Operator && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersResponseTypeFilterConditionClause.Operator = this.Read13_FilterOperatorType(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id80_Value && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersResponseTypeFilterConditionClause.Value = this.Read14_StringWithVersionType(false, true);
						array[2] = true;
					}
					else
					{
						base.UnknownNode(filtersResponseTypeFilterConditionClause, "HMSETTINGS::Field, HMSETTINGS::Operator, HMSETTINGS::Value");
					}
				}
				else
				{
					base.UnknownNode(filtersResponseTypeFilterConditionClause, "HMSETTINGS::Field, HMSETTINGS::Operator, HMSETTINGS::Value");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return filtersResponseTypeFilterConditionClause;
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x00079028 File Offset: 0x00077228
		private FilterOperatorType Read13_FilterOperatorType(string s)
		{
			switch (s)
			{
			case "Contains":
				return FilterOperatorType.Contains;
			case "Does not contain":
				return FilterOperatorType.Doesnotcontain;
			case "Contains word":
				return FilterOperatorType.Containsword;
			case "Starts with":
				return FilterOperatorType.Startswith;
			case "Ends with":
				return FilterOperatorType.Endswith;
			case "Equals":
				return FilterOperatorType.Equals;
			}
			throw base.CreateUnknownConstantException(s, typeof(FilterOperatorType));
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000790EC File Offset: 0x000772EC
		private FilterKeyType Read12_FilterKeyType(string s)
		{
			if (s != null)
			{
				if (s == "Subject")
				{
					return FilterKeyType.Subject;
				}
				if (s == "From Name")
				{
					return FilterKeyType.FromName;
				}
				if (s == "From Address")
				{
					return FilterKeyType.FromAddress;
				}
				if (s == "To or CC Line")
				{
					return FilterKeyType.ToorCCLine;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(FilterKeyType));
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x00079150 File Offset: 0x00077350
		private RunWhenType Read11_RunWhenType(string s)
		{
			if (s != null)
			{
				if (s == "MessageReceived")
				{
					return RunWhenType.MessageReceived;
				}
				if (s == "MessageSent")
				{
					return RunWhenType.MessageSent;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(RunWhenType));
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x00079194 File Offset: 0x00077394
		private SettingsServiceSettings Read10_SettingsServiceSettings(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SettingsServiceSettings settingsServiceSettings = new SettingsServiceSettings();
			bool[] array = new bool[5];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(settingsServiceSettings);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return settingsServiceSettings;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id4_Status && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsServiceSettings.Status = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id81_SafetyLevelRules && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsServiceSettings.SafetyLevelRules = this.Read4_RulesResponseType(false, true);
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id82_SafetyActions && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsServiceSettings.SafetyActions = this.Read4_RulesResponseType(false, true);
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id26_Properties && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsServiceSettings.Properties = this.Read6_Item(false, true);
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id12_Lists && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsServiceSettings.Lists = this.Read9_SettingsServiceSettingsLists(false, true);
						array[4] = true;
					}
					else
					{
						base.UnknownNode(settingsServiceSettings, "HMSETTINGS::Status, HMSETTINGS::SafetyLevelRules, HMSETTINGS::SafetyActions, HMSETTINGS::Properties, HMSETTINGS::Lists");
					}
				}
				else
				{
					base.UnknownNode(settingsServiceSettings, "HMSETTINGS::Status, HMSETTINGS::SafetyLevelRules, HMSETTINGS::SafetyActions, HMSETTINGS::Properties, HMSETTINGS::Lists");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settingsServiceSettings;
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x00079428 File Offset: 0x00077628
		private SettingsServiceSettingsLists Read9_SettingsServiceSettingsLists(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SettingsServiceSettingsLists settingsServiceSettingsLists = new SettingsServiceSettingsLists();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(settingsServiceSettingsLists);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return settingsServiceSettingsLists;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id4_Status && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsServiceSettingsLists.Status = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id9_Get && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsServiceSettingsLists.Get = this.Read8_Item(false, true);
						array[1] = true;
					}
					else
					{
						base.UnknownNode(settingsServiceSettingsLists, "HMSETTINGS::Status, HMSETTINGS::Get");
					}
				}
				else
				{
					base.UnknownNode(settingsServiceSettingsLists, "HMSETTINGS::Status, HMSETTINGS::Get");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settingsServiceSettingsLists;
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000795F4 File Offset: 0x000777F4
		private SettingsServiceSettingsListsGet Read8_Item(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SettingsServiceSettingsListsGet settingsServiceSettingsListsGet = new SettingsServiceSettingsListsGet();
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(settingsServiceSettingsListsGet);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return settingsServiceSettingsListsGet;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (base.Reader.LocalName == this.id12_Lists && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						if (!base.ReadNull())
						{
							ListsGetResponseTypeList[] array = null;
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
										if (base.Reader.LocalName == this.id20_List && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array = (ListsGetResponseTypeList[])base.EnsureArrayIndex(array, num2, typeof(ListsGetResponseTypeList));
											array[num2++] = this.Read7_ListsGetResponseTypeList(false, true);
										}
										else
										{
											base.UnknownNode(null, "HMSETTINGS::List");
										}
									}
									else
									{
										base.UnknownNode(null, "HMSETTINGS::List");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num3, ref readerCount2);
								}
								base.ReadEndElement();
							}
							settingsServiceSettingsListsGet.Lists = (ListsGetResponseTypeList[])base.ShrinkArray(array, num2, typeof(ListsGetResponseTypeList), false);
						}
					}
					else
					{
						base.UnknownNode(settingsServiceSettingsListsGet, "HMSETTINGS::Lists");
					}
				}
				else
				{
					base.UnknownNode(settingsServiceSettingsListsGet, "HMSETTINGS::Lists");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settingsServiceSettingsListsGet;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x0007988C File Offset: 0x00077A8C
		private SettingsServiceSettingsProperties Read6_Item(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SettingsServiceSettingsProperties settingsServiceSettingsProperties = new SettingsServiceSettingsProperties();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(settingsServiceSettingsProperties);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return settingsServiceSettingsProperties;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id4_Status && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsServiceSettingsProperties.Status = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id9_Get && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsServiceSettingsProperties.Get = this.Read5_ServiceSettingsPropertiesType(false, true);
						array[1] = true;
					}
					else
					{
						base.UnknownNode(settingsServiceSettingsProperties, "HMSETTINGS::Status, HMSETTINGS::Get");
					}
				}
				else
				{
					base.UnknownNode(settingsServiceSettingsProperties, "HMSETTINGS::Status, HMSETTINGS::Get");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settingsServiceSettingsProperties;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x00079A58 File Offset: 0x00077C58
		private ServiceSettingsPropertiesType Read5_ServiceSettingsPropertiesType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id83_ServiceSettingsPropertiesType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ServiceSettingsPropertiesType serviceSettingsPropertiesType = new ServiceSettingsPropertiesType();
			bool[] array = new bool[15];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(serviceSettingsPropertiesType);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return serviceSettingsPropertiesType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id84_ServiceTimeOut && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.ServiceTimeOut = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id85_MinSyncPollInterval && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MinSyncPollInterval = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id86_MinSettingsPollInterval && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MinSettingsPollInterval = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id87_SyncMultiplier && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.SyncMultiplier = XmlConvert.ToDouble(base.Reader.ReadElementString());
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id88_MaxObjectsInSync && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MaxObjectsInSync = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id89_MaxNumberOfEmailAdds && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MaxNumberOfEmailAdds = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id90_MaxNumberOfFolderAdds && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MaxNumberOfFolderAdds = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[6] = true;
					}
					else if (!array[7] && base.Reader.LocalName == this.id91_MaxNumberOfStatelessObjects && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MaxNumberOfStatelessObjects = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[7] = true;
					}
					else if (!array[8] && base.Reader.LocalName == this.id92_Item && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.DefaultStatelessEmailWindowSize = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[8] = true;
					}
					else if (!array[9] && base.Reader.LocalName == this.id93_MaxStatelessEmailWindowSize && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MaxStatelessEmailWindowSize = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[9] = true;
					}
					else if (!array[10] && base.Reader.LocalName == this.id94_Item && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MaxTotalLengthOfForwardingAddresses = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[10] = true;
					}
					else if (!array[11] && base.Reader.LocalName == this.id95_Item && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MaxVacationResponseMessageLength = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[11] = true;
					}
					else if (!array[12] && base.Reader.LocalName == this.id96_MinVacationResponseStartTime && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MinVacationResponseStartTime = base.Reader.ReadElementString();
						array[12] = true;
					}
					else if (!array[13] && base.Reader.LocalName == this.id97_MaxVacationResponseEndTime && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MaxVacationResponseEndTime = base.Reader.ReadElementString();
						array[13] = true;
					}
					else if (!array[14] && base.Reader.LocalName == this.id98_MaxNumberOfProvisionCommands && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsPropertiesType.MaxNumberOfProvisionCommands = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[14] = true;
					}
					else
					{
						base.UnknownNode(serviceSettingsPropertiesType, "HMSETTINGS::ServiceTimeOut, HMSETTINGS::MinSyncPollInterval, HMSETTINGS::MinSettingsPollInterval, HMSETTINGS::SyncMultiplier, HMSETTINGS::MaxObjectsInSync, HMSETTINGS::MaxNumberOfEmailAdds, HMSETTINGS::MaxNumberOfFolderAdds, HMSETTINGS::MaxNumberOfStatelessObjects, HMSETTINGS::DefaultStatelessEmailWindowSize, HMSETTINGS::MaxStatelessEmailWindowSize, HMSETTINGS::MaxTotalLengthOfForwardingAddresses, HMSETTINGS::MaxVacationResponseMessageLength, HMSETTINGS::MinVacationResponseStartTime, HMSETTINGS::MaxVacationResponseEndTime, HMSETTINGS::MaxNumberOfProvisionCommands");
					}
				}
				else
				{
					base.UnknownNode(serviceSettingsPropertiesType, "HMSETTINGS::ServiceTimeOut, HMSETTINGS::MinSyncPollInterval, HMSETTINGS::MinSettingsPollInterval, HMSETTINGS::SyncMultiplier, HMSETTINGS::MaxObjectsInSync, HMSETTINGS::MaxNumberOfEmailAdds, HMSETTINGS::MaxNumberOfFolderAdds, HMSETTINGS::MaxNumberOfStatelessObjects, HMSETTINGS::DefaultStatelessEmailWindowSize, HMSETTINGS::MaxStatelessEmailWindowSize, HMSETTINGS::MaxTotalLengthOfForwardingAddresses, HMSETTINGS::MaxVacationResponseMessageLength, HMSETTINGS::MinVacationResponseStartTime, HMSETTINGS::MaxVacationResponseEndTime, HMSETTINGS::MaxNumberOfProvisionCommands");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return serviceSettingsPropertiesType;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x00079FF0 File Offset: 0x000781F0
		private RulesResponseType Read4_RulesResponseType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id99_RulesResponseType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			RulesResponseType rulesResponseType = new RulesResponseType();
			bool[] array = new bool[3];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(rulesResponseType);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return rulesResponseType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id4_Status && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						rulesResponseType.Status = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id9_Get && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						rulesResponseType.Get = (XmlElement)base.ReadXmlNode(true);
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id100_Version && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						rulesResponseType.Version = base.Reader.ReadElementString();
						array[2] = true;
					}
					else
					{
						base.UnknownNode(rulesResponseType, "HMSETTINGS::Status, HMSETTINGS::Get, HMSETTINGS::Version");
					}
				}
				else
				{
					base.UnknownNode(rulesResponseType, "HMSETTINGS::Status, HMSETTINGS::Get, HMSETTINGS::Version");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return rulesResponseType;
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x0007A204 File Offset: 0x00078404
		private SettingsAuthPolicy Read3_SettingsAuthPolicy(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SettingsAuthPolicy settingsAuthPolicy = new SettingsAuthPolicy();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(settingsAuthPolicy);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return settingsAuthPolicy;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id101_SAP && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAuthPolicy.SAP = base.Reader.ReadElementString();
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id100_Version && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsAuthPolicy.Version = base.Reader.ReadElementString();
						array[1] = true;
					}
					else
					{
						base.UnknownNode(settingsAuthPolicy, "HMSETTINGS::SAP, HMSETTINGS::Version");
					}
				}
				else
				{
					base.UnknownNode(settingsAuthPolicy, "HMSETTINGS::SAP, HMSETTINGS::Version");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settingsAuthPolicy;
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x0007A3D0 File Offset: 0x000785D0
		private SettingsFault Read2_SettingsFault(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SettingsFault settingsFault = new SettingsFault();
			bool[] array = new bool[3];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(settingsFault);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return settingsFault;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id16_Faultcode && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsFault.Faultcode = base.Reader.ReadElementString();
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id17_Faultstring && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsFault.Faultstring = base.Reader.ReadElementString();
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id18_Detail && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settingsFault.Detail = base.Reader.ReadElementString();
						array[2] = true;
					}
					else
					{
						base.UnknownNode(settingsFault, "HMSETTINGS::Faultcode, HMSETTINGS::Faultstring, HMSETTINGS::Detail");
					}
				}
				else
				{
					base.UnknownNode(settingsFault, "HMSETTINGS::Faultcode, HMSETTINGS::Faultstring, HMSETTINGS::Detail");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settingsFault;
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x0007A5DE File Offset: 0x000787DE
		protected override void InitCallbacks()
		{
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x0007A5E0 File Offset: 0x000787E0
		protected override void InitIDs()
		{
			this.id17_Faultstring = base.Reader.NameTable.Add("Faultstring");
			this.id32_MailBoxSize = base.Reader.NameTable.Add("MailBoxSize");
			this.id65_Message = base.Reader.NameTable.Add("Message");
			this.id49_ConfirmSent = base.Reader.NameTable.Add("ConfirmSent");
			this.id98_MaxNumberOfProvisionCommands = base.Reader.NameTable.Add("MaxNumberOfProvisionCommands");
			this.id89_MaxNumberOfEmailAdds = base.Reader.NameTable.Add("MaxNumberOfEmailAdds");
			this.id23_Add = base.Reader.NameTable.Add("Add");
			this.id62_Address = base.Reader.NameTable.Add("Address");
			this.id37_MaxFilters = base.Reader.NameTable.Add("MaxFilters");
			this.id83_ServiceSettingsPropertiesType = base.Reader.NameTable.Add("ServiceSettingsPropertiesType");
			this.id35_MaxMessageSize = base.Reader.NameTable.Add("MaxMessageSize");
			this.id91_MaxNumberOfStatelessObjects = base.Reader.NameTable.Add("MaxNumberOfStatelessObjects");
			this.id22_Delete = base.Reader.NameTable.Add("Delete");
			this.id57_UseReplyToAddress = base.Reader.NameTable.Add("UseReplyToAddress");
			this.id77_Clause = base.Reader.NameTable.Add("Clause");
			this.id93_MaxStatelessEmailWindowSize = base.Reader.NameTable.Add("MaxStatelessEmailWindowSize");
			this.id18_Detail = base.Reader.NameTable.Add("Detail");
			this.id7_ServiceSettings = base.Reader.NameTable.Add("ServiceSettings");
			this.id40_MaxUserSignatureLength = base.Reader.NameTable.Add("MaxUserSignatureLength");
			this.id61_Addresses = base.Reader.NameTable.Add("Addresses");
			this.id59_MailForwarding = base.Reader.NameTable.Add("MailForwarding");
			this.id80_Value = base.Reader.NameTable.Add("Value");
			this.id74_Actions = base.Reader.NameTable.Add("Actions");
			this.id67_Domain = base.Reader.NameTable.Add("Domain");
			this.id28_version = base.Reader.NameTable.Add("version");
			this.id25_Filter = base.Reader.NameTable.Add("Filter");
			this.id48_OptionsType = base.Reader.NameTable.Add("OptionsType");
			this.id66_Domains = base.Reader.NameTable.Add("Domains");
			this.id55_ReplyToAddress = base.Reader.NameTable.Add("ReplyToAddress");
			this.id36_MaxUnencodedMessageSize = base.Reader.NameTable.Add("MaxUnencodedMessageSize");
			this.id90_MaxNumberOfFolderAdds = base.Reader.NameTable.Add("MaxNumberOfFolderAdds");
			this.id21_name = base.Reader.NameTable.Add("name");
			this.id4_Status = base.Reader.NameTable.Add("Status");
			this.id50_HeaderDisplay = base.Reader.NameTable.Add("HeaderDisplay");
			this.id20_List = base.Reader.NameTable.Add("List");
			this.id34_MaxAttachments = base.Reader.NameTable.Add("MaxAttachments");
			this.id33_MaxMailBoxSize = base.Reader.NameTable.Add("MaxMailBoxSize");
			this.id78_Field = base.Reader.NameTable.Add("Field");
			this.id63_StartTime = base.Reader.NameTable.Add("StartTime");
			this.id14_UserSignature = base.Reader.NameTable.Add("UserSignature");
			this.id10_Set = base.Reader.NameTable.Add("Set");
			this.id38_MaxFilterClauseValueLength = base.Reader.NameTable.Add("MaxFilterClauseValueLength");
			this.id30_AccountStatus = base.Reader.NameTable.Add("AccountStatus");
			this.id46_AlternateFromListMax = base.Reader.NameTable.Add("AlternateFromListMax");
			this.id45_WhiteToListMax = base.Reader.NameTable.Add("WhiteToListMax");
			this.id42_BlockListDomainMax = base.Reader.NameTable.Add("BlockListDomainMax");
			this.id54_ReplyIndicator = base.Reader.NameTable.Add("ReplyIndicator");
			this.id27_StringWithVersionType = base.Reader.NameTable.Add("StringWithVersionType");
			this.id99_RulesResponseType = base.Reader.NameTable.Add("RulesResponseType");
			this.id9_Get = base.Reader.NameTable.Add("Get");
			this.id44_WhiteListDomainMax = base.Reader.NameTable.Add("WhiteListDomainMax");
			this.id31_ParentalControlStatus = base.Reader.NameTable.Add("ParentalControlStatus");
			this.id96_MinVacationResponseStartTime = base.Reader.NameTable.Add("MinVacationResponseStartTime");
			this.id39_MaxRecipients = base.Reader.NameTable.Add("MaxRecipients");
			this.id75_MoveToFolder = base.Reader.NameTable.Add("MoveToFolder");
			this.id24_StatusType = base.Reader.NameTable.Add("StatusType");
			this.id69_LocalPart = base.Reader.NameTable.Add("LocalPart");
			this.id8_AccountSettings = base.Reader.NameTable.Add("AccountSettings");
			this.id47_MaxDailySendMessages = base.Reader.NameTable.Add("MaxDailySendMessages");
			this.id43_WhiteListAddressMax = base.Reader.NameTable.Add("WhiteListAddressMax");
			this.id85_MinSyncPollInterval = base.Reader.NameTable.Add("MinSyncPollInterval");
			this.id88_MaxObjectsInSync = base.Reader.NameTable.Add("MaxObjectsInSync");
			this.id60_Mode = base.Reader.NameTable.Add("Mode");
			this.id52_JunkLevel = base.Reader.NameTable.Add("JunkLevel");
			this.id16_Faultcode = base.Reader.NameTable.Add("Faultcode");
			this.id11_Filters = base.Reader.NameTable.Add("Filters");
			this.id26_Properties = base.Reader.NameTable.Add("Properties");
			this.id76_FolderId = base.Reader.NameTable.Add("FolderId");
			this.id94_Item = base.Reader.NameTable.Add("MaxTotalLengthOfForwardingAddresses");
			this.id82_SafetyActions = base.Reader.NameTable.Add("SafetyActions");
			this.id1_Settings = base.Reader.NameTable.Add("Settings");
			this.id6_AuthPolicy = base.Reader.NameTable.Add("AuthPolicy");
			this.id64_EndTime = base.Reader.NameTable.Add("EndTime");
			this.id84_ServiceTimeOut = base.Reader.NameTable.Add("ServiceTimeOut");
			this.id19_ListsSetResponseType = base.Reader.NameTable.Add("ListsSetResponseType");
			this.id72_RunWhen = base.Reader.NameTable.Add("RunWhen");
			this.id2_HMSETTINGS = base.Reader.NameTable.Add("HMSETTINGS:");
			this.id92_Item = base.Reader.NameTable.Add("DefaultStatelessEmailWindowSize");
			this.id68_LocalParts = base.Reader.NameTable.Add("LocalParts");
			this.id81_SafetyLevelRules = base.Reader.NameTable.Add("SafetyLevelRules");
			this.id41_BlockListAddressMax = base.Reader.NameTable.Add("BlockListAddressMax");
			this.id70_ExecutionOrder = base.Reader.NameTable.Add("ExecutionOrder");
			this.id51_IncludeOriginalInReply = base.Reader.NameTable.Add("IncludeOriginalInReply");
			this.id5_Fault = base.Reader.NameTable.Add("Fault");
			this.id100_Version = base.Reader.NameTable.Add("Version");
			this.id97_MaxVacationResponseEndTime = base.Reader.NameTable.Add("MaxVacationResponseEndTime");
			this.id58_VacationResponse = base.Reader.NameTable.Add("VacationResponse");
			this.id95_Item = base.Reader.NameTable.Add("MaxVacationResponseMessageLength");
			this.id71_Enabled = base.Reader.NameTable.Add("Enabled");
			this.id15_SettingsCategoryResponseType = base.Reader.NameTable.Add("SettingsCategoryResponseType");
			this.id12_Lists = base.Reader.NameTable.Add("Lists");
			this.id53_JunkMailDestination = base.Reader.NameTable.Add("JunkMailDestination");
			this.id101_SAP = base.Reader.NameTable.Add("SAP");
			this.id79_Operator = base.Reader.NameTable.Add("Operator");
			this.id86_MinSettingsPollInterval = base.Reader.NameTable.Add("MinSettingsPollInterval");
			this.id29_PropertiesType = base.Reader.NameTable.Add("PropertiesType");
			this.id56_SaveSentMail = base.Reader.NameTable.Add("SaveSentMail");
			this.id3_Item = base.Reader.NameTable.Add("");
			this.id87_SyncMultiplier = base.Reader.NameTable.Add("SyncMultiplier");
			this.id13_Options = base.Reader.NameTable.Add("Options");
			this.id73_Condition = base.Reader.NameTable.Add("Condition");
		}

		// Token: 0x04002A9F RID: 10911
		private string id17_Faultstring;

		// Token: 0x04002AA0 RID: 10912
		private string id32_MailBoxSize;

		// Token: 0x04002AA1 RID: 10913
		private string id65_Message;

		// Token: 0x04002AA2 RID: 10914
		private string id49_ConfirmSent;

		// Token: 0x04002AA3 RID: 10915
		private string id98_MaxNumberOfProvisionCommands;

		// Token: 0x04002AA4 RID: 10916
		private string id89_MaxNumberOfEmailAdds;

		// Token: 0x04002AA5 RID: 10917
		private string id23_Add;

		// Token: 0x04002AA6 RID: 10918
		private string id62_Address;

		// Token: 0x04002AA7 RID: 10919
		private string id37_MaxFilters;

		// Token: 0x04002AA8 RID: 10920
		private string id83_ServiceSettingsPropertiesType;

		// Token: 0x04002AA9 RID: 10921
		private string id35_MaxMessageSize;

		// Token: 0x04002AAA RID: 10922
		private string id91_MaxNumberOfStatelessObjects;

		// Token: 0x04002AAB RID: 10923
		private string id22_Delete;

		// Token: 0x04002AAC RID: 10924
		private string id57_UseReplyToAddress;

		// Token: 0x04002AAD RID: 10925
		private string id77_Clause;

		// Token: 0x04002AAE RID: 10926
		private string id93_MaxStatelessEmailWindowSize;

		// Token: 0x04002AAF RID: 10927
		private string id18_Detail;

		// Token: 0x04002AB0 RID: 10928
		private string id7_ServiceSettings;

		// Token: 0x04002AB1 RID: 10929
		private string id40_MaxUserSignatureLength;

		// Token: 0x04002AB2 RID: 10930
		private string id61_Addresses;

		// Token: 0x04002AB3 RID: 10931
		private string id59_MailForwarding;

		// Token: 0x04002AB4 RID: 10932
		private string id80_Value;

		// Token: 0x04002AB5 RID: 10933
		private string id74_Actions;

		// Token: 0x04002AB6 RID: 10934
		private string id67_Domain;

		// Token: 0x04002AB7 RID: 10935
		private string id28_version;

		// Token: 0x04002AB8 RID: 10936
		private string id25_Filter;

		// Token: 0x04002AB9 RID: 10937
		private string id48_OptionsType;

		// Token: 0x04002ABA RID: 10938
		private string id66_Domains;

		// Token: 0x04002ABB RID: 10939
		private string id55_ReplyToAddress;

		// Token: 0x04002ABC RID: 10940
		private string id36_MaxUnencodedMessageSize;

		// Token: 0x04002ABD RID: 10941
		private string id90_MaxNumberOfFolderAdds;

		// Token: 0x04002ABE RID: 10942
		private string id21_name;

		// Token: 0x04002ABF RID: 10943
		private string id4_Status;

		// Token: 0x04002AC0 RID: 10944
		private string id50_HeaderDisplay;

		// Token: 0x04002AC1 RID: 10945
		private string id20_List;

		// Token: 0x04002AC2 RID: 10946
		private string id34_MaxAttachments;

		// Token: 0x04002AC3 RID: 10947
		private string id33_MaxMailBoxSize;

		// Token: 0x04002AC4 RID: 10948
		private string id78_Field;

		// Token: 0x04002AC5 RID: 10949
		private string id63_StartTime;

		// Token: 0x04002AC6 RID: 10950
		private string id14_UserSignature;

		// Token: 0x04002AC7 RID: 10951
		private string id10_Set;

		// Token: 0x04002AC8 RID: 10952
		private string id38_MaxFilterClauseValueLength;

		// Token: 0x04002AC9 RID: 10953
		private string id30_AccountStatus;

		// Token: 0x04002ACA RID: 10954
		private string id46_AlternateFromListMax;

		// Token: 0x04002ACB RID: 10955
		private string id45_WhiteToListMax;

		// Token: 0x04002ACC RID: 10956
		private string id42_BlockListDomainMax;

		// Token: 0x04002ACD RID: 10957
		private string id54_ReplyIndicator;

		// Token: 0x04002ACE RID: 10958
		private string id27_StringWithVersionType;

		// Token: 0x04002ACF RID: 10959
		private string id99_RulesResponseType;

		// Token: 0x04002AD0 RID: 10960
		private string id9_Get;

		// Token: 0x04002AD1 RID: 10961
		private string id44_WhiteListDomainMax;

		// Token: 0x04002AD2 RID: 10962
		private string id31_ParentalControlStatus;

		// Token: 0x04002AD3 RID: 10963
		private string id96_MinVacationResponseStartTime;

		// Token: 0x04002AD4 RID: 10964
		private string id39_MaxRecipients;

		// Token: 0x04002AD5 RID: 10965
		private string id75_MoveToFolder;

		// Token: 0x04002AD6 RID: 10966
		private string id24_StatusType;

		// Token: 0x04002AD7 RID: 10967
		private string id69_LocalPart;

		// Token: 0x04002AD8 RID: 10968
		private string id8_AccountSettings;

		// Token: 0x04002AD9 RID: 10969
		private string id47_MaxDailySendMessages;

		// Token: 0x04002ADA RID: 10970
		private string id43_WhiteListAddressMax;

		// Token: 0x04002ADB RID: 10971
		private string id85_MinSyncPollInterval;

		// Token: 0x04002ADC RID: 10972
		private string id88_MaxObjectsInSync;

		// Token: 0x04002ADD RID: 10973
		private string id60_Mode;

		// Token: 0x04002ADE RID: 10974
		private string id52_JunkLevel;

		// Token: 0x04002ADF RID: 10975
		private string id16_Faultcode;

		// Token: 0x04002AE0 RID: 10976
		private string id11_Filters;

		// Token: 0x04002AE1 RID: 10977
		private string id26_Properties;

		// Token: 0x04002AE2 RID: 10978
		private string id76_FolderId;

		// Token: 0x04002AE3 RID: 10979
		private string id94_Item;

		// Token: 0x04002AE4 RID: 10980
		private string id82_SafetyActions;

		// Token: 0x04002AE5 RID: 10981
		private string id1_Settings;

		// Token: 0x04002AE6 RID: 10982
		private string id6_AuthPolicy;

		// Token: 0x04002AE7 RID: 10983
		private string id64_EndTime;

		// Token: 0x04002AE8 RID: 10984
		private string id84_ServiceTimeOut;

		// Token: 0x04002AE9 RID: 10985
		private string id19_ListsSetResponseType;

		// Token: 0x04002AEA RID: 10986
		private string id72_RunWhen;

		// Token: 0x04002AEB RID: 10987
		private string id2_HMSETTINGS;

		// Token: 0x04002AEC RID: 10988
		private string id92_Item;

		// Token: 0x04002AED RID: 10989
		private string id68_LocalParts;

		// Token: 0x04002AEE RID: 10990
		private string id81_SafetyLevelRules;

		// Token: 0x04002AEF RID: 10991
		private string id41_BlockListAddressMax;

		// Token: 0x04002AF0 RID: 10992
		private string id70_ExecutionOrder;

		// Token: 0x04002AF1 RID: 10993
		private string id51_IncludeOriginalInReply;

		// Token: 0x04002AF2 RID: 10994
		private string id5_Fault;

		// Token: 0x04002AF3 RID: 10995
		private string id100_Version;

		// Token: 0x04002AF4 RID: 10996
		private string id97_MaxVacationResponseEndTime;

		// Token: 0x04002AF5 RID: 10997
		private string id58_VacationResponse;

		// Token: 0x04002AF6 RID: 10998
		private string id95_Item;

		// Token: 0x04002AF7 RID: 10999
		private string id71_Enabled;

		// Token: 0x04002AF8 RID: 11000
		private string id15_SettingsCategoryResponseType;

		// Token: 0x04002AF9 RID: 11001
		private string id12_Lists;

		// Token: 0x04002AFA RID: 11002
		private string id53_JunkMailDestination;

		// Token: 0x04002AFB RID: 11003
		private string id101_SAP;

		// Token: 0x04002AFC RID: 11004
		private string id79_Operator;

		// Token: 0x04002AFD RID: 11005
		private string id86_MinSettingsPollInterval;

		// Token: 0x04002AFE RID: 11006
		private string id29_PropertiesType;

		// Token: 0x04002AFF RID: 11007
		private string id56_SaveSentMail;

		// Token: 0x04002B00 RID: 11008
		private string id3_Item;

		// Token: 0x04002B01 RID: 11009
		private string id87_SyncMultiplier;

		// Token: 0x04002B02 RID: 11010
		private string id13_Options;

		// Token: 0x04002B03 RID: 11011
		private string id73_Condition;
	}
}
