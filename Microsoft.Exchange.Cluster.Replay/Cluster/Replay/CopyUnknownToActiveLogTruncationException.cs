using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004C2 RID: 1218
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CopyUnknownToActiveLogTruncationException : LogTruncationException
	{
		// Token: 0x06002DA2 RID: 11682 RVA: 0x000C1D42 File Offset: 0x000BFF42
		public CopyUnknownToActiveLogTruncationException(string db, string activeNode, string targetNode, uint hresult) : base(ReplayStrings.CopyUnknownToActiveLogTruncationException(db, activeNode, targetNode, hresult))
		{
			this.db = db;
			this.activeNode = activeNode;
			this.targetNode = targetNode;
			this.hresult = hresult;
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000C1D76 File Offset: 0x000BFF76
		public CopyUnknownToActiveLogTruncationException(string db, string activeNode, string targetNode, uint hresult, Exception innerException) : base(ReplayStrings.CopyUnknownToActiveLogTruncationException(db, activeNode, targetNode, hresult), innerException)
		{
			this.db = db;
			this.activeNode = activeNode;
			this.targetNode = targetNode;
			this.hresult = hresult;
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000C1DAC File Offset: 0x000BFFAC
		protected CopyUnknownToActiveLogTruncationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.db = (string)info.GetValue("db", typeof(string));
			this.activeNode = (string)info.GetValue("activeNode", typeof(string));
			this.targetNode = (string)info.GetValue("targetNode", typeof(string));
			this.hresult = (uint)info.GetValue("hresult", typeof(uint));
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000C1E44 File Offset: 0x000C0044
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("db", this.db);
			info.AddValue("activeNode", this.activeNode);
			info.AddValue("targetNode", this.targetNode);
			info.AddValue("hresult", this.hresult);
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x000C1E9D File Offset: 0x000C009D
		public string Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x000C1EA5 File Offset: 0x000C00A5
		public string ActiveNode
		{
			get
			{
				return this.activeNode;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x000C1EAD File Offset: 0x000C00AD
		public string TargetNode
		{
			get
			{
				return this.targetNode;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x000C1EB5 File Offset: 0x000C00B5
		public uint Hresult
		{
			get
			{
				return this.hresult;
			}
		}

		// Token: 0x04001541 RID: 5441
		private readonly string db;

		// Token: 0x04001542 RID: 5442
		private readonly string activeNode;

		// Token: 0x04001543 RID: 5443
		private readonly string targetNode;

		// Token: 0x04001544 RID: 5444
		private readonly uint hresult;
	}
}
