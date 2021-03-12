using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000099 RID: 153
	[CollectionDataContract(Name = "Domains", ItemName = "Domain", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class DomainCollection : Collection<string>
	{
		// Token: 0x060003DF RID: 991 RVA: 0x000179D3 File Offset: 0x00015BD3
		public DomainCollection()
		{
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000179DC File Offset: 0x00015BDC
		public DomainCollection(IEnumerable<string> domains)
		{
			foreach (string item in domains)
			{
				base.Add(item);
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00017A2C File Offset: 0x00015C2C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Count * 40);
			foreach (string value in this)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}
	}
}
