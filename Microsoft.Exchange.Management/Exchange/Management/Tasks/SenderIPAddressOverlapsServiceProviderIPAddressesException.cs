using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001027 RID: 4135
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SenderIPAddressOverlapsServiceProviderIPAddressesException : LocalizedException
	{
		// Token: 0x0600AF7D RID: 44925 RVA: 0x0029474C File Offset: 0x0029294C
		public SenderIPAddressOverlapsServiceProviderIPAddressesException(string ipRange) : base(Strings.SenderIPAddressOverlapsServiceProviderIPAddressesId(ipRange))
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF7E RID: 44926 RVA: 0x00294761 File Offset: 0x00292961
		public SenderIPAddressOverlapsServiceProviderIPAddressesException(string ipRange, Exception innerException) : base(Strings.SenderIPAddressOverlapsServiceProviderIPAddressesId(ipRange), innerException)
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF7F RID: 44927 RVA: 0x00294777 File Offset: 0x00292977
		protected SenderIPAddressOverlapsServiceProviderIPAddressesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipRange = (string)info.GetValue("ipRange", typeof(string));
		}

		// Token: 0x0600AF80 RID: 44928 RVA: 0x002947A1 File Offset: 0x002929A1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipRange", this.ipRange);
		}

		// Token: 0x170037FE RID: 14334
		// (get) Token: 0x0600AF81 RID: 44929 RVA: 0x002947BC File Offset: 0x002929BC
		public string IpRange
		{
			get
			{
				return this.ipRange;
			}
		}

		// Token: 0x04006164 RID: 24932
		private readonly string ipRange;
	}
}
