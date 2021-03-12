using System;
using System.Collections;
using System.Management.Automation;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rps.Probes
{
	// Token: 0x02000439 RID: 1081
	internal static class RpsTipProbeUtilities
	{
		// Token: 0x06001BC0 RID: 7104 RVA: 0x0009D224 File Offset: 0x0009B424
		public static object GetPropertyValue(this PSObject psObject, string propertyName)
		{
			object obj = psObject.Properties[propertyName].Value;
			if (obj != null && obj is PSObject)
			{
				obj = ((PSObject)obj).BaseObject;
			}
			return obj;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0009D25C File Offset: 0x0009B45C
		public static T GetPropertyValue<T>(this PSObject psObject, string propertyName, T defaultValue)
		{
			object propertyValue = psObject.GetPropertyValue(propertyName);
			if (propertyValue != null)
			{
				return (T)((object)propertyValue);
			}
			return defaultValue;
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0009D27C File Offset: 0x0009B47C
		public static string GetStringValue(this PSObject psObject, string propertyName)
		{
			object propertyValue = psObject.GetPropertyValue(propertyName);
			if (propertyValue != null && propertyValue is IList)
			{
				return ((IList)propertyValue)[0] as string;
			}
			return propertyValue as string;
		}
	}
}
