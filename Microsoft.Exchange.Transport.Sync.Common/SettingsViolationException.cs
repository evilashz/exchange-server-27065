using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000031 RID: 49
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SettingsViolationException : LocalizedException
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000585A File Offset: 0x00003A5A
		public SettingsViolationException(int statusCode) : base(Strings.RequestSettingsException(statusCode))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000586F File Offset: 0x00003A6F
		public SettingsViolationException(int statusCode, Exception innerException) : base(Strings.RequestSettingsException(statusCode), innerException)
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005885 File Offset: 0x00003A85
		protected SettingsViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.statusCode = (int)info.GetValue("statusCode", typeof(int));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000058AF File Offset: 0x00003AAF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("statusCode", this.statusCode);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000058CA File Offset: 0x00003ACA
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x040000E8 RID: 232
		private readonly int statusCode;
	}
}
