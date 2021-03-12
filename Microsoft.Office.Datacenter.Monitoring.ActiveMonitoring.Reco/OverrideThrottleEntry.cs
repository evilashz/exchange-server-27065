using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000035 RID: 53
	internal class OverrideThrottleEntry : ThrottleDescriptionEntry
	{
		// Token: 0x060001AD RID: 429 RVA: 0x000062FC File Offset: 0x000044FC
		internal OverrideThrottleEntry(ThrottleEntryType entryType, RecoveryActionId recoveryActionId, ResponderCategory responderCategory, string responderTypeName, string responderName, string resourceName, string propertyName, WorkDefinitionOverride rawOverride) : base(entryType, recoveryActionId, responderCategory, responderTypeName, responderName, resourceName)
		{
			this.PropertyBag = new Dictionary<string, string>
			{
				{
					propertyName,
					rawOverride.NewPropertyValue
				}
			};
			this.RawOverride = rawOverride;
			this.HealthSetName = rawOverride.ServiceName;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006349 File Offset: 0x00004549
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00006351 File Offset: 0x00004551
		internal Dictionary<string, string> PropertyBag { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000635A File Offset: 0x0000455A
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00006362 File Offset: 0x00004562
		internal WorkDefinitionOverride RawOverride { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000636B File Offset: 0x0000456B
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00006373 File Offset: 0x00004573
		internal string HealthSetName { get; private set; }

		// Token: 0x060001B4 RID: 436 RVA: 0x00006384 File Offset: 0x00004584
		internal static OverrideThrottleEntry ParseRawOverride(ThrottleEntryType entryType, WorkDefinitionOverride rawOverride)
		{
			RecoveryActionId recoveryActionId = RecoveryActionId.None;
			ResponderCategory responderCategory = ResponderCategory.Default;
			string responderTypeName = "*";
			string responderName = "*";
			string resourceName = "*";
			string propertyName = string.Empty;
			string serviceName = rawOverride.ServiceName;
			string workDefinitionName = rawOverride.WorkDefinitionName;
			string propertyName2 = rawOverride.PropertyName;
			if (string.IsNullOrWhiteSpace(propertyName2))
			{
				return null;
			}
			if (string.Equals(serviceName, ExchangeComponent.RecoveryAction.Name))
			{
				if (!Enum.TryParse<RecoveryActionId>(workDefinitionName, true, out recoveryActionId))
				{
					return null;
				}
				string[] array = (from o in propertyName2.Split(new char[]
				{
					'/',
					':'
				})
				select o.Trim()).ToArray<string>();
				if (array.Length > 1)
				{
					if (!string.Equals(array[0], "*", StringComparison.OrdinalIgnoreCase) && !Enum.TryParse<ResponderCategory>(array[0], true, out responderCategory))
					{
						return null;
					}
					if (array.Length > 2)
					{
						responderTypeName = array[1];
					}
					if (array.Length > 3)
					{
						responderName = array[2];
					}
					if (array.Length > 4)
					{
						resourceName = array[3];
					}
				}
				propertyName = array[array.Length - 1];
			}
			else
			{
				string[] array2 = propertyName2.Split(new char[]
				{
					'.'
				});
				if (array2.Length < 2 || !string.Equals(array2[0], "ThrottleAttributes"))
				{
					return null;
				}
				recoveryActionId = RecoveryActionId.Any;
				responderName = workDefinitionName;
				propertyName = array2[1];
				string[] array3 = workDefinitionName.Split(new char[]
				{
					'/'
				});
				if (array3.Length > 1)
				{
					responderName = array3[0];
					resourceName = array3[1];
				}
			}
			return new OverrideThrottleEntry(entryType, recoveryActionId, responderCategory, responderTypeName, responderName, resourceName, propertyName, rawOverride);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006518 File Offset: 0x00004718
		internal bool IsHealthSetMatching(string healthSetName)
		{
			return !string.IsNullOrEmpty(this.HealthSetName) && !string.IsNullOrEmpty(healthSetName) && (string.Equals(this.HealthSetName, healthSetName, StringComparison.OrdinalIgnoreCase) || string.Equals(this.HealthSetName, ExchangeComponent.RecoveryAction.Name, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006568 File Offset: 0x00004768
		internal override Dictionary<string, string> GetPropertyBag()
		{
			return this.PropertyBag;
		}
	}
}
