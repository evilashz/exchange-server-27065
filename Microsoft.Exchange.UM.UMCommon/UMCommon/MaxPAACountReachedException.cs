using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001D5 RID: 469
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MaxPAACountReachedException : StoragePermanentException
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x00036397 File Offset: 0x00034597
		public MaxPAACountReachedException(int count) : base(Strings.ErrorMaxPAACountReached(count))
		{
			this.count = count;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x000363AC File Offset: 0x000345AC
		public MaxPAACountReachedException(int count, Exception innerException) : base(Strings.ErrorMaxPAACountReached(count), innerException)
		{
			this.count = count;
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x000363C2 File Offset: 0x000345C2
		protected MaxPAACountReachedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.count = (int)info.GetValue("count", typeof(int));
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x000363EC File Offset: 0x000345EC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("count", this.count);
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00036407 File Offset: 0x00034607
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x040007A6 RID: 1958
		private readonly int count;
	}
}
