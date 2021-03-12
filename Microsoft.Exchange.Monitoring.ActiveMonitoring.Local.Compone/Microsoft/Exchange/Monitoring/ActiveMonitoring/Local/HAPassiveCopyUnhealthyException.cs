using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005A1 RID: 1441
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class HAPassiveCopyUnhealthyException : LocalizedException
	{
		// Token: 0x060026BF RID: 9919 RVA: 0x000DDE9A File Offset: 0x000DC09A
		public HAPassiveCopyUnhealthyException(string copyState) : base(Strings.HAPassiveCopyUnhealthy(copyState))
		{
			this.copyState = copyState;
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x000DDEAF File Offset: 0x000DC0AF
		public HAPassiveCopyUnhealthyException(string copyState, Exception innerException) : base(Strings.HAPassiveCopyUnhealthy(copyState), innerException)
		{
			this.copyState = copyState;
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000DDEC5 File Offset: 0x000DC0C5
		protected HAPassiveCopyUnhealthyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.copyState = (string)info.GetValue("copyState", typeof(string));
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000DDEEF File Offset: 0x000DC0EF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("copyState", this.copyState);
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x000DDF0A File Offset: 0x000DC10A
		public string CopyState
		{
			get
			{
				return this.copyState;
			}
		}

		// Token: 0x04001C77 RID: 7287
		private readonly string copyState;
	}
}
