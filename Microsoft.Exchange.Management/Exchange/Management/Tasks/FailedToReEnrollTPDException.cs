using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010E7 RID: 4327
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToReEnrollTPDException : LocalizedException
	{
		// Token: 0x0600B371 RID: 45937 RVA: 0x0029B3D4 File Offset: 0x002995D4
		public FailedToReEnrollTPDException(Exception e) : base(Strings.FailedToReEnrollTPD(e))
		{
			this.e = e;
		}

		// Token: 0x0600B372 RID: 45938 RVA: 0x0029B3E9 File Offset: 0x002995E9
		public FailedToReEnrollTPDException(Exception e, Exception innerException) : base(Strings.FailedToReEnrollTPD(e), innerException)
		{
			this.e = e;
		}

		// Token: 0x0600B373 RID: 45939 RVA: 0x0029B3FF File Offset: 0x002995FF
		protected FailedToReEnrollTPDException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.e = (Exception)info.GetValue("e", typeof(Exception));
		}

		// Token: 0x0600B374 RID: 45940 RVA: 0x0029B429 File Offset: 0x00299629
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("e", this.e);
		}

		// Token: 0x170038F2 RID: 14578
		// (get) Token: 0x0600B375 RID: 45941 RVA: 0x0029B444 File Offset: 0x00299644
		public Exception E
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x04006258 RID: 25176
		private readonly Exception e;
	}
}
