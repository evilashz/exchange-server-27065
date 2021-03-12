using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200021D RID: 541
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class GrammarFetcherException : LocalizedException
	{
		// Token: 0x06001148 RID: 4424 RVA: 0x00039DF4 File Offset: 0x00037FF4
		public GrammarFetcherException(string msg) : base(Strings.GrammarFetcherException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00039E09 File Offset: 0x00038009
		public GrammarFetcherException(string msg, Exception innerException) : base(Strings.GrammarFetcherException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00039E1F File Offset: 0x0003801F
		protected GrammarFetcherException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00039E49 File Offset: 0x00038049
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x00039E64 File Offset: 0x00038064
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000892 RID: 2194
		private readonly string msg;
	}
}
