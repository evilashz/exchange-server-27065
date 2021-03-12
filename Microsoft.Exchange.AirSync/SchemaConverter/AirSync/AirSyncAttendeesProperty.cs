using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200012B RID: 299
	[Serializable]
	internal class AirSyncAttendeesProperty : AirSyncProperty, IAttendeesProperty, IMultivaluedProperty<AttendeeData>, IProperty, IEnumerable<AttendeeData>, IEnumerable
	{
		// Token: 0x06000F54 RID: 3924 RVA: 0x000580A4 File Offset: 0x000562A4
		public AirSyncAttendeesProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x000580AF File Offset: 0x000562AF
		public int Count
		{
			get
			{
				return base.XmlNode.ChildNodes.Count;
			}
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x000582D0 File Offset: 0x000564D0
		public IEnumerator<AttendeeData> GetEnumerator()
		{
			foreach (object obj in base.XmlNode.ChildNodes)
			{
				XmlNode childNode = (XmlNode)obj;
				if (childNode.FirstChild.Name == "Name")
				{
					AttendeeData returnVal = new AttendeeData(childNode.LastChild.InnerText, childNode.FirstChild.InnerText);
					yield return returnVal;
				}
				else
				{
					AttendeeData returnVal = new AttendeeData(childNode.FirstChild.InnerText, childNode.LastChild.InnerText);
					yield return returnVal;
				}
			}
			yield break;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x000582EC File Offset: 0x000564EC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00058304 File Offset: 0x00056504
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IAttendeesProperty attendeesProperty = srcProperty as IAttendeesProperty;
			if (attendeesProperty == null)
			{
				throw new UnexpectedTypeException("IAttendeesProperty", srcProperty);
			}
			base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
			foreach (AttendeeData attendeeData in attendeesProperty)
			{
				XmlNode xmlNode = base.XmlParentNode.OwnerDocument.CreateElement("Attendee", base.Namespace);
				XmlNode xmlNode2 = base.XmlParentNode.OwnerDocument.CreateElement("Email", base.Namespace);
				XmlNode xmlNode3 = base.XmlParentNode.OwnerDocument.CreateElement("Name", base.Namespace);
				xmlNode2.InnerText = attendeeData.EmailAddress;
				xmlNode3.InnerText = attendeeData.DisplayName;
				xmlNode.AppendChild(xmlNode2);
				xmlNode.AppendChild(xmlNode3);
				base.XmlNode.AppendChild(xmlNode);
			}
			if (base.XmlNode.HasChildNodes)
			{
				base.XmlParentNode.AppendChild(base.XmlNode);
			}
		}
	}
}
