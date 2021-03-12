using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x020001C5 RID: 453
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PublishingException : LocalizedException
	{
		// Token: 0x06000EFF RID: 3839 RVA: 0x00035D73 File Offset: 0x00033F73
		public PublishingException(LocalizedString msg) : base(Strings.PublishingException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00035D88 File Offset: 0x00033F88
		public PublishingException(LocalizedString msg, Exception innerException) : base(Strings.PublishingException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00035D9E File Offset: 0x00033F9E
		protected PublishingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (LocalizedString)info.GetValue("msg", typeof(LocalizedString));
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00035DC8 File Offset: 0x00033FC8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00035DE8 File Offset: 0x00033FE8
		public LocalizedString Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x0400079B RID: 1947
		private readonly LocalizedString msg;
	}
}
