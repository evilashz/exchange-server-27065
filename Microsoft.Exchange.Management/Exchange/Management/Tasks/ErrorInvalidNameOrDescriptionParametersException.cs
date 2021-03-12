using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FF1 RID: 4081
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorInvalidNameOrDescriptionParametersException : LocalizedException
	{
		// Token: 0x0600AE78 RID: 44664 RVA: 0x00293005 File Offset: 0x00291205
		public ErrorInvalidNameOrDescriptionParametersException(string parameterName, int length, int maxLength) : base(Strings.ErrorInvalidNameOrDescriptionParameters(parameterName, length, maxLength))
		{
			this.parameterName = parameterName;
			this.length = length;
			this.maxLength = maxLength;
		}

		// Token: 0x0600AE79 RID: 44665 RVA: 0x0029302A File Offset: 0x0029122A
		public ErrorInvalidNameOrDescriptionParametersException(string parameterName, int length, int maxLength, Exception innerException) : base(Strings.ErrorInvalidNameOrDescriptionParameters(parameterName, length, maxLength), innerException)
		{
			this.parameterName = parameterName;
			this.length = length;
			this.maxLength = maxLength;
		}

		// Token: 0x0600AE7A RID: 44666 RVA: 0x00293054 File Offset: 0x00291254
		protected ErrorInvalidNameOrDescriptionParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parameterName = (string)info.GetValue("parameterName", typeof(string));
			this.length = (int)info.GetValue("length", typeof(int));
			this.maxLength = (int)info.GetValue("maxLength", typeof(int));
		}

		// Token: 0x0600AE7B RID: 44667 RVA: 0x002930C9 File Offset: 0x002912C9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parameterName", this.parameterName);
			info.AddValue("length", this.length);
			info.AddValue("maxLength", this.maxLength);
		}

		// Token: 0x170037D1 RID: 14289
		// (get) Token: 0x0600AE7C RID: 44668 RVA: 0x00293106 File Offset: 0x00291306
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x170037D2 RID: 14290
		// (get) Token: 0x0600AE7D RID: 44669 RVA: 0x0029310E File Offset: 0x0029130E
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170037D3 RID: 14291
		// (get) Token: 0x0600AE7E RID: 44670 RVA: 0x00293116 File Offset: 0x00291316
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x04006137 RID: 24887
		private readonly string parameterName;

		// Token: 0x04006138 RID: 24888
		private readonly int length;

		// Token: 0x04006139 RID: 24889
		private readonly int maxLength;
	}
}
