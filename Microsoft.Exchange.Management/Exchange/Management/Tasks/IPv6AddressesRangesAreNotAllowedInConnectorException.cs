using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001022 RID: 4130
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPv6AddressesRangesAreNotAllowedInConnectorException : LocalizedException
	{
		// Token: 0x0600AF64 RID: 44900 RVA: 0x002944E7 File Offset: 0x002926E7
		public IPv6AddressesRangesAreNotAllowedInConnectorException(string ipRange) : base(Strings.IPv6AddressesRangesAreNotAllowedInConnectorId(ipRange))
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF65 RID: 44901 RVA: 0x002944FC File Offset: 0x002926FC
		public IPv6AddressesRangesAreNotAllowedInConnectorException(string ipRange, Exception innerException) : base(Strings.IPv6AddressesRangesAreNotAllowedInConnectorId(ipRange), innerException)
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF66 RID: 44902 RVA: 0x00294512 File Offset: 0x00292712
		protected IPv6AddressesRangesAreNotAllowedInConnectorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipRange = (string)info.GetValue("ipRange", typeof(string));
		}

		// Token: 0x0600AF67 RID: 44903 RVA: 0x0029453C File Offset: 0x0029273C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipRange", this.ipRange);
		}

		// Token: 0x170037F9 RID: 14329
		// (get) Token: 0x0600AF68 RID: 44904 RVA: 0x00294557 File Offset: 0x00292757
		public string IpRange
		{
			get
			{
				return this.ipRange;
			}
		}

		// Token: 0x0400615F RID: 24927
		private readonly string ipRange;
	}
}
