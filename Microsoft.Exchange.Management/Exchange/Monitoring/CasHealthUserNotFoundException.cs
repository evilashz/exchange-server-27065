using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F0C RID: 3852
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthUserNotFoundException : LocalizedException
	{
		// Token: 0x0600AA25 RID: 43557 RVA: 0x0028CDCD File Offset: 0x0028AFCD
		public CasHealthUserNotFoundException(string userPrincipalName, string errorString) : base(Strings.CasHealthUserNotFound(userPrincipalName, errorString))
		{
			this.userPrincipalName = userPrincipalName;
			this.errorString = errorString;
		}

		// Token: 0x0600AA26 RID: 43558 RVA: 0x0028CDEA File Offset: 0x0028AFEA
		public CasHealthUserNotFoundException(string userPrincipalName, string errorString, Exception innerException) : base(Strings.CasHealthUserNotFound(userPrincipalName, errorString), innerException)
		{
			this.userPrincipalName = userPrincipalName;
			this.errorString = errorString;
		}

		// Token: 0x0600AA27 RID: 43559 RVA: 0x0028CE08 File Offset: 0x0028B008
		protected CasHealthUserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userPrincipalName = (string)info.GetValue("userPrincipalName", typeof(string));
			this.errorString = (string)info.GetValue("errorString", typeof(string));
		}

		// Token: 0x0600AA28 RID: 43560 RVA: 0x0028CE5D File Offset: 0x0028B05D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userPrincipalName", this.userPrincipalName);
			info.AddValue("errorString", this.errorString);
		}

		// Token: 0x17003712 RID: 14098
		// (get) Token: 0x0600AA29 RID: 43561 RVA: 0x0028CE89 File Offset: 0x0028B089
		public string UserPrincipalName
		{
			get
			{
				return this.userPrincipalName;
			}
		}

		// Token: 0x17003713 RID: 14099
		// (get) Token: 0x0600AA2A RID: 43562 RVA: 0x0028CE91 File Offset: 0x0028B091
		public string ErrorString
		{
			get
			{
				return this.errorString;
			}
		}

		// Token: 0x04006078 RID: 24696
		private readonly string userPrincipalName;

		// Token: 0x04006079 RID: 24697
		private readonly string errorString;
	}
}
