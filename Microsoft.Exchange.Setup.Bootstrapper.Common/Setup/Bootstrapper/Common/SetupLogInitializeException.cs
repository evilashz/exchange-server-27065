using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x02000013 RID: 19
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SetupLogInitializeException : LocalizedException
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00004E7D File Offset: 0x0000307D
		public SetupLogInitializeException(string msg) : base(Strings.SetupLogInitializeFailure(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004E92 File Offset: 0x00003092
		public SetupLogInitializeException(string msg, Exception innerException) : base(Strings.SetupLogInitializeFailure(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004EA8 File Offset: 0x000030A8
		protected SetupLogInitializeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004ED2 File Offset: 0x000030D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004EED File Offset: 0x000030ED
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000BC RID: 188
		private readonly string msg;
	}
}
