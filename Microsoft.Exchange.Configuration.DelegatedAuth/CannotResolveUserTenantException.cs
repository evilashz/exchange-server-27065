using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.DelegatedAuthentication.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x02000014 RID: 20
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotResolveUserTenantException : LocalizedException
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000476F File Offset: 0x0000296F
		public CannotResolveUserTenantException(string userId) : base(Strings.CannotResolveUserTenantException(userId))
		{
			this.userId = userId;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004784 File Offset: 0x00002984
		public CannotResolveUserTenantException(string userId, Exception innerException) : base(Strings.CannotResolveUserTenantException(userId), innerException)
		{
			this.userId = userId;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000479A File Offset: 0x0000299A
		protected CannotResolveUserTenantException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userId = (string)info.GetValue("userId", typeof(string));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000047C4 File Offset: 0x000029C4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userId", this.userId);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000047DF File Offset: 0x000029DF
		public string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x0400005A RID: 90
		private readonly string userId;
	}
}
