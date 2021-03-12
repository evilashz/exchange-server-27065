using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000022 RID: 34
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExClusTransientException : TransientException
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00004015 File Offset: 0x00002215
		public ExClusTransientException(string funName) : base(CommonStrings.ExClusTransientException(funName))
		{
			this.funName = funName;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000402A File Offset: 0x0000222A
		public ExClusTransientException(string funName, Exception innerException) : base(CommonStrings.ExClusTransientException(funName), innerException)
		{
			this.funName = funName;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004040 File Offset: 0x00002240
		protected ExClusTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.funName = (string)info.GetValue("funName", typeof(string));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000406A File Offset: 0x0000226A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("funName", this.funName);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004085 File Offset: 0x00002285
		public string FunName
		{
			get
			{
				return this.funName;
			}
		}

		// Token: 0x0400007C RID: 124
		private readonly string funName;
	}
}
