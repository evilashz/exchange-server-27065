using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.TextMessagingSettingsVersion1Point0
{
	// Token: 0x02000F00 RID: 3840
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class XmlSerializationReaderTextMessagingSettingsVersion1Point0 : XmlSerializationReader
	{
		// Token: 0x0600845A RID: 33882 RVA: 0x0024063C File Offset: 0x0023E83C
		public object Read9_TextMessagingSettings()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_TextMessagingSettings || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read8_Item(true, true);
			}
			else
			{
				base.UnknownNode(null, ":TextMessagingSettings");
			}
			return result;
		}

		// Token: 0x0600845B RID: 33883 RVA: 0x002406AC File Offset: 0x0023E8AC
		private TextMessagingSettingsVersion1Point0 Read8_Item(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_Item || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			TextMessagingSettingsVersion1Point0 textMessagingSettingsVersion1Point = new TextMessagingSettingsVersion1Point0();
			if (textMessagingSettingsVersion1Point.DeliveryPoints == null)
			{
				textMessagingSettingsVersion1Point.DeliveryPoints = new List<DeliveryPoint>();
			}
			List<DeliveryPoint> deliveryPoints = textMessagingSettingsVersion1Point.DeliveryPoints;
			bool[] array = new bool[3];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id4_Version && base.Reader.NamespaceURI == this.id2_Item)
				{
					textMessagingSettingsVersion1Point.Version = base.Reader.Value;
					array[0] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(textMessagingSettingsVersion1Point, ":Version");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return textMessagingSettingsVersion1Point;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[1] && base.Reader.LocalName == this.id5_Item && base.Reader.NamespaceURI == this.id2_Item)
					{
						textMessagingSettingsVersion1Point.MachineToPersonMessagingPolicies = this.Read5_Item(false, true);
						array[1] = true;
					}
					else if (base.Reader.LocalName == this.id6_DeliveryPoint && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (deliveryPoints == null)
						{
							base.Reader.Skip();
						}
						else
						{
							deliveryPoints.Add(this.Read7_DeliveryPoint(false, true));
						}
					}
					else
					{
						base.UnknownNode(textMessagingSettingsVersion1Point, ":MachineToPersonMessagingPolicies, :DeliveryPoint");
					}
				}
				else
				{
					base.UnknownNode(textMessagingSettingsVersion1Point, ":MachineToPersonMessagingPolicies, :DeliveryPoint");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return textMessagingSettingsVersion1Point;
		}

		// Token: 0x0600845C RID: 33884 RVA: 0x002408DC File Offset: 0x0023EADC
		private DeliveryPoint Read7_DeliveryPoint(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id6_DeliveryPoint || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			DeliveryPoint deliveryPoint = new DeliveryPoint();
			bool[] array = new bool[9];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(deliveryPoint);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return deliveryPoint;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id7_Identity && base.Reader.NamespaceURI == this.id2_Item)
					{
						deliveryPoint.Identity = XmlConvert.ToByte(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id8_Type && base.Reader.NamespaceURI == this.id2_Item)
					{
						deliveryPoint.Type = this.Read6_DeliveryPointType(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id9_PhoneNumber && base.Reader.NamespaceURI == this.id2_Item)
					{
						deliveryPoint.PhoneNumber = (E164Number)base.ReadSerializable(new E164Number());
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id10_Protocol && base.Reader.NamespaceURI == this.id2_Item)
					{
						deliveryPoint.Protocol = base.Reader.ReadElementString();
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id11_DeviceType && base.Reader.NamespaceURI == this.id2_Item)
					{
						deliveryPoint.DeviceType = base.Reader.ReadElementString();
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id12_DeviceId && base.Reader.NamespaceURI == this.id2_Item)
					{
						deliveryPoint.DeviceId = base.Reader.ReadElementString();
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id13_DeviceFriendlyName && base.Reader.NamespaceURI == this.id2_Item)
					{
						deliveryPoint.DeviceFriendlyName = base.Reader.ReadElementString();
						array[6] = true;
					}
					else if (!array[7] && base.Reader.LocalName == this.id14_P2pMessaginPriority && base.Reader.NamespaceURI == this.id2_Item)
					{
						deliveryPoint.P2pMessagingPriority = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[7] = true;
					}
					else if (!array[8] && base.Reader.LocalName == this.id15_M2pMessagingPriority && base.Reader.NamespaceURI == this.id2_Item)
					{
						deliveryPoint.M2pMessagingPriority = XmlConvert.ToInt32(base.Reader.ReadElementString());
						array[8] = true;
					}
					else
					{
						base.UnknownNode(deliveryPoint, ":Identity, :Type, :PhoneNumber, :Protocol, :DeviceType, :DeviceId, :DeviceFriendlyName, :P2pMessaginPriority, :M2pMessagingPriority");
					}
				}
				else
				{
					base.UnknownNode(deliveryPoint, ":Identity, :Type, :PhoneNumber, :Protocol, :DeviceType, :DeviceId, :DeviceFriendlyName, :P2pMessaginPriority, :M2pMessagingPriority");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return deliveryPoint;
		}

		// Token: 0x0600845D RID: 33885 RVA: 0x00240CA4 File Offset: 0x0023EEA4
		private DeliveryPointType Read6_DeliveryPointType(string s)
		{
			if (s != null)
			{
				if (s == "Unknown")
				{
					return DeliveryPointType.Unknown;
				}
				if (s == "ExchangeActiveSync")
				{
					return DeliveryPointType.ExchangeActiveSync;
				}
				if (s == "SmtpToSmsGateway")
				{
					return DeliveryPointType.SmtpToSmsGateway;
				}
			}
			throw base.CreateUnknownConstantException(s, typeof(DeliveryPointType));
		}

		// Token: 0x0600845E RID: 33886 RVA: 0x00240CF8 File Offset: 0x0023EEF8
		private MachineToPersonMessagingPolicies Read5_Item(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id5_Item || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			MachineToPersonMessagingPolicies machineToPersonMessagingPolicies = new MachineToPersonMessagingPolicies();
			if (machineToPersonMessagingPolicies.PossibleRecipients == null)
			{
				machineToPersonMessagingPolicies.PossibleRecipients = new List<PossibleRecipient>();
			}
			List<PossibleRecipient> possibleRecipients = machineToPersonMessagingPolicies.PossibleRecipients;
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(machineToPersonMessagingPolicies);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return machineToPersonMessagingPolicies;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (base.Reader.LocalName == this.id16_PossibleRecipient && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (possibleRecipients == null)
						{
							base.Reader.Skip();
						}
						else
						{
							possibleRecipients.Add(this.Read4_PossibleRecipient(false, true));
						}
					}
					else
					{
						base.UnknownNode(machineToPersonMessagingPolicies, ":PossibleRecipient");
					}
				}
				else
				{
					base.UnknownNode(machineToPersonMessagingPolicies, ":PossibleRecipient");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return machineToPersonMessagingPolicies;
		}

		// Token: 0x0600845F RID: 33887 RVA: 0x00240E94 File Offset: 0x0023F094
		private PossibleRecipient Read4_PossibleRecipient(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id16_PossibleRecipient || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			PossibleRecipient possibleRecipient = new PossibleRecipient();
			if (possibleRecipient.PasscodeSentTimeHistory == null)
			{
				possibleRecipient.PasscodeSentTimeHistory = new List<DateTime>();
			}
			List<DateTime> passcodeSentTimeHistory = possibleRecipient.PasscodeSentTimeHistory;
			if (possibleRecipient.PasscodeVerificationFailedTimeHistory == null)
			{
				possibleRecipient.PasscodeVerificationFailedTimeHistory = new List<DateTime>();
			}
			List<DateTime> passcodeVerificationFailedTimeHistory = possibleRecipient.PasscodeVerificationFailedTimeHistory;
			bool[] array = new bool[10];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(possibleRecipient);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return possibleRecipient;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id17_Effective && base.Reader.NamespaceURI == this.id2_Item)
					{
						possibleRecipient.Effective = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id18_EffectiveLastModificationTime && base.Reader.NamespaceURI == this.id2_Item)
					{
						possibleRecipient.EffectiveLastModificationTime = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id19_Region && base.Reader.NamespaceURI == this.id2_Item)
					{
						possibleRecipient.Region = base.Reader.ReadElementString();
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id20_Carrier && base.Reader.NamespaceURI == this.id2_Item)
					{
						possibleRecipient.Carrier = base.Reader.ReadElementString();
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id9_PhoneNumber && base.Reader.NamespaceURI == this.id2_Item)
					{
						possibleRecipient.PhoneNumber = (E164Number)base.ReadSerializable(new E164Number());
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id21_PhoneNumberSetTime && base.Reader.NamespaceURI == this.id2_Item)
					{
						possibleRecipient.PhoneNumberSetTime = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id22_Acknowledged && base.Reader.NamespaceURI == this.id2_Item)
					{
						possibleRecipient.Acknowledged = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[6] = true;
					}
					else if (!array[7] && base.Reader.LocalName == this.id23_Passcode && base.Reader.NamespaceURI == this.id2_Item)
					{
						possibleRecipient.Passcode = base.Reader.ReadElementString();
						array[7] = true;
					}
					else if (base.Reader.LocalName == this.id24_PasscodeSentTimeHistory && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (!base.ReadNull())
						{
							if (possibleRecipient.PasscodeSentTimeHistory == null)
							{
								possibleRecipient.PasscodeSentTimeHistory = new List<DateTime>();
							}
							List<DateTime> passcodeSentTimeHistory2 = possibleRecipient.PasscodeSentTimeHistory;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num2 = 0;
								int readerCount2 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id25_SentTime && base.Reader.NamespaceURI == this.id2_Item)
										{
											passcodeSentTimeHistory2.Add(XmlSerializationReader.ToDateTime(base.Reader.ReadElementString()));
										}
										else
										{
											base.UnknownNode(null, ":SentTime");
										}
									}
									else
									{
										base.UnknownNode(null, ":SentTime");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num2, ref readerCount2);
								}
								base.ReadEndElement();
							}
						}
					}
					else if (base.Reader.LocalName == this.id26_Item && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (!base.ReadNull())
						{
							if (possibleRecipient.PasscodeVerificationFailedTimeHistory == null)
							{
								possibleRecipient.PasscodeVerificationFailedTimeHistory = new List<DateTime>();
							}
							List<DateTime> passcodeVerificationFailedTimeHistory2 = possibleRecipient.PasscodeVerificationFailedTimeHistory;
							if (base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num3 = 0;
								int readerCount3 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id27_FailedTime && base.Reader.NamespaceURI == this.id2_Item)
										{
											passcodeVerificationFailedTimeHistory2.Add(XmlSerializationReader.ToDateTime(base.Reader.ReadElementString()));
										}
										else
										{
											base.UnknownNode(null, ":FailedTime");
										}
									}
									else
									{
										base.UnknownNode(null, ":FailedTime");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num3, ref readerCount3);
								}
								base.ReadEndElement();
							}
						}
					}
					else
					{
						base.UnknownNode(possibleRecipient, ":Effective, :EffectiveLastModificationTime, :Region, :Carrier, :PhoneNumber, :PhoneNumberSetTime, :Acknowledged, :Passcode, :PasscodeSentTimeHistory, :PasscodeVerificationFailedTimeHistory");
					}
				}
				else
				{
					base.UnknownNode(possibleRecipient, ":Effective, :EffectiveLastModificationTime, :Region, :Carrier, :PhoneNumber, :PhoneNumberSetTime, :Acknowledged, :Passcode, :PasscodeSentTimeHistory, :PasscodeVerificationFailedTimeHistory");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return possibleRecipient;
		}

		// Token: 0x06008460 RID: 33888 RVA: 0x002414C0 File Offset: 0x0023F6C0
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06008461 RID: 33889 RVA: 0x002414C4 File Offset: 0x0023F6C4
		protected override void InitIDs()
		{
			this.id16_PossibleRecipient = base.Reader.NameTable.Add("PossibleRecipient");
			this.id5_Item = base.Reader.NameTable.Add("MachineToPersonMessagingPolicies");
			this.id4_Version = base.Reader.NameTable.Add("Version");
			this.id27_FailedTime = base.Reader.NameTable.Add("FailedTime");
			this.id12_DeviceId = base.Reader.NameTable.Add("DeviceId");
			this.id3_Item = base.Reader.NameTable.Add("TextMessagingSettingsVersion1Point0");
			this.id7_Identity = base.Reader.NameTable.Add("Identity");
			this.id20_Carrier = base.Reader.NameTable.Add("Carrier");
			this.id19_Region = base.Reader.NameTable.Add("Region");
			this.id23_Passcode = base.Reader.NameTable.Add("Passcode");
			this.id8_Type = base.Reader.NameTable.Add("Type");
			this.id17_Effective = base.Reader.NameTable.Add("Effective");
			this.id15_M2pMessagingPriority = base.Reader.NameTable.Add("M2pMessagingPriority");
			this.id1_TextMessagingSettings = base.Reader.NameTable.Add("TextMessagingSettings");
			this.id22_Acknowledged = base.Reader.NameTable.Add("Acknowledged");
			this.id2_Item = base.Reader.NameTable.Add("");
			this.id14_P2pMessaginPriority = base.Reader.NameTable.Add("P2pMessaginPriority");
			this.id6_DeliveryPoint = base.Reader.NameTable.Add("DeliveryPoint");
			this.id24_PasscodeSentTimeHistory = base.Reader.NameTable.Add("PasscodeSentTimeHistory");
			this.id11_DeviceType = base.Reader.NameTable.Add("DeviceType");
			this.id26_Item = base.Reader.NameTable.Add("PasscodeVerificationFailedTimeHistory");
			this.id18_EffectiveLastModificationTime = base.Reader.NameTable.Add("EffectiveLastModificationTime");
			this.id13_DeviceFriendlyName = base.Reader.NameTable.Add("DeviceFriendlyName");
			this.id10_Protocol = base.Reader.NameTable.Add("Protocol");
			this.id21_PhoneNumberSetTime = base.Reader.NameTable.Add("PhoneNumberSetTime");
			this.id25_SentTime = base.Reader.NameTable.Add("SentTime");
			this.id9_PhoneNumber = base.Reader.NameTable.Add("PhoneNumber");
		}

		// Token: 0x04005895 RID: 22677
		private string id16_PossibleRecipient;

		// Token: 0x04005896 RID: 22678
		private string id5_Item;

		// Token: 0x04005897 RID: 22679
		private string id4_Version;

		// Token: 0x04005898 RID: 22680
		private string id27_FailedTime;

		// Token: 0x04005899 RID: 22681
		private string id12_DeviceId;

		// Token: 0x0400589A RID: 22682
		private string id3_Item;

		// Token: 0x0400589B RID: 22683
		private string id7_Identity;

		// Token: 0x0400589C RID: 22684
		private string id20_Carrier;

		// Token: 0x0400589D RID: 22685
		private string id19_Region;

		// Token: 0x0400589E RID: 22686
		private string id23_Passcode;

		// Token: 0x0400589F RID: 22687
		private string id8_Type;

		// Token: 0x040058A0 RID: 22688
		private string id17_Effective;

		// Token: 0x040058A1 RID: 22689
		private string id15_M2pMessagingPriority;

		// Token: 0x040058A2 RID: 22690
		private string id1_TextMessagingSettings;

		// Token: 0x040058A3 RID: 22691
		private string id22_Acknowledged;

		// Token: 0x040058A4 RID: 22692
		private string id2_Item;

		// Token: 0x040058A5 RID: 22693
		private string id14_P2pMessaginPriority;

		// Token: 0x040058A6 RID: 22694
		private string id6_DeliveryPoint;

		// Token: 0x040058A7 RID: 22695
		private string id24_PasscodeSentTimeHistory;

		// Token: 0x040058A8 RID: 22696
		private string id11_DeviceType;

		// Token: 0x040058A9 RID: 22697
		private string id26_Item;

		// Token: 0x040058AA RID: 22698
		private string id18_EffectiveLastModificationTime;

		// Token: 0x040058AB RID: 22699
		private string id13_DeviceFriendlyName;

		// Token: 0x040058AC RID: 22700
		private string id10_Protocol;

		// Token: 0x040058AD RID: 22701
		private string id21_PhoneNumberSetTime;

		// Token: 0x040058AE RID: 22702
		private string id25_SentTime;

		// Token: 0x040058AF RID: 22703
		private string id9_PhoneNumber;
	}
}
