using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000181 RID: 385
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidSensitiveInformationParameterNameException : InvalidContentContainsSensitiveInformationException
	{
		// Token: 0x06000F7A RID: 3962 RVA: 0x000364AC File Offset: 0x000346AC
		public InvalidSensitiveInformationParameterNameException(string invalidParameter) : base(Strings.InvalidSensitiveInformationParameterName(invalidParameter))
		{
			this.invalidParameter = invalidParameter;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x000364C1 File Offset: 0x000346C1
		public InvalidSensitiveInformationParameterNameException(string invalidParameter, Exception innerException) : base(Strings.InvalidSensitiveInformationParameterName(invalidParameter), innerException)
		{
			this.invalidParameter = invalidParameter;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x000364D7 File Offset: 0x000346D7
		protected InvalidSensitiveInformationParameterNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.invalidParameter = (string)info.GetValue("invalidParameter", typeof(string));
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00036501 File Offset: 0x00034701
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("invalidParameter", this.invalidParameter);
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0003651C File Offset: 0x0003471C
		public string InvalidParameter
		{
			get
			{
				return this.invalidParameter;
			}
		}

		// Token: 0x04000682 RID: 1666
		private readonly string invalidParameter;
	}
}
