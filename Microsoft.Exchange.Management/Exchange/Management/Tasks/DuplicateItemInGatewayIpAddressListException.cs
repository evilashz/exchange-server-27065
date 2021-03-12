using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001018 RID: 4120
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DuplicateItemInGatewayIpAddressListException : LocalizedException
	{
		// Token: 0x0600AF35 RID: 44853 RVA: 0x00294112 File Offset: 0x00292312
		public DuplicateItemInGatewayIpAddressListException(string ip) : base(Strings.DuplicateItemInGatewayIpAddressListId(ip))
		{
			this.ip = ip;
		}

		// Token: 0x0600AF36 RID: 44854 RVA: 0x00294127 File Offset: 0x00292327
		public DuplicateItemInGatewayIpAddressListException(string ip, Exception innerException) : base(Strings.DuplicateItemInGatewayIpAddressListId(ip), innerException)
		{
			this.ip = ip;
		}

		// Token: 0x0600AF37 RID: 44855 RVA: 0x0029413D File Offset: 0x0029233D
		protected DuplicateItemInGatewayIpAddressListException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ip = (string)info.GetValue("ip", typeof(string));
		}

		// Token: 0x0600AF38 RID: 44856 RVA: 0x00294167 File Offset: 0x00292367
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ip", this.ip);
		}

		// Token: 0x170037F2 RID: 14322
		// (get) Token: 0x0600AF39 RID: 44857 RVA: 0x00294182 File Offset: 0x00292382
		public string Ip
		{
			get
			{
				return this.ip;
			}
		}

		// Token: 0x04006158 RID: 24920
		private readonly string ip;
	}
}
