using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010F5 RID: 4341
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToGenerateSharedKeyException : LocalizedException
	{
		// Token: 0x0600B3B3 RID: 46003 RVA: 0x0029B967 File Offset: 0x00299B67
		public FailedToGenerateSharedKeyException(Exception ex) : base(Strings.FailedToGenerateSharedKey(ex))
		{
			this.ex = ex;
		}

		// Token: 0x0600B3B4 RID: 46004 RVA: 0x0029B97C File Offset: 0x00299B7C
		public FailedToGenerateSharedKeyException(Exception ex, Exception innerException) : base(Strings.FailedToGenerateSharedKey(ex), innerException)
		{
			this.ex = ex;
		}

		// Token: 0x0600B3B5 RID: 46005 RVA: 0x0029B992 File Offset: 0x00299B92
		protected FailedToGenerateSharedKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B3B6 RID: 46006 RVA: 0x0029B9BC File Offset: 0x00299BBC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x170038FC RID: 14588
		// (get) Token: 0x0600B3B7 RID: 46007 RVA: 0x0029B9D7 File Offset: 0x00299BD7
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x04006262 RID: 25186
		private readonly Exception ex;
	}
}
