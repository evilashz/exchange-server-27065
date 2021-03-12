using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005B3 RID: 1459
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AssistantsActiveDatabaseException : LocalizedException
	{
		// Token: 0x06002717 RID: 10007 RVA: 0x000DE6AE File Offset: 0x000DC8AE
		public AssistantsActiveDatabaseException(string error) : base(Strings.AssistantsActiveDatabaseError(error))
		{
			this.error = error;
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000DE6C3 File Offset: 0x000DC8C3
		public AssistantsActiveDatabaseException(string error, Exception innerException) : base(Strings.AssistantsActiveDatabaseError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000DE6D9 File Offset: 0x000DC8D9
		protected AssistantsActiveDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000DE703 File Offset: 0x000DC903
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x000DE71E File Offset: 0x000DC91E
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001C87 RID: 7303
		private readonly string error;
	}
}
