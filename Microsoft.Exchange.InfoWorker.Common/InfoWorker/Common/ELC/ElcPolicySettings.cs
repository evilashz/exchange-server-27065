using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x020001A6 RID: 422
	internal sealed class ElcPolicySettings
	{
		// Token: 0x06000B1C RID: 2844 RVA: 0x0002F294 File Offset: 0x0002D494
		internal ElcPolicySettings(ContentSetting contentSettings, string messageClass)
		{
			this.contentSettings = contentSettings;
			this.messageClass = messageClass;
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0002F2AA File Offset: 0x0002D4AA
		public EnhancedTimeSpan? AgeLimitForRetention
		{
			get
			{
				return this.ContentSettings.AgeLimitForRetention;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002F2B7 File Offset: 0x0002D4B7
		public RetentionActionType RetentionAction
		{
			get
			{
				return this.ContentSettings.RetentionAction;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0002F2C4 File Offset: 0x0002D4C4
		public RetentionDateType TriggerForRetention
		{
			get
			{
				return this.ContentSettings.TriggerForRetention;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0002F2D1 File Offset: 0x0002D4D1
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x0002F2D9 File Offset: 0x0002D4D9
		internal ContentSetting ContentSettings
		{
			get
			{
				return this.contentSettings;
			}
			set
			{
				this.contentSettings = value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0002F2E2 File Offset: 0x0002D4E2
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0002F2EA File Offset: 0x0002D4EA
		internal string MessageClass
		{
			get
			{
				return this.messageClass;
			}
			set
			{
				this.messageClass = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0002F2F3 File Offset: 0x0002D4F3
		internal bool JournalingEnabled
		{
			get
			{
				return this.ContentSettings.JournalingEnabled;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0002F300 File Offset: 0x0002D500
		internal bool RetentionEnabled
		{
			get
			{
				return this.ContentSettings.RetentionEnabled;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0002F30D File Offset: 0x0002D50D
		internal string Name
		{
			get
			{
				return this.ContentSettings.Name;
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002F31C File Offset: 0x0002D51C
		internal static void ParseContentSettings(List<ElcPolicySettings> policyList, ContentSetting elcContentSetting)
		{
			if (ElcMessageClass.IsMultiMessageClass(elcContentSetting.MessageClass))
			{
				string[] array = elcContentSetting.MessageClass.Split(ElcMessageClass.MessageClassDelims, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					policyList.Add(new ElcPolicySettings(elcContentSetting, text));
				}
				return;
			}
			policyList.Add(new ElcPolicySettings(elcContentSetting, elcContentSetting.MessageClass));
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002F37C File Offset: 0x0002D57C
		internal static ContentSetting GetApplyingPolicy(List<ElcPolicySettings> elcPolicies, string itemClass, Dictionary<string, ContentSetting> itemClassToPolicyMapping)
		{
			return ElcPolicySettings.GetApplyingPolicy(elcPolicies, itemClass, itemClassToPolicyMapping, itemClass);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002F388 File Offset: 0x0002D588
		internal static ContentSetting GetApplyingPolicy(IEnumerable<ElcPolicySettings> elcPolicies, string itemClass, Dictionary<string, ContentSetting> itemClassToPolicyMapping, string cacheKey)
		{
			if (itemClassToPolicyMapping.ContainsKey(cacheKey))
			{
				return itemClassToPolicyMapping[cacheKey];
			}
			if (elcPolicies == null)
			{
				return null;
			}
			string effectiveItemClass = ElcPolicySettings.GetEffectiveItemClass(itemClass);
			ContentSetting contentSetting = null;
			foreach (ElcPolicySettings elcPolicySettings in elcPolicies)
			{
				if (elcPolicySettings.MessageClass == "*")
				{
					contentSetting = elcPolicySettings.ContentSettings;
				}
				else if (!string.IsNullOrEmpty(effectiveItemClass))
				{
					if (string.Compare(itemClass, elcPolicySettings.MessageClass, StringComparison.OrdinalIgnoreCase) == 0)
					{
						contentSetting = elcPolicySettings.ContentSettings;
						break;
					}
					if (elcPolicySettings.MessageClass.EndsWith("*") && effectiveItemClass.StartsWith(elcPolicySettings.MessageClass.TrimEnd(new char[]
					{
						'*'
					}), StringComparison.OrdinalIgnoreCase))
					{
						contentSetting = elcPolicySettings.ContentSettings;
						break;
					}
				}
			}
			itemClassToPolicyMapping[cacheKey] = contentSetting;
			return contentSetting;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002F474 File Offset: 0x0002D674
		internal static string GetEffectiveItemClass(string itemClass)
		{
			string result = itemClass;
			if (string.Compare(itemClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA", StringComparison.OrdinalIgnoreCase) == 0)
			{
				result = "IPM.Note.Microsoft.Voicemail.UM.CA";
			}
			else if (string.Compare(itemClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM", StringComparison.OrdinalIgnoreCase) == 0)
			{
				result = "IPM.Note.Microsoft.Voicemail.UM";
			}
			return result;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002F4B0 File Offset: 0x0002D6B0
		internal static bool ArePolicyPropertiesValid(ContentSetting policy, object objectToTrace, string mailboxAddress)
		{
			if (policy == null)
			{
				ElcPolicySettings.Tracer.TraceDebug(0L, "{0}: Policy is null.", new object[]
				{
					objectToTrace
				});
				return false;
			}
			if (!policy.RetentionEnabled)
			{
				ElcPolicySettings.Tracer.TraceDebug<object, string>(0L, "{0}: Policy '{1}' is disabled.", objectToTrace, policy.Name);
				return false;
			}
			EnhancedTimeSpan? ageLimitForRetention = policy.AgeLimitForRetention;
			TimeSpan? timeSpan = (ageLimitForRetention != null) ? new TimeSpan?(ageLimitForRetention.GetValueOrDefault()) : null;
			if (timeSpan == null || timeSpan.Value.TotalDays <= 0.0)
			{
				ElcPolicySettings.Tracer.TraceDebug<object, string>(0L, "{0}: Age limit of Policy '{1}' is not greater than 0.", objectToTrace, policy.Name);
				Globals.ELCLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_InvalidRetentionAgeLimit, "Managed Folder Assistant Run", new object[]
				{
					policy.Name,
					mailboxAddress
				});
				return false;
			}
			return true;
		}

		// Token: 0x04000847 RID: 2119
		private static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;

		// Token: 0x04000848 RID: 2120
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000849 RID: 2121
		private ContentSetting contentSettings;

		// Token: 0x0400084A RID: 2122
		private string messageClass;
	}
}
