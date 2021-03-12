using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02001200 RID: 4608
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToCreateGatewayObjectException : LocalizedException
	{
		// Token: 0x0600B9D6 RID: 47574 RVA: 0x002A6763 File Offset: 0x002A4963
		public UnableToCreateGatewayObjectException(string msg) : base(Strings.UnableToCreateGatewayObjectException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x0600B9D7 RID: 47575 RVA: 0x002A6778 File Offset: 0x002A4978
		public UnableToCreateGatewayObjectException(string msg, Exception innerException) : base(Strings.UnableToCreateGatewayObjectException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x0600B9D8 RID: 47576 RVA: 0x002A678E File Offset: 0x002A498E
		protected UnableToCreateGatewayObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x0600B9D9 RID: 47577 RVA: 0x002A67B8 File Offset: 0x002A49B8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17003A53 RID: 14931
		// (get) Token: 0x0600B9DA RID: 47578 RVA: 0x002A67D3 File Offset: 0x002A49D3
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x0400646E RID: 25710
		private readonly string msg;
	}
}
