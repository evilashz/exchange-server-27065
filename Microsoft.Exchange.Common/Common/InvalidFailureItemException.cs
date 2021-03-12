using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000024 RID: 36
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidFailureItemException : LocalizedException
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00004105 File Offset: 0x00002305
		public InvalidFailureItemException(string param) : base(CommonStrings.InvalidFailureItemException(param))
		{
			this.param = param;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000411A File Offset: 0x0000231A
		public InvalidFailureItemException(string param, Exception innerException) : base(CommonStrings.InvalidFailureItemException(param), innerException)
		{
			this.param = param;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004130 File Offset: 0x00002330
		protected InvalidFailureItemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.param = (string)info.GetValue("param", typeof(string));
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000415A File Offset: 0x0000235A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("param", this.param);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004175 File Offset: 0x00002375
		public string Param
		{
			get
			{
				return this.param;
			}
		}

		// Token: 0x0400007E RID: 126
		private readonly string param;
	}
}
