using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000215 RID: 533
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UserNotFoundException : LocalizedException
	{
		// Token: 0x06001122 RID: 4386 RVA: 0x00039AC9 File Offset: 0x00037CC9
		public UserNotFoundException(Guid id) : base(Strings.UserNotFoundException(id))
		{
			this.id = id;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00039ADE File Offset: 0x00037CDE
		public UserNotFoundException(Guid id, Exception innerException) : base(Strings.UserNotFoundException(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00039AF4 File Offset: 0x00037CF4
		protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (Guid)info.GetValue("id", typeof(Guid));
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00039B1E File Offset: 0x00037D1E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00039B3E File Offset: 0x00037D3E
		public Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x0400088C RID: 2188
		private readonly Guid id;
	}
}
