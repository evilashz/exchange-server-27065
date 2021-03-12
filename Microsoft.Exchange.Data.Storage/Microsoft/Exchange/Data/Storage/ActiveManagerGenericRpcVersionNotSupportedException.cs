using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000EA RID: 234
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ActiveManagerGenericRpcVersionNotSupportedException : AmServerException
	{
		// Token: 0x06001321 RID: 4897 RVA: 0x00068D74 File Offset: 0x00066F74
		public ActiveManagerGenericRpcVersionNotSupportedException(int requestServerVersion, int requestCommandId, int requestCommandMajorVersion, int requestCommandMinorVersion, int actualServerVersion, int actualMajorVersion, int actualMinorVersion) : base(ServerStrings.ActiveManagerGenericRpcVersionNotSupported(requestServerVersion, requestCommandId, requestCommandMajorVersion, requestCommandMinorVersion, actualServerVersion, actualMajorVersion, actualMinorVersion))
		{
			this.requestServerVersion = requestServerVersion;
			this.requestCommandId = requestCommandId;
			this.requestCommandMajorVersion = requestCommandMajorVersion;
			this.requestCommandMinorVersion = requestCommandMinorVersion;
			this.actualServerVersion = actualServerVersion;
			this.actualMajorVersion = actualMajorVersion;
			this.actualMinorVersion = actualMinorVersion;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00068DD4 File Offset: 0x00066FD4
		public ActiveManagerGenericRpcVersionNotSupportedException(int requestServerVersion, int requestCommandId, int requestCommandMajorVersion, int requestCommandMinorVersion, int actualServerVersion, int actualMajorVersion, int actualMinorVersion, Exception innerException) : base(ServerStrings.ActiveManagerGenericRpcVersionNotSupported(requestServerVersion, requestCommandId, requestCommandMajorVersion, requestCommandMinorVersion, actualServerVersion, actualMajorVersion, actualMinorVersion), innerException)
		{
			this.requestServerVersion = requestServerVersion;
			this.requestCommandId = requestCommandId;
			this.requestCommandMajorVersion = requestCommandMajorVersion;
			this.requestCommandMinorVersion = requestCommandMinorVersion;
			this.actualServerVersion = actualServerVersion;
			this.actualMajorVersion = actualMajorVersion;
			this.actualMinorVersion = actualMinorVersion;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00068E34 File Offset: 0x00067034
		protected ActiveManagerGenericRpcVersionNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.requestServerVersion = (int)info.GetValue("requestServerVersion", typeof(int));
			this.requestCommandId = (int)info.GetValue("requestCommandId", typeof(int));
			this.requestCommandMajorVersion = (int)info.GetValue("requestCommandMajorVersion", typeof(int));
			this.requestCommandMinorVersion = (int)info.GetValue("requestCommandMinorVersion", typeof(int));
			this.actualServerVersion = (int)info.GetValue("actualServerVersion", typeof(int));
			this.actualMajorVersion = (int)info.GetValue("actualMajorVersion", typeof(int));
			this.actualMinorVersion = (int)info.GetValue("actualMinorVersion", typeof(int));
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00068F2C File Offset: 0x0006712C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("requestServerVersion", this.requestServerVersion);
			info.AddValue("requestCommandId", this.requestCommandId);
			info.AddValue("requestCommandMajorVersion", this.requestCommandMajorVersion);
			info.AddValue("requestCommandMinorVersion", this.requestCommandMinorVersion);
			info.AddValue("actualServerVersion", this.actualServerVersion);
			info.AddValue("actualMajorVersion", this.actualMajorVersion);
			info.AddValue("actualMinorVersion", this.actualMinorVersion);
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x00068FB8 File Offset: 0x000671B8
		public int RequestServerVersion
		{
			get
			{
				return this.requestServerVersion;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00068FC0 File Offset: 0x000671C0
		public int RequestCommandId
		{
			get
			{
				return this.requestCommandId;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x00068FC8 File Offset: 0x000671C8
		public int RequestCommandMajorVersion
		{
			get
			{
				return this.requestCommandMajorVersion;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x00068FD0 File Offset: 0x000671D0
		public int RequestCommandMinorVersion
		{
			get
			{
				return this.requestCommandMinorVersion;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x00068FD8 File Offset: 0x000671D8
		public int ActualServerVersion
		{
			get
			{
				return this.actualServerVersion;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x00068FE0 File Offset: 0x000671E0
		public int ActualMajorVersion
		{
			get
			{
				return this.actualMajorVersion;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x00068FE8 File Offset: 0x000671E8
		public int ActualMinorVersion
		{
			get
			{
				return this.actualMinorVersion;
			}
		}

		// Token: 0x04000980 RID: 2432
		private readonly int requestServerVersion;

		// Token: 0x04000981 RID: 2433
		private readonly int requestCommandId;

		// Token: 0x04000982 RID: 2434
		private readonly int requestCommandMajorVersion;

		// Token: 0x04000983 RID: 2435
		private readonly int requestCommandMinorVersion;

		// Token: 0x04000984 RID: 2436
		private readonly int actualServerVersion;

		// Token: 0x04000985 RID: 2437
		private readonly int actualMajorVersion;

		// Token: 0x04000986 RID: 2438
		private readonly int actualMinorVersion;
	}
}
