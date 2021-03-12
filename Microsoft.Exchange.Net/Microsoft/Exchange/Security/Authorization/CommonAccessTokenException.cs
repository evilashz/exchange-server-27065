using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x020000C9 RID: 201
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CommonAccessTokenException : LocalizedException
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x00012E71 File Offset: 0x00011071
		public CommonAccessTokenException(int version, LocalizedString reason) : base(AuthorizationStrings.CommonAccessTokenException(version, reason))
		{
			this.version = version;
			this.reason = reason;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00012E8E File Offset: 0x0001108E
		public CommonAccessTokenException(int version, LocalizedString reason, Exception innerException) : base(AuthorizationStrings.CommonAccessTokenException(version, reason), innerException)
		{
			this.version = version;
			this.reason = reason;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00012EAC File Offset: 0x000110AC
		protected CommonAccessTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.version = (int)info.GetValue("version", typeof(int));
			this.reason = (LocalizedString)info.GetValue("reason", typeof(LocalizedString));
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00012F01 File Offset: 0x00011101
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("version", this.version);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00012F32 File Offset: 0x00011132
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00012F3A File Offset: 0x0001113A
		public LocalizedString Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000416 RID: 1046
		private readonly int version;

		// Token: 0x04000417 RID: 1047
		private readonly LocalizedString reason;
	}
}
