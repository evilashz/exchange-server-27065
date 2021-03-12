using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011BD RID: 4541
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPGatewayAlreadDisabledException : LocalizedException
	{
		// Token: 0x0600B89B RID: 47259 RVA: 0x002A4D61 File Offset: 0x002A2F61
		public IPGatewayAlreadDisabledException(string s) : base(Strings.IPGatewayAlreadDisabledException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B89C RID: 47260 RVA: 0x002A4D76 File Offset: 0x002A2F76
		public IPGatewayAlreadDisabledException(string s, Exception innerException) : base(Strings.IPGatewayAlreadDisabledException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B89D RID: 47261 RVA: 0x002A4D8C File Offset: 0x002A2F8C
		protected IPGatewayAlreadDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B89E RID: 47262 RVA: 0x002A4DB6 File Offset: 0x002A2FB6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A24 RID: 14884
		// (get) Token: 0x0600B89F RID: 47263 RVA: 0x002A4DD1 File Offset: 0x002A2FD1
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x0400643F RID: 25663
		private readonly string s;
	}
}
