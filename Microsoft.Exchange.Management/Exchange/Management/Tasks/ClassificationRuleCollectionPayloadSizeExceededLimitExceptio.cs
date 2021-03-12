using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FE4 RID: 4068
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionPayloadSizeExceededLimitException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE3A RID: 44602 RVA: 0x00292ABE File Offset: 0x00290CBE
		public ClassificationRuleCollectionPayloadSizeExceededLimitException(ulong inputSize, ulong limitSize) : base(Strings.ClassificationRuleCollectionPayloadSizeExceededLimitFailure(inputSize, limitSize))
		{
			this.inputSize = inputSize;
			this.limitSize = limitSize;
		}

		// Token: 0x0600AE3B RID: 44603 RVA: 0x00292ADB File Offset: 0x00290CDB
		public ClassificationRuleCollectionPayloadSizeExceededLimitException(ulong inputSize, ulong limitSize, Exception innerException) : base(Strings.ClassificationRuleCollectionPayloadSizeExceededLimitFailure(inputSize, limitSize), innerException)
		{
			this.inputSize = inputSize;
			this.limitSize = limitSize;
		}

		// Token: 0x0600AE3C RID: 44604 RVA: 0x00292AFC File Offset: 0x00290CFC
		protected ClassificationRuleCollectionPayloadSizeExceededLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.inputSize = (ulong)info.GetValue("inputSize", typeof(ulong));
			this.limitSize = (ulong)info.GetValue("limitSize", typeof(ulong));
		}

		// Token: 0x0600AE3D RID: 44605 RVA: 0x00292B51 File Offset: 0x00290D51
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("inputSize", this.inputSize);
			info.AddValue("limitSize", this.limitSize);
		}

		// Token: 0x170037C7 RID: 14279
		// (get) Token: 0x0600AE3E RID: 44606 RVA: 0x00292B7D File Offset: 0x00290D7D
		public ulong InputSize
		{
			get
			{
				return this.inputSize;
			}
		}

		// Token: 0x170037C8 RID: 14280
		// (get) Token: 0x0600AE3F RID: 44607 RVA: 0x00292B85 File Offset: 0x00290D85
		public ulong LimitSize
		{
			get
			{
				return this.limitSize;
			}
		}

		// Token: 0x0400612D RID: 24877
		private readonly ulong inputSize;

		// Token: 0x0400612E RID: 24878
		private readonly ulong limitSize;
	}
}
