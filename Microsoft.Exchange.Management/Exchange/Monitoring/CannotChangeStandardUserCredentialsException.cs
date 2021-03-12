using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F1F RID: 3871
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotChangeStandardUserCredentialsException : LocalizedException
	{
		// Token: 0x0600AA8C RID: 43660 RVA: 0x0028D97E File Offset: 0x0028BB7E
		public CannotChangeStandardUserCredentialsException(string username) : base(Strings.CannotChangeStandardUserCredentials(username))
		{
			this.username = username;
		}

		// Token: 0x0600AA8D RID: 43661 RVA: 0x0028D993 File Offset: 0x0028BB93
		public CannotChangeStandardUserCredentialsException(string username, Exception innerException) : base(Strings.CannotChangeStandardUserCredentials(username), innerException)
		{
			this.username = username;
		}

		// Token: 0x0600AA8E RID: 43662 RVA: 0x0028D9A9 File Offset: 0x0028BBA9
		protected CannotChangeStandardUserCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.username = (string)info.GetValue("username", typeof(string));
		}

		// Token: 0x0600AA8F RID: 43663 RVA: 0x0028D9D3 File Offset: 0x0028BBD3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("username", this.username);
		}

		// Token: 0x1700372D RID: 14125
		// (get) Token: 0x0600AA90 RID: 43664 RVA: 0x0028D9EE File Offset: 0x0028BBEE
		public string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x04006093 RID: 24723
		private readonly string username;
	}
}
