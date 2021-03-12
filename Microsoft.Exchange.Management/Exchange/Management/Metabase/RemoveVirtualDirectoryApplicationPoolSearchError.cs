using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FAF RID: 4015
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoveVirtualDirectoryApplicationPoolSearchError : LocalizedException
	{
		// Token: 0x0600AD3A RID: 44346 RVA: 0x00291438 File Offset: 0x0028F638
		public RemoveVirtualDirectoryApplicationPoolSearchError(string applicationPool, int hresult) : base(Strings.RemoveVirtualDirectoryApplicationPoolSearchError(applicationPool, hresult))
		{
			this.applicationPool = applicationPool;
			this.hresult = hresult;
		}

		// Token: 0x0600AD3B RID: 44347 RVA: 0x00291455 File Offset: 0x0028F655
		public RemoveVirtualDirectoryApplicationPoolSearchError(string applicationPool, int hresult, Exception innerException) : base(Strings.RemoveVirtualDirectoryApplicationPoolSearchError(applicationPool, hresult), innerException)
		{
			this.applicationPool = applicationPool;
			this.hresult = hresult;
		}

		// Token: 0x0600AD3C RID: 44348 RVA: 0x00291474 File Offset: 0x0028F674
		protected RemoveVirtualDirectoryApplicationPoolSearchError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.applicationPool = (string)info.GetValue("applicationPool", typeof(string));
			this.hresult = (int)info.GetValue("hresult", typeof(int));
		}

		// Token: 0x0600AD3D RID: 44349 RVA: 0x002914C9 File Offset: 0x0028F6C9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("applicationPool", this.applicationPool);
			info.AddValue("hresult", this.hresult);
		}

		// Token: 0x1700379B RID: 14235
		// (get) Token: 0x0600AD3E RID: 44350 RVA: 0x002914F5 File Offset: 0x0028F6F5
		public string ApplicationPool
		{
			get
			{
				return this.applicationPool;
			}
		}

		// Token: 0x1700379C RID: 14236
		// (get) Token: 0x0600AD3F RID: 44351 RVA: 0x002914FD File Offset: 0x0028F6FD
		public int Hresult
		{
			get
			{
				return this.hresult;
			}
		}

		// Token: 0x04006101 RID: 24833
		private readonly string applicationPool;

		// Token: 0x04006102 RID: 24834
		private readonly int hresult;
	}
}
