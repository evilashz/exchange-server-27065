using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006EC RID: 1772
	public static class WebServiceParametersExtensions
	{
		// Token: 0x06004A78 RID: 19064 RVA: 0x000E3AE8 File Offset: 0x000E1CE8
		public static void FaultIfNull(this WebServiceParameters properties)
		{
			if (properties == null)
			{
				throw new FaultException(new ArgumentNullException("properties").Message);
			}
		}
	}
}
