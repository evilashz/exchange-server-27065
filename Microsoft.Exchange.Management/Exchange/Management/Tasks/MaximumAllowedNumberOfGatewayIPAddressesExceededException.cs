using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200102C RID: 4140
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MaximumAllowedNumberOfGatewayIPAddressesExceededException : LocalizedException
	{
		// Token: 0x0600AF95 RID: 44949 RVA: 0x0029495B File Offset: 0x00292B5B
		public MaximumAllowedNumberOfGatewayIPAddressesExceededException(int maxAllowed) : base(Strings.MaximumAllowedNumberOfGatewayIPAddressesExceededId(maxAllowed))
		{
			this.maxAllowed = maxAllowed;
		}

		// Token: 0x0600AF96 RID: 44950 RVA: 0x00294970 File Offset: 0x00292B70
		public MaximumAllowedNumberOfGatewayIPAddressesExceededException(int maxAllowed, Exception innerException) : base(Strings.MaximumAllowedNumberOfGatewayIPAddressesExceededId(maxAllowed), innerException)
		{
			this.maxAllowed = maxAllowed;
		}

		// Token: 0x0600AF97 RID: 44951 RVA: 0x00294986 File Offset: 0x00292B86
		protected MaximumAllowedNumberOfGatewayIPAddressesExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.maxAllowed = (int)info.GetValue("maxAllowed", typeof(int));
		}

		// Token: 0x0600AF98 RID: 44952 RVA: 0x002949B0 File Offset: 0x00292BB0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("maxAllowed", this.maxAllowed);
		}

		// Token: 0x17003802 RID: 14338
		// (get) Token: 0x0600AF99 RID: 44953 RVA: 0x002949CB File Offset: 0x00292BCB
		public int MaxAllowed
		{
			get
			{
				return this.maxAllowed;
			}
		}

		// Token: 0x04006168 RID: 24936
		private readonly int maxAllowed;
	}
}
