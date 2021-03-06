using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.TextMessagingSettingsVersion1Point0
{
	// Token: 0x02000EFF RID: 3839
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class XmlSerializationWriterTextMessagingSettingsVersion1Point0 : XmlSerializationWriter
	{
		// Token: 0x06008452 RID: 33874 RVA: 0x0024011A File Offset: 0x0023E31A
		public void Write9_TextMessagingSettings(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("TextMessagingSettings", "");
				return;
			}
			base.TopLevelElement();
			this.Write8_Item("TextMessagingSettings", "", (TextMessagingSettingsVersion1Point0)o, true, false);
		}

		// Token: 0x06008453 RID: 33875 RVA: 0x00240154 File Offset: 0x0023E354
		private void Write8_Item(string n, string ns, TextMessagingSettingsVersion1Point0 o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(TextMessagingSettingsVersion1Point0)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("TextMessagingSettingsVersion1Point0", "");
			}
			base.WriteAttribute("Version", "", o.Version);
			this.Write5_Item("MachineToPersonMessagingPolicies", "", o.MachineToPersonMessagingPolicies, false, false);
			List<DeliveryPoint> deliveryPoints = o.DeliveryPoints;
			if (deliveryPoints != null)
			{
				for (int i = 0; i < ((ICollection)deliveryPoints).Count; i++)
				{
					this.Write7_DeliveryPoint("DeliveryPoint", "", deliveryPoints[i], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x06008454 RID: 33876 RVA: 0x00240220 File Offset: 0x0023E420
		private void Write7_DeliveryPoint(string n, string ns, DeliveryPoint o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(DeliveryPoint)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("DeliveryPoint", "");
			}
			base.WriteElementStringRaw("Identity", "", XmlConvert.ToString(o.Identity));
			base.WriteElementString("Type", "", this.Write6_DeliveryPointType(o.Type));
			base.WriteSerializable(o.PhoneNumber, "PhoneNumber", "", false, true);
			base.WriteElementString("Protocol", "", o.Protocol);
			base.WriteElementString("DeviceType", "", o.DeviceType);
			base.WriteElementString("DeviceId", "", o.DeviceId);
			base.WriteElementString("DeviceFriendlyName", "", o.DeviceFriendlyName);
			base.WriteElementStringRaw("P2pMessaginPriority", "", XmlConvert.ToString(o.P2pMessagingPriority));
			base.WriteElementStringRaw("M2pMessagingPriority", "", XmlConvert.ToString(o.M2pMessagingPriority));
			base.WriteEndElement(o);
		}

		// Token: 0x06008455 RID: 33877 RVA: 0x00240368 File Offset: 0x0023E568
		private string Write6_DeliveryPointType(DeliveryPointType v)
		{
			string result;
			switch (v)
			{
			case DeliveryPointType.Unknown:
				result = "Unknown";
				break;
			case DeliveryPointType.ExchangeActiveSync:
				result = "ExchangeActiveSync";
				break;
			case DeliveryPointType.SmtpToSmsGateway:
				result = "SmtpToSmsGateway";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Data.Directory.DeliveryPointType");
			}
			return result;
		}

		// Token: 0x06008456 RID: 33878 RVA: 0x002403C4 File Offset: 0x0023E5C4
		private void Write5_Item(string n, string ns, MachineToPersonMessagingPolicies o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(MachineToPersonMessagingPolicies)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("MachineToPersonMessagingPolicies", "");
			}
			List<PossibleRecipient> possibleRecipients = o.PossibleRecipients;
			if (possibleRecipients != null)
			{
				for (int i = 0; i < ((ICollection)possibleRecipients).Count; i++)
				{
					this.Write4_PossibleRecipient("PossibleRecipient", "", possibleRecipients[i], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x06008457 RID: 33879 RVA: 0x00240460 File Offset: 0x0023E660
		private void Write4_PossibleRecipient(string n, string ns, PossibleRecipient o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(PossibleRecipient)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("PossibleRecipient", "");
			}
			base.WriteElementStringRaw("Effective", "", XmlConvert.ToString(o.Effective));
			base.WriteElementStringRaw("EffectiveLastModificationTime", "", XmlSerializationWriter.FromDateTime(o.EffectiveLastModificationTime));
			base.WriteElementString("Region", "", o.Region);
			base.WriteElementString("Carrier", "", o.Carrier);
			base.WriteSerializable(o.PhoneNumber, "PhoneNumber", "", false, true);
			base.WriteElementStringRaw("PhoneNumberSetTime", "", XmlSerializationWriter.FromDateTime(o.PhoneNumberSetTime));
			base.WriteElementStringRaw("Acknowledged", "", XmlConvert.ToString(o.Acknowledged));
			base.WriteElementString("Passcode", "", o.Passcode);
			List<DateTime> passcodeSentTimeHistory = o.PasscodeSentTimeHistory;
			if (passcodeSentTimeHistory != null)
			{
				base.WriteStartElement("PasscodeSentTimeHistory", "", null, false);
				for (int i = 0; i < ((ICollection)passcodeSentTimeHistory).Count; i++)
				{
					base.WriteElementStringRaw("SentTime", "", XmlSerializationWriter.FromDateTime(passcodeSentTimeHistory[i]));
				}
				base.WriteEndElement();
			}
			List<DateTime> passcodeVerificationFailedTimeHistory = o.PasscodeVerificationFailedTimeHistory;
			if (passcodeVerificationFailedTimeHistory != null)
			{
				base.WriteStartElement("PasscodeVerificationFailedTimeHistory", "", null, false);
				for (int j = 0; j < ((ICollection)passcodeVerificationFailedTimeHistory).Count; j++)
				{
					base.WriteElementStringRaw("FailedTime", "", XmlSerializationWriter.FromDateTime(passcodeVerificationFailedTimeHistory[j]));
				}
				base.WriteEndElement();
			}
			base.WriteEndElement(o);
		}

		// Token: 0x06008458 RID: 33880 RVA: 0x00240631 File Offset: 0x0023E831
		protected override void InitCallbacks()
		{
		}
	}
}
