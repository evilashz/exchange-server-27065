using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x020000DF RID: 223
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidWaveFormatException : LocalizedException
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00015B28 File Offset: 0x00013D28
		public InvalidWaveFormatException(string s) : base(NetException.InvalidWaveFormat(s))
		{
			this.s = s;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00015B3D File Offset: 0x00013D3D
		public InvalidWaveFormatException(string s, Exception innerException) : base(NetException.InvalidWaveFormat(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00015B53 File Offset: 0x00013D53
		protected InvalidWaveFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00015B7D File Offset: 0x00013D7D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00015B98 File Offset: 0x00013D98
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x040004F2 RID: 1266
		private readonly string s;
	}
}
