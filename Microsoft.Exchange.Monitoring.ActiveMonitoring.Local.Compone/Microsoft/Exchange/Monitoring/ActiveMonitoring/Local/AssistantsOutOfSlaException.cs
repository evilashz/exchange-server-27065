using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005AF RID: 1455
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AssistantsOutOfSlaException : LocalizedException
	{
		// Token: 0x06002703 RID: 9987 RVA: 0x000DE4CE File Offset: 0x000DC6CE
		public AssistantsOutOfSlaException(string error) : base(Strings.AssistantsOutOfSlaError(error))
		{
			this.error = error;
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000DE4E3 File Offset: 0x000DC6E3
		public AssistantsOutOfSlaException(string error, Exception innerException) : base(Strings.AssistantsOutOfSlaError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000DE4F9 File Offset: 0x000DC6F9
		protected AssistantsOutOfSlaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000DE523 File Offset: 0x000DC723
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002707 RID: 9991 RVA: 0x000DE53E File Offset: 0x000DC73E
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001C83 RID: 7299
		private readonly string error;
	}
}
