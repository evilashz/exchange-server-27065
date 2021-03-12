using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Mdb;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x0200001A RID: 26
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FeedingSkippedException : ComponentFailedTransientException
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x000082C0 File Offset: 0x000064C0
		public FeedingSkippedException(MdbInfo mdbInfo, ContentIndexStatusType state, IndexStatusErrorCode indexStatusErrorCode) : base(Strings.FeedingSkipped(mdbInfo, state, indexStatusErrorCode))
		{
			this.mdbInfo = mdbInfo;
			this.state = state;
			this.indexStatusErrorCode = indexStatusErrorCode;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000082E5 File Offset: 0x000064E5
		public FeedingSkippedException(MdbInfo mdbInfo, ContentIndexStatusType state, IndexStatusErrorCode indexStatusErrorCode, Exception innerException) : base(Strings.FeedingSkipped(mdbInfo, state, indexStatusErrorCode), innerException)
		{
			this.mdbInfo = mdbInfo;
			this.state = state;
			this.indexStatusErrorCode = indexStatusErrorCode;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000830C File Offset: 0x0000650C
		protected FeedingSkippedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbInfo = (MdbInfo)info.GetValue("mdbInfo", typeof(MdbInfo));
			this.state = (ContentIndexStatusType)info.GetValue("state", typeof(ContentIndexStatusType));
			this.indexStatusErrorCode = (IndexStatusErrorCode)info.GetValue("indexStatusErrorCode", typeof(IndexStatusErrorCode));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008384 File Offset: 0x00006584
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbInfo", this.mdbInfo);
			info.AddValue("state", this.state);
			info.AddValue("indexStatusErrorCode", this.indexStatusErrorCode);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000083D6 File Offset: 0x000065D6
		public MdbInfo MdbInfo
		{
			get
			{
				return this.mdbInfo;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000083DE File Offset: 0x000065DE
		public ContentIndexStatusType State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000083E6 File Offset: 0x000065E6
		public IndexStatusErrorCode IndexStatusErrorCode
		{
			get
			{
				return this.indexStatusErrorCode;
			}
		}

		// Token: 0x0400007C RID: 124
		private readonly MdbInfo mdbInfo;

		// Token: 0x0400007D RID: 125
		private readonly ContentIndexStatusType state;

		// Token: 0x0400007E RID: 126
		private readonly IndexStatusErrorCode indexStatusErrorCode;
	}
}
