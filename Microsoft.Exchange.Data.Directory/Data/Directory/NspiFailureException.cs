using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AA7 RID: 2727
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NspiFailureException : DataSourceOperationException
	{
		// Token: 0x0600800B RID: 32779 RVA: 0x001A4C78 File Offset: 0x001A2E78
		public NspiFailureException(int status) : base(DirectoryStrings.NspiFailureException(status))
		{
			this.status = status;
		}

		// Token: 0x0600800C RID: 32780 RVA: 0x001A4C8D File Offset: 0x001A2E8D
		public NspiFailureException(int status, Exception innerException) : base(DirectoryStrings.NspiFailureException(status), innerException)
		{
			this.status = status;
		}

		// Token: 0x0600800D RID: 32781 RVA: 0x001A4CA3 File Offset: 0x001A2EA3
		protected NspiFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.status = (int)info.GetValue("status", typeof(int));
		}

		// Token: 0x0600800E RID: 32782 RVA: 0x001A4CCD File Offset: 0x001A2ECD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("status", this.status);
		}

		// Token: 0x17002EC6 RID: 11974
		// (get) Token: 0x0600800F RID: 32783 RVA: 0x001A4CE8 File Offset: 0x001A2EE8
		public int Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x040055A0 RID: 21920
		private readonly int status;
	}
}
