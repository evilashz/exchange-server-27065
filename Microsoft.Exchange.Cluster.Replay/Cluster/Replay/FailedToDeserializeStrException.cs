using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000522 RID: 1314
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToDeserializeStrException : DumpsterRedeliveryException
	{
		// Token: 0x06002FC5 RID: 12229 RVA: 0x000C6311 File Offset: 0x000C4511
		public FailedToDeserializeStrException(string stringToDeserialize, string typeName) : base(ReplayStrings.FailedToDeserializeStr(stringToDeserialize, typeName))
		{
			this.stringToDeserialize = stringToDeserialize;
			this.typeName = typeName;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000C6333 File Offset: 0x000C4533
		public FailedToDeserializeStrException(string stringToDeserialize, string typeName, Exception innerException) : base(ReplayStrings.FailedToDeserializeStr(stringToDeserialize, typeName), innerException)
		{
			this.stringToDeserialize = stringToDeserialize;
			this.typeName = typeName;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000C6358 File Offset: 0x000C4558
		protected FailedToDeserializeStrException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.stringToDeserialize = (string)info.GetValue("stringToDeserialize", typeof(string));
			this.typeName = (string)info.GetValue("typeName", typeof(string));
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000C63AD File Offset: 0x000C45AD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("stringToDeserialize", this.stringToDeserialize);
			info.AddValue("typeName", this.typeName);
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000C63D9 File Offset: 0x000C45D9
		public string StringToDeserialize
		{
			get
			{
				return this.stringToDeserialize;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x000C63E1 File Offset: 0x000C45E1
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x040015E4 RID: 5604
		private readonly string stringToDeserialize;

		// Token: 0x040015E5 RID: 5605
		private readonly string typeName;
	}
}
