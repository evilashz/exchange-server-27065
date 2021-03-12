using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007A4 RID: 1956
	internal class RemotingFieldCachedData : RemotingCachedData
	{
		// Token: 0x060055AE RID: 21934 RVA: 0x0012F8DC File Offset: 0x0012DADC
		internal RemotingFieldCachedData(RuntimeFieldInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x0012F8EB File Offset: 0x0012DAEB
		internal RemotingFieldCachedData(SerializationFieldInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x0012F8FC File Offset: 0x0012DAFC
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapFieldAttribute), false);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapFieldAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x040026FB RID: 9979
		private FieldInfo RI;
	}
}
