using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000D7 RID: 215
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusCommonValidationFailedException : LocalizedException
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x0001C252 File Offset: 0x0001A452
		public ClusCommonValidationFailedException(string error) : base(Strings.ClusCommonValidationFailedException(error))
		{
			this.error = error;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001C267 File Offset: 0x0001A467
		public ClusCommonValidationFailedException(string error, Exception innerException) : base(Strings.ClusCommonValidationFailedException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001C27D File Offset: 0x0001A47D
		protected ClusCommonValidationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001C2A7 File Offset: 0x0001A4A7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001C2C2 File Offset: 0x0001A4C2
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000730 RID: 1840
		private readonly string error;
	}
}
