using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200004A RID: 74
	public abstract class ThrottleSettingsBase
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000ACFD File Offset: 0x00008EFD
		public ThrottleSettingsBase()
		{
			this.fixedEntries = new FixedThrottleEntry[0];
			this.globalOverrides = new OverrideThrottleEntry[0];
			this.localOverrides = new OverrideThrottleEntry[0];
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000AD48 File Offset: 0x00008F48
		public void Initialize(IEnumerable<FixedThrottleEntry> fixedEntries, IEnumerable<WorkDefinitionOverride> globalOverrides, IEnumerable<WorkDefinitionOverride> localOverrides)
		{
			this.fixedEntries = this.OrderEntries<FixedThrottleEntry>(fixedEntries);
			if (globalOverrides != null)
			{
				this.globalOverrides = this.OrderEntries<OverrideThrottleEntry>(from rawOverride in globalOverrides
				select OverrideThrottleEntry.ParseRawOverride(ThrottleEntryType.GlobalOverride, rawOverride));
			}
			if (localOverrides != null)
			{
				this.localOverrides = this.OrderEntries<OverrideThrottleEntry>(from rawOverride in localOverrides
				select OverrideThrottleEntry.ParseRawOverride(ThrottleEntryType.LocalOverride, rawOverride));
			}
		}

		// Token: 0x06000318 RID: 792
		public abstract string[] GetServersInGroup(string categoryName);

		// Token: 0x06000319 RID: 793
		public abstract FixedThrottleEntry ConstructDefaultThrottlingSettings(RecoveryActionId recoveryActionId);

		// Token: 0x0600031A RID: 794 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		public FixedThrottleEntry GetThrottleDefinition(RecoveryActionId recoveryActionId, string resourceName, ResponderDefinition responderDefinition)
		{
			return this.GetThrottleDefinition(recoveryActionId, responderDefinition.ResponderCategory, responderDefinition.TypeName, responderDefinition.Name, resourceName, responderDefinition.ServiceName);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public FixedThrottleEntry GetThrottleDefinition(RecoveryActionId recoveryActionId, string responderCategoryStr, string responderTypeName, string responderName, string resourceName, string healthSetName)
		{
			ResponderCategory responderCategory = ThrottleDescriptionEntry.ParseResponderCategory(responderCategoryStr);
			FixedThrottleEntry fixedMatchingEntry = this.GetFixedMatchingEntry(recoveryActionId, responderCategory, responderTypeName, responderName, resourceName);
			this.ApplyOverrides(healthSetName, fixedMatchingEntry, this.globalOverrides);
			this.ApplyOverrides(healthSetName, fixedMatchingEntry, this.localOverrides);
			return fixedMatchingEntry;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000AE3C File Offset: 0x0000903C
		public string ConvertThrottleDefinitionsToCompactXml(IEnumerable<ThrottleDescriptionEntry> entries)
		{
			XElement xelement = new XElement("ThrottleEntries");
			foreach (ThrottleDescriptionEntry throttleDescriptionEntry in entries)
			{
				XElement xelement2 = new XElement(throttleDescriptionEntry.RecoveryActionId.ToString(), new XAttribute("ResourceName", throttleDescriptionEntry.ResourceName));
				xelement2.Add(throttleDescriptionEntry.GetThrottlePropertiesAsXml());
				xelement.Add(xelement2);
			}
			return xelement.ToString();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000AED8 File Offset: 0x000090D8
		public string GetThrottleDefinitionsAsCompactXml(RecoveryActionId recoveryActionId, string resourceName, ResponderDefinition responderDefinition)
		{
			return this.ConvertThrottleDefinitionsToCompactXml(new List<ThrottleDescriptionEntry>
			{
				this.GetThrottleDefinition(recoveryActionId, resourceName, responderDefinition)
			});
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000AF5C File Offset: 0x0000915C
		internal T[] OrderEntries<T>(IEnumerable<T> entries) where T : ThrottleDescriptionEntry
		{
			IOrderedEnumerable<T> source = from entry in entries
			where entry != null
			orderby entry.RecoveryActionId, entry.ResponderCategory, entry.ResponderTypeName, entry.ResponderName, entry.ResourceName
			select entry;
			return source.ToArray<T>();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B008 File Offset: 0x00009208
		internal FixedThrottleEntry GetFixedMatchingEntry(RecoveryActionId recoveryActionId, ResponderCategory responderCategory, string responderTypeName, string responderName, string resourceName)
		{
			FixedThrottleEntry fixedThrottleEntry = this.fixedEntries.LastOrDefault((FixedThrottleEntry v) => v.IsConfigMatches(recoveryActionId, responderCategory, responderTypeName, responderName, resourceName));
			if (fixedThrottleEntry == null)
			{
				WTFDiagnostics.TraceDebug<RecoveryActionId>(ExTraceGlobals.RecoveryActionTracer, this.traceContext, "RecoveryActionId: {0} is not registered with throttling framework. Using the default throttle settings", recoveryActionId, null, "GetFixedMatchingEntry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\ThrottleSettingsBase.cs", 255);
				fixedThrottleEntry = this.ConstructDefaultThrottlingSettings(recoveryActionId);
			}
			return new FixedThrottleEntry(recoveryActionId, responderCategory, responderTypeName, responderName, resourceName, fixedThrottleEntry.ThrottleParameters);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B0C0 File Offset: 0x000092C0
		internal void ApplyOverrides(string healthSetName, FixedThrottleEntry baseEntry, IEnumerable<OverrideThrottleEntry> overrideEntries)
		{
			foreach (OverrideThrottleEntry overrideThrottleEntry in overrideEntries)
			{
				if (overrideThrottleEntry.IsConfigMatches(baseEntry.RecoveryActionId, baseEntry.ResponderCategory, baseEntry.ResponderTypeName, baseEntry.ResponderName, baseEntry.ResourceName))
				{
					baseEntry.ThrottleParameters.ApplyPropertyOverrides(overrideThrottleEntry.PropertyBag);
				}
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B188 File Offset: 0x00009388
		protected void ReportAllThrottleEntriesToCrimson(bool isLogAsync)
		{
			Action action = delegate()
			{
				NativeMethods.EvtClearLog(IntPtr.Zero, "Microsoft-Exchange-ManagedAvailability/ThrottlingConfig", null, 0);
				this.ReportEntriesToCrimsonEvent(this.fixedEntries);
				this.ReportEntriesToCrimsonEvent(this.globalOverrides);
				this.ReportEntriesToCrimsonEvent(this.localOverrides);
			};
			if (isLogAsync)
			{
				ThreadPool.QueueUserWorkItem(delegate(object unused)
				{
					action();
				});
				return;
			}
			action();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B1DC File Offset: 0x000093DC
		private void ReportEntriesToCrimsonEvent(IEnumerable<ThrottleDescriptionEntry> entries)
		{
			foreach (ThrottleDescriptionEntry throttleDescriptionEntry in entries)
			{
				throttleDescriptionEntry.WriteToCrimsonLog();
			}
		}

		// Token: 0x040001E3 RID: 483
		public const string ThrottlingConfigChannelName = "Microsoft-Exchange-ManagedAvailability/ThrottlingConfig";

		// Token: 0x040001E4 RID: 484
		private FixedThrottleEntry[] fixedEntries;

		// Token: 0x040001E5 RID: 485
		private OverrideThrottleEntry[] globalOverrides;

		// Token: 0x040001E6 RID: 486
		private OverrideThrottleEntry[] localOverrides;

		// Token: 0x040001E7 RID: 487
		private TracingContext traceContext = TracingContext.Default;
	}
}
