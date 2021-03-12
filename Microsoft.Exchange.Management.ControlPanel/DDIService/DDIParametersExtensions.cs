using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000136 RID: 310
	public static class DDIParametersExtensions
	{
		// Token: 0x060020ED RID: 8429 RVA: 0x00063F65 File Offset: 0x00062165
		public static void FaultIfNull(this DDIParameters properties, string parameterName)
		{
			if (properties == null)
			{
				throw new FaultException(new ArgumentNullException(parameterName).Message);
			}
		}
	}
}
