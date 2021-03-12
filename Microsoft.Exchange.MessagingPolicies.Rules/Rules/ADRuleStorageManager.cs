using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000003 RID: 3
	internal class ADRuleStorageManager
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
		public ADRuleStorageManager(string ruleCollectionName, IConfigDataProvider session)
		{
			this.session = session;
			this.ruleCollectionName = ruleCollectionName;
			IConfigurationSession configurationSession = session as IConfigurationSession;
			if (configurationSession == null)
			{
				this.adRuleCollection = ADRuleStorageManager.FindRulesAdContainer(ruleCollectionName, session);
				return;
			}
			this.ruleCollectionObjectId = ADRuleStorageManager.GetContainerId(configurationSession, ruleCollectionName);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002148 File Offset: 0x00000348
		public ADRuleStorageManager(string ruleCollectionName, List<TransportRuleData> rules)
		{
			this.ruleCollectionName = ruleCollectionName;
			if (rules != null)
			{
				foreach (TransportRuleData transportRuleData in rules)
				{
					TransportRule transportRule = new TransportRule();
					transportRule.Xml = transportRuleData.Xml;
					transportRule.Priority = transportRuleData.Priority;
					transportRule.Name = transportRuleData.Name;
					transportRule.WhenChangedUTCCopy = transportRuleData.WhenChangedUTC;
					transportRule.ImmutableId = transportRuleData.ImmutableId;
					this.adRules.Add(transportRule);
				}
				this.adRules.Sort(new Comparison<TransportRule>(ADRuleStorageManager.CompareTransportRule));
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002214 File Offset: 0x00000414
		protected ADRuleStorageManager()
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002227 File Offset: 0x00000427
		public int Count
		{
			get
			{
				return this.adRules.Count;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002234 File Offset: 0x00000434
		public ADObjectId RuleCollectionId
		{
			get
			{
				if (this.adRuleCollection != null)
				{
					return (ADObjectId)this.adRuleCollection.Identity;
				}
				return this.ruleCollectionObjectId;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002255 File Offset: 0x00000455
		internal static ADObjectId GetContainerId(IConfigurationSession configurationSession, string ruleCollectionName)
		{
			return configurationSession.GetOrgContainerId().GetChildId("Transport Settings").GetChildId("Rules").GetChildId(ruleCollectionName);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002278 File Offset: 0x00000478
		public static void NormalizeInternalSequenceNumbersIfNecessary(List<TransportRule> adRules, IConfigDataProvider session)
		{
			if (adRules == null)
			{
				throw new ArgumentNullException("adRules");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (!ADRuleStorageManager.IsNormalizationNecessary(adRules))
			{
				return;
			}
			int num = 99990000;
			foreach (TransportRule transportRule in adRules)
			{
				if (num >= 2147473647)
				{
					throw new InvalidOperationException(RulesStrings.TooManyRules);
				}
				num += 10000;
				transportRule.Priority = num;
				session.Save(transportRule);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002318 File Offset: 0x00000518
		public static int AssignInternalSequenceNumber(List<TransportRule> rules, int newPosition)
		{
			if (newPosition > rules.Count || newPosition < 0)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"invalid position: new ",
					newPosition,
					" total ",
					rules.Count
				}));
			}
			if (rules.Count == 0)
			{
				return 100000000;
			}
			if (newPosition == rules.Count)
			{
				if (rules[rules.Count - 1].Priority < 2147473647)
				{
					return rules[rules.Count - 1].Priority + 10000;
				}
				return -1;
			}
			else if (newPosition == 0)
			{
				if (rules[0].Priority > 10000)
				{
					return rules[0].Priority - 10000;
				}
				return -1;
			}
			else
			{
				int num = rules[newPosition].Priority - rules[newPosition - 1].Priority;
				if (num >= 2)
				{
					return rules[newPosition - 1].Priority + num / 2;
				}
				return -1;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002418 File Offset: 0x00000618
		public static int CompareTransportRule(TransportRule x, TransportRule y)
		{
			return x.Priority.CompareTo(y.Priority);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000243C File Offset: 0x0000063C
		public static RuleCollection ParseRuleCollection(string ruleCollectionName, IEnumerable<TransportRule> adRules, RuleHealthMonitor ruleLoadMonitor, RuleParser parser = null)
		{
			if (parser == null)
			{
				parser = TransportRuleParser.Instance;
			}
			RuleCollection ruleCollection = parser.CreateRuleCollection(ruleCollectionName);
			RulesCreationContext creationContext = new RulesCreationContext();
			foreach (TransportRule transportRule in adRules)
			{
				if (ruleLoadMonitor != null)
				{
					ruleLoadMonitor.Restart();
				}
				Rule rule = parser.GetRule(transportRule.Xml, creationContext);
				rule.Name = transportRule.Name;
				rule.WhenChangedUTC = transportRule.WhenChangedUTCCopy;
				rule.ImmutableId = transportRule.ImmutableId;
				if (ruleCollection[rule.Name] != null)
				{
					throw new ParserException(RulesStrings.RuleNameExists(rule.Name));
				}
				ruleCollection.Add(rule);
				if (ruleLoadMonitor != null)
				{
					ruleLoadMonitor.RuleId = rule.ImmutableId.ToString();
					ruleLoadMonitor.Stop(true);
				}
			}
			return ruleCollection;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002528 File Offset: 0x00000728
		public void LoadRuleCollectionWithoutParsing()
		{
			this.LoadRuleCollectionWithoutParsing(null);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002534 File Offset: 0x00000734
		public void LoadRuleCollectionWithoutParsing(QueryFilter filter)
		{
			IEnumerable<TransportRule> enumerable = this.session.FindPaged<TransportRule>(filter, this.RuleCollectionId, false, null, 0);
			foreach (TransportRule item in enumerable)
			{
				this.adRules.Add(item);
			}
			this.adRules.Sort(new Comparison<TransportRule>(ADRuleStorageManager.CompareTransportRule));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025B0 File Offset: 0x000007B0
		public IEnumerable<Rule> WriteToStream(StreamWriter writer, Version maxVersion, RuleParser parser = null)
		{
			if (parser == null)
			{
				parser = this.Parser;
			}
			List<Rule> list = new List<Rule>();
			writer.Write("<rules name=\"" + this.ruleCollectionName + "\">");
			foreach (TransportRule transportRule in this.adRules)
			{
				Rule rule = this.Parser.GetRule(transportRule.Xml);
				if (rule.MinimumVersion > maxVersion)
				{
					list.Add(rule);
				}
				else
				{
					ADRuleStorageManager.WriteRuleToStream(writer, transportRule);
				}
			}
			writer.Write("</rules>");
			writer.Flush();
			return list;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000266C File Offset: 0x0000086C
		public void WriteRawRulesToStream(StreamWriter writer)
		{
			writer.Write("<rules name=\"" + this.ruleCollectionName + "\">");
			foreach (TransportRule rule in this.adRules)
			{
				ADRuleStorageManager.WriteRuleToStream(writer, rule);
			}
			writer.Write("</rules>");
			writer.Flush();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026EC File Offset: 0x000008EC
		public string GetHashOfRuleCollection()
		{
			StringBuilder stringBuilder = new StringBuilder(this.ruleCollectionName);
			foreach (TransportRule transportRule in this.adRules)
			{
				stringBuilder.Append(transportRule.Xml);
			}
			return TransportUtils.GenerateHashString(stringBuilder.ToString());
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000275C File Offset: 0x0000095C
		public void ParseRuleCollection()
		{
			this.ParseRuleCollection(null);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002765 File Offset: 0x00000965
		public void ParseRuleCollection(RuleHealthMonitor ruleLoadMonitor)
		{
			this.loadedRuleCollection = (TransportRuleCollection)ADRuleStorageManager.ParseRuleCollection(this.ruleCollectionName, this.adRules, ruleLoadMonitor, this.Parser);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000278A File Offset: 0x0000098A
		public void LoadRuleCollection()
		{
			this.LoadRuleCollectionWithoutParsing();
			this.ParseRuleCollection();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002798 File Offset: 0x00000998
		public TransportRuleCollection GetRuleCollection()
		{
			return this.loadedRuleCollection;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027DC File Offset: 0x000009DC
		public IEnumerable<TransportRuleHandle> GetRuleHandles()
		{
			return from rule in this.loadedRuleCollection
			join adRule in this.adRules on rule.Name equals adRule.Name
			select new TransportRuleHandle
			{
				AdRule = adRule,
				Rule = (TransportRule)rule
			};
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002888 File Offset: 0x00000A88
		public void UpdateRuleHandles(IEnumerable<TransportRuleHandle> updatedRules)
		{
			List<TransportRuleHandle> list = updatedRules.ToList<TransportRuleHandle>();
			list.ForEach(delegate(TransportRuleHandle handle)
			{
				handle.AdRule.Xml = this.Serializer.SaveRuleToString(handle.Rule);
			});
			list.ForEach(delegate(TransportRuleHandle handle)
			{
				this.session.Save(handle.AdRule);
			});
			this.ResetLoadedRules();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028C8 File Offset: 0x00000AC8
		public void NewRule(TransportRule rule, OrganizationId orgId, ref int position, out TransportRule transportRule)
		{
			this.ValidateTransportRule(rule);
			if (this.loadedRuleCollection == null)
			{
				this.LoadRuleCollection();
			}
			if (position < -1 || position > this.adRules.Count)
			{
				throw new InvalidPriorityException();
			}
			this.NormalizeInternalSequenceNumbersIfNecessary();
			if (position == -1)
			{
				position = this.adRules.Count;
			}
			this.loadedRuleCollection.Insert(position, rule);
			int num = ADRuleStorageManager.AssignInternalSequenceNumber(this.adRules, position);
			if (num == -1)
			{
				throw new UnableToUpdateRuleInAdException();
			}
			TransportRule transportRule2 = new TransportRule(rule.Name, this.RuleCollectionId);
			string xml = this.Serializer.SaveRuleToString(rule);
			transportRule2.Xml = xml;
			transportRule2.Priority = num;
			transportRule2.OrganizationId = orgId;
			this.session.Save(transportRule2);
			transportRule2 = (TransportRule)this.session.Read<TransportRule>(transportRule2.Id);
			transportRule = transportRule2;
			this.ResetLoadedRules();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000029A3 File Offset: 0x00000BA3
		public void GetRuleWithoutParsing(int position, out TransportRule transportRule)
		{
			transportRule = this.adRules[position];
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000029B4 File Offset: 0x00000BB4
		public void GetRule(ObjectId id, out TransportRule rule, out int position)
		{
			if (this.loadedRuleCollection == null)
			{
				this.LoadRuleCollection();
			}
			ADObjectId adobjectId = (ADObjectId)id;
			for (int i = 0; i < this.adRules.Count; i++)
			{
				if (this.adRules[i].Id.Equals(id))
				{
					rule = (TransportRule)this.loadedRuleCollection[i];
					position = i;
					return;
				}
			}
			rule = null;
			position = -1;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A24 File Offset: 0x00000C24
		public void GetRule(string name, out TransportRule rule, out int position, out TransportRule transportRule)
		{
			if (this.loadedRuleCollection == null)
			{
				this.LoadRuleCollection();
			}
			for (int i = 0; i < this.adRules.Count; i++)
			{
				if (this.loadedRuleCollection[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					rule = (TransportRule)this.loadedRuleCollection[i];
					position = i;
					transportRule = this.adRules[i];
					return;
				}
			}
			rule = null;
			position = -1;
			transportRule = null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public void UpdateRule(TransportRule rule, ObjectId id, int newPosition)
		{
			this.ValidateTransportRule(rule);
			if (this.loadedRuleCollection == null)
			{
				this.LoadRuleCollection();
			}
			if (newPosition < 0 || newPosition > this.adRules.Count - 1)
			{
				throw new InvalidPriorityException();
			}
			this.NormalizeInternalSequenceNumbersIfNecessary();
			TransportRule transportRule;
			int index;
			this.GetRule(id, out transportRule, out index);
			if (transportRule == null)
			{
				throw new RuleNotInAdException();
			}
			this.loadedRuleCollection.RemoveAt(index);
			this.loadedRuleCollection.Insert(newPosition, rule);
			TransportRule transportRule2 = this.adRules[index];
			this.adRules.RemoveAt(index);
			int num = ADRuleStorageManager.AssignInternalSequenceNumber(this.adRules, newPosition);
			if (num == -1)
			{
				throw new UnableToUpdateRuleInAdException();
			}
			string xml = this.Serializer.SaveRuleToString(rule);
			transportRule2.Xml = xml;
			transportRule2.Priority = num;
			if (transportRule2.Name != rule.Name)
			{
				transportRule2.Name = rule.Name;
			}
			this.UpdateRule(transportRule2);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B82 File Offset: 0x00000D82
		public void UpdateRule(TransportRule adRule)
		{
			this.session.Save(adRule);
			this.ResetLoadedRules();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public void ClearRules(ADRuleStorageManager.RuleFilter filter = null)
		{
			if (this.adRules == null)
			{
				throw new ArgumentNullException("adRules");
			}
			if (filter == null)
			{
				this.LoadRuleCollectionWithoutParsing();
				this.adRules.ForEach(delegate(TransportRule rule)
				{
					this.session.Delete(rule);
				});
			}
			else
			{
				this.LoadRuleCollection();
				if (this.loadedRuleCollection != null && this.adRules.Any<TransportRule>())
				{
					IEnumerable<TransportRuleHandle> ruleHandles = this.GetRuleHandles();
					if (ruleHandles == null || !ruleHandles.Any<TransportRuleHandle>())
					{
						goto IL_FF;
					}
					using (IEnumerator<TransportRuleHandle> enumerator = (from ruleHandle in ruleHandles
					where filter(ruleHandle)
					select ruleHandle).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TransportRuleHandle transportRuleHandle = enumerator.Current;
							this.session.Delete(transportRuleHandle.AdRule);
						}
						goto IL_FF;
					}
				}
				this.adRules.ForEach(delegate(TransportRule rule)
				{
					this.session.Delete(rule);
				});
			}
			IL_FF:
			this.ResetLoadedRules();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002CEC File Offset: 0x00000EEC
		public void ResetLoadedRules()
		{
			this.loadedRuleCollection = null;
			this.adRules.Clear();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002D00 File Offset: 0x00000F00
		public void ReplaceRules(TransportRuleCollection rules, OrganizationId organizationId)
		{
			foreach (Rule rule in rules)
			{
				TransportRule rule2 = (TransportRule)rule;
				this.ValidateTransportRule(rule2);
			}
			this.ClearRules(null);
			this.adRules = new List<TransportRule>();
			int num = 99990000;
			foreach (Rule rule3 in rules)
			{
				if (num >= 2147473647)
				{
					throw new InvalidOperationException(RulesStrings.TooManyRules);
				}
				num += 10000;
				foreach (Action action in rule3.Actions)
				{
					if (string.Equals(action.Name, "ApplyDisclaimer", StringComparison.OrdinalIgnoreCase) || string.Equals(action.Name, "ApplyDisclaimerWithSeparator", StringComparison.OrdinalIgnoreCase) || string.Equals(action.Name, "ApplyDisclaimerWithSeparatorAndReadingOrder", StringComparison.OrdinalIgnoreCase) || string.Equals(action.Name, "LogEvent", StringComparison.OrdinalIgnoreCase))
					{
						rule3.Actions = this.GetUpdatedRuleActions(rule3);
						break;
					}
				}
				if (rule3.Actions.Count != 0)
				{
					TransportRule transportRule = new TransportRule(rule3.Name, this.RuleCollectionId);
					string xml = this.Serializer.SaveRuleToString(rule3);
					transportRule.Xml = xml;
					transportRule.Priority = num;
					transportRule.OrganizationId = organizationId;
					if (rule3.ImmutableId != Guid.Empty)
					{
						transportRule.ImmutableId = rule3.ImmutableId;
					}
					this.session.Save(transportRule);
				}
			}
			this.ResetLoadedRules();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002EFC File Offset: 0x000010FC
		public bool CanRename(ADObjectId identity, string oldName, string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			if (name.Equals(oldName, StringComparison.Ordinal))
			{
				return true;
			}
			TransportRule transportRule = (TransportRule)this.session.Read<TransportRule>(this.RuleCollectionId.GetChildId(name));
			return transportRule == null || identity.Equals((ADObjectId)transportRule.Identity);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002F58 File Offset: 0x00001158
		private static bool IsNormalizationNecessary(List<TransportRule> adRules)
		{
			if (adRules.Count == 0)
			{
				return false;
			}
			if (adRules[0].Priority <= 10000)
			{
				return true;
			}
			if (adRules[adRules.Count - 1].Priority >= 2147473647)
			{
				return true;
			}
			int num = 0;
			foreach (TransportRule transportRule in adRules)
			{
				if (transportRule.Priority < num)
				{
					throw new InvalidOperationException();
				}
				if (transportRule.Priority - num < 2)
				{
					return true;
				}
				num = transportRule.Priority;
			}
			return false;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00003004 File Offset: 0x00001204
		protected virtual TransportRuleSerializer Serializer
		{
			get
			{
				return TransportRuleSerializer.Instance;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000300B File Offset: 0x0000120B
		protected virtual TransportRuleParser Parser
		{
			get
			{
				return TransportRuleParser.Instance;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003014 File Offset: 0x00001214
		private static TransportRuleCollection FindRulesAdContainer(string ruleCollectionName, IConfigDataProvider session)
		{
			QueryFilter filter = new TextFilter(ADObjectSchema.Name, ruleCollectionName, MatchOptions.FullString, MatchFlags.Default);
			TransportRuleCollection[] array = (TransportRuleCollection[])session.Find<TransportRuleCollection>(filter, null, true, null);
			if (array.Length != 1)
			{
				if (array.Length == 0)
				{
					ExTraceGlobals.TransportRulesEngineTracer.TraceError(0L, "ADRuleStorageManager - query for TransportRuleCollection returned 0 results");
				}
				else
				{
					ExTraceGlobals.TransportRulesEngineTracer.TraceError<int>(0L, "ADRuleStorageManager - query for TransportRuleCollection returned '{0}' results", array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						ExTraceGlobals.TransportRulesEngineTracer.TraceError<int, string>(0L, "ADRuleStorageManager Result #{0} DN - '{1}'", i, array[i].DistinguishedName.ToString());
					}
				}
				throw new RuleCollectionNotInAdException(ruleCollectionName);
			}
			return array[0];
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000030A8 File Offset: 0x000012A8
		internal static void WriteRuleToStream(StreamWriter writer, TransportRule rule)
		{
			XDocument xdocument = XDocument.Load(new StringReader(rule.Xml));
			if (xdocument != null && xdocument.Root.Attribute("id") == null)
			{
				xdocument.Root.Add(new XAttribute("id", rule.ImmutableId.ToString()));
				writer.Write(xdocument.ToString());
				return;
			}
			writer.Write(rule.Xml);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003128 File Offset: 0x00001328
		private void ValidateTransportRule(TransportRule rule)
		{
			TransportRule transportRule = new TransportRule();
			transportRule.Name = rule.Name;
			transportRule.Xml = this.Serializer.SaveRuleToString(rule);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003159 File Offset: 0x00001359
		private void NormalizeInternalSequenceNumbersIfNecessary()
		{
			ADRuleStorageManager.NormalizeInternalSequenceNumbersIfNecessary(this.adRules, this.session);
			this.ResetLoadedRules();
			this.LoadRuleCollection();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003178 File Offset: 0x00001378
		private ShortList<Action> GetUpdatedRuleActions(Rule rule)
		{
			ShortList<Action> shortList = new ShortList<Action>();
			ShortList<Action> actions = rule.Actions;
			for (int i = 0; i < actions.Count; i++)
			{
				if (!string.Equals(actions[i].Name, "LogEvent", StringComparison.OrdinalIgnoreCase) || string.Equals(this.ruleCollectionName, "Edge", StringComparison.OrdinalIgnoreCase))
				{
					if (string.Equals(actions[i].Name, "ApplyDisclaimer", StringComparison.OrdinalIgnoreCase))
					{
						shortList.Add(this.GetUpdatedDisclaimerAction(actions[i], false, false));
					}
					else if (string.Equals(actions[i].Name, "ApplyDisclaimerWithSeparator", StringComparison.OrdinalIgnoreCase))
					{
						shortList.Add(this.GetUpdatedDisclaimerAction(actions[i], true, false));
					}
					else if (string.Equals(actions[i].Name, "ApplyDisclaimerWithSeparatorAndReadingOrder", StringComparison.OrdinalIgnoreCase))
					{
						shortList.Add(this.GetUpdatedDisclaimerAction(actions[i], true, true));
					}
					else
					{
						shortList.Add(actions[i]);
					}
				}
			}
			return shortList;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003278 File Offset: 0x00001478
		private Action GetUpdatedDisclaimerAction(Action originalAction, bool hasSeparatorSpecified, bool hasReadingOrderSpecified)
		{
			string text = (string)originalAction.Arguments[0].GetValue(null);
			string text2 = (string)originalAction.Arguments[1].GetValue(null);
			string font = (string)originalAction.Arguments[2].GetValue(null);
			string size = (string)originalAction.Arguments[3].GetValue(null);
			string color = (string)originalAction.Arguments[4].GetValue(null);
			string value = (string)originalAction.Arguments[5].GetValue(null);
			bool append = !string.Equals(text, "Prepend", StringComparison.OrdinalIgnoreCase);
			bool needsSeparator = false;
			bool rightToLeftReadingOrder = false;
			if (hasSeparatorSpecified)
			{
				string a = (string)originalAction.Arguments[6].GetValue(null);
				needsSeparator = string.Equals(a, "WithSeparator", StringComparison.OrdinalIgnoreCase);
			}
			if (hasReadingOrderSpecified)
			{
				string a2 = (string)originalAction.Arguments[7].GetValue(null);
				rightToLeftReadingOrder = string.Equals(a2, "RightToLeft", StringComparison.OrdinalIgnoreCase);
			}
			string value2 = this.BuildHtmlDisclaimerText(append, text2, font, size, color, needsSeparator, rightToLeftReadingOrder);
			ShortList<Argument> shortList = new ShortList<Argument>();
			shortList.Add(new Value(text));
			shortList.Add(new Value(value2));
			shortList.Add(new Value(value));
			return TransportRuleParser.Instance.CreateAction("ApplyHtmlDisclaimer", shortList, null);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000033E0 File Offset: 0x000015E0
		private string BuildHtmlDisclaimerText(bool append, string text, string font, string size, string color, bool needsSeparator, bool rightToLeftReadingOrder)
		{
			if (string.Equals(font, "CourierNew"))
			{
				font = "Courier New";
			}
			if (string.Equals(size, "smallest", StringComparison.InvariantCultureIgnoreCase))
			{
				size = "1";
			}
			else if (string.Equals(size, "smaller", StringComparison.InvariantCultureIgnoreCase))
			{
				size = "2";
			}
			else if (string.Equals(size, "larger", StringComparison.InvariantCultureIgnoreCase))
			{
				size = "5";
			}
			else if (string.Equals(size, "largest", StringComparison.InvariantCultureIgnoreCase))
			{
				size = "6";
			}
			else
			{
				size = "3";
			}
			if (append)
			{
				text = "\r\n" + text;
			}
			else
			{
				text += "\r\n";
			}
			string text2 = string.Format("<font face='{0}' color='{1}' size='{2}'>", font, color, size);
			TextToText textToText = new TextToText();
			textToText.HtmlEscapeOutput = true;
			StringBuilder stringBuilder = new StringBuilder();
			using (StringReader stringReader = new StringReader(text.ToString()))
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					textToText.Convert(stringReader, stringWriter);
				}
			}
			string text3 = stringBuilder.ToString();
			text3 = text3.Replace("\r\n", "<br>");
			text3 = text3.Replace("\n", "<br>");
			text2 += text3;
			text2 += "</font>";
			if (rightToLeftReadingOrder)
			{
				text2 = "<p align='right'><span dir='rtl'>" + text2;
				text2 += "</span></p>";
			}
			if (needsSeparator)
			{
				if (append)
				{
					text2 = "<br><hr>" + text2;
				}
				else
				{
					text2 += "<hr><br>";
				}
			}
			else if (append)
			{
				text2 = "<br>" + text2;
			}
			else
			{
				text2 += "<br>";
			}
			return text2;
		}

		// Token: 0x04000003 RID: 3
		public const int MaxRuleXmlSize = 8192;

		// Token: 0x04000004 RID: 4
		private const int DefaultStartingInternalSequenceNumber = 100000000;

		// Token: 0x04000005 RID: 5
		private const int DefaultInternalSequenceNumberGap = 10000;

		// Token: 0x04000006 RID: 6
		private TransportRuleCollection loadedRuleCollection;

		// Token: 0x04000007 RID: 7
		private TransportRuleCollection adRuleCollection;

		// Token: 0x04000008 RID: 8
		private IConfigDataProvider session;

		// Token: 0x04000009 RID: 9
		protected string ruleCollectionName;

		// Token: 0x0400000A RID: 10
		protected List<TransportRule> adRules = new List<TransportRule>();

		// Token: 0x0400000B RID: 11
		private ADObjectId ruleCollectionObjectId;

		// Token: 0x02000004 RID: 4
		// (Invoke) Token: 0x06000036 RID: 54
		internal delegate bool RuleFilter(TransportRuleHandle ruleHandle);
	}
}
