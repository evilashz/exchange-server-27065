using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001019 RID: 4121
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPv6AddressesAreNotAllowedInInternalServerIPAddressesException : LocalizedException
	{
		// Token: 0x0600AF3A RID: 44858 RVA: 0x0029418A File Offset: 0x0029238A
		public IPv6AddressesAreNotAllowedInInternalServerIPAddressesException(string ip) : base(Strings.IPv6AddressesAreNotAllowedInInternalServerIPAddressesId(ip))
		{
			this.ip = ip;
		}

		// Token: 0x0600AF3B RID: 44859 RVA: 0x0029419F File Offset: 0x0029239F
		public IPv6AddressesAreNotAllowedInInternalServerIPAddressesException(string ip, Exception innerException) : base(Strings.IPv6AddressesAreNotAllowedInInternalServerIPAddressesId(ip), innerException)
		{
			this.ip = ip;
		}

		// Token: 0x0600AF3C RID: 44860 RVA: 0x002941B5 File Offset: 0x002923B5
		protected IPv6AddressesAreNotAllowedInInternalServerIPAddressesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ip = (string)info.GetValue("ip", typeof(string));
		}

		// Token: 0x0600AF3D RID: 44861 RVA: 0x002941DF File Offset: 0x002923DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ip", this.ip);
		}

		// Token: 0x170037F3 RID: 14323
		// (get) Token: 0x0600AF3E RID: 44862 RVA: 0x002941FA File Offset: 0x002923FA
		public string Ip
		{
			get
			{
				return this.ip;
			}
		}

		// Token: 0x04006159 RID: 24921
		private readonly string ip;
	}
}
