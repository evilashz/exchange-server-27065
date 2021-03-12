using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001026 RID: 4134
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SenderIPAddressOverlapsFfoDCIPAddressesException : LocalizedException
	{
		// Token: 0x0600AF78 RID: 44920 RVA: 0x002946D4 File Offset: 0x002928D4
		public SenderIPAddressOverlapsFfoDCIPAddressesException(string ipRange) : base(Strings.SenderIPAddressOverlapsFfoDCIPAddressesId(ipRange))
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF79 RID: 44921 RVA: 0x002946E9 File Offset: 0x002928E9
		public SenderIPAddressOverlapsFfoDCIPAddressesException(string ipRange, Exception innerException) : base(Strings.SenderIPAddressOverlapsFfoDCIPAddressesId(ipRange), innerException)
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF7A RID: 44922 RVA: 0x002946FF File Offset: 0x002928FF
		protected SenderIPAddressOverlapsFfoDCIPAddressesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipRange = (string)info.GetValue("ipRange", typeof(string));
		}

		// Token: 0x0600AF7B RID: 44923 RVA: 0x00294729 File Offset: 0x00292929
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipRange", this.ipRange);
		}

		// Token: 0x170037FD RID: 14333
		// (get) Token: 0x0600AF7C RID: 44924 RVA: 0x00294744 File Offset: 0x00292944
		public string IpRange
		{
			get
			{
				return this.ipRange;
			}
		}

		// Token: 0x04006163 RID: 24931
		private readonly string ipRange;
	}
}
