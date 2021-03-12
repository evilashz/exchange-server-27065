using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001B4 RID: 436
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADUMUserInvalidUMMailboxPolicyException : LocalizedException
	{
		// Token: 0x06000EB0 RID: 3760 RVA: 0x000356E1 File Offset: 0x000338E1
		public ADUMUserInvalidUMMailboxPolicyException(string useraddress) : base(Strings.ADUMUserInvalidUMMailboxPolicyException(useraddress))
		{
			this.useraddress = useraddress;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x000356F6 File Offset: 0x000338F6
		public ADUMUserInvalidUMMailboxPolicyException(string useraddress, Exception innerException) : base(Strings.ADUMUserInvalidUMMailboxPolicyException(useraddress), innerException)
		{
			this.useraddress = useraddress;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003570C File Offset: 0x0003390C
		protected ADUMUserInvalidUMMailboxPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.useraddress = (string)info.GetValue("useraddress", typeof(string));
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00035736 File Offset: 0x00033936
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("useraddress", this.useraddress);
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00035751 File Offset: 0x00033951
		public string Useraddress
		{
			get
			{
				return this.useraddress;
			}
		}

		// Token: 0x04000790 RID: 1936
		private readonly string useraddress;
	}
}
