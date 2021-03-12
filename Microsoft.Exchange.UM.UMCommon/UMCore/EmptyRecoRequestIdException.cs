using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000212 RID: 530
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EmptyRecoRequestIdException : MobileRecoInvalidRequestException
	{
		// Token: 0x06001112 RID: 4370 RVA: 0x00039900 File Offset: 0x00037B00
		public EmptyRecoRequestIdException(Guid id) : base(Strings.EmptyRecoRequestId(id))
		{
			this.id = id;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00039915 File Offset: 0x00037B15
		public EmptyRecoRequestIdException(Guid id, Exception innerException) : base(Strings.EmptyRecoRequestId(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003992B File Offset: 0x00037B2B
		protected EmptyRecoRequestIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (Guid)info.GetValue("id", typeof(Guid));
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00039955 File Offset: 0x00037B55
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00039975 File Offset: 0x00037B75
		public Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04000888 RID: 2184
		private readonly Guid id;
	}
}
