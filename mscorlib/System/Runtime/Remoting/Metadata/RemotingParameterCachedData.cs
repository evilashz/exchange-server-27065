using System;
using System.Reflection;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007A5 RID: 1957
	internal class RemotingParameterCachedData : RemotingCachedData
	{
		// Token: 0x060055B1 RID: 21937 RVA: 0x0012F947 File Offset: 0x0012DB47
		internal RemotingParameterCachedData(RuntimeParameterInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x0012F958 File Offset: 0x0012DB58
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapParameterAttribute), true);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapParameterAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapParameterAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x040026FC RID: 9980
		private RuntimeParameterInfo RI;
	}
}
