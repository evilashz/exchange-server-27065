using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001123 RID: 4387
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidEndpointParameterException : LocalizedException
	{
		// Token: 0x0600B49B RID: 46235 RVA: 0x0029D032 File Offset: 0x0029B232
		public InvalidEndpointParameterException(string parameterName, string type, LocalizedString reason) : base(Strings.ErrorInvalidEndpointParameter(parameterName, type, reason))
		{
			this.parameterName = parameterName;
			this.type = type;
			this.reason = reason;
		}

		// Token: 0x0600B49C RID: 46236 RVA: 0x0029D057 File Offset: 0x0029B257
		public InvalidEndpointParameterException(string parameterName, string type, LocalizedString reason, Exception innerException) : base(Strings.ErrorInvalidEndpointParameter(parameterName, type, reason), innerException)
		{
			this.parameterName = parameterName;
			this.type = type;
			this.reason = reason;
		}

		// Token: 0x0600B49D RID: 46237 RVA: 0x0029D080 File Offset: 0x0029B280
		protected InvalidEndpointParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parameterName = (string)info.GetValue("parameterName", typeof(string));
			this.type = (string)info.GetValue("type", typeof(string));
			this.reason = (LocalizedString)info.GetValue("reason", typeof(LocalizedString));
		}

		// Token: 0x0600B49E RID: 46238 RVA: 0x0029D0F8 File Offset: 0x0029B2F8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parameterName", this.parameterName);
			info.AddValue("type", this.type);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x1700392C RID: 14636
		// (get) Token: 0x0600B49F RID: 46239 RVA: 0x0029D145 File Offset: 0x0029B345
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x1700392D RID: 14637
		// (get) Token: 0x0600B4A0 RID: 46240 RVA: 0x0029D14D File Offset: 0x0029B34D
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700392E RID: 14638
		// (get) Token: 0x0600B4A1 RID: 46241 RVA: 0x0029D155 File Offset: 0x0029B355
		public LocalizedString Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04006292 RID: 25234
		private readonly string parameterName;

		// Token: 0x04006293 RID: 25235
		private readonly string type;

		// Token: 0x04006294 RID: 25236
		private readonly LocalizedString reason;
	}
}
