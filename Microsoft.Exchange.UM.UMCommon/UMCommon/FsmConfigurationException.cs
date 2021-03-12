using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001B0 RID: 432
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FsmConfigurationException : LocalizedException
	{
		// Token: 0x06000E9C RID: 3740 RVA: 0x00035501 File Offset: 0x00033701
		public FsmConfigurationException(string exceptionText) : base(Strings.FsmConfigurationException(exceptionText))
		{
			this.exceptionText = exceptionText;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00035516 File Offset: 0x00033716
		public FsmConfigurationException(string exceptionText, Exception innerException) : base(Strings.FsmConfigurationException(exceptionText), innerException)
		{
			this.exceptionText = exceptionText;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003552C File Offset: 0x0003372C
		protected FsmConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exceptionText = (string)info.GetValue("exceptionText", typeof(string));
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00035556 File Offset: 0x00033756
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exceptionText", this.exceptionText);
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00035571 File Offset: 0x00033771
		public string ExceptionText
		{
			get
			{
				return this.exceptionText;
			}
		}

		// Token: 0x0400078C RID: 1932
		private readonly string exceptionText;
	}
}
