using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F0D RID: 3853
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthMailboxNotFoundException : LocalizedException
	{
		// Token: 0x0600AA2B RID: 43563 RVA: 0x0028CE99 File Offset: 0x0028B099
		public CasHealthMailboxNotFoundException(string userPrincipalName) : base(Strings.CasHealthMailboxNotFound(userPrincipalName))
		{
			this.userPrincipalName = userPrincipalName;
		}

		// Token: 0x0600AA2C RID: 43564 RVA: 0x0028CEAE File Offset: 0x0028B0AE
		public CasHealthMailboxNotFoundException(string userPrincipalName, Exception innerException) : base(Strings.CasHealthMailboxNotFound(userPrincipalName), innerException)
		{
			this.userPrincipalName = userPrincipalName;
		}

		// Token: 0x0600AA2D RID: 43565 RVA: 0x0028CEC4 File Offset: 0x0028B0C4
		protected CasHealthMailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userPrincipalName = (string)info.GetValue("userPrincipalName", typeof(string));
		}

		// Token: 0x0600AA2E RID: 43566 RVA: 0x0028CEEE File Offset: 0x0028B0EE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userPrincipalName", this.userPrincipalName);
		}

		// Token: 0x17003714 RID: 14100
		// (get) Token: 0x0600AA2F RID: 43567 RVA: 0x0028CF09 File Offset: 0x0028B109
		public string UserPrincipalName
		{
			get
			{
				return this.userPrincipalName;
			}
		}

		// Token: 0x0400607A RID: 24698
		private readonly string userPrincipalName;
	}
}
