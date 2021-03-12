using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001021 RID: 4129
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPRangeInConnectorContainsReservedIPAddressesException : LocalizedException
	{
		// Token: 0x0600AF5F RID: 44895 RVA: 0x0029446F File Offset: 0x0029266F
		public IPRangeInConnectorContainsReservedIPAddressesException(string ipRange) : base(Strings.IPRangeInConnectorContainsReservedIPAddressesId(ipRange))
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF60 RID: 44896 RVA: 0x00294484 File Offset: 0x00292684
		public IPRangeInConnectorContainsReservedIPAddressesException(string ipRange, Exception innerException) : base(Strings.IPRangeInConnectorContainsReservedIPAddressesId(ipRange), innerException)
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF61 RID: 44897 RVA: 0x0029449A File Offset: 0x0029269A
		protected IPRangeInConnectorContainsReservedIPAddressesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipRange = (string)info.GetValue("ipRange", typeof(string));
		}

		// Token: 0x0600AF62 RID: 44898 RVA: 0x002944C4 File Offset: 0x002926C4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipRange", this.ipRange);
		}

		// Token: 0x170037F8 RID: 14328
		// (get) Token: 0x0600AF63 RID: 44899 RVA: 0x002944DF File Offset: 0x002926DF
		public string IpRange
		{
			get
			{
				return this.ipRange;
			}
		}

		// Token: 0x0400615E RID: 24926
		private readonly string ipRange;
	}
}
