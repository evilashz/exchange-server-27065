using System;
using System.Globalization;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000043 RID: 67
	internal class DsnParamItem
	{
		// Token: 0x060002AE RID: 686 RVA: 0x000105A2 File Offset: 0x0000E7A2
		public DsnParamItem(string[] paramNames, DsnParamItem.DsnParamItemGetStringDelegate getStringDelegate)
		{
			if (paramNames == null)
			{
				throw new ArgumentNullException("paramNames");
			}
			if (getStringDelegate == null)
			{
				throw new ArgumentNullException("getStringDelegate");
			}
			this.paramNames = paramNames;
			this.getStringDelegate = getStringDelegate;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000105D4 File Offset: 0x0000E7D4
		public string GetString(DsnParameters dsnParameters, CultureInfo culture, out bool overwriteDefault)
		{
			if (!this.AllParametersAvailable(dsnParameters))
			{
				overwriteDefault = false;
				return null;
			}
			return this.getStringDelegate(dsnParameters, culture, out overwriteDefault);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000105F4 File Offset: 0x0000E7F4
		private bool AllParametersAvailable(DsnParameters dsnParameters)
		{
			foreach (string key in this.paramNames)
			{
				if (!dsnParameters.ContainsKey(key) || dsnParameters[key] == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400032C RID: 812
		private string[] paramNames;

		// Token: 0x0400032D RID: 813
		private DsnParamItem.DsnParamItemGetStringDelegate getStringDelegate;

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x060002B2 RID: 690
		public delegate string DsnParamItemGetStringDelegate(DsnParameters dsnParameters, CultureInfo culture, out bool overwriteDefault);
	}
}
