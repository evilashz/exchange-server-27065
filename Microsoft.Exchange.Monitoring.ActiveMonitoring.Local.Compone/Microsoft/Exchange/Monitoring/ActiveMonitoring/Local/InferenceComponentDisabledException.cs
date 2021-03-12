using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x0200059D RID: 1437
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InferenceComponentDisabledException : LocalizedException
	{
		// Token: 0x060026AE RID: 9902 RVA: 0x000DDD95 File Offset: 0x000DBF95
		public InferenceComponentDisabledException(string details) : base(Strings.InferenceComponentDisabled(details))
		{
			this.details = details;
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000DDDAA File Offset: 0x000DBFAA
		public InferenceComponentDisabledException(string details, Exception innerException) : base(Strings.InferenceComponentDisabled(details), innerException)
		{
			this.details = details;
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000DDDC0 File Offset: 0x000DBFC0
		protected InferenceComponentDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.details = (string)info.GetValue("details", typeof(string));
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000DDDEA File Offset: 0x000DBFEA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("details", this.details);
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x000DDE05 File Offset: 0x000DC005
		public string Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x04001C76 RID: 7286
		private readonly string details;
	}
}
