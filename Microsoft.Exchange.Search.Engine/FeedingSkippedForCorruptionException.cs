using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Mdb;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x0200001B RID: 27
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FeedingSkippedForCorruptionException : ComponentFailedTransientException
	{
		// Token: 0x060000FF RID: 255 RVA: 0x000083EE File Offset: 0x000065EE
		public FeedingSkippedForCorruptionException(MdbInfo mdbInfo, ContentIndexStatusType state, IndexStatusErrorCode indexStatusErrorCode, int? failureCode, string failureReason) : base(Strings.FeedingSkippedWithFailureCode(mdbInfo, state, indexStatusErrorCode, failureCode, failureReason))
		{
			this.mdbInfo = mdbInfo;
			this.state = state;
			this.indexStatusErrorCode = indexStatusErrorCode;
			this.failureCode = failureCode;
			this.failureReason = failureReason;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008427 File Offset: 0x00006627
		public FeedingSkippedForCorruptionException(MdbInfo mdbInfo, ContentIndexStatusType state, IndexStatusErrorCode indexStatusErrorCode, int? failureCode, string failureReason, Exception innerException) : base(Strings.FeedingSkippedWithFailureCode(mdbInfo, state, indexStatusErrorCode, failureCode, failureReason), innerException)
		{
			this.mdbInfo = mdbInfo;
			this.state = state;
			this.indexStatusErrorCode = indexStatusErrorCode;
			this.failureCode = failureCode;
			this.failureReason = failureReason;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008464 File Offset: 0x00006664
		protected FeedingSkippedForCorruptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbInfo = (MdbInfo)info.GetValue("mdbInfo", typeof(MdbInfo));
			this.state = (ContentIndexStatusType)info.GetValue("state", typeof(ContentIndexStatusType));
			this.indexStatusErrorCode = (IndexStatusErrorCode)info.GetValue("indexStatusErrorCode", typeof(IndexStatusErrorCode));
			this.failureCode = (int?)info.GetValue("failureCode", typeof(int?));
			this.failureReason = (string)info.GetValue("failureReason", typeof(string));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000851C File Offset: 0x0000671C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbInfo", this.mdbInfo);
			info.AddValue("state", this.state);
			info.AddValue("indexStatusErrorCode", this.indexStatusErrorCode);
			info.AddValue("failureCode", this.failureCode);
			info.AddValue("failureReason", this.failureReason);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00008595 File Offset: 0x00006795
		public MdbInfo MdbInfo
		{
			get
			{
				return this.mdbInfo;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000859D File Offset: 0x0000679D
		public ContentIndexStatusType State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000085A5 File Offset: 0x000067A5
		public IndexStatusErrorCode IndexStatusErrorCode
		{
			get
			{
				return this.indexStatusErrorCode;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000085AD File Offset: 0x000067AD
		public int? FailureCode
		{
			get
			{
				return this.failureCode;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000085B5 File Offset: 0x000067B5
		public string FailureReason
		{
			get
			{
				return this.failureReason;
			}
		}

		// Token: 0x0400007F RID: 127
		private readonly MdbInfo mdbInfo;

		// Token: 0x04000080 RID: 128
		private readonly ContentIndexStatusType state;

		// Token: 0x04000081 RID: 129
		private readonly IndexStatusErrorCode indexStatusErrorCode;

		// Token: 0x04000082 RID: 130
		private readonly int? failureCode;

		// Token: 0x04000083 RID: 131
		private readonly string failureReason;
	}
}
