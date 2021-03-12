using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003C6 RID: 966
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckEDBMissingException : FileCheckException
	{
		// Token: 0x06002841 RID: 10305 RVA: 0x000B78AE File Offset: 0x000B5AAE
		public FileCheckEDBMissingException(string edbFileName) : base(ReplayStrings.FileCheckEDBMissing(edbFileName))
		{
			this.edbFileName = edbFileName;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000B78C8 File Offset: 0x000B5AC8
		public FileCheckEDBMissingException(string edbFileName, Exception innerException) : base(ReplayStrings.FileCheckEDBMissing(edbFileName), innerException)
		{
			this.edbFileName = edbFileName;
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000B78E3 File Offset: 0x000B5AE3
		protected FileCheckEDBMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.edbFileName = (string)info.GetValue("edbFileName", typeof(string));
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000B790D File Offset: 0x000B5B0D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("edbFileName", this.edbFileName);
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x000B7928 File Offset: 0x000B5B28
		public string EdbFileName
		{
			get
			{
				return this.edbFileName;
			}
		}

		// Token: 0x040013D0 RID: 5072
		private readonly string edbFileName;
	}
}
