using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x0200000B RID: 11
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class FindDomainsResponse : ResponseBase
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000027A4 File Offset: 0x000009A4
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000027AC File Offset: 0x000009AC
		[DataMember]
		public List<FindDomainResponse> DomainsResponse { get; set; }

		// Token: 0x06000032 RID: 50 RVA: 0x000027B8 File Offset: 0x000009B8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(500);
			try
			{
				stringBuilder.Append("<FindDomainsResponse>");
				if (this.DomainsResponse != null)
				{
					foreach (FindDomainResponse findDomainResponse in this.DomainsResponse)
					{
						stringBuilder.Append(findDomainResponse.ToString());
					}
				}
				stringBuilder.Append(base.ToString());
				stringBuilder.Append("</FindDomainsResponse>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
