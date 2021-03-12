using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000A5 RID: 165
	[Serializable]
	public class EsentErrorException : EsentException
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x000112ED File Offset: 0x0000F4ED
		internal EsentErrorException(string message, JET_err err) : base(message)
		{
			this.errorCode = err;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000112FD File Offset: 0x0000F4FD
		protected EsentErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorCode = (JET_err)info.GetInt32("errorCode");
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00011318 File Offset: 0x0000F518
		public JET_err Error
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00011320 File Offset: 0x0000F520
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("errorCode", this.errorCode, typeof(int));
			}
		}

		// Token: 0x04000339 RID: 825
		private JET_err errorCode;
	}
}
