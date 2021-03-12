using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011BC RID: 4540
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPGatewayAlreadEnabledException : LocalizedException
	{
		// Token: 0x0600B896 RID: 47254 RVA: 0x002A4CE9 File Offset: 0x002A2EE9
		public IPGatewayAlreadEnabledException(string s) : base(Strings.IPGatewayAlreadEnabledException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B897 RID: 47255 RVA: 0x002A4CFE File Offset: 0x002A2EFE
		public IPGatewayAlreadEnabledException(string s, Exception innerException) : base(Strings.IPGatewayAlreadEnabledException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B898 RID: 47256 RVA: 0x002A4D14 File Offset: 0x002A2F14
		protected IPGatewayAlreadEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B899 RID: 47257 RVA: 0x002A4D3E File Offset: 0x002A2F3E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A23 RID: 14883
		// (get) Token: 0x0600B89A RID: 47258 RVA: 0x002A4D59 File Offset: 0x002A2F59
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x0400643E RID: 25662
		private readonly string s;
	}
}
