using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001035 RID: 4149
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdUserNotFoundException : LocalizedException
	{
		// Token: 0x0600AFC0 RID: 44992 RVA: 0x00294D01 File Offset: 0x00292F01
		public AdUserNotFoundException(string userPrincipalName, string errorMessage) : base(Strings.ErrorAdUserNotFound(userPrincipalName, errorMessage))
		{
			this.userPrincipalName = userPrincipalName;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AFC1 RID: 44993 RVA: 0x00294D1E File Offset: 0x00292F1E
		public AdUserNotFoundException(string userPrincipalName, string errorMessage, Exception innerException) : base(Strings.ErrorAdUserNotFound(userPrincipalName, errorMessage), innerException)
		{
			this.userPrincipalName = userPrincipalName;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AFC2 RID: 44994 RVA: 0x00294D3C File Offset: 0x00292F3C
		protected AdUserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userPrincipalName = (string)info.GetValue("userPrincipalName", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600AFC3 RID: 44995 RVA: 0x00294D91 File Offset: 0x00292F91
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userPrincipalName", this.userPrincipalName);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17003809 RID: 14345
		// (get) Token: 0x0600AFC4 RID: 44996 RVA: 0x00294DBD File Offset: 0x00292FBD
		public string UserPrincipalName
		{
			get
			{
				return this.userPrincipalName;
			}
		}

		// Token: 0x1700380A RID: 14346
		// (get) Token: 0x0600AFC5 RID: 44997 RVA: 0x00294DC5 File Offset: 0x00292FC5
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400616F RID: 24943
		private readonly string userPrincipalName;

		// Token: 0x04006170 RID: 24944
		private readonly string errorMessage;
	}
}
