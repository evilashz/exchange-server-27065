using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Core.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000035 RID: 53
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UserTokenException : LocalizedException
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00007634 File Offset: 0x00005834
		public UserTokenException(string reason) : base(Strings.UserTokenException(reason))
		{
			this.reason = reason;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007649 File Offset: 0x00005849
		public UserTokenException(string reason, Exception innerException) : base(Strings.UserTokenException(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000765F File Offset: 0x0000585F
		protected UserTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007689 File Offset: 0x00005889
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000076A4 File Offset: 0x000058A4
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040000CA RID: 202
		private readonly string reason;
	}
}
