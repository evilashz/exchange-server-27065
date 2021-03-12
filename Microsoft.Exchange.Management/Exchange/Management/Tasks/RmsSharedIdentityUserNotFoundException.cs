using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E21 RID: 3617
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RmsSharedIdentityUserNotFoundException : LocalizedException
	{
		// Token: 0x0600A5AB RID: 42411 RVA: 0x002866FA File Offset: 0x002848FA
		public RmsSharedIdentityUserNotFoundException(string userCn) : base(Strings.RmsSharedIdentityUserNotFound(userCn))
		{
			this.userCn = userCn;
		}

		// Token: 0x0600A5AC RID: 42412 RVA: 0x0028670F File Offset: 0x0028490F
		public RmsSharedIdentityUserNotFoundException(string userCn, Exception innerException) : base(Strings.RmsSharedIdentityUserNotFound(userCn), innerException)
		{
			this.userCn = userCn;
		}

		// Token: 0x0600A5AD RID: 42413 RVA: 0x00286725 File Offset: 0x00284925
		protected RmsSharedIdentityUserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userCn = (string)info.GetValue("userCn", typeof(string));
		}

		// Token: 0x0600A5AE RID: 42414 RVA: 0x0028674F File Offset: 0x0028494F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userCn", this.userCn);
		}

		// Token: 0x17003644 RID: 13892
		// (get) Token: 0x0600A5AF RID: 42415 RVA: 0x0028676A File Offset: 0x0028496A
		public string UserCn
		{
			get
			{
				return this.userCn;
			}
		}

		// Token: 0x04005FAA RID: 24490
		private readonly string userCn;
	}
}
