using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005AE RID: 1454
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParseDiagnosticsStringException : LocalizedException
	{
		// Token: 0x060026FE RID: 9982 RVA: 0x000DE456 File Offset: 0x000DC656
		public ParseDiagnosticsStringException(string error) : base(Strings.ParseDiagnosticsStringError(error))
		{
			this.error = error;
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000DE46B File Offset: 0x000DC66B
		public ParseDiagnosticsStringException(string error, Exception innerException) : base(Strings.ParseDiagnosticsStringError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000DE481 File Offset: 0x000DC681
		protected ParseDiagnosticsStringException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000DE4AB File Offset: 0x000DC6AB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x000DE4C6 File Offset: 0x000DC6C6
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001C82 RID: 7298
		private readonly string error;
	}
}
