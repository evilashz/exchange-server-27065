using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000112 RID: 274
	internal class ClientAccessRuleSerializer : RuleSerializer
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0001DF4E File Offset: 0x0001C14E
		public static ClientAccessRuleSerializer Instance
		{
			get
			{
				return ClientAccessRuleSerializer.instance;
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001DF58 File Offset: 0x0001C158
		protected override void SaveRuleAttributes(XmlWriter xmlWriter, Rule rule)
		{
			ClientAccessRule clientAccessRule = rule as ClientAccessRule;
			if (clientAccessRule != null && clientAccessRule.DatacenterAdminsOnly)
			{
				xmlWriter.WriteAttributeString(ClientAccessRuleSerializer.DatacenterAdminsOnlyAttributeName, clientAccessRule.DatacenterAdminsOnly.ToString());
			}
			base.SaveRuleAttributes(xmlWriter, rule);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001DFE0 File Offset: 0x0001C1E0
		protected override void SaveValue(XmlWriter xmlWriter, Value value)
		{
			if (value != null)
			{
				if (value.ParsedValue is IEnumerable<string>)
				{
					ClientAccessRuleSerializer.SaveValue<string>(xmlWriter, (IEnumerable<string>)value.ParsedValue, (string v) => v.ToString());
					return;
				}
				if (value.ParsedValue is IEnumerable<IPRange>)
				{
					ClientAccessRuleSerializer.SaveValue<IPRange>(xmlWriter, (IEnumerable<IPRange>)value.ParsedValue, (IPRange v) => v.ToString());
					return;
				}
				if (value.ParsedValue is IEnumerable<IntRange>)
				{
					ClientAccessRuleSerializer.SaveValue<IntRange>(xmlWriter, (IEnumerable<IntRange>)value.ParsedValue, (IntRange v) => v.ToString());
					return;
				}
				if (value.ParsedValue is IEnumerable<ClientAccessProtocol>)
				{
					ClientAccessRuleSerializer.SaveValue<ClientAccessProtocol>(xmlWriter, (IEnumerable<ClientAccessProtocol>)value.ParsedValue, delegate(ClientAccessProtocol v)
					{
						int num = (int)v;
						return num.ToString();
					});
					return;
				}
				if (value.ParsedValue is IEnumerable<Regex>)
				{
					ClientAccessRuleSerializer.SaveValue<Regex>(xmlWriter, (IEnumerable<Regex>)value.ParsedValue, new Func<Regex, string>(ClientAccessRulesUsernamePatternProperty.GetDisplayValue));
					return;
				}
				if (value.ParsedValue is IEnumerable<ClientAccessAuthenticationMethod>)
				{
					ClientAccessRuleSerializer.SaveValue<ClientAccessAuthenticationMethod>(xmlWriter, (IEnumerable<ClientAccessAuthenticationMethod>)value.ParsedValue, delegate(ClientAccessAuthenticationMethod v)
					{
						int num = (int)v;
						return num.ToString();
					});
					return;
				}
			}
			base.SaveValue(xmlWriter, value);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001E154 File Offset: 0x0001C354
		private static void SaveValue<T>(XmlWriter xmlWriter, IEnumerable<T> values, Func<T, string> converter)
		{
			foreach (T arg in values)
			{
				xmlWriter.WriteStartElement("value");
				xmlWriter.WriteValue(converter(arg));
				xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x040005F9 RID: 1529
		private static readonly ClientAccessRuleSerializer instance = new ClientAccessRuleSerializer();

		// Token: 0x040005FA RID: 1530
		private static readonly string DatacenterAdminsOnlyAttributeName = "DatacenterAdminsOnly";
	}
}
