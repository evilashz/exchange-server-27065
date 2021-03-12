using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006BF RID: 1727
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADServerNotSuitableException : ADOperationException
	{
		// Token: 0x06004FB9 RID: 20409 RVA: 0x0012661C File Offset: 0x0012481C
		public ADServerNotSuitableException(LocalizedString message, string serverFqdn) : this(message)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("serverFqdn", serverFqdn);
			this.ServerFqdn = serverFqdn;
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x00126637 File Offset: 0x00124837
		public ADServerNotSuitableException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x00126640 File Offset: 0x00124840
		public ADServerNotSuitableException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x0012664A File Offset: 0x0012484A
		protected ADServerNotSuitableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x06004FBD RID: 20413 RVA: 0x00126654 File Offset: 0x00124854
		// (set) Token: 0x06004FBE RID: 20414 RVA: 0x0012665C File Offset: 0x0012485C
		internal string ServerFqdn { get; set; }

		// Token: 0x06004FBF RID: 20415 RVA: 0x00126665 File Offset: 0x00124865
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
