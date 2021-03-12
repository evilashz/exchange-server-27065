using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000445 RID: 1093
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeedDivergenceFailedException : SeederServerException
	{
		// Token: 0x06002AE7 RID: 10983 RVA: 0x000BC6F4 File Offset: 0x000BA8F4
		public SeedDivergenceFailedException(string targetCopyName, string divergenceFileName, string errorMsg) : base(ReplayStrings.SeedDivergenceFailedException(targetCopyName, divergenceFileName, errorMsg))
		{
			this.targetCopyName = targetCopyName;
			this.divergenceFileName = divergenceFileName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000BC71E File Offset: 0x000BA91E
		public SeedDivergenceFailedException(string targetCopyName, string divergenceFileName, string errorMsg, Exception innerException) : base(ReplayStrings.SeedDivergenceFailedException(targetCopyName, divergenceFileName, errorMsg), innerException)
		{
			this.targetCopyName = targetCopyName;
			this.divergenceFileName = divergenceFileName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000BC74C File Offset: 0x000BA94C
		protected SeedDivergenceFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.targetCopyName = (string)info.GetValue("targetCopyName", typeof(string));
			this.divergenceFileName = (string)info.GetValue("divergenceFileName", typeof(string));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000BC7C1 File Offset: 0x000BA9C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("targetCopyName", this.targetCopyName);
			info.AddValue("divergenceFileName", this.divergenceFileName);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x000BC7FE File Offset: 0x000BA9FE
		public string TargetCopyName
		{
			get
			{
				return this.targetCopyName;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000BC806 File Offset: 0x000BAA06
		public string DivergenceFileName
		{
			get
			{
				return this.divergenceFileName;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x000BC80E File Offset: 0x000BAA0E
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x0400147A RID: 5242
		private readonly string targetCopyName;

		// Token: 0x0400147B RID: 5243
		private readonly string divergenceFileName;

		// Token: 0x0400147C RID: 5244
		private readonly string errorMsg;
	}
}
