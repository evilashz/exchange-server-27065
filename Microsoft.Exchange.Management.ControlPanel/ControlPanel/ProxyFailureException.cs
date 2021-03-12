using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200000D RID: 13
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ProxyFailureException : ProxyException
	{
		// Token: 0x0600184F RID: 6223 RVA: 0x0004B4A5 File Offset: 0x000496A5
		public ProxyFailureException(Uri proxyTarget, ADObjectId user) : base(Strings.ProxyFailure(proxyTarget, user))
		{
			this.proxyTarget = proxyTarget;
			this.user = user;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0004B4C2 File Offset: 0x000496C2
		public ProxyFailureException(Uri proxyTarget, ADObjectId user, Exception innerException) : base(Strings.ProxyFailure(proxyTarget, user), innerException)
		{
			this.proxyTarget = proxyTarget;
			this.user = user;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0004B4E0 File Offset: 0x000496E0
		protected ProxyFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.proxyTarget = (Uri)info.GetValue("proxyTarget", typeof(Uri));
			this.user = (ADObjectId)info.GetValue("user", typeof(ADObjectId));
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0004B535 File Offset: 0x00049735
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("proxyTarget", this.proxyTarget);
			info.AddValue("user", this.user);
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0004B561 File Offset: 0x00049761
		public Uri ProxyTarget
		{
			get
			{
				return this.proxyTarget;
			}
		}

		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x0004B569 File Offset: 0x00049769
		public ADObjectId User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x0400184D RID: 6221
		private readonly Uri proxyTarget;

		// Token: 0x0400184E RID: 6222
		private readonly ADObjectId user;
	}
}
