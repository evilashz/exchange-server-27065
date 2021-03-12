using System;
using System.DirectoryServices.Protocols;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000057 RID: 87
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADOperationException : DataSourceOperationException
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x000191B2 File Offset: 0x000173B2
		public ADOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000191BB File Offset: 0x000173BB
		public ADOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000191C5 File Offset: 0x000173C5
		protected ADOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000191CF File Offset: 0x000173CF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x170000ED RID: 237
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x000191D9 File Offset: 0x000173D9
		internal PooledLdapConnection Connection
		{
			set
			{
				this.connection = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x000191E4 File Offset: 0x000173E4
		internal DirectoryRequest ADRequest
		{
			set
			{
				this.request = value;
				SearchRequest searchRequest = value as SearchRequest;
				if (searchRequest != null && searchRequest.Filter != null)
				{
					ExWatson.AddExtraData(searchRequest.Filter.ToString());
				}
			}
		}

		// Token: 0x040001A2 RID: 418
		private PooledLdapConnection connection;

		// Token: 0x040001A3 RID: 419
		private DirectoryRequest request;
	}
}
