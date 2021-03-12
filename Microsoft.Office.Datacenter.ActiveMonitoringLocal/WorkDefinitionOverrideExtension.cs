using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x020000A0 RID: 160
	internal static class WorkDefinitionOverrideExtension
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x000202E4 File Offset: 0x0001E4E4
		internal static void TryApplyTo(this WorkDefinitionOverride definitionOverride, WorkDefinition definition, TracingContext traceContext)
		{
			if (string.Compare(definitionOverride.ServiceName, definition.ServiceName, StringComparison.OrdinalIgnoreCase) != 0 || string.Compare(definitionOverride.WorkDefinitionName, definition.Name, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return;
			}
			if (!string.IsNullOrWhiteSpace(definitionOverride.Scope) && string.Compare(definitionOverride.Scope, definition.TargetResource, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return;
			}
			try
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add(definitionOverride.PropertyName, definitionOverride.NewPropertyValue);
				((IPersistence)definition).SetProperties(dictionary);
				if (definitionOverride.PropertyName == "ExtensionAttributes")
				{
					definition.ParseExtensionAttributes(true);
				}
				if (definitionOverride.PropertyName.StartsWith("ExtensionAttributes."))
				{
					string text = definitionOverride.PropertyName.Substring("ExtensionAttributes.".Length);
					if (!string.IsNullOrWhiteSpace(text))
					{
						definition.Attributes[text] = definitionOverride.NewPropertyValue;
						definition.SyncExtensionAttributes(true);
					}
				}
			}
			catch (Exception arg)
			{
				if (definitionOverride.PropertyName == "ExtensionAttributes")
				{
					definition.ExtensionAttributes = null;
				}
				WTFDiagnostics.TraceError<Exception>(WTFLog.DataAccess, traceContext, "[WorkDefinitionOverrideExtension.TryApplyTo]: Failed to apply override: {0}", arg, null, "TryApplyTo", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\WorkDefinitionOverrideExtension.cs", 93);
			}
		}

		// Token: 0x040005F2 RID: 1522
		private const string ExtensionAttributesName = "ExtensionAttributes";

		// Token: 0x040005F3 RID: 1523
		private const string ExtensionAttributesOverridePrefix = "ExtensionAttributes.";
	}
}
