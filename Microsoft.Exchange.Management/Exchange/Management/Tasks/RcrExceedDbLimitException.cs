using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EE6 RID: 3814
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RcrExceedDbLimitException : LocalizedException
	{
		// Token: 0x0600A95B RID: 43355 RVA: 0x0028B7D9 File Offset: 0x002899D9
		public RcrExceedDbLimitException(string server, int limit) : base(Strings.RcrExceedDbLimitException(server, limit))
		{
			this.server = server;
			this.limit = limit;
		}

		// Token: 0x0600A95C RID: 43356 RVA: 0x0028B7F6 File Offset: 0x002899F6
		public RcrExceedDbLimitException(string server, int limit, Exception innerException) : base(Strings.RcrExceedDbLimitException(server, limit), innerException)
		{
			this.server = server;
			this.limit = limit;
		}

		// Token: 0x0600A95D RID: 43357 RVA: 0x0028B814 File Offset: 0x00289A14
		protected RcrExceedDbLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
			this.limit = (int)info.GetValue("limit", typeof(int));
		}

		// Token: 0x0600A95E RID: 43358 RVA: 0x0028B869 File Offset: 0x00289A69
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
			info.AddValue("limit", this.limit);
		}

		// Token: 0x170036E0 RID: 14048
		// (get) Token: 0x0600A95F RID: 43359 RVA: 0x0028B895 File Offset: 0x00289A95
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170036E1 RID: 14049
		// (get) Token: 0x0600A960 RID: 43360 RVA: 0x0028B89D File Offset: 0x00289A9D
		public int Limit
		{
			get
			{
				return this.limit;
			}
		}

		// Token: 0x04006046 RID: 24646
		private readonly string server;

		// Token: 0x04006047 RID: 24647
		private readonly int limit;
	}
}
