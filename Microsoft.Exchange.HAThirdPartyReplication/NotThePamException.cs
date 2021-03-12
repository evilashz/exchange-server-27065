using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000016 RID: 22
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotThePamException : ThirdPartyReplicationException
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00003009 File Offset: 0x00001209
		public NotThePamException(string apiName) : base(ThirdPartyReplication.OnlyPAMError(apiName))
		{
			this.apiName = apiName;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003023 File Offset: 0x00001223
		public NotThePamException(string apiName, Exception innerException) : base(ThirdPartyReplication.OnlyPAMError(apiName), innerException)
		{
			this.apiName = apiName;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000303E File Offset: 0x0000123E
		protected NotThePamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.apiName = (string)info.GetValue("apiName", typeof(string));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003068 File Offset: 0x00001268
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("apiName", this.apiName);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003083 File Offset: 0x00001283
		public string ApiName
		{
			get
			{
				return this.apiName;
			}
		}

		// Token: 0x0400001F RID: 31
		private readonly string apiName;
	}
}
