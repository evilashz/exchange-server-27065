using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005B1 RID: 1457
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InfrastructureValidationException : LocalizedException
	{
		// Token: 0x0600270D RID: 9997 RVA: 0x000DE5BE File Offset: 0x000DC7BE
		public InfrastructureValidationException(string error) : base(Strings.InfrastructureValidationError(error))
		{
			this.error = error;
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000DE5D3 File Offset: 0x000DC7D3
		public InfrastructureValidationException(string error, Exception innerException) : base(Strings.InfrastructureValidationError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x000DE5E9 File Offset: 0x000DC7E9
		protected InfrastructureValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000DE613 File Offset: 0x000DC813
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x000DE62E File Offset: 0x000DC82E
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001C85 RID: 7301
		private readonly string error;
	}
}
