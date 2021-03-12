using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000128 RID: 296
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ActiveMonitoringRpcVersionNotSupportedException : ActiveMonitoringServerException
	{
		// Token: 0x06001457 RID: 5207 RVA: 0x0006AA04 File Offset: 0x00068C04
		public ActiveMonitoringRpcVersionNotSupportedException(int requestServerVersion, int requestCommandId, int requestCommandMajorVersion, int requestCommandMinorVersion, int actualServerVersion, int actualMajorVersion, int actualMinorVersion) : base(ServerStrings.ActiveMonitoringRpcVersionNotSupported(requestServerVersion, requestCommandId, requestCommandMajorVersion, requestCommandMinorVersion, actualServerVersion, actualMajorVersion, actualMinorVersion))
		{
			this.requestServerVersion = requestServerVersion;
			this.requestCommandId = requestCommandId;
			this.requestCommandMajorVersion = requestCommandMajorVersion;
			this.requestCommandMinorVersion = requestCommandMinorVersion;
			this.actualServerVersion = actualServerVersion;
			this.actualMajorVersion = actualMajorVersion;
			this.actualMinorVersion = actualMinorVersion;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0006AA64 File Offset: 0x00068C64
		public ActiveMonitoringRpcVersionNotSupportedException(int requestServerVersion, int requestCommandId, int requestCommandMajorVersion, int requestCommandMinorVersion, int actualServerVersion, int actualMajorVersion, int actualMinorVersion, Exception innerException) : base(ServerStrings.ActiveMonitoringRpcVersionNotSupported(requestServerVersion, requestCommandId, requestCommandMajorVersion, requestCommandMinorVersion, actualServerVersion, actualMajorVersion, actualMinorVersion), innerException)
		{
			this.requestServerVersion = requestServerVersion;
			this.requestCommandId = requestCommandId;
			this.requestCommandMajorVersion = requestCommandMajorVersion;
			this.requestCommandMinorVersion = requestCommandMinorVersion;
			this.actualServerVersion = actualServerVersion;
			this.actualMajorVersion = actualMajorVersion;
			this.actualMinorVersion = actualMinorVersion;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0006AAC4 File Offset: 0x00068CC4
		protected ActiveMonitoringRpcVersionNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.requestServerVersion = (int)info.GetValue("requestServerVersion", typeof(int));
			this.requestCommandId = (int)info.GetValue("requestCommandId", typeof(int));
			this.requestCommandMajorVersion = (int)info.GetValue("requestCommandMajorVersion", typeof(int));
			this.requestCommandMinorVersion = (int)info.GetValue("requestCommandMinorVersion", typeof(int));
			this.actualServerVersion = (int)info.GetValue("actualServerVersion", typeof(int));
			this.actualMajorVersion = (int)info.GetValue("actualMajorVersion", typeof(int));
			this.actualMinorVersion = (int)info.GetValue("actualMinorVersion", typeof(int));
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0006ABBC File Offset: 0x00068DBC
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

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0006AC48 File Offset: 0x00068E48
		public int RequestServerVersion
		{
			get
			{
				return this.requestServerVersion;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0006AC50 File Offset: 0x00068E50
		public int RequestCommandId
		{
			get
			{
				return this.requestCommandId;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0006AC58 File Offset: 0x00068E58
		public int RequestCommandMajorVersion
		{
			get
			{
				return this.requestCommandMajorVersion;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0006AC60 File Offset: 0x00068E60
		public int RequestCommandMinorVersion
		{
			get
			{
				return this.requestCommandMinorVersion;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0006AC68 File Offset: 0x00068E68
		public int ActualServerVersion
		{
			get
			{
				return this.actualServerVersion;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0006AC70 File Offset: 0x00068E70
		public int ActualMajorVersion
		{
			get
			{
				return this.actualMajorVersion;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0006AC78 File Offset: 0x00068E78
		public int ActualMinorVersion
		{
			get
			{
				return this.actualMinorVersion;
			}
		}

		// Token: 0x040009B6 RID: 2486
		private readonly int requestServerVersion;

		// Token: 0x040009B7 RID: 2487
		private readonly int requestCommandId;

		// Token: 0x040009B8 RID: 2488
		private readonly int requestCommandMajorVersion;

		// Token: 0x040009B9 RID: 2489
		private readonly int requestCommandMinorVersion;

		// Token: 0x040009BA RID: 2490
		private readonly int actualServerVersion;

		// Token: 0x040009BB RID: 2491
		private readonly int actualMajorVersion;

		// Token: 0x040009BC RID: 2492
		private readonly int actualMinorVersion;
	}
}
