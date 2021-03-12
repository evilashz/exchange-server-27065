using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001D1 RID: 465
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PromptSynthesisException : LocalizedException
	{
		// Token: 0x06000F38 RID: 3896 RVA: 0x00036249 File Offset: 0x00034449
		public PromptSynthesisException(string info) : base(Strings.PromptSynthesisException(info))
		{
			this.info = info;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0003625E File Offset: 0x0003445E
		public PromptSynthesisException(string info, Exception innerException) : base(Strings.PromptSynthesisException(info), innerException)
		{
			this.info = info;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00036274 File Offset: 0x00034474
		protected PromptSynthesisException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.info = (string)info.GetValue("info", typeof(string));
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0003629E File Offset: 0x0003449E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("info", this.info);
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x000362B9 File Offset: 0x000344B9
		public string Info
		{
			get
			{
				return this.info;
			}
		}

		// Token: 0x040007A4 RID: 1956
		private readonly string info;
	}
}
