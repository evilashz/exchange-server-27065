using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011BE RID: 4542
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidIPGatewayStateOperationException : LocalizedException
	{
		// Token: 0x0600B8A0 RID: 47264 RVA: 0x002A4DD9 File Offset: 0x002A2FD9
		public InvalidIPGatewayStateOperationException(string s) : base(Strings.InvalidIPGatewayStateOperationException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B8A1 RID: 47265 RVA: 0x002A4DEE File Offset: 0x002A2FEE
		public InvalidIPGatewayStateOperationException(string s, Exception innerException) : base(Strings.InvalidIPGatewayStateOperationException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B8A2 RID: 47266 RVA: 0x002A4E04 File Offset: 0x002A3004
		protected InvalidIPGatewayStateOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B8A3 RID: 47267 RVA: 0x002A4E2E File Offset: 0x002A302E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A25 RID: 14885
		// (get) Token: 0x0600B8A4 RID: 47268 RVA: 0x002A4E49 File Offset: 0x002A3049
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006440 RID: 25664
		private readonly string s;
	}
}
