using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005B2 RID: 1458
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AssistantsNotRunningToCompletionException : LocalizedException
	{
		// Token: 0x06002712 RID: 10002 RVA: 0x000DE636 File Offset: 0x000DC836
		public AssistantsNotRunningToCompletionException(string error) : base(Strings.AssistantsNotRunningToCompletionError(error))
		{
			this.error = error;
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000DE64B File Offset: 0x000DC84B
		public AssistantsNotRunningToCompletionException(string error, Exception innerException) : base(Strings.AssistantsNotRunningToCompletionError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x000DE661 File Offset: 0x000DC861
		protected AssistantsNotRunningToCompletionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000DE68B File Offset: 0x000DC88B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002716 RID: 10006 RVA: 0x000DE6A6 File Offset: 0x000DC8A6
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001C86 RID: 7302
		private readonly string error;
	}
}
