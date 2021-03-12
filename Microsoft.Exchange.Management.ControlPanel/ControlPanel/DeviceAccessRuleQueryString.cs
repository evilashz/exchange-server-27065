using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000323 RID: 803
	[DataContract]
	public class DeviceAccessRuleQueryString : IComparable
	{
		// Token: 0x17001EBF RID: 7871
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x0008EF6B File Offset: 0x0008D16B
		// (set) Token: 0x06002ED1 RID: 11985 RVA: 0x0008EF73 File Offset: 0x0008D173
		[DataMember]
		public bool IsWildcard { get; set; }

		// Token: 0x17001EC0 RID: 7872
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x0008EF7C File Offset: 0x0008D17C
		// (set) Token: 0x06002ED3 RID: 11987 RVA: 0x0008EF84 File Offset: 0x0008D184
		[DataMember]
		public string QueryString { get; set; }

		// Token: 0x06002ED4 RID: 11988 RVA: 0x0008EF90 File Offset: 0x0008D190
		public int CompareTo(object obj)
		{
			if (obj is DeviceAccessRuleQueryString)
			{
				DeviceAccessRuleQueryString deviceAccessRuleQueryString = (DeviceAccessRuleQueryString)obj;
				return this.QueryString.CompareTo(deviceAccessRuleQueryString.QueryString);
			}
			throw new ArgumentException("object is not a DeviceAccessRuleQueryString");
		}
	}
}
