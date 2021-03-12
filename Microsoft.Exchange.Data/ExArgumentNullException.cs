using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000E1 RID: 225
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ExArgumentNullException : LocalizedException
	{
		// Token: 0x06000802 RID: 2050 RVA: 0x0001ADDD File Offset: 0x00018FDD
		public ExArgumentNullException(string paramName) : base(DataStrings.ExArgumentNullException(paramName))
		{
			this.paramName = paramName;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001ADF2 File Offset: 0x00018FF2
		public ExArgumentNullException(string paramName, Exception innerException) : base(DataStrings.ExArgumentNullException(paramName), innerException)
		{
			this.paramName = paramName;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001AE08 File Offset: 0x00019008
		protected ExArgumentNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.paramName = (string)info.GetValue("paramName", typeof(string));
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001AE32 File Offset: 0x00019032
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("paramName", this.paramName);
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0001AE4D File Offset: 0x0001904D
		public string ParamName
		{
			get
			{
				return this.paramName;
			}
		}

		// Token: 0x04000584 RID: 1412
		private readonly string paramName;
	}
}
