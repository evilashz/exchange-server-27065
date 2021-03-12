using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x0200023C RID: 572
	[Serializable]
	internal sealed class RBACContext : IEquatable<RBACContext>
	{
		// Token: 0x0600143F RID: 5183 RVA: 0x0004A494 File Offset: 0x00048694
		private RBACContext(SerializedAccessToken impersonatedUser, IList<RoleType> roleTypeFilter, List<RoleEntry> sortedRoleEntryFilter, IList<RoleType> logonUserRequiredRoleTypes, bool callerCheckedAccess)
		{
			if (impersonatedUser != null)
			{
				this.roleTypeFilter = roleTypeFilter;
				this.sortedRoleEntryFilter = sortedRoleEntryFilter;
				this.logonUserRequiredRoleTypes = logonUserRequiredRoleTypes;
				this.callerCheckedAccess = callerCheckedAccess;
				this.impersonatedUserSddl = impersonatedUser.UserSid;
				this.impersonatedAuthenticationType = impersonatedUser.AuthenticationType;
			}
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0004A4E0 File Offset: 0x000486E0
		public RBACContext(SerializedAccessToken executingUser, SerializedAccessToken impersonatedUser, IList<RoleType> roleTypeFilter, List<RoleEntry> sortedRoleEntryFilter, IList<RoleType> logonUserRequiredRoleTypes, bool callerCheckedAccess) : this(impersonatedUser, roleTypeFilter, sortedRoleEntryFilter, logonUserRequiredRoleTypes, callerCheckedAccess)
		{
			if (executingUser == null)
			{
				throw new ArgumentNullException("executingUser");
			}
			this.serializedExecutingUser = executingUser.ToString();
			this.ExecutingUserName = executingUser.LogonName;
			this.AuthenticationType = executingUser.AuthenticationType;
			this.contextType = RBACContext.RBACContextType.Windows;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0004A534 File Offset: 0x00048734
		public RBACContext(DelegatedPrincipal executingUser, SerializedAccessToken impersonatedUser, IList<RoleType> roleTypeFilter, List<RoleEntry> sortedRoleEntryFilter, IList<RoleType> logonUserRequiredRoleTypes, bool callerCheckedAccess) : this(impersonatedUser, roleTypeFilter, sortedRoleEntryFilter, logonUserRequiredRoleTypes, callerCheckedAccess)
		{
			if (executingUser == null)
			{
				throw new ArgumentNullException("executingUser");
			}
			this.AuthenticationType = DelegatedPrincipal.DelegatedAuthenticationType;
			this.ExecutingUserName = executingUser.DisplayName;
			this.serializedExecutingUser = executingUser.Identity.Name;
			this.contextType = RBACContext.RBACContextType.Delegated;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0004A58C File Offset: 0x0004878C
		public RBACContext(SerializedAccessToken executingUser) : this(executingUser, null, null, null, null, false)
		{
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0004A59A File Offset: 0x0004879A
		public RBACContext(DelegatedPrincipal executingUser) : this(executingUser, null, null, null, null, false)
		{
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0004A5A8 File Offset: 0x000487A8
		public RBACContext(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(inputStream))
			{
				this.Deserialize(xmlTextReader);
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0004A5F4 File Offset: 0x000487F4
		public ExchangeRunspaceConfiguration CreateExchangeRunspaceConfiguration()
		{
			IIdentity identity = this.GetExecutingUserIdentity();
			IIdentity identity2 = null;
			if (!string.IsNullOrEmpty(this.impersonatedUserSddl))
			{
				identity2 = new GenericSidIdentity(this.impersonatedUserSddl, this.impersonatedAuthenticationType, new SecurityIdentifier(this.impersonatedUserSddl));
			}
			ExchangeRunspaceConfiguration result;
			if (identity2 == null)
			{
				result = new ExchangeRunspaceConfiguration(identity);
			}
			else
			{
				result = new ExchangeRunspaceConfiguration(identity, identity2, ExchangeRunspaceConfigurationSettings.GetDefaultInstance(), this.roleTypeFilter, this.sortedRoleEntryFilter, this.logonUserRequiredRoleTypes, this.callerCheckedAccess);
			}
			return result;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0004A666 File Offset: 0x00048866
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x0004A66E File Offset: 0x0004886E
		public string ExecutingUserName { get; private set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0004A677 File Offset: 0x00048877
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x0004A67F File Offset: 0x0004887F
		public string AuthenticationType { get; private set; }

		// Token: 0x0600144A RID: 5194 RVA: 0x0004A688 File Offset: 0x00048888
		private IIdentity GetExecutingUserIdentity()
		{
			if (this.executingUserIdentity == null)
			{
				switch (this.contextType)
				{
				case RBACContext.RBACContextType.Delegated:
					break;
				case (RBACContext.RBACContextType)3:
					goto IL_69;
				case RBACContext.RBACContextType.Windows:
					using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(this.serializedExecutingUser)))
					{
						this.executingUserIdentity = new SerializedIdentity(new SerializedAccessToken(memoryStream));
						goto IL_69;
					}
					break;
				default:
					goto IL_69;
				}
				this.executingUserIdentity = DelegatedPrincipal.GetDelegatedIdentity(this.serializedExecutingUser);
			}
			IL_69:
			return this.executingUserIdentity;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0004A72C File Offset: 0x0004892C
		public bool Equals(RBACContext other)
		{
			if (other == null)
			{
				return false;
			}
			if (!string.Equals(this.impersonatedUserSddl, other.impersonatedUserSddl, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (this.roleTypeFilter != null && other.roleTypeFilter != null)
			{
				if (this.roleTypeFilter.Count != other.roleTypeFilter.Count)
				{
					return false;
				}
				using (IEnumerator<RoleType> enumerator = this.roleTypeFilter.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RoleType item = enumerator.Current;
						if (!other.roleTypeFilter.Contains(item))
						{
							return false;
						}
					}
					goto IL_99;
				}
			}
			if (this.roleTypeFilter != other.roleTypeFilter)
			{
				return false;
			}
			IL_99:
			if (this.sortedRoleEntryFilter != null && other.sortedRoleEntryFilter != null)
			{
				if (this.sortedRoleEntryFilter.Count != other.sortedRoleEntryFilter.Count)
				{
					return false;
				}
				using (List<RoleEntry>.Enumerator enumerator2 = this.sortedRoleEntryFilter.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						RoleEntry element = enumerator2.Current;
						if (null == other.sortedRoleEntryFilter.FirstOrDefault((RoleEntry x) => x.Equals(element)))
						{
							return false;
						}
					}
					goto IL_142;
				}
			}
			if (this.sortedRoleEntryFilter != other.sortedRoleEntryFilter)
			{
				return false;
			}
			IL_142:
			if (this.logonUserRequiredRoleTypes != null && other.logonUserRequiredRoleTypes != null)
			{
				if (this.logonUserRequiredRoleTypes.Count != other.logonUserRequiredRoleTypes.Count)
				{
					return false;
				}
				using (IEnumerator<RoleType> enumerator3 = this.logonUserRequiredRoleTypes.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						RoleType item2 = enumerator3.Current;
						if (!other.logonUserRequiredRoleTypes.Contains(item2))
						{
							return false;
						}
					}
					goto IL_1BD;
				}
			}
			if (this.logonUserRequiredRoleTypes != other.logonUserRequiredRoleTypes)
			{
				return false;
			}
			IL_1BD:
			return this.callerCheckedAccess == other.callerCheckedAccess && string.Equals(this.serializedExecutingUser, other.serializedExecutingUser, StringComparison.OrdinalIgnoreCase) && string.Equals(this.AuthenticationType, other.AuthenticationType, StringComparison.OrdinalIgnoreCase) && string.Equals(this.ExecutingUserName, other.ExecutingUserName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0004A974 File Offset: 0x00048B74
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

		// Token: 0x0600144D RID: 5197 RVA: 0x0004A9D0 File Offset: 0x00048BD0
		public static bool TryParseRbacContextString(string inputString, out RBACContext context)
		{
			context = null;
			bool result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(inputString)))
				{
					context = new RBACContext(memoryStream);
				}
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0004AA2C File Offset: 0x00048C2C
		private void Serialize(XmlTextWriter xmlWriter)
		{
			xmlWriter.WriteStartElement("erccontext");
			xmlWriter.WriteAttributeString("at", this.AuthenticationType);
			xmlWriter.WriteAttributeString("un", this.ExecutingUserName);
			string localName = "t";
			int num = (int)this.contextType;
			xmlWriter.WriteAttributeString(localName, num.ToString());
			this.WriteExecutingUser(xmlWriter);
			this.WriteImpersonatedUser(xmlWriter);
			xmlWriter.WriteEndElement();
			xmlWriter.Flush();
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0004AA99 File Offset: 0x00048C99
		private void WriteExecutingUser(XmlWriter writer)
		{
			writer.WriteElementString("ex", this.serializedExecutingUser);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0004AAE4 File Offset: 0x00048CE4
		private void WriteImpersonatedUser(XmlWriter writer)
		{
			if (string.IsNullOrEmpty(this.impersonatedUserSddl))
			{
				return;
			}
			writer.WriteStartElement("im");
			writer.WriteAttributeString("sddl", this.impersonatedUserSddl);
			writer.WriteAttributeString("at", this.impersonatedAuthenticationType);
			writer.WriteAttributeString("cca", this.callerCheckedAccess.ToString());
			this.WriteList<RoleType>(writer, "rtf", this.roleTypeFilter, delegate(RoleType x)
			{
				int num = (int)x;
				return num.ToString();
			});
			this.WriteList<RoleType>(writer, "lrr", this.logonUserRequiredRoleTypes, delegate(RoleType x)
			{
				int num = (int)x;
				return num.ToString();
			});
			this.WriteList<RoleEntry>(writer, "sref", this.sortedRoleEntryFilter, (RoleEntry x) => x.ToADString());
			writer.WriteEndElement();
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0004ABD8 File Offset: 0x00048DD8
		private void WriteList<T>(XmlWriter writer, string elementName, IList<T> list, Func<T, string> convertToStringFunc)
		{
			if (list == null || list.Count == 0)
			{
				return;
			}
			writer.WriteStartElement(elementName);
			foreach (T arg in list)
			{
				writer.WriteElementString("le", convertToStringFunc(arg));
			}
			writer.WriteEndElement();
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0004AC48 File Offset: 0x00048E48
		private void Deserialize(XmlTextReader reader)
		{
			try
			{
				this.ReadStartOfNodeElement(reader, "erccontext");
				if (reader.MoveToFirstAttribute())
				{
					for (;;)
					{
						if (!StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "t"))
						{
							goto IL_88;
						}
						if (!string.IsNullOrEmpty(reader.Value))
						{
							try
							{
								this.contextType = (RBACContext.RBACContextType)int.Parse(reader.Value);
								goto IL_E4;
							}
							catch (FormatException)
							{
								this.ThrowParserException(reader, Strings.InvalidAttributeValue(reader.Value, "t"));
								goto IL_E4;
							}
							catch (OverflowException)
							{
								this.ThrowParserException(reader, Strings.InvalidAttributeValue(reader.Value, "t"));
								goto IL_E4;
							}
							goto IL_88;
						}
						IL_E4:
						if (!reader.MoveToNextAttribute())
						{
							break;
						}
						continue;
						IL_88:
						if (StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "at"))
						{
							this.AuthenticationType = reader.Value;
							goto IL_E4;
						}
						if (StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "un"))
						{
							this.ExecutingUserName = reader.Value;
							goto IL_E4;
						}
						this.ThrowParserException(reader, Strings.InvalidAttribute(reader.Name));
						goto IL_E4;
					}
				}
				if (RBACContext.RBACContextType.Windows != this.contextType && RBACContext.RBACContextType.Delegated != this.contextType)
				{
					this.ThrowParserException(reader, Strings.InvalidRBACContextType(this.contextType.ToString()));
				}
				if (this.AuthenticationType == null)
				{
					this.ThrowParserException(reader, AuthorizationStrings.AuthenticationTypeIsMissing);
				}
				if (string.IsNullOrEmpty(this.ExecutingUserName))
				{
					this.ThrowParserException(reader, Strings.ExecutingUserNameIsMissing);
				}
				this.ReadExecutingUser(reader);
				this.ReadImpersonatedUser(reader);
			}
			catch (XmlException ex)
			{
				this.ThrowParserException(reader, new LocalizedString(AuthorizationStrings.InvalidXml + " : " + ex.Message));
			}
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0004AE24 File Offset: 0x00049024
		private void ReadExecutingUser(XmlTextReader reader)
		{
			this.ReadStartOfNodeElement(reader, "ex");
			if (this.ElementContainsAttributes(reader))
			{
				this.ThrowParserException(reader, Strings.ElementMustNotHaveAttributes("ex"));
			}
			if (!reader.Read() || XmlNodeType.Text != reader.NodeType || string.IsNullOrEmpty(reader.Value))
			{
				this.ThrowParserException(reader, Strings.InvalidElementValue(reader.Value, "ex"));
			}
			this.serializedExecutingUser = reader.Value;
			this.ReadEndOfNodeElement(reader, "ex");
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0004AEA4 File Offset: 0x000490A4
		private void ReadImpersonatedUser(XmlTextReader reader)
		{
			if (reader.Read() && XmlNodeType.EndElement == reader.NodeType && StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "erccontext"))
			{
				return;
			}
			if (XmlNodeType.Element != reader.NodeType || !StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "im"))
			{
				this.ThrowParserException(reader, Strings.UnExpectedElement("im", reader.Name));
			}
			bool flag = false;
			if (reader.MoveToFirstAttribute())
			{
				do
				{
					if (StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "sddl"))
					{
						this.impersonatedUserSddl = reader.Value;
					}
					else if (StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "at"))
					{
						this.impersonatedAuthenticationType = reader.Value;
					}
					else
					{
						if (StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "cca"))
						{
							if (string.IsNullOrEmpty(reader.Value))
							{
								goto IL_120;
							}
							try
							{
								this.callerCheckedAccess = bool.Parse(reader.Value);
								flag = true;
								goto IL_120;
							}
							catch (FormatException)
							{
								this.ThrowParserException(reader, Strings.InvalidAttributeValue(reader.Value, "cca"));
								goto IL_120;
							}
						}
						this.ThrowParserException(reader, Strings.InvalidAttribute(reader.Name));
					}
					IL_120:;
				}
				while (reader.MoveToNextAttribute());
			}
			if (string.IsNullOrEmpty(this.impersonatedUserSddl))
			{
				this.ThrowParserException(reader, Strings.MissingImpersonatedUserSid);
			}
			if (this.impersonatedAuthenticationType == null)
			{
				this.ThrowParserException(reader, AuthorizationStrings.AuthenticationTypeIsMissing);
			}
			if (!flag)
			{
				this.ThrowParserException(reader, Strings.MissingAttribute("cca", "im"));
			}
			while (reader.Read())
			{
				if (XmlNodeType.Element != reader.NodeType)
				{
					return;
				}
				string name;
				if ((name = reader.Name) != null)
				{
					if (name == "rtf")
					{
						this.roleTypeFilter = this.ReadListElements<RoleType>(reader, "rtf", new Func<XmlTextReader, RoleType>(this.ParseListElementToRoleType));
						continue;
					}
					if (name == "lrr")
					{
						this.logonUserRequiredRoleTypes = this.ReadListElements<RoleType>(reader, "lrr", new Func<XmlTextReader, RoleType>(this.ParseListElementToRoleType));
						continue;
					}
					if (name == "sref")
					{
						this.sortedRoleEntryFilter = this.ReadListElements<RoleEntry>(reader, "sref", new Func<XmlTextReader, RoleEntry>(this.ParseListElementToRoleEntry));
						continue;
					}
				}
				this.ThrowParserException(reader, Strings.InvalidElement(reader.Name));
			}
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0004B0F8 File Offset: 0x000492F8
		private List<T> ReadListElements<T>(XmlTextReader reader, string listElementName, Func<XmlTextReader, T> parseFunction)
		{
			List<T> list = new List<T>();
			foreach (T item in this.EnumerateListElements<T>(reader, listElementName, parseFunction))
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0004B3AC File Offset: 0x000495AC
		private IEnumerable<T> EnumerateListElements<T>(XmlTextReader reader, string listElementName, Func<XmlTextReader, T> parseFunction)
		{
			if (this.ElementContainsAttributes(reader))
			{
				this.ThrowParserException(reader, Strings.ElementMustNotHaveAttributes(listElementName));
			}
			while (reader.Read() && (XmlNodeType.EndElement != reader.NodeType || !StringComparer.OrdinalIgnoreCase.Equals(reader.Name, listElementName)))
			{
				if (XmlNodeType.Element != reader.NodeType || !StringComparer.OrdinalIgnoreCase.Equals(reader.Name, "le"))
				{
					this.ThrowParserException(reader, Strings.UnExpectedElement("le", reader.Name));
				}
				if (this.ElementContainsAttributes(reader))
				{
					this.ThrowParserException(reader, Strings.ElementMustNotHaveAttributes("le"));
				}
				if (!reader.Read() || XmlNodeType.Text != reader.NodeType || string.IsNullOrEmpty(reader.Value))
				{
					this.ThrowParserException(reader, Strings.InvalidElementValue(reader.Value, "le"));
				}
				yield return parseFunction(reader);
				this.ReadEndOfNodeElement(reader, "le");
			}
			yield break;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0004B3DE File Offset: 0x000495DE
		private void ReadStartOfNodeElement(XmlTextReader reader, string elementName)
		{
			if (!reader.Read() || XmlNodeType.Element != reader.NodeType || !StringComparer.OrdinalIgnoreCase.Equals(reader.Name, elementName))
			{
				this.ThrowParserException(reader, Strings.UnExpectedElement(elementName, reader.Name));
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0004B417 File Offset: 0x00049617
		private void ReadEndOfNodeElement(XmlTextReader reader, string elementName)
		{
			if (!reader.Read() || XmlNodeType.EndElement != reader.NodeType || !StringComparer.OrdinalIgnoreCase.Equals(reader.Name, elementName))
			{
				this.ThrowParserException(reader, Strings.UnExpectedElement(elementName, reader.Name));
			}
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0004B454 File Offset: 0x00049654
		private bool ElementContainsAttributes(XmlTextReader reader)
		{
			int num = 0;
			if (reader.MoveToFirstAttribute())
			{
				do
				{
					num++;
				}
				while (reader.MoveToNextAttribute());
			}
			return 0 != num;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0004B480 File Offset: 0x00049680
		private RoleType ParseListElementToRoleType(XmlTextReader reader)
		{
			int result = int.MaxValue;
			try
			{
				result = int.Parse(reader.Value);
			}
			catch (FormatException)
			{
				this.ThrowParserException(reader, Strings.InvalidElementValue(reader.Value, "le"));
			}
			catch (OverflowException)
			{
				this.ThrowParserException(reader, Strings.InvalidElementValue(reader.Value, "le"));
			}
			return (RoleType)result;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0004B4F4 File Offset: 0x000496F4
		private RoleEntry ParseListElementToRoleEntry(XmlTextReader reader)
		{
			RoleEntry result = null;
			try
			{
				result = RoleEntry.Parse(reader.Value);
			}
			catch (FormatException ex)
			{
				this.ThrowParserException(reader, new LocalizedString(ex.Message));
			}
			return result;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0004B538 File Offset: 0x00049738
		private void ThrowParserException(XmlTextReader reader, LocalizedString description)
		{
			throw new RBACContextParserException(reader.LineNumber, reader.LinePosition, description);
		}

		// Token: 0x040005B8 RID: 1464
		private const string rootNode = "erccontext";

		// Token: 0x040005B9 RID: 1465
		private const string contextTypeAttributeName = "t";

		// Token: 0x040005BA RID: 1466
		private const string authenticationTypeAttributeName = "at";

		// Token: 0x040005BB RID: 1467
		private const string executingUserNameAttributeName = "un";

		// Token: 0x040005BC RID: 1468
		private const string serializedExecutingUserElementName = "ex";

		// Token: 0x040005BD RID: 1469
		private const string serializedImpersonatedUserElementName = "im";

		// Token: 0x040005BE RID: 1470
		private const string listElement = "le";

		// Token: 0x040005BF RID: 1471
		private const string impersonatedUserSddlSidAttributeName = "sddl";

		// Token: 0x040005C0 RID: 1472
		private const string impersonatedCallerCheckAccessAttributeName = "cca";

		// Token: 0x040005C1 RID: 1473
		private const string impersonatedRoleTypeFilterElement = "rtf";

		// Token: 0x040005C2 RID: 1474
		private const string impersonatedSortedEntryFilterElement = "sref";

		// Token: 0x040005C3 RID: 1475
		private const string impersonatedLogonRequiredRolesTypesElement = "lrr";

		// Token: 0x040005C4 RID: 1476
		private RBACContext.RBACContextType contextType;

		// Token: 0x040005C5 RID: 1477
		private string serializedExecutingUser;

		// Token: 0x040005C6 RID: 1478
		[NonSerialized]
		private IIdentity executingUserIdentity;

		// Token: 0x040005C7 RID: 1479
		private IList<RoleType> roleTypeFilter;

		// Token: 0x040005C8 RID: 1480
		private List<RoleEntry> sortedRoleEntryFilter;

		// Token: 0x040005C9 RID: 1481
		private IList<RoleType> logonUserRequiredRoleTypes;

		// Token: 0x040005CA RID: 1482
		private bool callerCheckedAccess;

		// Token: 0x040005CB RID: 1483
		private string impersonatedUserSddl;

		// Token: 0x040005CC RID: 1484
		private string impersonatedAuthenticationType;

		// Token: 0x0200023D RID: 573
		private enum RBACContextType
		{
			// Token: 0x040005D3 RID: 1491
			None,
			// Token: 0x040005D4 RID: 1492
			Delegated = 2,
			// Token: 0x040005D5 RID: 1493
			Windows = 4
		}
	}
}
