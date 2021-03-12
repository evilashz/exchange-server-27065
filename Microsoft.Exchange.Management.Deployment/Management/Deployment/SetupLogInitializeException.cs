using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200007D RID: 125
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SetupLogInitializeException : LocalizedException
	{
		// Token: 0x06000BBD RID: 3005 RVA: 0x00029ADD File Offset: 0x00027CDD
		public SetupLogInitializeException(string msg) : base(Strings.SetupLogInitializeFailure(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00029AF2 File Offset: 0x00027CF2
		public SetupLogInitializeException(string msg, Exception innerException) : base(Strings.SetupLogInitializeFailure(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00029B08 File Offset: 0x00027D08
		protected SetupLogInitializeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00029B32 File Offset: 0x00027D32
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00029B4D File Offset: 0x00027D4D
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x0400070C RID: 1804
		private readonly string msg;
	}
}
