using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001023 RID: 4131
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidIPRangeFormatException : LocalizedException
	{
		// Token: 0x0600AF69 RID: 44905 RVA: 0x0029455F File Offset: 0x0029275F
		public InvalidIPRangeFormatException(string ipRange) : base(Strings.InvalidIPRangeFormatId(ipRange))
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF6A RID: 44906 RVA: 0x00294574 File Offset: 0x00292774
		public InvalidIPRangeFormatException(string ipRange, Exception innerException) : base(Strings.InvalidIPRangeFormatId(ipRange), innerException)
		{
			this.ipRange = ipRange;
		}

		// Token: 0x0600AF6B RID: 44907 RVA: 0x0029458A File Offset: 0x0029278A
		protected InvalidIPRangeFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipRange = (string)info.GetValue("ipRange", typeof(string));
		}

		// Token: 0x0600AF6C RID: 44908 RVA: 0x002945B4 File Offset: 0x002927B4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipRange", this.ipRange);
		}

		// Token: 0x170037FA RID: 14330
		// (get) Token: 0x0600AF6D RID: 44909 RVA: 0x002945CF File Offset: 0x002927CF
		public string IpRange
		{
			get
			{
				return this.ipRange;
			}
		}

		// Token: 0x04006160 RID: 24928
		private readonly string ipRange;
	}
}
