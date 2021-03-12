using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000521 RID: 1313
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToDeserializeDumpsterRequestStrException : DumpsterRedeliveryException
	{
		// Token: 0x06002FBD RID: 12221 RVA: 0x000C6195 File Offset: 0x000C4395
		public FailedToDeserializeDumpsterRequestStrException(string dbName, string stringToDeserialize, string typeName, string serializationError) : base(ReplayStrings.FailedToDeserializeDumpsterRequestStrException(dbName, stringToDeserialize, typeName, serializationError))
		{
			this.dbName = dbName;
			this.stringToDeserialize = stringToDeserialize;
			this.typeName = typeName;
			this.serializationError = serializationError;
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000C61C9 File Offset: 0x000C43C9
		public FailedToDeserializeDumpsterRequestStrException(string dbName, string stringToDeserialize, string typeName, string serializationError, Exception innerException) : base(ReplayStrings.FailedToDeserializeDumpsterRequestStrException(dbName, stringToDeserialize, typeName, serializationError), innerException)
		{
			this.dbName = dbName;
			this.stringToDeserialize = stringToDeserialize;
			this.typeName = typeName;
			this.serializationError = serializationError;
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000C6200 File Offset: 0x000C4400
		protected FailedToDeserializeDumpsterRequestStrException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.stringToDeserialize = (string)info.GetValue("stringToDeserialize", typeof(string));
			this.typeName = (string)info.GetValue("typeName", typeof(string));
			this.serializationError = (string)info.GetValue("serializationError", typeof(string));
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000C6298 File Offset: 0x000C4498
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("stringToDeserialize", this.stringToDeserialize);
			info.AddValue("typeName", this.typeName);
			info.AddValue("serializationError", this.serializationError);
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000C62F1 File Offset: 0x000C44F1
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x000C62F9 File Offset: 0x000C44F9
		public string StringToDeserialize
		{
			get
			{
				return this.stringToDeserialize;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000C6301 File Offset: 0x000C4501
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06002FC4 RID: 12228 RVA: 0x000C6309 File Offset: 0x000C4509
		public string SerializationError
		{
			get
			{
				return this.serializationError;
			}
		}

		// Token: 0x040015E0 RID: 5600
		private readonly string dbName;

		// Token: 0x040015E1 RID: 5601
		private readonly string stringToDeserialize;

		// Token: 0x040015E2 RID: 5602
		private readonly string typeName;

		// Token: 0x040015E3 RID: 5603
		private readonly string serializationError;
	}
}
