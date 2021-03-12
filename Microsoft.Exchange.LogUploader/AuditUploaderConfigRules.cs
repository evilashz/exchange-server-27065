using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000008 RID: 8
	public class AuditUploaderConfigRules
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000374A File Offset: 0x0000194A
		public int Count
		{
			get
			{
				return this.auditUploaderRules.Count;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003758 File Offset: 0x00001958
		public Actions GetOperation(string component, string tenant, string user, string operation, DateTime currentTime)
		{
			AuditUploaderDictionaryKey currentKey = new AuditUploaderDictionaryKey(component, tenant, user, operation);
			int num = 0;
			while ((double)num < Math.Pow((double)AuditUploaderDictionaryKey.NumberOfFields, 2.0))
			{
				AuditUploaderDictionaryKey nextKey = this.GetNextKey(currentKey, num);
				if (this.auditUploaderRules == null)
				{
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_InvalidRuleCollection, "Audit Uploader rules collection has not been generated yet.", new object[0]);
					return Actions.Skip;
				}
				if (this.auditUploaderRules.ContainsKey(nextKey))
				{
					TimeSpan? actionThrottlingInterval = this.auditUploaderRules[nextKey].ActionThrottlingInterval;
					if (actionThrottlingInterval == null || currentTime.Subtract(actionThrottlingInterval.Value).CompareTo(this.auditUploaderRules[nextKey].LastTriggerDate) > 0)
					{
						this.auditUploaderRules[nextKey].LastTriggerDate = currentTime;
						return this.auditUploaderRules[nextKey].ActionToPerform;
					}
					return Actions.LetThrough;
				}
				else
				{
					num++;
				}
			}
			EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_InvalidRuleCollection, "Audit Uploader rules collection does not contain a matching rule for the given entry.", new object[0]);
			return Actions.LetThrough;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000386D File Offset: 0x00001A6D
		public void Add(AuditUploaderDictionaryKey key, AuditUploaderAction action)
		{
			this.auditUploaderRules.Add(key, action);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000387C File Offset: 0x00001A7C
		public bool Contains(AuditUploaderDictionaryKey key)
		{
			return this.auditUploaderRules.ContainsKey(key);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000388C File Offset: 0x00001A8C
		private AuditUploaderDictionaryKey GetNextKey(AuditUploaderDictionaryKey currentKey, int index)
		{
			this.tempKey.CopyFrom(currentKey);
			for (int i = 0; i < AuditUploaderDictionaryKey.NumberOfFields; i++)
			{
				if ((index >> i & 1) == 1)
				{
					this.tempKey[i] = AuditUploaderDictionaryKey.WildCard;
				}
			}
			return this.tempKey;
		}

		// Token: 0x0400003E RID: 62
		private Dictionary<AuditUploaderDictionaryKey, AuditUploaderAction> auditUploaderRules = new Dictionary<AuditUploaderDictionaryKey, AuditUploaderAction>();

		// Token: 0x0400003F RID: 63
		private AuditUploaderDictionaryKey tempKey = new AuditUploaderDictionaryKey(AuditUploaderDictionaryKey.WildCard, AuditUploaderDictionaryKey.WildCard, AuditUploaderDictionaryKey.WildCard, AuditUploaderDictionaryKey.WildCard);
	}
}
