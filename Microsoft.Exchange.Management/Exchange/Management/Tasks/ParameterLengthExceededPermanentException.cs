using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ED3 RID: 3795
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParameterLengthExceededPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8F7 RID: 43255 RVA: 0x0028AD28 File Offset: 0x00288F28
		public ParameterLengthExceededPermanentException(string parameterName, int maxValueLength) : base(Strings.ErrorMaxParameterLengthExceeded(parameterName, maxValueLength))
		{
			this.parameterName = parameterName;
			this.maxValueLength = maxValueLength;
		}

		// Token: 0x0600A8F8 RID: 43256 RVA: 0x0028AD45 File Offset: 0x00288F45
		public ParameterLengthExceededPermanentException(string parameterName, int maxValueLength, Exception innerException) : base(Strings.ErrorMaxParameterLengthExceeded(parameterName, maxValueLength), innerException)
		{
			this.parameterName = parameterName;
			this.maxValueLength = maxValueLength;
		}

		// Token: 0x0600A8F9 RID: 43257 RVA: 0x0028AD64 File Offset: 0x00288F64
		protected ParameterLengthExceededPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parameterName = (string)info.GetValue("parameterName", typeof(string));
			this.maxValueLength = (int)info.GetValue("maxValueLength", typeof(int));
		}

		// Token: 0x0600A8FA RID: 43258 RVA: 0x0028ADB9 File Offset: 0x00288FB9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parameterName", this.parameterName);
			info.AddValue("maxValueLength", this.maxValueLength);
		}

		// Token: 0x170036C8 RID: 14024
		// (get) Token: 0x0600A8FB RID: 43259 RVA: 0x0028ADE5 File Offset: 0x00288FE5
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x170036C9 RID: 14025
		// (get) Token: 0x0600A8FC RID: 43260 RVA: 0x0028ADED File Offset: 0x00288FED
		public int MaxValueLength
		{
			get
			{
				return this.maxValueLength;
			}
		}

		// Token: 0x0400602E RID: 24622
		private readonly string parameterName;

		// Token: 0x0400602F RID: 24623
		private readonly int maxValueLength;
	}
}
