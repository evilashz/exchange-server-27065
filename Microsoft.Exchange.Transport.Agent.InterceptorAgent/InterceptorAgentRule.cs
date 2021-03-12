using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000012 RID: 18
	[XmlRoot("Rule")]
	public sealed class InterceptorAgentRule : IConfigurable, IEquatable<InterceptorAgentRule>
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000046A2 File Offset: 0x000028A2
		public InterceptorAgentRule()
		{
			this.conditions = new List<InterceptorAgentCondition>();
			this.events = InterceptorAgentEvent.Invalid;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000046D2 File Offset: 0x000028D2
		internal InterceptorAgentRule(string name, string description, List<InterceptorAgentCondition> conditions, InterceptorAgentAction action, InterceptorAgentEvent evt) : this(name, description, conditions, action, evt, SourceType.User, InterceptorAgentRule.DefaultUser)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000046E8 File Offset: 0x000028E8
		internal InterceptorAgentRule(string name, string description, List<InterceptorAgentCondition> conditions, InterceptorAgentAction action, InterceptorAgentEvent evt, SourceType source, string createdBy)
		{
			this.Name = name;
			this.description = description;
			this.conditions = conditions;
			this.events = evt;
			this.action = action;
			this.Source = source;
			this.CreatedBy = createdBy;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004746 File Offset: 0x00002946
		public static IEnumerable<KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>> InvalidConditionEventPairs
		{
			get
			{
				return InterceptorAgentRule.invalidConditionEventPairs;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000474D File Offset: 0x0000294D
		public static IEnumerable<KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>> InvalidActionEventPairs
		{
			get
			{
				return InterceptorAgentRule.invalidActionEventPairs;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004754 File Offset: 0x00002954
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000475C File Offset: 0x0000295C
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004765 File Offset: 0x00002965
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x0000476D File Offset: 0x0000296D
		[XmlAttribute]
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004776 File Offset: 0x00002976
		[XmlIgnore]
		public DateTime ExpireTime
		{
			get
			{
				return this.expireTime;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000477E File Offset: 0x0000297E
		[XmlIgnore]
		public DateTime ExpireTimeUtc
		{
			get
			{
				return this.expireTimeUtc;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004786 File Offset: 0x00002986
		[XmlIgnore]
		public MultiValuedProperty<ADObjectId> Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000478E File Offset: 0x0000298E
		[XmlIgnore]
		public Version RuleVersion
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004796 File Offset: 0x00002996
		// (set) Token: 0x060000BF RID: 191 RVA: 0x0000479E File Offset: 0x0000299E
		[XmlAttribute("Event")]
		public InterceptorAgentEvent Events
		{
			get
			{
				return this.events;
			}
			set
			{
				this.events = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000047A7 File Offset: 0x000029A7
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000047AF File Offset: 0x000029AF
		[XmlAttribute("Source")]
		public SourceType Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000047B8 File Offset: 0x000029B8
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000047C0 File Offset: 0x000029C0
		[XmlAttribute("CreatedBy")]
		public string CreatedBy
		{
			get
			{
				return this.createdBy;
			}
			set
			{
				this.createdBy = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000047C9 File Offset: 0x000029C9
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000047D1 File Offset: 0x000029D1
		[XmlArray]
		[XmlArrayItem("Condition")]
		public List<InterceptorAgentCondition> Conditions
		{
			get
			{
				return this.conditions;
			}
			set
			{
				this.conditions = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000047DA File Offset: 0x000029DA
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000047E2 File Offset: 0x000029E2
		public InterceptorAgentAction Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000047EB File Offset: 0x000029EB
		[XmlIgnore]
		public bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000047EE File Offset: 0x000029EE
		[XmlIgnore]
		public ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000047F6 File Offset: 0x000029F6
		[XmlIgnore]
		public ObjectState ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000047F9 File Offset: 0x000029F9
		[XmlIgnore]
		public string DistinguishedName
		{
			get
			{
				return this.distinguishedName;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004801 File Offset: 0x00002A01
		[XmlIgnore]
		public ADObjectId ObjectCategory
		{
			get
			{
				return this.objectCategory;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004809 File Offset: 0x00002A09
		[XmlIgnore]
		public string AdminDisplayName
		{
			get
			{
				return this.adminDisplayName;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004811 File Offset: 0x00002A11
		[XmlIgnore]
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004819 File Offset: 0x00002A19
		[XmlIgnore]
		public DateTime? WhenChangedUtc
		{
			get
			{
				return this.whenChangedUtc;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004821 File Offset: 0x00002A21
		[XmlIgnore]
		public DateTime? WhenChanged
		{
			get
			{
				return this.whenChanged;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004829 File Offset: 0x00002A29
		[XmlIgnore]
		public DateTime? WhenCreatedUtc
		{
			get
			{
				return this.whenCreatedUtc;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004831 File Offset: 0x00002A31
		[XmlIgnore]
		public DateTime? WhenCreated
		{
			get
			{
				return this.whenCreated;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000483C File Offset: 0x00002A3C
		public static InterceptorAgentRule CreateRuleFromXml(string xml)
		{
			InterceptorAgentRule result;
			using (StringReader stringReader = new StringReader(xml))
			{
				InterceptorAgentRule interceptorAgentRule = (InterceptorAgentRule)InterceptorAgentRule.serializer.Deserialize(stringReader);
				interceptorAgentRule.Verify();
				result = interceptorAgentRule;
			}
			return result;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004888 File Offset: 0x00002A88
		public string ToXmlString()
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(InterceptorAgentRule));
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				xmlSerializer.Serialize(stringWriter, this);
				result = stringWriter.GetStringBuilder().ToString();
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000048DC File Offset: 0x00002ADC
		public bool IsMatch(string subject, string envelopeFrom, string messageId, EnvelopeRecipientCollection recipients, RoutingAddress envelopeTo, HeaderList headers, string smtpClientHostName, Guid tenantId, MailDirectionality directionality, string accountForest)
		{
			foreach (InterceptorAgentCondition interceptorAgentCondition in this.conditions)
			{
				if (!interceptorAgentCondition.IsMatch(subject, envelopeFrom, messageId, recipients, envelopeTo, headers, smtpClientHostName, tenantId, directionality, accountForest))
				{
					return false;
				}
			}
			if (this.ExpireTime < DateTime.UtcNow + InterceptorAgentRule.RuleExpirationWarningInterval)
			{
				Util.EventLog.LogEvent(TransportEventLogConstants.Tuple_InterceptorRuleNearingExpiration, this.name, new object[]
				{
					this.name,
					this.ExpireTime.ToLocalTime()
				});
				EventNotificationItem.Publish(ExchangeComponent.FfoTransport.Name, "InterceptorRuleNearingExpiration", null, string.Format("The interceptor rule {0} is nearing expiration.", this.name), ResultSeverityLevel.Warning, false);
			}
			return true;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000049CC File Offset: 0x00002BCC
		public void ToString(StringBuilder result)
		{
			result.AppendFormat("<rule name=\"{0}\" description=\"{1}\" Source = \"{2}\" CreatedBy = \"{3}\" >\n", new object[]
			{
				this.name,
				this.description,
				this.source.ToString(),
				this.createdBy
			});
			result.Append("<conditions>\n");
			foreach (InterceptorAgentCondition interceptorAgentCondition in this.conditions)
			{
				result.Append("\t <condition ");
				interceptorAgentCondition.ToString(result);
				result.Append("/>\n");
			}
			result.Append("</conditions>\n");
			result.Append("<actions>\n");
			result.Append("\t <");
			this.action.ToString(result);
			result.AppendFormat(" event=\"{0}\" />\n", this.events.ToString());
			result.Append("</actions>\n");
			result.Append("</rule>\n");
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004AEC File Offset: 0x00002CEC
		public ValidationError[] Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004AF4 File Offset: 0x00002CF4
		public void ActivateRule()
		{
			if (this.isActive)
			{
				return;
			}
			this.ruleStartTime = new MemoryCounter("Rule Start Time")
			{
				RawValue = Stopwatch.GetTimestamp()
			};
			this.lastActionTakenTime = new MemoryCounter("Last Action Taken Time")
			{
				RawValue = Stopwatch.GetTimestamp()
			};
			this.eventPerfCountersGroup = new InterceptorCountersGroup(this.Events);
			this.actionPerfCountersGroup = new InterceptorCountersGroup(this.Action.Action);
			this.isActive = true;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004B72 File Offset: 0x00002D72
		public void DeactivateRule()
		{
			if (!this.isActive)
			{
				return;
			}
			this.eventPerfCountersGroup.StopTracking();
			this.actionPerfCountersGroup.StopTracking();
			this.isActive = false;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004B9C File Offset: 0x00002D9C
		public bool Equals(InterceptorAgentRule other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(this, other) || (!(this.version != other.version) && !(this.Name != other.Name) && !(this.Description != other.description) && this.source == other.source && !(this.createdBy != other.createdBy) && this.Events == other.Events && !(this.ExpireTimeUtc != other.ExpireTimeUtc) && this.Action.Equals(other.Action) && (this.Target != null || other.Target == null) && (this.Target == null || this.Target.Equals(other.Target)) && this.ConditionsEqual(other)));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004C93 File Offset: 0x00002E93
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InterceptorAgentRule);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004CA1 File Offset: 0x00002EA1
		public override int GetHashCode()
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004CBD File Offset: 0x00002EBD
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004CBF File Offset: 0x00002EBF
		void IConfigurable.ResetChangeTracking()
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004CC4 File Offset: 0x00002EC4
		internal static string ToString(IEnumerable<InterceptorAgentRule> rules)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\n<rules>\n");
			if (rules != null)
			{
				foreach (InterceptorAgentRule interceptorAgentRule in rules)
				{
					interceptorAgentRule.ToString(stringBuilder);
				}
			}
			stringBuilder.Append("</rules>");
			return stringBuilder.ToString();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004D34 File Offset: 0x00002F34
		internal static InterceptorAgentRule InternalEvaluate(IEnumerable<InterceptorAgentRule> rules, InterceptorAgentEvent evt, string subject, string envelopeFrom, string messageId, EnvelopeRecipientCollection recipients, RoutingAddress envelopeTo, HeaderList headers, string smtpClientHostName, Guid tenantId, MailDirectionality directionality, string accountForest)
		{
			InterceptorAgentRule interceptorAgentRule = null;
			foreach (InterceptorAgentRule interceptorAgentRule2 in rules)
			{
				if ((ushort)(interceptorAgentRule2.Events & evt) != 0)
				{
					interceptorAgentRule2.eventPerfCountersGroup.Increment(evt, false);
					if (interceptorAgentRule2.IsMatch(subject, envelopeFrom, messageId, recipients, envelopeTo, headers, smtpClientHostName, tenantId, directionality, accountForest))
					{
						interceptorAgentRule = interceptorAgentRule2;
						interceptorAgentRule.eventPerfCountersGroup.Increment(evt, true);
						break;
					}
				}
			}
			return interceptorAgentRule;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004DBC File Offset: 0x00002FBC
		internal bool MatchRoleAndServerVersion(ProcessTransportRole role, ServerVersion version)
		{
			foreach (InterceptorAgentCondition interceptorAgentCondition in this.conditions)
			{
				if (!interceptorAgentCondition.MatchRoleAndServerVersion(role, version))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004E1C File Offset: 0x0000301C
		internal void AddCondition(InterceptorAgentCondition condition)
		{
			this.conditions.Add(condition);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004E2A File Offset: 0x0000302A
		internal void SetAction(InterceptorAgentEvent evt, InterceptorAgentRuleBehavior action)
		{
			this.SetAction(new InterceptorAgentAction(action));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004E38 File Offset: 0x00003038
		internal void SetAction(InterceptorAgentAction action)
		{
			if (this.action != null)
			{
				throw new InvalidOperationException("Only 1 action allowed");
			}
			this.action = action;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004E54 File Offset: 0x00003054
		internal InterceptorAgentRuleBehavior PerformAction(MailItem mailItem, Action drop, Action<SmtpResponse> reject, Action<TimeSpan> defer = null)
		{
			InterceptorAgentRuleBehavior interceptorAgentRuleBehavior = this.Action.PerformAction(this, mailItem, drop, reject, defer);
			this.IncrementActionPerfCounter(interceptorAgentRuleBehavior);
			return interceptorAgentRuleBehavior;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004E7C File Offset: 0x0000307C
		internal string GetSourceContext(string agentName, InterceptorAgentEvent evt, bool includeResponseString)
		{
			string result;
			if (!includeResponseString || string.IsNullOrEmpty(this.Action.ResponseString))
			{
				result = string.Format("Interceptor Agent = {0}; Interceptor rule '{1}\\{2} Message' {3}", new object[]
				{
					agentName,
					this.Name,
					this.Action.Action.ToString(),
					evt.ToString()
				});
			}
			else
			{
				result = string.Format("Interceptor Agent = {0}; Interceptor rule '{1}\\{2} Message' {3}; <{4}>", new object[]
				{
					agentName,
					this.Name,
					this.Action.Action.ToString(),
					evt.ToString(),
					this.Action.ResponseString
				});
			}
			return result;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004F3C File Offset: 0x0000313C
		internal void SetPropertiesFromAdObjet(InterceptorRule adRule)
		{
			this.identity = adRule.Identity;
			if (!Version.TryParse(adRule.Version, out this.version))
			{
				throw new InvalidOperationException(string.Format("Invalid version property: {0}", adRule.Version));
			}
			this.target = adRule.Target;
			this.expireTimeUtc = adRule.ExpireTimeUtc;
			this.expireTime = adRule.ExpireTime;
			this.distinguishedName = adRule.DistinguishedName;
			this.objectCategory = adRule.ObjectCategory;
			this.guid = adRule.Guid;
			this.adminDisplayName = adRule.AdminDisplayName;
			this.whenCreatedUtc = adRule.WhenCreatedUTC;
			this.whenCreated = adRule.WhenCreated;
			this.whenChangedUtc = adRule.WhenChangedUTC;
			this.whenChanged = adRule.WhenChanged;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005004 File Offset: 0x00003204
		internal XElement GetDiagnosticInfoOfPerfCounters()
		{
			XElement xelement = new XElement("PerfCounters");
			this.eventPerfCountersGroup.GetDiagnosticInfo(xelement);
			this.actionPerfCountersGroup.GetDiagnosticInfo(xelement);
			TimeSpan timeSpan = new TimeSpan(Stopwatch.GetTimestamp() - this.ruleStartTime.RawValue);
			xelement.Add(new XElement("RuleStartElapsedTime", timeSpan.ToString()));
			timeSpan = new TimeSpan(Stopwatch.GetTimestamp() - this.lastActionTakenTime.RawValue);
			xelement.Add(new XElement("LastActionTakenElapsedTime", timeSpan.ToString()));
			return xelement;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000050B0 File Offset: 0x000032B0
		private void IncrementActionPerfCounter(InterceptorAgentRuleBehavior actions)
		{
			if (actions.HasFlag(InterceptorAgentRuleBehavior.Archive))
			{
				this.actionPerfCountersGroup.Increment(InterceptorAgentRuleBehavior.Archive);
			}
			else if (actions.HasFlag(InterceptorAgentRuleBehavior.ArchiveHeaders))
			{
				this.actionPerfCountersGroup.Increment(InterceptorAgentRuleBehavior.ArchiveHeaders);
			}
			this.actionPerfCountersGroup.Increment(actions & ~(InterceptorAgentRuleBehavior.Archive | InterceptorAgentRuleBehavior.ArchiveHeaders));
			this.lastActionTakenTime.RawValue = Stopwatch.GetTimestamp();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005124 File Offset: 0x00003324
		private void Verify()
		{
			foreach (KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent> keyValuePair in InterceptorAgentRule.InvalidConditionEventPairs)
			{
				if ((ushort)(keyValuePair.Value & this.Events) != 0)
				{
					foreach (InterceptorAgentCondition interceptorAgentCondition in this.Conditions)
					{
						if (keyValuePair.Key == interceptorAgentCondition.Property)
						{
							string message = string.Format("Condition '{0}' does not support event '{1}'", interceptorAgentCondition.Property, keyValuePair.Value);
							throw new InvalidOperationException(message);
						}
					}
				}
			}
			foreach (KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent> keyValuePair2 in InterceptorAgentRule.InvalidActionEventPairs)
			{
				if ((ushort)(keyValuePair2.Value & this.Events) != 0)
				{
					InterceptorAgentAction interceptorAgentAction = this.Action;
					if (interceptorAgentAction != null && keyValuePair2.Key == interceptorAgentAction.Action)
					{
						string message = string.Format("Action '{0}' does not support event '{1}'", interceptorAgentAction.Action, keyValuePair2.Value & this.Events);
						throw new InvalidOperationException(message);
					}
				}
			}
			foreach (InterceptorAgentCondition interceptorAgentCondition2 in this.conditions)
			{
				interceptorAgentCondition2.Verify();
			}
			this.action.Verify();
			this.VerifyRequiredFields();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000052E8 File Offset: 0x000034E8
		private void VerifyRequiredFields()
		{
			string text = null;
			if (string.IsNullOrEmpty(this.Name))
			{
				text = "Name";
			}
			else if (this.Action == null)
			{
				text = "Action";
			}
			else if (this.Events == InterceptorAgentEvent.Invalid)
			{
				text = "Event";
			}
			else if (this.Conditions == null)
			{
				text = "Conditions";
			}
			else if (string.IsNullOrEmpty(this.Description))
			{
				text = "Description";
			}
			if (!string.IsNullOrEmpty(text))
			{
				throw new MissingMemberException(string.Format("'{0}' is missing. This rule can not be used on this server.", text));
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005368 File Offset: 0x00003568
		private bool ConditionsEqual(InterceptorAgentRule other)
		{
			ArgumentValidator.ThrowIfNull("other", other);
			return this.conditions.Count == other.conditions.Count && !this.conditions.Except(other.conditions).Any<InterceptorAgentCondition>() && !other.conditions.Except(this.conditions).Any<InterceptorAgentCondition>();
		}

		// Token: 0x0400006F RID: 111
		internal const string RuleStartTime = "Rule Start Time";

		// Token: 0x04000070 RID: 112
		internal const string LastActionTakenTime = "Last Action Taken Time";

		// Token: 0x04000071 RID: 113
		internal static readonly Version Version = new Version(1, 2);

		// Token: 0x04000072 RID: 114
		internal static readonly string DefaultUser = "NotSpecified";

		// Token: 0x04000073 RID: 115
		private static readonly TimeSpan RuleExpirationWarningInterval = TimeSpan.FromHours(24.0);

		// Token: 0x04000074 RID: 116
		private static readonly InterceptorAgentEvent CanDropOrDeferEvents = InterceptorAgentEvent.OnSubmittedMessage | InterceptorAgentEvent.OnResolvedMessage | InterceptorAgentEvent.OnRoutedMessage | InterceptorAgentEvent.OnCategorizedMessage;

		// Token: 0x04000075 RID: 117
		private static readonly InterceptorAgentEvent CanTransientlyRejectEvents = InterceptorAgentEvent.OnMailFrom | InterceptorAgentEvent.OnRcptTo | InterceptorAgentEvent.OnEndOfHeaders | InterceptorAgentEvent.OnEndOfData;

		// Token: 0x04000076 RID: 118
		private static readonly InterceptorAgentEvent CanDropEvents = (InterceptorAgentEvent)((ushort)(InterceptorAgentRule.CanDropOrDeferEvents | InterceptorAgentEvent.OnLoadedMessage) | 4 | 8 | 256 | 8192 | 1024);

		// Token: 0x04000077 RID: 119
		private static readonly InterceptorAgentEvent CanArchiveEvents = InterceptorAgentEvent.OnEndOfData | InterceptorAgentEvent.OnSubmittedMessage | InterceptorAgentEvent.OnResolvedMessage | InterceptorAgentEvent.OnRoutedMessage | InterceptorAgentEvent.OnCategorizedMessage | InterceptorAgentEvent.OnInitMsg | InterceptorAgentEvent.OnPromotedMessage | InterceptorAgentEvent.OnCreatedMessage | InterceptorAgentEvent.OnDemotedMessage;

		// Token: 0x04000078 RID: 120
		private static readonly InterceptorAgentEvent CanArchiveHeadersEvents = InterceptorAgentRule.CanArchiveEvents | InterceptorAgentEvent.OnEndOfHeaders;

		// Token: 0x04000079 RID: 121
		private static readonly InterceptorAgentEvent CanArchiveAndTransientRejectEvents = InterceptorAgentRule.CanArchiveEvents & InterceptorAgentRule.CanTransientlyRejectEvents;

		// Token: 0x0400007A RID: 122
		private static readonly InterceptorAgentEvent CanArchiveAndDropEvents = InterceptorAgentRule.CanArchiveEvents & InterceptorAgentRule.CanDropEvents;

		// Token: 0x0400007B RID: 123
		private static readonly InterceptorAgentEvent CanArchiveHeadersAndDropEvents = InterceptorAgentRule.CanArchiveHeadersEvents & InterceptorAgentRule.CanDropEvents;

		// Token: 0x0400007C RID: 124
		private static readonly InterceptorAgentEvent CanArchiveHeadersAndTransientRejectEvents = InterceptorAgentRule.CanArchiveHeadersEvents & InterceptorAgentRule.CanTransientlyRejectEvents;

		// Token: 0x0400007D RID: 125
		private static readonly List<KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>> invalidConditionEventPairs = new List<KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>>
		{
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.MessageSubject, InterceptorAgentEvent.OnMailFrom),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.MessageSubject, InterceptorAgentEvent.OnRcptTo),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.MessageId, InterceptorAgentEvent.OnMailFrom),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.MessageId, InterceptorAgentEvent.OnRcptTo),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.EnvelopeTo, InterceptorAgentEvent.OnMailFrom),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.HeaderName, InterceptorAgentEvent.OnMailFrom),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.HeaderValue, InterceptorAgentEvent.OnMailFrom),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.HeaderName, InterceptorAgentEvent.OnRcptTo),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.HeaderValue, InterceptorAgentEvent.OnRcptTo),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.HeaderName, InterceptorAgentEvent.OnLoadedMessage),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.HeaderValue, InterceptorAgentEvent.OnLoadedMessage),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.SmtpClientHostName, InterceptorAgentEvent.OnSubmittedMessage),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.SmtpClientHostName, InterceptorAgentEvent.OnResolvedMessage),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.SmtpClientHostName, InterceptorAgentEvent.OnRoutedMessage),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.SmtpClientHostName, InterceptorAgentEvent.OnCategorizedMessage),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.SmtpClientHostName, InterceptorAgentEvent.OnInitMsg),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.SmtpClientHostName, InterceptorAgentEvent.OnPromotedMessage),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.SmtpClientHostName, InterceptorAgentEvent.OnCreatedMessage),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.SmtpClientHostName, InterceptorAgentEvent.OnDemotedMessage),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.SmtpClientHostName, InterceptorAgentEvent.OnLoadedMessage),
			new KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>(InterceptorAgentConditionType.AccountForest, InterceptorAgentEvent.OnMailFrom)
		};

		// Token: 0x0400007E RID: 126
		private static readonly List<KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>> invalidActionEventPairs = new List<KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>>
		{
			new KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>(InterceptorAgentRuleBehavior.Drop, ~InterceptorAgentRule.CanDropEvents),
			new KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>(InterceptorAgentRuleBehavior.Defer, ~InterceptorAgentRule.CanDropOrDeferEvents),
			new KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>(InterceptorAgentRuleBehavior.TransientReject, ~InterceptorAgentRule.CanTransientlyRejectEvents),
			new KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>(InterceptorAgentRuleBehavior.Delay, InterceptorAgentEvent.OnLoadedMessage),
			new KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>(InterceptorAgentRuleBehavior.Archive, ~InterceptorAgentRule.CanArchiveEvents),
			new KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>(InterceptorAgentRuleBehavior.ArchiveHeaders, ~InterceptorAgentRule.CanArchiveHeadersEvents),
			new KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>(InterceptorAgentRuleBehavior.ArchiveAndTransientReject, ~InterceptorAgentRule.CanArchiveAndTransientRejectEvents),
			new KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>(InterceptorAgentRuleBehavior.ArchiveAndPermanentReject, ~InterceptorAgentRule.CanArchiveEvents),
			new KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>(InterceptorAgentRuleBehavior.ArchiveAndDrop, ~InterceptorAgentRule.CanArchiveAndDropEvents),
			new KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>(InterceptorAgentRuleBehavior.ArchiveHeadersAndDrop, ~InterceptorAgentRule.CanArchiveHeadersAndDropEvents),
			new KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent>(InterceptorAgentRuleBehavior.ArchiveHeadersAndTransientReject, ~InterceptorAgentRule.CanArchiveHeadersAndTransientRejectEvents)
		};

		// Token: 0x0400007F RID: 127
		private static XmlSerializer serializer = new XmlSerializer(typeof(InterceptorAgentRule));

		// Token: 0x04000080 RID: 128
		private string name = string.Empty;

		// Token: 0x04000081 RID: 129
		private string description;

		// Token: 0x04000082 RID: 130
		private SourceType source;

		// Token: 0x04000083 RID: 131
		private string createdBy = InterceptorAgentRule.DefaultUser;

		// Token: 0x04000084 RID: 132
		private InterceptorAgentEvent events;

		// Token: 0x04000085 RID: 133
		private List<InterceptorAgentCondition> conditions;

		// Token: 0x04000086 RID: 134
		private InterceptorAgentAction action;

		// Token: 0x04000087 RID: 135
		private Version version;

		// Token: 0x04000088 RID: 136
		private MultiValuedProperty<ADObjectId> target;

		// Token: 0x04000089 RID: 137
		private DateTime expireTimeUtc;

		// Token: 0x0400008A RID: 138
		private DateTime expireTime;

		// Token: 0x0400008B RID: 139
		private InterceptorCountersGroup eventPerfCountersGroup;

		// Token: 0x0400008C RID: 140
		private InterceptorCountersGroup actionPerfCountersGroup;

		// Token: 0x0400008D RID: 141
		private MemoryCounter ruleStartTime;

		// Token: 0x0400008E RID: 142
		private MemoryCounter lastActionTakenTime;

		// Token: 0x0400008F RID: 143
		private bool isActive;

		// Token: 0x04000090 RID: 144
		private ObjectId identity;

		// Token: 0x04000091 RID: 145
		private string distinguishedName;

		// Token: 0x04000092 RID: 146
		private ADObjectId objectCategory;

		// Token: 0x04000093 RID: 147
		private string adminDisplayName;

		// Token: 0x04000094 RID: 148
		private Guid guid;

		// Token: 0x04000095 RID: 149
		private DateTime? whenChangedUtc;

		// Token: 0x04000096 RID: 150
		private DateTime? whenChanged;

		// Token: 0x04000097 RID: 151
		private DateTime? whenCreatedUtc;

		// Token: 0x04000098 RID: 152
		private DateTime? whenCreated;
	}
}
