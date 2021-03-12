using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200007B RID: 123
	internal class SerializedClientSecurityContext : ISecurityAccessToken
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00016187 File Offset: 0x00014387
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0001618F File Offset: 0x0001438F
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

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00016198 File Offset: 0x00014398
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x000161A0 File Offset: 0x000143A0
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x000161A9 File Offset: 0x000143A9
		// (set) Token: 0x060003BA RID: 954 RVA: 0x000161B1 File Offset: 0x000143B1
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

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003BB RID: 955 RVA: 0x000161BA File Offset: 0x000143BA
		// (set) Token: 0x060003BC RID: 956 RVA: 0x000161C2 File Offset: 0x000143C2
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

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000161CB File Offset: 0x000143CB
		// (set) Token: 0x060003BE RID: 958 RVA: 0x000161D3 File Offset: 0x000143D3
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

		// Token: 0x060003BF RID: 959 RVA: 0x000161DC File Offset: 0x000143DC
		public static SerializedClientSecurityContext CreateFromClientSecurityContext(ClientSecurityContext clientSecurityContext, string logonName, string authenticationType)
		{
			SerializedClientSecurityContext serializedClientSecurityContext = new SerializedClientSecurityContext();
			clientSecurityContext.SetSecurityAccessToken(serializedClientSecurityContext);
			serializedClientSecurityContext.LogonName = logonName;
			serializedClientSecurityContext.AuthenticationType = authenticationType;
			return serializedClientSecurityContext;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00016208 File Offset: 0x00014408
		public void Serialize(XmlTextWriter writer)
		{
			writer.WriteStartElement(SerializedClientSecurityContext.RootElementName);
			writer.WriteAttributeString(SerializedClientSecurityContext.AuthenticationTypeAttributeName, this.authenticationType);
			writer.WriteAttributeString(SerializedClientSecurityContext.LogonNameAttributeName, this.logonName);
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

		// Token: 0x060003C1 RID: 961 RVA: 0x000162D0 File Offset: 0x000144D0
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
					xmlTextReader.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00016318 File Offset: 0x00014518
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
				if (!reader.Read() || XmlNodeType.Element != reader.NodeType || StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.RootElementName) != 0)
				{
					SerializedClientSecurityContext.ThrowParserException(reader, "Missing or invalid root node");
				}
				if (reader.MoveToFirstAttribute())
				{
					do
					{
						if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.AuthenticationTypeAttributeName) == 0)
						{
							if (serializedClientSecurityContext.authenticationType != null)
							{
								SerializedClientSecurityContext.ThrowParserException(reader, string.Format("Duplicated attribute {0}", SerializedClientSecurityContext.AuthenticationTypeAttributeName));
							}
							serializedClientSecurityContext.authenticationType = reader.Value;
						}
						else if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.LogonNameAttributeName) == 0)
						{
							if (serializedClientSecurityContext.logonName != null)
							{
								SerializedClientSecurityContext.ThrowParserException(reader, string.Format("Duplicated attribute {0}", SerializedClientSecurityContext.LogonNameAttributeName));
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
					if (XmlNodeType.EndElement == reader.NodeType && StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.RootElementName) == 0)
					{
						flag = true;
						break;
					}
					if (XmlNodeType.Element != reader.NodeType || StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.SidElementName) != 0)
					{
						SerializedClientSecurityContext.ThrowParserException(reader, "Expecting SID node");
					}
					SerializedClientSecurityContext.SidType sidType = SerializedClientSecurityContext.SidType.User;
					uint num2 = 0U;
					if (reader.MoveToFirstAttribute())
					{
						do
						{
							if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.SidTypeAttributeName) == 0)
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
							else if (StringComparer.OrdinalIgnoreCase.Compare(reader.Name, SerializedClientSecurityContext.SidAttributesAttributeName) == 0)
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
						throw new Exception(string.Format("Too many SID nodes in the request, maximum is {0}", SerializedClientSecurityContext.MaximumSidsPerContext));
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

		// Token: 0x060003C3 RID: 963 RVA: 0x0001669C File Offset: 0x0001489C
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
					xmlTextWriter.Flush();
					xmlTextWriter.Dispose();
				}
				if (stringWriter != null)
				{
					stringWriter.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000166F8 File Offset: 0x000148F8
		private static void ThrowParserException(XmlTextReader reader, string description)
		{
			throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Invalid serialized client context. Line number: {0} Position: {1}.{2}", new object[]
			{
				reader.LineNumber.ToString(CultureInfo.InvariantCulture),
				reader.LinePosition.ToString(CultureInfo.InvariantCulture),
				(description != null) ? (" " + description) : string.Empty
			}));
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00016768 File Offset: 0x00014968
		private static void WriteSid(XmlTextWriter writer, string sid, uint attributes, SerializedClientSecurityContext.SidType sidType)
		{
			writer.WriteStartElement(SerializedClientSecurityContext.SidElementName);
			if (attributes != 0U)
			{
				writer.WriteAttributeString(SerializedClientSecurityContext.SidAttributesAttributeName, attributes.ToString());
			}
			if (sidType != SerializedClientSecurityContext.SidType.User)
			{
				string sidTypeAttributeName = SerializedClientSecurityContext.SidTypeAttributeName;
				int num = (int)sidType;
				writer.WriteAttributeString(sidTypeAttributeName, num.ToString());
			}
			writer.WriteString(sid);
			writer.WriteEndElement();
		}

		// Token: 0x040002B2 RID: 690
		private static readonly int MaximumSidsPerContext = 3000;

		// Token: 0x040002B3 RID: 691
		private static readonly string RootElementName = "r";

		// Token: 0x040002B4 RID: 692
		private static readonly string AuthenticationTypeAttributeName = "at";

		// Token: 0x040002B5 RID: 693
		private static readonly string LogonNameAttributeName = "ln";

		// Token: 0x040002B6 RID: 694
		private static readonly string SidElementName = "s";

		// Token: 0x040002B7 RID: 695
		private static readonly string SidTypeAttributeName = "t";

		// Token: 0x040002B8 RID: 696
		private static readonly string SidAttributesAttributeName = "a";

		// Token: 0x040002B9 RID: 697
		private string userSid;

		// Token: 0x040002BA RID: 698
		private SidStringAndAttributes[] groupSids;

		// Token: 0x040002BB RID: 699
		private SidStringAndAttributes[] restrictedGroupSids;

		// Token: 0x040002BC RID: 700
		private string authenticationType;

		// Token: 0x040002BD RID: 701
		private string logonName;

		// Token: 0x0200007C RID: 124
		private enum SidType
		{
			// Token: 0x040002BF RID: 703
			User,
			// Token: 0x040002C0 RID: 704
			Group,
			// Token: 0x040002C1 RID: 705
			RestrictedGroup
		}
	}
}
