using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000213 RID: 531
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidRecoRequestIdException : MobileRecoInvalidRequestException
	{
		// Token: 0x06001117 RID: 4375 RVA: 0x0003997D File Offset: 0x00037B7D
		public InvalidRecoRequestIdException(Guid id) : base(Strings.InvalidRecoRequestId(id))
		{
			this.id = id;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00039992 File Offset: 0x00037B92
		public InvalidRecoRequestIdException(Guid id, Exception innerException) : base(Strings.InvalidRecoRequestId(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000399A8 File Offset: 0x00037BA8
		protected InvalidRecoRequestIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (Guid)info.GetValue("id", typeof(Guid));
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000399D2 File Offset: 0x00037BD2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x000399F2 File Offset: 0x00037BF2
		public Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04000889 RID: 2185
		private readonly Guid id;
	}
}
