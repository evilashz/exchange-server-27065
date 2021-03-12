﻿using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008CF RID: 2255
	internal class XmlSerializationReaderSettings : XmlSerializationReader
	{
		// Token: 0x06003056 RID: 12374 RVA: 0x0006E64C File Offset: 0x0006C84C
		public object Read33_Settings()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_Settings || base.Reader.NamespaceURI != this.id2_HMSETTINGS)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read32_Settings(false, true);
			}
			else
			{
				base.UnknownNode(null, "HMSETTINGS::Settings");
			}
			return result;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x0006E6BC File Offset: 0x0006C8BC
		private Settings Read32_Settings(bool isNullable, bool checkType)
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
			bool[] array = new bool[2];
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
					if (!array[0] && base.Reader.LocalName == this.id4_ServiceSettings && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settings.ServiceSettings = this.Read6_ServiceSettingsType(false, true);
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id5_AccountSettings && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						settings.AccountSettings = this.Read31_AccountSettingsType(false, true);
						array[1] = true;
					}
					else
					{
						base.UnknownNode(settings, "HMSETTINGS::ServiceSettings, HMSETTINGS::AccountSettings");
					}
				}
				else
				{
					base.UnknownNode(settings, "HMSETTINGS::ServiceSettings, HMSETTINGS::AccountSettings");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return settings;
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x0006E880 File Offset: 0x0006CA80
		private AccountSettingsType Read31_AccountSettingsType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id6_AccountSettingsType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			AccountSettingsType accountSettingsType = new AccountSettingsType();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(accountSettingsType);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return accountSettingsType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id7_Set && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						accountSettingsType.Item = this.Read29_AccountSettingsTypeSet(false, true);
						array[0] = true;
					}
					else if (!array[0] && base.Reader.LocalName == this.id8_Get && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						accountSettingsType.Item = this.Read30_AccountSettingsTypeGet(false, true);
						array[0] = true;
					}
					else
					{
						base.UnknownNode(accountSettingsType, "HMSETTINGS::Set, HMSETTINGS::Get");
					}
				}
				else
				{
					base.UnknownNode(accountSettingsType, "HMSETTINGS::Set, HMSETTINGS::Get");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return accountSettingsType;
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x0006EA44 File Offset: 0x0006CC44
		private AccountSettingsTypeGet Read30_AccountSettingsTypeGet(bool isNullable, bool checkType)
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
			AccountSettingsTypeGet accountSettingsTypeGet = new AccountSettingsTypeGet();
			bool[] array = new bool[5];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(accountSettingsTypeGet);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return accountSettingsTypeGet;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id9_Filters && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						accountSettingsTypeGet.Filters = this.Read1_Object(false, true);
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id10_Lists && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						accountSettingsTypeGet.Lists = this.Read1_Object(false, true);
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id11_Options && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						accountSettingsTypeGet.Options = this.Read1_Object(false, true);
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id12_Properties && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						accountSettingsTypeGet.Properties = this.Read1_Object(false, true);
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id13_UserSignature && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						accountSettingsTypeGet.UserSignature = this.Read1_Object(false, true);
						array[4] = true;
					}
					else
					{
						base.UnknownNode(accountSettingsTypeGet, "HMSETTINGS::Filters, HMSETTINGS::Lists, HMSETTINGS::Options, HMSETTINGS::Properties, HMSETTINGS::UserSignature");
					}
				}
				else
				{
					base.UnknownNode(accountSettingsTypeGet, "HMSETTINGS::Filters, HMSETTINGS::Lists, HMSETTINGS::Options, HMSETTINGS::Properties, HMSETTINGS::UserSignature");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return accountSettingsTypeGet;
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x0006ECD0 File Offset: 0x0006CED0
		private object Read1_Object(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType)
			{
				if (flag)
				{
					if (xmlQualifiedName != null)
					{
						return base.ReadTypedNull(xmlQualifiedName);
					}
					return null;
				}
				else
				{
					if (xmlQualifiedName == null)
					{
						return base.ReadTypedPrimitive(new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema"));
					}
					if (xmlQualifiedName.Name == this.id6_AccountSettingsType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						return this.Read31_AccountSettingsType(isNullable, false);
					}
					if (xmlQualifiedName.Name == this.id14_OptionsType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						return this.Read28_OptionsType(isNullable, false);
					}
					if (xmlQualifiedName.Name == this.id15_AddressesAndDomainsType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						return this.Read17_AddressesAndDomainsType(isNullable, false);
					}
					if (xmlQualifiedName.Name == this.id16_StringWithCharSetType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						return this.Read10_StringWithCharSetType(isNullable, false);
					}
					if (xmlQualifiedName.Name == this.id17_ServiceSettingsType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						return this.Read6_ServiceSettingsType(isNullable, false);
					}
					if (xmlQualifiedName.Name == this.id18_RunWhenType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						base.Reader.ReadStartElement();
						object result = this.Read7_RunWhenType(base.CollapseWhitespace(base.Reader.ReadString()));
						base.ReadEndElement();
						return result;
					}
					if (xmlQualifiedName.Name == this.id19_FilterKeyType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						base.Reader.ReadStartElement();
						object result2 = this.Read8_FilterKeyType(base.CollapseWhitespace(base.Reader.ReadString()));
						base.ReadEndElement();
						return result2;
					}
					if (xmlQualifiedName.Name == this.id20_FilterOperatorType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						base.Reader.ReadStartElement();
						object result3 = this.Read9_FilterOperatorType(base.CollapseWhitespace(base.Reader.ReadString()));
						base.ReadEndElement();
						return result3;
					}
					if (xmlQualifiedName.Name == this.id21_Item && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						FiltersRequestTypeFilter[] result4 = null;
						if (!base.ReadNull())
						{
							FiltersRequestTypeFilter[] array = null;
							int num = 0;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num2 = 0;
								int readerCount = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id22_Filter && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array = (FiltersRequestTypeFilter[])base.EnsureArrayIndex(array, num, typeof(FiltersRequestTypeFilter));
											array[num++] = this.Read15_FiltersRequestTypeFilter(false, true);
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
									base.CheckReaderCount(ref num2, ref readerCount);
								}
								base.ReadEndElement();
							}
							result4 = (FiltersRequestTypeFilter[])base.ShrinkArray(array, num, typeof(FiltersRequestTypeFilter), false);
						}
						return result4;
					}
					if (xmlQualifiedName.Name == this.id23_ItemsChoiceType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						base.Reader.ReadStartElement();
						object result5 = this.Read16_ItemsChoiceType(base.CollapseWhitespace(base.Reader.ReadString()));
						base.ReadEndElement();
						return result5;
					}
					if (xmlQualifiedName.Name == this.id24_ArrayOfString && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						string[] result6 = null;
						if (!base.ReadNull())
						{
							string[] array2 = null;
							int num3 = 0;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num4 = 0;
								int readerCount2 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id25_Address && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array2 = (string[])base.EnsureArrayIndex(array2, num3, typeof(string));
											array2[num3++] = base.Reader.ReadElementString();
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
									base.CheckReaderCount(ref num4, ref readerCount2);
								}
								base.ReadEndElement();
							}
							result6 = (string[])base.ShrinkArray(array2, num3, typeof(string), false);
						}
						return result6;
					}
					if (xmlQualifiedName.Name == this.id26_ArrayOfString1 && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						string[] result7 = null;
						if (!base.ReadNull())
						{
							string[] array3 = null;
							int num5 = 0;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num6 = 0;
								int readerCount3 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id27_Domain && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array3 = (string[])base.EnsureArrayIndex(array3, num5, typeof(string));
											array3[num5++] = base.Reader.ReadElementString();
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
									base.CheckReaderCount(ref num6, ref readerCount3);
								}
								base.ReadEndElement();
							}
							result7 = (string[])base.ShrinkArray(array3, num5, typeof(string), false);
						}
						return result7;
					}
					if (xmlQualifiedName.Name == this.id28_ArrayOfListsRequestTypeList && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						ListsRequestTypeList[] result8 = null;
						if (!base.ReadNull())
						{
							ListsRequestTypeList[] array4 = null;
							int num7 = 0;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num8 = 0;
								int readerCount4 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id29_List && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array4 = (ListsRequestTypeList[])base.EnsureArrayIndex(array4, num7, typeof(ListsRequestTypeList));
											array4[num7++] = this.Read18_ListsRequestTypeList(false, true);
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
									base.CheckReaderCount(ref num8, ref readerCount4);
								}
								base.ReadEndElement();
							}
							result8 = (ListsRequestTypeList[])base.ShrinkArray(array4, num7, typeof(ListsRequestTypeList), false);
						}
						return result8;
					}
					if (xmlQualifiedName.Name == this.id30_HeaderDisplayType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						base.Reader.ReadStartElement();
						object result9 = this.Read19_HeaderDisplayType(base.CollapseWhitespace(base.Reader.ReadString()));
						base.ReadEndElement();
						return result9;
					}
					if (xmlQualifiedName.Name == this.id31_IncludeOriginalInReplyType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						base.Reader.ReadStartElement();
						object result10 = this.Read20_IncludeOriginalInReplyType(base.CollapseWhitespace(base.Reader.ReadString()));
						base.ReadEndElement();
						return result10;
					}
					if (xmlQualifiedName.Name == this.id32_JunkLevelType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						base.Reader.ReadStartElement();
						object result11 = this.Read21_JunkLevelType(base.CollapseWhitespace(base.Reader.ReadString()));
						base.ReadEndElement();
						return result11;
					}
					if (xmlQualifiedName.Name == this.id33_JunkMailDestinationType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						base.Reader.ReadStartElement();
						object result12 = this.Read22_JunkMailDestinationType(base.CollapseWhitespace(base.Reader.ReadString()));
						base.ReadEndElement();
						return result12;
					}
					if (xmlQualifiedName.Name == this.id34_ReplyIndicatorType && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						base.Reader.ReadStartElement();
						object result13 = this.Read23_ReplyIndicatorType(base.CollapseWhitespace(base.Reader.ReadString()));
						base.ReadEndElement();
						return result13;
					}
					if (xmlQualifiedName.Name == this.id35_VacationResponseMode && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						base.Reader.ReadStartElement();
						object result14 = this.Read24_VacationResponseMode(base.CollapseWhitespace(base.Reader.ReadString()));
						base.ReadEndElement();
						return result14;
					}
					if (xmlQualifiedName.Name == this.id36_ForwardingMode && xmlQualifiedName.Namespace == this.id2_HMSETTINGS)
					{
						base.Reader.ReadStartElement();
						object result15 = this.Read26_ForwardingMode(base.CollapseWhitespace(base.Reader.ReadString()));
						base.ReadEndElement();
						return result15;
					}
					return base.ReadTypedPrimitive(xmlQualifiedName);
				}
			}
			else
			{
				if (flag)
				{
					return null;
				}
				object obj = new object();
				while (base.Reader.MoveToNextAttribute())
				{
					if (!base.IsXmlnsAttribute(base.Reader.Name))
					{
						base.UnknownNode(obj);
					}
				}
				base.Reader.MoveToElement();
				if (base.Reader.IsEmptyElement)
				{
					base.Reader.Skip();
					return obj;
				}
				base.Reader.ReadStartElement();
				base.Reader.MoveToContent();
				int num9 = 0;
				int readerCount5 = base.ReaderCount;
				while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
				{
					if (base.Reader.NodeType == XmlNodeType.Element)
					{
						base.UnknownNode(obj, "");
					}
					else
					{
						base.UnknownNode(obj, "");
					}
					base.Reader.MoveToContent();
					base.CheckReaderCount(ref num9, ref readerCount5);
				}
				base.ReadEndElement();
				return obj;
			}
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x0006F778 File Offset: 0x0006D978
		private ForwardingMode Read26_ForwardingMode(string s)
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

		// Token: 0x0600305C RID: 12380 RVA: 0x0006F7CC File Offset: 0x0006D9CC
		private VacationResponseMode Read24_VacationResponseMode(string s)
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

		// Token: 0x0600305D RID: 12381 RVA: 0x0006F820 File Offset: 0x0006DA20
		private ReplyIndicatorType Read23_ReplyIndicatorType(string s)
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

		// Token: 0x0600305E RID: 12382 RVA: 0x0006F874 File Offset: 0x0006DA74
		private JunkMailDestinationType Read22_JunkMailDestinationType(string s)
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

		// Token: 0x0600305F RID: 12383 RVA: 0x0006F8B8 File Offset: 0x0006DAB8
		private JunkLevelType Read21_JunkLevelType(string s)
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

		// Token: 0x06003060 RID: 12384 RVA: 0x0006F91C File Offset: 0x0006DB1C
		private IncludeOriginalInReplyType Read20_IncludeOriginalInReplyType(string s)
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

		// Token: 0x06003061 RID: 12385 RVA: 0x0006F960 File Offset: 0x0006DB60
		private HeaderDisplayType Read19_HeaderDisplayType(string s)
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

		// Token: 0x06003062 RID: 12386 RVA: 0x0006F9C4 File Offset: 0x0006DBC4
		private ListsRequestTypeList Read18_ListsRequestTypeList(bool isNullable, bool checkType)
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
			ListsRequestTypeList listsRequestTypeList = new ListsRequestTypeList();
			AddressesAndDomainsType[] array = null;
			int num = 0;
			ItemsChoiceType[] array2 = null;
			int num2 = 0;
			bool[] array3 = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array3[1] && base.Reader.LocalName == this.id37_name && base.Reader.NamespaceURI == this.id3_Item)
				{
					listsRequestTypeList.name = base.Reader.Value;
					array3[1] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(listsRequestTypeList, ":name");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				listsRequestTypeList.Items = (AddressesAndDomainsType[])base.ShrinkArray(array, num, typeof(AddressesAndDomainsType), true);
				listsRequestTypeList.ItemsElementName = (ItemsChoiceType[])base.ShrinkArray(array2, num2, typeof(ItemsChoiceType), true);
				return listsRequestTypeList;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num3 = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (base.Reader.LocalName == this.id38_Add && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						array = (AddressesAndDomainsType[])base.EnsureArrayIndex(array, num, typeof(AddressesAndDomainsType));
						array[num++] = this.Read17_AddressesAndDomainsType(false, true);
						array2 = (ItemsChoiceType[])base.EnsureArrayIndex(array2, num2, typeof(ItemsChoiceType));
						array2[num2++] = ItemsChoiceType.Add;
					}
					else if (base.Reader.LocalName == this.id39_Delete && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						array = (AddressesAndDomainsType[])base.EnsureArrayIndex(array, num, typeof(AddressesAndDomainsType));
						array[num++] = this.Read17_AddressesAndDomainsType(false, true);
						array2 = (ItemsChoiceType[])base.EnsureArrayIndex(array2, num2, typeof(ItemsChoiceType));
						array2[num2++] = ItemsChoiceType.Delete;
					}
					else if (base.Reader.LocalName == this.id7_Set && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						array = (AddressesAndDomainsType[])base.EnsureArrayIndex(array, num, typeof(AddressesAndDomainsType));
						array[num++] = this.Read17_AddressesAndDomainsType(false, true);
						array2 = (ItemsChoiceType[])base.EnsureArrayIndex(array2, num2, typeof(ItemsChoiceType));
						array2[num2++] = ItemsChoiceType.Set;
					}
					else
					{
						base.UnknownNode(listsRequestTypeList, "HMSETTINGS::Add, HMSETTINGS::Delete, HMSETTINGS::Set");
					}
				}
				else
				{
					base.UnknownNode(listsRequestTypeList, "HMSETTINGS::Add, HMSETTINGS::Delete, HMSETTINGS::Set");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num3, ref readerCount);
			}
			listsRequestTypeList.Items = (AddressesAndDomainsType[])base.ShrinkArray(array, num, typeof(AddressesAndDomainsType), true);
			listsRequestTypeList.ItemsElementName = (ItemsChoiceType[])base.ShrinkArray(array2, num2, typeof(ItemsChoiceType), true);
			base.ReadEndElement();
			return listsRequestTypeList;
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x0006FD4C File Offset: 0x0006DF4C
		private AddressesAndDomainsType Read17_AddressesAndDomainsType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id15_AddressesAndDomainsType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			AddressesAndDomainsType addressesAndDomainsType = new AddressesAndDomainsType();
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(addressesAndDomainsType);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return addressesAndDomainsType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (base.Reader.LocalName == this.id40_Addresses && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						if (!base.ReadNull())
						{
							string[] array = null;
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
										if (base.Reader.LocalName == this.id25_Address && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array = (string[])base.EnsureArrayIndex(array, num2, typeof(string));
											array[num2++] = base.Reader.ReadElementString();
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
							addressesAndDomainsType.Addresses = (string[])base.ShrinkArray(array, num2, typeof(string), false);
						}
					}
					else if (base.Reader.LocalName == this.id41_Domains && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						if (!base.ReadNull())
						{
							string[] array2 = null;
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
										if (base.Reader.LocalName == this.id27_Domain && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array2 = (string[])base.EnsureArrayIndex(array2, num4, typeof(string));
											array2[num4++] = base.Reader.ReadElementString();
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
							addressesAndDomainsType.Domains = (string[])base.ShrinkArray(array2, num4, typeof(string), false);
						}
					}
					else
					{
						base.UnknownNode(addressesAndDomainsType, "HMSETTINGS::Addresses, HMSETTINGS::Domains");
					}
				}
				else
				{
					base.UnknownNode(addressesAndDomainsType, "HMSETTINGS::Addresses, HMSETTINGS::Domains");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return addressesAndDomainsType;
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x00070148 File Offset: 0x0006E348
		private ItemsChoiceType Read16_ItemsChoiceType(string s)
		{
			if (s != null)
			{
				if (s == "Add")
				{
					return ItemsChoiceType.Add;
				}
				if (s == "Delete")
				{
					return ItemsChoiceType.Delete;
				}
				if (s == "Set")
				{
					return ItemsChoiceType.Set;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(ItemsChoiceType));
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x0007019C File Offset: 0x0006E39C
		private FiltersRequestTypeFilter Read15_FiltersRequestTypeFilter(bool isNullable, bool checkType)
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
			FiltersRequestTypeFilter filtersRequestTypeFilter = new FiltersRequestTypeFilter();
			bool[] array = new bool[5];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(filtersRequestTypeFilter);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return filtersRequestTypeFilter;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id42_ExecutionOrder && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersRequestTypeFilter.ExecutionOrder = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id43_Enabled && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersRequestTypeFilter.Enabled = XmlConvert.ToByte(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id44_RunWhen && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersRequestTypeFilter.RunWhen = this.Read7_RunWhenType(base.Reader.ReadElementString());
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id45_Condition && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersRequestTypeFilter.Condition = this.Read12_Item(false, true);
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id46_Actions && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersRequestTypeFilter.Actions = this.Read14_Item(false, true);
						array[4] = true;
					}
					else
					{
						base.UnknownNode(filtersRequestTypeFilter, "HMSETTINGS::ExecutionOrder, HMSETTINGS::Enabled, HMSETTINGS::RunWhen, HMSETTINGS::Condition, HMSETTINGS::Actions");
					}
				}
				else
				{
					base.UnknownNode(filtersRequestTypeFilter, "HMSETTINGS::ExecutionOrder, HMSETTINGS::Enabled, HMSETTINGS::RunWhen, HMSETTINGS::Condition, HMSETTINGS::Actions");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return filtersRequestTypeFilter;
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x00070440 File Offset: 0x0006E640
		private FiltersRequestTypeFilterActions Read14_Item(bool isNullable, bool checkType)
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
			FiltersRequestTypeFilterActions filtersRequestTypeFilterActions = new FiltersRequestTypeFilterActions();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(filtersRequestTypeFilterActions);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return filtersRequestTypeFilterActions;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id47_MoveToFolder && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersRequestTypeFilterActions.MoveToFolder = this.Read13_Item(false, true);
						array[0] = true;
					}
					else
					{
						base.UnknownNode(filtersRequestTypeFilterActions, "HMSETTINGS::MoveToFolder");
					}
				}
				else
				{
					base.UnknownNode(filtersRequestTypeFilterActions, "HMSETTINGS::MoveToFolder");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return filtersRequestTypeFilterActions;
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000705C0 File Offset: 0x0006E7C0
		private FiltersRequestTypeFilterActionsMoveToFolder Read13_Item(bool isNullable, bool checkType)
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
			FiltersRequestTypeFilterActionsMoveToFolder filtersRequestTypeFilterActionsMoveToFolder = new FiltersRequestTypeFilterActionsMoveToFolder();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(filtersRequestTypeFilterActionsMoveToFolder);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return filtersRequestTypeFilterActionsMoveToFolder;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id48_FolderId && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersRequestTypeFilterActionsMoveToFolder.FolderId = base.Reader.ReadElementString();
						array[0] = true;
					}
					else
					{
						base.UnknownNode(filtersRequestTypeFilterActionsMoveToFolder, "HMSETTINGS::FolderId");
					}
				}
				else
				{
					base.UnknownNode(filtersRequestTypeFilterActionsMoveToFolder, "HMSETTINGS::FolderId");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return filtersRequestTypeFilterActionsMoveToFolder;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x00070744 File Offset: 0x0006E944
		private FiltersRequestTypeFilterCondition Read12_Item(bool isNullable, bool checkType)
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
			FiltersRequestTypeFilterCondition filtersRequestTypeFilterCondition = new FiltersRequestTypeFilterCondition();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(filtersRequestTypeFilterCondition);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return filtersRequestTypeFilterCondition;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id49_Clause && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersRequestTypeFilterCondition.Clause = this.Read11_Item(false, true);
						array[0] = true;
					}
					else
					{
						base.UnknownNode(filtersRequestTypeFilterCondition, "HMSETTINGS::Clause");
					}
				}
				else
				{
					base.UnknownNode(filtersRequestTypeFilterCondition, "HMSETTINGS::Clause");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return filtersRequestTypeFilterCondition;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000708C4 File Offset: 0x0006EAC4
		private FiltersRequestTypeFilterConditionClause Read11_Item(bool isNullable, bool checkType)
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
			FiltersRequestTypeFilterConditionClause filtersRequestTypeFilterConditionClause = new FiltersRequestTypeFilterConditionClause();
			bool[] array = new bool[3];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(filtersRequestTypeFilterConditionClause);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return filtersRequestTypeFilterConditionClause;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id50_Field && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersRequestTypeFilterConditionClause.Field = this.Read8_FilterKeyType(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id51_Operator && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersRequestTypeFilterConditionClause.Operator = this.Read9_FilterOperatorType(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id52_Value && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						filtersRequestTypeFilterConditionClause.Value = this.Read10_StringWithCharSetType(false, true);
						array[2] = true;
					}
					else
					{
						base.UnknownNode(filtersRequestTypeFilterConditionClause, "HMSETTINGS::Field, HMSETTINGS::Operator, HMSETTINGS::Value");
					}
				}
				else
				{
					base.UnknownNode(filtersRequestTypeFilterConditionClause, "HMSETTINGS::Field, HMSETTINGS::Operator, HMSETTINGS::Value");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return filtersRequestTypeFilterConditionClause;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x00070ADC File Offset: 0x0006ECDC
		private StringWithCharSetType Read10_StringWithCharSetType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id16_StringWithCharSetType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			StringWithCharSetType stringWithCharSetType = new StringWithCharSetType();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id53_charset && base.Reader.NamespaceURI == this.id3_Item)
				{
					stringWithCharSetType.charset = base.Reader.Value;
					array[0] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(stringWithCharSetType, ":charset");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return stringWithCharSetType;
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
					base.UnknownNode(stringWithCharSetType, "");
				}
				else if (base.Reader.NodeType == XmlNodeType.Text || base.Reader.NodeType == XmlNodeType.CDATA || base.Reader.NodeType == XmlNodeType.Whitespace || base.Reader.NodeType == XmlNodeType.SignificantWhitespace)
				{
					value = base.ReadString(value, false);
					stringWithCharSetType.Value = value;
				}
				else
				{
					base.UnknownNode(stringWithCharSetType, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return stringWithCharSetType;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x00070CB8 File Offset: 0x0006EEB8
		private FilterOperatorType Read9_FilterOperatorType(string s)
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

		// Token: 0x0600306C RID: 12396 RVA: 0x00070D7C File Offset: 0x0006EF7C
		private FilterKeyType Read8_FilterKeyType(string s)
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

		// Token: 0x0600306D RID: 12397 RVA: 0x00070DE0 File Offset: 0x0006EFE0
		private RunWhenType Read7_RunWhenType(string s)
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

		// Token: 0x0600306E RID: 12398 RVA: 0x00070E24 File Offset: 0x0006F024
		private ServiceSettingsType Read6_ServiceSettingsType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id17_ServiceSettingsType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ServiceSettingsType serviceSettingsType = new ServiceSettingsType();
			bool[] array = new bool[5];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(serviceSettingsType);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return serviceSettingsType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id54_SafetySchemaVersion && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsType.SafetySchemaVersion = base.Reader.ReadElementString();
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id55_SafetyLevelRules && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsType.SafetyLevelRules = this.Read2_Item(false, true);
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id56_SafetyActions && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsType.SafetyActions = this.Read3_Item(false, true);
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id12_Properties && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsType.Properties = this.Read4_ServiceSettingsTypeProperties(false, true);
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id10_Lists && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsType.Lists = this.Read5_ServiceSettingsTypeLists(false, true);
						array[4] = true;
					}
					else
					{
						base.UnknownNode(serviceSettingsType, "HMSETTINGS::SafetySchemaVersion, HMSETTINGS::SafetyLevelRules, HMSETTINGS::SafetyActions, HMSETTINGS::Properties, HMSETTINGS::Lists");
					}
				}
				else
				{
					base.UnknownNode(serviceSettingsType, "HMSETTINGS::SafetySchemaVersion, HMSETTINGS::SafetyLevelRules, HMSETTINGS::SafetyActions, HMSETTINGS::Properties, HMSETTINGS::Lists");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return serviceSettingsType;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000710B0 File Offset: 0x0006F2B0
		private ServiceSettingsTypeLists Read5_ServiceSettingsTypeLists(bool isNullable, bool checkType)
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
			ServiceSettingsTypeLists serviceSettingsTypeLists = new ServiceSettingsTypeLists();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(serviceSettingsTypeLists);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return serviceSettingsTypeLists;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id8_Get && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsTypeLists.Get = this.Read1_Object(false, true);
						array[0] = true;
					}
					else
					{
						base.UnknownNode(serviceSettingsTypeLists, "HMSETTINGS::Get");
					}
				}
				else
				{
					base.UnknownNode(serviceSettingsTypeLists, "HMSETTINGS::Get");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return serviceSettingsTypeLists;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x00071230 File Offset: 0x0006F430
		private ServiceSettingsTypeProperties Read4_ServiceSettingsTypeProperties(bool isNullable, bool checkType)
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
			ServiceSettingsTypeProperties serviceSettingsTypeProperties = new ServiceSettingsTypeProperties();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(serviceSettingsTypeProperties);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return serviceSettingsTypeProperties;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id8_Get && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsTypeProperties.Get = this.Read1_Object(false, true);
						array[0] = true;
					}
					else
					{
						base.UnknownNode(serviceSettingsTypeProperties, "HMSETTINGS::Get");
					}
				}
				else
				{
					base.UnknownNode(serviceSettingsTypeProperties, "HMSETTINGS::Get");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return serviceSettingsTypeProperties;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000713B0 File Offset: 0x0006F5B0
		private ServiceSettingsTypeSafetyActions Read3_Item(bool isNullable, bool checkType)
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
			ServiceSettingsTypeSafetyActions serviceSettingsTypeSafetyActions = new ServiceSettingsTypeSafetyActions();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(serviceSettingsTypeSafetyActions);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return serviceSettingsTypeSafetyActions;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id57_GetVersion && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsTypeSafetyActions.GetVersion = this.Read1_Object(false, true);
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id8_Get && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsTypeSafetyActions.Get = this.Read1_Object(false, true);
						array[1] = true;
					}
					else
					{
						base.UnknownNode(serviceSettingsTypeSafetyActions, "HMSETTINGS::GetVersion, HMSETTINGS::Get");
					}
				}
				else
				{
					base.UnknownNode(serviceSettingsTypeSafetyActions, "HMSETTINGS::GetVersion, HMSETTINGS::Get");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return serviceSettingsTypeSafetyActions;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x00071574 File Offset: 0x0006F774
		private ServiceSettingsTypeSafetyLevelRules Read2_Item(bool isNullable, bool checkType)
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
			ServiceSettingsTypeSafetyLevelRules serviceSettingsTypeSafetyLevelRules = new ServiceSettingsTypeSafetyLevelRules();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(serviceSettingsTypeSafetyLevelRules);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return serviceSettingsTypeSafetyLevelRules;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id57_GetVersion && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsTypeSafetyLevelRules.GetVersion = this.Read1_Object(false, true);
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id8_Get && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						serviceSettingsTypeSafetyLevelRules.Get = this.Read1_Object(false, true);
						array[1] = true;
					}
					else
					{
						base.UnknownNode(serviceSettingsTypeSafetyLevelRules, "HMSETTINGS::GetVersion, HMSETTINGS::Get");
					}
				}
				else
				{
					base.UnknownNode(serviceSettingsTypeSafetyLevelRules, "HMSETTINGS::GetVersion, HMSETTINGS::Get");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return serviceSettingsTypeSafetyLevelRules;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x00071738 File Offset: 0x0006F938
		private OptionsType Read28_OptionsType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id14_OptionsType || xmlQualifiedName.Namespace != this.id2_HMSETTINGS))
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
					if (!array[0] && base.Reader.LocalName == this.id58_ConfirmSent && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.ConfirmSent = XmlConvert.ToByte(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id59_HeaderDisplay && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.HeaderDisplay = this.Read19_HeaderDisplayType(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id60_IncludeOriginalInReply && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.IncludeOriginalInReply = this.Read20_IncludeOriginalInReplyType(base.Reader.ReadElementString());
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id61_JunkLevel && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.JunkLevel = this.Read21_JunkLevelType(base.Reader.ReadElementString());
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id62_JunkMailDestination && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.JunkMailDestination = this.Read22_JunkMailDestinationType(base.Reader.ReadElementString());
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id63_ReplyIndicator && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.ReplyIndicator = this.Read23_ReplyIndicatorType(base.Reader.ReadElementString());
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id64_ReplyToAddress && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.ReplyToAddress = base.Reader.ReadElementString();
						array[6] = true;
					}
					else if (!array[7] && base.Reader.LocalName == this.id65_SaveSentMail && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.SaveSentMail = XmlConvert.ToByte(base.Reader.ReadElementString());
						array[7] = true;
					}
					else if (!array[8] && base.Reader.LocalName == this.id66_UseReplyToAddress && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.UseReplyToAddress = XmlConvert.ToByte(base.Reader.ReadElementString());
						array[8] = true;
					}
					else if (!array[9] && base.Reader.LocalName == this.id67_VacationResponse && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.VacationResponse = this.Read25_OptionsTypeVacationResponse(false, true);
						array[9] = true;
					}
					else if (!array[10] && base.Reader.LocalName == this.id68_MailForwarding && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsType.MailForwarding = this.Read27_OptionsTypeMailForwarding(false, true);
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

		// Token: 0x06003074 RID: 12404 RVA: 0x00071B9C File Offset: 0x0006FD9C
		private OptionsTypeMailForwarding Read27_OptionsTypeMailForwarding(bool isNullable, bool checkType)
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
					if (!array[0] && base.Reader.LocalName == this.id69_Mode && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsTypeMailForwarding.Mode = this.Read26_ForwardingMode(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (base.Reader.LocalName == this.id40_Addresses && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
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
										if (base.Reader.LocalName == this.id25_Address && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
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

		// Token: 0x06003075 RID: 12405 RVA: 0x00071E8C File Offset: 0x0007008C
		private OptionsTypeVacationResponse Read25_OptionsTypeVacationResponse(bool isNullable, bool checkType)
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
					if (!array[0] && base.Reader.LocalName == this.id69_Mode && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsTypeVacationResponse.Mode = this.Read24_VacationResponseMode(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id70_StartTime && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsTypeVacationResponse.StartTime = base.Reader.ReadElementString();
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id71_EndTime && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						optionsTypeVacationResponse.EndTime = base.Reader.ReadElementString();
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id72_Message && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
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

		// Token: 0x06003076 RID: 12406 RVA: 0x000720E8 File Offset: 0x000702E8
		private AccountSettingsTypeSet Read29_AccountSettingsTypeSet(bool isNullable, bool checkType)
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
			AccountSettingsTypeSet accountSettingsTypeSet = new AccountSettingsTypeSet();
			bool[] array = new bool[4];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(accountSettingsTypeSet);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return accountSettingsTypeSet;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (base.Reader.LocalName == this.id9_Filters && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						if (!base.ReadNull())
						{
							FiltersRequestTypeFilter[] array2 = null;
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
										if (base.Reader.LocalName == this.id22_Filter && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array2 = (FiltersRequestTypeFilter[])base.EnsureArrayIndex(array2, num2, typeof(FiltersRequestTypeFilter));
											array2[num2++] = this.Read15_FiltersRequestTypeFilter(false, true);
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
							accountSettingsTypeSet.Filters = (FiltersRequestTypeFilter[])base.ShrinkArray(array2, num2, typeof(FiltersRequestTypeFilter), false);
						}
					}
					else if (base.Reader.LocalName == this.id10_Lists && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						if (!base.ReadNull())
						{
							ListsRequestTypeList[] array3 = null;
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
										if (base.Reader.LocalName == this.id29_List && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
										{
											array3 = (ListsRequestTypeList[])base.EnsureArrayIndex(array3, num4, typeof(ListsRequestTypeList));
											array3[num4++] = this.Read18_ListsRequestTypeList(false, true);
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
							accountSettingsTypeSet.Lists = (ListsRequestTypeList[])base.ShrinkArray(array3, num4, typeof(ListsRequestTypeList), false);
						}
					}
					else if (!array[2] && base.Reader.LocalName == this.id11_Options && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						accountSettingsTypeSet.Options = this.Read28_OptionsType(false, true);
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id13_UserSignature && base.Reader.NamespaceURI == this.id2_HMSETTINGS)
					{
						accountSettingsTypeSet.UserSignature = this.Read10_StringWithCharSetType(false, true);
						array[3] = true;
					}
					else
					{
						base.UnknownNode(accountSettingsTypeSet, "HMSETTINGS::Filters, HMSETTINGS::Lists, HMSETTINGS::Options, HMSETTINGS::UserSignature");
					}
				}
				else
				{
					base.UnknownNode(accountSettingsTypeSet, "HMSETTINGS::Filters, HMSETTINGS::Lists, HMSETTINGS::Options, HMSETTINGS::UserSignature");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return accountSettingsTypeSet;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x00072567 File Offset: 0x00070767
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x0007256C File Offset: 0x0007076C
		protected override void InitIDs()
		{
			this.id16_StringWithCharSetType = base.Reader.NameTable.Add("StringWithCharSetType");
			this.id32_JunkLevelType = base.Reader.NameTable.Add("JunkLevelType");
			this.id19_FilterKeyType = base.Reader.NameTable.Add("FilterKeyType");
			this.id49_Clause = base.Reader.NameTable.Add("Clause");
			this.id43_Enabled = base.Reader.NameTable.Add("Enabled");
			this.id61_JunkLevel = base.Reader.NameTable.Add("JunkLevel");
			this.id64_ReplyToAddress = base.Reader.NameTable.Add("ReplyToAddress");
			this.id42_ExecutionOrder = base.Reader.NameTable.Add("ExecutionOrder");
			this.id20_FilterOperatorType = base.Reader.NameTable.Add("FilterOperatorType");
			this.id63_ReplyIndicator = base.Reader.NameTable.Add("ReplyIndicator");
			this.id41_Domains = base.Reader.NameTable.Add("Domains");
			this.id17_ServiceSettingsType = base.Reader.NameTable.Add("ServiceSettingsType");
			this.id71_EndTime = base.Reader.NameTable.Add("EndTime");
			this.id29_List = base.Reader.NameTable.Add("List");
			this.id62_JunkMailDestination = base.Reader.NameTable.Add("JunkMailDestination");
			this.id34_ReplyIndicatorType = base.Reader.NameTable.Add("ReplyIndicatorType");
			this.id33_JunkMailDestinationType = base.Reader.NameTable.Add("JunkMailDestinationType");
			this.id5_AccountSettings = base.Reader.NameTable.Add("AccountSettings");
			this.id21_Item = base.Reader.NameTable.Add("ArrayOfFiltersRequestTypeFilter");
			this.id69_Mode = base.Reader.NameTable.Add("Mode");
			this.id9_Filters = base.Reader.NameTable.Add("Filters");
			this.id22_Filter = base.Reader.NameTable.Add("Filter");
			this.id58_ConfirmSent = base.Reader.NameTable.Add("ConfirmSent");
			this.id60_IncludeOriginalInReply = base.Reader.NameTable.Add("IncludeOriginalInReply");
			this.id39_Delete = base.Reader.NameTable.Add("Delete");
			this.id59_HeaderDisplay = base.Reader.NameTable.Add("HeaderDisplay");
			this.id45_Condition = base.Reader.NameTable.Add("Condition");
			this.id3_Item = base.Reader.NameTable.Add("");
			this.id13_UserSignature = base.Reader.NameTable.Add("UserSignature");
			this.id66_UseReplyToAddress = base.Reader.NameTable.Add("UseReplyToAddress");
			this.id51_Operator = base.Reader.NameTable.Add("Operator");
			this.id31_IncludeOriginalInReplyType = base.Reader.NameTable.Add("IncludeOriginalInReplyType");
			this.id72_Message = base.Reader.NameTable.Add("Message");
			this.id57_GetVersion = base.Reader.NameTable.Add("GetVersion");
			this.id10_Lists = base.Reader.NameTable.Add("Lists");
			this.id27_Domain = base.Reader.NameTable.Add("Domain");
			this.id54_SafetySchemaVersion = base.Reader.NameTable.Add("SafetySchemaVersion");
			this.id44_RunWhen = base.Reader.NameTable.Add("RunWhen");
			this.id47_MoveToFolder = base.Reader.NameTable.Add("MoveToFolder");
			this.id11_Options = base.Reader.NameTable.Add("Options");
			this.id52_Value = base.Reader.NameTable.Add("Value");
			this.id70_StartTime = base.Reader.NameTable.Add("StartTime");
			this.id53_charset = base.Reader.NameTable.Add("charset");
			this.id56_SafetyActions = base.Reader.NameTable.Add("SafetyActions");
			this.id50_Field = base.Reader.NameTable.Add("Field");
			this.id7_Set = base.Reader.NameTable.Add("Set");
			this.id37_name = base.Reader.NameTable.Add("name");
			this.id12_Properties = base.Reader.NameTable.Add("Properties");
			this.id65_SaveSentMail = base.Reader.NameTable.Add("SaveSentMail");
			this.id4_ServiceSettings = base.Reader.NameTable.Add("ServiceSettings");
			this.id67_VacationResponse = base.Reader.NameTable.Add("VacationResponse");
			this.id23_ItemsChoiceType = base.Reader.NameTable.Add("ItemsChoiceType");
			this.id18_RunWhenType = base.Reader.NameTable.Add("RunWhenType");
			this.id24_ArrayOfString = base.Reader.NameTable.Add("ArrayOfString");
			this.id28_ArrayOfListsRequestTypeList = base.Reader.NameTable.Add("ArrayOfListsRequestTypeList");
			this.id46_Actions = base.Reader.NameTable.Add("Actions");
			this.id36_ForwardingMode = base.Reader.NameTable.Add("ForwardingMode");
			this.id6_AccountSettingsType = base.Reader.NameTable.Add("AccountSettingsType");
			this.id8_Get = base.Reader.NameTable.Add("Get");
			this.id2_HMSETTINGS = base.Reader.NameTable.Add("HMSETTINGS:");
			this.id35_VacationResponseMode = base.Reader.NameTable.Add("VacationResponseMode");
			this.id30_HeaderDisplayType = base.Reader.NameTable.Add("HeaderDisplayType");
			this.id38_Add = base.Reader.NameTable.Add("Add");
			this.id48_FolderId = base.Reader.NameTable.Add("FolderId");
			this.id68_MailForwarding = base.Reader.NameTable.Add("MailForwarding");
			this.id25_Address = base.Reader.NameTable.Add("Address");
			this.id14_OptionsType = base.Reader.NameTable.Add("OptionsType");
			this.id15_AddressesAndDomainsType = base.Reader.NameTable.Add("AddressesAndDomainsType");
			this.id1_Settings = base.Reader.NameTable.Add("Settings");
			this.id26_ArrayOfString1 = base.Reader.NameTable.Add("ArrayOfString1");
			this.id40_Addresses = base.Reader.NameTable.Add("Addresses");
			this.id55_SafetyLevelRules = base.Reader.NameTable.Add("SafetyLevelRules");
		}

		// Token: 0x040029AA RID: 10666
		private string id16_StringWithCharSetType;

		// Token: 0x040029AB RID: 10667
		private string id32_JunkLevelType;

		// Token: 0x040029AC RID: 10668
		private string id19_FilterKeyType;

		// Token: 0x040029AD RID: 10669
		private string id49_Clause;

		// Token: 0x040029AE RID: 10670
		private string id43_Enabled;

		// Token: 0x040029AF RID: 10671
		private string id61_JunkLevel;

		// Token: 0x040029B0 RID: 10672
		private string id64_ReplyToAddress;

		// Token: 0x040029B1 RID: 10673
		private string id42_ExecutionOrder;

		// Token: 0x040029B2 RID: 10674
		private string id20_FilterOperatorType;

		// Token: 0x040029B3 RID: 10675
		private string id63_ReplyIndicator;

		// Token: 0x040029B4 RID: 10676
		private string id41_Domains;

		// Token: 0x040029B5 RID: 10677
		private string id17_ServiceSettingsType;

		// Token: 0x040029B6 RID: 10678
		private string id71_EndTime;

		// Token: 0x040029B7 RID: 10679
		private string id29_List;

		// Token: 0x040029B8 RID: 10680
		private string id62_JunkMailDestination;

		// Token: 0x040029B9 RID: 10681
		private string id34_ReplyIndicatorType;

		// Token: 0x040029BA RID: 10682
		private string id33_JunkMailDestinationType;

		// Token: 0x040029BB RID: 10683
		private string id5_AccountSettings;

		// Token: 0x040029BC RID: 10684
		private string id21_Item;

		// Token: 0x040029BD RID: 10685
		private string id69_Mode;

		// Token: 0x040029BE RID: 10686
		private string id9_Filters;

		// Token: 0x040029BF RID: 10687
		private string id22_Filter;

		// Token: 0x040029C0 RID: 10688
		private string id58_ConfirmSent;

		// Token: 0x040029C1 RID: 10689
		private string id60_IncludeOriginalInReply;

		// Token: 0x040029C2 RID: 10690
		private string id39_Delete;

		// Token: 0x040029C3 RID: 10691
		private string id59_HeaderDisplay;

		// Token: 0x040029C4 RID: 10692
		private string id45_Condition;

		// Token: 0x040029C5 RID: 10693
		private string id3_Item;

		// Token: 0x040029C6 RID: 10694
		private string id13_UserSignature;

		// Token: 0x040029C7 RID: 10695
		private string id66_UseReplyToAddress;

		// Token: 0x040029C8 RID: 10696
		private string id51_Operator;

		// Token: 0x040029C9 RID: 10697
		private string id31_IncludeOriginalInReplyType;

		// Token: 0x040029CA RID: 10698
		private string id72_Message;

		// Token: 0x040029CB RID: 10699
		private string id57_GetVersion;

		// Token: 0x040029CC RID: 10700
		private string id10_Lists;

		// Token: 0x040029CD RID: 10701
		private string id27_Domain;

		// Token: 0x040029CE RID: 10702
		private string id54_SafetySchemaVersion;

		// Token: 0x040029CF RID: 10703
		private string id44_RunWhen;

		// Token: 0x040029D0 RID: 10704
		private string id47_MoveToFolder;

		// Token: 0x040029D1 RID: 10705
		private string id11_Options;

		// Token: 0x040029D2 RID: 10706
		private string id52_Value;

		// Token: 0x040029D3 RID: 10707
		private string id70_StartTime;

		// Token: 0x040029D4 RID: 10708
		private string id53_charset;

		// Token: 0x040029D5 RID: 10709
		private string id56_SafetyActions;

		// Token: 0x040029D6 RID: 10710
		private string id50_Field;

		// Token: 0x040029D7 RID: 10711
		private string id7_Set;

		// Token: 0x040029D8 RID: 10712
		private string id37_name;

		// Token: 0x040029D9 RID: 10713
		private string id12_Properties;

		// Token: 0x040029DA RID: 10714
		private string id65_SaveSentMail;

		// Token: 0x040029DB RID: 10715
		private string id4_ServiceSettings;

		// Token: 0x040029DC RID: 10716
		private string id67_VacationResponse;

		// Token: 0x040029DD RID: 10717
		private string id23_ItemsChoiceType;

		// Token: 0x040029DE RID: 10718
		private string id18_RunWhenType;

		// Token: 0x040029DF RID: 10719
		private string id24_ArrayOfString;

		// Token: 0x040029E0 RID: 10720
		private string id28_ArrayOfListsRequestTypeList;

		// Token: 0x040029E1 RID: 10721
		private string id46_Actions;

		// Token: 0x040029E2 RID: 10722
		private string id36_ForwardingMode;

		// Token: 0x040029E3 RID: 10723
		private string id6_AccountSettingsType;

		// Token: 0x040029E4 RID: 10724
		private string id8_Get;

		// Token: 0x040029E5 RID: 10725
		private string id2_HMSETTINGS;

		// Token: 0x040029E6 RID: 10726
		private string id35_VacationResponseMode;

		// Token: 0x040029E7 RID: 10727
		private string id30_HeaderDisplayType;

		// Token: 0x040029E8 RID: 10728
		private string id38_Add;

		// Token: 0x040029E9 RID: 10729
		private string id48_FolderId;

		// Token: 0x040029EA RID: 10730
		private string id68_MailForwarding;

		// Token: 0x040029EB RID: 10731
		private string id25_Address;

		// Token: 0x040029EC RID: 10732
		private string id14_OptionsType;

		// Token: 0x040029ED RID: 10733
		private string id15_AddressesAndDomainsType;

		// Token: 0x040029EE RID: 10734
		private string id1_Settings;

		// Token: 0x040029EF RID: 10735
		private string id26_ArrayOfString1;

		// Token: 0x040029F0 RID: 10736
		private string id40_Addresses;

		// Token: 0x040029F1 RID: 10737
		private string id55_SafetyLevelRules;
	}
}
