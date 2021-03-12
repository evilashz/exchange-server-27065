using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001B3 RID: 435
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserConfigurationException : LocalizedException
	{
		// Token: 0x06000EAB RID: 3755 RVA: 0x00035669 File Offset: 0x00033869
		public UserConfigurationException(string exceptionText) : base(Strings.UserConfigurationException(exceptionText))
		{
			this.exceptionText = exceptionText;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003567E File Offset: 0x0003387E
		public UserConfigurationException(string exceptionText, Exception innerException) : base(Strings.UserConfigurationException(exceptionText), innerException)
		{
			this.exceptionText = exceptionText;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00035694 File Offset: 0x00033894
		protected UserConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exceptionText = (string)info.GetValue("exceptionText", typeof(string));
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x000356BE File Offset: 0x000338BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exceptionText", this.exceptionText);
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x000356D9 File Offset: 0x000338D9
		public string ExceptionText
		{
			get
			{
				return this.exceptionText;
			}
		}

		// Token: 0x0400078F RID: 1935
		private readonly string exceptionText;
	}
}
