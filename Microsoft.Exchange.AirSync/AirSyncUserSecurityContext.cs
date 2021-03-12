using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000028 RID: 40
	internal sealed class AirSyncUserSecurityContext : ISecurityAccessToken
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000101EE File Offset: 0x0000E3EE
		// (set) Token: 0x06000307 RID: 775 RVA: 0x000101F6 File Offset: 0x0000E3F6
		public string UserSid
		{
			get
			{
				return this.userSid;
			}
			set
			{
				this.userSid = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000308 RID: 776 RVA: 0x000101FF File Offset: 0x0000E3FF
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00010207 File Offset: 0x0000E407
		public SidStringAndAttributes[] GroupSids
		{
			get
			{
				return this.groupSids;
			}
			set
			{
				this.groupSids = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00010210 File Offset: 0x0000E410
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00010218 File Offset: 0x0000E418
		public SidStringAndAttributes[] RestrictedGroupSids
		{
			get
			{
				return this.restrictedGroupSids;
			}
			set
			{
				this.restrictedGroupSids = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00010221 File Offset: 0x0000E421
		// (set) Token: 0x0600030D RID: 781 RVA: 0x00010229 File Offset: 0x0000E429
		internal string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
			set
			{
				this.authenticationType = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00010232 File Offset: 0x0000E432
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0001023A File Offset: 0x0000E43A
		internal string LogonName
		{
			get
			{
				return this.logonName;
			}
			set
			{
				this.logonName = value;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00010244 File Offset: 0x0000E444
		public void Serialize(XmlTextWriter writer)
		{
			writer.WriteStartElement("r");
			writer.WriteAttributeString("at", this.authenticationType);
			writer.WriteAttributeString("ln", this.logonName);
			AirSyncUserSecurityContext.WriteSid(writer, this.UserSid, 0U, AirSyncUserSecurityContext.SidType.User);
			if (this.GroupSids != null)
			{
				for (int i = 0; i < this.GroupSids.Length; i++)
				{
					AirSyncUserSecurityContext.WriteSid(writer, this.GroupSids[i].SecurityIdentifier, this.GroupSids[i].Attributes, AirSyncUserSecurityContext.SidType.Group);
				}
			}
			if (this.RestrictedGroupSids != null)
			{
				for (int j = 0; j < this.RestrictedGroupSids.Length; j++)
				{
					AirSyncUserSecurityContext.WriteSid(writer, this.RestrictedGroupSids[j].SecurityIdentifier, this.RestrictedGroupSids[j].Attributes, AirSyncUserSecurityContext.SidType.RestrictedGroup);
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001030C File Offset: 0x0000E50C
		internal static AirSyncUserSecurityContext Deserialize(Stream input)
		{
			XmlTextReader xmlTextReader = null;
			AirSyncUserSecurityContext result;
			try
			{
				xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(input);
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.All;
				AirSyncUserSecurityContext airSyncUserSecurityContext = AirSyncUserSecurityContext.Deserialize(xmlTextReader);
				result = airSyncUserSecurityContext;
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
			return result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00010354 File Offset: 0x0000E554
		internal static AirSyncUserSecurityContext Deserialize(XmlTextReader reader)
		{
			AirSyncUserSecurityContext airSyncUserSecurityContext = new AirSyncUserSecurityContext();
			airSyncUserSecurityContext.UserSid = null;
			airSyncUserSecurityContext.GroupSids = null;
			airSyncUserSecurityContext.RestrictedGroupSids = null;
			try
			{
				List<SidStringAndAttributes> list = new List<SidStringAndAttributes>();
				List<SidStringAndAttributes> list2 = new List<SidStringAndAttributes>();
				if (!reader.Read() || XmlNodeType.Element != reader.NodeType || StringComparer.OrdinalIgnoreCase.Compare(reader.Name, "r") != 0)
				{
					AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:RootNodeMissing");
				}
				if (reader.MoveToFirstAttribute())
				{
					do
					{
						if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, "at") == 0)
						{
							if (airSyncUserSecurityContext.authenticationType != null)
							{
								AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:AuthTypeTwice");
							}
							airSyncUserSecurityContext.authenticationType = reader.Value;
						}
						else if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, "ln") == 0)
						{
							if (airSyncUserSecurityContext.logonName != null)
							{
								AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:UserNameTwice");
							}
							airSyncUserSecurityContext.logonName = reader.Value;
						}
						else
						{
							string protocolErrorString = "ProxyRequestError:UnknownElement(" + reader.Name + ")";
							AirSyncUserSecurityContext.ThrowParserException(protocolErrorString);
						}
					}
					while (reader.MoveToNextAttribute());
				}
				if (airSyncUserSecurityContext.authenticationType == null || airSyncUserSecurityContext.logonName == null)
				{
					AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:AuthTypeLogonNameMissing");
				}
				bool flag = false;
				int num = 0;
				while (reader.Read())
				{
					if (XmlNodeType.EndElement == reader.NodeType && StringComparer.OrdinalIgnoreCase.Compare(reader.Name, "r") == 0)
					{
						flag = true;
						break;
					}
					if (XmlNodeType.Element != reader.NodeType || StringComparer.OrdinalIgnoreCase.Compare(reader.Name, "s") != 0)
					{
						AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:NoSID");
					}
					AirSyncUserSecurityContext.SidType sidType = AirSyncUserSecurityContext.SidType.User;
					uint num2 = 0U;
					if (reader.MoveToFirstAttribute())
					{
						do
						{
							if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, "t") == 0)
							{
								int num3 = int.Parse(reader.Value, CultureInfo.InvariantCulture);
								if (num3 == 1)
								{
									sidType = AirSyncUserSecurityContext.SidType.Group;
								}
								else if (num3 == 2)
								{
									sidType = AirSyncUserSecurityContext.SidType.RestrictedGroup;
								}
								else
								{
									AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:InvalidSIDType");
								}
							}
							else if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, "a") == 0)
							{
								num2 = uint.Parse(reader.Value, CultureInfo.InvariantCulture);
							}
							else
							{
								AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:InvalidSIDAttribute");
							}
						}
						while (reader.MoveToNextAttribute());
					}
					if (sidType == AirSyncUserSecurityContext.SidType.User)
					{
						if (num2 != 0U)
						{
							AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:AttributesOnUserSID");
						}
						else if (airSyncUserSecurityContext.UserSid != null)
						{
							AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:MultipleUserSIDs");
						}
					}
					if (!reader.Read() || XmlNodeType.Text != reader.NodeType || string.IsNullOrEmpty(reader.Value))
					{
						AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:BadProxyHeader");
					}
					string value = reader.Value;
					if (sidType == AirSyncUserSecurityContext.SidType.User)
					{
						airSyncUserSecurityContext.UserSid = value;
					}
					else if (sidType == AirSyncUserSecurityContext.SidType.Group)
					{
						SidStringAndAttributes item = new SidStringAndAttributes(value, num2);
						list.Add(item);
					}
					else if (sidType == AirSyncUserSecurityContext.SidType.RestrictedGroup)
					{
						SidStringAndAttributes item2 = new SidStringAndAttributes(value, num2);
						list2.Add(item2);
					}
					if (!reader.Read() || XmlNodeType.EndElement != reader.NodeType)
					{
						AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:BadProxyHeader2");
					}
					num++;
					if (num > 3000)
					{
						AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:TooManySIDs");
					}
				}
				if (airSyncUserSecurityContext.UserSid == null)
				{
					AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:NoUserSID");
				}
				if (!flag)
				{
					AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:BadParsing");
				}
				if (list.Count > 0)
				{
					airSyncUserSecurityContext.GroupSids = list.ToArray();
				}
				if (list2.Count > 0)
				{
					airSyncUserSecurityContext.RestrictedGroupSids = list2.ToArray();
				}
			}
			catch (XmlException)
			{
				AirSyncUserSecurityContext.ThrowParserException("ProxyRequestError:XMLParsingException");
			}
			return airSyncUserSecurityContext;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000106BC File Offset: 0x0000E8BC
		internal string Serialize()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			string result;
			try
			{
				this.Serialize(xmlTextWriter);
				stringWriter.Flush();
				result = stringWriter.ToString();
			}
			finally
			{
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Close();
				}
				if (stringWriter != null)
				{
					stringWriter.Close();
				}
			}
			return result;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00010718 File Offset: 0x0000E918
		private static void WriteSid(XmlTextWriter writer, string sid, uint attributes, AirSyncUserSecurityContext.SidType sidType)
		{
			writer.WriteStartElement("s");
			if (attributes != 0U)
			{
				writer.WriteAttributeString("a", attributes.ToString(CultureInfo.InvariantCulture));
			}
			if (sidType != AirSyncUserSecurityContext.SidType.User)
			{
				string localName = "t";
				int num = (int)sidType;
				writer.WriteAttributeString(localName, num.ToString(CultureInfo.InvariantCulture));
			}
			writer.WriteString(sid);
			writer.WriteEndElement();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00010774 File Offset: 0x0000E974
		private static void ThrowParserException(string protocolErrorString)
		{
			throw new AirSyncPermanentException(false)
			{
				ErrorStringForProtocolLogger = protocolErrorString
			};
		}

		// Token: 0x0400025E RID: 606
		private const int MaximumSidsPerContext = 3000;

		// Token: 0x0400025F RID: 607
		private const string RootElementName = "r";

		// Token: 0x04000260 RID: 608
		private const string AuthenticationTypeAttributeName = "at";

		// Token: 0x04000261 RID: 609
		private const string LogonNameAttributeName = "ln";

		// Token: 0x04000262 RID: 610
		private const string SidElementName = "s";

		// Token: 0x04000263 RID: 611
		private const string SidTypeAttributeName = "t";

		// Token: 0x04000264 RID: 612
		private const string SidAttributesAttributeName = "a";

		// Token: 0x04000265 RID: 613
		private string userSid;

		// Token: 0x04000266 RID: 614
		private SidStringAndAttributes[] groupSids;

		// Token: 0x04000267 RID: 615
		private SidStringAndAttributes[] restrictedGroupSids;

		// Token: 0x04000268 RID: 616
		private string authenticationType;

		// Token: 0x04000269 RID: 617
		private string logonName;

		// Token: 0x02000029 RID: 41
		private enum SidType
		{
			// Token: 0x0400026B RID: 619
			User,
			// Token: 0x0400026C RID: 620
			Group,
			// Token: 0x0400026D RID: 621
			RestrictedGroup
		}
	}
}
