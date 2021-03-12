using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F83 RID: 3971
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReadNetworkAdapterInfoException : LocalizedException
	{
		// Token: 0x0600AC6D RID: 44141 RVA: 0x002903B3 File Offset: 0x0028E5B3
		public ReadNetworkAdapterInfoException(string exception) : base(Strings.ReadNetworkAdapterInfo(exception))
		{
			this.exception = exception;
		}

		// Token: 0x0600AC6E RID: 44142 RVA: 0x002903C8 File Offset: 0x0028E5C8
		public ReadNetworkAdapterInfoException(string exception, Exception innerException) : base(Strings.ReadNetworkAdapterInfo(exception), innerException)
		{
			this.exception = exception;
		}

		// Token: 0x0600AC6F RID: 44143 RVA: 0x002903DE File Offset: 0x0028E5DE
		protected ReadNetworkAdapterInfoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exception = (string)info.GetValue("exception", typeof(string));
		}

		// Token: 0x0600AC70 RID: 44144 RVA: 0x00290408 File Offset: 0x0028E608
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exception", this.exception);
		}

		// Token: 0x1700377E RID: 14206
		// (get) Token: 0x0600AC71 RID: 44145 RVA: 0x00290423 File Offset: 0x0028E623
		public string Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040060E4 RID: 24804
		private readonly string exception;
	}
}
