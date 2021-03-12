using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x02000007 RID: 7
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class FindDomainRequest
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000234C File Offset: 0x0000054C
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002354 File Offset: 0x00000554
		[DataMember(IsRequired = true)]
		public DomainQuery Domain { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000235D File Offset: 0x0000055D
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002365 File Offset: 0x00000565
		[DataMember]
		public TenantQuery Tenant { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000236E File Offset: 0x0000056E
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002376 File Offset: 0x00000576
		[DataMember(IsRequired = false)]
		public int ReadFlag { get; set; }

		// Token: 0x06000018 RID: 24 RVA: 0x00002380 File Offset: 0x00000580
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(300);
			try
			{
				stringBuilder.Append("<FindDomainRequest>");
				stringBuilder.Append(this.Domain.ToString());
				if (this.Tenant != null)
				{
					stringBuilder.Append(this.Tenant.ToString());
				}
				stringBuilder.AppendFormat("ReadFlag={0}", this.ReadFlag);
				stringBuilder.Append("</FindDomainRequest>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
