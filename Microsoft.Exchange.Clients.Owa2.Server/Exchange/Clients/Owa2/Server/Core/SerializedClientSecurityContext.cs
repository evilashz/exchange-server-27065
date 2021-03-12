using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000B1 RID: 177
	internal class SerializedClientSecurityContext : ISecurityAccessToken
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00015006 File Offset: 0x00013206
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x0001500E File Offset: 0x0001320E
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

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00015017 File Offset: 0x00013217
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001501F File Offset: 0x0001321F
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

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00015028 File Offset: 0x00013228
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x00015030 File Offset: 0x00013230
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

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00015039 File Offset: 0x00013239
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x00015041 File Offset: 0x00013241
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

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001504A File Offset: 0x0001324A
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x00015052 File Offset: 0x00013252
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

		// Token: 0x06000708 RID: 1800 RVA: 0x0001505C File Offset: 0x0001325C
		public static SerializedClientSecurityContext CreateFromOwaIdentity(OwaIdentity owaIdentity)
		{
			SerializedClientSecurityContext serializedClientSecurityContext = new SerializedClientSecurityContext();
			owaIdentity.ClientSecurityContext.SetSecurityAccessToken(serializedClientSecurityContext);
			serializedClientSecurityContext.LogonName = owaIdentity.GetLogonName();
			serializedClientSecurityContext.AuthenticationType = owaIdentity.AuthenticationType;
			return serializedClientSecurityContext;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00015094 File Offset: 0x00013294
		public void Serialize(XmlTextWriter writer)
		{
			writer.WriteStartElement(SerializedClientSecurityContext.rootElementName);
			writer.WriteAttributeString(SerializedClientSecurityContext.authenticationTypeAttributeName, this.authenticationType);
			writer.WriteAttributeString(SerializedClientSecurityContext.logonNameAttributeName, this.logonName);
			SerializedClientSecurityContext.WriteSid(writer, this.UserSid, 0U, SerializedClientSecurityContext.SidType.User);
			if (this.GroupSids != null)
			{
				for (int i = 0; i < this.GroupSids.Length; i++)
				{
					SerializedClientSecurityContext.WriteSid(writer, this.GroupSids[i].SecurityIdentifier, this.GroupSids[i].Attributes, SerializedClientSecurityContext.SidType.Group);
				}
			}
			if (this.RestrictedGroupSids != null)
			{
				for (int j = 0; j < this.RestrictedGroupSids.Length; j++)
				{
					SerializedClientSecurityContext.WriteSid(writer, this.RestrictedGroupSids[j].SecurityIdentifier, this.RestrictedGroupSids[j].Attributes, SerializedClientSecurityContext.SidType.RestrictedGroup);
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001515C File Offset: 0x0001335C
		internal static SerializedClientSecurityContext Deserialize(Stream input)
		{
			XmlTextReader xmlTextReader = null;
			SerializedClientSecurityContext result;
			try
			{
				xmlTextReader = new XmlTextReader(input);
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.All;
				SerializedClientSecurityContext serializedClientSecurityContext = SerializedClientSecurityContext.Deserialize(xmlTextReader);
				result = serializedClientSecurityContext;
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

		// Token: 0x0600070B RID: 1803 RVA: 0x000151A4 File Offset: 0x000133A4
		internal static SerializedClientSecurityContext Deserialize(XmlTextReader reader)
		{
			SerializedClientSecurityContext serializedClientSecurityContext = new SerializedClientSecurityContext();
			serializedClientSecurityContext.UserSid = null;
			serializedClientSecurityContext.GroupSids = null;
			serializedClientSecurityContext.RestrictedGroupSids = null;
			try
			{
				List<SidStringAndAttributes> list = new List<SidStringAndAttributes>();
				List<SidStringAndAttributes> list2 = new List<SidStringAndAttributes>();
				if (!reader.Read() || XmlNodeType.Element != reader.NodeType || StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.rootElementName) != 0)
				{
					SerializedClientSecurityContext.ThrowParserException(reader, "Missing or invalid root node");
				}
				if (reader.MoveToFirstAttribute())
				{
					do
					{
						if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.authenticationTypeAttributeName) == 0)
						{
							if (serializedClientSecurityContext.authenticationType != null)
							{
								SerializedClientSecurityContext.ThrowParserException(reader, string.Format("Duplicated attribute {0}", SerializedClientSecurityContext.authenticationTypeAttributeName));
							}
							serializedClientSecurityContext.authenticationType = reader.Value;
						}
						else if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.logonNameAttributeName) == 0)
						{
							if (serializedClientSecurityContext.logonName != null)
							{
								SerializedClientSecurityContext.ThrowParserException(reader, string.Format("Duplicated attribute {0}", SerializedClientSecurityContext.logonNameAttributeName));
							}
							serializedClientSecurityContext.logonName = reader.Value;
						}
						else
						{
							SerializedClientSecurityContext.ThrowParserException(reader, "Found invalid attribute in root element");
						}
					}
					while (reader.MoveToNextAttribute());
				}
				if (serializedClientSecurityContext.authenticationType == null || serializedClientSecurityContext.logonName == null)
				{
					SerializedClientSecurityContext.ThrowParserException(reader, "Auth type or logon name attributes are missing");
				}
				bool flag = false;
				int num = 0;
				while (reader.Read())
				{
					if (XmlNodeType.EndElement == reader.NodeType && StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.rootElementName) == 0)
					{
						flag = true;
						break;
					}
					if (XmlNodeType.Element != reader.NodeType || StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.sidElementName) != 0)
					{
						SerializedClientSecurityContext.ThrowParserException(reader, "Expecting SID node");
					}
					SerializedClientSecurityContext.SidType sidType = SerializedClientSecurityContext.SidType.User;
					uint num2 = 0U;
					if (reader.MoveToFirstAttribute())
					{
						do
						{
							if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.sidTypeAttributeName) == 0)
							{
								int num3 = int.Parse(reader.Value);
								if (num3 == 1)
								{
									sidType = SerializedClientSecurityContext.SidType.Group;
								}
								else if (num3 == 2)
								{
									sidType = SerializedClientSecurityContext.SidType.RestrictedGroup;
								}
								else
								{
									SerializedClientSecurityContext.ThrowParserException(reader, "Invalid SID type");
								}
							}
							else if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.sidAttributesAttributeName) == 0)
							{
								num2 = uint.Parse(reader.Value);
							}
							else
							{
								SerializedClientSecurityContext.ThrowParserException(reader, "Found invalid attribute in SID element");
							}
						}
						while (reader.MoveToNextAttribute());
					}
					if (sidType == SerializedClientSecurityContext.SidType.User)
					{
						if (num2 != 0U)
						{
							SerializedClientSecurityContext.ThrowParserException(reader, "'Attributes' shouldn't be set in an user SID");
						}
						else if (serializedClientSecurityContext.UserSid != null)
						{
							SerializedClientSecurityContext.ThrowParserException(reader, "There can only be one user SID in the XML document");
						}
					}
					if (!reader.Read() || XmlNodeType.Text != reader.NodeType || string.IsNullOrEmpty(reader.Value))
					{
						SerializedClientSecurityContext.ThrowParserException(reader, "Expecting SID value in SDDL format");
					}
					string value = reader.Value;
					if (sidType == SerializedClientSecurityContext.SidType.User)
					{
						serializedClientSecurityContext.UserSid = value;
					}
					else if (sidType == SerializedClientSecurityContext.SidType.Group)
					{
						SidStringAndAttributes item = new SidStringAndAttributes(value, num2);
						list.Add(item);
					}
					else if (sidType == SerializedClientSecurityContext.SidType.RestrictedGroup)
					{
						SidStringAndAttributes item2 = new SidStringAndAttributes(value, num2);
						list2.Add(item2);
					}
					if (!reader.Read() || XmlNodeType.EndElement != reader.NodeType)
					{
						SerializedClientSecurityContext.ThrowParserException(reader, "Expected end of SID node");
					}
					num++;
					if (num > SerializedClientSecurityContext.maximumSidsPerContext)
					{
						throw new OwaSecurityContextSidLimitException(string.Format("Too many SID nodes in the request, maximum is {0}", SerializedClientSecurityContext.maximumSidsPerContext), serializedClientSecurityContext.logonName, serializedClientSecurityContext.authenticationType);
					}
				}
				if (serializedClientSecurityContext.UserSid == null)
				{
					SerializedClientSecurityContext.ThrowParserException(reader, "Serialized context should at least contain an user SID");
				}
				if (!flag)
				{
					SerializedClientSecurityContext.ThrowParserException(reader, "Parsing error");
				}
				if (list.Count > 0)
				{
					serializedClientSecurityContext.GroupSids = list.ToArray();
				}
				if (list2.Count > 0)
				{
					serializedClientSecurityContext.RestrictedGroupSids = list2.ToArray();
				}
			}
			catch (XmlException ex)
			{
				SerializedClientSecurityContext.ThrowParserException(reader, string.Format("Parser threw an XML exception: {0}", ex.Message));
			}
			return serializedClientSecurityContext;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00015534 File Offset: 0x00013734
		internal string Serialize()
		{
			StringWriter stringWriter = new StringWriter();
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

		// Token: 0x0600070D RID: 1805 RVA: 0x00015588 File Offset: 0x00013788
		private static void ThrowParserException(XmlTextReader reader, string description)
		{
			throw new OwaInvalidRequestException(string.Format(CultureInfo.InvariantCulture, "Invalid serialized client context. Line number: {0} Position: {1}.{2}", new object[]
			{
				reader.LineNumber.ToString(CultureInfo.InvariantCulture),
				reader.LinePosition.ToString(CultureInfo.InvariantCulture),
				(description != null) ? (" " + description) : string.Empty
			}));
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000155F8 File Offset: 0x000137F8
		private static void WriteSid(XmlTextWriter writer, string sid, uint attributes, SerializedClientSecurityContext.SidType sidType)
		{
			writer.WriteStartElement(SerializedClientSecurityContext.sidElementName);
			if (attributes != 0U)
			{
				writer.WriteAttributeString(SerializedClientSecurityContext.sidAttributesAttributeName, attributes.ToString());
			}
			if (sidType != SerializedClientSecurityContext.SidType.User)
			{
				string localName = SerializedClientSecurityContext.sidTypeAttributeName;
				int num = (int)sidType;
				writer.WriteAttributeString(localName, num.ToString());
			}
			writer.WriteString(sid);
			writer.WriteEndElement();
		}

		// Token: 0x040003CD RID: 973
		private static readonly int maximumSidsPerContext = 3000;

		// Token: 0x040003CE RID: 974
		private static readonly string rootElementName = "r";

		// Token: 0x040003CF RID: 975
		private static readonly string authenticationTypeAttributeName = "at";

		// Token: 0x040003D0 RID: 976
		private static readonly string logonNameAttributeName = "ln";

		// Token: 0x040003D1 RID: 977
		private static readonly string sidElementName = "s";

		// Token: 0x040003D2 RID: 978
		private static readonly string sidTypeAttributeName = "t";

		// Token: 0x040003D3 RID: 979
		private static readonly string sidAttributesAttributeName = "a";

		// Token: 0x040003D4 RID: 980
		private string userSid;

		// Token: 0x040003D5 RID: 981
		private SidStringAndAttributes[] groupSids;

		// Token: 0x040003D6 RID: 982
		private SidStringAndAttributes[] restrictedGroupSids;

		// Token: 0x040003D7 RID: 983
		private string authenticationType;

		// Token: 0x040003D8 RID: 984
		private string logonName;

		// Token: 0x020000B2 RID: 178
		private enum SidType
		{
			// Token: 0x040003DA RID: 986
			User,
			// Token: 0x040003DB RID: 987
			Group,
			// Token: 0x040003DC RID: 988
			RestrictedGroup
		}
	}
}
