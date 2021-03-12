using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200064E RID: 1614
	internal sealed class SerializedAccessToken : SecurityAccessToken
	{
		// Token: 0x06001D3F RID: 7487 RVA: 0x000356A4 File Offset: 0x000338A4
		public SerializedAccessToken(string logonName, string authenticationType, ClientSecurityContext clientSecurityContext)
		{
			if (logonName == null)
			{
				throw new ArgumentNullException("logonName");
			}
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			this.LogonName = logonName;
			this.AuthenticationType = authenticationType;
			clientSecurityContext.SetSecurityAccessToken(this);
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x000356F8 File Offset: 0x000338F8
		public SerializedAccessToken(Stream input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(input))
			{
				this.Deserialize(xmlTextReader);
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x00035744 File Offset: 0x00033944
		public static SerializedAccessToken Deserialize(string token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			SerializedAccessToken result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(token)))
			{
				result = new SerializedAccessToken(memoryStream);
			}
			return result;
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x00035794 File Offset: 0x00033994
		// (set) Token: 0x06001D43 RID: 7491 RVA: 0x0003579C File Offset: 0x0003399C
		public string LogonName { get; private set; }

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x000357A5 File Offset: 0x000339A5
		// (set) Token: 0x06001D45 RID: 7493 RVA: 0x000357AD File Offset: 0x000339AD
		public string AuthenticationType { get; private set; }

		// Token: 0x06001D46 RID: 7494 RVA: 0x000357B8 File Offset: 0x000339B8
		public override string ToString()
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					this.Serialize(xmlTextWriter);
					result = stringWriter.ToString();
				}
			}
			return result;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00035814 File Offset: 0x00033A14
		public void Serialize(Stream writeStream)
		{
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(writeStream, Encoding.UTF8))
			{
				this.Serialize(xmlTextWriter);
			}
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x00035850 File Offset: 0x00033A50
		private void Serialize(XmlWriter writer)
		{
			if (string.IsNullOrEmpty(base.UserSid))
			{
				throw new InvalidOperationException();
			}
			writer.WriteStartElement("r");
			writer.WriteAttributeString("at", this.AuthenticationType);
			writer.WriteAttributeString("ln", this.LogonName);
			SerializedAccessToken.WriteSid(writer, base.UserSid, 0U, "0");
			SerializedAccessToken.WriteGroups(writer, base.GroupSids, "1");
			SerializedAccessToken.WriteGroups(writer, base.RestrictedGroupSids, "2");
			writer.WriteEndElement();
			writer.Flush();
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x000358E0 File Offset: 0x00033AE0
		private static void WriteSid(XmlWriter writer, string sid, uint attributes, string sidType)
		{
			writer.WriteStartElement("s");
			if (attributes != 0U)
			{
				writer.WriteAttributeString("a", attributes.ToString());
			}
			if (sidType != "0")
			{
				writer.WriteAttributeString("t", sidType);
			}
			writer.WriteString(sid);
			writer.WriteEndElement();
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x00035934 File Offset: 0x00033B34
		private static void WriteGroups(XmlWriter writer, SidStringAndAttributes[] groups, string sidType)
		{
			if (groups != null)
			{
				for (int i = 0; i < groups.Length; i++)
				{
					SerializedAccessToken.WriteSid(writer, groups[i].SecurityIdentifier, groups[i].Attributes, sidType);
				}
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0003596C File Offset: 0x00033B6C
		private void Deserialize(XmlTextReader reader)
		{
			try
			{
				this.ReadRootNode(reader);
				this.ReadSidNodes(reader);
				if (base.UserSid == null)
				{
					SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.MissingUserSid);
				}
			}
			catch (XmlException innerException)
			{
				throw new SerializedAccessTokenParserException(reader.LineNumber, reader.LinePosition, AuthorizationStrings.InvalidXml, innerException);
			}
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x000359C8 File Offset: 0x00033BC8
		private void ReadRootNode(XmlTextReader reader)
		{
			SerializedAccessToken.ReadRootNodeElement(reader);
			this.ReadRootAttributes(reader);
			if (this.AuthenticationType == null)
			{
				SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.AuthenticationTypeIsMissing);
			}
			if (this.LogonName == null)
			{
				SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.LogonNameIsMissing);
			}
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x000359FD File Offset: 0x00033BFD
		private static void ReadRootNodeElement(XmlTextReader reader)
		{
			if (!reader.Read() || XmlNodeType.Element != reader.NodeType || !StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "r"))
			{
				SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.InvalidRoot);
			}
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00035A34 File Offset: 0x00033C34
		private void ReadRootAttributes(XmlTextReader reader)
		{
			if (reader.MoveToFirstAttribute())
			{
				do
				{
					if (StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "at"))
					{
						this.AuthenticationType = reader.Value;
					}
					else if (StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "ln"))
					{
						this.LogonName = reader.Value;
					}
					else
					{
						SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.InvalidRootAttribute(reader.Name));
					}
				}
				while (reader.MoveToNextAttribute());
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00035AAC File Offset: 0x00033CAC
		private void ReadSidNodes(XmlTextReader reader)
		{
			List<SidStringAndAttributes> list = new List<SidStringAndAttributes>();
			List<SidStringAndAttributes> list2 = new List<SidStringAndAttributes>();
			foreach (SerializedAccessToken.SidNode sidNode in this.EnumerateSidNodes(reader))
			{
				if (sidNode.Type == "0")
				{
					if (base.UserSid != null)
					{
						SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.MultipleUserSid);
					}
					base.UserSid = sidNode.SidStringAndAttributes.SecurityIdentifier;
				}
				else if (sidNode.Type == "1")
				{
					list.Add(sidNode.SidStringAndAttributes);
				}
				else if (sidNode.Type == "2")
				{
					list2.Add(sidNode.SidStringAndAttributes);
				}
			}
			base.GroupSids = list.ToArray();
			base.RestrictedGroupSids = list2.ToArray();
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x00035CDC File Offset: 0x00033EDC
		private IEnumerable<SerializedAccessToken.SidNode> EnumerateSidNodes(XmlTextReader reader)
		{
			int sidCount = 0;
			while (reader.Read() && !SerializedAccessToken.IsEndOfRootNode(reader))
			{
				yield return SerializedAccessToken.SidNode.Read(reader);
				sidCount++;
				if (sidCount > 3000)
				{
					SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.TooManySidNodes(this.LogonName, 3000));
				}
			}
			yield break;
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00035D00 File Offset: 0x00033F00
		private static bool IsEndOfRootNode(XmlTextReader reader)
		{
			return XmlNodeType.EndElement == reader.NodeType && StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "r");
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x00035D23 File Offset: 0x00033F23
		private static void ThrowParserException(XmlTextReader reader, LocalizedString description)
		{
			throw new SerializedAccessTokenParserException(reader.LineNumber, reader.LinePosition, description);
		}

		// Token: 0x04001D98 RID: 7576
		private const int MaximumSidsPerContext = 3000;

		// Token: 0x04001D99 RID: 7577
		private const string rootElementName = "r";

		// Token: 0x04001D9A RID: 7578
		private const string authenticationTypeAttributeName = "at";

		// Token: 0x04001D9B RID: 7579
		private const string logonNameAttributeName = "ln";

		// Token: 0x04001D9C RID: 7580
		private const string sidElementName = "s";

		// Token: 0x04001D9D RID: 7581
		private const string sidTypeAttributeName = "t";

		// Token: 0x04001D9E RID: 7582
		private const string sidAttributesAttributeName = "a";

		// Token: 0x0200064F RID: 1615
		private struct SidType
		{
			// Token: 0x04001DA1 RID: 7585
			public const string User = "0";

			// Token: 0x04001DA2 RID: 7586
			public const string Group = "1";

			// Token: 0x04001DA3 RID: 7587
			public const string RestrictedGroup = "2";
		}

		// Token: 0x02000650 RID: 1616
		private struct SidNode
		{
			// Token: 0x06001D53 RID: 7507 RVA: 0x00035D37 File Offset: 0x00033F37
			public SidNode(string sidType, string sidValue, uint attributes)
			{
				this.Type = sidType;
				this.SidStringAndAttributes = new SidStringAndAttributes(sidValue, attributes);
			}

			// Token: 0x06001D54 RID: 7508 RVA: 0x00035D50 File Offset: 0x00033F50
			public static SerializedAccessToken.SidNode Read(XmlTextReader reader)
			{
				string text = "0";
				uint num = 0U;
				SerializedAccessToken.SidNode.CheckReaderOnSidNode(reader);
				if (reader.MoveToFirstAttribute())
				{
					do
					{
						if (StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "t"))
						{
							text = SerializedAccessToken.SidNode.ParseSidType(reader);
						}
						else if (StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "a"))
						{
							num = SerializedAccessToken.SidNode.ParseSidAttributes(reader);
						}
						else
						{
							SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.InvalidSidAttribute(reader.Name));
						}
					}
					while (reader.MoveToNextAttribute());
				}
				if (text == "0" && num != 0U)
				{
					SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.UserSidMustNotHaveAttributes);
				}
				string sidValue = SerializedAccessToken.SidNode.ReadSidValue(reader);
				SerializedAccessToken.SidNode.ReadEndOfSidNode(reader);
				return new SerializedAccessToken.SidNode(text, sidValue, num);
			}

			// Token: 0x06001D55 RID: 7509 RVA: 0x00035DFC File Offset: 0x00033FFC
			private static void CheckReaderOnSidNode(XmlTextReader reader)
			{
				if (XmlNodeType.Element != reader.NodeType || !StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "s"))
				{
					SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.SidNodeExpected);
				}
			}

			// Token: 0x06001D56 RID: 7510 RVA: 0x00035E2C File Offset: 0x0003402C
			private static string ParseSidType(XmlTextReader reader)
			{
				string value = reader.Value;
				if (value != "0" && value != "1" && value != "2")
				{
					SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.InvalidSidType);
				}
				return value;
			}

			// Token: 0x06001D57 RID: 7511 RVA: 0x00035E74 File Offset: 0x00034074
			private static uint ParseSidAttributes(XmlTextReader reader)
			{
				uint result;
				try
				{
					uint num = uint.Parse(reader.Value);
					if (num == 0U)
					{
						SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.InvalidAttributeValue);
					}
					result = num;
				}
				catch (FormatException innerException)
				{
					throw new SerializedAccessTokenParserException(reader.LineNumber, reader.LinePosition, AuthorizationStrings.InvalidAttributeValue, innerException);
				}
				catch (OverflowException innerException2)
				{
					throw new SerializedAccessTokenParserException(reader.LineNumber, reader.LinePosition, AuthorizationStrings.InvalidAttributeValue, innerException2);
				}
				return result;
			}

			// Token: 0x06001D58 RID: 7512 RVA: 0x00035EF0 File Offset: 0x000340F0
			private static string ReadSidValue(XmlTextReader reader)
			{
				if (!reader.Read() || XmlNodeType.Text != reader.NodeType || string.IsNullOrEmpty(reader.Value))
				{
					SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.ExpectingSidValue);
				}
				return reader.Value;
			}

			// Token: 0x06001D59 RID: 7513 RVA: 0x00035F21 File Offset: 0x00034121
			private static void ReadEndOfSidNode(XmlTextReader reader)
			{
				if (!reader.Read() || XmlNodeType.EndElement != reader.NodeType)
				{
					SerializedAccessToken.ThrowParserException(reader, AuthorizationStrings.ExpectingEndOfSid);
				}
			}

			// Token: 0x04001DA4 RID: 7588
			public string Type;

			// Token: 0x04001DA5 RID: 7589
			public SidStringAndAttributes SidStringAndAttributes;
		}
	}
}
