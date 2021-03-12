using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AB4 RID: 2740
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidCookieServiceInstanceIdException : LocalizedException
	{
		// Token: 0x06008044 RID: 32836 RVA: 0x001A5045 File Offset: 0x001A3245
		public InvalidCookieServiceInstanceIdException(string serviceInstanceId) : base(DirectoryStrings.InvalidCookieServiceInstanceIdException(serviceInstanceId))
		{
			this.serviceInstanceId = serviceInstanceId;
		}

		// Token: 0x06008045 RID: 32837 RVA: 0x001A505A File Offset: 0x001A325A
		public InvalidCookieServiceInstanceIdException(string serviceInstanceId, Exception innerException) : base(DirectoryStrings.InvalidCookieServiceInstanceIdException(serviceInstanceId), innerException)
		{
			this.serviceInstanceId = serviceInstanceId;
		}

		// Token: 0x06008046 RID: 32838 RVA: 0x001A5070 File Offset: 0x001A3270
		protected InvalidCookieServiceInstanceIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceInstanceId = (string)info.GetValue("serviceInstanceId", typeof(string));
		}

		// Token: 0x06008047 RID: 32839 RVA: 0x001A509A File Offset: 0x001A329A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceInstanceId", this.serviceInstanceId);
		}

		// Token: 0x17002ECB RID: 11979
		// (get) Token: 0x06008048 RID: 32840 RVA: 0x001A50B5 File Offset: 0x001A32B5
		public string ServiceInstanceId
		{
			get
			{
				return this.serviceInstanceId;
			}
		}

		// Token: 0x040055A5 RID: 21925
		private readonly string serviceInstanceId;
	}
}
