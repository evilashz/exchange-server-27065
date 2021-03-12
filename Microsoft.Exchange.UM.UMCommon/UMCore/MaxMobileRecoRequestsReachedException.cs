using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200020C RID: 524
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MaxMobileRecoRequestsReachedException : MobileRecoRequestCannotBeHandledException
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x000396B3 File Offset: 0x000378B3
		public MaxMobileRecoRequestsReachedException(int current, int max) : base(Strings.MaxMobileRecoRequestsReached(current, max))
		{
			this.current = current;
			this.max = max;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000396D0 File Offset: 0x000378D0
		public MaxMobileRecoRequestsReachedException(int current, int max, Exception innerException) : base(Strings.MaxMobileRecoRequestsReached(current, max), innerException)
		{
			this.current = current;
			this.max = max;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000396F0 File Offset: 0x000378F0
		protected MaxMobileRecoRequestsReachedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.current = (int)info.GetValue("current", typeof(int));
			this.max = (int)info.GetValue("max", typeof(int));
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00039745 File Offset: 0x00037945
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("current", this.current);
			info.AddValue("max", this.max);
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00039771 File Offset: 0x00037971
		public int Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00039779 File Offset: 0x00037979
		public int Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x04000884 RID: 2180
		private readonly int current;

		// Token: 0x04000885 RID: 2181
		private readonly int max;
	}
}
