using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000220 RID: 544
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UMServiceBaseException : LocalizedException
	{
		// Token: 0x06001156 RID: 4438 RVA: 0x00039F13 File Offset: 0x00038113
		public UMServiceBaseException(string exceptionText) : base(Strings.UMServiceBaseException(exceptionText))
		{
			this.exceptionText = exceptionText;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00039F28 File Offset: 0x00038128
		public UMServiceBaseException(string exceptionText, Exception innerException) : base(Strings.UMServiceBaseException(exceptionText), innerException)
		{
			this.exceptionText = exceptionText;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00039F3E File Offset: 0x0003813E
		protected UMServiceBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exceptionText = (string)info.GetValue("exceptionText", typeof(string));
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00039F68 File Offset: 0x00038168
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exceptionText", this.exceptionText);
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00039F83 File Offset: 0x00038183
		public string ExceptionText
		{
			get
			{
				return this.exceptionText;
			}
		}

		// Token: 0x04000894 RID: 2196
		private readonly string exceptionText;
	}
}
