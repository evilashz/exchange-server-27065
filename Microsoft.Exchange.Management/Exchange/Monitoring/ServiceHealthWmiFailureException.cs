using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F02 RID: 3842
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceHealthWmiFailureException : LocalizedException
	{
		// Token: 0x0600A9F3 RID: 43507 RVA: 0x0028C90A File Offset: 0x0028AB0A
		public ServiceHealthWmiFailureException(string errorMsg) : base(Strings.ServiceHealthWmiFailure(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A9F4 RID: 43508 RVA: 0x0028C91F File Offset: 0x0028AB1F
		public ServiceHealthWmiFailureException(string errorMsg, Exception innerException) : base(Strings.ServiceHealthWmiFailure(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A9F5 RID: 43509 RVA: 0x0028C935 File Offset: 0x0028AB35
		protected ServiceHealthWmiFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x0600A9F6 RID: 43510 RVA: 0x0028C95F File Offset: 0x0028AB5F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17003708 RID: 14088
		// (get) Token: 0x0600A9F7 RID: 43511 RVA: 0x0028C97A File Offset: 0x0028AB7A
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x0400606E RID: 24686
		private readonly string errorMsg;
	}
}
