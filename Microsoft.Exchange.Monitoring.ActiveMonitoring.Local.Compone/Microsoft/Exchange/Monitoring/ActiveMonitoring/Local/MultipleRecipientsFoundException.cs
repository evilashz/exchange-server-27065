using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x0200059C RID: 1436
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MultipleRecipientsFoundException : LocalizedException
	{
		// Token: 0x060026A9 RID: 9897 RVA: 0x000DDD1D File Offset: 0x000DBF1D
		public MultipleRecipientsFoundException(string queryFilter) : base(Strings.MultipleRecipientsFound(queryFilter))
		{
			this.queryFilter = queryFilter;
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000DDD32 File Offset: 0x000DBF32
		public MultipleRecipientsFoundException(string queryFilter, Exception innerException) : base(Strings.MultipleRecipientsFound(queryFilter), innerException)
		{
			this.queryFilter = queryFilter;
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000DDD48 File Offset: 0x000DBF48
		protected MultipleRecipientsFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.queryFilter = (string)info.GetValue("queryFilter", typeof(string));
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000DDD72 File Offset: 0x000DBF72
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("queryFilter", this.queryFilter);
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x000DDD8D File Offset: 0x000DBF8D
		public string QueryFilter
		{
			get
			{
				return this.queryFilter;
			}
		}

		// Token: 0x04001C75 RID: 7285
		private readonly string queryFilter;
	}
}
