using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010B1 RID: 4273
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidApplicationUriException : FederationException
	{
		// Token: 0x0600B266 RID: 45670 RVA: 0x00299A7F File Offset: 0x00297C7F
		public InvalidApplicationUriException(string uri) : base(Strings.ErrorInvalidApplicationUri(uri))
		{
			this.uri = uri;
		}

		// Token: 0x0600B267 RID: 45671 RVA: 0x00299A94 File Offset: 0x00297C94
		public InvalidApplicationUriException(string uri, Exception innerException) : base(Strings.ErrorInvalidApplicationUri(uri), innerException)
		{
			this.uri = uri;
		}

		// Token: 0x0600B268 RID: 45672 RVA: 0x00299AAA File Offset: 0x00297CAA
		protected InvalidApplicationUriException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.uri = (string)info.GetValue("uri", typeof(string));
		}

		// Token: 0x0600B269 RID: 45673 RVA: 0x00299AD4 File Offset: 0x00297CD4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("uri", this.uri);
		}

		// Token: 0x170038BF RID: 14527
		// (get) Token: 0x0600B26A RID: 45674 RVA: 0x00299AEF File Offset: 0x00297CEF
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x04006225 RID: 25125
		private readonly string uri;
	}
}
