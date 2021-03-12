using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x0200014D RID: 333
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class CalendarRepairLogEntryBase : IXmlSerializable
	{
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00053218 File Offset: 0x00051418
		protected virtual bool ShouldLog
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0005321C File Offset: 0x0005141C
		protected void WriteChildNodes<T>(string collectionNodeName, ICollection<T> childCollection, XmlWriter writer, bool writeIfEmpty, params Pair<string, string>[] attributes) where T : CalendarRepairLogEntryBase
		{
			if (writeIfEmpty || childCollection.Count != 0)
			{
				writer.WriteStartElement(collectionNodeName);
				foreach (Pair<string, string> pair in attributes)
				{
					writer.WriteAttributeString(pair.First, pair.Second);
				}
				foreach (T t in childCollection)
				{
					CalendarRepairLogEntryBase calendarRepairLogEntryBase = t;
					if (calendarRepairLogEntryBase.ShouldLog)
					{
						calendarRepairLogEntryBase.WriteXml(writer);
					}
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000532B8 File Offset: 0x000514B8
		protected static void WriteXmlElementStringIfNotNullOrEmpty(XmlWriter writer, string elementName, string elementValue)
		{
			if (!string.IsNullOrEmpty(elementValue))
			{
				writer.WriteElementString(elementName, elementValue);
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000532CC File Offset: 0x000514CC
		protected static string GetDateTimeString(ExDateTime? dateTime)
		{
			return CalendarRepairLogEntryBase.GetDateTimeString(dateTime ?? ExDateTime.MinValue);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000532F8 File Offset: 0x000514F8
		protected static string GetDateTimeString(ExDateTime dateTime)
		{
			return dateTime.ToUtc().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00053319 File Offset: 0x00051519
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0005331C File Offset: 0x0005151C
		public void ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000DB7 RID: 3511
		public abstract void WriteXml(XmlWriter writer);
	}
}
