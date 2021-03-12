using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011C8 RID: 4552
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DialPlanAssociatedWithIPGatewayException : LocalizedException
	{
		// Token: 0x0600B8D0 RID: 47312 RVA: 0x002A51F7 File Offset: 0x002A33F7
		public DialPlanAssociatedWithIPGatewayException(string s) : base(Strings.DialPlanAssociatedWithIPGatewayException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B8D1 RID: 47313 RVA: 0x002A520C File Offset: 0x002A340C
		public DialPlanAssociatedWithIPGatewayException(string s, Exception innerException) : base(Strings.DialPlanAssociatedWithIPGatewayException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B8D2 RID: 47314 RVA: 0x002A5222 File Offset: 0x002A3422
		protected DialPlanAssociatedWithIPGatewayException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B8D3 RID: 47315 RVA: 0x002A524C File Offset: 0x002A344C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A2D RID: 14893
		// (get) Token: 0x0600B8D4 RID: 47316 RVA: 0x002A5267 File Offset: 0x002A3467
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006448 RID: 25672
		private readonly string s;
	}
}
