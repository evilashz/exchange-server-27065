using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001B1 RID: 433
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MowaGrammarException : LocalizedException
	{
		// Token: 0x06000EA1 RID: 3745 RVA: 0x00035579 File Offset: 0x00033779
		public MowaGrammarException(string exceptionText) : base(Strings.MowaGrammarException(exceptionText))
		{
			this.exceptionText = exceptionText;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003558E File Offset: 0x0003378E
		public MowaGrammarException(string exceptionText, Exception innerException) : base(Strings.MowaGrammarException(exceptionText), innerException)
		{
			this.exceptionText = exceptionText;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000355A4 File Offset: 0x000337A4
		protected MowaGrammarException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exceptionText = (string)info.GetValue("exceptionText", typeof(string));
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000355CE File Offset: 0x000337CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exceptionText", this.exceptionText);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x000355E9 File Offset: 0x000337E9
		public string ExceptionText
		{
			get
			{
				return this.exceptionText;
			}
		}

		// Token: 0x0400078D RID: 1933
		private readonly string exceptionText;
	}
}
