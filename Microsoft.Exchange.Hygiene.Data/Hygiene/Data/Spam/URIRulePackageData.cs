using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x02000212 RID: 530
	[Serializable]
	public class URIRulePackageData : ISerializable
	{
		// Token: 0x0600160A RID: 5642 RVA: 0x00044BBE File Offset: 0x00042DBE
		public URIRulePackageData()
		{
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00044BC6 File Offset: 0x00042DC6
		public URIRulePackageData(SerializationInfo info, StreamingContext context)
		{
			this.Rules = (URIRuleData[])info.GetValue("Rule", typeof(URIRuleData[]));
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00044BEE File Offset: 0x00042DEE
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Rule", this.Rules);
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00044C01 File Offset: 0x00042E01
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x00044C09 File Offset: 0x00042E09
		public URIRuleData[] Rules { get; set; }

		// Token: 0x02000213 RID: 531
		internal static class URIRulePackageDataConstants
		{
			// Token: 0x04000B1A RID: 2842
			internal const string RuleSerializationName = "Rule";
		}
	}
}
