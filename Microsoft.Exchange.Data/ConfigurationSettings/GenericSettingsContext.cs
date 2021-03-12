using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x02000201 RID: 513
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GenericSettingsContext : SettingsContextBase
	{
		// Token: 0x060011F6 RID: 4598 RVA: 0x00036145 File Offset: 0x00034345
		public GenericSettingsContext(string propertyName, string propertyValue, SettingsContextBase nextContext = null) : base(nextContext)
		{
			this.propertyName = propertyName;
			this.propertyValue = propertyValue;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0003615C File Offset: 0x0003435C
		public override string GetGenericProperty(string propertyName)
		{
			if (StringComparer.InvariantCultureIgnoreCase.Equals(propertyName, this.propertyName))
			{
				return this.propertyValue;
			}
			return null;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0003617C File Offset: 0x0003437C
		public override XElement GetDiagnosticInfo(string argument)
		{
			XElement diagnosticInfo = base.GetDiagnosticInfo(argument);
			diagnosticInfo.Add(new XElement(this.propertyName, this.propertyValue));
			return diagnosticInfo;
		}

		// Token: 0x04000AD8 RID: 2776
		private readonly string propertyName;

		// Token: 0x04000AD9 RID: 2777
		private readonly string propertyValue;
	}
}
