using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.CalendarNotificationContentVersion1Point0
{
	// Token: 0x02000EF6 RID: 3830
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class XmlSerializationReaderCalendarNotificationContentVersion1Point0 : XmlSerializationReader
	{
		// Token: 0x0600840F RID: 33807 RVA: 0x0023D4B4 File Offset: 0x0023B6B4
		public object Read7_CalendarNotificationContent()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_CalendarNotificationContent || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read6_Item(true, true);
			}
			else
			{
				base.UnknownNode(null, ":CalendarNotificationContent");
			}
			return result;
		}

		// Token: 0x06008410 RID: 33808 RVA: 0x0023D524 File Offset: 0x0023B724
		private CalendarNotificationContentVersion1Point0 Read6_Item(bool isNullable, bool checkType)
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
			CalendarNotificationContentVersion1Point0 calendarNotificationContentVersion1Point = new CalendarNotificationContentVersion1Point0();
			if (calendarNotificationContentVersion1Point.CalEvents == null)
			{
				calendarNotificationContentVersion1Point.CalEvents = new List<CalendarEvent>();
			}
			List<CalendarEvent> calEvents = calendarNotificationContentVersion1Point.CalEvents;
			bool[] array = new bool[4];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id4_Version && base.Reader.NamespaceURI == this.id2_Item)
				{
					calendarNotificationContentVersion1Point.Version = base.Reader.Value;
					array[0] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(calendarNotificationContentVersion1Point, ":Version");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return calendarNotificationContentVersion1Point;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[1] && base.Reader.LocalName == this.id5_CalNotifType && base.Reader.NamespaceURI == this.id2_Item)
					{
						calendarNotificationContentVersion1Point.CalNotifType = this.Read4_CalendarNotificationType(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id6_CalNotifTypeDesc && base.Reader.NamespaceURI == this.id2_Item)
					{
						calendarNotificationContentVersion1Point.CalNotifTypeDesc = base.Reader.ReadElementString();
						array[2] = true;
					}
					else if (base.Reader.LocalName == this.id7_CalEvent && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (calEvents == null)
						{
							base.Reader.Skip();
						}
						else
						{
							calEvents.Add(this.Read5_CalendarEvent(false, true));
						}
					}
					else
					{
						base.UnknownNode(calendarNotificationContentVersion1Point, ":CalNotifType, :CalNotifTypeDesc, :CalEvent");
					}
				}
				else
				{
					base.UnknownNode(calendarNotificationContentVersion1Point, ":CalNotifType, :CalNotifTypeDesc, :CalEvent");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return calendarNotificationContentVersion1Point;
		}

		// Token: 0x06008411 RID: 33809 RVA: 0x0023D7A4 File Offset: 0x0023B9A4
		private CalendarEvent Read5_CalendarEvent(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id8_CalendarEvent || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			CalendarEvent calendarEvent = new CalendarEvent();
			bool[] array = new bool[8];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(calendarEvent);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return calendarEvent;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id9_DayOfWeekOfStartTime && base.Reader.NamespaceURI == this.id2_Item)
					{
						calendarEvent.DayOfWeekOfStartTime = base.Reader.ReadElementString();
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id10_DateOfStartTime && base.Reader.NamespaceURI == this.id2_Item)
					{
						calendarEvent.DateOfStartTime = base.Reader.ReadElementString();
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id11_TimeOfStartTime && base.Reader.NamespaceURI == this.id2_Item)
					{
						calendarEvent.TimeOfStartTime = base.Reader.ReadElementString();
						array[2] = true;
					}
					else if (!array[3] && base.Reader.LocalName == this.id12_DayOfWeekOfEndTime && base.Reader.NamespaceURI == this.id2_Item)
					{
						calendarEvent.DayOfWeekOfEndTime = base.Reader.ReadElementString();
						array[3] = true;
					}
					else if (!array[4] && base.Reader.LocalName == this.id13_DateOfEndTime && base.Reader.NamespaceURI == this.id2_Item)
					{
						calendarEvent.DateOfEndTime = base.Reader.ReadElementString();
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id14_TimeOfEndTime && base.Reader.NamespaceURI == this.id2_Item)
					{
						calendarEvent.TimeOfEndTime = base.Reader.ReadElementString();
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id15_Subject && base.Reader.NamespaceURI == this.id2_Item)
					{
						calendarEvent.Subject = base.Reader.ReadElementString();
						array[6] = true;
					}
					else if (!array[7] && base.Reader.LocalName == this.id16_Location && base.Reader.NamespaceURI == this.id2_Item)
					{
						calendarEvent.Location = base.Reader.ReadElementString();
						array[7] = true;
					}
					else
					{
						base.UnknownNode(calendarEvent, ":DayOfWeekOfStartTime, :DateOfStartTime, :TimeOfStartTime, :DayOfWeekOfEndTime, :DateOfEndTime, :TimeOfEndTime, :Subject, :Location");
					}
				}
				else
				{
					base.UnknownNode(calendarEvent, ":DayOfWeekOfStartTime, :DateOfStartTime, :TimeOfStartTime, :DayOfWeekOfEndTime, :DateOfEndTime, :TimeOfEndTime, :Subject, :Location");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return calendarEvent;
		}

		// Token: 0x06008412 RID: 33810 RVA: 0x0023DB0C File Offset: 0x0023BD0C
		private CalendarNotificationType Read4_CalendarNotificationType(string s)
		{
			switch (s)
			{
			case "Uninteresting":
				return CalendarNotificationType.Uninteresting;
			case "Summary":
				return CalendarNotificationType.Summary;
			case "Reminder":
				return CalendarNotificationType.Reminder;
			case "NewUpdate":
				return CalendarNotificationType.NewUpdate;
			case "ChangedUpdate":
				return CalendarNotificationType.ChangedUpdate;
			case "DeletedUpdate":
				return CalendarNotificationType.DeletedUpdate;
			}
			throw base.CreateUnknownConstantException(s, typeof(CalendarNotificationType));
		}

		// Token: 0x06008413 RID: 33811 RVA: 0x0023DBCD File Offset: 0x0023BDCD
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06008414 RID: 33812 RVA: 0x0023DBD0 File Offset: 0x0023BDD0
		protected override void InitIDs()
		{
			this.id3_Item = base.Reader.NameTable.Add("CalendarNotificationContentVersion1Point0");
			this.id14_TimeOfEndTime = base.Reader.NameTable.Add("TimeOfEndTime");
			this.id16_Location = base.Reader.NameTable.Add("Location");
			this.id11_TimeOfStartTime = base.Reader.NameTable.Add("TimeOfStartTime");
			this.id2_Item = base.Reader.NameTable.Add("");
			this.id9_DayOfWeekOfStartTime = base.Reader.NameTable.Add("DayOfWeekOfStartTime");
			this.id5_CalNotifType = base.Reader.NameTable.Add("CalNotifType");
			this.id12_DayOfWeekOfEndTime = base.Reader.NameTable.Add("DayOfWeekOfEndTime");
			this.id13_DateOfEndTime = base.Reader.NameTable.Add("DateOfEndTime");
			this.id1_CalendarNotificationContent = base.Reader.NameTable.Add("CalendarNotificationContent");
			this.id15_Subject = base.Reader.NameTable.Add("Subject");
			this.id6_CalNotifTypeDesc = base.Reader.NameTable.Add("CalNotifTypeDesc");
			this.id4_Version = base.Reader.NameTable.Add("Version");
			this.id7_CalEvent = base.Reader.NameTable.Add("CalEvent");
			this.id10_DateOfStartTime = base.Reader.NameTable.Add("DateOfStartTime");
			this.id8_CalendarEvent = base.Reader.NameTable.Add("CalendarEvent");
		}

		// Token: 0x04005860 RID: 22624
		private string id3_Item;

		// Token: 0x04005861 RID: 22625
		private string id14_TimeOfEndTime;

		// Token: 0x04005862 RID: 22626
		private string id16_Location;

		// Token: 0x04005863 RID: 22627
		private string id11_TimeOfStartTime;

		// Token: 0x04005864 RID: 22628
		private string id2_Item;

		// Token: 0x04005865 RID: 22629
		private string id9_DayOfWeekOfStartTime;

		// Token: 0x04005866 RID: 22630
		private string id5_CalNotifType;

		// Token: 0x04005867 RID: 22631
		private string id12_DayOfWeekOfEndTime;

		// Token: 0x04005868 RID: 22632
		private string id13_DateOfEndTime;

		// Token: 0x04005869 RID: 22633
		private string id1_CalendarNotificationContent;

		// Token: 0x0400586A RID: 22634
		private string id15_Subject;

		// Token: 0x0400586B RID: 22635
		private string id6_CalNotifTypeDesc;

		// Token: 0x0400586C RID: 22636
		private string id4_Version;

		// Token: 0x0400586D RID: 22637
		private string id7_CalEvent;

		// Token: 0x0400586E RID: 22638
		private string id10_DateOfStartTime;

		// Token: 0x0400586F RID: 22639
		private string id8_CalendarEvent;
	}
}
