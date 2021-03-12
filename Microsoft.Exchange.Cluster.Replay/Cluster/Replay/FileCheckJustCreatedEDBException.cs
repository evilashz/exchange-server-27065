using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003CC RID: 972
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckJustCreatedEDBException : FileCheckException
	{
		// Token: 0x06002862 RID: 10338 RVA: 0x000B7CB9 File Offset: 0x000B5EB9
		public FileCheckJustCreatedEDBException(string file) : base(ReplayStrings.FileCheckJustCreatedEDB(file))
		{
			this.file = file;
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000B7CD3 File Offset: 0x000B5ED3
		public FileCheckJustCreatedEDBException(string file, Exception innerException) : base(ReplayStrings.FileCheckJustCreatedEDB(file), innerException)
		{
			this.file = file;
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000B7CEE File Offset: 0x000B5EEE
		protected FileCheckJustCreatedEDBException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.file = (string)info.GetValue("file", typeof(string));
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000B7D18 File Offset: 0x000B5F18
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("file", this.file);
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002866 RID: 10342 RVA: 0x000B7D33 File Offset: 0x000B5F33
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x040013D9 RID: 5081
		private readonly string file;
	}
}
