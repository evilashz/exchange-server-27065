using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x020001AA RID: 426
	internal static class EventParametersParser
	{
		// Token: 0x06001223 RID: 4643 RVA: 0x0006281C File Offset: 0x00060A1C
		public static CancelEventParameters ParseCancel(XmlNode xmlNode)
		{
			CancelEventParameters cancelEventParameters = new CancelEventParameters();
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				string localName;
				if ((localName = xmlNode2.LocalName) != null)
				{
					if (localName == "Importance")
					{
						cancelEventParameters.Importance = EventParametersParser.ParseImportance(xmlNode2);
						continue;
					}
					if (localName == "Body")
					{
						cancelEventParameters.Notes = EventParametersParser.ParseBody(xmlNode2);
						continue;
					}
				}
				throw new RequestParsingException("BadCancelEventNode:" + xmlNode2.LocalName);
			}
			return cancelEventParameters;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000628D0 File Offset: 0x00060AD0
		public static ForwardEventParameters ParseForward(XmlNode xmlNode)
		{
			ForwardEventParameters forwardEventParameters = new ForwardEventParameters();
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				string localName;
				switch (localName = xmlNode2.LocalName)
				{
				case "Forwardees":
					forwardEventParameters.Forwardees = EventParametersParser.ParseForwardees(xmlNode2);
					continue;
				case "Importance":
					forwardEventParameters.Importance = EventParametersParser.ParseImportance(xmlNode2);
					continue;
				case "Body":
					forwardEventParameters.Notes = EventParametersParser.ParseBody(xmlNode2);
					continue;
				case "Source":
				case "ClientId":
				case "AccountId":
					continue;
				}
				throw new RequestParsingException("BadForwardEventNode:" + xmlNode2.LocalName);
			}
			if (forwardEventParameters.Forwardees == null || forwardEventParameters.Forwardees.Count == 0)
			{
				throw new RequestParsingException("MissingForwardees");
			}
			return forwardEventParameters;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00062A6C File Offset: 0x00060C6C
		public static ItemBody ParseBody(XmlNode bodyNode)
		{
			if (!bodyNode.HasChildNodes)
			{
				throw new RequestParsingException("NoChildNodes");
			}
			XmlNode xmlNode = bodyNode["Type", "AirSyncBase:"];
			if (xmlNode == null || string.IsNullOrEmpty(xmlNode.InnerText))
			{
				throw new RequestParsingException("NoBodyType");
			}
			int num;
			if (!int.TryParse(xmlNode.InnerText, out num))
			{
				throw new RequestParsingException("InvalidBodyType:" + xmlNode.InnerText);
			}
			ItemBody itemBody;
			switch (num)
			{
			case 0:
				itemBody = null;
				break;
			case 1:
				itemBody = new ItemBody
				{
					ContentType = BodyType.Text
				};
				break;
			case 2:
				itemBody = new ItemBody
				{
					ContentType = BodyType.Html
				};
				break;
			default:
				throw new RequestParsingException("UnsupportedBodyType:" + num);
			}
			if (itemBody != null)
			{
				XmlNode xmlNode2 = bodyNode["Data", "AirSyncBase:"];
				if (xmlNode2 == null || string.IsNullOrEmpty(xmlNode2.InnerText))
				{
					throw new RequestParsingException("NoBodyText");
				}
				itemBody.Content = xmlNode2.InnerText;
			}
			return itemBody;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00062B78 File Offset: 0x00060D78
		public static Importance ParseImportance(XmlNode xmlNode)
		{
			int num;
			if (!int.TryParse(xmlNode.InnerText, out num) || !Enum.IsDefined(typeof(Importance), num))
			{
				throw new RequestParsingException("BadImportance:" + xmlNode.InnerText);
			}
			return (Importance)num;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00062BC4 File Offset: 0x00060DC4
		public static ExDateTime ParseDateTime(XmlNode xmlNode, string propertyName)
		{
			ExDateTime result;
			if (string.IsNullOrEmpty(xmlNode.InnerText) || !ExDateTime.TryParseExact(xmlNode.InnerText, "yyyyMMdd\\THHmmss\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
			{
				throw new RequestParsingException(string.Format("Invalid{0}:{1}", propertyName, xmlNode.InnerText));
			}
			return result;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00062C14 File Offset: 0x00060E14
		private static IList<Recipient<RecipientSchema>> ParseForwardees(XmlNode xmlNode)
		{
			List<Recipient<RecipientSchema>> list = new List<Recipient<RecipientSchema>>();
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				Recipient<RecipientSchema> recipient = new Recipient<RecipientSchema>();
				if (xmlNode2.FirstChild.Name == "Name")
				{
					recipient.Name = xmlNode2.FirstChild.InnerText;
					recipient.EmailAddress = xmlNode2.LastChild.InnerText;
				}
				else
				{
					recipient.EmailAddress = xmlNode2.FirstChild.InnerText;
					if (xmlNode2.FirstChild != xmlNode2.LastChild)
					{
						recipient.Name = xmlNode2.LastChild.InnerText;
					}
				}
				list.Add(recipient);
			}
			return list;
		}
	}
}
