using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001EC RID: 492
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConfigurationException : LocalizedException
	{
		// Token: 0x06001062 RID: 4194 RVA: 0x00038B1D File Offset: 0x00036D1D
		public ConfigurationException(string msg) : base(Strings.ConfigurationException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00038B32 File Offset: 0x00036D32
		public ConfigurationException(string msg, Exception innerException) : base(Strings.ConfigurationException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00038B48 File Offset: 0x00036D48
		protected ConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00038B72 File Offset: 0x00036D72
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00038B8D File Offset: 0x00036D8D
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000870 RID: 2160
		private readonly string msg;
	}
}
