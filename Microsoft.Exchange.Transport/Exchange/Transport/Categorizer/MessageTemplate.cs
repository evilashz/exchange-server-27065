using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001DF RID: 479
	internal class MessageTemplate : IEquatable<MessageTemplate>, IComparable<MessageTemplate>
	{
		// Token: 0x060015A8 RID: 5544 RVA: 0x00057BE0 File Offset: 0x00055DE0
		public MessageTemplate(string reversePath, AutoResponseSuppress suppress, string resentFrom, bool transmitHistory, bool redirectHandled, bool suppressRecallReport, bool bypassChildModeration)
		{
			this.reversePath = reversePath;
			this.suppress = suppress;
			this.resentFrom = resentFrom;
			this.transmitHistory = transmitHistory;
			this.redirectHandled = redirectHandled;
			this.suppressRecallReport = suppressRecallReport;
			this.bypassChildModeration = bypassChildModeration;
			this.formatted = this.Format();
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00057C34 File Offset: 0x00055E34
		public string ReversePath
		{
			get
			{
				return this.reversePath;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00057C3C File Offset: 0x00055E3C
		public bool TransmitHistory
		{
			get
			{
				return this.transmitHistory;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x00057C44 File Offset: 0x00055E44
		public bool BypassChildModeration
		{
			get
			{
				return this.bypassChildModeration;
			}
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00057C4C File Offset: 0x00055E4C
		public static bool operator ==(MessageTemplate a, MessageTemplate b)
		{
			return a == b || (a != null && b != null && string.Equals(a.formatted, b.formatted, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00057C6E File Offset: 0x00055E6E
		public static bool operator !=(MessageTemplate a, MessageTemplate b)
		{
			return !(a == b);
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00057C7C File Offset: 0x00055E7C
		public static MessageTemplate ReadFrom(MailRecipient recipient)
		{
			string text;
			if (!recipient.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Transport.MessageTemplate", out text) || string.IsNullOrEmpty(text))
			{
				return MessageTemplate.Default;
			}
			return MessageTemplate.Parse(text);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00057CB4 File Offset: 0x00055EB4
		public MessageTemplate Merge(string reversePath, AutoResponseSuppress suppress, string resentFrom, bool transmitHistory, bool redirectHandled, bool suppressRecallReport, bool bypassChildModerationFlag)
		{
			return new MessageTemplate(reversePath ?? this.reversePath, suppress | this.suppress, resentFrom ?? this.resentFrom, transmitHistory, redirectHandled || this.redirectHandled, suppressRecallReport || this.suppressRecallReport, bypassChildModerationFlag || this.bypassChildModeration);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00057D0D File Offset: 0x00055F0D
		public MessageTemplate Derive(MessageTemplate other)
		{
			return this.Merge(other.reversePath, other.suppress, other.resentFrom, other.transmitHistory, other.redirectHandled, other.suppressRecallReport, other.bypassChildModeration);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00057D3F File Offset: 0x00055F3F
		public void WriteTo(MailRecipient recipient)
		{
			recipient.ExtendedProperties.SetValue<string>("Microsoft.Exchange.Transport.MessageTemplate", this.formatted);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00057D58 File Offset: 0x00055F58
		public void ApplyTo(TransportMailItem mailItem, ResolverMessage message)
		{
			if (this.reversePath != null)
			{
				mailItem.From = (RoutingAddress)this.reversePath;
			}
			if (this.suppress != (AutoResponseSuppress)0)
			{
				message.AutoResponseSuppress |= this.suppress;
			}
			if (this.resentFrom != null)
			{
				message.AddResentFrom(this.resentFrom);
			}
			if (!this.transmitHistory)
			{
				message.ClearHistory(mailItem);
			}
			if (this.redirectHandled)
			{
				message.RedirectHandled = true;
			}
			if (this.suppressRecallReport)
			{
				message.SuppressRecallReport = true;
			}
			if (this.bypassChildModeration)
			{
				message.BypassChildModeration = true;
			}
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00057DE9 File Offset: 0x00055FE9
		public void Normalize(ResolverMessage message)
		{
			if (!this.transmitHistory && message.History == null)
			{
				this.transmitHistory = true;
			}
			this.formatted = this.Format();
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00057E14 File Offset: 0x00056014
		public bool Equals(MessageTemplate other)
		{
			return this == other;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00057E1D File Offset: 0x0005601D
		public int CompareTo(MessageTemplate other)
		{
			return string.CompareOrdinal(this.formatted, other.formatted);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00057E30 File Offset: 0x00056030
		public override int GetHashCode()
		{
			return this.formatted.GetHashCode();
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00057E3D File Offset: 0x0005603D
		public override bool Equals(object obj)
		{
			return this == obj as MessageTemplate;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00057E4B File Offset: 0x0005604B
		private static void FormatField(StringBuilder s, string name, string value)
		{
			s.Append(name);
			s.Append(": ");
			s.AppendLine(value);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00057E6C File Offset: 0x0005606C
		private static bool ParseField(string line, out string name, out string value)
		{
			int num = line.IndexOf(':');
			if (num == -1)
			{
				name = null;
				value = null;
				return false;
			}
			name = line.Substring(0, num);
			value = line.Substring(num + 1).Trim();
			return true;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00057EAC File Offset: 0x000560AC
		private static MessageTemplate Parse(string s)
		{
			string text = null;
			AutoResponseSuppress autoResponseSuppress = (AutoResponseSuppress)0;
			string text2 = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			MessageTemplate result;
			using (StringReader stringReader = new StringReader(s))
			{
				for (string line = stringReader.ReadLine(); line != null; line = stringReader.ReadLine())
				{
					string a;
					string text3;
					if (MessageTemplate.ParseField(line, out a, out text3))
					{
						if (string.Equals(a, "ReversePath", StringComparison.OrdinalIgnoreCase))
						{
							text = text3;
						}
						else if (string.Equals(a, "AutoResponseSuppress", StringComparison.OrdinalIgnoreCase))
						{
							AutoResponseSuppress autoResponseSuppress2 = (AutoResponseSuppress)0;
							if (EnumValidator<AutoResponseSuppress>.TryParse(text3, EnumParseOptions.IgnoreCase, out autoResponseSuppress2))
							{
								autoResponseSuppress = autoResponseSuppress2;
							}
							else
							{
								autoResponseSuppress = (AutoResponseSuppress)0;
							}
						}
						else if (string.Equals(a, "ResentFrom", StringComparison.OrdinalIgnoreCase))
						{
							text2 = text3;
						}
						else if (string.Equals(a, "TransmitHistory", StringComparison.OrdinalIgnoreCase))
						{
							bool.TryParse(text3, out flag);
						}
						else if (string.Equals(a, "RedirectHandled", StringComparison.OrdinalIgnoreCase))
						{
							bool.TryParse(text3, out flag2);
						}
						else if (string.Equals(a, "SuppressRecallReport", StringComparison.OrdinalIgnoreCase))
						{
							bool.TryParse(text3, out flag3);
						}
						else if (string.Equals(a, "BypassChildModeration", StringComparison.OrdinalIgnoreCase))
						{
							bool.TryParse(text3, out flag4);
						}
					}
				}
				result = new MessageTemplate(text, autoResponseSuppress, text2, flag, flag2, flag3, flag4);
			}
			return result;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00057FFC File Offset: 0x000561FC
		private string Format()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.reversePath != null)
			{
				MessageTemplate.FormatField(stringBuilder, "ReversePath", this.reversePath);
			}
			MessageTemplate.FormatField(stringBuilder, "AutoResponseSuppress", ResolverMessage.FormatAutoResponseSuppressHeaderValue(this.suppress));
			if (this.resentFrom != null)
			{
				MessageTemplate.FormatField(stringBuilder, "ResentFrom", this.resentFrom);
			}
			MessageTemplate.FormatField(stringBuilder, "TransmitHistory", this.transmitHistory ? "True" : "False");
			if (this.redirectHandled)
			{
				MessageTemplate.FormatField(stringBuilder, "RedirectHandled", "True");
			}
			if (this.suppressRecallReport)
			{
				MessageTemplate.FormatField(stringBuilder, "SuppressRecallReport", "True");
			}
			if (this.bypassChildModeration)
			{
				MessageTemplate.FormatField(stringBuilder, "BypassChildModeration", "True");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000AC2 RID: 2754
		public const string BoolHeaderSetValue = "True";

		// Token: 0x04000AC3 RID: 2755
		public const string BoolHeaderNotSetValue = "False";

		// Token: 0x04000AC4 RID: 2756
		public const string TemplateProperty = "Microsoft.Exchange.Transport.MessageTemplate";

		// Token: 0x04000AC5 RID: 2757
		public static readonly MessageTemplate Default = new MessageTemplate(null, (AutoResponseSuppress)0, null, true, false, false, false);

		// Token: 0x04000AC6 RID: 2758
		public static readonly MessageTemplate StripHistory = new MessageTemplate(null, (AutoResponseSuppress)0, null, false, false, false, false);

		// Token: 0x04000AC7 RID: 2759
		private string reversePath;

		// Token: 0x04000AC8 RID: 2760
		private AutoResponseSuppress suppress;

		// Token: 0x04000AC9 RID: 2761
		private string resentFrom;

		// Token: 0x04000ACA RID: 2762
		private bool transmitHistory;

		// Token: 0x04000ACB RID: 2763
		private bool redirectHandled;

		// Token: 0x04000ACC RID: 2764
		private bool suppressRecallReport;

		// Token: 0x04000ACD RID: 2765
		private bool bypassChildModeration;

		// Token: 0x04000ACE RID: 2766
		private string formatted;

		// Token: 0x020001E0 RID: 480
		private static class Fields
		{
			// Token: 0x04000ACF RID: 2767
			public const string ReversePath = "ReversePath";

			// Token: 0x04000AD0 RID: 2768
			public const string AutoResponseSuppress = "AutoResponseSuppress";

			// Token: 0x04000AD1 RID: 2769
			public const string ResentFrom = "ResentFrom";

			// Token: 0x04000AD2 RID: 2770
			public const string TransmitHistory = "TransmitHistory";

			// Token: 0x04000AD3 RID: 2771
			public const string RedirectHandled = "RedirectHandled";

			// Token: 0x04000AD4 RID: 2772
			public const string SuppressRecallReport = "SuppressRecallReport";

			// Token: 0x04000AD5 RID: 2773
			public const string BypassChildModeration = "BypassChildModeration";
		}
	}
}
