using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000248 RID: 584
	internal class SerializedClientSecurityContext : ISecurityAccessToken
	{
		// Token: 0x06001394 RID: 5012 RVA: 0x00078990 File Offset: 0x00076B90
		public static SerializedClientSecurityContext CreateFromOwaIdentity(OwaIdentity owaIdentity)
		{
			SerializedClientSecurityContext serializedClientSecurityContext = new SerializedClientSecurityContext();
			owaIdentity.ClientSecurityContext.SetSecurityAccessToken(serializedClientSecurityContext);
			serializedClientSecurityContext.LogonName = owaIdentity.GetLogonName();
			serializedClientSecurityContext.AuthenticationType = owaIdentity.AuthenticationType;
			return serializedClientSecurityContext;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x000789C8 File Offset: 0x00076BC8
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x000789D0 File Offset: 0x00076BD0
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

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x000789D9 File Offset: 0x00076BD9
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x000789E1 File Offset: 0x00076BE1
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

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x000789EA File Offset: 0x00076BEA
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x000789F2 File Offset: 0x00076BF2
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

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x000789FB File Offset: 0x00076BFB
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x00078A03 File Offset: 0x00076C03
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

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x00078A0C File Offset: 0x00076C0C
		// (set) Token: 0x0600139E RID: 5022 RVA: 0x00078A14 File Offset: 0x00076C14
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

		// Token: 0x0600139F RID: 5023 RVA: 0x00078A20 File Offset: 0x00076C20
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

		// Token: 0x060013A0 RID: 5024 RVA: 0x00078AE8 File Offset: 0x00076CE8
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

		// Token: 0x060013A1 RID: 5025 RVA: 0x00078B3C File Offset: 0x00076D3C
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

		// Token: 0x060013A2 RID: 5026 RVA: 0x00078B90 File Offset: 0x00076D90
		internal static SerializedClientSecurityContext Deserialize(Stream input)
		{
			XmlTextReader xmlTextReader = null;
			SerializedClientSecurityContext result;
			try
			{
				xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(input);
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

		// Token: 0x060013A3 RID: 5027 RVA: 0x00078BD8 File Offset: 0x00076DD8
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
					if (num > SerializedClientSecurityContext.MaximumSidsPerContext)
					{
						throw new OwaSecurityContextSidLimitException(string.Format("Too many SID nodes in the request, maximum is {0}", SerializedClientSecurityContext.MaximumSidsPerContext), serializedClientSecurityContext.logonName, serializedClientSecurityContext.authenticationType);
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

		// Token: 0x060013A4 RID: 5028 RVA: 0x00078F68 File Offset: 0x00077168
		private static void ThrowParserException(XmlTextReader reader, string description)
		{
			throw new OwaInvalidRequestException(string.Format(CultureInfo.InvariantCulture, "Invalid serialized client context. Line number: {0} Position: {1}.{2}", new object[]
			{
				reader.LineNumber.ToString(CultureInfo.InvariantCulture),
				reader.LinePosition.ToString(CultureInfo.InvariantCulture),
				(description != null) ? (" " + description) : string.Empty
			}));
		}

		// Token: 0x04000D7F RID: 3455
		public static int MaximumSidsPerContext = 3000;

		// Token: 0x04000D80 RID: 3456
		private static readonly string rootElementName = "r";

		// Token: 0x04000D81 RID: 3457
		private static readonly string authenticationTypeAttributeName = "at";

		// Token: 0x04000D82 RID: 3458
		private static readonly string logonNameAttributeName = "ln";

		// Token: 0x04000D83 RID: 3459
		private static readonly string sidElementName = "s";

		// Token: 0x04000D84 RID: 3460
		private static readonly string sidTypeAttributeName = "t";

		// Token: 0x04000D85 RID: 3461
		private static readonly string sidAttributesAttributeName = "a";

		// Token: 0x04000D86 RID: 3462
		private string userSid;

		// Token: 0x04000D87 RID: 3463
		private SidStringAndAttributes[] groupSids;

		// Token: 0x04000D88 RID: 3464
		private SidStringAndAttributes[] restrictedGroupSids;

		// Token: 0x04000D89 RID: 3465
		private string authenticationType;

		// Token: 0x04000D8A RID: 3466
		private string logonName;

		// Token: 0x02000249 RID: 585
		private enum SidType
		{
			// Token: 0x04000D8C RID: 3468
			User,
			// Token: 0x04000D8D RID: 3469
			Group,
			// Token: 0x04000D8E RID: 3470
			RestrictedGroup
		}
	}
}
