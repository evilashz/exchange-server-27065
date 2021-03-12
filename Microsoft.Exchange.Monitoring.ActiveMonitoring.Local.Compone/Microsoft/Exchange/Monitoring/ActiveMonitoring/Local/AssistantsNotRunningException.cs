using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005B0 RID: 1456
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AssistantsNotRunningException : LocalizedException
	{
		// Token: 0x06002708 RID: 9992 RVA: 0x000DE546 File Offset: 0x000DC746
		public AssistantsNotRunningException(string error) : base(Strings.AssistantsNotRunningError(error))
		{
			this.error = error;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000DE55B File Offset: 0x000DC75B
		public AssistantsNotRunningException(string error, Exception innerException) : base(Strings.AssistantsNotRunningError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000DE571 File Offset: 0x000DC771
		protected AssistantsNotRunningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000DE59B File Offset: 0x000DC79B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x000DE5B6 File Offset: 0x000DC7B6
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001C84 RID: 7300
		private readonly string error;
	}
}
