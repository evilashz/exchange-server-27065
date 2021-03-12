using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000033 RID: 51
	public abstract class ThrottleDescriptionEntry
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00005F14 File Offset: 0x00004114
		public ThrottleDescriptionEntry(ThrottleEntryType entryType, RecoveryActionId recoveryActionId, ResponderCategory responderCategory = ResponderCategory.Default, string responderTypeName = "*", string responderName = "*", string resourceName = "*")
		{
			this.ThrottleEntryType = entryType;
			this.RecoveryActionId = recoveryActionId;
			this.ResponderCategory = responderCategory;
			this.ResponderTypeName = (string.IsNullOrWhiteSpace(responderTypeName) ? "*" : responderTypeName);
			this.ResponderName = (string.IsNullOrWhiteSpace(responderName) ? "*" : responderName);
			this.ResourceName = (string.IsNullOrWhiteSpace(resourceName) ? "*" : resourceName);
			this.Identity = this.ConstructIdentity();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00005F90 File Offset: 0x00004190
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00005F98 File Offset: 0x00004198
		internal RecoveryActionId RecoveryActionId { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005FA1 File Offset: 0x000041A1
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00005FA9 File Offset: 0x000041A9
		internal ResponderCategory ResponderCategory { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005FB2 File Offset: 0x000041B2
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00005FBA File Offset: 0x000041BA
		internal string ResponderTypeName { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005FC3 File Offset: 0x000041C3
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00005FCB File Offset: 0x000041CB
		internal string ResponderName { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005FD4 File Offset: 0x000041D4
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00005FDC File Offset: 0x000041DC
		internal string ResourceName { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005FE5 File Offset: 0x000041E5
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00005FED File Offset: 0x000041ED
		internal string Identity { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00005FF6 File Offset: 0x000041F6
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00005FFE File Offset: 0x000041FE
		internal ThrottleEntryType ThrottleEntryType { get; private set; }

		// Token: 0x0600019F RID: 415 RVA: 0x00006008 File Offset: 0x00004208
		internal static bool IsMatchWildcard(string source, string target)
		{
			if (string.IsNullOrEmpty(source) || string.Equals(source, "*"))
			{
				return true;
			}
			if (string.IsNullOrEmpty(target))
			{
				return false;
			}
			string pattern = "^" + Regex.Escape(source).Replace("\\*", ".*").Replace("\\?", ".") + "$";
			return Regex.IsMatch(target, pattern, RegexOptions.IgnoreCase);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006074 File Offset: 0x00004274
		internal static ResponderCategory ParseResponderCategory(string categoryStr)
		{
			ResponderCategory result = ResponderCategory.Default;
			if (!string.IsNullOrWhiteSpace(categoryStr) && !Enum.TryParse<ResponderCategory>(categoryStr, true, out result))
			{
				result = ResponderCategory.Default;
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000609C File Offset: 0x0000429C
		internal string ConstructIdentity()
		{
			return string.Format("{0}/{1}/{2}/{3}/{4}", new object[]
			{
				this.RecoveryActionId,
				this.ResponderCategory,
				this.ResponderTypeName,
				this.ResponderName,
				this.ResourceName
			});
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000060F4 File Offset: 0x000042F4
		internal bool IsConfigMatches(RecoveryActionId recoveryActionId, ResponderCategory responderCategory, string responderTypeName, string responderName, string resourceName)
		{
			return (this.RecoveryActionId == RecoveryActionId.Any || this.RecoveryActionId == recoveryActionId) && (this.ResponderCategory == ResponderCategory.Default || this.ResponderCategory == responderCategory) && ThrottleDescriptionEntry.IsMatchWildcard(this.ResponderTypeName, responderTypeName) && ThrottleDescriptionEntry.IsMatchWildcard(this.ResponderName, responderName) && ThrottleDescriptionEntry.IsMatchWildcard(this.ResourceName, resourceName);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006158 File Offset: 0x00004358
		internal void WriteToCrimsonLog()
		{
			ManagedAvailabilityCrimsonEvent managedAvailabilityCrimsonEvent;
			switch (this.ThrottleEntryType)
			{
			case ThrottleEntryType.BaseConfig:
				managedAvailabilityCrimsonEvent = ManagedAvailabilityCrimsonEvents.ThrottleBaseConfig;
				break;
			case ThrottleEntryType.GlobalOverride:
				managedAvailabilityCrimsonEvent = ManagedAvailabilityCrimsonEvents.ThrottleGlobalOverride;
				break;
			case ThrottleEntryType.LocalOverride:
				managedAvailabilityCrimsonEvent = ManagedAvailabilityCrimsonEvents.ThrottleLocalOverride;
				break;
			default:
				managedAvailabilityCrimsonEvent = ManagedAvailabilityCrimsonEvents.ThrottleEffective;
				break;
			}
			XElement throttlePropertiesAsXml = this.GetThrottlePropertiesAsXml();
			managedAvailabilityCrimsonEvent.LogGeneric(new object[]
			{
				this.Identity,
				this.RecoveryActionId,
				this.ResponderCategory,
				this.ResponderTypeName,
				this.ResponderName,
				this.ResourceName,
				throttlePropertiesAsXml.ToString()
			});
		}

		// Token: 0x060001A4 RID: 420
		internal abstract Dictionary<string, string> GetPropertyBag();

		// Token: 0x060001A5 RID: 421 RVA: 0x00006218 File Offset: 0x00004418
		internal XElement GetThrottlePropertiesAsXml()
		{
			Dictionary<string, string> propertyBag = this.GetPropertyBag();
			XAttribute[] content = (from kvp in propertyBag
			select new XAttribute(kvp.Key, kvp.Value)).ToArray<XAttribute>();
			return new XElement("ThrottleConfig", content);
		}

		// Token: 0x040000E1 RID: 225
		internal const string Default = "*";
	}
}
