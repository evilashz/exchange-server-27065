using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200007C RID: 124
	[KnownType(typeof(RuleActionForwardData))]
	[KnownType(typeof(RuleActionDelegateData))]
	[KnownType(typeof(RuleActionTagData))]
	[DataContract]
	[KnownType(typeof(RuleActionBounceData))]
	[KnownType(typeof(RuleActionCopyData))]
	[KnownType(typeof(RuleActionDeferData))]
	[KnownType(typeof(RuleActionDeleteData))]
	[KnownType(typeof(RuleActionExternalCopyData))]
	[KnownType(typeof(RuleActionExternalMoveData))]
	[KnownType(typeof(RuleActionInMailboxCopyData))]
	[KnownType(typeof(RuleActionInMailboxMoveData))]
	[KnownType(typeof(RuleActionMarkAsReadData))]
	[KnownType(typeof(RuleActionMoveData))]
	[KnownType(typeof(RuleActionOOFReplyData))]
	[KnownType(typeof(RuleActionReplyData))]
	internal sealed class RuleData
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x00009F71 File Offset: 0x00008171
		public RuleData()
		{
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00009F79 File Offset: 0x00008179
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00009F81 File Offset: 0x00008181
		[DataMember]
		public int ExecutionSequence { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00009F8A File Offset: 0x0000818A
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00009F92 File Offset: 0x00008192
		[DataMember]
		public int Level { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00009F9B File Offset: 0x0000819B
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x00009FA3 File Offset: 0x000081A3
		[DataMember]
		public uint StateFlags { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00009FAC File Offset: 0x000081AC
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x00009FB4 File Offset: 0x000081B4
		[DataMember]
		public uint UserFlags { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00009FBD File Offset: 0x000081BD
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00009FC5 File Offset: 0x000081C5
		[DataMember]
		public RestrictionData Condition { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00009FCE File Offset: 0x000081CE
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00009FD6 File Offset: 0x000081D6
		[DataMember]
		public RuleActionData[] Actions { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00009FDF File Offset: 0x000081DF
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x00009FE7 File Offset: 0x000081E7
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00009FF0 File Offset: 0x000081F0
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00009FF8 File Offset: 0x000081F8
		[DataMember]
		public string Provider { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000A001 File Offset: 0x00008201
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0000A009 File Offset: 0x00008209
		[DataMember]
		public byte[] ProviderData { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000A012 File Offset: 0x00008212
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0000A01A File Offset: 0x0000821A
		[DataMember]
		public bool IsExtended { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000A023 File Offset: 0x00008223
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0000A02B File Offset: 0x0000822B
		[DataMember]
		public PropValueData[] ExtraProperties { get; set; }

		// Token: 0x0600057D RID: 1405 RVA: 0x0000A034 File Offset: 0x00008234
		private RuleData(Rule rule)
		{
			this.ExecutionSequence = rule.ExecutionSequence;
			this.Level = rule.Level;
			this.StateFlags = (uint)rule.StateFlags;
			this.UserFlags = rule.UserFlags;
			this.Condition = DataConverter<RestrictionConverter, Restriction, RestrictionData>.GetData(rule.Condition);
			this.Actions = DataConverter<RuleActionConverter, RuleAction, RuleActionData>.GetData(rule.Actions);
			this.Name = rule.Name;
			this.Provider = rule.Provider;
			this.ProviderData = rule.ProviderData;
			this.IsExtended = rule.IsExtended;
			if (rule.IsExtended)
			{
				this.ExtraProperties = DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(rule.ExtraProperties);
				return;
			}
			if (rule.ExtraProperties != null && rule.ExtraProperties.Length > 0)
			{
				MrsTracer.Common.Error("Non-extended rule '{0}' has {1} extra properties", new object[]
				{
					rule.Name,
					rule.ExtraProperties.Length
				});
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000A126 File Offset: 0x00008326
		public static RuleData Create(Rule rule)
		{
			if (rule == null)
			{
				return null;
			}
			return new RuleData(rule);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000A134 File Offset: 0x00008334
		public static void ConvertRulesToUplevel(RuleData[] rules, Func<byte[], bool> isFolderLocal)
		{
			if (rules != null)
			{
				foreach (RuleData ruleData in rules)
				{
					if (ruleData.Actions != null)
					{
						for (int j = 0; j < ruleData.Actions.Length; j++)
						{
							RuleActionMoveCopyData ruleActionMoveCopyData = ruleData.Actions[j] as RuleActionMoveCopyData;
							if (ruleActionMoveCopyData != null)
							{
								bool folderIsLocal = isFolderLocal(ruleActionMoveCopyData.FolderEntryID);
								ruleData.Actions[j] = RuleActionMoveCopyData.ConvertToUplevel(ruleActionMoveCopyData, folderIsLocal);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000A1A8 File Offset: 0x000083A8
		public static void ConvertRulesToDownlevel(RuleData[] rules)
		{
			if (rules != null)
			{
				foreach (RuleData ruleData in rules)
				{
					if (ruleData.Actions != null)
					{
						for (int j = 0; j < ruleData.Actions.Length; j++)
						{
							RuleActionMoveCopyData ruleActionMoveCopyData = ruleData.Actions[j] as RuleActionMoveCopyData;
							if (ruleActionMoveCopyData != null)
							{
								ruleData.Actions[j] = RuleActionMoveCopyData.ConvertToDownlevel(ruleActionMoveCopyData);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000A20C File Offset: 0x0000840C
		public void Enumerate(CommonUtils.EnumPropTagDelegate propTagEnumerator, CommonUtils.EnumPropValueDelegate propValueEnumerator, CommonUtils.EnumAdrEntryDelegate adrEntryEnumerator)
		{
			if (this.Condition != null)
			{
				if (propTagEnumerator != null)
				{
					this.Condition.EnumeratePropTags(propTagEnumerator);
				}
				if (propValueEnumerator != null)
				{
					this.Condition.EnumeratePropValues(propValueEnumerator);
				}
			}
			if (this.Actions != null)
			{
				foreach (RuleActionData ruleActionData in this.Actions)
				{
					ruleActionData.Enumerate(propTagEnumerator, propValueEnumerator, adrEntryEnumerator);
				}
			}
			if (this.ExtraProperties != null)
			{
				foreach (PropValueData propValueData in this.ExtraProperties)
				{
					if (propTagEnumerator != null)
					{
						int propTag = propValueData.PropTag;
						propTagEnumerator(ref propTag);
						propValueData.PropTag = propTag;
					}
					if (propValueEnumerator != null)
					{
						propValueEnumerator(propValueData);
					}
				}
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000A2BC File Offset: 0x000084BC
		public Rule GetRule()
		{
			Rule rule = new Rule();
			rule.ExecutionSequence = this.ExecutionSequence;
			rule.Level = this.Level;
			rule.StateFlags = (RuleStateFlags)this.StateFlags;
			rule.UserFlags = this.UserFlags;
			rule.Condition = DataConverter<RestrictionConverter, Restriction, RestrictionData>.GetNative(this.Condition);
			rule.Actions = DataConverter<RuleActionConverter, RuleAction, RuleActionData>.GetNative(this.Actions);
			rule.Name = this.Name;
			rule.Provider = this.Provider;
			rule.ProviderData = this.ProviderData;
			rule.IsExtended = this.IsExtended;
			if (this.IsExtended)
			{
				rule.ExtraProperties = DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(this.ExtraProperties);
			}
			else if (this.ExtraProperties != null && this.ExtraProperties.Length > 0)
			{
				MrsTracer.Common.Error("Non-extended rule '{0}' has {1} extra properties", new object[]
				{
					this.Name,
					this.ExtraProperties.Length
				});
			}
			return rule;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000A3B0 File Offset: 0x000085B0
		public override string ToString()
		{
			return string.Format("Rule: Condition: {0}; Actions: {1}; Name '{2}'; Provider: '{3}'; ProviderData: {4}; ExecutionSequence: {5}; Level: {6}; StateFlags: {7}; UserFlags: {8}; IsExtended: {9}", new object[]
			{
				(this.Condition == null) ? "none" : this.Condition.ToString(),
				CommonUtils.ConcatEntries<RuleActionData>(this.Actions, null),
				this.Name,
				this.Provider,
				TraceUtils.DumpBytes(this.ProviderData),
				this.ExecutionSequence,
				this.Level,
				this.StateFlags,
				this.UserFlags,
				this.IsExtended
			});
		}
	}
}
