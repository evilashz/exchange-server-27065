using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003C1 RID: 961
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckDatabaseLogfileSignatureException : FileCheckException
	{
		// Token: 0x06002822 RID: 10274 RVA: 0x000B744A File Offset: 0x000B564A
		public FileCheckDatabaseLogfileSignatureException(string database, string logfileSignature, string expectedSignature) : base(ReplayStrings.FileCheckDatabaseLogfileSignature(database, logfileSignature, expectedSignature))
		{
			this.database = database;
			this.logfileSignature = logfileSignature;
			this.expectedSignature = expectedSignature;
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000B7474 File Offset: 0x000B5674
		public FileCheckDatabaseLogfileSignatureException(string database, string logfileSignature, string expectedSignature, Exception innerException) : base(ReplayStrings.FileCheckDatabaseLogfileSignature(database, logfileSignature, expectedSignature), innerException)
		{
			this.database = database;
			this.logfileSignature = logfileSignature;
			this.expectedSignature = expectedSignature;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000B74A0 File Offset: 0x000B56A0
		protected FileCheckDatabaseLogfileSignatureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.logfileSignature = (string)info.GetValue("logfileSignature", typeof(string));
			this.expectedSignature = (string)info.GetValue("expectedSignature", typeof(string));
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000B7515 File Offset: 0x000B5715
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("logfileSignature", this.logfileSignature);
			info.AddValue("expectedSignature", this.expectedSignature);
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x000B7552 File Offset: 0x000B5752
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x000B755A File Offset: 0x000B575A
		public string LogfileSignature
		{
			get
			{
				return this.logfileSignature;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x000B7562 File Offset: 0x000B5762
		public string ExpectedSignature
		{
			get
			{
				return this.expectedSignature;
			}
		}

		// Token: 0x040013C5 RID: 5061
		private readonly string database;

		// Token: 0x040013C6 RID: 5062
		private readonly string logfileSignature;

		// Token: 0x040013C7 RID: 5063
		private readonly string expectedSignature;
	}
}
