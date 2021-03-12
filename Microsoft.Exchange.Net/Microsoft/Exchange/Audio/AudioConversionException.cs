using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x020000E1 RID: 225
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AudioConversionException : LocalizedException
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x00015BCF File Offset: 0x00013DCF
		public AudioConversionException(string reason) : base(NetException.AudioConversionFailed(reason))
		{
			this.reason = reason;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00015BE4 File Offset: 0x00013DE4
		public AudioConversionException(string reason, Exception innerException) : base(NetException.AudioConversionFailed(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00015BFA File Offset: 0x00013DFA
		protected AudioConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00015C24 File Offset: 0x00013E24
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00015C3F File Offset: 0x00013E3F
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040004F3 RID: 1267
		private readonly string reason;
	}
}
