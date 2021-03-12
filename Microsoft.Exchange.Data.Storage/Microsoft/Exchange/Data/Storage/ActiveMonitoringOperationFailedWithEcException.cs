using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000125 RID: 293
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ActiveMonitoringOperationFailedWithEcException : ActiveMonitoringServerException
	{
		// Token: 0x06001445 RID: 5189 RVA: 0x0006A788 File Offset: 0x00068988
		public ActiveMonitoringOperationFailedWithEcException(int ec) : base(ServerStrings.ActiveMonitoringOperationFailedWithEcException(ec))
		{
			this.ec = ec;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0006A7A2 File Offset: 0x000689A2
		public ActiveMonitoringOperationFailedWithEcException(int ec, Exception innerException) : base(ServerStrings.ActiveMonitoringOperationFailedWithEcException(ec), innerException)
		{
			this.ec = ec;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0006A7BD File Offset: 0x000689BD
		protected ActiveMonitoringOperationFailedWithEcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ec = (int)info.GetValue("ec", typeof(int));
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0006A7E7 File Offset: 0x000689E7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ec", this.ec);
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0006A802 File Offset: 0x00068A02
		public int Ec
		{
			get
			{
				return this.ec;
			}
		}

		// Token: 0x040009B0 RID: 2480
		private readonly int ec;
	}
}
