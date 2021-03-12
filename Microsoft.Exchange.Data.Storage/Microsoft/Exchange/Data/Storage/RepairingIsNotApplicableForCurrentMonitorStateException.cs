using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200012A RID: 298
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RepairingIsNotApplicableForCurrentMonitorStateException : ActiveMonitoringServerTransientException
	{
		// Token: 0x06001467 RID: 5223 RVA: 0x0006ACD6 File Offset: 0x00068ED6
		public RepairingIsNotApplicableForCurrentMonitorStateException(string monitorName, string targetResource, string alertState) : base(ServerStrings.RepairingIsNotApplicableForCurrentMonitorState(monitorName, targetResource, alertState))
		{
			this.monitorName = monitorName;
			this.targetResource = targetResource;
			this.alertState = alertState;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0006AD00 File Offset: 0x00068F00
		public RepairingIsNotApplicableForCurrentMonitorStateException(string monitorName, string targetResource, string alertState, Exception innerException) : base(ServerStrings.RepairingIsNotApplicableForCurrentMonitorState(monitorName, targetResource, alertState), innerException)
		{
			this.monitorName = monitorName;
			this.targetResource = targetResource;
			this.alertState = alertState;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0006AD2C File Offset: 0x00068F2C
		protected RepairingIsNotApplicableForCurrentMonitorStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.monitorName = (string)info.GetValue("monitorName", typeof(string));
			this.targetResource = (string)info.GetValue("targetResource", typeof(string));
			this.alertState = (string)info.GetValue("alertState", typeof(string));
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0006ADA1 File Offset: 0x00068FA1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("monitorName", this.monitorName);
			info.AddValue("targetResource", this.targetResource);
			info.AddValue("alertState", this.alertState);
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0006ADDE File Offset: 0x00068FDE
		public string MonitorName
		{
			get
			{
				return this.monitorName;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0006ADE6 File Offset: 0x00068FE6
		public string TargetResource
		{
			get
			{
				return this.targetResource;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0006ADEE File Offset: 0x00068FEE
		public string AlertState
		{
			get
			{
				return this.alertState;
			}
		}

		// Token: 0x040009BD RID: 2493
		private readonly string monitorName;

		// Token: 0x040009BE RID: 2494
		private readonly string targetResource;

		// Token: 0x040009BF RID: 2495
		private readonly string alertState;
	}
}
