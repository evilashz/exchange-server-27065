using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200015B RID: 347
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class BindingCountExceedsLimitException : LocalizedException
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x00035734 File Offset: 0x00033934
		public BindingCountExceedsLimitException(string workLoad, int limit) : base(Strings.BindingCountExceedsLimit(workLoad, limit))
		{
			this.workLoad = workLoad;
			this.limit = limit;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00035751 File Offset: 0x00033951
		public BindingCountExceedsLimitException(string workLoad, int limit, Exception innerException) : base(Strings.BindingCountExceedsLimit(workLoad, limit), innerException)
		{
			this.workLoad = workLoad;
			this.limit = limit;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00035770 File Offset: 0x00033970
		protected BindingCountExceedsLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.workLoad = (string)info.GetValue("workLoad", typeof(string));
			this.limit = (int)info.GetValue("limit", typeof(int));
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x000357C5 File Offset: 0x000339C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("workLoad", this.workLoad);
			info.AddValue("limit", this.limit);
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x000357F1 File Offset: 0x000339F1
		public string WorkLoad
		{
			get
			{
				return this.workLoad;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x000357F9 File Offset: 0x000339F9
		public int Limit
		{
			get
			{
				return this.limit;
			}
		}

		// Token: 0x0400066B RID: 1643
		private readonly string workLoad;

		// Token: 0x0400066C RID: 1644
		private readonly int limit;
	}
}
