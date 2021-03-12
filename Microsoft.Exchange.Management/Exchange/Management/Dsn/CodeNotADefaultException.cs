using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Dsn
{
	// Token: 0x02000F43 RID: 3907
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CodeNotADefaultException : LocalizedException
	{
		// Token: 0x0600AB49 RID: 43849 RVA: 0x0028ED80 File Offset: 0x0028CF80
		public CodeNotADefaultException(EnhancedStatusCode code) : base(Strings.DsnCodeNotADefault(code))
		{
			this.code = code;
		}

		// Token: 0x0600AB4A RID: 43850 RVA: 0x0028ED95 File Offset: 0x0028CF95
		public CodeNotADefaultException(EnhancedStatusCode code, Exception innerException) : base(Strings.DsnCodeNotADefault(code), innerException)
		{
			this.code = code;
		}

		// Token: 0x0600AB4B RID: 43851 RVA: 0x0028EDAB File Offset: 0x0028CFAB
		protected CodeNotADefaultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.code = (EnhancedStatusCode)info.GetValue("code", typeof(EnhancedStatusCode));
		}

		// Token: 0x0600AB4C RID: 43852 RVA: 0x0028EDD5 File Offset: 0x0028CFD5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("code", this.code);
		}

		// Token: 0x1700375A RID: 14170
		// (get) Token: 0x0600AB4D RID: 43853 RVA: 0x0028EDF0 File Offset: 0x0028CFF0
		public EnhancedStatusCode Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x040060C0 RID: 24768
		private readonly EnhancedStatusCode code;
	}
}
