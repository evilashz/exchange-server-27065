using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F74 RID: 3956
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidFqdnUnderExchangeServerAuthException : LocalizedException
	{
		// Token: 0x0600AC2F RID: 44079 RVA: 0x00290055 File Offset: 0x0028E255
		public InvalidFqdnUnderExchangeServerAuthException(string fqdn, string netbios) : base(Strings.InvalidFqdnUnderExchangeServerAuth(fqdn, netbios))
		{
			this.fqdn = fqdn;
			this.netbios = netbios;
		}

		// Token: 0x0600AC30 RID: 44080 RVA: 0x00290072 File Offset: 0x0028E272
		public InvalidFqdnUnderExchangeServerAuthException(string fqdn, string netbios, Exception innerException) : base(Strings.InvalidFqdnUnderExchangeServerAuth(fqdn, netbios), innerException)
		{
			this.fqdn = fqdn;
			this.netbios = netbios;
		}

		// Token: 0x0600AC31 RID: 44081 RVA: 0x00290090 File Offset: 0x0028E290
		protected InvalidFqdnUnderExchangeServerAuthException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fqdn = (string)info.GetValue("fqdn", typeof(string));
			this.netbios = (string)info.GetValue("netbios", typeof(string));
		}

		// Token: 0x0600AC32 RID: 44082 RVA: 0x002900E5 File Offset: 0x0028E2E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fqdn", this.fqdn);
			info.AddValue("netbios", this.netbios);
		}

		// Token: 0x1700377C RID: 14204
		// (get) Token: 0x0600AC33 RID: 44083 RVA: 0x00290111 File Offset: 0x0028E311
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x1700377D RID: 14205
		// (get) Token: 0x0600AC34 RID: 44084 RVA: 0x00290119 File Offset: 0x0028E319
		public string Netbios
		{
			get
			{
				return this.netbios;
			}
		}

		// Token: 0x040060E2 RID: 24802
		private readonly string fqdn;

		// Token: 0x040060E3 RID: 24803
		private readonly string netbios;
	}
}
