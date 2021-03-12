using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000295 RID: 661
	public sealed class SyncContentsManifestState : XMLSerializableBase
	{
		// Token: 0x06002024 RID: 8228 RVA: 0x00044980 File Offset: 0x00042B80
		public SyncContentsManifestState()
		{
			uint idsGivenPropTag = FastTransferIcsState.IdsetGiven;
			this.folderId = null;
			this.data = null;
			this.idSetGiven = new Lazy<IdSet>(() => MapiStore.GetIdSetFromMapiManifestBlob((PropTag)idsGivenPropTag, this.Data));
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x000449D5 File Offset: 0x00042BD5
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x000449DD File Offset: 0x00042BDD
		[XmlElement]
		public byte[] FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x000449E6 File Offset: 0x00042BE6
		// (set) Token: 0x06002028 RID: 8232 RVA: 0x000449EE File Offset: 0x00042BEE
		[XmlElement]
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x000449F7 File Offset: 0x00042BF7
		[XmlIgnore]
		private IdSet IdSetGiven
		{
			get
			{
				return this.idSetGiven.Value;
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00044A04 File Offset: 0x00042C04
		public MemoryStream GetDataStream()
		{
			MemoryStream memoryStream = new MemoryStream(this.data.Length);
			memoryStream.Write(this.data, 0, this.data.Length);
			return memoryStream;
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00044A38 File Offset: 0x00042C38
		public bool IdSetGivenContainsEntryId(byte[] entryId)
		{
			try
			{
				if (this.Data == null)
				{
					MrsTracer.Common.Warning("Data not created to generate idset.  default to true", new object[0]);
					return true;
				}
				if (this.IdSetGiven == null)
				{
					MrsTracer.Common.Warning("Couldn't generate idset to check. default to true", new object[0]);
					return true;
				}
				return this.IdSetGiven.Contains(IdConverter.MessageGuidGlobCountFromEntryId(entryId));
			}
			catch (MapiPermanentException ex)
			{
				MrsTracer.Common.Warning("Couldn't generate idset to check. default to true {0}", new object[]
				{
					CommonUtils.FullExceptionMessage(ex)
				});
			}
			catch (MapiRetryableException ex2)
			{
				MrsTracer.Common.Warning("Couldn't generate idset to check. default to true {0}", new object[]
				{
					CommonUtils.FullExceptionMessage(ex2)
				});
			}
			return true;
		}

		// Token: 0x04000D08 RID: 3336
		private byte[] folderId;

		// Token: 0x04000D09 RID: 3337
		private byte[] data;

		// Token: 0x04000D0A RID: 3338
		private Lazy<IdSet> idSetGiven;
	}
}
