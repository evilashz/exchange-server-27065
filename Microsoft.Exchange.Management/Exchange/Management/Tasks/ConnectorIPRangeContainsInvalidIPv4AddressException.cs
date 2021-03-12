using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001020 RID: 4128
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConnectorIPRangeContainsInvalidIPv4AddressException : LocalizedException
	{
		// Token: 0x0600AF5A RID: 44890 RVA: 0x002943F7 File Offset: 0x002925F7
		public ConnectorIPRangeContainsInvalidIPv4AddressException(string ipRange) : base(Strings.ConnectorIPRangeContainsInvalidIPv4AddressId(ipRange))
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF5B RID: 44891 RVA: 0x0029440C File Offset: 0x0029260C
		public ConnectorIPRangeContainsInvalidIPv4AddressException(string ipRange, Exception innerException) : base(Strings.ConnectorIPRangeContainsInvalidIPv4AddressId(ipRange), innerException)
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF5C RID: 44892 RVA: 0x00294422 File Offset: 0x00292622
		protected ConnectorIPRangeContainsInvalidIPv4AddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipRange = (string)info.GetValue("ipRange", typeof(string));
		}

		// Token: 0x0600AF5D RID: 44893 RVA: 0x0029444C File Offset: 0x0029264C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipRange", this.ipRange);
		}

		// Token: 0x170037F7 RID: 14327
		// (get) Token: 0x0600AF5E RID: 44894 RVA: 0x00294467 File Offset: 0x00292667
		public string IpRange
		{
			get
			{
				return this.ipRange;
			}
		}

		// Token: 0x0400615D RID: 24925
		private readonly string ipRange;
	}
}
