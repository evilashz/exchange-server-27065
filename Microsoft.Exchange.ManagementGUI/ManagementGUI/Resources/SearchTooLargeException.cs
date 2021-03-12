using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ManagementGUI.Resources
{
	// Token: 0x02000016 RID: 22
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SearchTooLargeException : LocalizedException
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x0003708A File Offset: 0x0003528A
		public SearchTooLargeException(int max) : base(Strings.SearchTooLargeError(max))
		{
			this.max = max;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0003709F File Offset: 0x0003529F
		public SearchTooLargeException(int max, Exception innerException) : base(Strings.SearchTooLargeError(max), innerException)
		{
			this.max = max;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x000370B5 File Offset: 0x000352B5
		protected SearchTooLargeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.max = (int)info.GetValue("max", typeof(int));
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000370DF File Offset: 0x000352DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("max", this.max);
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x000370FA File Offset: 0x000352FA
		public int Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x04001072 RID: 4210
		private readonly int max;
	}
}
