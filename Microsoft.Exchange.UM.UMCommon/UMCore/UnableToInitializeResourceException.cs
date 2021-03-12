using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000204 RID: 516
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnableToInitializeResourceException : LocalizedException
	{
		// Token: 0x060010D3 RID: 4307 RVA: 0x00039468 File Offset: 0x00037668
		public UnableToInitializeResourceException(string reason) : base(Strings.UnableToInitializeResource(reason))
		{
			this.reason = reason;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0003947D File Offset: 0x0003767D
		public UnableToInitializeResourceException(string reason, Exception innerException) : base(Strings.UnableToInitializeResource(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00039493 File Offset: 0x00037693
		protected UnableToInitializeResourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x000394BD File Offset: 0x000376BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x000394D8 File Offset: 0x000376D8
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000881 RID: 2177
		private readonly string reason;
	}
}
