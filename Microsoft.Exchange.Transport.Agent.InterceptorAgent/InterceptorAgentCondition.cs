using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public sealed class InterceptorAgentCondition : IComparable<InterceptorAgentCondition>, IEquatable<InterceptorAgentCondition>
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003824 File Offset: 0x00001A24
		public InterceptorAgentCondition(string header, InterceptorAgentConditionMatchType headerMatchType, string value, InterceptorAgentConditionMatchType valueMatchType) : this(InterceptorAgentConditionType.HeaderValue, value, valueMatchType)
		{
			if (string.IsNullOrEmpty(header))
			{
				throw new ArgumentNullException("header");
			}
			if (this.headerType == InterceptorAgentConditionMatchType.Regex)
			{
				this.headerRegex = new Regex(this.header);
			}
			this.header = header;
			this.headerType = headerMatchType;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003878 File Offset: 0x00001A78
		public InterceptorAgentCondition(ServerVersion serverVersion, string str, InterceptorAgentConditionMatchType matchType) : this(InterceptorAgentConditionType.ServerVersion, str, matchType)
		{
			this.serverVersion = serverVersion;
			string message;
			if (!InterceptorAgentCondition.ValidateMatchTypeForServiceVersion(matchType, out message))
			{
				throw new ArgumentException(message);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000038A8 File Offset: 0x00001AA8
		public InterceptorAgentCondition(InterceptorAgentConditionType cond, string str, InterceptorAgentConditionMatchType matchType)
		{
			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentNullException("str");
			}
			switch (cond)
			{
			case InterceptorAgentConditionType.MessageSubject:
			case InterceptorAgentConditionType.EnvelopeFrom:
			case InterceptorAgentConditionType.EnvelopeTo:
			case InterceptorAgentConditionType.MessageId:
			case InterceptorAgentConditionType.HeaderValue:
			case InterceptorAgentConditionType.SmtpClientHostName:
			case InterceptorAgentConditionType.ProcessRole:
			case InterceptorAgentConditionType.ServerVersion:
			case InterceptorAgentConditionType.TenantId:
			case InterceptorAgentConditionType.Directionality:
			case InterceptorAgentConditionType.AccountForest:
				switch (matchType)
				{
				case InterceptorAgentConditionMatchType.CaseInsensitive:
				case InterceptorAgentConditionMatchType.CaseSensitive:
				case InterceptorAgentConditionMatchType.CaseSensitiveEqual:
				case InterceptorAgentConditionMatchType.CaseInsensitiveEqual:
				case InterceptorAgentConditionMatchType.CaseSensitiveNotEqual:
				case InterceptorAgentConditionMatchType.CaseInsensitiveNotEqual:
				case InterceptorAgentConditionMatchType.PatternMatch:
				case InterceptorAgentConditionMatchType.GreaterThan:
				case InterceptorAgentConditionMatchType.GreaterThanOrEqual:
				case InterceptorAgentConditionMatchType.LessThan:
				case InterceptorAgentConditionMatchType.LessThanOrEqual:
					this.matchString = str;
					break;
				case InterceptorAgentConditionMatchType.Regex:
					this.matchRegex = new Regex(str);
					this.matchString = this.matchRegex.ToString();
					break;
				default:
					throw new ArgumentException(string.Format("Unrecognized match type '{0}'", matchType), "matchType");
				}
				this.isMatchStringRedacted = (Util.IsDataRedactionNecessary() && SuppressingPiiData.ContainsRedactedValue(str));
				this.field = cond;
				this.type = matchType;
				return;
			default:
				throw new ArgumentException(string.Format("Unrecognized condition type '{0}'", cond), "cond");
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000039C4 File Offset: 0x00001BC4
		internal InterceptorAgentCondition(ProcessTransportRole[] processRoles, string str, InterceptorAgentConditionMatchType matchType) : this(InterceptorAgentConditionType.ProcessRole, str, matchType)
		{
			this.processRoles = processRoles;
			string message;
			if (!InterceptorAgentCondition.ValidateMatchTypeForProcessRole(matchType, out message))
			{
				throw new ArgumentException(message);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000039F2 File Offset: 0x00001BF2
		private InterceptorAgentCondition()
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000039FA File Offset: 0x00001BFA
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003A02 File Offset: 0x00001C02
		[XmlAttribute("property")]
		public InterceptorAgentConditionType Property
		{
			get
			{
				return this.field;
			}
			set
			{
				this.field = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003A0B File Offset: 0x00001C0B
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003A13 File Offset: 0x00001C13
		[XmlAttribute("matchType")]
		public InterceptorAgentConditionMatchType MatchType
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003A1C File Offset: 0x00001C1C
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003A24 File Offset: 0x00001C24
		[XmlAttribute("value")]
		public string MatchString
		{
			get
			{
				return this.matchString;
			}
			set
			{
				this.matchString = value;
				this.isMatchStringRedacted = (Util.IsDataRedactionNecessary() && SuppressingPiiData.ContainsRedactedValue(value));
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003A43 File Offset: 0x00001C43
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003A4B File Offset: 0x00001C4B
		[XmlAttribute("header")]
		public string HeaderName
		{
			get
			{
				return this.header;
			}
			set
			{
				this.header = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003A54 File Offset: 0x00001C54
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003A5C File Offset: 0x00001C5C
		[XmlAttribute("headerMatchType")]
		public InterceptorAgentConditionMatchType HeaderMatchType
		{
			get
			{
				return this.headerType;
			}
			set
			{
				this.headerType = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003A65 File Offset: 0x00001C65
		internal static string[] AllConditions
		{
			get
			{
				return InterceptorAgentCondition.allConditionNames;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003A6C File Offset: 0x00001C6C
		internal static string[] AllMailDirectionalities
		{
			get
			{
				return InterceptorAgentCondition.allMailDirectionalityNames;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003A73 File Offset: 0x00001C73
		private Regex MatchRegex
		{
			get
			{
				if (this.matchRegex == null && this.MatchType == InterceptorAgentConditionMatchType.Regex)
				{
					this.matchRegex = new Regex(this.matchString);
				}
				return this.matchRegex;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003A9D File Offset: 0x00001C9D
		private Regex HeaderRegex
		{
			get
			{
				if (this.headerRegex == null && this.HeaderMatchType == InterceptorAgentConditionMatchType.Regex)
				{
					this.headerRegex = new Regex(this.HeaderName);
				}
				return this.headerRegex;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003AC7 File Offset: 0x00001CC7
		public bool Equals(InterceptorAgentCondition other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003AD3 File Offset: 0x00001CD3
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InterceptorAgentCondition);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public override int GetHashCode()
		{
			int num = (int)this.Property;
			num ^= (int)this.MatchType;
			num ^= ((this.MatchString != null) ? this.MatchString.GetHashCode() : 0);
			if (this.Property == InterceptorAgentConditionType.HeaderValue)
			{
				num ^= ((this.HeaderName != null) ? this.HeaderName.GetHashCode() : 0);
				num ^= (int)this.HeaderMatchType;
			}
			return num;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003B48 File Offset: 0x00001D48
		public int CompareTo(InterceptorAgentCondition other)
		{
			if (other == null)
			{
				return 1;
			}
			if (object.ReferenceEquals(this, other))
			{
				return 0;
			}
			if (this.Property != other.Property)
			{
				return this.Property - other.Property;
			}
			if (this.MatchType != other.MatchType)
			{
				return this.MatchType - other.MatchType;
			}
			if (this.MatchString != other.MatchString)
			{
				return string.Compare(this.MatchString, other.MatchString, StringComparison.InvariantCulture);
			}
			if (this.Property == InterceptorAgentConditionType.HeaderValue && this.HeaderName != other.HeaderName)
			{
				return string.Compare(this.HeaderName, other.HeaderName, StringComparison.InvariantCulture);
			}
			if (this.Property == InterceptorAgentConditionType.HeaderValue && this.HeaderMatchType != other.HeaderMatchType)
			{
				return this.HeaderMatchType - other.HeaderMatchType;
			}
			return 0;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003C18 File Offset: 0x00001E18
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003C38 File Offset: 0x00001E38
		internal static bool PatternMatch(string pattern, string value)
		{
			if (!pattern.Contains("*"))
			{
				return value.Equals(pattern);
			}
			if (pattern.StartsWith("*") && pattern.EndsWith("*"))
			{
				return InterceptorAgentCondition.InnerPatternMatch(pattern.Trim(new char[]
				{
					'*'
				}), value);
			}
			if (!pattern.StartsWith("*"))
			{
				int num = pattern.IndexOf('*');
				return value.StartsWith(pattern.Substring(0, num), StringComparison.InvariantCultureIgnoreCase) && InterceptorAgentCondition.PatternMatch(pattern.Substring(num, pattern.Length - num), value.Substring(num));
			}
			int num2 = pattern.LastIndexOf('*');
			return value.EndsWith(pattern.Substring(num2 + 1), StringComparison.InvariantCultureIgnoreCase) && InterceptorAgentCondition.PatternMatch(pattern.Substring(0, num2 + 1), value.Substring(0, value.Length - pattern.Length + num2 + 1));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003D18 File Offset: 0x00001F18
		internal static bool ValidateProcessRole(InterceptorAgentConditionMatchType type, string matchString, out ProcessTransportRole[] roles, out string error)
		{
			roles = null;
			if (!InterceptorAgentCondition.ValidateMatchTypeForProcessRole(type, out error))
			{
				return false;
			}
			string[] array = matchString.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 0)
			{
				error = "ProcessRole not specified in condition";
				return false;
			}
			List<ProcessTransportRole> list = new List<ProcessTransportRole>();
			foreach (string text in array)
			{
				ProcessTransportRole item;
				if (!EnumValidator.TryParse<ProcessTransportRole>(text, EnumParseOptions.IgnoreCase, out item))
				{
					error = string.Format("Invalid ProcessRole {0} in condition. Allowed values are {1}", text, string.Join(",", Enum.GetNames(typeof(ProcessTransportRole))));
					return false;
				}
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			roles = list.ToArray();
			error = null;
			return true;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003DD0 File Offset: 0x00001FD0
		internal static bool ValidateServerVersion(InterceptorAgentConditionMatchType type, string matchString, out ServerVersion serverVersion, out string error)
		{
			serverVersion = null;
			if (!InterceptorAgentCondition.ValidateMatchTypeForServiceVersion(type, out error))
			{
				return false;
			}
			if (!ServerVersion.TryParseFromSerialNumber(matchString, out serverVersion))
			{
				Version version;
				if (!Version.TryParse(matchString, out version))
				{
					error = string.Format("Invalid serial number {0} in condition. Serial Number should be a valid ServerVersion like 15.00.0649.000 or Version 15.0 (Build 649.0)", matchString);
					return false;
				}
				serverVersion = new ServerVersion(version.Major, version.Minor, version.Build, version.Revision);
			}
			error = null;
			return true;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003E34 File Offset: 0x00002034
		internal static bool ValidateCondition(InterceptorAgentConditionType conditionType, InterceptorAgentConditionMatchType matchType, out string error)
		{
			if (conditionType != InterceptorAgentConditionType.ServerVersion)
			{
				switch (matchType)
				{
				case InterceptorAgentConditionMatchType.GreaterThan:
				case InterceptorAgentConditionMatchType.GreaterThanOrEqual:
				case InterceptorAgentConditionMatchType.LessThan:
				case InterceptorAgentConditionMatchType.LessThanOrEqual:
					error = "GreaterThan, GreaterThanOrEqual, LessThan and LessThanOrEqual are only allowed for Condition ServerVersion";
					return false;
				}
			}
			error = null;
			return true;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003E70 File Offset: 0x00002070
		internal static bool ValidateConditionTypeValue(InterceptorAgentConditionType conditionType, string value, out string error)
		{
			if (string.IsNullOrEmpty(value))
			{
				error = InterceptorAgentStrings.ConditionTypeValueCannotBeNullOrEmpty;
				return false;
			}
			switch (conditionType)
			{
			case InterceptorAgentConditionType.TenantId:
			{
				Guid guid;
				if (!Guid.TryParse(value, out guid))
				{
					error = InterceptorAgentStrings.ConditionTypeValueInvalidTenantIdGuid;
					return false;
				}
				break;
			}
			case InterceptorAgentConditionType.Directionality:
			{
				MailDirectionality mailDirectionality;
				if (!InterceptorAgentCondition.ValidateDirectionality(value, out mailDirectionality))
				{
					error = InterceptorAgentStrings.ConditionTypeValueInvalidDirectionalityType(string.Join(",", InterceptorAgentCondition.AllMailDirectionalities));
					return false;
				}
				break;
			}
			}
			error = null;
			return true;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003EEB File Offset: 0x000020EB
		internal static bool ValidateDirectionality(string value, out MailDirectionality directionality)
		{
			return EnumValidator.TryParse<MailDirectionality>(value, EnumParseOptions.AllowNumericConstants | EnumParseOptions.IgnoreCase, out directionality);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003EF8 File Offset: 0x000020F8
		internal bool MatchRoleAndServerVersion(ProcessTransportRole role, ServerVersion version)
		{
			if (this.field == InterceptorAgentConditionType.ProcessRole)
			{
				if (this.processRoles == null)
				{
					throw new InvalidOperationException("ProcessRoles is not initialized in InterceptorAgentCondition");
				}
				foreach (ProcessTransportRole processTransportRole in this.processRoles)
				{
					if (role == processTransportRole)
					{
						return true;
					}
				}
				return false;
			}
			else
			{
				if (this.field != InterceptorAgentConditionType.ServerVersion)
				{
					return true;
				}
				if (this.serverVersion == null)
				{
					throw new InvalidOperationException("ServerVersion is not initialized in InterceptorAgentCondition");
				}
				int num = ServerVersion.Compare(version, this.serverVersion);
				switch (this.type)
				{
				case InterceptorAgentConditionMatchType.CaseSensitiveEqual:
				case InterceptorAgentConditionMatchType.CaseInsensitiveEqual:
					return num == 0;
				case InterceptorAgentConditionMatchType.GreaterThan:
					return num > 0;
				case InterceptorAgentConditionMatchType.GreaterThanOrEqual:
					return num >= 0;
				case InterceptorAgentConditionMatchType.LessThan:
					return num < 0;
				case InterceptorAgentConditionMatchType.LessThanOrEqual:
					return num <= 0;
				}
				return false;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003FE0 File Offset: 0x000021E0
		internal bool IsMatch(string subject, string envelopeFrom, string messageId, EnvelopeRecipientCollection recipients, RoutingAddress envelopeTo, HeaderList headers, string smtpClientHostName, Guid tenantId, MailDirectionality directionality, string accountForest)
		{
			switch (this.field)
			{
			case InterceptorAgentConditionType.MessageSubject:
				return this.Match(this.isMatchStringRedacted ? Util.Redact(subject.ToUpperInvariant()) : subject);
			case InterceptorAgentConditionType.EnvelopeFrom:
				return this.Match(this.isMatchStringRedacted ? Util.Redact(envelopeFrom) : envelopeFrom);
			case InterceptorAgentConditionType.EnvelopeTo:
				if (envelopeTo.IsValid)
				{
					return this.Match(this.isMatchStringRedacted ? Util.Redact(envelopeTo) : envelopeTo.ToString());
				}
				foreach (EnvelopeRecipient envelopeRecipient in recipients)
				{
					if (this.Match(this.isMatchStringRedacted ? Util.Redact(envelopeRecipient.Address) : envelopeRecipient.Address.ToString()))
					{
						return true;
					}
				}
				return false;
			case InterceptorAgentConditionType.MessageId:
				return this.Match(messageId);
			case InterceptorAgentConditionType.HeaderValue:
				return this.MatchHeader(headers);
			case InterceptorAgentConditionType.SmtpClientHostName:
				return this.Match(smtpClientHostName);
			case InterceptorAgentConditionType.ProcessRole:
			case InterceptorAgentConditionType.ServerVersion:
				return true;
			case InterceptorAgentConditionType.TenantId:
				return this.Match(tenantId.ToString());
			case InterceptorAgentConditionType.Directionality:
				return this.Match(directionality.ToString());
			case InterceptorAgentConditionType.AccountForest:
				return this.Match(accountForest);
			}
			return false;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004158 File Offset: 0x00002358
		internal void Verify()
		{
			try
			{
				if (this.headerType == InterceptorAgentConditionMatchType.Regex && this.HeaderRegex == null)
				{
					throw new InvalidOperationException("Missing header regex for regex matchType");
				}
				if (this.type == InterceptorAgentConditionMatchType.Regex && this.MatchRegex == null)
				{
					throw new InvalidOperationException("Missing value regex for regex matchType");
				}
				string message;
				if (this.field == InterceptorAgentConditionType.ProcessRole && !InterceptorAgentCondition.ValidateProcessRole(this.type, this.matchString, out this.processRoles, out message))
				{
					throw new InvalidOperationException(message);
				}
				if (this.field == InterceptorAgentConditionType.ServerVersion && !InterceptorAgentCondition.ValidateServerVersion(this.type, this.matchString, out this.serverVersion, out message))
				{
					throw new InvalidOperationException(message);
				}
				if (!InterceptorAgentCondition.ValidateCondition(this.field, this.type, out message))
				{
					throw new InvalidOperationException(message);
				}
			}
			catch (ArgumentException)
			{
				throw new InvalidOperationException("Value is not a valid regex");
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004230 File Offset: 0x00002430
		internal void ToString(StringBuilder result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			result.AppendFormat("property=\"{0}\" ", this.field);
			if (this.field == InterceptorAgentConditionType.HeaderValue)
			{
				result.AppendFormat("header=\"{0}\" headerMatchType=\"{1}\"", this.header, this.HeaderMatchType);
			}
			result.AppendFormat("value=\"{0}\" matchType=\"{1}\" ", this.matchString, this.type.ToString());
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000042AC File Offset: 0x000024AC
		private static bool InnerPatternMatch(string pattern, string value)
		{
			if (!pattern.Contains("*"))
			{
				return value.IndexOf(pattern, StringComparison.InvariantCultureIgnoreCase) != -1;
			}
			int num = pattern.IndexOf('*');
			int num2 = value.IndexOf(pattern.Substring(0, num), StringComparison.InvariantCultureIgnoreCase);
			return num2 != -1 && InterceptorAgentCondition.InnerPatternMatch(pattern.Substring(num + 1, pattern.Length - num - 1), value.Substring(num2 + num));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004315 File Offset: 0x00002515
		private static bool ValidateMatchTypeForProcessRole(InterceptorAgentConditionMatchType type, out string error)
		{
			if (type != InterceptorAgentConditionMatchType.CaseSensitiveEqual && type != InterceptorAgentConditionMatchType.CaseInsensitiveEqual)
			{
				error = "CaseSensitiveEqual and CaseInsensitiveEqual are the only match types allowed for condition ProcessRole";
				return false;
			}
			error = null;
			return true;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000432C File Offset: 0x0000252C
		private static bool ValidateMatchTypeForServiceVersion(InterceptorAgentConditionMatchType type, out string error)
		{
			switch (type)
			{
			case InterceptorAgentConditionMatchType.CaseSensitiveEqual:
			case InterceptorAgentConditionMatchType.CaseInsensitiveEqual:
			case InterceptorAgentConditionMatchType.GreaterThan:
			case InterceptorAgentConditionMatchType.GreaterThanOrEqual:
			case InterceptorAgentConditionMatchType.LessThan:
			case InterceptorAgentConditionMatchType.LessThanOrEqual:
				error = null;
				return true;
			}
			error = "CaseSensitiveEqual, CaseInsensitiveEqual, GreaterThan, GreaterThanOrEqual, LessThan and LessThanOrEqual are the only match types allowed for condition ServerVersion";
			return false;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000437C File Offset: 0x0000257C
		private bool MatchHeaderValue(Header header)
		{
			if (!string.IsNullOrEmpty(header.Value))
			{
				return this.Match(header.Value);
			}
			AddressHeader addressHeader = header as AddressHeader;
			if (addressHeader != null)
			{
				foreach (AddressItem addressItem in addressHeader)
				{
					MimeRecipient mimeRecipient = addressItem as MimeRecipient;
					if (mimeRecipient != null)
					{
						if (this.Match(((RoutingAddress)mimeRecipient.Email).ToString()))
						{
							return true;
						}
					}
					else
					{
						MimeGroup mimeGroup = addressItem as MimeGroup;
						if (mimeGroup != null)
						{
							foreach (MimeRecipient mimeRecipient2 in mimeGroup)
							{
								if (this.Match(((RoutingAddress)mimeRecipient2.Email).ToString()))
								{
									return true;
								}
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004494 File Offset: 0x00002694
		private bool Match(string toCompare)
		{
			if (toCompare == null)
			{
				return false;
			}
			bool flag;
			switch (this.type)
			{
			case InterceptorAgentConditionMatchType.CaseInsensitive:
			case InterceptorAgentConditionMatchType.CaseInsensitiveEqual:
			case InterceptorAgentConditionMatchType.CaseInsensitiveNotEqual:
				flag = string.Equals(this.matchString, toCompare, StringComparison.InvariantCultureIgnoreCase);
				break;
			case InterceptorAgentConditionMatchType.CaseSensitive:
			case InterceptorAgentConditionMatchType.CaseSensitiveEqual:
			case InterceptorAgentConditionMatchType.CaseSensitiveNotEqual:
				flag = string.Equals(this.matchString, toCompare, StringComparison.InvariantCulture);
				break;
			case InterceptorAgentConditionMatchType.Regex:
				return this.matchRegex.IsMatch(toCompare);
			case InterceptorAgentConditionMatchType.PatternMatch:
				return InterceptorAgentCondition.PatternMatch(this.matchString, toCompare);
			default:
				throw new InvalidOperationException(string.Format("Unrecognized MatchType '{0}'", this.type));
			}
			switch (this.type)
			{
			case InterceptorAgentConditionMatchType.CaseInsensitive:
			case InterceptorAgentConditionMatchType.CaseSensitive:
			case InterceptorAgentConditionMatchType.CaseSensitiveEqual:
			case InterceptorAgentConditionMatchType.CaseInsensitiveEqual:
				return flag;
			case InterceptorAgentConditionMatchType.CaseSensitiveNotEqual:
			case InterceptorAgentConditionMatchType.CaseInsensitiveNotEqual:
				return !flag;
			default:
				return false;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000455C File Offset: 0x0000275C
		private bool MatchHeader(HeaderList headers)
		{
			if (headers == null)
			{
				return false;
			}
			if (this.HeaderMatchType == InterceptorAgentConditionMatchType.Regex)
			{
				using (MimeNode.Enumerator<Header> enumerator = headers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Header header = enumerator.Current;
						if (this.headerRegex.IsMatch(header.Name) && this.MatchHeaderValue(header))
						{
							return true;
						}
					}
					return false;
				}
			}
			if (this.HeaderMatchType == InterceptorAgentConditionMatchType.PatternMatch)
			{
				using (MimeNode.Enumerator<Header> enumerator2 = headers.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Header header2 = enumerator2.Current;
						if (InterceptorAgentCondition.PatternMatch(this.header, header2.Name) && this.MatchHeaderValue(header2))
						{
							return true;
						}
					}
					return false;
				}
			}
			Header[] array = headers.FindAll(this.header);
			foreach (Header header3 in array)
			{
				if (this.MatchHeaderValue(header3))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000050 RID: 80
		private static string[] allConditionNames = Enum.GetNames(typeof(InterceptorAgentConditionType));

		// Token: 0x04000051 RID: 81
		private static string[] allMailDirectionalityNames = Enum.GetNames(typeof(MailDirectionality));

		// Token: 0x04000052 RID: 82
		private Regex matchRegex;

		// Token: 0x04000053 RID: 83
		private string header;

		// Token: 0x04000054 RID: 84
		private Regex headerRegex;

		// Token: 0x04000055 RID: 85
		private InterceptorAgentConditionType field;

		// Token: 0x04000056 RID: 86
		private InterceptorAgentConditionMatchType type;

		// Token: 0x04000057 RID: 87
		private InterceptorAgentConditionMatchType headerType;

		// Token: 0x04000058 RID: 88
		private string matchString;

		// Token: 0x04000059 RID: 89
		private bool isMatchStringRedacted;

		// Token: 0x0400005A RID: 90
		private ServerVersion serverVersion;

		// Token: 0x0400005B RID: 91
		private ProcessTransportRole[] processRoles;
	}
}
