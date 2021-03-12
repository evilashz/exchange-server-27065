using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200020F RID: 527
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MobileRecoRPCShutdownException : MobileRecoRequestCannotBeHandledException
	{
		// Token: 0x06001104 RID: 4356 RVA: 0x000397DF File Offset: 0x000379DF
		public MobileRecoRPCShutdownException(Guid id) : base(Strings.MobileRecoRPCShutdownException(id))
		{
			this.id = id;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000397F4 File Offset: 0x000379F4
		public MobileRecoRPCShutdownException(Guid id, Exception innerException) : base(Strings.MobileRecoRPCShutdownException(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0003980A File Offset: 0x00037A0A
		protected MobileRecoRPCShutdownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (Guid)info.GetValue("id", typeof(Guid));
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00039834 File Offset: 0x00037A34
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x00039854 File Offset: 0x00037A54
		public Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04000886 RID: 2182
		private readonly Guid id;
	}
}
