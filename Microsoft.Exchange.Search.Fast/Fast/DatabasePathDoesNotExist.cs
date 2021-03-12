using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000037 RID: 55
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DatabasePathDoesNotExist : ComponentFailedPermanentException
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x0000F3BE File Offset: 0x0000D5BE
		public DatabasePathDoesNotExist(string databasePath) : base(Strings.DatabasePathDoesNotExist(databasePath))
		{
			this.databasePath = databasePath;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000F3D3 File Offset: 0x0000D5D3
		public DatabasePathDoesNotExist(string databasePath, Exception innerException) : base(Strings.DatabasePathDoesNotExist(databasePath), innerException)
		{
			this.databasePath = databasePath;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000F3E9 File Offset: 0x0000D5E9
		protected DatabasePathDoesNotExist(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databasePath = (string)info.GetValue("databasePath", typeof(string));
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000F413 File Offset: 0x0000D613
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databasePath", this.databasePath);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000F42E File Offset: 0x0000D62E
		public string DatabasePath
		{
			get
			{
				return this.databasePath;
			}
		}

		// Token: 0x04000147 RID: 327
		private readonly string databasePath;
	}
}
