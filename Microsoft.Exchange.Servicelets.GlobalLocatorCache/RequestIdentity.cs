using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x0200001A RID: 26
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class RequestIdentity
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004035 File Offset: 0x00002235
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000403D File Offset: 0x0000223D
		[DataMember(IsRequired = true)]
		public string CallerId { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004046 File Offset: 0x00002246
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000404E File Offset: 0x0000224E
		[DataMember(IsRequired = false)]
		public Guid TrackingGuid { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004057 File Offset: 0x00002257
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000405F File Offset: 0x0000225F
		[DataMember(IsRequired = false)]
		public Guid RequestTrackingGuid { get; set; }

		// Token: 0x06000084 RID: 132 RVA: 0x00004068 File Offset: 0x00002268
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			try
			{
				stringBuilder.Append("<RequestIdentity>");
				stringBuilder.AppendFormat("CallerId={0},TrackingGuid={1},RequestTrackingGuid={2}", this.CallerId, this.TrackingGuid, this.RequestTrackingGuid);
				stringBuilder.Append("</RequestIdentity>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
