using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000013 RID: 19
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class BadQueryParameterException : LocalizedException
	{
		// Token: 0x0600186D RID: 6253 RVA: 0x0004B787 File Offset: 0x00049987
		public BadQueryParameterException(string queryParam) : base(Strings.BadQueryParameterError(queryParam))
		{
			this.queryParam = queryParam;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0004B79C File Offset: 0x0004999C
		public BadQueryParameterException(string queryParam, Exception innerException) : base(Strings.BadQueryParameterError(queryParam), innerException)
		{
			this.queryParam = queryParam;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0004B7B2 File Offset: 0x000499B2
		protected BadQueryParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.queryParam = (string)info.GetValue("queryParam", typeof(string));
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0004B7DC File Offset: 0x000499DC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("queryParam", this.queryParam);
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0004B7F7 File Offset: 0x000499F7
		public string QueryParam
		{
			get
			{
				return this.queryParam;
			}
		}

		// Token: 0x04001853 RID: 6227
		private readonly string queryParam;
	}
}
