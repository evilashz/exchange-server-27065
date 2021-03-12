using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000EAA RID: 3754
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceAndTargetDialPlanURITypeMismatchException : LocalizedException
	{
		// Token: 0x0600A824 RID: 43044 RVA: 0x002897AA File Offset: 0x002879AA
		public SourceAndTargetDialPlanURITypeMismatchException(string sourceUriType, string targetUriType) : base(Strings.SourceAndTargetDialPlanURITypeMismatch(sourceUriType, targetUriType))
		{
			this.sourceUriType = sourceUriType;
			this.targetUriType = targetUriType;
		}

		// Token: 0x0600A825 RID: 43045 RVA: 0x002897C7 File Offset: 0x002879C7
		public SourceAndTargetDialPlanURITypeMismatchException(string sourceUriType, string targetUriType, Exception innerException) : base(Strings.SourceAndTargetDialPlanURITypeMismatch(sourceUriType, targetUriType), innerException)
		{
			this.sourceUriType = sourceUriType;
			this.targetUriType = targetUriType;
		}

		// Token: 0x0600A826 RID: 43046 RVA: 0x002897E8 File Offset: 0x002879E8
		protected SourceAndTargetDialPlanURITypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sourceUriType = (string)info.GetValue("sourceUriType", typeof(string));
			this.targetUriType = (string)info.GetValue("targetUriType", typeof(string));
		}

		// Token: 0x0600A827 RID: 43047 RVA: 0x0028983D File Offset: 0x00287A3D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sourceUriType", this.sourceUriType);
			info.AddValue("targetUriType", this.targetUriType);
		}

		// Token: 0x17003699 RID: 13977
		// (get) Token: 0x0600A828 RID: 43048 RVA: 0x00289869 File Offset: 0x00287A69
		public string SourceUriType
		{
			get
			{
				return this.sourceUriType;
			}
		}

		// Token: 0x1700369A RID: 13978
		// (get) Token: 0x0600A829 RID: 43049 RVA: 0x00289871 File Offset: 0x00287A71
		public string TargetUriType
		{
			get
			{
				return this.targetUriType;
			}
		}

		// Token: 0x04005FFF RID: 24575
		private readonly string sourceUriType;

		// Token: 0x04006000 RID: 24576
		private readonly string targetUriType;
	}
}
