using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200015A RID: 346
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CompliancePolicyCountExceedsLimitException : LocalizedException
	{
		// Token: 0x06000EC6 RID: 3782 RVA: 0x000356BC File Offset: 0x000338BC
		public CompliancePolicyCountExceedsLimitException(int limit) : base(Strings.CompliancePolicyCountExceedsLimit(limit))
		{
			this.limit = limit;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000356D1 File Offset: 0x000338D1
		public CompliancePolicyCountExceedsLimitException(int limit, Exception innerException) : base(Strings.CompliancePolicyCountExceedsLimit(limit), innerException)
		{
			this.limit = limit;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x000356E7 File Offset: 0x000338E7
		protected CompliancePolicyCountExceedsLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.limit = (int)info.GetValue("limit", typeof(int));
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00035711 File Offset: 0x00033911
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("limit", this.limit);
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0003572C File Offset: 0x0003392C
		public int Limit
		{
			get
			{
				return this.limit;
			}
		}

		// Token: 0x0400066A RID: 1642
		private readonly int limit;
	}
}
