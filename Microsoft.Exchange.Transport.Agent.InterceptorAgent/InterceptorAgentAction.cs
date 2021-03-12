using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public sealed class InterceptorAgentAction : IEquatable<InterceptorAgentAction>
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002FCA File Offset: 0x000011CA
		public InterceptorAgentAction(InterceptorAgentRuleBehavior action)
		{
			if (action != InterceptorAgentRuleBehavior.Drop && action != InterceptorAgentRuleBehavior.NoOp)
			{
				throw new InvalidOperationException("Only the drop or no-op actions have no additional parameters");
			}
			this.action = action;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003004 File Offset: 0x00001204
		public InterceptorAgentAction(InterceptorAgentRuleBehavior action, SmtpResponse response)
		{
			if (SmtpResponse.Empty.Equals(response))
			{
				throw new ArgumentException("Empty SmtpResponse", "response");
			}
			switch (action)
			{
			case InterceptorAgentRuleBehavior.PermanentReject:
			case InterceptorAgentRuleBehavior.TransientReject:
				this.response = response;
				this.action = action;
				return;
			default:
				throw new InvalidOperationException(string.Format("Wrong constructor for the specified action '{0}'", action));
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003088 File Offset: 0x00001288
		public InterceptorAgentAction(InterceptorAgentRuleBehavior action, TimeSpan delay)
		{
			if (action != InterceptorAgentRuleBehavior.Delay && action != InterceptorAgentRuleBehavior.Defer)
			{
				throw new InvalidOperationException("This constructor can only be used by Delay or Defer Behavior");
			}
			this.delay = delay;
			if (this.delay.Ticks > 0L)
			{
				this.DelaySpecified = true;
			}
			this.action = action;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000030EC File Offset: 0x000012EC
		public InterceptorAgentAction(InterceptorAgentRuleBehavior action, string path, SmtpResponse response)
		{
			if (!InterceptorAgentAction.IsArchivingBehavior(action))
			{
				throw new InvalidOperationException(string.Format("Wrong constructor for the specified action '{0}'", action));
			}
			this.path = path;
			this.action = action;
			if (action == InterceptorAgentRuleBehavior.ArchiveAndPermanentReject || action == InterceptorAgentRuleBehavior.ArchiveAndTransientReject)
			{
				this.response = response;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003152 File Offset: 0x00001352
		private InterceptorAgentAction()
		{
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003170 File Offset: 0x00001370
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003178 File Offset: 0x00001378
		[XmlAttribute]
		public InterceptorAgentRuleBehavior Action
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003181 File Offset: 0x00001381
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003194 File Offset: 0x00001394
		[XmlAttribute]
		public string ResponseString
		{
			get
			{
				return this.response.ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.response = SmtpResponse.Empty;
					return;
				}
				if (!SmtpResponse.TryParse(value, out this.response))
				{
					throw new ArgumentException(string.Format("Cannot parse smtp response from {0}", value), "Response");
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000031CE File Offset: 0x000013CE
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000031E1 File Offset: 0x000013E1
		[XmlAttribute("Delay", DataType = "duration")]
		public string DelayString
		{
			get
			{
				return this.delay.ToString();
			}
			set
			{
				this.delay = TimeSpan.Parse(value);
				if (this.delay.Ticks > 0L)
				{
					this.DelaySpecified = true;
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003205 File Offset: 0x00001405
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000320D File Offset: 0x0000140D
		[XmlAttribute]
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003216 File Offset: 0x00001416
		internal static string[] AllActions
		{
			get
			{
				return InterceptorAgentAction.allActionNames;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000321D File Offset: 0x0000141D
		[XmlIgnore]
		internal SmtpResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003225 File Offset: 0x00001425
		[XmlIgnore]
		internal TimeSpan Delay
		{
			get
			{
				return this.delay;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000322D File Offset: 0x0000142D
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003235 File Offset: 0x00001435
		private bool DelaySpecified { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00003258 File Offset: 0x00001458
		public static bool IsValidRuleBehavior(InterceptorAgentRuleBehavior behavior)
		{
			return Enum.GetValues(typeof(InterceptorAgentRuleBehavior)).Cast<object>().Any((object val) => behavior == (InterceptorAgentRuleBehavior)val);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003297 File Offset: 0x00001497
		public static bool IsArchivingBehavior(InterceptorAgentRuleBehavior behavior)
		{
			return (ushort)(behavior & (InterceptorAgentRuleBehavior.Archive | InterceptorAgentRuleBehavior.ArchiveHeaders)) != 0;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000032A4 File Offset: 0x000014A4
		public static bool IsRejectingBehavior(InterceptorAgentRuleBehavior behavior)
		{
			return (ushort)(behavior & (InterceptorAgentRuleBehavior.PermanentReject | InterceptorAgentRuleBehavior.TransientReject)) != 0;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000032B0 File Offset: 0x000014B0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000032D0 File Offset: 0x000014D0
		public void ToString(StringBuilder result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			result.Append(this.Action.ToString());
			if (!SmtpResponse.Empty.Equals(this.response))
			{
				result.AppendFormat(" rejectResponse=\"{0}\"", this.response);
			}
			if ((ushort)(this.Action & InterceptorAgentRuleBehavior.Archive) != 0 || (ushort)(this.Action & InterceptorAgentRuleBehavior.ArchiveHeaders) != 0)
			{
				result.AppendFormat(" path=\"{0}\"", this.Path);
			}
			if (this.DelaySpecified && this.delay.Ticks > 0L)
			{
				result.AppendFormat(" delay=\"{0}\"", this.Delay);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003386 File Offset: 0x00001586
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InterceptorAgentAction);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003394 File Offset: 0x00001594
		public override int GetHashCode()
		{
			return this.Action.GetHashCode() ^ this.response.ToString().GetHashCode() ^ this.Delay.GetHashCode() ^ ((this.Path == null) ? 0 : this.Path.GetHashCode());
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000033F4 File Offset: 0x000015F4
		public bool Equals(InterceptorAgentAction other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(this, other) || (this.Action == other.Action && !(this.Delay != other.Delay) && string.Equals(this.Path, other.Path) && this.response.ToString().Equals(other.response.ToString())));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003478 File Offset: 0x00001678
		internal InterceptorAgentRuleBehavior PerformAction(InterceptorAgentRule matchedRule, MailItem mailItem, Action drop, Action<SmtpResponse> reject, Action<TimeSpan> defer = null)
		{
			InterceptorAgentRuleBehavior interceptorAgentRuleBehavior = InterceptorAgentRuleBehavior.NoOp;
			if (this.action == InterceptorAgentRuleBehavior.NoOp)
			{
				return interceptorAgentRuleBehavior;
			}
			if (matchedRule == null)
			{
				throw new ArgumentNullException("matchedRule");
			}
			bool flag = false;
			if ((ushort)(this.action & InterceptorAgentRuleBehavior.Archive) != 0)
			{
				if (mailItem == null)
				{
					throw new ArgumentNullException("mailItem");
				}
				if (!Archiver.Instance.TryArchiveMessage(mailItem, this.path ?? string.Empty))
				{
					ExTraceGlobals.InterceptorAgentTracer.TraceError((long)this.GetHashCode(), "Failed to archive mail item; will not reject or drop it");
					flag = true;
				}
				else
				{
					interceptorAgentRuleBehavior |= InterceptorAgentRuleBehavior.Archive;
				}
			}
			else if ((ushort)(this.action & InterceptorAgentRuleBehavior.ArchiveHeaders) != 0)
			{
				if (mailItem == null)
				{
					throw new ArgumentNullException("mailItem");
				}
				if (!Archiver.Instance.TryArchiveHeaders(mailItem, this.path ?? string.Empty))
				{
					ExTraceGlobals.InterceptorAgentTracer.TraceError((long)this.GetHashCode(), "Failed to archive mail item headers");
				}
				else
				{
					interceptorAgentRuleBehavior |= InterceptorAgentRuleBehavior.ArchiveHeaders;
				}
			}
			if (!flag && (ushort)(this.action & InterceptorAgentRuleBehavior.Drop) != 0)
			{
				if (drop == null)
				{
					throw new ArgumentNullException("drop");
				}
				drop();
				interceptorAgentRuleBehavior |= InterceptorAgentRuleBehavior.Drop;
			}
			else if (!flag && ((ushort)(this.action & InterceptorAgentRuleBehavior.PermanentReject) != 0 || (ushort)(this.action & InterceptorAgentRuleBehavior.TransientReject) != 0))
			{
				if (reject == null)
				{
					throw new ArgumentNullException("reject");
				}
				if ((ushort)(this.action & InterceptorAgentRuleBehavior.PermanentReject) != 0)
				{
					if (!this.RuleAlreadyApplied(matchedRule.Guid, mailItem))
					{
						reject(this.response);
						interceptorAgentRuleBehavior |= InterceptorAgentRuleBehavior.PermanentReject;
					}
				}
				else
				{
					reject(this.response);
					interceptorAgentRuleBehavior |= InterceptorAgentRuleBehavior.TransientReject;
				}
			}
			else if ((ushort)(this.action & InterceptorAgentRuleBehavior.Defer) != 0)
			{
				if (defer == null)
				{
					throw new ArgumentNullException("defer");
				}
				if (!this.RuleAlreadyApplied(matchedRule.Guid, mailItem))
				{
					defer(this.delay);
					interceptorAgentRuleBehavior |= InterceptorAgentRuleBehavior.Defer;
				}
			}
			if ((ushort)(this.action & InterceptorAgentRuleBehavior.Delay) != 0 && this.delay > TimeSpan.Zero && this.delay < TimeSpan.FromMilliseconds(2147483647.0))
			{
				ExTraceGlobals.InterceptorAgentTracer.TraceInformation<string>(0, (long)this.GetHashCode(), "Delaying message by blocking thread for {0}", this.Delay.ToString());
				Thread.Sleep(this.delay);
				interceptorAgentRuleBehavior |= InterceptorAgentRuleBehavior.Delay;
			}
			return interceptorAgentRuleBehavior;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003694 File Offset: 0x00001894
		internal void Verify()
		{
			if (!InterceptorAgentAction.IsValidRuleBehavior(this.action))
			{
				throw new InvalidOperationException(string.Format("Invalid action '{0}'", this.action));
			}
			if ((this.action == InterceptorAgentRuleBehavior.Delay || this.action == InterceptorAgentRuleBehavior.Defer) && this.Delay == TimeSpan.Zero)
			{
				throw new InvalidOperationException("No delay specified for delay or defer action");
			}
			if (this.action == InterceptorAgentRuleBehavior.TransientReject && this.Response.SmtpResponseType != SmtpResponseType.TransientError)
			{
				throw new InvalidOperationException("TransientReject must have a transient error response code");
			}
			if (this.action == InterceptorAgentRuleBehavior.PermanentReject && this.Response.SmtpResponseType != SmtpResponseType.PermanentError)
			{
				throw new InvalidOperationException("PermanentReject must have a permanent error response code");
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003744 File Offset: 0x00001944
		private bool RuleAlreadyApplied(Guid matchedRuleGuid, MailItem mailItem)
		{
			if (mailItem == null || mailItem.MimeDocument == null || mailItem.MimeDocument.RootPart == null || mailItem.MimeDocument.RootPart.Headers == null)
			{
				return false;
			}
			HeaderList headers = mailItem.MimeDocument.RootPart.Headers;
			Header[] array = headers.FindAll("X-MS-Exchange-Organization-Matched-Interceptor-Rule");
			foreach (Header header in array)
			{
				if (header.Value.Equals(matchedRuleGuid.ToString(), StringComparison.InvariantCultureIgnoreCase))
				{
					ExTraceGlobals.InterceptorAgentTracer.TraceInformation<Guid>(0, (long)this.GetHashCode(), "interceptor rule with guid {0} has already been applied on this message", matchedRuleGuid);
					return true;
				}
			}
			headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-Matched-Interceptor-Rule", matchedRuleGuid.ToString()));
			return false;
		}

		// Token: 0x0400002F RID: 47
		private static readonly string[] allActionNames = Enum.GetNames(typeof(InterceptorAgentRuleBehavior));

		// Token: 0x04000030 RID: 48
		private SmtpResponse response;

		// Token: 0x04000031 RID: 49
		private TimeSpan delay = TimeSpan.Zero;

		// Token: 0x04000032 RID: 50
		private InterceptorAgentRuleBehavior action;

		// Token: 0x04000033 RID: 51
		private string path = string.Empty;
	}
}
