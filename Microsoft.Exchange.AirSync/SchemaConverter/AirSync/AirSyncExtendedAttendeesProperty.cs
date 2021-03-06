using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000153 RID: 339
	[Serializable]
	internal class AirSyncExtendedAttendeesProperty : AirSyncProperty, IExtendedAttendeesProperty, IMultivaluedProperty<ExtendedAttendeeData>, IProperty, IEnumerable<ExtendedAttendeeData>, IEnumerable
	{
		// Token: 0x06000FE9 RID: 4073 RVA: 0x0005A694 File Offset: 0x00058894
		public AirSyncExtendedAttendeesProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x0005A69F File Offset: 0x0005889F
		public int Count
		{
			get
			{
				return base.XmlNode.ChildNodes.Count;
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0005A95C File Offset: 0x00058B5C
		public IEnumerator<ExtendedAttendeeData> GetEnumerator()
		{
			foreach (object obj in base.XmlNode.ChildNodes)
			{
				XmlNode attendee = (XmlNode)obj;
				string name = null;
				string email = null;
				int status = -1;
				int type = -1;
				foreach (object obj2 in attendee.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					string name2;
					if ((name2 = xmlNode.Name) != null)
					{
						if (!(name2 == "Name"))
						{
							if (!(name2 == "Email"))
							{
								if (!(name2 == "AttendeeStatus"))
								{
									if (name2 == "AttendeeType")
									{
										type = int.Parse(xmlNode.InnerText);
									}
								}
								else
								{
									status = int.Parse(xmlNode.InnerText);
								}
							}
							else
							{
								email = xmlNode.InnerText;
							}
						}
						else
						{
							name = xmlNode.InnerText;
						}
					}
				}
				bool sendData = status != -1;
				ExtendedAttendeeData returnVal = new ExtendedAttendeeData(email, name, status, type, sendData);
				yield return returnVal;
			}
			yield break;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0005A978 File Offset: 0x00058B78
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0005A990 File Offset: 0x00058B90
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IExtendedAttendeesProperty extendedAttendeesProperty = srcProperty as IExtendedAttendeesProperty;
			if (extendedAttendeesProperty == null)
			{
				throw new UnexpectedTypeException("IExtendedAttendeesProperty", srcProperty);
			}
			base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
			foreach (ExtendedAttendeeData extendedAttendeeData in extendedAttendeesProperty)
			{
				XmlNode xmlNode = base.XmlParentNode.OwnerDocument.CreateElement("Attendee", base.Namespace);
				XmlNode xmlNode2 = base.XmlParentNode.OwnerDocument.CreateElement("Email", base.Namespace);
				XmlNode xmlNode3 = base.XmlParentNode.OwnerDocument.CreateElement("Name", base.Namespace);
				XmlNode xmlNode4 = base.XmlParentNode.OwnerDocument.CreateElement("AttendeeType", base.Namespace);
				xmlNode2.InnerText = extendedAttendeeData.EmailAddress;
				xmlNode3.InnerText = extendedAttendeeData.DisplayName;
				xmlNode4.InnerText = extendedAttendeeData.Type.ToString(CultureInfo.InvariantCulture);
				xmlNode.AppendChild(xmlNode2);
				xmlNode.AppendChild(xmlNode3);
				if (extendedAttendeeData.SendExtendedData)
				{
					XmlNode xmlNode5 = base.XmlParentNode.OwnerDocument.CreateElement("AttendeeStatus", base.Namespace);
					xmlNode5.InnerText = extendedAttendeeData.Status.ToString(CultureInfo.InvariantCulture);
					xmlNode.AppendChild(xmlNode5);
				}
				xmlNode.AppendChild(xmlNode4);
				base.XmlNode.AppendChild(xmlNode);
			}
			if (base.XmlNode.HasChildNodes)
			{
				base.XmlParentNode.AppendChild(base.XmlNode);
			}
		}
	}
}
