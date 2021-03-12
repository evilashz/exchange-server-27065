using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200016E RID: 366
	public class DDISetToVariableConverter : IDDIValidationObjectConverter
	{
		// Token: 0x0600221A RID: 8730 RVA: 0x00066CCE File Offset: 0x00064ECE
		public object ConvertTo(object obj)
		{
			return (obj as Set).Variable;
		}
	}
}
