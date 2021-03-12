using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EC6 RID: 3782
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnknownRequestIndexPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8B6 RID: 43190 RVA: 0x0028A6F4 File Offset: 0x002888F4
		public UnknownRequestIndexPermanentException(string indexId) : base(Strings.ErrorUnknownRequestIndex(indexId))
		{
			this.indexId = indexId;
		}

		// Token: 0x0600A8B7 RID: 43191 RVA: 0x0028A709 File Offset: 0x00288909
		public UnknownRequestIndexPermanentException(string indexId, Exception innerException) : base(Strings.ErrorUnknownRequestIndex(indexId), innerException)
		{
			this.indexId = indexId;
		}

		// Token: 0x0600A8B8 RID: 43192 RVA: 0x0028A71F File Offset: 0x0028891F
		protected UnknownRequestIndexPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.indexId = (string)info.GetValue("indexId", typeof(string));
		}

		// Token: 0x0600A8B9 RID: 43193 RVA: 0x0028A749 File Offset: 0x00288949
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("indexId", this.indexId);
		}

		// Token: 0x170036BB RID: 14011
		// (get) Token: 0x0600A8BA RID: 43194 RVA: 0x0028A764 File Offset: 0x00288964
		public string IndexId
		{
			get
			{
				return this.indexId;
			}
		}

		// Token: 0x04006021 RID: 24609
		private readonly string indexId;
	}
}
