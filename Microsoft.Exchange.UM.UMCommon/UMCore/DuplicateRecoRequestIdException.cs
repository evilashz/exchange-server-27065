using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000211 RID: 529
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DuplicateRecoRequestIdException : MobileRecoInvalidRequestException
	{
		// Token: 0x0600110D RID: 4365 RVA: 0x00039883 File Offset: 0x00037A83
		public DuplicateRecoRequestIdException(Guid id) : base(Strings.DuplicateRecoRequestId(id))
		{
			this.id = id;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00039898 File Offset: 0x00037A98
		public DuplicateRecoRequestIdException(Guid id, Exception innerException) : base(Strings.DuplicateRecoRequestId(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000398AE File Offset: 0x00037AAE
		protected DuplicateRecoRequestIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (Guid)info.GetValue("id", typeof(Guid));
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x000398D8 File Offset: 0x00037AD8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x000398F8 File Offset: 0x00037AF8
		public Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04000887 RID: 2183
		private readonly Guid id;
	}
}
