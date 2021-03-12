using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000010 RID: 16
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnknownDatabaseException : LocalizedException
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003E50 File Offset: 0x00002050
		public UnknownDatabaseException(string databaseId) : base(Strings.descUnknownDatabase(databaseId))
		{
			this.databaseId = databaseId;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003E65 File Offset: 0x00002065
		public UnknownDatabaseException(string databaseId, Exception innerException) : base(Strings.descUnknownDatabase(databaseId), innerException)
		{
			this.databaseId = databaseId;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003E7B File Offset: 0x0000207B
		protected UnknownDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseId = (string)info.GetValue("databaseId", typeof(string));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003EA5 File Offset: 0x000020A5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseId", this.databaseId);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003EC0 File Offset: 0x000020C0
		public string DatabaseId
		{
			get
			{
				return this.databaseId;
			}
		}

		// Token: 0x0400009E RID: 158
		private readonly string databaseId;
	}
}
