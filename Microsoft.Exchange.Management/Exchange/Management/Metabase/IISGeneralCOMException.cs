using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000DDD RID: 3549
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IISGeneralCOMException : DataSourceOperationException
	{
		// Token: 0x0600A43E RID: 42046 RVA: 0x00283E46 File Offset: 0x00282046
		public IISGeneralCOMException(string errorMsg, int code) : base(Strings.IISGeneralCOMException(errorMsg, code))
		{
			this.errorMsg = errorMsg;
			this.code = code;
		}

		// Token: 0x0600A43F RID: 42047 RVA: 0x00283E63 File Offset: 0x00282063
		public IISGeneralCOMException(string errorMsg, int code, Exception innerException) : base(Strings.IISGeneralCOMException(errorMsg, code), innerException)
		{
			this.errorMsg = errorMsg;
			this.code = code;
		}

		// Token: 0x0600A440 RID: 42048 RVA: 0x00283E84 File Offset: 0x00282084
		protected IISGeneralCOMException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
			this.code = (int)info.GetValue("code", typeof(int));
		}

		// Token: 0x0600A441 RID: 42049 RVA: 0x00283ED9 File Offset: 0x002820D9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
			info.AddValue("code", this.code);
		}

		// Token: 0x170035E7 RID: 13799
		// (get) Token: 0x0600A442 RID: 42050 RVA: 0x00283F05 File Offset: 0x00282105
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x170035E8 RID: 13800
		// (get) Token: 0x0600A443 RID: 42051 RVA: 0x00283F0D File Offset: 0x0028210D
		public int Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x04005F4D RID: 24397
		private readonly string errorMsg;

		// Token: 0x04005F4E RID: 24398
		private readonly int code;
	}
}
