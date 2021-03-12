using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000232 RID: 562
	internal sealed class SuccessfulCreateFolderResult : RopResult
	{
		// Token: 0x06000C47 RID: 3143 RVA: 0x00026F40 File Offset: 0x00025140
		internal SuccessfulCreateFolderResult(IServerObject serverObject, StoreId folderId, bool existed, bool hasRules, ReplicaServerInfo? replicaInfo) : base(RopId.CreateFolder, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
			this.FolderId = folderId;
			this.Existed = existed;
			this.HasRules = hasRules;
			this.replicaInfo = replicaInfo;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00026F78 File Offset: 0x00025178
		internal SuccessfulCreateFolderResult(Reader reader) : base(reader)
		{
			this.FolderId = StoreId.Parse(reader);
			this.Existed = reader.ReadBool();
			if (this.Existed)
			{
				this.HasRules = reader.ReadBool();
				bool flag = reader.ReadBool();
				if (flag)
				{
					this.replicaInfo = new ReplicaServerInfo?(ReplicaServerInfo.Parse(reader));
				}
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00026FD4 File Offset: 0x000251D4
		internal bool IsGhosted
		{
			get
			{
				return this.replicaInfo != null;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00026FEF File Offset: 0x000251EF
		internal ReplicaServerInfo? ReplicaInfo
		{
			get
			{
				return this.replicaInfo;
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00026FF7 File Offset: 0x000251F7
		internal static RopResult Parse(Reader reader)
		{
			return new SuccessfulCreateFolderResult(reader);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00027000 File Offset: 0x00025200
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.FolderId.Serialize(writer);
			writer.WriteBool(this.Existed, 1);
			if (this.Existed)
			{
				writer.WriteBool(this.HasRules, 1);
				writer.WriteBool(this.IsGhosted, 1);
				if (this.IsGhosted)
				{
					this.replicaInfo.Value.Serialize(writer);
				}
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00027074 File Offset: 0x00025274
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" ID=").Append(this.FolderId.ToString());
			stringBuilder.Append(" Existed=").Append(this.Existed);
			stringBuilder.Append(" Has Rules=").Append(this.HasRules);
			stringBuilder.Append(" Ghosted=").Append(this.IsGhosted);
			if (this.IsGhosted)
			{
				stringBuilder.Append(" ReplicaInfo=").Append(this.replicaInfo.Value.ToString());
			}
		}

		// Token: 0x040006C6 RID: 1734
		internal readonly StoreId FolderId;

		// Token: 0x040006C7 RID: 1735
		internal readonly bool Existed;

		// Token: 0x040006C8 RID: 1736
		internal readonly bool HasRules;

		// Token: 0x040006C9 RID: 1737
		private readonly ReplicaServerInfo? replicaInfo;
	}
}
