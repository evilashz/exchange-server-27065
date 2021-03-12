using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ThirdPartyReplicationException : LocalizedException
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002F91 File Offset: 0x00001191
		public ThirdPartyReplicationException(string error) : base(ThirdPartyReplication.TPRBaseError(error))
		{
			this.error = error;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002FA6 File Offset: 0x000011A6
		public ThirdPartyReplicationException(string error, Exception innerException) : base(ThirdPartyReplication.TPRBaseError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002FBC File Offset: 0x000011BC
		protected ThirdPartyReplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002FE6 File Offset: 0x000011E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003001 File Offset: 0x00001201
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400001E RID: 30
		private readonly string error;
	}
}
