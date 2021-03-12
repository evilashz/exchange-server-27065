using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003E4 RID: 996
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class ServiceObjectId
	{
		// Token: 0x06001BF3 RID: 7155 RVA: 0x0009DB7A File Offset: 0x0009BD7A
		internal BaseServerIdInfo GetServerInfo(bool isHierarchicalOperation)
		{
			if (this.serverInfo == null)
			{
				this.InitServerInfo(isHierarchicalOperation);
			}
			return this.serverInfo;
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0009DB91 File Offset: 0x0009BD91
		internal void SetServerInfo(BaseServerIdInfo serverInfo)
		{
			this.serverInfo = serverInfo;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001BF5 RID: 7157
		internal abstract BasicTypes BasicType { get; }

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0009DB9A File Offset: 0x0009BD9A
		public ServiceObjectId()
		{
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0009DBA2 File Offset: 0x0009BDA2
		public virtual string GetId()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0009DBA9 File Offset: 0x0009BDA9
		public virtual string GetChangeKey()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x0009DBB0 File Offset: 0x0009BDB0
		protected virtual void InitServerInfo(bool isHierarchicalOperation)
		{
			CallContext callContext = CallContext.Current;
			IdHeaderInformation header = IdConverter.ConvertFromConcatenatedId(this.GetId(), this.BasicType, null, false);
			this.serverInfo = IdConverter.ServerInfoFromIdHeaderInformation(callContext, header, isHierarchicalOperation);
		}

		// Token: 0x04001267 RID: 4711
		private BaseServerIdInfo serverInfo;
	}
}
