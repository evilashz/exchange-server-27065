using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200012B RID: 299
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RepairingIsNotSetSinceMonitorEntryIsNotFoundException : ActiveMonitoringServerTransientException
	{
		// Token: 0x0600146E RID: 5230 RVA: 0x0006ADF6 File Offset: 0x00068FF6
		public RepairingIsNotSetSinceMonitorEntryIsNotFoundException(string monitorName, string targetResource) : base(ServerStrings.RepairingIsNotSetSinceMonitorEntryIsNotFound(monitorName, targetResource))
		{
			this.monitorName = monitorName;
			this.targetResource = targetResource;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0006AE18 File Offset: 0x00069018
		public RepairingIsNotSetSinceMonitorEntryIsNotFoundException(string monitorName, string targetResource, Exception innerException) : base(ServerStrings.RepairingIsNotSetSinceMonitorEntryIsNotFound(monitorName, targetResource), innerException)
		{
			this.monitorName = monitorName;
			this.targetResource = targetResource;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0006AE3C File Offset: 0x0006903C
		protected RepairingIsNotSetSinceMonitorEntryIsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.monitorName = (string)info.GetValue("monitorName", typeof(string));
			this.targetResource = (string)info.GetValue("targetResource", typeof(string));
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0006AE91 File Offset: 0x00069091
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("monitorName", this.monitorName);
			info.AddValue("targetResource", this.targetResource);
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0006AEBD File Offset: 0x000690BD
		public string MonitorName
		{
			get
			{
				return this.monitorName;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0006AEC5 File Offset: 0x000690C5
		public string TargetResource
		{
			get
			{
				return this.targetResource;
			}
		}

		// Token: 0x040009C0 RID: 2496
		private readonly string monitorName;

		// Token: 0x040009C1 RID: 2497
		private readonly string targetResource;
	}
}
