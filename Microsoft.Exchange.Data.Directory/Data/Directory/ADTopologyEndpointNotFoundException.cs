using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ABD RID: 2749
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADTopologyEndpointNotFoundException : ADTransientException
	{
		// Token: 0x0600806D RID: 32877 RVA: 0x001A5351 File Offset: 0x001A3551
		public ADTopologyEndpointNotFoundException(string url) : base(DirectoryStrings.ADTopologyEndpointNotFoundException(url))
		{
			this.url = url;
		}

		// Token: 0x0600806E RID: 32878 RVA: 0x001A5366 File Offset: 0x001A3566
		public ADTopologyEndpointNotFoundException(string url, Exception innerException) : base(DirectoryStrings.ADTopologyEndpointNotFoundException(url), innerException)
		{
			this.url = url;
		}

		// Token: 0x0600806F RID: 32879 RVA: 0x001A537C File Offset: 0x001A357C
		protected ADTopologyEndpointNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.url = (string)info.GetValue("url", typeof(string));
		}

		// Token: 0x06008070 RID: 32880 RVA: 0x001A53A6 File Offset: 0x001A35A6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("url", this.url);
		}

		// Token: 0x17002ED0 RID: 11984
		// (get) Token: 0x06008071 RID: 32881 RVA: 0x001A53C1 File Offset: 0x001A35C1
		public string Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x040055AA RID: 21930
		private readonly string url;
	}
}
