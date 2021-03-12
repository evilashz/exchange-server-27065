using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AC1 RID: 2753
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidEndpointAddressException : LocalizedException
	{
		// Token: 0x0600807E RID: 32894 RVA: 0x001A543E File Offset: 0x001A363E
		public InvalidEndpointAddressException(string exceptionType, string wcfEndpoint) : base(DirectoryStrings.InvalidEndpointAddressErrorMessage(exceptionType, wcfEndpoint))
		{
			this.exceptionType = exceptionType;
			this.wcfEndpoint = wcfEndpoint;
		}

		// Token: 0x0600807F RID: 32895 RVA: 0x001A545B File Offset: 0x001A365B
		public InvalidEndpointAddressException(string exceptionType, string wcfEndpoint, Exception innerException) : base(DirectoryStrings.InvalidEndpointAddressErrorMessage(exceptionType, wcfEndpoint), innerException)
		{
			this.exceptionType = exceptionType;
			this.wcfEndpoint = wcfEndpoint;
		}

		// Token: 0x06008080 RID: 32896 RVA: 0x001A547C File Offset: 0x001A367C
		protected InvalidEndpointAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exceptionType = (string)info.GetValue("exceptionType", typeof(string));
			this.wcfEndpoint = (string)info.GetValue("wcfEndpoint", typeof(string));
		}

		// Token: 0x06008081 RID: 32897 RVA: 0x001A54D1 File Offset: 0x001A36D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exceptionType", this.exceptionType);
			info.AddValue("wcfEndpoint", this.wcfEndpoint);
		}

		// Token: 0x17002ED1 RID: 11985
		// (get) Token: 0x06008082 RID: 32898 RVA: 0x001A54FD File Offset: 0x001A36FD
		public string ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
		}

		// Token: 0x17002ED2 RID: 11986
		// (get) Token: 0x06008083 RID: 32899 RVA: 0x001A5505 File Offset: 0x001A3705
		public string WcfEndpoint
		{
			get
			{
				return this.wcfEndpoint;
			}
		}

		// Token: 0x040055AB RID: 21931
		private readonly string exceptionType;

		// Token: 0x040055AC RID: 21932
		private readonly string wcfEndpoint;
	}
}
